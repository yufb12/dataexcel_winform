using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class ChartGridAxisItemCollection : IList<ChartGridAxisItem>
    {

        private List<ChartGridAxisItem> list = new List<ChartGridAxisItem>();
        public int IndexOf(ChartGridAxisItem item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, ChartGridAxisItem item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public ChartGridAxisItem this[int index]
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

        public void Add(ChartGridAxisItem item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(ChartGridAxisItem item)
        {
            return list.Contains(item);
        }

        public void CopyTo(ChartGridAxisItem[] array, int arrayIndex)
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

        public bool Remove(ChartGridAxisItem item)
        {
            return list.Remove(item);
        }

        public IEnumerator<ChartGridAxisItem> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

}
