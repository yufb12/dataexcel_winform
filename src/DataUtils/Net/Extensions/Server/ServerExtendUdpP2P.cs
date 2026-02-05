using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;
using System.IO;
using Feng.Net.Tcp;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
using Feng.Net.Packets;

namespace Feng.Net.Extend
{
    public class ServerExtendUdpP2P : ServerExtendBase
    {
        public ServerExtendUdpP2P(Base.NetServer server) : base(server)
        {

        }  
        public override void DoExtendCommand(NetPacket ph, ReciveEventArgs e)
        {
            if (ph.PacketMainCommand == PacketMainCmd.UDP)
            {
                int uc = ph.PacketSubcommand;
                switch (uc)
                {
                    case PacketSubCmd_UdpCommandSection.CheckClientIP:
                        CheckClientIP(ph, e);
                        break;
                    case PacketSubCmd_UdpCommandSection.OpenUdpServer:
                        OpenUdpServer(ph, e);
                        break; 
                    case PacketSubCmd_UdpCommandSection.TellRomteAddress:
                        DoComTLRMMyAdd(ph, e);
                        break; 
                    default:
                        break;
                }
            }
        }
 
 
        public virtual void DoComTLRMMyAdd(NetPacket ph, ReciveEventArgs e)
        {
            ph.BeginRead();
            string selfid = ph.Reader.ReadString();
            string ip = ph.Reader.ReadString();
            int port = ph.Reader.ReadInt();
            string id = ph.Reader.ReadString();
            ph.EndRead();
            e.ClientProxy.Respond(ph);
            IClientProxy clientproxy = this.Server.Clients.Get(id);
            if (clientproxy != null)
            {
                ph = PacketTool.GetPacket(PacketMainCmd.UDP, PacketSubCmd_UdpCommandSection.SC_TLRMMyAdd);
                ph.BeginWriter();
                ph.Writer.Write(Feng.Utils.Constants.OKText);
                ph.Writer.Write(id);
                ph.Writer.Write(ip);
                ph.Writer.Write(port);
                ph.Writer.Write(selfid);
                ph.EndWriter();
                clientproxy.Post(ph);
            } 
        }


        public virtual void CheckClientIP(NetPacket ph, ReciveEventArgs e)
        {
            ph.BeginRead();
            string id = ph.Reader.ReadString();
            string udpserverip = ph.Reader.ReadString();
            int port = ph.Reader.ReadInt32();
            ph.EndRead();
            e.ClientProxy.Respond(ph);
            DoCMDSC_BeginP2P(id, e.ClientProxy.ID, udpserverip, port);
        }
        public virtual void DoCMDSC_BeginP2P(string id, string remoteclientid, string udpserverip, int port)
        {
            IClientProxy clientproxy = this.Server.Clients.Get(id);
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.UDP, PacketSubCmd_UdpCommandSection.SC_BeginP2P);
            ph.BeginWriter();
            ph.Writer.Write(remoteclientid);
            ph.Writer.Write(udpserverip);
            ph.Writer.Write(port); 
            ph.EndWriter();
            clientproxy.Post(ph);
        }
        private static UDP.UdpServer udpserver = null;
 
        public virtual void OpenUdpServer(NetPacket ph, ReciveEventArgs e)
        {
            OpenUdpServer();
            ph.BeginWriter();
            ph.Writer.Write(Feng.Utils.Constants.OKText);
            ph.Writer.Write(string.Empty);
            ph.Writer.Write(udpserver.Port);
            ph.EndWriter();
            e.ClientProxy.Respond(ph);
        }
        void udpserver_ClientConnectioned(object sender, IClientProxy client)
        {
            try
            {
                ServerController serverController = new ServerController(udpserver, client);
                ServerExtendKernal kernalextend = new ServerExtendKernal(udpserver);
                serverController.Bingding(kernalextend);
                kernalextend.RegeditSession += server_RegeditSession;
                client.DataReceive += client_DataReceive;
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
        }

        int session = 1001;
        void server_RegeditSession(object sender, RegeditSessionEventArgs e)
        {
            e.Session = session;// Feng.Utils.RandomCache.Next(1000000, 2000000);
            e.ID = session.ToString();// DateTime.Now.ToString("HHmmss");// Guid.NewGuid().ToString();
            session++;
        }

        //void server_RegeditSession(object sender, Feng.Net.RegeditSessionEventArgs e)
        //{
        //    e.Session = Feng.Utils.RandomCache.Next();
        //    e.ID = DateTime.Now.ToString("HHmmss");// Guid.NewGuid().ToString();
        //}
        void client_DataReceive(object sender, ReciveEventArgs e, NetPacket ph)
        {
            try
            {
                if (ph.PacketMainCommand == PacketMainCmd.UDP)
                {
                    switch (ph.PacketSubcommand)
                    {
                        case PacketSubCmd_UdpCommandSection.ConnectionUdpServer:
                            e.ClientProxy.Respond(ph);
                            break;
                        case PacketSubCmd_UdpCommandSection.GetUdpRemoteLoaclInfo:
                            DoUdpCom_GetUdpRemoteLoaclInfo(sender, e, ph);
                            break; 
                        default:
                            break;
                    } 
                } 
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
            }
        }
        public virtual void DoUdpCom_GetUdpRemoteLoaclInfo(object sender, ReciveEventArgs e, NetPacket ph)
        {
            ph.BeginWriter();
            ph.Writer.Write(e.ClientProxy.RemoteIP);
            ph.Writer.Write(e.ClientProxy.RemotePort);
            ph.EndWriter();
            e.ClientProxy.Respond(ph);
                
        }
 
        public virtual void NotiClientOpenUdp(NetPacket ph,string id)
        {
            IClientProxy clientproxy = this.Server.Clients.Get(id);
            clientproxy.Send(ph);
        }
 
        public string CleintIp(string id)
        {
            string result = string.Empty;
            try
            {
                for (int i = this.Server.Clients.Count - 1; i >= 0; i--)
                {
                    IClientProxy clientproxy = this.Server.Clients[i];
                    if (clientproxy.ID == id)
                    {
                        return id; 
                    } 
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
            }
            return result;
        }
        public delegate void UdpServerCreateHandler(object sender, UDP.UdpServer udpserver);
        public event UdpServerCreateHandler UdpServerCreate;
        public virtual void OnUdpClientCreate(UDP.UdpServer udpserver)
        {
            if (UdpServerCreate != null)
            {
                UdpServerCreate(this, udpserver);
            }
        }

        public delegate UDP.UdpServer UdpServerBeforeCreateHandler(object sender);
        public event UdpServerBeforeCreateHandler UdpServerBeforeCreate;
        public virtual UDP.UdpServer OnUdpServerBeforeCreate()
        {
            if (UdpServerBeforeCreate != null)
            {
                return UdpServerBeforeCreate(this);
            }
            return null;
        }

        public virtual void OpenUdpServer()
        {
            udpserver = OnUdpServerBeforeCreate();
            if (udpserver == null)
            {
                udpserver = new UDP.UdpServer();
            }
            OnUdpClientCreate(udpserver);
            if (!udpserver.Opened)
            {
                udpserver.Open();
                udpserver.Connectioned += udpserver_ClientConnectioned;
            } 
        }
    }

}
