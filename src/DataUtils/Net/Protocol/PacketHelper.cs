using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Feng.Net.Tcp;
using System.Drawing;
using Feng.Data;
using Feng.Net.EventHandlers;
using Feng.Net.Base;
using Feng.Utils;

namespace Feng.Net.Packets
{
    public class NetPacket
    {
        public static readonly byte[] Empty = new byte[] { };
        public const int Version = 103;
        public static event EncryptNetPacketHandler EncryptNetPacket;
        public static event DEncryptNetPacketHandler DEncryptNetPacket;
        public NetPacket()
        { 
            PacketHeader = (NetSettings.PacketHeader); 
            PacketFooter = (NetSettings.PacketFooter);
            PacketMode = Feng.Net.Packets.PacketMode.Send;
        }

        public NetPacket(byte packettype, short packetmaincommand, int packetsubcommand, byte[] data)
        { 
            PacketHeader = (NetSettings.PacketHeader);
            PacketMainCommand = (packetmaincommand);
            PacketSubcommand = (packetsubcommand);
            PacketFooter = (NetSettings.PacketFooter);
            this.PacketContents = data;
         
        }
        public NetPacket(byte packettype, short packetmaincommand, int packetsubcommand)
        { 
            PacketHeader = (NetSettings.PacketHeader);
            PacketMainCommand = (packetmaincommand);
            PacketSubcommand = (packetsubcommand);
            PacketFooter = (NetSettings.PacketFooter);

        }
        public NetPacket(short packetmaincommand, int packetsubcommand)
        { 
            PacketHeader = (NetSettings.PacketHeader);
            PacketMainCommand = (packetmaincommand);
            PacketSubcommand = (packetsubcommand);
            PacketFooter = (NetSettings.PacketFooter);

        }
        public NetPacket(short packetmaincommand, int packetsubcommand, byte[] data)
        { 
            PacketHeader = (NetSettings.PacketHeader);
            PacketMainCommand = (packetmaincommand);
            PacketSubcommand = (packetsubcommand);
            PacketFooter = (NetSettings.PacketFooter);
            this.PacketContents = data;
        }
        public NetPacket(short packetmaincommand, int packetsubcommand, string data)
        { 
            PacketHeader = (NetSettings.PacketHeader);
            PacketMainCommand = (packetmaincommand);
            PacketSubcommand = (packetsubcommand);
            PacketFooter = (NetSettings.PacketFooter);
            this.PacketContents = Feng.IO.BitConver.GetBytes(data);
        }
 
        public byte[] GetData()
        {
            return GetData(this);
        }

        public static byte[] GetData(NetPacket ph)
        {

            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(ph.PacketHeader);
                bw.Write(ph.DEncrypt);
                byte[] data = ph.PacketContents;

                if (ph.DEncrypt == Constants.TRUE)
                {
                    if (EncryptNetPacket != null)
                    {
                        using (Feng.IO.BufferWriter bwen = new Feng.IO.BufferWriter())
                        {
                            bwen.Write(ph.Packetindex);
                            bwen.Write(ph.Session);
                            bwen.Write(ph.PacketMainCommand);
                            bwen.Write(ph.PacketSubcommand);
                            bwen.Write(ph.PacketMode);
                            bwen.Write(ph.Compress);
                            data = ph.PacketContents;

                            if (ph.Compress == Feng.Utils.Constants.TRUE)
                            {
                                data = Feng.IO.CompressHelper.GZipCompress(data); 
                            }
                            bwen.Write(data);
                            data = bwen.GetData();
                        }
                        data = EncryptNetPacket(ph, data);
                    }
                }
                else
                {
                    bw.Write(ph.Packetindex);
                    bw.Write(ph.Session);
                    bw.Write(ph.PacketMainCommand);
                    bw.Write(ph.PacketSubcommand);
                    bw.Write(ph.PacketMode);
                    bw.Write(ph.Compress);
                    data = ph.PacketContents;
                    
                    if (ph.Compress == Feng.Utils.Constants.TRUE)
                    {
                        data = Feng.IO.CompressHelper.GZipCompress(data); 
                    }
                }

                bw.Write(data); 
                int crc = CRC(bw.GetData());
                bw.Write(crc);
                bw.Write(ph.PacketFooter);
                return bw.GetData();
            }
        }

        public static int CRC(Byte[] buffer)
        {
            return CRC(buffer, 0, buffer.Length);
        }

        public static int CRC(Byte[] buffer, int start, int end)
        {
            int crcmode = 0;
            for (int i = start; i < end; i++)
            {
                crcmode = (crcmode + (sbyte)buffer[i]) % short.MaxValue;
            }

            return crcmode;
        }
         

        public bool Init(byte[] data, int start, int length)
        {
            if (data == null)
                return false;
            if (length < 29)
                return false;
            int crc = CRC(data, start, start + length - 6);
            using (Feng.IO.BufferReader bw = new Feng.IO.BufferReader(data, start, length))
            {
                this.PacketHeader = bw.ReadInt16();
                this.DEncrypt = bw.ReadByte();
                if (this.DEncrypt == Constants.TRUE)
                {
                    byte[] buffer = bw.ReadBuffer();
                    if (DEncryptNetPacket != null)
                    {
                        buffer = DEncryptNetPacket(this, buffer);
                        using (Feng.IO.BufferReader bwen = new Feng.IO.BufferReader(buffer))
                        {
                            this.Packetindex = bwen.ReadInt32();
                            this.Session = bwen.ReadInt32();
                            this.PacketMainCommand = bwen.ReadInt16();
                            this.PacketSubcommand = bwen.ReadInt32();
                            this.PacketMode = bwen.ReadByte();
                            this.Compress = bwen.ReadByte();
                            buffer = bwen.ReadBuffer();
                            if (this.Compress == Feng.Utils.Constants.TRUE)
                            {
                                this.PacketContents = Feng.IO.CompressHelper.GZipDecompress(buffer);
                            }
                            else
                            {
                                this.PacketContents = buffer;
                            }
                        }
                    } 
                }
                else
                {
                    this.Packetindex = bw.ReadInt32();
                    this.Session = bw.ReadInt32();
                    this.PacketMainCommand = bw.ReadInt16();
                    this.PacketSubcommand = bw.ReadInt32();
                    this.PacketMode = bw.ReadByte();
                    this.Compress = bw.ReadByte();
                    int PacketContentslen = bw.ReadInt32();
                    if (PacketContentslen + bw.BaseStream.Position + 6 > bw.BaseStream.Length)
                    {
                        goto LabelEndPoint;
                    }
                    byte[] buffer = bw.ReadBytes(PacketContentslen);
                    if (this.Compress == Feng.Utils.Constants.TRUE)
                    {
                        this.PacketContents = Feng.IO.CompressHelper.GZipDecompress(buffer);
                    }
                    else
                    {
                        this.PacketContents = buffer;
                    }
                }
                this.Crc = bw.ReadInt32();
                this.PacketFooter = bw.ReadInt16();
                if (this.Crc == crc)
                {
                    return true;
                }
            LabelEndPoint:
                return false;
            }
        }

        #region 属性
        private short _packetheader = 0;                        //int packetheader, 
        private int _packetindex = 0;                         //int packetindex, 
        private byte _packetmode = 0;                         //byte packetmode,            SEND,POST,ERROR,RETURN,ASK,ANSWER
        private int _session = 0;                             //int session,  
        private short _packetmaincommand = 0;                   //SHORT packetcommand,        SYSTEM,
        private int _packetsubcommand = 0;                    //SHORT packetsubcommand,     REGEDIT,TEXT,DATA,IMAGE,
        //byte[] packetcontents, 
        private byte[] _packetcontents;                       //int packetfooter                     
        private int _crc = 0;
        private short _packetfooter = 0;
        private byte _compress = Feng.Utils.Constants.FALSE;
        private byte _DEncrypt = Feng.Utils.Constants.FALSE;
        private short _waittime = 15; 
        /// <summary>
        /// 包头固定大小
        /// </summary>
        public short PacketHeader
        {
            get { return _packetheader; }
            set { _packetheader = value; }
        }

        /// <summary>
        /// 包顺序
        /// </summary>
        public int Packetindex
        {
            get { return _packetindex; }
            set { _packetindex = value; }
        }

        /// <summary>
        /// 包模式，是否需要返回，是否POST。
        /// </summary>
        public byte PacketMode
        {
            get { return _packetmode; }
            set { _packetmode = value; }
        }
        /// <summary>
        /// 包的Session
        /// </summary>
        public int Session
        {
            get { return _session; }
            set { _session = value; }
        }
 
        /// <summary>
        /// 包命令模式
        /// </summary>
        public short PacketMainCommand
        {
            get { return _packetmaincommand; }
            set { _packetmaincommand = value; }
        }
        /// <summary>
        /// 子命令
        /// </summary>
        public int PacketSubcommand
        {
            get { return _packetsubcommand; }
            set { _packetsubcommand = value; }
        }

        /// <summary>
        /// 包的具体内容
        /// </summary>
        public byte[] PacketContents
        {
            get { return _packetcontents; }
            set { _packetcontents = value; }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public int Crc
        {
            get { return _crc; }
            set { _crc = value; }
        }

        /// <summary>
        /// 包尾
        /// </summary>
        public short PacketFooter
        {
            get { return _packetfooter; }
            set { _packetfooter = value; }
        }
 
        /// <summary>
        /// 等待返回的时候
        /// </summary>
        public short WaitTime
        {
            get { return _waittime; }
            set { _waittime = value; }
        }
 
        /// <summary>
        /// 是否压缩包
        /// </summary>
        public byte Compress
        {
            get { return _compress; }
            set { _compress = value; }
        }

        /// <summary>
        /// 是否压缩包
        /// </summary>
        public byte DEncrypt
        {
            get { return _DEncrypt; }
            set { _DEncrypt= value; }
        }
        #endregion
        public static NetPacket Get(byte[] Data)
        { 
            NetPacket ph = new NetPacket();
            if (ph.Init(Data, 0, Data.Length))
            {
                return ph;
            }
            return null;
        }
        public static NetPacket Get(byte[] Data, int start, int length)
        {
            NetPacket ph = new NetPacket();
            if (ph.Init(Data, start, length))
            {
                return ph;
            }
            return null;
        }
        public ResultString ReadResultString()
        {
            ResultString result = new ResultString();
            using (Feng.IO.BufferReader bw = new IO.BufferReader(this.PacketContents))
            {
                result.Success = bw.ReadBoolean();
                result.Value = bw.ReadString();
            }
            return result;
        }
        public void WriteResultString(bool success, string value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }
           
        }
        public void WriteResultString(bool success, string title, string value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                bw.Write(title);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResultString(bool success, string title, int value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                bw.Write(title);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResult(bool success, byte[] value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResult(bool success, int value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResult(bool success, decimal value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResult(bool success, bool value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResult(bool success)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success); 
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResult(bool success, string msg)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                bw.Write(msg);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResult(bool success, string error, byte[] value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(success);
                if (success)
                {
                    bw.Write(value);
                }
                else
                {
                    bw.Write(error);
                }
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResultSuccess(byte[] value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(true);
                bw.Write(value); 
                this.PacketContents = bw.GetData();
            }

        }
         
        public void WriteResultSuccess()
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(true); 
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResultSuccess(string value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(true);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResultSuccess(bool value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(true);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }

        public void WriteResultSuccess(int value)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(true);
                bw.Write(value);
                this.PacketContents = bw.GetData();
            }

        }
        public void WriteResultSuccess(int value,byte [] data)
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(value);
                bw.Write(data);
                this.PacketContents = bw.GetData();
            }

        }

        Feng.IO.BufferWriter bw = null;
        public Feng.IO.BufferWriter Writer
        {
            get {
                return bw;
            }
        }
        public virtual void BeginWriter()
        {
            bw = new IO.BufferWriter();
        }
        public virtual void EndWriter()
        {
            if (bw != null)
            {
                this.PacketContents = bw.GetData();
                bw.Close();
                bw = null;
            }
        }
        Feng.IO.BufferReader reader = null;
        public Feng.IO.BufferReader Reader
        {
            get {
                return reader;
            }
        }
        public virtual void BeginRead()
        {
            reader = new IO.BufferReader(this.PacketContents);
        }
        public virtual void EndRead()
        {
            reader.Close();
            reader = null;
        }

        public virtual bool ReadFirstBoolValue()
        {
            BeginRead();
            bool result = reader.ReadIndex(1, false);
            EndRead();
            return result;
        }

        public virtual string ReadFirstTextValue()
        {
            BeginRead();
            string result = reader.ReadIndex(1, string.Empty);
            EndRead();
            return result;
        }
 
        public virtual long ReadFirstLongValue()
        {
            BeginRead();
            long result = reader.ReadIndex(1, (long)0);
            EndRead();
            return result;
        }
 
        public virtual int ReadFirstIntValue()
        {
            BeginRead();
            int result = reader.ReadIndex(1, (int)0);
            EndRead();
            return result;
        }
    }
}


 