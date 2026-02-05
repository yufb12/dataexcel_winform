
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.Text;

namespace Feng.Net.UDP
{


    public class IcmpPacket
    {

        private void Button1_click(Object Sender, String Hostclient)
        {

            int K;
            for (K = 0; K < 3; K++)
            {
                Socket Socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);

                IPHostEntry Hostinfo = null;
                try
                {
                    //解析主机ip入口
                    Hostinfo = Dns.GetHostEntry(Hostclient);
                }
                catch (Exception)
                {
                }
                // 取服务器端主机的30号端口
                EndPoint Hostpoint = (EndPoint)new IPEndPoint(Hostinfo.AddressList[0], 30);
                IPHostEntry Clientinfo;
                Clientinfo = Dns.GetHostEntry(Hostclient);
                // 取客户机端主机的30端口
                EndPoint Clientpoint = (EndPoint)new IPEndPoint(Clientinfo.AddressList[0], 30);
                //设置icmp报文
                int Datasize = 4; // Icmp数据包大小 ;
                int Packetsize = Datasize + 8;//总报文长度
                const int Icmp_echo = 8;
                IcmpPacket Packet = new IcmpPacket(Icmp_echo, 0, 0, 45, 0, Datasize);
                Byte[] Buffer = new Byte[Packetsize];
                int index = Packet.CountByte(Buffer);
                //报文出错
                if (index != Packetsize)
                {
                }
                int Cksum_buffer_length = (int)Math.Ceiling(((Double)index) / 2);
                UInt16[] Cksum_buffer = new UInt16[Cksum_buffer_length];
                int Icmp_header_buffer_index = 0;
                for (int I = 0; I < Cksum_buffer_length; I++)
                {
                    //将两个byte转化为一个uint16

                    Cksum_buffer[I] = BitConverter.ToUInt16(Buffer, Icmp_header_buffer_index);
                    Icmp_header_buffer_index += 2;
                }
                //将校验和保存至报文里
                Packet.CheckSum = IcmpPacket.SumOfCheck(Cksum_buffer);
                // 保存校验和后，再次将报文转化为数据包
                Byte[] Senddata = new Byte[Packetsize];
                index = Packet.CountByte(Senddata);
                //报文出错
                if (index != Packetsize)
                {
                }
                int Nbytes = 0;
                //系统计时开始
                int Starttime = Environment.TickCount;
                //发送数据包
                if ((Nbytes = Socket.SendTo(Senddata, Packetsize, SocketFlags.None, Hostpoint)) == -1)
                {
                }
                Byte[] Receivedata = new Byte[256]; //接收数据
                Nbytes = 0;
                int Timeout = 0;
                int Timeconsume = 0;
                while (true)
                {
                    Nbytes = Socket.ReceiveFrom(Receivedata, 256, SocketFlags.None, ref  Clientpoint);
                    if (Nbytes == -1)
                    {
                        break;
                    }
                    else if (Nbytes > 0)
                    {
                        Timeconsume = System.Environment.TickCount - Starttime;

                        break;
                    }
                    Timeconsume = Environment.TickCount - Starttime;
                    if (Timeout > 1000)
                    {
                        break;
                    }
                }
                //关闭套接字
                Socket.Close();
            }
        }

        private Byte _type;
        // 类型
        private Byte _subCode;
        // 代码
        private UInt16 _checkSum;
        // 校验和
        private UInt16 _identifier;
        // 识别符
        private UInt16 _sequenceNumber;
        // 序列号 
        private Byte[] _data;
        //选项数据
        public IcmpPacket(Byte type, Byte subCode, UInt16 checkSum, UInt16 identifier, UInt16 sequenceNumber, int dataSize)
        {
            _type = type;
            _subCode = subCode;
            _checkSum = checkSum;
            _identifier = identifier;
            _sequenceNumber = sequenceNumber;
            _data = new Byte[dataSize];
            //在数据中，写入指定的数据大小
            for (int i = 0; i < dataSize; i++)
            {
                //由于选项数据在此命令中并不重要，所以你可以改换任何你喜欢的字符 
                _data[i] = (byte)'#';
            }
        }
        public UInt16 CheckSum
        {
            get
            {
                return _checkSum;
            }
            set
            {
                _checkSum = value;
            }
        }
        //初始化ICMP报文
        public int CountByte(Byte[] buffer)
        {
            Byte[] b_type = new Byte[1] { _type };
            Byte[] b_code = new Byte[1] { _subCode };
            Byte[] b_cksum = BitConverter.GetBytes(_checkSum);
            Byte[] b_id = BitConverter.GetBytes(_identifier);
            Byte[] b_seq = BitConverter.GetBytes(_sequenceNumber);
            int i = 0;
            Array.Copy(b_type, 0, buffer, i, b_type.Length);
            i += b_type.Length;
            Array.Copy(b_code, 0, buffer, i, b_code.Length);
            i += b_code.Length;
            Array.Copy(b_cksum, 0, buffer, i, b_cksum.Length);
            i += b_cksum.Length;
            Array.Copy(b_id, 0, buffer, i, b_id.Length);
            i += b_id.Length;
            Array.Copy(b_seq, 0, buffer, i, b_seq.Length);
            i += b_seq.Length;
            Array.Copy(_data, 0, buffer, i, _data.Length);
            i += _data.Length;
            return i;
        }
        //将整个ICMP报文信息和数据转化为Byte数据包 
        public static UInt16 SumOfCheck(UInt16[] buffer)
        {
            int cksum = 0;
            for (int i = 0; i < buffer.Length; i++)
                cksum += (int)buffer[i];
            cksum = (cksum >> 16) + (cksum & 0xffff);
            cksum += (cksum >> 16);
            return (UInt16)(~cksum);
        }

    }


}
