using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementCollection : IList<StatementBase>
    {
        private List<StatementBase> list = new List<StatementBase>();
        public int IndexOf(StatementBase item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, StatementBase item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public StatementBase this[int index]
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

        public void Add(StatementBase item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(StatementBase item)
        {
            return list.Contains(item);
        }

        public void CopyTo(StatementBase[] array, int arrayIndex)
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

        public bool Remove(StatementBase item)
        {
            return list.Remove(item);
        }

        public IEnumerator<StatementBase> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public StatementBase Next(StatementBase item)
        {
            if (item.Index < this.Count-1)
            {
                return this[item.Index + 1];
            }
            return null;
        }
        public int Index { get; set; }
        public StatementBase Peek()
        {
            if (Index < this.Count - 1)
            {
                return this[Index + 1];
            }
            return null;
        }
        public StatementBase Current()
        {
            if (Index < this.Count)
            {
                return this[Index];
            }
            return null;
        }
        public StatementBase Pop()
        {
            Index = Index + 1;
            if (Index < this.Count)
            {
                StatementBase statement = this[Index ];
                return statement;
            }
            return null;
        }

    }

}
