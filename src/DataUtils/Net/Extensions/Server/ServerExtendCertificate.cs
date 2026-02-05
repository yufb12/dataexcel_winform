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

    public class ServerExtendCertificate : ServerExtendBase
    {
        public ServerExtendCertificate(Base.NetServer server) :base(server)
        {
            
        }
        public override void DoExtendCommand(NetPacket ph, ReciveEventArgs e)
        {
            if (ph.PacketMainCommand == PacketMainCmd.System)
            {
                switch (ph.PacketSubcommand)
                {
                    //case PacketSystemSubCmd.RegeditSession:
                    //    DoCommandRegeditSesstion(e, ph);
                    //    break; 
                    default:
                        break;
                }
            }
        }
 
        public virtual void DoCommandRegeditSesstion(ReciveEventArgs ee,NetPacket ph)
        {
            ph.BeginRead();
            string user = ph.Reader.ReadString();
            string password = ph.Reader.ReadString();
            byte[] data = ph.Reader.ReadBuffer(); 
            ph.EndRead();
 
        }
 
    }

}
