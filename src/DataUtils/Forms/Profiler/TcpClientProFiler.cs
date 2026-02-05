 
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using Feng.Net.Tcp;
using Feng.Net.Extend;
using Feng.Net.NetArgs;
using Feng.Net.Base;
namespace Feng.Net.ProFiler
{
    public class ProFilerData
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime Time { get; set; }
        public string Method { get; set; }
        public int DataLength { get {
            if (this.Data == null)
            {
                return 0;
            }
            else
            {
                return this.Data.Length;
            }
        } }
        public byte[] Data { get; set; }
    }

    public class TcpClientProFiler
    {
        private List<ProFilerData> list = new List<ProFilerData>();
        public List<ProFilerData> List { get {
            return list;
        } }
        int index = 1;
        public void Add(ProFilerData data)
        {
            lock (this)
            {
                data.Index = index++;
                list.Add(data);
            }
        }
    }

    public class TcpClientExtendProFiler : ClientExtendBase
    {
        public TcpClientExtendProFiler(TcpClientProFiler proriler)
        {
            this.profiler = proriler;
        }

        private TcpClientProFiler profiler = null;
        public TcpClientProFiler ProFiler { get { return profiler; } }
        public override void UnBingding()
        {
            if (this.Client != null)
            {
                this.Client.BeforeDataReceive += client_BeforeDataReceive;
                this.Client.BeforeSendData += client_BeforeSendData;
            }
            base.UnBingding();
        }

        public override void Bingding(ClientProxyBase client)
        {
            if (client != null)
            {
                client.BeforeDataReceive += client_BeforeDataReceive;
                client.BeforeSendData += client_BeforeSendData;
            }
            base.Bingding(client);
        }

        void client_BeforeSendData(object sender, byte[] data)
        {
            try
            {
                ProFilerData profilerdata = new ProFilerData()
                {
                    Index = -1,
                    Data = data,
                    IP = this.Client.LocalIP,
                    Method = "SEND",
                    Name = this.Client.Name,
                    Time = DateTime.Now
                };
                ProFiler.Add(profilerdata);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        void client_BeforeDataReceive(object sender, ReciveEventArgs e)
        {

            try
            {
                ProFilerData profilerdata = new ProFilerData()
                {
                    Index = -1,
                    Data = e.Data,
                    IP = this.Client.LocalIP,
                    Method = "RECV",
                    Name = this.Client.Name,
                    Time = DateTime.Now
                };
                ProFiler.Add(profilerdata);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
  
        }
    }
}
