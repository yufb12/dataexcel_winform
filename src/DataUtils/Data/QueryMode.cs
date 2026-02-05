using System;
using System.Data;
namespace Feng.Data
{ 
    public class DataConnection : Feng.IO.ISaveable
    {
        public string Title { get; set; }

        public string IpAddress { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string DataBase { get; set; }

        public string Port   { get; set; }
        public void Read(Feng.IO.BufferReader reader)
        {
            IpAddress = reader.ReadString();
            User = reader.ReadString();
            Password = reader.ReadString();
            DataBase = reader.ReadString();
            Port = reader.ReadString();
        }
        public void Write(Feng.IO.BufferWriter br)
        {
            br.Write(IpAddress);
            br.Write(User);
            br.Write(Password);
            br.Write(DataBase);
            br.Write(Port);
        }

        public void Read(byte [] data)
        {
            using (Feng.IO.BufferReader reader = new IO.BufferReader(data))
            {
                IpAddress = reader.ReadString();
                User = reader.ReadString();
                Password = reader.ReadString();
                DataBase = reader.ReadString();
                Port = reader.ReadString();
                Title = reader.ReadString();
            }
        }
        public byte[] Write()
        {
            Feng.IO.BufferWriter br = new IO.BufferWriter();
            br.Write(IpAddress);
            br.Write(User);
            br.Write(Password);
            br.Write(DataBase);
            br.Write(Port);
            br.Write(Title);
            return br.GetData();
        }
        
    } 
}
