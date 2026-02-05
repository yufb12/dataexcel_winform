using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace Feng.Net.Tcp
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 8192;
        public byte[] buffer = new byte[BufferSize];
        public void ReSetBuffer()
        {
            buffer = new byte[BufferSize];
        }
    }
}
