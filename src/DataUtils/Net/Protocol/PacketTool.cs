using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Feng.Net.Tcp;

namespace Feng.Net.Packets
{
    public static  class PacketTool
    {
        public static NetPacket GetPacket(short packetmaincommand, int packetsubcommand)
        {
            NetPacket ph = new NetPacket(packetmaincommand,
   packetsubcommand);
            return ph;
        }
        public static NetPacket GetPacket(short packetmaincommand, int packetsubcommand, string data)
        {
            NetPacket ph = new NetPacket(packetmaincommand, packetsubcommand, data); 
            return ph;
        }
        public static NetPacket GetPacket(short packetmaincommand, int packetsubcommand, byte[] data)
        {
            NetPacket ph = new NetPacket(packetmaincommand, packetsubcommand, data);
            return ph;
        }
        public static NetPacket GetPacket(short packetmaincommand, int packetsubcommand, string[] args)
        {
            byte[] data = NetPacket.Empty;
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                foreach (string text in args)
                {
                    bw.Write(text);
                }
                data = bw.GetData();
            }
            NetPacket ph = new NetPacket(packetmaincommand, packetsubcommand, data);
            return ph;
        }

        public static void WriteSucessResult(NetPacket packet)
        {
            packet.PacketContents = new byte[] { };
        }
        public static readonly byte[] EmptyContents = null;
    }
}
