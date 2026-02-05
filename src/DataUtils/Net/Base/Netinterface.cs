using System;
using System.Net.Sockets;
using System.Collections.Generic;
using Feng.Net.Tcp;
using Feng.Net.EventHandlers;
using Feng.Net.NetArgs;
namespace Feng.Net.Interfaces
{
    public interface ITcp : IPort, IClose, IIPAdress, IStart
    {
        
    }
    public interface IDBConnectionString
    {
        string DBConnectionString { get; set; }
    }
 
 
    public interface IOnSocketHelperException
    {
        void OnSocketHelperException(object sender, SocketExceptionArgs ex);
    }
 
    //public interface IPacketHeadHelper
    //{
    //    byte[] GetReceiveData(byte[] data);
    //    byte[] GetSendData(byte[] data);
    //    ISocketHelper SocketHelper { get; set; }
    //}

    public interface IDefultEncoding
    {
        System.Text.Encoding DefultEncoding { get; set; }
    }

    //public interface ISocketHelper
    //{
    //    void Read();
    //    bool Send(PacketHelper in_value);
    //    void OnDataReceive(ReciveEventArgs e);
    //}

    public class SocketSateObject
    {
        public Socket soket = null;
        public static int BufferSize = 2048;
        public byte[] buffer = new byte[BufferSize];

    }

    public interface ISocketEvent
    {
        event ReceiveDataEventHandler DataReceive; 
        event SocketHelperException SocketHelperException;
    }
 
 
    public interface IPort
    {
        int Port { get; set; }
    }
    public interface IAutoConnection
    {
        bool AutoConnection { get; set; } 
    }

    public interface IClose
    { 
        void Close(); 
    }

    public interface IConnected
    {
        bool Connected { get; } 
    }

    public interface IIndex
    {
        ushort Index { get; set; } 
    }

    public interface IIPAdress
    { 
        System.Net.IPAddress IPAdress { get; set; } 
    }
 
    public interface IReceive
    { 
        void Receive(); 
    }

    public interface IStart
    {
        void Start();
    }
}
