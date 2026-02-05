using System.Collections;
using System.Collections.Generic;
namespace Feng.Data
{
    public class DataStruct
    {
        public readonly static DataStruct DataStructNull = null;
        public DataStruct()
        {
            ReadValue = true;
        }
        public string Version { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string AessemlyDownLoadUrl { get; set; }
        public string DllName { get; set; }
        public byte[] Data { get; set; }
        public bool ReadValue { get; set; }
        public byte[] ToData()
        {
            using (Feng.IO.BufferWriter bufferWriter = new IO.BufferWriter())
            {
                bufferWriter.Write(this);
                return bufferWriter.GetData();
            }

        }
        public static DataStruct ReadData(byte [] buffer)
        {
            using (Feng.IO.BufferReader reader = new IO.BufferReader(buffer))
            {
                return reader.ReadDataStruct();
            }
        }
    }

    public class DataStructCollection : IList<DataStruct>
    {
        private List<DataStruct> list = new List<DataStruct>();
        public int IndexOf(DataStruct item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, DataStruct item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public DataStruct this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(DataStruct item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(DataStruct item)
        {
            return list.Contains(item);
        }

        public void CopyTo(DataStruct[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DataStruct item)
        {
            return list.Remove(item);
        }

        public IEnumerator<DataStruct> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

}
