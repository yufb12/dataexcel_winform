using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;

namespace Feng.Net.Packets 
{
    /// <summary>
    /// 1000以下为系统专用
    /// </summary>
    public class PacketMainCmd
    {
        public const short System = 1;
        public const short UDP = 3;
        public const short File = 6;
        public const short SQL = 7;
        public const short DataProject = 8;
        public const short Test = 99;
        public const short Simple = 100;
        public const short DefaultUserMainCommand = 0x1000; 
    }


    //public class PacketMainCmd
    //{
    //    public const short Defult = 0;
    //    public const short Connection = 1;
    //    public const short RegeditSession = 2;
    //    public const short UserChanged = 3;
    //    public const short GetOnlineUser = 4;
    //    public const short Heartbeat = 5;
    //    public const short Close = 6;
    //    public const short CheckVersion = 7;
    //    public const short Text = 8;
    //    public const short Regedit = 9;
    //    public const short Login = 10;
    //    public const short ToOtherUser = 11;
    //    public const short ToAllUser = 12;
    //    public const short ToServer = 13;
    //    public const short SQL = 14;
    //    public const short Room = 15;
    //    public const short DefaultUserMainCommand = 0xFF;
    //    public const short CheckOnLine = 0x21;
    //    public const short AutoUpdate = 0x22;
    //    public const short RecvFile = 0x23;
    //    public const short Changed = 0x24;
    //    public const short OtherLogin = 0x25;//其他地方登录
    //    public const short File = 0xA3;
    //    public const short DOM = 0x26; 

    //}
}
