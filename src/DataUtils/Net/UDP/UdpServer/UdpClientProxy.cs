
using Feng.IO;
using Feng.Net.Base;
using Feng.Net.EventHandlers;
using Feng.Net.Packets;
using Feng.Net.Tcp;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Feng.Net.UDP
{
    public partial class UdpClientProxy : UdpClientProxyBase
    { 
        private int _port = NetSettings.DefultUdpServerPort;
        private ushort _index = 0;  
        private Socket _client;
  
        private object newclientobj = new object();
        public object readonlyobject = new object(); 

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

        private DateTime _conectiontime = DateTime.MaxValue;
        public override DateTime ConnectionTime
        {
            get { return _conectiontime; }
        }
 
        public const int MaxSocketAsync = 16;

        public virtual NetServer Server { get { return _server; } }
        private NetServer _server = null;

        public Socket Client
        {
            get
            {
                return this._client;
            }
        }
        private string remoteip = string.Empty;
        public override string RemoteIP
        {
            get
            {
                return remoteip;
            }
        }
        private int remoteport = 0;
        public override int RemotePort
        {
            get
            {
                return remoteport;
            }
        }
        public UdpClientProxy(Socket handler, NetServer server, EndPoint point)
        {
            IPEndPoint ippoint = point as IPEndPoint;
            if (ippoint != null)
            {
                remoteip = ippoint.Address.ToString();
                remoteport = ippoint.Port;
            }
            this._client = handler;
            _server = server;
            _conectiontime = DateTime.Now; 
        }
        public override void Dispose()
        {
            this.Close();
        }

        public override void Read()
        { 

        }
        public override void Open()
        { 
        }
        public override void ChangServer(NetPacket ph, string ip, int port)
        {

        }
        public virtual NetResult Post(string text)
        {
            NetPacket ph = new NetPacket(Feng.Net.Packets.PacketMainCmd.Simple, Feng.Net.Packets.PacketSystemSubCmd.Text, Feng.IO.BitConver.GetBytes(text));
            return this.Post(ph);
        }
        public override NetResult Post(NetPacket packet)
        {
            packet.PacketMode = PacketMode.ANSWER;
            return SendToRemote(packet);
        }
 
        public override NetResult Send(NetPacket packet)
        {
            packet.PacketMode = PacketMode.Send;
            return this.SendToRemote(packet);
        }
        public override NetResult Respond(NetPacket packet)
        {
            packet.PacketMode = PacketMode.ANSWER;
            return this.SendToRemote(packet);
        }

        public override NetResult SendToRemote(NetPacket packet)
        {
            NetResult result = NetResult.EmptyResult();
            try
            {
                packet.Session = this.Session;
                byte[] data = packet.GetData();
                data = this.GetSendData(data);
                Send(data);
                result = NetResult.SuccessResult();
            }
            catch (Exception ex)
            {
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                result = NetResult.ErrorResult(ex.Message, NetResult.CODE_SYSTEM);
            }
            return result;
        }
 
        public override void Close()
        {
            Monitor.Enter(this);
            try
            {
                if (this._client != null)
                {
                    this._client.Shutdown(SocketShutdown.Both);
                    this._client.Close();
                    this._client = null;
                }
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        private EndPoint remoteep = null;
        public EndPoint remoteEP
        {
            get {
                if (remoteep == null)
                {
                    remoteep = new IPEndPoint(IPAddress.Parse(this.RemoteIP), this.RemotePort);
                }
                return remoteep;
            }
        }
        private void Send(byte[] data)
        { 
            Client.BeginSendTo(data, 0, data.Length, SocketFlags.None, remoteEP, 
                new AsyncCallback(SendCallback), Client);
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
 

    }
}
