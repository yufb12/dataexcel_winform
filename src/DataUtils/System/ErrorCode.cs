using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Feng.Utils
{
    public static class ErrorCode
    {
        public const string ErrorNull = "UNKNOW:1001";
        public const string ErrorUnknow = "UNKNOW:1004";
        public const string ErrorNet = "NET:03001";
        public const string ErrorNetSender = "NET:03002";
        public const string ErrorNetRecv = "NET:03003";
        public const string ErrorNetPacket = "NET:03004";
        public const string ErrorNetEmpty = "NET:03005";
        public const string ErrorNetReturnedDataError = "NET:03006";//返回数据不正确
        public const string DataProjectErrorGetNumbID = "DATAPROJECT:05001";
        public const string FormatError = "FORMART:05001！";
        public const string SqlErrorCode_ConnectionNull = "SQL:4001";
        public const string SqlErrorCode_ARGS = "SQL:4002";//参数不匹配
        public static string GetErrorMsg(string id)
        {
            switch (id)
            {
                case SqlErrorCode_ARGS:
                    return "ERROR:" + id + " ,参数不匹配"; 
                default:
                    break;
            }
            return "ERROR:" + id + ""; ;
        }
        public const int DataProject_ServerExtend_ArgColumnIndex = 1051030011;
        public const int DataProject_ServerExtend_ArgCout = 1051030012;
        public const string DataProject_ServerExtend_ArgColumnIndex_Text = "ArgColumnIndex 未定义";
    }
}
