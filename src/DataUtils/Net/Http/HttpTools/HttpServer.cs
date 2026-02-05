//using Feng.Net.Tcp;
//using System;
//using System.Collections;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Security.Authentication.ExtendedProtection;
//using System.Text;
//using System.Threading;
//using System.Web;
//using System.Xml;
//using System.Xml.Serialization;

//namespace Feng.Net.Http
//{
//    public class HttpServer
//    {
//        public HttpServer()
//        {
//            httpListener = new HttpListener(); 
//        }
//        public HttpListener httpListener { get; set; }
//        public void Start()
//        {
//            return;
//            httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
//            //httpListener.Prefixes.Add("http://localhost:8080/");
//            int iPort = 3332;
//            string strPrefixes = string.Format("http://*:{0}/", iPort);
//            //httpListener.Prefixes.Add("http://*:1020/");
//            httpListener.Prefixes.Add(strPrefixes);
//            httpListener.Start();
//            new Thread(new ThreadStart(delegate
//            {
//                while (true)
//                {
//                    HttpListenerContext httpListenerContext = httpListener.GetContext();
//                    using (StreamReader reader = new StreamReader(httpListenerContext.Request.InputStream))
//                    {
//                        string text = reader.ReadToEnd();
//                        Feng.Utils.TraceHelper.WriteTrace("Feng.Net.Http", "HttpServer", "reader", text);
//                    }
//                    httpListenerContext.Response.StatusCode = 200;
//                    using (StreamWriter writer = new StreamWriter(httpListenerContext.Response.OutputStream))
//                    {
//                        writer.WriteLine("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/><title>测试服务器</title></head><body>");
//                        writer.WriteLine("<div style=\"height:20px;color:blue;text-align:center;\"><p> hello</p></div>");
//                        writer.WriteLine("<ul>");
//                        writer.WriteLine("</ul>");
//                        writer.WriteLine("</body></html>");

//                    }

//                }
//            })).Start();
//            httpListener.Start();
//        }
 
//        public void Stop()
//        {
//            try
//            {
//                httpListener.Stop();
//            }
//            catch (Exception ex)
//            {   
//                Feng.Utils.TraceHelper.WriteTrace("Feng.Net.Http", "HttpServer", "reader", ex); 
//            }
//        }
//    }
//}