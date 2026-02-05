using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

using System.Net.NetworkInformation;
using Feng.Utils;
using Feng.Net.NetArgs;
using Feng.Net.Base;
using Feng.Net.Packets;

namespace Feng.Net.Tcp
{ 
    public class TcpMapping
    {
        private TcpClient tcpclient = null;
        private TcpServer tcpserver = null;
        public TcpMapping()
        {
            tcpclient = new TcpClient();
            tcpserver = new TcpServer();
        }
        public TcpClient TcpClient { get { return tcpclient; } }
        public TcpServer TcpServer { get { return tcpserver; } }

        public void InitServer(string ip,int port)
        {
            tcpserver.IP = ip;
            tcpserver.Port = port;
            tcpserver.ClientChanged += Tcpserver_ClientChanged;
            tcpserver.RecvData += Tcpserver_RecvData;
        }
        public void InitClient(string ip, int port)
        {
            tcpclient.RemoteIP = ip;
            tcpclient.RemotePort = port;
            tcpclient.RecvData += Tcpclient_RecvData;
        }

        private bool Tcpclient_RecvData(object sender, byte[] data)
        {
            try
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Net", "TcpMapping", "Tcpclient", "收到data长度:" + data.Length);
                string txt = System.Text.Encoding.ASCII.GetString(data);
                Feng.Utils.TraceHelper.WriteTrace("Feng.Net", "TcpMapping", "Tcpclient_RecvData", "收到data:\r\n" + txt);
                if (ClientProxy != null)
                {
                    ClientProxy.Send(data);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Net", "TcpMapping", "Tcpclient_BeforeDataReceive", ex);
            }
            return true;
        }

        public void OpenServer()
        {
            tcpserver.Open();
        }
        public void OpenClient()
        {
            tcpclient.Connection();
        }

        Interfaces.IClientProxy ClientProxy = null;
        private bool Tcpserver_RecvData(object sender, byte[] data)
        {
            try
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Net", "TcpMapping", "Tcpserver_RecvData", "收到data长度:" + data .Length);
                string txt = System.Text.Encoding.ASCII.GetString(data);
                Feng.Utils.TraceHelper.WriteTrace("Feng.Net", "TcpMapping", "Tcpserver_RecvData", "收到data:\r\n" + txt);
                tcpclient.Send(data);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Net", "TcpMapping", "Tcpserver", ex);
            }
            return true;
        }
        private void Tcpserver_ClientChanged(Interfaces.IClientProxy sh, ClientChangedMode mode)
        {
            try
            {
                ClientProxy = sh;
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Net", "TcpMapping", "Tcpserver_ClientChanged", ex);
            }
        }
 
        public static void Test()
        {
            TcpMapping tcpMapping = new TcpMapping();
            tcpMapping.InitClient("127.0.0.1", 44388);
            tcpMapping.InitServer("127.0.0.1", 43388);
            tcpMapping.OpenClient();
            tcpMapping.OpenServer();
            while (true)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
    
}