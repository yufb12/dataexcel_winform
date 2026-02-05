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
using Feng.Net.Base;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
namespace Feng.Net.Extend
{
    public class ClientExtendUdpP2P : ClientExtendBase, IDisposable 
    {

        //1.Check ID Exists
        //2.Open UDP Server
        //3.Udp to Connection Server
        //4.Initiator Send ID
        //5.Udp Server tell Receiver To Connected
        //6.Udp Server tell Initiator Receiver's Address(ip,port)
        //7.Udp Server tell Receiver Initiator's Address(ip,port)
        //8.Initiator Ping Receiver
        //9.Receiver Ping Initiator

        public ClientExtendUdpP2P()
        {

        }

        public override void Bingding(ClientProxyBase client)
        {
            if (client != null)
            {
                client.DataReceive += client_DataReceived;
                base.Bingding(client);
            }
        }
        UDP.UdpClient udpclient = null;
        public UDP.UdpClient UdpClient
        {
            get
            {
                return udpclient;
            }
        }
        public virtual void InitRemoteClientID(string remoteclientid)
        {
            RemoteClientID = remoteclientid;
        }

        private string udpserverip = string.Empty;
        private int udpserverport = 0;
        Feng.Net.Extend.ClientExtendKernal kernalextend = new ClientExtendKernal();


        //第一步 检查服务器上的UDP服务器是否打开，如果未打开则打开
        public virtual NetResult OpenUdpServer()
        {
            Feng.Utils.TraceHelper.WriteTrace("DataUtils","CLIENT", this.Name,true, "执行:" + "打开服务上UDP服务器");
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.UDP, PacketSubCmd_UdpCommandSection.OpenUdpServer);
            NetResult result = this.Client.Send(ph);
            if (result.Success)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "执行:" + "打开服务上UDP服务器" + " 结果：成功");
                ph = NetPacket.Get(result.OrgValue);
                ph.BeginRead();
                string res = ph.Reader.ReadString();
                string ip = ph.Reader.ReadString();
                if (string.IsNullOrWhiteSpace(ip))
                {
                    udpserverip = Client.RemoteIP;
                }
                int port = ph.Reader.ReadInt();
                udpserverport = port;
                ph.EndRead();
            }
            return result;
        }

        //第二步 查询对方ID存不存在，并通知对方本地的ID，并执行下面的步骤
        public virtual NetResult Step2()
        {
            return CheckClient();
        }
        public virtual NetResult CheckClient()
        {
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "执行:" + "检验ID是否存在");
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.UDP, PacketSubCmd_UdpCommandSection.CheckClientIP);
            ph.BeginWriter();
            ph.Writer.Write(RemoteClientID);
            ph.Writer.Write(udpserverip);
            ph.Writer.Write(udpserverport);
            ph.EndWriter();
            ph.PacketMode = (byte)PacketMode.Send;
            NetResult result = this.Client.Send(ph);
            if (result.Success)
            { 
                ph = NetPacket.Get(result.OrgValue);
                ph.BeginRead();
                string rid = ph.Reader.ReadString();
                string ip = ph.Reader.ReadString();
                if (string.IsNullOrWhiteSpace(ip))
                {
                    udpserverip = Client.RemoteIP;
                }
                int port = ph.Reader.ReadInt();
                udpserverport = port;
                ph.EndRead();
                if (string.IsNullOrWhiteSpace(rid))
                {
                    NetResult.ErrorResult(RemoteClientID + " not exists", NetResult.CODE_USER);
                }
            } 
            return result;
        }
        public virtual NetResult DoP2PStep()
        {
            NetResult result = Step3();
            if (!result.Success)
                return result;
            result = Step4();
            if (!result.Success)
                return result;
            result = Step5();
            if (!result.Success)
                return result;
            result = Step6();
            if (!result.Success)
                return result;
            //result = Step7();
            //if (!result.Success)
            //    return result;
            result = Step8();
            if (!result.Success)
                return result;
            result = Step9();
            return result; 
        }
        //第三步 连接UDP服务器 并获取注册Session 让服务器端UDP服务器拒绝非法连接
        public virtual NetResult Step3()
        {
            return ConnectionUdpServer();
        }
        public virtual NetResult ConnectionUdpServer()
        {
            udpclient = new UDP.UdpClient();
            kernalextend.Bingding(udpclient);
            udpclient.LocalIP = NetSettings.LocalHostIP;
            udpclient.LoalPort = NetSettings.NextLoacPort;
            udpclient.RemoteIP = this.udpserverip;
            udpclient.RemotePort = this.udpserverport;
            udpclient.Connected += client_Connected;
            udpclient.Open();
            udpclient.Read();
            udpclient.DataReceive += udpclient_DataReceive;
            udpclient.Connection();
            
            OnUdpClientCreate(udpclient);
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "执行:" + "连接UDP服务器");
            NetResult result = kernalextend.RegeditSession("admin", "123456", null);
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "执行:" + "RegeditSession");
            return result;
        }

        //第四步 获取本机相关UDP客户端的对应的外网的IP与接口
        public virtual NetResult Step4()
        {
            return GetUdpRemoteLoaclInfo();
        }
        public virtual NetResult GetUdpRemoteLoaclInfo()
        {
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.UDP, PacketSubCmd_UdpCommandSection.GetUdpRemoteLoaclInfo);
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "执行:" + "获取服务上关于本连接客户端信息");
            NetResult result = udpclient.Send(ph);
            if (result.Success)
            {
                ph = NetPacket.Get(result.OrgValue);
                ph.BeginRead();
                string ip = ph.Reader.ReadString();
                int port = ph.Reader.ReadInt();
                ph.BeginRead();
                NetAddress add = new NetAddress();
                add.IP = ip;
                add.Port = port;
                UdpRemoteLocalMyAddress = add; 
            }
            return result;
        }

        //第五步 向对方发送本机的外网的IP与端口
        public virtual NetResult Step5()
        {
            return SendToRemoteClientLocalUdpRemoteAddress();
        }
        public virtual NetResult SendToRemoteClientLocalUdpRemoteAddress()
        {
            if (UdpRemoteLocalMyAddress == null)
                return NetResult.ErrorResult("Empty My RemoteIP", NetResult.CODE_USER);
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.UDP, PacketSubCmd_UdpCommandSection.TellRomteAddress);
            ph.BeginWriter();
            ph.Writer.Write(this.Client.ID);
            ph.Writer.Write(UdpRemoteLocalMyAddress.IP);
            ph.Writer.Write(UdpRemoteLocalMyAddress.Port);
            ph.Writer.Write(RemoteClientID);
            ph.EndWriter();
            NetResult result = this.Client.Post(ph);
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name,true, "执行:" + "向对方发送本机的外网的IP与端口" + UdpRemoteLocalMyAddress.IP + " " + UdpRemoteLocalMyAddress.Port);
            return result;
        }

        //第六步 切换本地UDP连接的服务器地址为远程客户端
        public virtual NetResult Step6()
        {
            return ChangedP2P();
        }
        public virtual NetResult ChangedP2P()
        {
            for (int i = 0; i < 50; i++)
            {
                if (string.IsNullOrWhiteSpace(P2P_ClientIP))
                {
                    int waketime = i * 10;
                    if (waketime > 60)
                    {
                        waketime = 60;
                    }
                    System.Threading.Thread.Sleep(waketime);
                }
            }
            if (string.IsNullOrWhiteSpace(P2P_ClientIP))
            {
                return NetResult.ErrorResult("P2P_ClientIP Is NULL", NetResult.CODE_USER);
            }
            UdpClient.RemoteIP = P2P_ClientIP;
            UdpClient.RemotePort = P2P_ClientPort;
            return NetResult.SuccessResult();
           
        }
 
        //第七步 换行第一次打洞
        public virtual NetResult Step8()
        {
            return DoFirstP2P();
        }
        public virtual NetResult DoFirstP2P()
        {
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.UDP, PacketSubCmd_UdpCommandSection.FirstP2P);
            NetResult result = udpclient.Post(ph);
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "执行:" + "执行第一次连接");
            return result;
        }

        //第八步 发送测试数据验证是否打洞成功
        public virtual NetResult Step9()
        {
            return CheckP2PSuccess();
        }

        public virtual NetResult CheckP2PSuccess()
        {
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.UDP, PacketSubCmd_UdpCommandSection.P2PSuccess);
            ph.BeginWriter();
            ph.Writer.Write("UdpName:" + this.Name + " 收到打洞的数据！" + DateTime.Now.ToString());
            ph.EndWriter();
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "执行:" + "发送测试数据验证是否打洞成功");
            NetResult result = udpclient.Send(ph);
            if (result.Success)
            {
                ph = NetPacket.Get(result.OrgValue);
                ph.BeginRead();
                string value = ph.Reader.ReadString();
                ph.EndRead();
                Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "结果:" + "打洞成功!" + " 打洞数据:" + value);
            }
            else
            {
                Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "结果:" + "打洞失败");
            }
            return result;
        }
         
        public string RemoteClientID = string.Empty;
 
        void udpclient_DataReceive(object sender, ReciveEventArgs e, NetPacket ph)
        {
            if (ph.PacketMainCommand == PacketMainCmd.UDP)
            {
                switch (ph.PacketSubcommand)
                {
                    case PacketSubCmd_UdpCommandSection.P2PSuccess:
                        e.ClientProxy.Respond(ph);
                        Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "结果:" + "收到打洞数据，并完成回复！");
                        break;
                    default:
                        break;
                }
            }
        }

        void client_Connected(object sender, IClientProxy client)
        {
            //bool res = kernalextend.RegeditSession("admin", "123456");
        }
 
        void client_DataReceived(object sender, ReciveEventArgs e, NetPacket ph)
        { 
            if (ph != null)
            {
                if (ph.PacketMainCommand == PacketMainCmd.UDP)
                {
                    switch (ph.PacketSubcommand)
                    { 
                        case PacketSubCmd_UdpCommandSection.SC_TLRMMyAdd:
                            CommandSC_TLRMMyAdd(sender, e, ph);
                            break; 
                        case PacketSubCmd_UdpCommandSection.SC_BeginP2P:
                            CommandSC_BeginP2P(sender, e, ph);
                            break; 
                        default:
                            break ;
                    }
                }
            }
        }
        private void CommandConnectUdpServer(object sender, ReciveEventArgs e, NetPacket ph)
        {
            ph.BeginRead();
            string res = ph.Reader.ReadString();
            string ip = ph.Reader.ReadString();
            if (string.IsNullOrWhiteSpace(ip))
            {
                udpserverip = Client.RemoteIP;
            }
            int port = ph.Reader.ReadInt();
            udpserverport = port;
            ph.EndRead();
            ConnectionUdpServer();
        }
        private string P2P_ClientIP = string.Empty;
        private int P2P_ClientPort = 0;
        private void CommandSC_TLRMMyAdd(object sender, ReciveEventArgs e, NetPacket ph)
        { 
            ph.BeginRead();
            string res = ph.Reader.ReadString();
            string selid = ph.Reader.ReadString();
            string ip = ph.Reader.ReadString();
            int port = ph.Reader.ReadInt();
            string id = ph.Reader.ReadString();
            udpserverport = port;
            ph.EndRead();
            this.RemoteClientID = id;
            P2P_ClientIP = ip;
            P2P_ClientPort = port;
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, ip + "：" + port + "请求连接");
        }
 
        private void CommandSC_BeginP2P(object sender, ReciveEventArgs e, NetPacket ph)
        {
            Feng.Utils.TraceHelper.WriteTrace("DataUtils", "CLIENT", this.Name, true, "收到:" + " 进行P2P请救");
            ph.BeginRead();
            string clientid = ph.Reader.ReadString();
            udpserverip = ph.Reader.ReadString();
            udpserverport = ph.Reader.ReadInt32(); 
            this.RemoteClientID = clientid;
            ph.EndRead(); 
            DoP2PStep();
        }
        public void Close()
        {
            if (UdpClient != null)
            {
                UdpClient.Close();
            }
        }
        public override void UnBingding()
        {
            base.UnBingding();
        }
        public void Dispose()
        {
            Close();
        }
        public delegate void UdpClientCreateHandler(object sender, UDP.UdpClient udpclient);
        public event UdpClientCreateHandler UdpClientCreate;
        public virtual void OnUdpClientCreate(UDP.UdpClient udpclient)
        {
            if (UdpClientCreate != null)
            {
                UdpClientCreate(this, udpclient);
            }
        }
 
        private NetAddress UdpRemoteLocalMyAddress = null;

        public virtual string Name { get { return this.Client.ID; } }

        public NetResult Send(NetPacket ph)
        {
            return this.UdpClient.Send(ph);
        }
    }
}