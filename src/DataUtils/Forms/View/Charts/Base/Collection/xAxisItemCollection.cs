using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class AxisItemCollection : IList<ViewItem>
    {

        private List<ViewItem> list = new List<ViewItem>();
        public int IndexOf(ViewItem item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, ViewItem item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public ViewItem this[int index]
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

        public void Add(ViewItem item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(ViewItem item)
        {
            return list.Contains(item);
        }

        public void CopyTo(ViewItem[] array, int arrayIndex)
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

        public bool Remove(ViewItem item)
        {
            return list.Remove(item);
        }

        public IEnumerator<ViewItem> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

}
