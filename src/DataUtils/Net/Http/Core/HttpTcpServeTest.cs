using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Feng.Net.Http
{
    public class HttpTcpServeTest
    {
        public static void Run()
        {
            // 创建服务器监听 Socket
            var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 43388));
            serverSocket.Listen(10);

            Console.WriteLine("Server started on port 8080...");

            while (true)
            {
                // 接受客户端连接
                Socket clientSocket = serverSocket.Accept();

                // 处理请求
                HandleClientRequest(clientSocket);
            }
        }

        static void HandleClientRequest(Socket clientSocket)
        {
            // 读取请求数据
            byte[] buffer = new byte[4096];
            int bytesRead = clientSocket.Receive(buffer);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 处理跨域请求（CORS）
            if (request.Contains("Origin:"))
            {
                // 获取 Origin 头部的值
                int originIndex = request.IndexOf("Origin:");
                int endIndex = request.IndexOf("\r\n", originIndex);
                string origin = request.Substring(originIndex + 7, endIndex - (originIndex + 7)).Trim();

                // 设置允许跨域请求的响应头
                string response = "HTTP/1.1 200 OK\r\n" +
                                  "Access-Control-Allow-Origin: " + origin + "\r\n" +
                                  "Access-Control-Allow-Methods: POST, GET\r\n" +
                                  "Access-Control-Allow-Headers: Content-Type\r\n" +
                                  "\r\n";

                // 发送响应
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                clientSocket.Send(responseBytes);
            }
            else
            {
                // 处理其他请求（例如处理 GET 请求）
                // 在这里添加你的业务逻辑代码
            }

            // 关闭连接
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}