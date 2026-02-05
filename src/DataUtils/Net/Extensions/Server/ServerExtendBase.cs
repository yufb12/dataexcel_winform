using Feng.Net.Base;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
using Feng.Net.Packets;
using System;
using System.Collections.Generic;

namespace Feng.Net.Extend
{
    public interface IServerExtend
    {
        //ServerController Controller { get; }
        //void Close();
        //void Bingding(ServerController controller);
        //void UnBingding();
    }

    public class ServerController
    {
        private IClientProxy _ClientProxy = null;
        public virtual IClientProxy ClientProxy { get { return this._ClientProxy; } }

        private NetServer _Server = null;
        public virtual NetServer Server { get { return this._Server; } }
        private List<ServerExtendBase> _Extends = new List<ServerExtendBase>();

        public ServerController(NetServer server, IClientProxy client)
        {
            this._Server = server;
            this._ClientProxy = client;
            this._ClientProxy.DataReceive += server_DataReceive;
        }
        public virtual List<ServerExtendBase> Extends
        {
            get
            {
                return _Extends;
            }
        }
        public virtual void Bingding(ServerExtendBase controller)
        {
            lock (this)
            {
                Extends.Add(controller);
            }
        }
 
        public virtual void UnBingding()
        {
            if (ClientProxy != null)
            {
                ClientProxy.DataReceive -= server_DataReceive;
            }
        }
        public virtual void DoExtendCommand(NetPacket ph, ReciveEventArgs e)
        {
            int count = Extends.Count;
#if DEBUG
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            Feng.Utils.TraceHelper.WriteTrace("ServerController", "DoExtendCommand", "ph", "Packetindex:" + ph.Packetindex,this);
#endif
            for (int i = count - 1; i >= 0; i--)
            {
                try
                {
#if DEBUG
                    stopwatch.Restart();
#endif
                    Extends[i].DoExtendCommand(ph, e);
#if DEBUG
                    stopwatch.Stop();
                    if (stopwatch.Elapsed.TotalMilliseconds > 200)
                    {
                        Feng.Utils.TraceHelper.WriteTrace("Net", "Extend", "DoExtendCommand", true, "HashCode:" + this.GetHashCode()+ ":耗时:" + stopwatch.Elapsed.TotalSeconds.ToString("#0.000"));
                    } 
#endif
                }
                catch (Exception ex)
                {
                    Feng.Utils.BugReport.Log(ex);
                    Feng.Utils.TraceHelper.WriteTrace("Net", "Extend", "DoExtendCommand", ex);
                }
            }
        }
        void server_DataReceive(object sender, ReciveEventArgs e, NetPacket ph)
        {

            try
            {
                DoExtendCommand(ph, e);
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }

        }

        public virtual void Remove(ServerExtendBase serverExtendBase)
        {
            lock (this)
            {
                Extends.Remove(serverExtendBase);
            }
        }
    }

    public abstract class ServerExtendBase : IServerExtend
    {

        private NetServer _Server = null;
        public virtual NetServer Server { get { return this._Server; } }
        public ServerExtendBase(NetServer server)
        {
            _Server = server;
        }
        //private ServerController _Controller = null;
        //public ServerController Controller { get { return this._Controller; } }
        //public virtual void Bingding(ServerController controller)
        //{
        //    _Controller = controller;
        //    _Controller.Bingding(this);
        //}
        public abstract void DoExtendCommand(NetPacket ph, ReciveEventArgs e);
        //public virtual void Close()
        //{
        //    UnBingding();
        //}
        //public virtual void Dispose()
        //{
        //    this.Close();
        //}
        //public virtual void UnBingding()
        //{
        //    Controller.Remove(this);
        //}
    }


}
