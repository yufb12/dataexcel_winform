using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Net.Dms
{
    public class DMSServer
    {
        Feng.Net.Tcp.TcpServer server = new Tcp.TcpServer();
        public int Port = 21109;
        public void Start()
        {
            server.Port = Port;
            server.Open();
        }
        public void SendMsg()
        {
            
        }
    }
     
}
