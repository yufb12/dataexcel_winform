using System;
using System.Collections.Generic;
using System.Drawing;

namespace Feng.Forms.Views
{
    public class ViewCollection:IList<DivView>
    {
        public ViewCollection()
        {

        }

        private List<DivView> list = new List<DivView>();
        public int IndexOf(DivView item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, DivView item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public DivView this[int index]
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

        public void Add(DivView item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(DivView item)
        {
            return list.Contains(item);
        }

        public void CopyTo(DivView[] array, int arrayIndex)
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

        public bool Remove(DivView item)
        {
            return list.Remove(item);
        }

        public IEnumerator<DivView> GetEnumerator()
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
        public int Compare(DivView a, DivView b)
        {
            if (a.Zlevel > b.Zlevel)
                return 1;
            if (a.Zlevel < b.Zlevel)
                return -1;
            return 0;
        }
        public void Sort(Comparison<DivView> comparison)
        {
            this.list.Sort(comparison);
        }
    }
}

