using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Feng.Net.Base;
namespace Feng.Net.Extend
{
    public interface IClientExtend
    {
        ClientProxyBase Client { get; }
        void Bingding(ClientProxyBase client);
        void UnBingding();
    }
    public abstract class ClientExtendBase : IClientExtend
    {

        private ClientProxyBase netclient = null;
        public virtual ClientProxyBase Client
        {
            get
            {
                return this.netclient;
            }
        }
        public virtual void Bingding(ClientProxyBase client)
        { 
            netclient = client; 
        }

 
        public virtual void UnBingding()
        { 

        }
    }
 
}