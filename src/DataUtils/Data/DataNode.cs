using System;
using System.Collections.Generic;
namespace Feng.Data
{
    public class DataNode : Feng.IO.ISaveable
    {
        public DataNode()
        {

        }
        public DataNode(string name, string id, string text, int leavel)
        {
            Name = name;
            ID = id;
            Text = text;
            Leavel = leavel;
        }
        public string Name
        {
            get;
            set;
        }

        public string ID { get; set; }
        public string Text { get; set; }
        public int Leavel { get; set; }
        public string ParentID { get; set; }

        public DataNode Parent { get; set; }
        private DataNodeCollection _items = null;
        public DataNodeCollection Items
        {
            get
            {
                if (_items == null)
                    _items = new DataNodeCollection();
                return _items;
            }
        }
        public Dictionary<string, DataNode> Dics { get; set; }

        public void Copy(DataNode node)
        {
            this.ID = node.ID;
            this.Leavel = node.Leavel;
            this.Name = node.Name;
            this.Parent = node.Parent;
            this.ParentID = node.ParentID;
            this.Text = node.Text;

        }

        public override string ToString()
        {
            return this.Text;
        }
        public virtual void Read(Feng.IO.BufferReader reader)
        {

        }
        public virtual void Write(Feng.IO.BufferWriter br)
        {
 
        }
    }
    public class DataNodeCollection : IList<DataNode>
    {
        private List<DataNode> list = new List<DataNode>();
        public int IndexOf(DataNode item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, DataNode item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public DataNode this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list[index] = value;
            }
        }

        public void Add(DataNode item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(DataNode item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(DataNode[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DataNode item)
        {
            return this.list.Remove(item);
        }

        public IEnumerator<DataNode> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
    }
}
