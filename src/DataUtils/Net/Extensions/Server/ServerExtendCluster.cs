using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using Feng.Net.Tcp;
using System.ComponentModel;
using Feng.Net.NetArgs;
using Feng.Net.Packets;

namespace Feng.Net.Extend
{

    public class ServerExtendCluster : ServerExtendBase
    {
        public ServerExtendCluster(Base.NetServer server) : base(server)
        {

        }
        public override void DoExtendCommand(NetPacket ph, ReciveEventArgs e)
        {
            if (ph.PacketMainCommand == PacketMainCmd.System)
            {
                switch (ph.PacketSubcommand)
                {
                    case PacketSystemSubCmd.RegeditSession:
                        break;
                    default:
                        break;
                }
            }
        }
        public delegate void GetServerEventHandler(object sender, GetServerEventArgs e);
        public event GetServerEventHandler GetServer;
        public virtual void OnGetServer(GetServerEventArgs e)
        {
            if (GetServer != null)
            {
                GetServer(this, e);
            }
        }

    }

}
