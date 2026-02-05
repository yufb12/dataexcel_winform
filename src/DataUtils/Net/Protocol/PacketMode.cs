using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;

namespace Feng.Net.Packets 
{

    public static class PacketMode
    {
        public const byte Send = 1;
        public const byte POST = 3;
        public const byte ChangServer = 16;
        public const byte ANSWER = 32;
        public const byte ERROR = 128;
        public const byte TIMEOUT = 0XE;

    }
}
