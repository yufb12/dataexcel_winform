using Feng.Net.Base;
using Feng.Net.NetArgs;
using Feng.Net.Packets;
using System;
using System.Collections.Generic;
namespace Feng.Net.Extend
{
    public class ClientExtendUser : ClientExtendBase, IDisposable
    {
        public ClientExtendUser()
        {

        }
        private UserClientCollection _userlist = new UserClientCollection();
        public override void Bingding(ClientProxyBase client)
        {
            client.DataReceive -= client_DataReceived;
            client.DataReceive += client_DataReceived;
            base.Bingding(client);
        }

        public void Dispose()
        {
            this.Client.DataReceive -= client_DataReceived;
        }

        void client_DataReceived(object sender, ReciveEventArgs e, NetPacket ph)
        {
            switch (ph.PacketSubcommand)
            {
                case PacketSystemSubCmd.UserChanged:
                    OnClientChanged(ph);
                    break;
                case PacketSystemSubCmd.Heartbeat:
                    break;
                case PacketSystemSubCmd.OtherLogin:
                    OnOtherLogin(ph);
                    break;
                default:
                    break;
            }
        }

        public delegate void ClientChangedEventHandler(object sender, string uc, ClientChangedMode mode);
        public delegate void RecviceUserDataEventHandler(object sender, NetPacket ph, string uc, byte[] data);
        public delegate void OtherLoginEeventHandler(object sender, NetPacket ph);
        public delegate void InitOnLineUsersEventHandler(object sender, UserClientCollection uclist);
        public event ClientChangedEventHandler ClientChanged;
        public event RecviceUserDataEventHandler RecviceUserData;
        public event OtherLoginEeventHandler OtherLogin;
        public event InitOnLineUsersEventHandler InitOnLineUsers;

        protected object newclientobj = new object();

        private void OnRecviceUserData(NetPacket ph)
        {
            byte[] data = ph.PacketContents;
            if (RecviceUserData != null)
            {
                RecviceUserData(this, ph, this.Client.Name, data);
            }
        }
        public virtual void SendToOtherUser(string user, byte[] data)
        {

        }
        public virtual void SendToOtherUser(string user, string text)
        {
            List<string> users = new List<string>();
            users.Add(user);
            SendToOtherUser(users, text);
        }

        public virtual void SendToOtherUser(string user, short maincommand, int subcommand, byte[] data)
        {
            List<string> list = new List<string>();
            list.Add(user);
            SendToOtherUser(list, maincommand, subcommand, data);
        }

        public virtual void SendToOtherUser(List<string> users, string text)
        {
            byte[] data = Feng.IO.BitConver.GetBytes(text);
            SendToOtherUser(users, PacketMainCmd.System, PacketSystemSubCmd.Text, data);
        }

        public virtual void SendToOtherUser(List<string> users, short maincommand, int subcommand, byte[] data)
        {

            NetPacket ph = new NetPacket();
            ph.PacketMainCommand = maincommand;
            ph.PacketSubcommand = subcommand;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(this.Client.Name);
                br.Write(users.Count);
                foreach (string str in users)
                {
                    br.Write(str);
                }
                if (data == null)
                {
                    br.Write(new byte[] { });
                }
                else
                {
                    br.Write(data);
                }
                ph.PacketContents = br.GetData();
            }
            this.Client.Send(ph);
        }

        public virtual NetResult GetOnlineUser()
        {
            NetPacket indata = new NetPacket(PacketMainCmd.System, PacketSystemSubCmd.GetOnlineUser, new byte[0]);

            NetResult fengresult = this.Client.Send(indata);
            if (fengresult.Success)
            {
                NetPacket ph = NetPacket.Get(fengresult.OrgValue);
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    int len = br.ReadInt32();
                    this._userlist.Clear();
                    for (int i = 0; i < len; i++)
                    {
                        string user = br.ReadString();
                        if (ClientChanged != null)
                        {
                            this._userlist.Add(user);
                        }
                    }

                    if (InitOnLineUsers != null)
                    {
                        InitOnLineUsers(this, this._userlist);
                    }
                }
            }
            return fengresult;
        }

        public virtual void SendToAllUser(string text)
        {
            byte[] data = Feng.IO.BitConver.GetBytes(text);
            SendToAllUser(PacketMainCmd.System, PacketSystemSubCmd.Text, data);
        }
        public virtual void SendToAllUser(NetPacket ph)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(this.Client.Name);
                bw.Write(this.Client.ID);
                bw.Write(ph.PacketContents);
                ph.PacketContents = bw.GetData();
            }
            this.Client.Send(ph);
        }
        public virtual void SendToAllUser(short maincommand, int subcommand, byte[] data)
        {
            NetPacket ph = new NetPacket();
            ph.PacketMainCommand = maincommand;
            ph.PacketSubcommand = subcommand;

            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                if (data == null)
                {
                    br.Write(new byte[] { });
                }
                else
                {
                    br.Write(data);
                }
                ph.PacketContents = br.GetData();
            }
            SendToAllUser(ph);
        }

        public virtual void SendToAllClientFile(string filename)
        {
            byte[] buffer = null;
            int bufferlen = 1024 * 1024 * 4;
            DateTime dt = DateTime.Now;
            bool isbegin = true;
            bool iscomplete = false;
            int index = 1;
            using (System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.Open))
            {
                buffer = new byte[bufferlen];
                int res = fs.Read(buffer, 0, buffer.Length);
                int start = 0;
                while (res > 0)
                {
                    NetPacket ph = new NetPacket(PacketMainCmd.File, (int)PacketFileSubCmd.SendFile);
                    ph.PacketMode = (byte)PacketMode.POST;
                    using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
                    {
                        br.Write(this.Client.Name);
                        br.Write(string.Empty);
                        br.WriteInt(0);
                        br.WriteInt(0);
                        br.Write(filename);
                        br.Write(fs.Length);
                        br.Write(isbegin);
                        if (start + res == fs.Length)
                        {
                            iscomplete = true;
                        }
                        br.Write(iscomplete);
                        br.Write(index);
                        br.Write(start);
                        br.Write(dt);
                        br.Write(buffer, res);
                        ph.PacketContents = br.GetData();
                    }
                    start += res;
                    index++;
                    this.Client.Send(ph);
                    buffer = new byte[bufferlen];
                    res = fs.Read(buffer, 0, buffer.Length);

                }
            }
        }

        private void OnToAllUser(NetPacket ph)
        {
            lock (newclientobj)
            {
                int ccm = ph.PacketSubcommand;
                switch (ccm)
                {
                    default:
                        OnRecviceUserData(ph);
                        break;
                }
            }

        }

        private void OnToOtherUser(NetPacket ph)
        {
            lock (newclientobj)
            {
                int ccm = ph.PacketSubcommand;
                switch (ccm)
                {
                    default:
                        OnRecviceUserData(ph);
                        break;
                }
            }

        }

        private void OnClientChanged(NetPacket ph)
        {
            lock (newclientobj)
            {
                ClientChangedMode ccm = (ClientChangedMode)ph.PacketSubcommand;

                switch (ccm)
                {
                    case ClientChangedMode.Add:
                        using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                        {
                            br.ReadInt32();
                            string user = br.ReadString();
                            if (ClientChanged != null)
                            {

                                ClientChanged(this, user, ccm);
                                this._userlist.Add(user);
                            }
                        }
                        break;
                    case ClientChangedMode.Remove:
                        using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                        {
                            br.ReadInt32();
                            string user = br.ReadString();
                            if (ClientChanged != null)
                            {
                                ClientChanged(this, this._userlist[user], ccm);
                            }
                            this._userlist.Remove(this._userlist[user]);
                        }
                        break;
                    default:
                        break;

                }

            }

        }

        public void OnOtherLogin(NetPacket ph)
        {
            if (OtherLogin != null)
            {
                OtherLogin(this, ph);
            }
        }


        public virtual NetResult OnLineChecked(out int count, byte[] data)
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(data);
                NetPacket ph = new NetPacket(PacketMainCmd.System,
                    PacketSystemSubCmd.CheckOnLine, bw.GetData());
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

        public virtual ResultString ModifyPassword(string user, string originalPassword, string password)
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(user);
                bw.Write(originalPassword);
                bw.Write(password);
                NetPacket ph = new NetPacket(PacketMainCmd.System,
                 PacketSystemSubCmd.ModifyPassword, bw.GetData());
                ph.PacketMode = (byte)PacketMode.Send;
                NetResult fengresult = this.Client.Send(ph, out ph);
                if (fengresult.Success)
                {
                    return ResultString.ReadResult(ph);
                }
            }
            return ResultString.NetSendError;
        }

    }
}