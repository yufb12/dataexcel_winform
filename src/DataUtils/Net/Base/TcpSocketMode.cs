using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

namespace Feng.Net.Tcp
{
    [Serializable]
    public class NetCommand
    {
        public virtual void Execute()
        {

        }
    }
}