using Feng.Data;
using System;
using System.ComponentModel;

namespace Feng.IO
{
    public class FileBuffer
    {
        private string _DirectoryName = string.Empty;
        public virtual string DirectoryName { get { return this._DirectoryName; } set { this._DirectoryName = value; } }

        private bool _IsReadOnly = false;
        public virtual bool IsReadOnly
        { get { return this._IsReadOnly; } set { this._IsReadOnly = value; } }

        private long _Length = 0;
        public virtual long Length { get { return this._Length; } set { this._Length = value; } }

        private string _Name = string.Empty;
        public virtual string Name { get { return this._Name; } set { this._Name = value; } }

        private DateTime _CreationTime = DateTime.MinValue;
        public virtual DateTime CreationTime { get { return this._CreationTime; } set { this._CreationTime = value; } }

        private string _Extension = string.Empty;
        public virtual string Extension { get { return this._Extension; } set { this._Extension = value; } }

        private string _FullName = string.Empty;
        public virtual string FullName { get { return this._FullName; } set { this._FullName = value; } }

        private DateTime _LastAccessTime = DateTime.MinValue;
        public virtual DateTime LastAccessTime { get { return this._LastAccessTime; } set { this._LastAccessTime = value; } }

        private DateTime _LastWriteTime = DateTime.MinValue;
        public virtual DateTime LastWriteTime { get { return this._LastWriteTime; } set { this._LastWriteTime = value; } }

        private byte[] _Buffer = null;
        public virtual byte[] Buffer { get { return this._Buffer; } set { this._Buffer = value; } }

        public virtual void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new BufferReader(data.Data))
            { 
                this._Buffer = reader.ReadIndex(1, this._Buffer);
                this._CreationTime = reader.ReadIndex(2, this._CreationTime);
                this._DirectoryName = reader.ReadIndex(3, this._DirectoryName);
                this._Extension = reader.ReadIndex(4, this._Extension);
                this._FullName = reader.ReadIndex(5, this._FullName);
                this._IsReadOnly = reader.ReadIndex(6, this._IsReadOnly);
                this._LastAccessTime = reader.ReadIndex(7, this._LastAccessTime);
                this._LastWriteTime = reader.ReadIndex(8, this._LastWriteTime);
                this._Length = reader.ReadIndex(9, this._Length);
                this._Name = reader.ReadIndex(10, this._Name);
            }
        }

        [Browsable(false)]
        public virtual DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = string.Empty,
                    Version = string.Empty,
                    AessemlyDownLoadUrl = string.Empty,
                    FullName = t.FullName,
                    Name = t.Name,
                };

                using (Feng.IO.BufferWriter bw = new BufferWriter())
                {
                    bw.Write(1, this._Buffer);
                    bw.Write(2, this._CreationTime);
                    bw.Write(3, this._DirectoryName);
                    bw.Write(4, this._Extension);
                    bw.Write(5, this._FullName);
                    bw.Write(6, this._IsReadOnly);
                    bw.Write(7, this._LastAccessTime);
                    bw.Write(8, this._LastWriteTime);
                    bw.Write(9, this._Length);
                    bw.Write(10, this._Name);
 
                    data.Data = bw.GetData();
                }
                return data;
            }
        }
    }
}
