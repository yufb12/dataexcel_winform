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

    public class ResultString
    {
        public ResultString()
        {

        }
        public ResultString(bool success, string value)
        {
            this.Success = success;
            this.Value = value;
        }
        public string Value { get; set; }
        public bool Success { get; set; }
        public int ErrorCode
        {
            get { return _errorcode; }
            set { _errorcode = value; }
        }
        private int _errorcode = 0;

        public string Title { get; set; }
        public int IntRes { get; set; }
        public string StrRes { get; set; }
        private static ResultString _netsenderror = null;
        public static ResultString NetSendError
        {
            get
            {
                if (_netsenderror == null)
                {
                    _netsenderror = new ResultString(false, "发生异常，请检查网络是否连接，并联系管理员！");
                    _netsenderror.ErrorCode = 10001;
                }
                return _netsenderror;
            }
        }

        private static ResultString _unkownerror = null;
        public static ResultString UnkownError
        {
            get
            {
                if (_unkownerror == null)
                {
                    _unkownerror = new ResultString(false, "发生未知异常，并联系管理员！");
                    _unkownerror.ErrorCode = 10002;
                }
                return _unkownerror;
            }
        }


        private static ResultString _null = null;
        public static ResultString Null
        {
            get
            {
                if (_null == null)
                {
                    _null = new ResultString(false, "未返回任何信息！");
                    _null.ErrorCode = 10003;
                }
                return _null;
            }
        }

        public int IntValue
        {
            get
            {
                return Feng.Utils.ConvertHelper.ToInt(Value);
            }
        }

        public DateTime DeteValue
        {
            get
            {
                return Feng.Utils.ConvertHelper.ToDateTime(Value);
            }
        }

        public decimal DecValue
        {
            get
            {
                return Feng.Utils.ConvertHelper.ToDecimal(Value);
            }
        }
        public override string ToString()
        {
            if (_errorcode > 0)
            {
                return string.Format("{0},错误代码:{1}", Value, ErrorCode);
            }
            else
            {
                return Value;
            }
        }

        public static ResultString ReadResult(NetPacket ph)
        {
            ResultString res = new ResultString();
            using (Feng.IO.BufferReader reader = new IO.BufferReader(ph.PacketContents))
            {
                res.Success = reader.ReadBoolean();
                res.Value = reader.ReadString();
            }
            return res;
        }


    }


}
