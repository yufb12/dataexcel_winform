 
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
using Feng.Net.NetArgs;

namespace Feng.Net.Base
{
    public abstract class TcpClientProxyBase : ClientProxyBase
    {
        public void InitServer(NetServer server)
        {
            _server = server;
        }
        public virtual NetServer Server { get { return _server; } }
        private NetServer _server = null;
        private bool _hasconnected = false;
        public override bool HasConnected
        {
            get
            {
                return _hasconnected;
            }
            set
            {
                _hasconnected = value;
            }
        }
        public object readonlyobject = new object();

        protected List<byte> recvbuffer = new List<byte>();
        private DateTime _lastrecvdatatime = DateTime.MinValue;
        public override DateTime LastRecvDataTime
        {
            get
            {
                return base.LastRecvDataTime;
            }
        }
        public virtual void GetReceiveData(byte[] data)
        {
            //Feng.Utils.TraceHelper.WriteTrace("TcpClientProxyBase", "GetReceiveData", "GetReceiveData", "GetReceiveData", this);
            lock (readonlyobject)
            {
                _lastrecvdatatime = DateTime.Now;
                //Feng.Utils.TraceHelper.WriteTrace("TcpClientProxy", "TcpClientProxyBase", "GetReceiveData", _lastrecvdatatime.ToString());
                recvbuffer.AddRange(data);
                while (recvbuffer.Count > 4)
                {
                    byte[] buffer = recvbuffer.ToArray();
                    int len = BitConverter.ToInt32(buffer, 0);
                    if (buffer.Length >= (len + 4))
                    {
                        byte[] temp = new byte[len];
                        Array.Copy(buffer, 4, temp, 0, len);
                        System.Threading.ThreadPool.QueueUserWorkItem(OnGetReceiveData, new ReciveEventArgs(temp, this));
                        int yc = buffer.Length - 4 - len;
                        temp = new byte[yc];
                        recvbuffer.Clear();
                        if (yc > 0)
                        {
                            Array.Copy(buffer, len + 4, temp, 0, yc);
                            recvbuffer.Clear();
                        }
                        recvbuffer.AddRange(temp);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        public virtual void ClearCache()
        {
            recvbuffer.Clear();
        }
        protected virtual void CloseSocket()
        {
            this.ClearCache();
        }
        public event ConnectedEventHandler Connected;
        public event ClosedConnectedEventHandler ClosedConnected;

        protected virtual void OnClosedConnected()
        {
            if (ClosedConnected != null)
            {
                ClosedConnected(this);
            }
        }

        protected virtual void OnConnected()
        {
            if (Connected != null)
            {
                Connected(this, this);
            }
        }
        public virtual void OnGetReceiveData(object state)
        {

            try
            {
                Feng.Utils.TraceHelper.WriteTrace("ClientProxyBase", "OnGetReceiveData", "OnGetReceiveData", "OnGetReceiveData", this);
                ReciveEventArgs data = state as ReciveEventArgs;
                if (data != null)
                {
                    OnDataReceive(data);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                if (this.Server != null)
                {
                    this.Server.OnUnhandledException(ex);
                }
                else
                {
                    Feng.Utils.ExceptionHelper.ShowError(ex);
                }
            }

        }
    }

     
}
