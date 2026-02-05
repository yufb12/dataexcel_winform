
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic; 
using System.Net.NetworkInformation;
using Feng.Net.Interfaces;
using Feng.Net.Packets; 

namespace Feng.Net
{
    public class NetErrorTool
    {
        static NetErrorTool()
        {
            IncludSpecificInformation = true;
        }
        public static bool IncludSpecificInformation { get; set; }
        public static void RespondError(IClientProxy clientproxy, NetPacket ph, Exception ex)
        {
            string msg = ex.Message;
            if (IncludSpecificInformation)
            {
                msg = ex.Source + "\r\n"  + ex.StackTrace;
            }
            ph.PacketMode = PacketMode.ANSWER | PacketMode.ERROR;
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(1, ex.Message);
                bw.Write(2, msg);
                ph.PacketContents = bw.GetData();
            }
            clientproxy.SendToRemote(ph);

        }
    }
}