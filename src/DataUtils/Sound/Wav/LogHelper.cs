using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Feng.Sound
{
    public class RIFF
    {
        public virtual string ID { get; set; }
        public virtual int Length { get; set; }
    }

    public class Wav : RIFF 
    {
        public override string ID
        {
            get
            {
                return "wav ";
            }
            set
            { 
            }
        }
        
        private string file = string.Empty;
        public void Read()
        {
            Read(file);
        }

        public void Read(string file)
        {
            Read(System.IO.File.ReadAllBytes(file));
        }

        public void Read(byte[] data)
        {
            using (Feng.IO.BufferReader reader = new IO.BufferReader(data))
            {
                Read(reader);
            }
        }

        public void Read(Feng.IO.BufferReader reader)
        {
            string id = reader.ReadASCII4();
            int filelen = reader.ReadInt();
            string wave = reader.ReadASCII4();
       
            while (!reader.IsEnd())
            {
                string type = reader.ReadASCII4();
                switch (type)
                {
                    case "data":
                        DataChunk.Read(reader);
                        break;
                    case "fmt ":
                        FormatChunk.Read(reader);
                        break;
                    default:
                        int len = reader.ReadInt32();
                        reader.ReadBytes(len);
                        break;
                }
            }
  
        }

        public void Save()
        {

        }

    }
    public class DataChunk
    {
        public static void Read(Feng.IO.BufferReader reader)
        { 
            int chunksize = reader.ReadInt();
            byte[] data = reader.ReadBytes(chunksize);
        }
    }
    public class FormatChunk
    {
        public static void Read(Feng.IO.BufferReader read)
        {
            int len = read.ReadInt32();
            byte[] data = read.ReadBytes(len);
            using (Feng.IO.BufferReader reader = new IO.BufferReader(data))
            {
                ///压缩方式
                short pch = reader.ReadInt16();
                ///声音通道
                short channelnum = reader.ReadInt16();
                ///取样 频率
                int sampling = reader.ReadInt();
                ///播放字节数
                int speed = reader.ReadInt();
                ///采样框架
                short adjustment = reader.ReadInt16();
                ///比特率
                short databits = reader.ReadInt16();
            }
        }
    }
}
