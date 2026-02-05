 
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Feng.Net.NetArgs;
using Feng.Net.Tcp;

using Feng.Utils;
using Feng.Net.EventHandlers;
using Feng.Net.Base;
using Feng.Net.Packets; 
namespace Feng.Net.UDP
{

    public class UdpClient : UdpClientProxyBase, IDisposable
    {
        Socket clientsocket;
        public static int DefaultBufferSize = 1024 * 1024;


        public UdpClient()
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

        private DateTime conectiontime = DateTime.MaxValue;
        public override DateTime ConnectionTime
        {
            get { return conectiontime; }
        }
 
        public const int MaxSocketAsync = 16;
        private int _dafaultsendfilesize = 1024 * 4;
        public int DefaultSendFileSize
        {
            get {
                return _dafaultsendfilesize;
            }
            set {
                _dafaultsendfilesize = value;
            }
        }
        private bool _hasconnected = false;
        public override bool HasConnected
        {
            get
            {
                return _hasconnected;
            }
            set
            {
                _hasconnected = value;
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
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", "监测", true, "执行Read:");
            System.Threading.Thread th = new Thread(BeginRecv);
            th.IsBackground = true;
            th.Start(); 
        }

        private static int tempcount = 0; 
 
        public void BeginRecv(object obj)
        {

            try
            {
                byte[] receivebuffer = new byte[DefaultBufferSize];
                EndPoint remoteEP = new IPEndPoint(System.Net.IPAddress.Any, 0);
                int len = clientsocket.ReceiveFrom(receivebuffer, SocketFlags.None, ref remoteEP);
                if (clientsocket.Available == 0)
                {
                    Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.ID, true, "Available:0 " + " RECVLEN:" + len + " Completed 执行次数:BeginRecv len=" + tempcount);
                }
                if (len > 0)
                {
                    this.RecvAdd();
                    byte[] data = new byte[len];
                    System.Buffer.BlockCopy(receivebuffer, 0, data, 0, len);
                    len = BitConverter.ToInt32(data, 0);
                    if (data.Length == (len + 4))
                    {
                        byte[] temp = new byte[len];
                        Array.Copy(data, 4, temp, 0, len);
                        ReciveEventArgs recve = new ReciveEventArgs(temp, this);
                        this.OnDataReceive(recve);
                    }
                    tempcount++; 
                    BeginRecv(obj);
                }
                else
                { 
                    Feng.IO.LogHelper.Log("BeginRecv len=0");
                    BeginRecv(obj);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        public override void ChangServer(NetPacket ph, string ip, int port)
        {

        }
        public override NetResult SendToRemote(NetPacket ph)
        {
            try
            {
                Feng.Utils.TraceHelper.WriteTrace("CLIENT", "发送数据", this.ID, true, "Packetindex:" + ph.Packetindex + " MainCommand:" + ph.PacketMainCommand + " Subcommand:" + Feng.Utils.ConvertHelper.ToString(ph.PacketSubcommand, 16));
                ph.Session = this.Session;
                byte[] data = ph.GetData();
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

        public virtual NetResult SendToRemote(NetPacket ph, string remoteip, int remoteport)
        {
            try
            {
                Feng.Utils.TraceHelper.WriteTrace("CLIENT", "发送数据", this.ID, true, "Packetindex:" + ph.Packetindex + " MainCommand:" + ph.PacketMainCommand + " Subcommand:" + Feng.Utils.ConvertHelper.ToString(ph.PacketSubcommand, 16));
                ph.Session = this.Session;
                byte[] data = ph.GetData();
                data = this.GetSendData(data);
                bool res = SendByte(data, remoteip, remoteport);
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
 
        public virtual bool SendByte(byte[] data)
        {
            if (string.IsNullOrWhiteSpace(this.RemoteIP))
            {
                return false;
            }
            this.SendAdd();
            IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse(this.RemoteIP), this.RemotePort);
            System.Threading.Thread.Sleep(30);
            int len= clientsocket.SendTo(data, RemoteEndPoint);
            bool res = true;
            if (len < data.Length)
            {
                res = false;
            }
            return res;
        }
        public virtual bool SendByte(byte[] data, string remoteip, int remoteport)
        {
            if (string.IsNullOrWhiteSpace(this.RemoteIP))
            {
                return false;
            }
            this.SendAdd();
            IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteip), remoteport);
            System.Threading.Thread.Sleep(30);
            int len = clientsocket.SendTo(data, RemoteEndPoint);
            bool res = true;
            if (len < data.Length)
            {
                res = false;
            }
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

        public virtual NetResult Respond(NetPacket ph, string remoteip, int remoteport)
        {
            ph.PacketMode = PacketMode.ANSWER;
            return SendToRemote(ph, remoteip, remoteport);
        }

        public virtual NetResult Post(NetPacket packet, string remoteip, int remoteport)
        {
            packet.PacketMode = PacketMode.POST;
            return SendToRemote(packet, remoteip, remoteport);
        }
         
     
    }
} 