using System.Collections.Generic;

namespace Feng.IO
{
    public class FileInfo
    {
        public string File { get; set; }
        public byte[] Data { get; set; }
    }

    public class FileInfoCollection : IList<FileInfo>
    {
        public List<FileInfo> list = new List<FileInfo>();
        public int IndexOf(FileInfo item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, FileInfo item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public FileInfo this[int index]
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

        public void Add(FileInfo item)
        {
              list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(FileInfo item)
        {
            return list.Contains(item);
        }

        public void CopyTo(FileInfo[] array, int arrayIndex)
        {
              list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(FileInfo item)
        {
            return list.Remove(item);
        }

        public IEnumerator<FileInfo> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
