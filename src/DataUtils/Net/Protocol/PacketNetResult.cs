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
 
    public class PacketNetResult
    {
        public PacketNetResult()
        {

        }

        public byte[] Value { get; set; }
        public object Obj { get; set; }
        public bool Success { get; set; }
        public string ErrorCode
        {
            get { return _errorcode; }
            set { _errorcode = value; }
        }
        private string _errorcode = string.Empty;

        private static PacketNetResult _netsenderror = null;
        public static PacketNetResult NetSendError
        {
            get
            {
                if (_netsenderror == null)
                {
                    _netsenderror.ErrorCode = Feng.Utils.ErrorCode.ErrorNetSender;
                }
                return _netsenderror;
            }
        }

        private static PacketNetResult _unkownerror = null;
        public static PacketNetResult UnkownError
        {
            get
            {
                if (_unkownerror == null)
                {
                    _netsenderror.ErrorCode = Feng.Utils.ErrorCode.ErrorUnknow;
                }
                return _unkownerror;
            }
        }

        private static PacketNetResult _null = null;
        public static PacketNetResult Null
        {
            get
            {
                if (_null == null)
                {
                    _netsenderror.ErrorCode = Feng.Utils.ErrorCode.ErrorNull;
                }
                return _null;
            }
        }


        private static PacketNetResult _ReturnedDataError = null;
        public static PacketNetResult ReturnedDataError
        {
            get
            {
                if (_ReturnedDataError == null)
                {
                    _netsenderror.ErrorCode = Feng.Utils.ErrorCode.ErrorNetReturnedDataError;
                }
                return _null;
            }
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(_errorcode))
            {
                return string.Format("{0},错误代码:{1}", Value, ErrorCode);
            }
            else
            {
                return Feng.Utils.ConvertHelper.ToString(Value);
            }
        }

        public static PacketNetResult GetResult(NetPacket ph)
        {
            PacketNetResult result = new PacketNetResult();
            using (Feng.IO.BufferReader reader = new IO.BufferReader(ph.PacketContents))
            {
                bool success = reader.ReadBoolean();
                result.Success = success;
                if (success)
                {
                    result.Value = reader.ReadBuffer();
                }
                else
                {
                    result.ErrorCode = reader.ReadString();
                }
            }
            return result;
        }
    }
}
