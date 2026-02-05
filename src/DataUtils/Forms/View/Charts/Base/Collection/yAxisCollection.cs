using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class yAxisCollection : IList<yAxisBase>
    {

        private List<yAxisBase> list = new List<yAxisBase>();
        public int IndexOf(yAxisBase item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, yAxisBase item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public yAxisBase this[int index]
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

        public void Add(yAxisBase item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(yAxisBase item)
        {
            return list.Contains(item);
        }

        public void CopyTo(yAxisBase[] array, int arrayIndex)
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

        public bool Remove(yAxisBase item)
        {
            return list.Remove(item);
        }

        public IEnumerator<yAxisBase> GetEnumerator()
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

        public virtual int Compare(yAxisBase itema, yAxisBase itemb)
        {
            if (itema.Index > itemb.Index)
                return 1;
            if (itema.Index < itemb.Index)
                return -1;
            return 0;

        }
    }

}
