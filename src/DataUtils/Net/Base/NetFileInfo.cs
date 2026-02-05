using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

using System.IO;
namespace Feng.Net.Tcp
{ 
    [Serializable]
    public sealed class NetFileInfo  
    {    
        public bool IsReadOnly { get; set; }

        public long Length { get; set; } 
 
        public DateTime CreationTime { get; set; }

        public string Extension { get; set; }

        public string FullName { get; set; }
 
        public DateTime LastAccessTime { get; set; }
   
        public DateTime LastWriteTime { get; set; }

        public string Name { get; set; }

        public static NetFileInfo Get(byte[] data)
        {
            if (data.Length < 1)
                return null;
            NetFileInfo tfi = new NetFileInfo();
            using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(data))
            {
                tfi.CreationTime = br.ReadDateTime();
                tfi.Extension = br.ReadString();
                tfi.FullName = br.ReadString();
                tfi.IsReadOnly = br.ReadBoolean();
                tfi.LastAccessTime = br.ReadDateTime();
                tfi.LastWriteTime = br.ReadDateTime();
                tfi.Length = br.ReadInt64();
                tfi.Name = br.ReadString();
            }
            return tfi;
        }

        public byte[] ToData()
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(this.CreationTime);
                bw.Write(this.Extension);
                bw.Write(this.FullName);
                bw.Write(this.IsReadOnly);
                bw.Write(this.LastAccessTime);
                bw.Write(this.LastWriteTime);
                bw.Write(this.Length);
                bw.Write(this.Name);
                return bw.GetData();

            }
        }
    }
}