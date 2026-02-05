using Feng.Data;
using System.Drawing;


namespace Feng.IO
{
    public static class BinaryReaderTool   
    {
        public static DataStruct ReadDataStruct(byte[] data)
        {
            DataStruct datastruct = new DataStruct();
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data))
            {
                datastruct.Name = reader.ReadString();
                datastruct.Version = reader.ReadString();
                datastruct.FullName = reader.ReadString();
                datastruct.AessemlyDownLoadUrl = reader.ReadString();
                datastruct.DllName = reader.ReadString();
                datastruct.Data = reader.ReadBytes();
                return datastruct;
            }
        }
        public static DataStructCollection ReadDataStructs(byte[] data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data))
            {
                DataStructCollection list = new DataStructCollection();

                int count = reader.ReadInt();
                for (int i = 0; i < count; i++)
                {
                    DataStruct ds = reader.ReadDataStruct();
                    list.Add(ds);
                }

                return list;
            } 
        }
        public static Font ReadFont(byte[] data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data))
            { 
                return reader.ReadFont();
            }
        }
    }
}
