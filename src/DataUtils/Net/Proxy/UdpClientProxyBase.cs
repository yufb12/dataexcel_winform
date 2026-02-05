 
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

namespace Feng.Net.Base
{
    public abstract class UdpClientProxyBase : ClientProxyBase
    { 
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

         


         
         
    }

     
}
