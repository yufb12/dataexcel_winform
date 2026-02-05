//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading;
//using System.Text;
//using System.Collections.Generic;

//using System.Net.NetworkInformation;
//using Feng.Utils;
//using Feng.Net.NetArgs;
//using Feng.Net.Tcp;

//namespace Feng.Net
//{
//    public abstract partial class Client : ClientProxyBase, IDisposable
//    {  

//        public Client()
//        {
//        }
 
//        private bool _hasconnected = false;
//        public virtual bool HasConnected
//        {
//            get
//            {
//                return _hasconnected;
//            }
//            set
//            {
//                _hasconnected = value;
//            }
//        }

//        private DateTime _conectiontime = DateTime.MaxValue;
//        public virtual DateTime ConnectionTime
//        {
//            get { return _conectiontime; }
//        }


//        public object readonlyobject = new object();
 
//        private DateTime _lastrecvdatatime = DateTime.MinValue;

//        public virtual DateTime LastRecvDataTime
//        {
//            get { return _lastrecvdatatime; }
//        }

//        public virtual void OnGetReceiveData(object state)
//        {
//            ReciveEventArgs data = (ReciveEventArgs)state;
//            OnDataReceive(data);
//        }

//        public class ClientReciveEventArgs
//        {
//            public byte[] Data;
//            public Client SocketHelper = null;
//            public ClientReciveEventArgs(byte[] data, Client sockethelper)
//            {
//                Data = data;
//                SocketHelper = sockethelper;
//            }
//        }

//        public ClientReciveEventArgs GetClientReciveEventArgs(byte[] data, Client netbase)
//        {
//            return new ClientReciveEventArgs(data, this);
//        }
 
//        public abstract void Open();
//        public abstract void Close();
//        public abstract void ConnectionServer();
 
//        public virtual NetResult Send(short maincommand, int subcommand, byte[] data)
//        {
//            NetPacket package = new NetPacket(maincommand, subcommand, data);
//            NetResult result = Send(package);
//            return result;
//        }
 
//        public virtual void ReceiveData(ReciveEventArgs recvdata)
//        {
//            OnGetReceiveData(recvdata);
//        }

//        public override string ToString()
//        {
//            return this.Name;
//        }

//        public abstract void Dispose();
  

//        public virtual byte[] GetSendData(byte[] data)
//        {
//            List<byte> list = new List<byte>();
//            list.AddRange(BitConverter.GetBytes(data.Length));
//            list.AddRange(data);
//            return list.ToArray();
//        }
 
//    }
//}