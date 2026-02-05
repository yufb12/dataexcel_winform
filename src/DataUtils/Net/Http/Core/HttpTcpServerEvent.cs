using Feng.Net.EventHandlers;
using Feng.Net.NetArgs;
using Feng.Net.Packets;
using System;
namespace Feng.Net.Http
{
    public partial class HttpTcpServer
    {
        public delegate void LogEventHandler(object sender, string msgtype, string msg);
        public delegate void BeforeDataReceiveEventHandler(object sender, ReciveEventArgs e);

        public delegate void ListenExceptionEventHandler(object sender, Exception ex);

        public delegate void BeforeSendDataEventHandler(object sender, byte[] data);

        public delegate void ReceiveSystemDataEventHandler(object sender, NetPacket ph, ReciveEventArgs e);


        public delegate void UnhandledExceptionHandler(object sender, Exception ex);
        public delegate void ListenCloseHandler();

        public event LogEventHandler Loged;

        public event BeforeConnectedEventHandler BeforeConnected;
        public event ConnectedEventHandler Connectioned;
        public event ListenExceptionEventHandler ListenException;


        public event ListenCloseHandler ListenCloseed;

        public event UnhandledExceptionHandler UnhandledException;

        public event BeforeSendDataEventHandler BeforeSendData;
        public event ReceiveSystemDataEventHandler E_ReceiveSystemData;

        public override void OnUnhandledException(Exception ex)
        {
            if (UnhandledException != null)
            {
                UnhandledException(this, ex);
            }
        }



        public void OnListenCloseed()
        {
            if (ListenCloseed != null)
            {
                ListenCloseed();
            }
        }

        public virtual void OnLog(string msgtype, string msg)
        {
            if (Loged != null)
            {
                Loged(this, msgtype, msg);
            }
        }

        public virtual void OnReceiveSystemData(NetPacket ph, ReciveEventArgs e)
        {
            if (E_ReceiveSystemData != null)
            {
                E_ReceiveSystemData(this, ph, e);
            }
        }

        public override void OnBeforeSendData(byte[] data)
        {
            if (BeforeSendData != null)
            {
                BeforeSendData(this, data);
            }
        }

        public override void OnBeforeDataReceive(ReciveEventArgs e)
        {

        }

    }

}