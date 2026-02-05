
using Feng.Net.Base;
using Feng.Net.NetArgs;
using Feng.Net.Packets;
using System;
using System.Net;
using System.Net.Sockets;
namespace Feng.Net.UDP
{

    public class UdpAsyncClient : UdpClientProxyBase
    {
        Socket clientsocket;
        public static int DefaultBufferSize = 1024 * 1024;

        private DateTime conectiontime = DateTime.MaxValue;
        public override DateTime ConnectionTime
        {
            get { return conectiontime; }
        }
        public UdpAsyncClient()
        {
        }
        public override void Dispose()
        {
            this.Close();
        }

        private int _port = NetSettings.DefultUdpServerPort;
        public override int LoalPort
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public const int MaxSocketAsync = 16;
        private int _dafaultsendfilesize = 1024 * 4;
        public int DefaultSendFileSize
        {
            get
            {
                return _dafaultsendfilesize;
            }
            set
            {
                _dafaultsendfilesize = value;
            }
        }

        public override bool HasConnected
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public override void Open()
        {

            try
            {
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(this.LocalIP), LoalPort);
                clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                clientsocket.ExclusiveAddressUse = false;
                clientsocket.Bind(localEndPoint);

                InitSocketAsyncEventArgs();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public override void Connection()
        {
            try
            {
                conectiontime = DateTime.Now;
                this.HasConnected = true;
                this.OnConnected();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public override void Read()
        {
            BeginRecv(this);
        }

        private static int tempcount = 0;
        SocketAsyncEventArgs asynargsRECV = new SocketAsyncEventArgs();

        public virtual void InitSocketAsyncEventArgs()
        {
            asynargsRECV.Completed += asynargs_Completed;
            asynargsSEND.Completed += asynargs_Completed;

            asynargsRECV.UserToken = tempcount.ToString();
            byte[] receivebuffer = new byte[DefaultBufferSize];
            asynargsRECV.SetBuffer(receivebuffer, 0, receivebuffer.Length);

            asynargsSEND.SetBuffer(receivebuffer, 0, receivebuffer.Length);
        }
        public void BeginRecv(object obj)
        {

            try
            {
                this.SendAdd();
                asynargsRECV.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(this.LocalIP), LoalPort);

                tempcount++;
                if (!clientsocket.ReceiveFromAsync(asynargsRECV))
                {

                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        void asynargs_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.ReceiveFrom:
                    this.DoDataReceived(e);
                    return;
                default:
                    break;
            }
        }
        void DoDataReceived(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                int bytesRead = e.BytesTransferred;
                byte[] data = new byte[bytesRead];
                System.Buffer.BlockCopy(e.Buffer, 0, data, 0, bytesRead);
                int len = BitConverter.ToInt32(data, 0);
                if (data.Length == (len + 4))
                {
                    byte[] temp = new byte[len];
                    Array.Copy(data, 4, temp, 0, len);
                    ReciveEventArgs recve = new ReciveEventArgs(temp, this);
                    this.OnDataReceive(recve);
                }

            }
        }
        public override void ChangServer(NetPacket ph, string ip, int port)
        {

        }
        public override NetResult SendToRemote(NetPacket packet)
        {
            try
            {
                packet.Session = this.Session;
                byte[] data = packet.GetData();
                data = this.GetSendData(data);
                bool res = SendByte(data);
                if (res)
                {
                    return NetResult.SuccessResult();
                }
            }
            catch (Exception ex)
            {
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                NetResult.ErrorResult(ex.Message, NetResult.CODE_KERNEL);
            }
            return NetResult.ErrorResult("发送失败", NetResult.CODE_USER);
        }

        public override NetResult Post(NetPacket packet)
        {
            packet.PacketMode = PacketMode.POST;
            return SendToRemote(packet);
        }

        SocketAsyncEventArgs asynargsSEND = new SocketAsyncEventArgs();
        public virtual bool SendByte(byte[] data)
        {
            if (string.IsNullOrWhiteSpace(this.RemoteIP))
            {
                return false;
            }
            this.SendAdd();
            asynargsSEND.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(this.RemoteIP), this.RemotePort);


            if (asynargsSEND.Buffer.Length > data.Length)
            {
                System.Buffer.BlockCopy(data, 0, asynargsSEND.Buffer, 0, data.Length);
                asynargsSEND.SetBuffer(0, data.Length);
            }
            else
            {
                throw new Exception("Data Too Long! >" + DefaultBufferSize);
            }
            bool res = clientsocket.SendToAsync(asynargsSEND);
            return res;
        }
        public override void Close()
        {
            lock (this)
            {
                if (clientsocket != null)
                {
                    clientsocket.Shutdown(SocketShutdown.Both);

                    clientsocket.Close();
                    clientsocket = null;

                }
            }
        }
        public override string ToString()
        {
            return this.Name;
        }
        private string remoteip = string.Empty;
        public override string RemoteIP
        {
            get
            {
                return remoteip;
            }
            set
            {
                remoteip = value;
            }
        }
        private int remoteport = 0;
        public override int RemotePort
        {
            get
            {
                return remoteport;
            }
            set
            {
                remoteport = value;
            }
        }

    }
}