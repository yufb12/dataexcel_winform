 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.ComponentModel;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;
using Feng.Net.EventHandlers;
using Feng.Net.Packets;
using Feng.Net.NetArgs;
using Feng.Net.Interfaces;

namespace Feng.Net.Base
{
    public abstract class ClientProxyBase : IClientProxy, IDisposable
    {
        private int _session = 0;
        public virtual int Session
        {
            get { return _session; }
            set
            {
                _session = value;
            }
        }

        private string _sessionkey = string.Empty;
        public virtual string SessionKey
        {
            get { return _sessionkey; }
            set
            {
                _sessionkey = value;
            }
        }

        private string _name = string.Empty;
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _id = string.Empty;
        public virtual string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string LocalIP
        {
            get;
            set;
        }
        public virtual int LoalPort { get; set; }

        public abstract void Open();
        public abstract void Close(); 

        public virtual NetResult Respond(NetPacket ph)
        {
            ph.PacketMode = PacketMode.ANSWER;
            return SendToRemote(ph);
        }

        public virtual NetResult Post(NetPacket packet)
        {
            packet.PacketMode = PacketMode.POST;
            return SendToRemote(packet);
        }

        public virtual NetResult Send(NetPacket packet, out NetPacket outvalue)
        {
            string error = string.Empty;
            outvalue = null;
            NetResult result = Send(packet);
            if (result.Success)
            {
                outvalue = NetPacket.Get(result.OrgValue);
            }
            return result;
        }

        public virtual NetResult Send(NetPacket ph)
        { 
            NetResult result = null;
            if (!this.HasConnected)
            {
                return NetResult.ErrorResult("服务器连接失败，稍候再试", NetResult.CODE_SYSTEM);
            } 
            ph.PacketMode = PacketMode.Send;
            result = SendAndWaitingValue(ph);
            if (result.Success)
            {
                NetPacket phres = NetPacket.Get(result.OrgValue);
                if (phres != null)
                {
                    if ((phres.PacketMode & PacketMode.ERROR) == PacketMode.ERROR)
                    {
                        using (Feng.IO.BufferReader reader = new IO.BufferReader(phres.PacketContents))
                        {
                            string msg = reader.ReadIndex(1, string.Empty);
                            string trace = reader.ReadIndex(2, string.Empty);
                            result.SetError(msg, trace, NetResult.CODE_USER);

                        }
                    }
                    else if (phres.PacketMode == PacketMode.ChangServer)
                    {
                        phres.BeginRead();
                        string ip = ph.Reader.ReadIndex(1, string.Empty);
                        int port = ph.Reader.ReadIndex(2, 0);
                        phres.EndRead();
                        ChangServer(phres,ip,port);
                        return Send(ph);
                    }
                    result.Packet = phres;
                }
            }
            return result;
        }

        public virtual bool Send(byte[] data)
        {
            return false;
        }

        public abstract void ChangServer(NetPacket ph,string ip,int port);

        public abstract NetResult SendToRemote(NetPacket packet);

        public readonly object readonlyindex = new object();
        public virtual ushort Index
        {
            get { return _index; }
            set { _index = value; }
        }
        private ushort _index = 0;

        private Dictionary<int, IndexObject> sendlist = new Dictionary<int, IndexObject>();
        protected virtual NetResult SendAndWaitingValue(NetPacket packet)
        {
            NetResult fengresult = null;
            try
            { 

                IndexObject indexobjec = new IndexObject();
                lock (readonlyindex)
                {
                    Index++;
                    ushort uindex = Index;
                    indexobjec.SendTime = DateTime.Now;
                    indexobjec.Index = uindex;
                    sendlist.Add(uindex, indexobjec);
                    packet.Packetindex = uindex;
                }  
                fengresult = SendToRemote(packet);
                if (fengresult.Success)
                {
           
                    indexobjec.AutoResetEvent.Reset(); 
                    indexobjec.AutoResetEvent.WaitOne(packet.WaitTime * 1000, false);
                }

                lock (readonlyindex)
                {
                    if (indexobjec.IsReturn)
                    {
                        fengresult.Success = true;
                        fengresult.OrgValue = indexobjec.Value;
                    }
                    else
                    {
                        fengresult.Success = false;
                        fengresult.Code = NetResult.CODE_SYSTEM;
                        fengresult.Message = "client is not time out";
                    }
                    sendlist.Remove(indexobjec.Index);
                }
            }
            catch (Exception ex)
            {
                if (fengresult == null)
                {
                    fengresult = new NetResult();
                }
                fengresult.Success = false;
                fengresult.Code = NetResult.CODE_SYSTEM;
                fengresult.Message = ex.Message;
            }
            return fengresult;
        }
 
        public abstract DateTime ConnectionTime { get; }

        private DateTime _lastrecvdatatime = DateTime.MinValue;
        public virtual DateTime LastRecvDataTime
        {
            get { return _lastrecvdatatime; }
        }

        public abstract bool HasConnected { get; set; }
 
        public virtual void ReceiveData(ReciveEventArgs recvdata)
        {
            OnDataReceive(recvdata);
        }

        public virtual byte[] GetSendData(byte[] data)
        {
            List<byte> list = new List<byte>();
            list.AddRange(BitConverter.GetBytes(data.Length));
            list.AddRange(data);
            return list.ToArray();
        }

        public virtual void Connection()
        {

        }

        public abstract void Read();

        public virtual event DataReceiveEventHandler DataReceive;

        public virtual void OnDataReceive(ReciveEventArgs e, NetPacket ph)
        {
            if (DataReceive != null)
            {
                DataReceive(this, e, ph);
            }
        }

        public virtual string RemoteIP
        {
            get;
            set;
        }

        public virtual int RemotePort
        {
            get;
            set;
        }

        private int state = 0;
        public virtual int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        private long _SendTimes = 0;
        public long SendTimes
        {
            get
            {
                return _SendTimes;
            } 
        }

        private long _RecvTimes = 0;
        public long RecvTimes
        {
            get
            {
                return _RecvTimes;
            }
        }

        private object tag = null;
        public virtual object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public void SendAdd()
        {
            _SendTimes++;
        }

        public void RecvAdd()
        {
            _RecvTimes++;
        }
 
        public abstract void Dispose();


        public event BeforeSendDataEventHandler BeforeSendData;
        public virtual void OnBeforeSendData(byte[] data)
        {
            if (BeforeSendData != null)
            {
                BeforeSendData(this, data);
            }
        }

        public event BeforeDataReceiveEventHandler BeforeDataReceive;
        public virtual void OnDataReceive(ReciveEventArgs e)
        {
            //Feng.Utils.TraceHelper.WriteTrace("ClientProxyBase", "OnDataReceive", "ClientProxyBase", "ClientProxyBase", this);
 

            if (BeforeDataReceive != null)
            {
                BeforeDataReceive(this, e);
                if (e.Cancel)
                {
                    return;
                }
            }
            NetPacket ph = NetPacket.Get(e.Data);
            if (ph != null)
            {
#if DEBUG2
                Feng.Utils.TraceHelper.WriteTrace("CLIENT", "接收数据", this.ID, true, "Packetindex:" + ph.Packetindex + " MainCommand:" + ph.PacketMainCommand + " Subcommand:" + Feng.Utils.ConvertHelper.ToString(ph.PacketSubcommand, 16));
#endif
                int strindex = ph.Packetindex;
                if ((ph.PacketMode & PacketMode.ANSWER) == PacketMode.ANSWER)
                {
                    lock (readonlyindex)
                    {
                        if (sendlist.ContainsKey(strindex))
                        {
                            sendlist[strindex].IsReturn = true;
                            sendlist[strindex].Value = e.Data;
                            sendlist[strindex].AutoResetEvent.Set();
                        }
                    }
                    return;
                }

                if (DataReceive != null)
                {
                    DataReceive(this, e, ph);
                }
            }
        }
    }

     
}
