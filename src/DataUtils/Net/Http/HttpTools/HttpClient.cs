//using Feng.Net.Tcp;
//using System;
//using System.Collections;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Web;
//using System.Xml;
//using System.Xml.Serialization;

//namespace Feng.Net.Http
//{
//    public class HttpClient
//    {
//        public string PostMethod = "Post";
//        public string url = string.Empty;
//        public string versiont = "HTTP/1.1";
//        public System.Collections.Generic.Dictionary<string, string> keyValues = new System.Collections.Generic.Dictionary<string, string>();
        
//        public void Clear()
//        {
//            keyValues.Clear();
//            PostMethod = "Post";
//            url = string.Empty;
//            versiont = "HTTP/1.1";
//        }

//        public string Post()
//        {
//            lock (this)
//            {
//                Uri uri = new Uri(url);
//                Socket client = new Socket(AddressFamily.InterNetwork,
//    SocketType.Stream, ProtocolType.Tcp); 
//                IPAddress iPAddress = null;
//                if (uri.IsLoopback)
//                {
//                    iPAddress = IPAddress.Loopback;
//                }
//                else
//                {
//                    switch (uri.HostNameType)
//                    {
//                        case UriHostNameType.Basic:
//                            iPAddress = Dns.GetHostAddresses(uri.Host)[0];
//                            break;
//                        case UriHostNameType.Unknown:
//                            throw new Exception(" HostName Unknown");
//                        case UriHostNameType.Dns:
//                            IPAddress[] iPAddresses = Dns.GetHostAddresses(uri.Host);
//                            iPAddress = iPAddresses[0];
//                            break;
//                        case UriHostNameType.IPv4:
//                            iPAddress = IPAddress.Parse(uri.Host);
//                            break;
//                        case UriHostNameType.IPv6:
//                            iPAddress = IPAddress.Parse(uri.Host);
//                            break;
//                        default:
//                            break;
//                    }
//                }
//                IPEndPoint remoteEP = new IPEndPoint(iPAddress, uri.Port);
//                client.SendBufferSize = (1024 * 1024 * 8);
//                client.Connect(remoteEP);
//                PostText(client);
//            }
//            return string.Empty;
//        }

//        private void PostText(Socket client)
//        {
//            //client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
//            string text = string.Empty;
//            text = @"POST /WebService1.asmx/HelloWorld HTTP/1.1
//Host: 112.126.57.50
//Content-Type: application/x-www-form-urlencoded
//Content-Length: 0";
//            byte[] buf = System.Text.Encoding.UTF8.GetBytes(text);
//            client.Send(buf);
//            byte[] data = new byte[8];
//            client.Receive(data);
//        }

//        private void ConnectCallback(IAsyncResult ar)
//        {
//            Socket client = (Socket)ar.AsyncState;
//            client.EndConnect(ar);
//            Read(client);
//        }

//        public void Read(Socket client)
//        {
//            StateObject state = new StateObject();
//            state.workSocket = client;
//            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                new AsyncCallback(ReceiveCallback), state);
//        }

//        private void ReceiveCallback(IAsyncResult ar)
//        {
//            lock (this)
//            {
//                StateObject state = (StateObject)ar.AsyncState;
//                Socket handler = state.workSocket;
//                int bytesRead = handler.EndReceive(ar);
//                if (ar.IsCompleted)
//                {
//                    if (bytesRead > 0)
//                    {
//                        byte[] data = new byte[bytesRead];
//                        System.Buffer.BlockCopy(state.buffer, 0, data, 0, bytesRead);
//                        state.ReSetBuffer();
                         
//                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                            new AsyncCallback(ReceiveCallback), state);
//                    }
//                }
//            }
//        }
//    }
//}