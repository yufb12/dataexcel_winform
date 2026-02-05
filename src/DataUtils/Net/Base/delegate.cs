using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;

using Feng.Args;
using Feng.Net.NetArgs;
using Feng.Net.Packets;
using Feng.Net.Interfaces;
using System.Data;
using Feng.Net.Base;

namespace Feng.Net.EventHandlers
{
    public delegate bool RecvDataEventHandler(object sender, byte[] data);
    public delegate void QueryTableCallBack(object sender, DataTable table);
    public delegate byte[] DEncryptNetPacketHandler(object sender, byte[] data);
    public delegate byte[] EncryptNetPacketHandler(object sender, byte[] data);
    public delegate void DataReceiveEventHandler(object sender, ReciveEventArgs e, NetPacket ph);
    public delegate void BeforeConnectedEventHandler(object sender, BeforeConnectedEventArgs e);
    public delegate void ConnectedEventHandler(object sender, IClientProxy client);
    public delegate void ClosedConnectedEventHandler(object sender);
    public delegate void BeforeSendDataEventHandler(object sender, byte[] data); 
    public delegate void BeforeDataReceiveEventHandler(object sender, ReciveEventArgs e); 
    public delegate void ClientChangedHandler(IClientProxy sh, ClientChangedMode mode);
    public delegate void ReceiveDataEventHandler(object sender, ReciveEventArgs e);
    public delegate void BeforeAddNewConnectionEventHandler(object sender, CancelEventArgs e, IClientProxy sh);
    public delegate void LogEventHandler(object sender, string msgtype, string msg);
    public delegate void ListenExceptionEventHandler(object sender, Exception ex);
    public delegate void BeforeRecvDataEventHandler(object sender, ReciveEventArgs e);
    public delegate void ReceiveSystemDataEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
    public delegate void UnhandledExceptionHandler(object sender, Exception ex);
    public delegate void ListenCloseHandler();
}
namespace Feng.Net.NetArgs
{
    public class HandleArgs : EventArgs
    {
        public bool Handle { get; set; }
        public object Tag { get; set; }
    }
}
namespace Feng.Events
{
    public class CancelArgs : CancelEventArgs
    {
        public bool Handle { get; set; }
        public object Tag { get; set; }
    }
}
namespace Feng.EventHelper
{
    public delegate void ExceptionHandler(object sender, Exception e);
    public delegate void LegalCopyCheckEventHandler(object sender, EventArgs e);

    public delegate void ValueChangedEventHandler(object sender, int position);
    public delegate void BeforePositionChangedEventHandler(object sender, BeforePositionChangedArgs e);
    public delegate void ClickEventHandler(object sender, Point pt);
    public delegate void FiguresChangedEventHandler(object sender, FiguresEventArgs pt);
}