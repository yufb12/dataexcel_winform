
using Feng.Net.Base;
using Feng.Net.EventHandlers;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
namespace Feng.Net.Tcp
{
    public partial class TcpServer : NetServer
    {
        public TcpServer(int port)
        {
            Port = port;
        }

        public TcpServer()
        {
        }

        private bool listenstate = false;
        public virtual bool Listen
        {
            get { return listenstate; }
            set { listenstate = value; }
        }
        Socket listener = null;

        public override void Open()
        {
            if (listener != null)
                return;
            IPAddress ipdres = IPAddress.Any;
            if (!string.IsNullOrWhiteSpace(this.IP))
            {
                ipdres = IPAddress.Parse(this.IP);
            }
            IPEndPoint localEndPoint = new IPEndPoint(ipdres, Port);
            listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            listener.ExclusiveAddressUse = false;
            listener.Bind(localEndPoint);
            listenstate = true;
            listener.Listen(300);
            Thread th = new Thread(new ParameterizedThreadStart(BeginAccept));
            th.IsBackground = true;
            th.Start();
            base.Open();
        }
        AutoResetEvent acceptautoresetevent;
        public byte ConnectionState = ConnectState.NotConnection;

        public virtual void BeginAccept(object obj)
        {
            if (acceptautoresetevent == null)
            {
                acceptautoresetevent = new AutoResetEvent(true);
            }
            while (listenstate)
            {
                try
                {
                    acceptautoresetevent.WaitOne();
                    listener.BeginAccept(
                                new AsyncCallback(AcceptCallback),
                                listener);
                    ConnectionState = ConnectState.Connectioning;
                }
                catch (Exception ex)
                {
                    Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                    if (ListenException != null)
                    {
                        ListenException(this, ex);
                    }
                }
                finally
                {

                }
            }

        }

        private byte[] IOControl
        {
            get
            {
                uint dummy = 0;
                byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
                return inOptionValues;

            }
        }

        public virtual void AcceptCallback(IAsyncResult ar)
        {

            try
            {
                acceptautoresetevent.Set();
                Socket listener = (Socket)ar.AsyncState;
                if (listener == null)
                    return;

                Socket handler = listener.EndAccept(ar);

                handler.IOControl(IOControlCode.KeepAliveValues, IOControl, null);
                IClientProxy client = ClientProxyFactory.CreatProxy(handler, this);
                client.RemoteIP = handler.RemoteEndPoint.ToString();
                BeforeConnectedEventArgs e = new BeforeConnectedEventArgs(client);
                if (BeforeConnected != null)
                {
                    BeforeConnected(this, e);
                    if (e.Cancel)
                    {
                        return;
                    }
                }

                if (Connectioned != null)
                {
                    Connectioned(this, client);
                }
                Add(client);
                client.Read();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "TcpServer", "AcceptCallback", ex);
                if (ListenException != null)
                {
                    ListenException(this, ex);
                }
            }

        }

        public override void Close()
        {
            try
            {
                ConnectionState = Feng.Net.Base.ConnectState.Closed;
                listenstate = false;
                listener.Close();
            }
            catch (Exception)
            {
            }
            try
            {
                for (int i = this.Clients.Count - 1; i >= 0; i--)
                {
                    this.Clients[i].Close();
                }
            }
            catch (Exception)
            {
            }
        }


    }

}