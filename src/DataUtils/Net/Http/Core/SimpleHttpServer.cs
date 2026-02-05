using Feng.Net.Tcp;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpClient = System.Net.Sockets.TcpClient;

namespace Feng.Net.Http
{ 
    public class SimpleHttpServer
    {
        private TcpListener listener;
        private const int DefaultPort = 43388;  // 默认端口号
        private bool isRunning = false;
        public static void Test()
        {

            SimpleHttpServer server = new SimpleHttpServer();
            server.Start();
            while (true)
            {
                Thread.Sleep(100);
            }
        }
        public SimpleHttpServer()
        {
            listener = new TcpListener(IPAddress.Any, DefaultPort);
        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(this.Listen));
            serverThread.Start();
        }

        public void Stop()
        {
            this.isRunning = false;
            this.listener.Stop();
        }

        private void Listen()
        {
            this.isRunning = true;
            this.listener.Start();

            Console.WriteLine("Web server started. Listening on port {0}", DefaultPort);

            while (this.isRunning)
            {
                TcpClient client = this.listener.AcceptTcpClient();

                Thread clientThread = new Thread(new ParameterizedThreadStart(this.HandleClient));
                clientThread.Start(client);
            }
        }

        private void HandleClient(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;

            StreamReader reader = new StreamReader(client.GetStream());
            string request = reader.ReadLine();

            Console.WriteLine("Request: {0}", request);

            string[] tokens = request.Split(' ');
            string method = tokens[0];
            string url = tokens[1];

            if (method == "POST") // 处理POST请求
            {
                string postData = "";
                while (reader.Peek() >= 0)
                {
                    postData += reader.ReadLine();
                }

                Console.WriteLine("POST data: {0}", postData);

                // 在这里处理POST请求数据
                // ...

                // 返回响应数据
                string responseString = "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\n\r\n" +
                    "<html><body><h1>Hello World!</h1></body></html>";
                byte[] responseBytes = Encoding.ASCII.GetBytes(responseString);
                client.GetStream().Write(responseBytes, 0, responseBytes.Length);
            }
            else // 处理GET请求
            {
                // 在这里处理GET请求
                // ...

                // 返回响应数据
                string responseString = "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\n\r\n" +
                    "<html><body><h1>Hello World!</h1></body></html>";
                byte[] responseBytes = Encoding.ASCII.GetBytes(responseString);
                client.GetStream().Write(responseBytes, 0, responseBytes.Length);
            }

            client.Close();
        }
    }
}