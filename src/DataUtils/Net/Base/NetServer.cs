using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using Feng.Net.Tcp;
using Feng.Net.EventHandlers;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
namespace Feng.Net.Base
{
    public abstract class NetServer
    {
        private DateTime _starttime = DateTime.Now;
        public virtual DateTime StartTime
        {
            get
            {
                return _starttime;
            }
        }

        private int _port = NetSettings.DefultTcpServerPort;
        public virtual int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        private string _ip = string.Empty;
        public virtual string IP
        {
            get {
                return this._ip;
            }
            set
            {
                this._ip = value;
            }
        }
        private string _name = "Server";
        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private bool _opened = false;
        public virtual bool Opened
        {
            get {
                return _opened;
            }
        }

        private ClientProxyCollection _clients = new ClientProxyCollection();

        public virtual ClientProxyCollection Clients
        {
            get { return _clients; }
        }

        public virtual void Open()
        {
            _opened = true;
            _starttime = DateTime.Now;
        }

        public abstract void Close();

        public abstract void OnBeforeDataReceive(ReciveEventArgs e);

        public abstract void OnBeforeSendData(byte[] data);

        public abstract void OnUnhandledException(Exception ex);

        public virtual void Remove(IClientProxy clientproxy)
        {
            this.Clients.Remove(clientproxy);
            OnClientChanged(clientproxy, ClientChangedMode.Remove);
        }

        public virtual void Add(IClientProxy clientproxy)
        {
            this.Clients.Add(clientproxy);
            OnClientChanged(clientproxy, ClientChangedMode.Add);
        }

        public event ClientChangedHandler ClientChanged;

        public virtual void OnClientChanged(IClientProxy clientproxy,ClientChangedMode mode)
        {
            if (ClientChanged != null)
            {
                ClientChanged(clientproxy, mode);
            }
        }

        public virtual bool OnRecvData(byte[] data)
        {
            if (this.RecvData != null)
            {
                return this.RecvData(this, data);
            }
            return false;
        }

        public event RecvDataEventHandler RecvData;
    }
} 