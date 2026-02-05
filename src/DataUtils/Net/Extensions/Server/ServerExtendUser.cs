using Feng.Net.Base;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
using Feng.Net.Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Feng.Net.Extend
{

    public class ServerExtendUser : ServerExtendBase
    {
        public ServerExtendUser(Base.NetServer server) : base(server)
        {

        }
        private object USERLISTLOCK = new object();

        public object GROUPLISTLOCK = new object();
        public delegate void ToAllUserEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public delegate void ToOtherUserEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public delegate void GetOnlineUserEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public delegate void ReceiveUserDataEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public delegate void RoomEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public delegate void ToServerEventHandler(object sender, NetPacket ph, ReciveEventArgs e);

        public virtual void OnGetOnlineUser(NetPacket ph, ReciveEventArgs e)
        {
            if (E_GetOnlineUser != null)
            {
                E_GetOnlineUser(this, ph, e);
            }
        }

        public virtual void OnToOtherUser(NetPacket ph, ReciveEventArgs e)
        {
            if (E_ToOtherUser != null)
            {
                E_ToOtherUser(this, ph, e);
            }
        }
        public virtual void OnToAllUser(NetPacket ph, ReciveEventArgs e)
        {
            if (E_ToAllUser != null)
            {
                E_ToAllUser(this, ph, e);
            }
        }
        public delegate void OtherLoginEventHandler(object sender, OtherLoginEventArgs e);
        public event OtherLoginEventHandler OtherLogin;
        public virtual void OnRoom(NetPacket ph, ReciveEventArgs e)
        {
            if (E_Room != null)
            {
                E_Room(this, ph, e);
            }
        }
        public event ToOtherUserEventHandler E_ToOtherUser;
        public event GetOnlineUserEventHandler E_GetOnlineUser;
        public event ToAllUserEventHandler E_ToAllUser;
        public event ToServerEventHandler E_ToServer;
        public event RoomEventHandler E_Room;
        public class ModifyPasswordEventArgs : CancelEventArgs
        {
            private string _user = string.Empty;
            public string User { get { return _user; } }
            public string Password;
            public string OriginalPassword;
            public ServerExtendUser Extend;
            public ModifyPasswordEventArgs(string user, string originalPassword, string password, NetPacket packet, ServerExtendUser extend)
            {
                _user = user;
                Password = password;
                OriginalPassword = originalPassword;
                this.Extend = extend;
                this.Packet = packet;
            }

            public NetPacket Packet { get; set; }
        }
        public delegate void ModifyPasswordEventHandler(object sender, ModifyPasswordEventArgs e);
        public event ModifyPasswordEventHandler ModifyPassword;
        public virtual void OnModifyPassword(ModifyPasswordEventArgs e)
        {
            if (ModifyPassword != null)
            {
                ModifyPassword(this, e);
            }
        }
        public virtual void OnOtherLogin(OtherLoginEventArgs e)
        {
            if (OtherLogin != null)
            {
                OtherLogin(this, e);
            }
        }
        public override void DoExtendCommand(NetPacket ph, ReciveEventArgs e)
        {
            this.OnReceiveUserData(ph, e);
            if (e.Cancel)
            {
                return;
            }
            int uc = ph.PacketSubcommand;
            switch (uc)
            {
                case PacketFileSubCmd.SendFile:
                    throw new Exception("SendFile");
                    break;
                case PacketFileSubCmd.CreatePath:
                    //CreatePath(ph, e);
                    break;
                default:
                    break;
            }
        }
        private List<string> _rooms;
        public List<string> Rooms
        {
            get
            {
                if (_rooms == null)
                {
                    _rooms = new List<string>();
                }
                return _rooms;
            }
        }
        public virtual void OnComeInRoom(NetPacket ph, ReciveEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }
            string roomname = string.Empty;
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(ph.PacketContents))
            {
                roomname = reader.ReadString();
            }

            AddGroupName(roomname, e.ClientProxy);
        }

        public virtual void OnComeOutRoom(NetPacket ph, ReciveEventArgs e)
        {
            string roomname = string.Empty;
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(ph.PacketContents))
            {
                roomname = reader.ReadString();
            }

            RemoveGroupName(roomname, e.ClientProxy);
        }
        public delegate void CheckOnLineEventHandler(object sender, OnLineCheckedEventArgs sh);
        public event CheckOnLineEventHandler CheckOnLine;
        public void OnCheckOnLine(OnLineCheckedEventArgs sh)
        {
            if (CheckOnLine != null)
            {
                CheckOnLine(this, sh);
            }
        }
        public virtual void ToAllUser(NetPacket ph, ReciveEventArgs e)
        {

            this.OnToAllUser(ph, e);
            if (e.Cancel)
            {
                return;
            }

            SendToAllClient(ph);
        }
        public class OnLineCheckedEventArgs : CancelEventArgs
        {
            public ServerExtendUser Extend;
            public int Count { get; set; }
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
            public OnLineCheckedEventArgs(byte[] data, ServerExtendUser extend)
            {
                Extend = extend;
                Data = data;
            }
        }
        //public virtual void CheckOnLine(PacketHelper ph, ReciveEventArgs e)
        //{
        //    using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
        //    {
        //        byte[] data = ph.PacketContents;
        //        OnLineCheckedEventArgs rse = new OnLineCheckedEventArgs(data, this);
        //        this.OnCheckOnLine(rse);
        //        if (rse.Cancel)
        //        {
        //            return;
        //        }
        //        ph.PacketMode = (byte)PacketMode.Return;
        //        using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
        //        {
        //            bw.Write(rse.Count);
        //            bw.Write(rse.Data);
        //            ph.PacketContents = bw.GetData();
        //        }
        //        e.ClientProxy.Send(ph);
        //    }
        //}
        public virtual void DoToAllUser(ReciveEventArgs e, NetPacket ph)
        {
            string name = string.Empty;
            string id = string.Empty;
            byte[] data = null;
            using (Feng.IO.BufferReader br = new IO.BufferReader(ph.PacketContents))
            {
                name = br.ReadString();
                id = br.ReadString();
                data = br.ReadBytes();
                ph.PacketContents = data;
            }
            this.SendToAllClient(ph);
        }
        public virtual void DoToOtherUser(ReciveEventArgs e, NetPacket ph)
        {

        }
        public virtual void DoToGroup(ReciveEventArgs e, NetPacket ph)
        {

        }
        public virtual void ToOtherUser(NetPacket ph, ReciveEventArgs e)
        {
            this.OnToOtherUser(ph, e);
            if (e.Cancel)
            {
                return;
            }
            using (Feng.IO.BufferReader bw = new Feng.IO.BufferReader(ph.PacketContents))
            {
                string Name = bw.ReadString();
                int len = bw.ReadInt32();
                List<string> listsqm = new List<string>();
                for (int i = 0; i < len; i++)
                {
                    listsqm.Add(bw.ReadString());
                }
                byte[] data = bw.ReadBytes();
                ph.PacketContents = data;
                this.SendToClient(listsqm, ph);
            }
        }

        public virtual void GetOnlineUser(NetPacket ph, ReciveEventArgs e)
        {
            this.OnGetOnlineUser(ph, e);
            if (e.Cancel)
            {
                return;
            }
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(this.Server.Clients.Count);
                for (int i = this.Server.Clients.Count - 1; i >= 0; i--)
                {
                    IClientProxy soc = this.Server.Clients[i];
                    br.Write(soc.Name);
                }
                ph.PacketContents = br.GetData();
                ph.PacketMode = 3;
                e.ClientProxy.Send(ph);
            }
        }
        public virtual void OnModifyPassword(NetPacket ph, ReciveEventArgs e)
        {
            using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
            {
                string user = br.ReadString();
                string originalPassword = br.ReadString();
                string password = br.ReadString();
                ModifyPasswordEventArgs rse = new ModifyPasswordEventArgs(user, originalPassword, password, ph, this);
                this.OnModifyPassword(rse);
            }
        }

        public virtual void Add(IClientProxy soc, ReciveEventArgs e)
        {
            lock (USERLISTLOCK)
            {
                IClientProxy shp = soc;
                if (shp != null)
                {
                    OtherLoginEventArgs ole = new OtherLoginEventArgs(soc);
                    OnOtherLogin(ole);
                    if (ole.Cancel)
                    {
                        NetPacket ph = new NetPacket();
                        ph.PacketMainCommand = PacketMainCmd.System;
                        ph.PacketSubcommand = PacketSystemSubCmd.OtherLogin;
                        using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                        {
                            bw.Write(1, soc.Name);
                            bw.Write(2, soc.Session);
                            bw.Write(3, e.ClientProxy.ID);
                            ph.PacketContents = bw.GetData();
                        }
                        shp.Send(ph);
                    }
                }
            }
        }
        public virtual void Remove(IClientProxy soc)
        {
            lock (USERLISTLOCK)
            {
                RemoveGroupName(soc);
            }
        }
        private Dictionary<string, List<IClientProxy>> _groups;

        public Dictionary<string, List<IClientProxy>> Groups
        {
            get
            {
                if (_groups == null)
                {
                    _groups = new Dictionary<string, List<IClientProxy>>();
                }
                return _groups;
            }
            set
            {
                _groups = value;
            }
        }

        public virtual void AddGroupName(string roomname, IClientProxy sh)
        {
            lock (GROUPLISTLOCK)
            {
                if (this.Groups.ContainsKey(roomname))
                {
                    List<IClientProxy> list = this.Groups[roomname];
                    if (!list.Contains(sh))
                    {
                        list.Add(sh);
                    }
                }
                else
                {
                    List<IClientProxy> list = new List<IClientProxy>();
                    list.Add(sh);
                    this.Groups.Add(roomname, list);
                }
                Rooms.Add(roomname);
            }
        }

        public virtual void RemoveGroupName(string roomname, IClientProxy sh)
        {
            lock (GROUPLISTLOCK)
            {

                if (this.Groups.ContainsKey(roomname))
                {
                    List<IClientProxy> list = this.Groups[roomname];
                    if (list.Contains(sh))
                    {
                        list.Remove(sh);
                    }
                }
                Rooms.Remove(roomname);
            }
        }

        public virtual void RemoveGroupName(IClientProxy sh)
        {
            lock (GROUPLISTLOCK)
            {
                for (int i = Rooms.Count - 1; i >= 0; i--)
                {
                    string roomname = Rooms[i];
                    if (this.Groups.ContainsKey(roomname))
                    {
                        List<IClientProxy> list = this.Groups[roomname];
                        if (!list.Contains(sh))
                        {
                            list.Remove(sh);
                        }
                    }
                }
                Rooms.Clear();
            }
        }
        public virtual void SendToAllClient(NetPacket ph)
        {
            lock (USERLISTLOCK)
            {
                for (int i = this.Server.Clients.Count - 1; i >= 0; i--)
                {
                    IClientProxy sh = this.Server.Clients[i];
                    if (!sh.Send(ph).Success)
                    {
                        sh.Close();
                    }
                }
            }
        }

        public virtual void SendToGroup(NetPacket ph, string group)
        {
            lock (USERLISTLOCK)
            {
                if (this.Groups.ContainsKey(group))
                {
                    List<IClientProxy> list = this.Groups[group];
                    int count = list.Count;
                    for (int i = count - 1; i >= 0; i--)
                    {
                        IClientProxy sh = list[i];
                        sh.Send(ph);
                    }
                }
            }
        }
#warning lock error
        public virtual void SendToAllOther(NetPacket ph, IClientProxy client)
        {
            lock (USERLISTLOCK)
            {
                for (int i = this.Server.Clients.Count - 1; i >= 0; i--)
                {
                    IClientProxy sh = this.Server.Clients[i];
                    if (client != sh)
                    {
                        sh.Send(ph);
                    }
                }
            }
        }

        public virtual void SendToOther(NetPacket ph, IClientProxy client, List<string> users)
        {
            lock (USERLISTLOCK)
            {
                for (int i = this.Server.Clients.Count - 1; i >= 0; i--)
                {
                    IClientProxy sh = this.Server.Clients[i];
                    if (users.Contains(sh.Name))
                    {
                        if (client != sh)
                        {
                            sh.Send(ph);
                        }
                    }
                }
            }
        }

        public virtual void SendToAllOther(short maincommand, int subcommand, byte[] data, IClientProxy client)
        {
            NetPacket ph = new NetPacket(maincommand, subcommand, data);
            ph.PacketMode = (byte)PacketMode.POST;
            SendToAllOther(ph, client);
        }

        public virtual void SendToAllOther(string data, IClientProxy client)
        {
            SendToAllOther(PacketMainCmd.System, PacketSystemSubCmd.Text, Feng.IO.BitConver.GetBytes(data), client);
        }

        public virtual void SendToAllOtherFile(string filename, IClientProxy client)
        {
            byte[] buffer = null;
            using (System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.Open))
            {
                buffer = new byte[1024 * 1024 * 4];
                int res = fs.Read(buffer, 0, buffer.Length);
                int start = 0;
                while (res > 0)
                {
                    NetPacket ph = new NetPacket(PacketMainCmd.File, PacketFileSubCmd.SendFile);
                    ph.PacketMode = (byte)PacketMode.POST;
                    using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
                    {
                        br.Write(string.Empty);
                        br.Write(string.Empty);
                        br.WriteInt(0);
                        br.Write(filename);
                        br.Write(start);
                        br.Write(buffer, res);
                        ph.PacketContents = br.GetData();
                    }
                    start += res;
                    SendToAllOther(ph, client);
                    buffer = new byte[1024 * 1024 * 4];
                    res = fs.Read(buffer, 0, buffer.Length);

                }
            }
        }

        public virtual void SendToAllClient(short maincommand, int subcommand, byte[] data)
        {
            NetPacket ph = new NetPacket(maincommand, subcommand, data);
            ph.PacketMode = (byte)PacketMode.POST;
            SendToAllClient(ph);
        }

        public virtual void SendToAllClient(string data)
        {
            SendToAllClient(PacketMainCmd.System, PacketSystemSubCmd.Text, Feng.IO.BitConver.GetBytes(data));
        }

        public virtual void SendToAllClientFile(string filename)
        {
            byte[] buffer = null;
            using (System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.Open))
            {
                buffer = new byte[1024 * 1024 * 4];
                int res = fs.Read(buffer, 0, buffer.Length);
                int start = 0;
                while (res > 0)
                {
                    NetPacket ph = new NetPacket(PacketMainCmd.File, (int)PacketFileSubCmd.SendFile);
                    ph.PacketMode = (byte)PacketMode.POST;
                    using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
                    {
                        br.Write(string.Empty);
                        br.Write(string.Empty);
                        br.WriteInt(0);
                        br.Write(filename);
                        br.Write(start);
                        br.Write(buffer, res);
                        ph.PacketContents = br.GetData();
                    }
                    start += res;
                    SendToAllClient(ph);
                    buffer = new byte[1024 * 1024 * 4];
                    res = fs.Read(buffer, 0, buffer.Length);

                }
            }
        }

        public virtual void SendToClient(List<string> userlist, NetPacket ph)
        {
            lock (USERLISTLOCK)
            {
                for (int i = this.Server.Clients.Count - 1; i >= 0; i--)
                {
                    IClientProxy sh = this.Server.Clients[i];
                    if (userlist.Contains(sh.Name))
                    {
                        sh.Send(ph);
                    }
                }
            }
        }

        public virtual void SendToClient(List<string> userlist, short maincommand, int subcommand, byte[] data)
        {
            NetPacket ph = new NetPacket();
            ph.PacketMainCommand = maincommand;
            ph.PacketSubcommand = subcommand;
            ph.PacketContents = data;
            SendToClient(userlist, ph);
        }

        public virtual void SendToClient(List<string> userlist, string text)
        {
            NetPacket ph = new NetPacket();
            ph.PacketMainCommand = PacketMainCmd.System;
            ph.PacketSubcommand = PacketSystemSubCmd.Text;
            ph.PacketContents = Feng.IO.BitConver.GetBytes(text);
            SendToClient(userlist, ph);
        }

        public virtual void SendToClientFile(string uc, List<string> userlist, string filename)
        {
            byte[] buffer = null;
            using (System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.Open))
            {
                buffer = new byte[1024 * 1024 * 4];
                int res = fs.Read(buffer, 0, buffer.Length);
                int start = 0;
                while (res > 0)
                {

                    NetPacket ph = new NetPacket(PacketMainCmd.File, (int)PacketFileSubCmd.SendFile);
                    ph.PacketMode = (byte)PacketMode.POST;
                    using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
                    {
                        br.Write(uc);
                        br.Write(filename);
                        br.Write(start);
                        br.Write(buffer, res);
                        ph.PacketContents = br.GetData();
                    }
                    start += res;
                    SendToClient(userlist, ph);
                    buffer = new byte[1024 * 1024 * 4];
                    res = fs.Read(buffer, 0, buffer.Length);

                }
            }
        }

        public virtual void SendClientChanged(ClientChangedMode mode, IClientProxy soc)
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write((int)mode);
                bw.Write(soc.Name);

                NetPacket ph = new NetPacket(PacketMainCmd.System,
        PacketSystemSubCmd.UserChanged, bw.GetData());
                ph.PacketMode = (byte)PacketMode.POST;
                SendToAllOther(ph, soc);
            }
        }

        public event ReceiveUserDataEventHandler ReceiveUserData;
        public virtual void OnReceiveUserData(NetPacket ph, ReciveEventArgs e)
        {
            if (ReceiveUserData != null)
            {
                ReceiveUserData(this, ph, e);
            }
        }

    }

}
