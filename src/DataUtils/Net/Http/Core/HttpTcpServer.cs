

using Feng.Net.Base;
using Feng.Net.Extend;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
using Feng.Net.Tcp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Feng.Net.Http
{
    public interface IHttpRequest
    {
        bool DoHttpRequest(HttpRequest request, HttpResponse httpResponse );
    }
    public partial class HttpTcpServer : NetServer
    {
        public HttpTcpServer(int port)
        {
            Port = port;
        }

        public HttpTcpServer()
        {
        }

        public FileServerResourceManager FSRM { get; set; }

        private bool listenstate = false;
        public virtual bool Listen
        {
            get { return listenstate; }
            set { listenstate = value; }
        }
        Socket listener = null; 
        public virtual void Init()
        { 
        }
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


            th = new Thread(RunCheck);
            th.IsBackground = true;
            th.Start();
        }
 
        public void RunCheck()
        {
            while (true)
            {
                //int count = clients.Count;
                //for (int i = count - 1; i >= 0; i--)
                //{
                //    HttpTcpServerProxy client = clients[i];
                //    if (!client.hasClose)
                //    {
                //        if ((DateTime.Now - client.LastReadTime).TotalSeconds > 60)
                //        {
                //            client.Close();
                //        }
                //    }
                //}
                System.Threading.Thread.Sleep(2100);
            }
        }
        AutoResetEvent acceptautoresetevent;
        public byte ConnectionState = ConnectState.NotConnection;
        private List<HttpTcpServerProxy> clients = new List<HttpTcpServerProxy>();
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
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    ConnectionState = ConnectState.Connectioning;
                }
                catch (Exception ex)
                {
                    Utils.TraceHelper.WriteTrace("Net.Http", "HttpTcpServer", "BeginAccept", ex);
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
 
        public virtual void AcceptCallback(IAsyncResult ar)
        {

            try
            {
                acceptautoresetevent.Set();
                //if (listenstate)
                //{
                //    listener.BeginAccept(
                //                new AsyncCallback(AcceptCallback),
                //                listener);
                //} 
                Socket listener = (Socket)ar.AsyncState;
                if (listener == null)
                {
                    return;
                }
                Socket handler = listener.EndAccept(ar);
                Feng.Utils.TraceHelper.WriteTrace("Net.Http", "HttpTcpServer", "AcceptCallback", handler.RemoteEndPoint.ToString());
                HttpTcpServerProxy client = new HttpTcpServerProxy(handler, this);
                clients.Add(client);
            }
            catch (Exception ex)
            {
                Utils.TraceHelper.WriteTrace("DataUtils", "TcpServer", "AcceptCallback", ex);
            }
            finally
            {

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

        private List<IHttpRequest> httpRequests = new List<IHttpRequest>();

        public void BingdingHttpRequest(IHttpRequest doHttpRequest)
        {
            httpRequests.Add(doHttpRequest);
        }

        public void DoCommand(HttpRequest request, HttpResponse httpResponse, Feng.Events.CancelArgs arg)
        {
            try
            {
                foreach (IHttpRequest item in httpRequests)
                {
                    try
                    {
                        if (arg.Handle)
                        {
                            return;
                        }
                        item.DoHttpRequest(request, httpResponse);
                    }
                    catch (Exception ex)
                    {
                        Feng.Utils.TraceHelper.WriteTrace("HttpTcpServer", "DoCommand", "item", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("HttpTcpServer", "HttpTcpServer", "DoCommand", ex);
            }
        }

        public virtual void DoRequest(HttpRequest request, HttpTcpServerProxy httpTcpServerProxy)
        {
            try
            {
                HttpResponse httpResponse = new HttpResponse(request);
                httpResponse.HttpProxy = httpTcpServerProxy;
                request.Prepare(request, httpResponse);
                Feng.Events.CancelArgs arg = new Events.CancelArgs();
                DoCommand(request, httpResponse, arg);
                if (httpResponse.ResponseTimes < 1)
                {

                } 
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("HttpTcpServer", "HttpTcpServer", "DoRequest", ex);
            }
        }
    }
}