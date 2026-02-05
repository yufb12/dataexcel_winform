
using Feng.IO;
using Feng.Net.Base;
using Feng.Net.EventHandlers;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
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
    public partial class UdpServerBase : NetServer
    {
        public static int DefaultBufferSize = 1024 * 1024;
        private bool listenstate = false;
        public virtual bool Listen
        {
            get { return listenstate; }
            set { listenstate = value; }
        }
        private int _port = NetSettings.DefultUdpServerPort;
        public override int Port
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
        private IPEndPoint _endpoint = null;
        public virtual IPEndPoint EndPoint
        {
            get
            {
                if (_endpoint == null)
                {
                    _endpoint = new IPEndPoint(IPAddress.Parse(this.IP), Port);
                }
                return _endpoint;
            }
        }
        public event ConnectedEventHandler Connectioned;
        public virtual void OnClientConnectioned(object sender, IClientProxy client)
        {
            if (Connectioned != null)
            {
                Connectioned(sender, client);
            }

        }

        public event BeforeConnectedEventHandler BeforeConnected;
        public virtual void OnBeforeConnected(object sender, BeforeConnectedEventArgs e)
        { 
            if (BeforeConnected != null)
            {
                BeforeConnected(this, e);
            }
        }
        public override void Close()
        {
            
        }
        public override void OnBeforeDataReceive(ReciveEventArgs e)
        {

        }
        public override void OnBeforeSendData(byte[] data)
        {

        }
        public override void OnUnhandledException(Exception ex)
        {

        }
    }
    public partial class UdpCompletionPortServer : UdpServerBase
    {

        Socket listener = null;
        public override void Open()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(this.IP), Port);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            listener.ExclusiveAddressUse = false;
            listener.Bind(localEndPoint);
            Listen = true;
            Thread th = new Thread(new ParameterizedThreadStart(BeginRecv));
            th.IsBackground = true;
            th.Start();
            base.Open();
        }
        public void BeginRecv(object obj)
        {
            try
            {
                SocketAsyncEventArgs asynargs = new SocketAsyncEventArgs ();
                asynargs.RemoteEndPoint = EndPoint;
                byte[] receivebuffer = new byte[DefaultBufferSize];
                asynargs.SetBuffer(receivebuffer, 0, receivebuffer.Length);
                asynargs.Completed += asynargs_Completed;
                if (!listener.ReceiveFromAsync(asynargs))
                {

                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        private Dictionary<string, IClientProxy> dicsclients = new Dictionary<string, IClientProxy>();
        protected virtual void RecvData(EndPoint point, byte[] data)
        {
            string clientpoint = point.ToString();
            IClientProxy client = null;
            lock (this)
            {
                if (dicsclients.ContainsKey(clientpoint))
                {
                    client = dicsclients[clientpoint];
                    if (client.State == ClientProxyState.Remove)
                    {
                        dicsclients.Remove(clientpoint);
                        client = null;
                    }
                }
                if (client == null)
                {
                    client = ClientProxyFactory.CreatUdpClientProxy(this.listener, this, point);
                    dicsclients.Add(clientpoint, client);
                }
            }
            BeforeConnectedEventArgs e = new BeforeConnectedEventArgs(client);
            OnBeforeConnected(this, e);
            if (e.Cancel)
            {
                return;
            }

            OnClientConnectioned(this, client); 
            Add(client);
            client.Read();
            ReciveEventArgs recvdata = new ReciveEventArgs(data, client);
            client.ReceiveData(recvdata);
        }
        private void asynargs_Completed(object sender, SocketAsyncEventArgs e)
        {
            SocketAsyncOperation lastOperation = e.LastOperation;
            switch (lastOperation)
            {
                case SocketAsyncOperation.ReceiveFrom: 
                    this.DoDataReceived(e);
                    this.BeginRecv(null); 
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
                RecvData(e.RemoteEndPoint, data);
            }
        }
        public override void Remove(IClientProxy clientproxy)
        {
            clientproxy.State = ClientProxyState.Remove;
            base.Remove(clientproxy);
        }
        public event BeforeConnectedEventHandler BeforeConnected;
        public event ConnectedEventHandler ClientConnectioned;
        public override void Close()
        { 
            Listen = false;
            listener.Close();
            for (int i = this.Clients.Count - 1; i >= 0; i--)
            {
                this.Clients[i].Close();
            }
        }
    }
}
