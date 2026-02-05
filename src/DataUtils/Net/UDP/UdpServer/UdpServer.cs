
using Feng.IO;
using Feng.Net.Base;
using Feng.Net.EventHandlers;
using Feng.Net.Interfaces;
using Feng.Net.NetArgs;
using Feng.Net.Tcp;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Feng.Net.UDP
{
    public partial class UdpServer : UdpServerBase
    { 
        Socket listener = null;
 
        public override void Open()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(this.IP), Port);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            listener.ExclusiveAddressUse = false;
            listener.Bind(localEndPoint);
            Listen = true; 
            Thread th = new Thread(new ParameterizedThreadStart(BeginAccept));
            th.IsBackground = true;
            th.Start();
            base.Open();
        }

        private Dictionary<string, IClientProxy> dicsclients = new Dictionary<string, IClientProxy>();

        public virtual void BeginAccept(object obj)
        {
            while (true)
            {
                Listen = true;
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);
                byte[] buffer = new byte[1024];
                int length = listener.ReceiveFrom(buffer, ref point);
                byte[] data = new byte[length];
                System.Buffer.BlockCopy(buffer, 0, data, 0, length);
                int len = BitConverter.ToInt32(buffer, 0);
                if (buffer.Length >= (len + 4))
                {
                    data = new byte[len];
                    Array.Copy(buffer, 4, data, 0, len);
                }
                else
                {
                    return;
                }

                string clientpoint = point.ToString();
                IClientProxy client = null;
                bool newclient = false;

                lock (this)
                {
                    if (dicsclients.ContainsKey(clientpoint))
                    {
                        client = dicsclients[clientpoint];
                        if (client.State == ClientProxyState.Remove)
                        {
                            dicsclients.Remove(clientpoint);
                            client = null;
                        }
                    }
                    if (client == null)
                    {
                        newclient = true;
                        client = ClientProxyFactory.CreatUdpClientProxy(this.listener, this, point);
                        dicsclients.Add(clientpoint, client);
                    }
                }
                BeforeConnectedEventArgs e = new BeforeConnectedEventArgs(client);
                OnBeforeConnected(this, e);
                if (e.Cancel)
                {
                    return;
                }
                if (newclient)
                {
                    OnClientConnectioned(this, client);
                    Add(client);
                    client.Read();
                }
                ReciveEventArgs recvdata = new ReciveEventArgs(data, client);
                client.ReceiveData(recvdata);

            }
        }

        public override void Remove(IClientProxy clientproxy)
        {
            clientproxy.State = ClientProxyState.Remove;
            base.Remove(clientproxy);
        }
     
        public override void Close()
        { 
            Listen = false;
            listener.Close();
            for (int i = this.Clients.Count - 1; i >= 0; i--)
            {
                this.Clients[i].Close();
            }
        }

 
    }
}
