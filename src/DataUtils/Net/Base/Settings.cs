using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Net.Base
{
    public static class NetSettings
    {
        public static int BufferSize = 512;
        public static bool ProcessExit = false;
        public static int DefultTcpServerPort = 6655;
        public static int DefultUdpServerPort = 6688;
        public static int DefultPort2 = 6699;
        public static string DeafultIPAddress = "192.168.1.6";
        public static string LocalHostIP = "127.0.0.1";
        public static int ConnectionWaitingTime = 3;
 
        public static int DeafultWaitServerReturn = 15;
 
        public static int SendBufferSize = 8192;
        public static int RecvBufferSize = 512;
        public const short PacketHeader = 0x0A02;//版本号
        public const short PacketFooter = 0x0A02;

        private static int LocalPortCache = 20000;
        public static int NextLoacPort
        {
            get
            {
                return LocalPortCache++;
            }
        }
    }
}
