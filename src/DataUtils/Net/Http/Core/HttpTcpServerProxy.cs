using Feng.Net.Tcp;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Feng.Net.Http
{
    public class HttpTcpServerProxy
    {
        private Socket _clientsocket = null;
        private HttpTcpServer _server = null;
        public virtual Socket ClientSocket { get { return _clientsocket; } }
        public HttpTcpServer Server { get { return _server; } }
        public string RomoteEndPointText { get; set; }
        private List<HttpRequest> HttpRequestes = new List<HttpRequest>();
        private System.Threading.Thread wordthread = null;
        //private byte[] IOControl
        //{
        //    get
        //    {
        //        uint dummy = 0;
        //        byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
        //        BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
        //        BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
        //        BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
        //        return inOptionValues;

        //    }
        //}
        public HttpTcpServerProxy(Socket socket, HttpTcpServer server)
        {
            //socket.IOControl(IOControlCode.KeepAliveValues, IOControl, null);
            _clientsocket = socket;
            _server = server;
            this.RomoteEndPointText = socket.RemoteEndPoint.ToString();
            wordthread = new System.Threading.Thread(Start);
            wordthread.IsBackground = true;
            wordthread.Start();
        }

        public virtual void Start()
        {
            while (!hasClose)
            {
                try
                {
                    Read();
                }
                catch (Exception ex)
                {
                    Feng.Utils.TraceHelper.WriteTrace("Http", "HttpTcpServerProxy", "Start", ex);
                    hasClose = true;
                }
            }
            Feng.Utils.TraceHelper.WriteTrace("Net.Http", "HttpTcpServerProxy", "Start_Exit", this.RomoteEndPointText);

        }

        public DateTime LastReadTime { get; set; }
        public virtual void Read()
        {
            int BufferSize = 4096;
            byte[] buffer = new byte[BufferSize];
            LastReadTime = DateTime.Now;
            int bytesRead = ClientSocket.Receive(buffer);
            Feng.Utils.TraceHelper.WriteTrace("Http", "HttpTcpServerProxy", "Read", "bytesRead:" + bytesRead, this);

            if (bytesRead > 0)
            {
                byte[] data = new byte[bytesRead];
                System.Buffer.BlockCopy(buffer, 0, data, 0, bytesRead);
                HttpRequest request = GetReceiveData(data, ClientSocket);
                //System.Net.HttpListener
#if DEBUG
                string txt = System.Text.Encoding.UTF8.GetString(data);
                request.DebugText = txt;
#endif
                HttpRequestes.Add(request);
                DoRequest(request);
            }
            else
            {
                this.Close();
            }
        }

        public bool hasClose { get; set; }

        public void Close()
        {
            try
            {
                hasClose = true;
                try
                {
                    wordthread.Abort();
                }
                catch (Exception ex)
                {
                    Utils.TraceHelper.WriteTrace("Feng.Net.Http", "HttpTcpServerProxy", "Close wordthread.Abort", ex);
                }
                if (this.ClientSocket == null)
                    return; 
                try
                {
                    this.ClientSocket.Close();
                }
                catch (Exception ex)
                {
                    Utils.TraceHelper.WriteTrace("Feng.Net.Http", "HttpTcpServerProxy", "Close ClientSocket.Close", ex);
                }
                this._clientsocket = null;
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("Feng.Net.Http", "HttpTcpServerProxy", "OnDraw", ex);
            }

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
                this.Close();
            }
        }

        public virtual void Send(byte[] data)
        {
            ClientSocket.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), ClientSocket);
        }

        public HttpRequest GetReceiveData(byte[] htmldata, Socket clientsocket)
        {
            HttpRequest request = new HttpRequest();
            try
            {
                request.HttpServer = this.Server;
                request.RemoteEndPoint = clientsocket.RemoteEndPoint as System.Net.IPEndPoint;
                HttpRequestHeader requestHeader = new HttpRequestHeader();
                int index = 0;
                for (int i = 0; i < 1000; i++)
                {
                    bool isfinished = false;
                    int findindex = GetLineIndex(htmldata, index, out isfinished);
                    if (findindex < 1)
                    {
                        break;
                    }
                    string line = System.Text.Encoding.UTF8.GetString(htmldata, index, findindex - index + 1);
                    requestHeader.List.Add(line);
                    if (line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase))
                    {
                        int contentLength = 0;
                        string value = line.Substring("Content-Length:".Length).Trim();
                        int.TryParse(value, out contentLength);
                        requestHeader.Content_Length = contentLength;
                    }
                    if (isfinished)
                    {
                        if (requestHeader.Content_Length > 0)
                        {
                            HttpRequestBody requestBody = new HttpRequestBody(request);
                            request.Body = requestBody;
                            int bodystart = findindex + 3;
                            int bodyreaded = htmldata.Length - bodystart;
                            requestBody.BodyData = new byte[requestHeader.Content_Length];
                            requestBody.Index = bodyreaded;
                            System.Buffer.BlockCopy(htmldata, bodystart, requestBody.BodyData, 0, bodyreaded);
                            if (bodyreaded < requestHeader.Content_Length)
                            {
                                DoReadHtmlBody(request, clientsocket);
                            }
                        }
                        break;
                    }
                    index = findindex + 1;
                }
                request.Header = requestHeader;
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Http", "HttpTcpServerProxy", "GetReceiveData", ex);
            }
            return request;
        }

        private void DoReadHtmlBody(HttpRequest request, Socket clientsocket)
        {
            while (request.Body.Index < request.Body.BodyData.Length)
            {
                int BufferSize = 4096;
                byte[] buffer = new byte[BufferSize];
                int bytesRead = ClientSocket.Receive(buffer);
                System.Buffer.BlockCopy(buffer, 0, request.Body.BodyData, request.Body.Index, bytesRead);
                request.Body.Index = request.Body.Index + bytesRead;
            }

        }

        public void DoRequest(HttpRequest request)
        {
            if (request == null)
                return;
            this.Server.DoRequest(request, this);
        }

        private int GetLineIndex(byte[] data, int index, out bool headerfinish)
        {
            headerfinish = false;
            int findindex = -1;
            for (int i = index; i < data.Length; i++)
            {
                if (data[i] == '\r')
                {
                    if (data[i + 1] == '\n')
                    {
                        findindex = i + 1;

                        if ((i + 3) < data.Length)
                        {
                            if (data[i + 2] == '\r')
                            {
                                if (data[i + 3] == '\n')
                                {
                                    headerfinish = true;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            return findindex;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("hasClose:" + hasClose);
            sb.Append(" REP:" + RomoteEndPointText);
            foreach (HttpRequest item in HttpRequestes)
            {
                sb.Append(" "+item.Header.Url);
            }
            return sb.ToString();
        }
    }

}