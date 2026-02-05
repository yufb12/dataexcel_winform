
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using Feng.Net.Tcp;
using Feng.Net.UDP;
using Feng.Net.Packets;
using Feng.Net.Interfaces;
namespace Feng.Args
{
    public class BeforePositionChangedArgs : CancelEventArgs
    {
        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class FiguresEventArgs : EventArgs
    {
        public float OldValue
        {
            get;
            set;
        }

        public float NewValue
        {
            get;
            set;
        }
    }
}

namespace Feng.Net.NetArgs 
{

    public class RegeditUdpSessionEventArgs : CancelEventArgs
    {
        //public UdpClientHelper SocketHelper;
        //private string _user = string.Empty;
        //public string User { get { return _user; } }
        //public string Password;
        //public byte[] Data { get; set; }
        //public int Session;
        //public bool AllowConnection = true;
        //public RegeditUdpSessionEventArgs(string user, string password, int session, UdpClientHelper sockethelper)
        //{
        //    _user = user;
        //    Password = password;
        //    Session = session;
        //    SocketHelper = sockethelper;
        //}

        //public RegeditUdpSessionEventArgs(string user, string password, int session, byte[] data, UdpClientHelper sockethelper)
        //{
        //    _user = user;
        //    Password = password;
        //    Session = session;
        //    SocketHelper = sockethelper;
        //    this.Data = data;
        //}

    }
 
 

    public class ReciveEventArgs : CancelEventArgs
    {
        public byte[] Data;
        public IClientProxy ClientProxy = null;
        public ReciveEventArgs(byte[] data, IClientProxy sockethelper) 
        {
            Data = data;
            ClientProxy = sockethelper;
        }

    }
 

    public class BeforeConnectedEventArgs : CancelEventArgs
    {
        public IClientProxy SocketHelper = null;
        public BeforeConnectedEventArgs(IClientProxy sockethelper)
        {
            SocketHelper = sockethelper;
        }

    }

    public class CheckLineEventArgs : CancelEventArgs
    {
        public TcpClientProxy SocketHelper = null;
        public int Count { get; set; }
        public CheckLineEventArgs(TcpClientProxy sockethelper)
        {
            SocketHelper = sockethelper;
        }

    }


    public class  OtherLoginEventArgs : CancelEventArgs
    {
        public IClientProxy SocketHelper = null;
        public OtherLoginEventArgs(IClientProxy sockethelper)
        { 
            SocketHelper = sockethelper;
        }

    }

    public class NetCancelEventArgsBase : CancelEventArgs
    {
        public IClientProxy Server { get; set; }

    }
    public class NetCancelEventArgs : NetCancelEventArgsBase
    { 
        public NetPacket Packet { get; set; }
    }


    public class GetServerEventArgs : CancelEventArgs
    { 
        public ReciveEventArgs Args { get; set; }
        public string Url { get; set; }
        
        public string IP { get; set; }
        public int Port { get; set; }
        public GetServerEventArgs(ReciveEventArgs e, string url)
        {
            Url = url;
            Args = e; 
        } 
    }
    public class RegeditSessionEventArgs : CancelEventArgs
    {
        public IClientProxy SocketHelper;
        public NetPacket Packet { get; set; }
        private string _user = string.Empty;
        public string User { get { return _user; } }
        public string Password;
        public byte[] Data { get; set; }
        public int Session { get; set; }
        public string ID { get; set; }
        public RegeditSessionEventArgs(string user, string password, IClientProxy client)
        {
            _user = user;
            Password = password; 
            SocketHelper = client;
        }

        public RegeditSessionEventArgs(string user, string password, byte[] data, IClientProxy client)
        {
            _user = user;
            Password = password; 
            SocketHelper = client;
            this.Data = data;
        }

    }
 
    public class SocketException : Exception
    {
        public bool Cancel { get; set; }
    }
    public delegate void SocketErrorHandler(object sender, SocketErrorArgs ex);
    public delegate void SocketHelperException(object sender, SocketExceptionArgs ex);
    public class SocketErrorArgs : Exception
    {

        public SocketErrorArgs(TcpClientProxy soc, SocketError se)
        {
            this.SocketHelper = soc;
            this.SocketError = se;
        }

        public TcpClientProxy SocketHelper
        { get; set; }

        public SocketError SocketError
        {
            get;
            set;
        }
    }
    public class SocketExceptionArgs : CancelEventArgs
    {
        public SocketExceptionArgs()
        {

        }
        public SocketExceptionArgs(Exception ex, string proc)
        {
            this.BaseException = ex;
            this.ProcessName = proc;
        }

        public string ProcessName { get; set; }
        public Exception BaseException { get; set; }
    }
}
