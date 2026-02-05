using Feng.Net.Base;
using Feng.Net.NetArgs;
using Feng.Net.Packets;
using System;
using System.Collections.Generic;
using System.Net;
namespace Feng.Net.Extend
{
    public class ClientExtendCertificate : ClientExtendBase, IDisposable
    {
        public ClientExtendCertificate()
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
                    case PacketSystemSubCmd.Execute:
                        DoDomExecute(ph);
                        break;
                    case PacketSystemSubCmd.Attach:
                        OnDoDomAttach(ph);
                        break;
                    default:
                        break;
                }
            }
        }

        public virtual bool RegeditSession(string user, string password)
        {
            NetResult result = RegeditSession(user, password, new byte[] { });
            return result.Success;
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

        public delegate void DomExecuteEventHandler(object sender, NetPacket ph);

        public delegate void DomAttachEventHandler(object sender, NetPacket ph);

        public event DomAttachEventHandler DomAttach;

        public event DomExecuteEventHandler DomExecute;
        protected virtual void DoDomExecute(NetPacket ph)
        {
            if (DomExecute != null)
            {
                DomExecute(this, ph);
            }
        }

        protected virtual void OnDoDomAttach(NetPacket ph)
        {
            if (DomAttach != null)
            {
                DomAttach(this, ph);
            }
        }

        public delegate void AppUpdateEventHandler(object sender, NetPacket ph);
        public event AppUpdateEventHandler AppUpdate;
        private void OnDoAppUpdate(NetPacket ph)
        {
            if (AppUpdate != null)
            {
                AppUpdate(this, ph);
            }
        }
        public virtual DateTime GetServerTime()
        {
            DateTime dt = DateTime.MinValue;
            NetPacket ph = new NetPacket();
            ph.PacketMainCommand = PacketMainCmd.System;
            ph.PacketSubcommand = PacketSystemSubCmd.ServerTime;
            ph.WaitTime = 3;
            NetResult fengresult = this.Client.Send(ph, out ph);
            if (fengresult.Success)
            {
                using (Feng.IO.BufferReader reader = new IO.BufferReader(ph.PacketContents))
                {
                    dt = reader.ReadIndex(1, DateTime.MinValue);
                }
            }
            return dt;
        }

        public delegate void SessionChangedHandler(object sender, int Session);
        public event SessionChangedHandler SessionChanged;
        public virtual void OnSessionChanged(int session)
        {
            if (SessionChanged != null)
            {
                SessionChanged(this, session);
            }
        }

        public virtual NetResult RegeditSession(string user, string password, byte[] data)
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(user);
                bw.Write(password);
                bw.Write(data);
                NetPacket ph = new NetPacket(PacketMainCmd.System,
                 PacketSystemSubCmd.RegeditSession, bw.GetData());

                NetResult fengresult = this.Client.Send(ph);
                if (fengresult.Success)
                {
                    ph = NetPacket.Get(fengresult.OrgValue);
                    ph.BeginRead();
                    int session = ph.Reader.ReadIndex(1, 0);
                    string id = ph.Reader.ReadIndex(2, string.Empty);
                    ph.EndRead();
                    if (session > 1)
                    {
                        this.Client.Session = session;
                        this.Client.ID = id;
                        this.Client.Name = user;
                        _hasregeditsession = true;
                        this.OnSessionChanged(this.Client.Session);
                    }
                }
                return fengresult;
            }
        }

        public virtual NetResult HeartBeat()
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(0);
                NetPacket ph = new NetPacket(PacketMainCmd.System,
                PacketSystemSubCmd.Heartbeat, bw.GetData());
                ph.WaitTime = 2;
                ph.PacketMode = PacketMode.Send;
                NetResult fengresult = this.Client.Send(ph);
                return fengresult;
            }
        }

        public virtual NetResult Ping()
        {
            NetPacket ph = new NetPacket(PacketMainCmd.System,
            PacketSystemSubCmd.Ping);
            NetResult fengresult = this.Client.Post(ph);
            return fengresult;

        }

        public virtual NetResult UpdateChecked(out int count, byte[] data)
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(data);
                NetPacket ph = new NetPacket(PacketMainCmd.File,
                    PacketSystemSubCmd.AutoUpdate, bw.GetData());
                ph.PacketMode = (byte)PacketMode.Send;
                count = 0;
                NetResult fengresult = this.Client.Send(ph);
                if (fengresult.Success)
                {
                    ph = NetPacket.Get(fengresult.OrgValue);
                    using (Feng.IO.BufferReader reader = new IO.BufferReader(ph.PacketContents))
                    {
                        count = reader.ReadInt32();
                        data = reader.ReadBytes();
                    }
                }
                return fengresult;
            }
        }

        public virtual NetResult VersionChecked(string version, int upgrade, out int outversion, out string downloadurl, byte[] data)
        {
            outversion = 0;
            downloadurl = string.Empty;
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(version);
                bw.Write(upgrade);
                if (data == null)
                {
                    bw.Write(new byte[] { });
                }
                else
                {
                    bw.Write(data);
                }
                NetPacket ph = new NetPacket(PacketMainCmd.System,
                    PacketSystemSubCmd.CheckVersion, bw.GetData());
                ph.PacketMode = (byte)PacketMode.Send;

                NetResult fengresult = this.Client.Send(ph);
                if (fengresult.Success)
                {
                    ph = NetPacket.Get(fengresult.OrgValue);
                    using (Feng.IO.BufferReader reader = new IO.BufferReader(ph.PacketContents))
                    {
                        downloadurl = reader.ReadString();
                        outversion = reader.ReadInt32();
                    }
                }
                return fengresult;
            }
        }

        public virtual void DoDomAttach()
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                List<string> maclist = new List<string>();
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    System.Net.NetworkInformation.NetworkInterface[] nis = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                    if (nis.Length > 0)
                    {
                        foreach (System.Net.NetworkInformation.NetworkInterface ni in nis)
                        {
                            maclist.Add(ni.Id);
                        }
                    }
                }
                string name = System.Environment.MachineName;
                bw.Write(maclist);
                bw.Write(name);
                NetPacket ph = new NetPacket(PacketMainCmd.System,
                    PacketSystemSubCmd.Attach, bw.GetData());
                if (this.Client.Post(ph).Success)
                {

                }
            }
        }

        public virtual void SendClosed()
        {
            NetPacket ph = new NetPacket(PacketMainCmd.System, PacketSystemSubCmd.Close);
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(this.Client.Name);
                br.Write(this.Client.ID);
                ph.PacketContents = br.GetData();
            }
            this.Client.Post(ph);
            return;
        }
    }
}