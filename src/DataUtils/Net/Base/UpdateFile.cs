using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.App
{
    public class UpdateFileData
    {
        public int Index { get; set; }
        public string FileName { get; set; }
        public int Position { get; set; }
        public byte[] Data { get; set; }
        public int FileLength { get; set; }
        public bool IsEnd { get; set; }
        public int Length {
            get {
                return this.Data.Length; 
            }
        }
        public void Write(Feng.IO.BufferWriter bw)
        {
            bw.Write(0, true);
            bw.Write(1, this.Index);
            bw.Write(2, this.FileName);
            bw.Write(3, this.Position);
            bw.Write(4, this.Data);
            bw.Write(5, this.IsEnd);
            bw.Write(0, false);

        }
        public void Read(Feng.IO.BufferReader reader)
        {
            int res = reader.ReadIndex(0, 1);
            this.Index = reader.ReadIndex(1, this.Index);
            this.FileName = reader.ReadIndex(2, this.FileName);
            this.Position = reader.ReadIndex(3, this.Position);
            this.Data = reader.ReadIndex(4, new byte[] { });
            this.IsEnd = reader.ReadIndex(5, false);
            res = reader.ReadIndex(0, -1); 
        }
    }
     
    public class UpdateFile 
    {
        public int Index { get; set; }
        public string Path { get; set; }  
        public string FileName { get; set; }  
        public int FileLength { get; set; }
        public int Version { get; set; }
        public void Write(Feng.IO.BufferWriter bw)
        {
            bw.Write(0, true);
            bw.Write(1, this.Index);
            bw.Write(2, this.FileName);
            bw.Write(3, this.Version);
            bw.Write(4, this.Path); 
            bw.Write(0, false);

        }

        public void Read(Feng.IO.BufferReader reader)
        {
            int res = reader.ReadIndex(0, 1);
            this.Index = reader.ReadIndex(1, this.Index);
            this.FileName = reader.ReadIndex(2, this.FileName);
            this.Version = reader.ReadIndex(3, this.Version);
            this.Path = reader.ReadIndex(4, this.Path); 
            res = reader.ReadIndex(0, -1);
        }
    }
    public class UpdateFileCollection : IList<UpdateFile>
    {
        public void Write(Feng.IO.BufferWriter bw)
        {
            bw.Write(this.Count);
            foreach (UpdateFile model in this.list)
            {
                model.Write(bw);
            }
        }

        public void Read(Feng.IO.BufferReader reader)
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                UpdateFile model = new UpdateFile();
                this.Add(model);
                model.Read(reader);
            }
        }
        #region IList<UpdateFile> 成员
        List<UpdateFile> list = new List<UpdateFile>();
        public int IndexOf(UpdateFile item)
        {
            lock (this)
            {
                return list.IndexOf(item);
            }
        }

        public void Insert(int index, UpdateFile item)
        {
            lock (this)
            {
                if (list.Contains(item))
                {
                    list.Remove(item);
                }
                list.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (this)
            {
                list.RemoveAt(index);
            }
        }

        public UpdateFile this[int index]
        {
            get
            {
                lock (this)
                {
                    return list[index];
                }
            }
            set
            {
                lock (this)
                {
                    this[index] = value;
                }
            }
        }

        #endregion

        #region ICollection<UpdateFile> 成员

        public void Add(UpdateFile item)
        {
            lock (this)
            {
                list.Add(item);
            }
        }

        public void Clear()
        {
            lock (this)
            {
                list.Clear();
            }
        }

        public bool Contains(UpdateFile item)
        {
            lock (this)
            {
                return list.Contains(item);
            }
        }

        public void CopyTo(UpdateFile[] array, int arrayIndex)
        {
            lock (this)
            {
                list.CopyTo(array, arrayIndex);
            }
        }

        public int Count
        {
            get
            {
                lock (this)
                {
                    return list.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                lock (this)
                {
                    return false;
                }
            }
        }

        public bool Remove(UpdateFile item)
        {
            lock (this)
            {
                return list.Remove(item);
            }
        }

        #endregion

        #region IEnumerable<UpdateFile> 成员

        public IEnumerator<UpdateFile> GetEnumerator()
        {
            lock (this)
            {
                return list.GetEnumerator();
            }
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            lock (this)
            {
                return list.GetEnumerator();
            }
        }

        #endregion
    }
}
