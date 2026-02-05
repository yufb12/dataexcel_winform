using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using Feng.Excel.Interfaces;
namespace Feng.Excel.Collections
{
    public class FocsedCellList : IList<ICell>
    {
        private int index = 0;

        public ICell Header {
            get {
                if (list.Count > 0)
                {
                    return list[0];
                }
                return null;
            }
        }

        public ICell Footer
        {
            get
            {
                if (list.Count > 0)
                {
                    return list[list.Count - 1];
                }
                return null;
            }
        }

        public ICell Prev
        {
            get
            {
                index = index - 1;
                if (index < 1)
                {
                    index = list.Count - 1;
                }
                if (index > (list.Count - 1))
                {
                    index = list.Count - 1;
                }
                if (list.Count > index)
                {
                    return list[index];
                }
                return null;
            }
        }
 
        public ICell Next
        {
            get
            {
                index = index + 1;
                if (index < 1)
                {
                    index = 0;
                }
                if (index > (list.Count - 1))
                {
                    index = list.Count - 1;
                }
                if (list.Count > index)
                {
                    return list[index];
                }
                return null;
            }
        }

        private List<ICell> list = new List<ICell>();
        public int IndexOf(ICell item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, ICell item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public ICell this[int index]
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

        public void Add(ICell item)
        {
            index = 0;
            if (list.Contains(item))
            {
                list.Remove(item);
            }
            list.Add(item);
        }

        public void Clear()
        {
            index = 0;
            list.Clear();
        }

        public bool Contains(ICell item)
        {
            return list.Contains(item);
        }

        public void CopyTo(ICell[] array, int arrayIndex)
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

        public bool Remove(ICell item)
        {
            index = 0;
            return list.Remove(item);
        }

        public IEnumerator<ICell> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}