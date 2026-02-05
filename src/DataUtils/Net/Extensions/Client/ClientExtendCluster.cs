using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Feng.Net.Tcp;
using Feng.Net.Packets;
using Feng.Net.NetArgs;
using Feng.Net.Base;
namespace Feng.Net.Extend
{
    public class ClientExtendCluster : ClientExtendBase, IDisposable 
    {
        public ClientExtendCluster()
        {

        }

        private bool _hasregeditsession = false;
        public bool HasRegeditSession
        {
            get
            {
                return _hasregeditsession;
            }
            set
            {
                _hasregeditsession = value;
            }
        }

        public override void Bingding(ClientProxyBase client)
        {
            client.DataReceive += client_DataReceived;
            base.Bingding(client);
        }

        public override void UnBingding()
        {
            this.Client.DataReceive += client_DataReceived;
            base.UnBingding();
        }

        void client_DataReceived(object sender, ReciveEventArgs e, NetPacket ph)
        {

            try
            { 
                DoCommand(ph);
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
  
        }
         
        public void Dispose()
        {
            UnBingding();
        }

        public virtual void DoCommand(NetPacket ph)
        {
            if (ph.PacketMainCommand == PacketMainCmd.System)
            {
                switch (ph.PacketSubcommand)
                {
                    case PacketSystemSubCmd.Heartbeat:
                        break;  
                    default:
                        break;
                }
            }
        }

        public virtual IPEndPoint GetServer(string url)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.System, PacketSystemSubCmd.GetServer);
            ph.BeginWriter();
            ph.Writer.Write(url);
            ph.EndWriter();
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                ph.BeginRead();
                string ip = ph.Reader.ReadIndex(1, string.Empty);
                int port = ph.Reader.ReadIndex(2, 0);
                ph.EndRead();
                if (!string.IsNullOrWhiteSpace(ip))
                {
                    IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
                    return point;
                }
            }
            return null;
        }

    }
}