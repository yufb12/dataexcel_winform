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
    public class HttpTcpClientTest
    {
        public static void Test(string webServiceUrl)
        {
            // Web服务相关信息
            //string webServiceUrl = "http://example.com/Log.asmx/HelloWorld";
            string soapRequestBody = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                                     "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                     "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" " +
                                     "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n" +
                                     "<soap:Body>\r\n" +
                                     "<HelloWorld xmlns=\"http://tempuri.org/\">\r\n" +
                                     "</HelloWorld>\r\n" +
                                     "</soap:Body>\r\n" +
                                     "</soap:Envelope>";

            // 创建Socket连接
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                // 解析Web服务的主机和端口
                string host = new Uri(webServiceUrl).Host;
                int port = new Uri(webServiceUrl).Port; // 默认HTTP端口为80

                // 连接到Web服务
                socket.Connect(host, port);

                // 构造HTTP POST请求
                string requestHeaders = "POST " + new Uri(webServiceUrl).AbsolutePath + " HTTP/1.1\r\n" +
                                        "Host: " + host + "\r\n" +
                                        "Content-Type: text/xml; charset=utf-8\r\n" +
                                        "Content-Length: " + soapRequestBody.Length + "\r\n" +
                                        "SOAPAction: \"http://tempuri.org/HelloWorld\"\r\n" +
                                        "\r\n";

                string fullRequest = requestHeaders + soapRequestBody;

                // 发送请求
                byte[] requestBytes = Encoding.ASCII.GetBytes(fullRequest);
                socket.Send(requestBytes);

                // 接收响应
                byte[] responseBytes = new byte[4096];
                int bytesRead = socket.Receive(responseBytes);
                string response = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);

                // 处理响应
                Console.WriteLine(response);
            }
        }
        public static void Test2(string webServiceUrl)
        { 
            string txt = @"POST /Log.asmx HTTP/1.1
Host:localhost
Content-Type: text/xml; charset = utf-8
Content-Length: "+600+@"
SOAPAction: ""http://tempuri.org/HelloWorld""
<?xml version = ""1.0"" encoding = ""utf-8"" ?>
<soap:Envelope xmlns: xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns: xsd = ""http://www.w3.org/2001/XMLSchema"" xmlns: soap = ""http://schemas.xmlsoap.org/soap/envelope/"" >
<soap:Body> 
<HelloWorld xmlns = ""http://tempuri.org/"" />
</soap:Body>
</soap:Envelope>";

            // 创建Socket连接
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                // 解析Web服务的主机和端口
                string host = new Uri(webServiceUrl).Host;
                int port = new Uri(webServiceUrl).Port; // 默认HTTP端口为80

                // 连接到Web服务
                socket.Connect(host, port);
 

                // 发送请求
                byte[] requestBytes = Encoding.ASCII.GetBytes(txt);
                socket.Send(requestBytes);

                // 接收响应
                byte[] responseBytes = new byte[4096];
                int bytesRead = socket.Receive(responseBytes);
                string response = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);

                // 处理响应
                Console.WriteLine(response);
            }
        }
        public static void Test3(string webServiceUrl)
        {
            string rn ="\r\n";
            string txt = "POST " + new Uri(webServiceUrl).AbsolutePath + " HTTP/1.1" + rn;
            txt = txt + "Host:localhost:37850" + rn;
            txt = txt + "Content-Type:application/x-www-form-urlencoded" + rn;
            txt = txt + "Content-Length:0" + rn;
            txt = txt + rn;

            // 创建Socket连接
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                // 解析Web服务的主机和端口
                string host = new Uri(webServiceUrl).Host;
                int port = new Uri(webServiceUrl).Port; // 默认HTTP端口为80

                // 连接到Web服务
                socket.Connect(host, port);
 

                // 发送请求
                byte[] requestBytes = Encoding.ASCII.GetBytes(txt);
                socket.Send(requestBytes);

                // 接收响应
                byte[] responseBytes = new byte[4096];
                int bytesRead = socket.Receive(responseBytes);
                string response = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);

                // 处理响应
                Console.WriteLine(response);
            }
        }
    }
}