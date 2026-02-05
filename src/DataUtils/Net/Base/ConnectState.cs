using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;

namespace Feng.Net.Base
{
    public static class ConnectState
    {
        public const byte NotConnection= 1;
        public const byte Connectioning= 2;
        public const byte Closed = 3;
        public const byte ConnectionMore = 4;

        public static string GetStateText(byte state)
        {
            switch (state )
            {
                case ConnectState .Closed :
                    return "CLOSED";

                case ConnectState.Connectioning :
                    return "Connectioning";

                case ConnectState.NotConnection:
                    return "NotConnection";

                case ConnectState.ConnectionMore:
                    return "ConnectionMore";
                default:
                    break;
            }
            return string.Empty;
        }

    }

}
