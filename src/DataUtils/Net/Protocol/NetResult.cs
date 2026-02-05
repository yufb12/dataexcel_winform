using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;
using Feng.Net.Packets;

namespace Feng.Net.Packets
{ 
    public class NetResult
    { 
        public NetResult()
        {

        }
        public NetResult(bool success)
        {
            this.Success = success;
        }
        public static NetResult EmptyResult()
        {
            return new NetResult();
        }
        public static NetResult ErrorResult(string msg, int code)
        {
            NetResult result = new NetResult();
            result.Success = false;
            result.Message = msg;
            result.Code = code;
            return result;
        }
        public static NetResult SuccessResult(string msg)
        {
            NetResult result = new NetResult();
            result.Success = true;
            result.Message = msg; 
            return result;
        }
        public static NetResult SuccessResult()
        {
            NetResult result = new NetResult();
            result.Success = true; 
            return result;
        }

        /// <summary>
        /// 代表Util Tools类库无法处理，内核级错误
        /// 抛出层次
        /// </summary>
        public const int CODE_KERNEL = 1000;

        /// <summary>
        /// 代表Util Tools类库问题，可以通过tools类库改进
        /// 抛出层次
        /// </summary>
        public const int CODE_SYSTEM = 2000;

        /// <summary>
        /// 代表权限问题
        /// 抛出层次
        /// </summary>
        public const int CODE_PERMISSION = 3000;

        /// <summary>
        /// 代表网络层出现问题
        /// 抛出层次
        /// </summary>
        public const int CODE_NET = 4000;
        /// <summary>
        /// 代表用户代码输入，顺序，方式，方法，传值有可能错误
        /// 抛出层次
        /// </summary>
        public const int CODE_USER = 9000;
        private int _code = 1;
        private bool _success = false;
        private object _value = null;
        private string _message = null;

        private byte[] _orgvalue = null;
        public virtual void Clear()
        {
            _code = 1;
            _success = false;
            _value = null;
            _message = null;
            _orgvalue = null;
        }
        public virtual int Code { get { return _code; } set { _code = value; } }
        public virtual bool Success { get { return _success; } set { _success = value; } }
        public virtual object Value { get { return _value; } set { _value = value; } }
        public virtual string Message { get { return _message; } set { _message = value; } }
        public virtual string Trace { get; set; }
        public virtual byte[] OrgValue { get { return _orgvalue; } set { _orgvalue = value; } }
        public virtual NetPacket Packet { get; set; }
        public void SetError(string msg,string trace,int code)
        {
            Success = false;
            Message = msg;
            Code = code;
            Trace = trace;
        }
        public void SetError(string msg, int code)
        {
            Success = false;
            Message = msg;
            Code = code;
        }
        public virtual byte[] GetData()
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(1, Success);
                bw.Write(2, Code);
                bw.Write(3, Message);
                bw.Write(9, OrgValue);
                return bw.GetData();
            }
        }

        public virtual void Read(byte[] data)
        {
            using (Feng.IO.BufferReader bw = new IO.BufferReader(data))
            {
                Success = bw.ReadIndex(1, Success);
                Code = bw.ReadIndex(2, Code);
                Message = bw.ReadIndex(3, Message);
                OrgValue = bw.ReadIndex(9, OrgValue);
            }
        }
    }
 
}
