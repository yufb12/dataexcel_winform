#define TestValid

using System;

using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Collections
{
    [Serializable]
    public class CellChangedCollection : ICellChangedCollection 
    {
        private List<ICell> list = new List<ICell>();

        public CellChangedCollection(DataExcel grid)
        {
            _grid = grid;
        }
        public int IndexOf(ICell item)
        {
            return list.IndexOf(item);
        }

        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        public ICell this[int i]
        {
            get {
                return this.list[i];
            }
            set
            {
                this.list[i] = value;
            }
        }
        public void Insert(int index, ICell item)
        {
            if (this.list.Contains(item))
            {
                return;
            }
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }
 
        public void Add(ICell item)
        {
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

        public virtual bool Contains(ICell item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(ICell[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(ICell item)
        {
            return this.list.Remove(item);
        }

        public IEnumerator<ICell> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public void AddRange(params ICell[] ts)
        {
            foreach (ICell c in ts)
            {
                this.Add(c);
            }
        }

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion
    }
}
