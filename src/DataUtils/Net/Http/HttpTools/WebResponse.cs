//using System;
//using System.IO;
//using System.Net.Sockets;
//using System.Text;

//public class HttpRequestTool
//{
//    private const string CRLF = "\r\n";

//    public static string Get(string url)
//    {
//        Uri uri = new Uri(url);a
//        string host = uri.Host;
//        int port = uri.Port;
//        string path = uri.PathAndQuery;

//        string request = String.Format("GET {0} HTTP/1.1{1}" +
//                         "Host: {2}{1}" +
//                         "Connection: close{1}{1}", path, CRLF, host);

//        return SendRequest(host, port, request);
//    }

//    public static string Post(string url, string data, string contentType)
//    {
//        Uri uri = new Uri(url);
//        string host = uri.Host;
//        int port = uri.Port;
//        string path = uri.PathAndQuery;

//        string request = String.Format("POST {0} HTTP/1.1{1}" +
//                         "Host: {2}{1}" +
//                         "Content-Type: {3}{1}" +
//                         "Content-Length: {4}{1}" +
//                         "Connection: close{1}{1}" +
//                         "{5}", path, CRLF, host, contentType, data.Length, data);

//        return SendRequest(host, port, request);
//    }

//    private static string SendRequest(string host, int port, string request)
//    {
//        using (TcpClient client = new TcpClient(host, port))
//        {
//            using (NetworkStream stream = client.GetStream())
//            {
//                byte[] requestData = Encoding.UTF8.GetBytes(request);
//                stream.Write(requestData, 0, requestData.Length);

//                MemoryStream responseStream = new MemoryStream();
//                byte[] buffer = new byte[4096];
//                int bytesRead;

//                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
//                {
//                    responseStream.Write(buffer, 0, bytesRead);
//                }

//                byte[] responseData = responseStream.ToArray();
//                string response = Encoding.UTF8.GetString(responseData);

//                return response;
//            }
//        }
//    }
//}