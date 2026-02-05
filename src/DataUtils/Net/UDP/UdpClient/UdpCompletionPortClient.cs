 
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Windows.Forms;
//using Feng.Net.NetArgs;
//using Feng.Net.Tcp;

//using Feng.Utils; 
//namespace Feng.Net.UDP
//{

//    public class UdpCompletionPortClient : UdpClientProxyBase, IDisposable
//    {
//        Socket _client;
//        public static int DefaultBufferSize = 1024 * 1024;
//        public Stack<SocketAsyncEventArgs> SocketAsyncStack = new Stack<SocketAsyncEventArgs>();

//        private DateTime connctiontime = DateTime.MinValue;
//        public override DateTime ConnectionTime
//        {
//            get { return connctiontime; }
//        }
//        public UdpCompletionPortClient()
//        {
//            this.DefaultSendFileSize = 1024 * 32;
//        }
//        public override void Dispose()
//        {
//            this.Close();
//        }

//        private bool _hasconnected = false;
//        public override bool HasConnected
//        {
//            get
//            {
//                return _hasconnected;
//            }
//            set
//            {
//                _hasconnected = value;
//            }
//        }
 
//        private int _port = NetSettings.DefultUdpServerPort;
 
//        public override int LoalPort
//        {
//            get
//            {
//                return _port;
//            }
//            set
//            {
//                _port = value;
//            }
//        }
//        private IPEndPoint _endpoint = null;
//        public virtual IPEndPoint EndPoint
//        {
//            get
//            {
//                if (_endpoint == null)
//                {
//                    _endpoint = new IPEndPoint(IPAddress.Parse(this.LocalIP), this.LoalPort);
//                }
//                return _endpoint;
//            }
//        }
//        public IPEndPoint _RemoteEndPoint = null;

//        public virtual IPEndPoint RemoteEndPoint
//        {
//            get
//            {
//                return _RemoteEndPoint;
//            }
//            set
//            {
//                _RemoteEndPoint = value;
//            }
//        }
//        private List<SocketAsyncEventArgs> listSocketAsync = new List<SocketAsyncEventArgs>();
//        public const int MaxSocketAsync = 16;
//        public virtual SocketAsyncEventArgs GetSocketAsyncEventArgs()
//        {
//            SocketAsyncEventArgs result = new SocketAsyncEventArgs();
//            if (listSocketAsync.Count > MaxSocketAsync)
//            {
//                for (int i = 0; i < 10; i++)
//                { 
//                    System.Threading.Thread.Sleep(50);
//                    if (this.SocketAsyncStack.Count > 0)
//                    {
//                        break;
//                    }
//                }
//            }
//            if (this.SocketAsyncStack.Count < 1)
//            { 
//                return result;
//            }
//            lock (this)
//            {
//                result = this.SocketAsyncStack.Pop();
//            }
//            return result;
//        }

//        private int _dafaultsendfilesize = 1024 * 4;
//        public int DefaultSendFileSize
//        {
//            get {
//                return _dafaultsendfilesize;
//            }
//            set {
//                _dafaultsendfilesize = value;
//            }
//        } 
//        public override void Open()
//        {

//            try
//            { 
//                _client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            } 
//        }
//        public override void ConnectionServer()
//        {
//            connctiontime = DateTime.Now;
//        }
//        public override void Read()
//        {
//            connctiontime = DateTime.Now;
//            BeginRecv(this);
//        }
//        public void BeginRecv(object obj)
//        {

//            try
//            { 
//                SocketAsyncEventArgs asynargs = GetSocketAsyncEventArgs();
//                asynargs.RemoteEndPoint = this.EndPoint; 
//                if (!_client.ReceiveFromAsync(asynargs))
//                {

//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }

//        }
//        public void BeginRecv(SocketAsyncEventArgs asynargs)
//        {

//            try
//            {  
//                asynargs.RemoteEndPoint = this.EndPoint;
//                if (!_client.ReceiveFromAsync(asynargs))
//                {

//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }

//        }

//        void asynargs_Completed(object sender, SocketAsyncEventArgs e)
//        {
//            //this.RemoteEndPoint = e.RemoteEndPoint as IPEndPoint;
//            //TraceHelper.WriteConsole("UDPCLIENT====ASYNARGS_COMPLETED LastOperation==" + e.LastOperation.ToString()); 
 
//            switch (e.LastOperation)
//            {
//                case SocketAsyncOperation.SendTo:
//                    this.DoSend(e);
//                    break;
//                case SocketAsyncOperation.ReceiveFrom:
//                    this.DoDataReceived(e);
//                    return;
//                default:
//                    break;
//            }
//            lock (this)
//            {
//                SocketAsyncStack.Push(e);
//            }
//        }

//        void DoDataReceived(SocketAsyncEventArgs e)
//        { 
//            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
//            {
//                int bytesRead = e.BytesTransferred;
//                byte[] data = new byte[bytesRead];
//                System.Buffer.BlockCopy(e.Buffer, 0, data, 0, bytesRead);
//                GetReceiveData(data);
//            }
//            if (this._client != null)
//            {
//                this.BeginRecv(e);
//            }
//            //ThreadPool.QueueUserWorkItem(new WaitCallback(BeginRecv));
//            //TraceHelper.WriteConsole("UDPCLIENT====QueueUserWorkItem(BeginRecv)");
//        }
 
//        public void DoSend(SocketAsyncEventArgs e)
//        {

//        }

//        public override NetResult SendToRemote(NetPacket packet)
//        {
//            return Post(packet);
//        }

//        public override NetResult Post(NetPacket packet)
//        { 
//            try
//            {
//                packet.Session = this.Session;
//                byte[] data = packet.GetData();
//                data = this.GetSendData(data);

//                SocketAsyncEventArgs asynargs = GetSocketAsyncEventArgs();
//                asynargs.RemoteEndPoint = EndPoint;

//                if (asynargs.Buffer.Length > data.Length)
//                {
//                    System.Buffer.BlockCopy(data, 0, asynargs.Buffer, 0, data.Length);
//                    asynargs.SetBuffer(0, data.Length);
//                }
//                else
//                {
//                    throw new Exception("Data Too Long! >" + DefaultBufferSize);
//                }
//                bool res = _client.SendToAsync(asynargs);
//                if (res)
//                {
//                    return NetResult.SuccessResult();
//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.TraceHelper.WriteTrace(ex);
//                NetResult.ErrorResult(ex.Message, NetResult.CODE_KERNEL);
//            }

//            return NetResult.SuccessResult();
       
//        }

//        public override void Close()
//        { 
//            lock (this)
//            {  
//                if (_client != null)
//                {
//                    _client.Shutdown(SocketShutdown.Both);

//                    _client.Close();
//                    _client = null;
      
//                } 
//            }
//        }
 
//        public override string ToString()
//        {
//            return this.Name;
//        }
 
//    }
//} 