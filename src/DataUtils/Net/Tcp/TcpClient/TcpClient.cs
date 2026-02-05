using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

using System.Net.NetworkInformation;
using Feng.Utils;
using Feng.Net.NetArgs;
using Feng.Net.Base;
using Feng.Net.Packets;
using Feng.Net.EventHandlers;

namespace Feng.Net.Tcp
{

    public partial class TcpClient : TcpClientProxyBase, IDisposable
    { 
        public TcpClient()
        {
            _id = Guid.NewGuid().ToString();
        }
  
        private bool _autoconnected = true;
        public virtual bool AutoConnected
        {
            get { return _autoconnected; }
            set { _autoconnected = value; }
        }
        private int _defaultwaitconnectiontime = 3;
        public virtual int DefaultWaitConnectionTime
        {
            get { return _defaultwaitconnectiontime; }
            set { _defaultwaitconnectiontime = value; }
        }
        private DateTime connectiontime = DateTime.MinValue;
        public override DateTime ConnectionTime
        {
            get { return connectiontime; }
        }
        private string _id = string.Empty;
        public override string ID { get { return _id; } set { this._id = value; } }

        Socket client;
        Thread autonconnectthread;
        object objnewstarting = new object();
        AutoResetEvent autoconnectedevent = new AutoResetEvent(true);
        public override void Open()
        { 
        }
        public override void Connection()
        {
            if (autonconnectthread == null)
            {
                System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
                autonconnectthread = new Thread(StartConnection);
                autonconnectthread.IsBackground = true;
                autonconnectthread.Start();
            }
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            AutoConnected = false;
            autoconnectedevent.Set();
        }

        private void StartConnection()
        {
            connectiontime = DateTime.Now;
            IPAddress ip = IPAddress.Parse(this.RemoteIP);
            while (this.AutoConnected)
            {
                try
                {
                    autoconnectedevent.Reset();

                    Feng.Utils.TraceHelper.WriteTrace("DataUtils", "TcpClient", "ConnectionServer", true, ""); 
                    this.HasConnected = false;  
                    client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint remoteEP = new IPEndPoint(ip, this.RemotePort);
                    client.SendBufferSize = (1024 * 1024 * 8);
                    client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);

                    lock (readonlyobject)
                    {
                        recvbuffer.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                }
                finally
                {
                    autoconnectedevent.WaitOne();
                    Thread.Sleep(1000 * DefaultWaitConnectionTime);
                }
            }
            autonconnectthread = null;
        }

        protected override void CloseSocket()
        {
            lock (this)
            {
                try
                {
                    if (client != null)
                    {
                        try
                        {
                            client.Shutdown(SocketShutdown.Both);
                        }
                        catch (System.Net.Sockets.SocketException ex)
                        {
                            Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                        }
                        catch (Exception ex)
                        {
                            Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                        }
                        HasConnected = false;
                        autoconnectedevent.Set();
                        client.Close();
                        client = null;
                        this.ClearCache();
                        OnClosedConnected();
                    }
                }
                catch (Exception ex)
                {
                    Feng.Utils.BugReport.Log(ex);
                }
            }
            base.CloseSocket();
        }

        public override void Close()
        {
            this.AutoConnected = false; 
            CloseSocket();
        } 

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                HasConnected = true;
                Read();
                IPEndPoint iPEndPoint = client.LocalEndPoint as IPEndPoint;
                if (iPEndPoint !=null)
                {
                    this.LocalIP = iPEndPoint.Address.ToString();
                    this.LoalPort = iPEndPoint.Port;
                }
                OnConnected();
                
            }
            catch (Exception ex)
            {
                this.CloseSocket(); 
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "TcpClient", "ConnectCallback", ex);
            }
        }
        //#error 修改这里
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                lock (this)
                {
                    StateObject state = (StateObject)ar.AsyncState;
                    Socket handler = state.workSocket;
                    int bytesRead = handler.EndReceive(ar);
                    if (ar.IsCompleted)
                    {
                        if (bytesRead > 0)
                        {
                            byte[] data = new byte[bytesRead];
                            System.Buffer.BlockCopy(state.buffer, 0, data, 0, bytesRead);
                            state.ReSetBuffer();
                            if (!OnRecvData(data))
                            {
                                GetReceiveData(data);
                            }
                            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReceiveCallback), state);
                        }
                        else
                        {
                            this.CloseSocket();
                        }
                    }
                    else
                    {
                        this.CloseSocket();
                    }
                }
            }
            catch (Exception)
            {
                this.CloseSocket();
            }
        }
  
        private Dictionary<int, IndexObject> sendlist = new Dictionary<int, IndexObject>();

        private UserClientCollection _userlist = new UserClientCollection();

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
            }
            catch (Exception)
            {
                this.CloseSocket();

            }
        }

        public override void ChangServer(NetPacket ph, string ip, int port)
        {

        }

        public override NetResult SendToRemote(NetPacket packet)
        {
            NetResult fengresult = new NetResult();
            try
            { 
                packet.Session = this.Session;
                byte[] data = packet.GetData();

                OnBeforeSendData(data);
                data = this.GetSendData(data);
                if (client == null)
                {
                    fengresult.SetError("client Is Null", NetResult.CODE_SYSTEM);
                    return fengresult;
                }
                IAsyncResult result = client.BeginSend(data, 0, data.Length, 0,
                    new AsyncCallback(SendCallback), client);
                fengresult.Success = true;
                return fengresult;
            }
            catch (System.Net.Sockets.SocketException se)
            {
                fengresult.SetError(se.Message, NetResult.CODE_KERNEL);
                this.CloseSocket();
            }
            catch (ObjectDisposedException ode)
            {
                fengresult.SetError(ode.Message, NetResult.CODE_KERNEL);
                this.CloseSocket();
            }
            catch (Exception ex)
            {
                fengresult.SetError(ex.Message, NetResult.CODE_KERNEL);
                this.CloseSocket();
            }
            return fengresult;

        }
        public override bool Send(byte[] data)
        {
            try
            {
                IAsyncResult result = client.BeginSend(data, 0, data.Length, 0,
                    new AsyncCallback(SendCallback), client);
                return false;
            }
            catch (Exception ex)
            {
                this.CloseSocket();
            }
            return false;
        }
        public override NetResult Post(NetPacket packet)
        {
            packet.PacketMode = PacketMode.POST;
            return SendToRemote(packet);
        }

        public override void Read()
        {
            try
            {
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception)
            {
                this.CloseSocket();

            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override void Dispose()
        {
            this.AutoConnected = false;
            this.CloseSocket();
        }

        public TcpClient Clone()
        {
            TcpClient sc = new TcpClient();
            sc.LocalIP = this.LocalIP;
            sc.Name = this.Name; 
            sc.LoalPort = this.LoalPort;
            return sc;
        }

        public virtual bool OnRecvData(byte[] data)
        {
            if (this.RecvData != null)
            {
                return this.RecvData(this, data);
            }
            return false;
        }

        public event RecvDataEventHandler RecvData;
    }
}