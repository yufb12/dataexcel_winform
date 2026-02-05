using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Collections
{
    public class ObjectCollection : IList<object>
    {
        private List<object> list = new List<object>();

        public int IndexOf(object item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, object item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public object this[int index]
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

        public void Add(object item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(object item)
        {
            return list.Contains(item);
        }

        public void CopyTo(object[] array, int arrayIndex)
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

        public bool Remove(object item)
        {
            return list.Remove(item);
        }

        public IEnumerator<object> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

}
