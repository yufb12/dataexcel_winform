using System.Collections.Generic;

namespace Feng.IO.File
{
    public class LastFiles  
    {
        private System.Collections.Generic.List<string> _list = new List<string>();

        public LastFiles()
        {

        }

        public void Add(string item)
        {
            if (this._list.Contains(item))
            {
                this._list.Remove(item);
            }
            this._list.Add(item);
        }

        public string this[int i]
        {
            get {
                return this._list[i];
            }
        }

        public void Clear()
        {
            this._list.Clear();
        }

        public bool Contains(string item)
        {
            return this._list.Contains(item);
        } 

        public int Count
        {
            get { return this._list.Count; }
        }

        public bool Remove(string item)
        {
            return this._list.Remove(item);
        }

        public void Load(string file)
        {
            if (!System.IO.File.Exists(file))
                return;
            using (System.IO.FileStream fs = System.IO.File.OpenRead(file))
            {
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(fs))
                {
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        this._list.Add(br.ReadString());
                    }
                }
            }
        }

        public void Save(string file)
        {
            using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.OpenOrCreate))
            {
                using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter(fs))
                {
                    br.Write(this._list.Count);
                    for (int i = 0; i < this._list.Count; i++)
                    {
                        br.Write(this._list[i]);
                    }
                }
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }
    } 
}
