using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class xAxisCollection : IList<xAxisBase>
    {

        private List<xAxisBase> list = new List<xAxisBase>();
        public int IndexOf(xAxisBase item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, xAxisBase item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public xAxisBase this[int index]
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

        public void Add(xAxisBase item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(xAxisBase item)
        {
            return list.Contains(item);
        }

        public void CopyTo(xAxisBase[] array, int arrayIndex)
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

        public bool Remove(xAxisBase item)
        {
            return list.Remove(item);
        }

        public IEnumerator<xAxisBase> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public void Sort()
        {
            this.list.Sort(Compare);
        }

        public virtual int Compare(xAxisBase itema, xAxisBase itemb)
        {
            if (itema.Index > itemb.Index)
                return 1;
            if (itema.Index < itemb.Index)
                return -1;
            return 0;

        }
    }

}
