
using Feng.Net.Base;
using Feng.Net.Packets;
using System;
using System.Net;
using System.Net.Sockets;

namespace Feng.Net.Tcp
{


    public class TcpAsyncClientProxy : TcpClientProxyBase, IDisposable
    {
        public TcpAsyncClientProxy(Socket socket, NetServer server)
        {
            this._clientsocket = socket;
            this.InitServer(server);
            _conectiontime = DateTime.Now;
            this.Name = socket.RemoteEndPoint.ToString();
        }

        private Socket _clientsocket = null;
        public override string LocalIP
        {
            get
            {
                if (Client != null)
                {
                    IPEndPoint remote = Client.RemoteEndPoint as IPEndPoint;
                    if (remote != null)
                    {
                        return remote.Address.ToString();
                    }
                }
                return string.Empty;
            }
            set { }
        }
        public override int LoalPort
        {
            get
            {
                if (Client != null)
                {
                    IPEndPoint remote = Client.RemoteEndPoint as IPEndPoint;
                    if (remote != null)
                    {
                        return remote.Port;
                    }

                }
                return 0;
            }
            set { }
        }

        private DateTime _conectiontime = DateTime.MaxValue;
        public override DateTime ConnectionTime
        {
            get { return _conectiontime; }
        }
        public virtual Socket Client { get { return _clientsocket; } }

        object newcloseobje = new object();

        public event EventHandler Closed;

        public void OnClosed(EventArgs e)
        {
            if (Closed != null)
            {
                Closed(this, e);
            }
        }

        public override void Close()
        {
            try
            {
                lock (newcloseobje)
                {
                    if (this._clientsocket == null)
                        return;
                    Server.Remove(this);
                    try
                    {
                        this.Client.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception ex)
                    {
                        Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                    }
                    try
                    {
                        this.Client.Close();
                    }
                    catch (Exception ex)
                    {
                        Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                    }
                    this._clientsocket = null;
                    OnClosed(null);
                }
            }
            catch (Exception ex)
            {
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
            }

        }

        public override void Open()
        {
        }

        public override NetResult Respond(NetPacket ph)
        {
            ph.PacketMode = PacketMode.ANSWER;
            return SendToRemote(ph);
        }

        public override NetResult Post(NetPacket ph)
        {
            ph.PacketMode = PacketMode.POST;
            return SendToRemote(ph);
        }

        public override NetResult Send(NetPacket ph)
        {
            ph.PacketMode = PacketMode.Send;
            return SendToRemote(ph);
        }

        public override void ChangServer(NetPacket ph, string ip, int port)
        {

        }

        public override NetResult SendToRemote(NetPacket ph)
        {
            NetResult result = NetResult.SuccessResult();
            try
            {
                ph.Session = this.Session;
                byte[] data = ph.GetData();
                if (this.Client == null)
                {
                    return NetResult.ErrorResult("Client Is Null", NetResult.CODE_USER);
                }
                Server.OnBeforeSendData(data);
                data = this.GetSendData(data);
                Send(data);
                System.Threading.Thread.Sleep(10);
            }
            catch (Exception ex)
            {
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                result = NetResult.ErrorResult(ex.Message, NetResult.CODE_SYSTEM);
            }

            return result;
        }

        public override void Read()
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = Client;
                Client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
            }

        }

        public virtual void ReadCallback(IAsyncResult ar)
        {

            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    byte[] data = new byte[bytesRead];
                    System.Buffer.BlockCopy(state.buffer, 0, data, 0, bytesRead);
                    state.ReSetBuffer();
                    if (!this.Server.OnRecvData(data))
                    {
                        GetReceiveData(data);
                    } 
                    Read();
                }
                else
                {
                    Client.Send(new byte[0]);
                }

            }
            catch (Exception ex)
            {
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                this.Close();
            }

        }

        public override bool Send(byte[] data)
        {
            Client.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), Client);
            return false;
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception ex)
            {
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                this.Close();
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override void Dispose()
        {
            this.Close();
        }

    }
}
