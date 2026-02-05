
using Feng.Net.EventHandlers;
using Feng.Net.NetArgs;
using Feng.Net.Packets;
using System;

namespace Feng.Net.Interfaces
{
    public interface IClientProxy
    {
        string ID { get; set; }
        string Name { get; set; }
        string LocalIP { get; }
        int LoalPort { get; }
        string RemoteIP { get; set; }
        int RemotePort { get; set; }
        int Session { get; set; }
        void Close();

        object Tag { get; set; }

        /// <summary>
        /// PacketMode=Respond
        /// </summary>
        /// <param name="ph"></param>
        /// <returns></returns>
        NetResult Respond(NetPacket ph);
        /// <summary>
        /// PacketMode=Send
        /// </summary>
        /// <param name="ph"></param>
        /// <returns></returns>
        NetResult Send(NetPacket ph);
        bool Send(byte [] data);
        /// <summary>
        /// PacketMode=Post
        /// </summary>
        /// <param name="ph"></param>
        /// <returns></returns>
        NetResult Post(NetPacket ph);
        /// <summary>
        /// PacketMode  Manual
        /// </summary>
        /// <param name="ph"></param>
        /// <returns></returns>
        NetResult SendToRemote(NetPacket ph);

        DateTime ConnectionTime { get; }
        bool HasConnected { get; set; }
        DateTime LastRecvDataTime { get; }
        event DataReceiveEventHandler DataReceive;
        void Read();

        void ReceiveData(ReciveEventArgs recvdata);
        int State { get; set; }

        long SendTimes { get; }
        long RecvTimes { get; }

        void SendAdd();
        void RecvAdd();
    }

}
