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

    public class ServerExtendKernal : ServerExtendBase
    {
        public ServerExtendKernal(Base.NetServer server) : base(server)
        {

        }
        public override void DoExtendCommand(NetPacket ph, ReciveEventArgs e)
        {
            if (ph.PacketMainCommand == PacketMainCmd.System)
            {
                switch (ph.PacketSubcommand)
                {
                    case PacketSystemSubCmd.RegeditSession:
                        DoCommandRegeditSesstion(e, ph);
                        break;
                    case PacketSystemSubCmd.GetServer:
                        DoCommandGetServer(e, ph);
                        break;
                    case PacketSystemSubCmd.Heartbeat:
                        OnHeartbeat(ph, e);
                        break;
                    case PacketSystemSubCmd.ChangedServer:
                        OnChangedServer(ph, e);
                        break;
                    case PacketSystemSubCmd.ServerTime:
                        DoCommandServerTime(e, ph);
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnChangedServer(NetPacket ph, ReciveEventArgs e)
        {
            ph.PacketContents = new byte[] { };
            ph.PacketMode = PacketMode.ChangServer;
            e.ClientProxy.Send(ph);
        }
 
        public void OnHeartbeat(NetPacket ph, ReciveEventArgs e)
        {
            ph.PacketContents = new byte[] { };
            e.ClientProxy.Send(ph);
        }
        public void OnServerTime(NetPacket ph, ReciveEventArgs e)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(DateTime.Now);
                ph.PacketContents = bw.GetData();
            }
            e.ClientProxy.Send(ph);
        }
        public virtual void DoCommandServerTime(ReciveEventArgs e, NetPacket ph)
        { 
            ph.BeginWriter();
            ph.Writer.Write(1, DateTime.Now);
            ph.EndWriter();
            e.ClientProxy.Respond(ph);
        }
        public delegate void VersionCheckedEventHandler(object sender, VersionCheckedEventArgs e);
        public event VersionCheckedEventHandler VersionChecked;
        public virtual void OnVersionChecked(VersionCheckedEventArgs e)
        {
            if (VersionChecked != null)
            {
                VersionChecked(this, e);
            }
        }
        public class VersionCheckedEventArgs : CancelEventArgs
        {
            public ServerExtendKernal Extend;
            private string _version = string.Empty;
            public string Version { get { return _version; } }
            public int Upgrade;
            private byte[] _data;
            public byte[] Data
            {
                get
                {
                    if (_data == null)
                        _data = new byte[0];
                    return _data;
                    ;
                }
                set { _data = value; }
            }
            public string downloadurl = string.Empty;
            public NetPacket Packet { get; set; }
            public VersionCheckedEventArgs(string version, int upgrade, ServerExtendKernal extend, NetPacket ph)
            {
                _version = version;
                Upgrade = upgrade;
                Extend = extend;
                Packet = ph;
            }

            public VersionCheckedEventArgs(string version, int upgrade, int session, byte[] data, ServerExtendKernal extend, NetPacket ph)
            {
                _version = version;
                Upgrade = upgrade;
                Extend = extend;
                this.Data = data;
                Packet = ph;
            }

        }
        public virtual void CheckVersion(NetPacket ph, ReciveEventArgs e)
        {
            using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
            {

                string version = br.ReadString();
                int upgrade = br.ReadInt32();
                byte[] data = ph.PacketContents;
                VersionCheckedEventArgs rse = new VersionCheckedEventArgs(version, upgrade, e.ClientProxy.Session, data, this, ph);
                this.OnVersionChecked(rse);
                if (rse.Cancel)
                {
                    return;
                } 

                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(rse.downloadurl);
                    bw.Write(rse.Upgrade);
                    bw.Write(rse.Data);
                    ph.PacketContents = bw.GetData();
                }
                e.ClientProxy.Respond(ph);
            }
        }
 
        public delegate void RegeditSessionEventHandler(object sender, RegeditSessionEventArgs e);
        public event RegeditSessionEventHandler RegeditSession;
        public virtual void DoCommandRegeditSesstion(ReciveEventArgs ee,NetPacket ph)
        {
            ph.BeginRead();
            string user = ph.Reader.ReadString();
            string password = ph.Reader.ReadString();
            byte[] data = ph.Reader.ReadBuffer();
            RegeditSessionEventArgs e = new RegeditSessionEventArgs(user, password, data, ee.ClientProxy);
            ph.EndRead();
            OnRegeditSession(e);
            if (e.Cancel)
                return;
            ph.BeginWriter();
            ph.Writer.Write(1, e.Session);
            ph.Writer.Write(2, e.ID);
            ph.EndWriter();
            ee.ClientProxy.ID = e.ID;
            ee.ClientProxy.Session = e.Session;
            ee.ClientProxy.Respond(ph);
        }
        public virtual void DoCommandGetServer(ReciveEventArgs e,NetPacket ph)
        {
            ph.BeginRead();
            string url = ph.Reader.ReadString(); 
            ph.EndRead();
            GetServerEventArgs ee = new GetServerEventArgs(e, url);
            OnGetServer(ee);
            if (ee.Cancel)
                return;
            ph.BeginWriter();
            ph.Writer.Write(1, ee.IP);
            ph.Writer.Write(2, ee.Port);
            ph.EndWriter(); 
            e.ClientProxy.Respond(ph);
        }
        public virtual void OnRegeditSession(RegeditSessionEventArgs e)
        {
            if (RegeditSession != null)
            {
                RegeditSession(this, e);
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
