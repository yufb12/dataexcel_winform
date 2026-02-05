using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class ChartSerieCollection : IList<ChartSerie>
    {
        public ChartSerieCollection()
        {

        }

        private List<ChartSerie> list = new List<ChartSerie>();
        public int IndexOf(ChartSerie item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, ChartSerie item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public ChartSerie this[int index]
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

        public void Add(ChartSerie item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(ChartSerie item)
        {
            return list.Contains(item);
        }

        public void CopyTo(ChartSerie[] array, int arrayIndex)
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

        public bool Remove(ChartSerie item)
        {
            return list.Remove(item);
        }

        public IEnumerator<ChartSerie> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
 
}
