using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Feng.IO
{
    public interface ISaveable
    {
        void Write(Feng.IO.BufferWriter br);
        void Read(Feng.IO.BufferReader reader);
    }
    public class SerializableHelper
    {
        public static void SaveObject(ISaveable obj, string filename)
        {
            using (FileStream fs = System.IO.File.Open(filename, FileMode.Create))
            {
                using (Feng.IO.BufferWriter bw = new BufferWriter(fs))
                {
                    obj.Write(bw);
                } 
            }
        }
        public static void ReadObject(ISaveable obj, string filename)
        {
            using (FileStream fs = System.IO.File.Open(filename, FileMode.Open))
            {
                using (Feng.IO.BufferReader reader = new BufferReader(fs))
                {
                    obj.Read(reader);
                }  
            }
        }

        public static void ReadObject(ISaveable obj, byte[] data)
        { 
            using (Feng.IO.BufferReader reader = new BufferReader(data))
            {
                obj.Read(reader);
            }
        }
        public static byte[] GetDataFromObject(ISaveable obj)
        {
            using (Feng.IO.BufferWriter br = new BufferWriter())
            {
                obj.Write(br);
                return br.GetData();
            }
        }
        public static void SerializToFile(object obj, string path)
        {
            using (FileStream fs = System.IO.File.Create(path))
            {
                BinaryFormatter __bf = new BinaryFormatter();
                __bf.Serialize(fs, obj);
                fs.Close();
            }
        }

        public static object DeSerializFromFile(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                BinaryFormatter __bf = new BinaryFormatter();
                object result = __bf.Deserialize(fs);
                fs.Close();
                return result;
            }

        }

        public static System.IO.MemoryStream SerializeToMemoryStream(object obj)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            serializer.Serialize(memStream, obj);
            return memStream;
        }

        public static object DeSerializeFromMemoryStream(System.IO.MemoryStream memStream)
        {
            memStream.Position = 0;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            object newobj = deserializer.Deserialize(memStream);

            memStream.Close();
            return newobj;
        }

        public static object DeSerializeFromBinary(byte[] data)
        {
            using (System.IO.MemoryStream v_memStream = new MemoryStream(data))
            {
                v_memStream.Position = 0;
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                object newobj = deserializer.Deserialize(v_memStream);
                v_memStream.Close();
                return newobj;
            }
        }

        public static byte[] SerializeToBinary(object obj)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (System.IO.MemoryStream memStream = new System.IO.MemoryStream())
            {
                serializer.Serialize(memStream, obj);
                return memStream.ToArray();
            }
        }
 
        public static byte[] DataSetToBinary(System.Data.DataSet dataset)
        {
            using (Feng.IO.BufferWriter bw = new BufferWriter())
            {
                bw.Write(dataset);
                return bw.GetData();
            }
        }

        public static byte[] DataTableToBinary(System.Data.DataTable table)
        {
            using (Feng.IO.BufferWriter bw = new BufferWriter())
            {
                bw.Write(table);
                return bw.GetData();
            } 
        }
 
        public static System.Data.DataSet DataSetFromBinary(byte[] data)
        {
            using (Feng.IO.BufferReader br = new BufferReader(data))
            { 
                return br.ReadDataSet();
            }
        }

        public static System.Data.DataTable DataTableFromBinary(byte[] data)
        {
            using (Feng.IO.BufferReader br = new BufferReader(data))
            {
                return br.ReadDataTable();
            }
        }
    }
}
