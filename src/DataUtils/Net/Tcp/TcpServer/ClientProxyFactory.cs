using Feng.Net.Interfaces;
using Feng.Net.Tcp;
using Feng.Net.UDP;
using System.Net;
using System.Net.Sockets;

namespace Feng.Net.Base
{
    public class ClientProxyFactory
    {
        public ClientProxyFactory()
        {

        }

        public static IClientProxy CreatProxy(Socket handler, NetServer server)
        {
            return new TcpAsyncClientProxy(handler, server);
        }
        public static IClientProxy CreatUdpClientProxy(Socket handler, NetServer server, EndPoint ippoint)
        {
            return new UdpClientProxy(handler, server, ippoint);
        }
    }


}
