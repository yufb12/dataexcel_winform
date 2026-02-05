#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Feng.Utils;
using Feng.Excel.Interfaces;
using Feng.Excel.Drawing;
using Feng.Drawing;
using System.Collections;

namespace Feng.Excel.Collections
{
    public class SelectRangeCollection : IList<ICell>
    {
        public SelectRangeCollection()
        {
            
        }
        private List<ICell> list = new List<ICell>();
        public ICell this[int index]
        {
            get {
                return list[index];
            }
            set { list[index] = value; }
        }

        public int Count { get { return list.Count; } }

        public bool IsReadOnly { get { return false; } }

        public void Add(ICell item)
        {
            if (item == null)
            {
                return;
            }
            if (this.list.Contains(item))
            {
                return;
            }
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public ICell[] ToArray()
        {
            return this.list.ToArray();
        }

        public bool Contains(ICell item)
        {
          return   this.list.Contains(item);
        }

        public void CopyTo(ICell[] array, int arrayIndex)
        {
            this.list.CopyTo(  array, arrayIndex);
        }

        public IEnumerator<ICell> GetEnumerator()
        {
          return   this.list.GetEnumerator();
        }

        public int IndexOf(ICell item)
        {
           return  this.list.IndexOf(item);
        }

        public void Insert(int index, ICell item)
        {
            this.list.Insert(index, item);
        }

        public bool Remove(ICell item)
        {
          return   this.list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
    }




}
