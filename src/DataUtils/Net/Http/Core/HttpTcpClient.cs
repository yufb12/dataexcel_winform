using Feng.Net.Base;
using Feng.Net.Tcp;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Feng.Net.Http
{ 
    public partial class HttpTcpClient  
    {
        public HttpTcpClient()
        {
        }

        Socket client;

        public virtual string RemoteIP
        {
            get;
            set;
        }

        public virtual int RemotePort
        {
            get;
            set;
        }

        public virtual void Open()
        {
            IPAddress ip = IPAddress.Parse(this.RemoteIP);
            client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint remoteEP = new IPEndPoint(ip, this.RemotePort);
            client.SendBufferSize = (1024 * 1024 * 8);
            client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
        }

        private void CloseSocket()
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
                        client.Close();
                        client = null;
                    }
                }
                catch (Exception ex)
                {
                    Feng.Utils.BugReport.Log(ex);
                }
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                Read();
            }
            catch (Exception ex)
            {
                this.CloseSocket();
                Utils.TraceHelper.WriteTrace("DataUtils", "TcpClient", "ConnectCallback", ex);
            }
        }

        public void GetReceiveData(byte[] data)
        {
            string txt = System.Text.Encoding.UTF8.GetString(data);
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

                            GetReceiveData(data);
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

        public virtual void Send(string txt)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(txt);
            Send(data);
        }

        public virtual void Send(byte[] data)
        {
            try
            {
                if (client == null)
                {
                    return;
                }
                IAsyncResult result = client.BeginSend(data, 0, data.Length, 0,
                    new AsyncCallback(SendCallback), client);
            }
            catch (Exception ex)
            {
                this.CloseSocket();
            }
        }

        public void Read()
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

        //"HTTP/1.1 400 Bad Request\r\n
        //Content-Type: text/html; charset=us-ascii\r\n
        //Server: Microsoft-HTTPAPI/2.0\r\n
        //Date: Thu, 31 Aug 2023 13:40:22 GMT\r\n
        //Connection: close\r\n
        //Content-Length: 326\r\n\r\n
        //<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\"\"http://www.w3.org/TR/html4/strict.dtd\">\r\n
        //<HTML><HEAD><TITLE>Bad Request</TITLE>\r\n
        //<META HTTP-EQUIV=\"Content-Type\" Content=\"text/html; charset=us-ascii\"></HEAD>\r\n
        //<BODY><h2>Bad Request - Invalid Verb</h2>\r\n
        //<hr><p>HTTP Error 400. The request verb is invalid.</p>\r\n
        //</BODY></HTML>\r\n"

        public static void Test(string ip,int port,string str)
        {
            HttpTcpClient client = new HttpTcpClient();
            client.RemoteIP = ip;
            client.RemotePort = port;
            client.Open();
            //string str =
            //    "GET /file/test/index.html HTTP/1.1\r\nHost: 112.126.57.50:80\r\n Cache-Control: max-age=0\r\nsec-ch-ua: \"Chromium\";v=\"116\", \"Not)A;Brand\";v=\"24\", \"Microsoft Edge\";v=\"116\"\r\nsec-ch-ua-platform: \"Windows\"\r\nDNT: 1\r\nUpgrade-Insecure-Requests: 1\r\nUser-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36 Edg/116.0.1938.54\r\nAccept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7\r\nSec-Fetch-Site: none\r\nSec-Fetch-Mode: navigate\r\nSec-Fetch-Dest: document\r\nAccept-Encoding: gzip, deflate, br\r\nAccept-Language: zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6\r\n\r\n";
            System.Threading.Thread.Sleep(200);
            //            str = @"HTTP/1.1 200 OK\r\n
            //Content-Type: text/html\r\nLast-Modified: Thu, 31 Aug 2023 13:36:08 GMT\r\n
            //Accept-Ranges: bytes\r\nETag: \""323a3b1910dcd91:0\""\r\n
            //Server: Microsoft-IIS/7.5\r\nX-Powered-By: ASP.NET\r\nDate: Thu, 31 Aug 2023 14:15:49 GMT\r\n
            //Connection: close\r\n
            //Content-Length: 11\r\n\r\nhello world";
            client.Send(str);
        }

        public static void Test1()
        {
            //string url = "http://127.0.0.1:37850/Log.asmx/HelloWorld";
            string url = "http://localhost:37850/Log.asmx/HelloWorld";
            HttpTcpClientTest.Test3(url);
            HttpTcpClientTest.Test(url);
            Uri uri = new Uri(url) ;
            string host = uri.Host;
            string absurl = uri.AbsoluteUri;
            string text = @"OPTIONS /Log.asmx/HelloWorld HTTP/1.1
Host: 127.0.0.1:37850
Connection: keep - alive
Pragma: no - cache
Cache - Control: no - cache
Accept: */*
Access-Control-Request-Method: POST
Access-Control-Request-Headers: content-type
Origin: http://localhost:37850
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36 Edg/118.0.2088.46
Sec-Fetch-Mode: cors
Sec-Fetch-Site: cross-site
Sec-Fetch-Dest: empty
Referer: http://localhost:37850/
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6
";
            string ip = "127.0.0.1";
            int port = 37850;
            Test(ip, port, text);
        }
    }
}