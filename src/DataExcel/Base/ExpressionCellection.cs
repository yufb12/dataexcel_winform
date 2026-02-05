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
    public class ExpressionCellection : IList<ICell>
    {
        private List<ICell> _list = new List<ICell>();

        public ExpressionCellection()
        {
        }

        public int IndexOf(ICell item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, ICell item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.Remove(_list[index]);
        }

        public void Add(ICell item)
        {
            if (this._list.Contains(item))
                return;
            _list.Add(item);
        }

        public void Clear()
        {
            this._list.Clear();
        }

        public virtual bool Contains(ICell item)
        {
            return this._list.Contains(item);
        }

        public void CopyTo(ICell[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return true; }
        }

        public virtual bool Remove(ICell item)
        {
            return this._list.Remove(item);
        }

        public IEnumerator<ICell> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        public ICell this[int index]
        {
            get
            {
                return this._list[index];
            }
            set
            {
                this._list[index] = value;
            }
        }

        public ICell this[string key]
        {
            get
            {
                foreach (ICell icell in this._list)
                {
                    if (icell.FieldName == key)
                    {
                        return icell;
                    }
                }
                return null;
            }
            set
            {
                if (!this._list.Contains(value))
                {
                    this._list.Add(value);
                }
            }
        }

        public void AddRange(params ICell[] ts)
        {
            foreach (ICell c in ts)
            {
                this.Add(c);
            }
        }

        public void Sort()
        {
            this._list.Sort(Compare);
        }

        public int Compare(ICell a, ICell b)
        {
            try
            {
                if (a.ExpressionIndex == 100 && b.ExpressionIndex==100)
                {
                    if (a.Row.Index > b.Row.Index)
                    {
                        return 1;
                    }

                    if (a.Row.Index < b.Row.Index)
                    {
                        return -1;
                    }
                    if (a.Row.Index == b.Row.Index)
                    {
                        if (a.Column.Index > b.Column.Index)
                        {
                            return 1;
                        }
                        if (a.Column.Index < b.Column.Index)
                        {
                            return -1;
                        }
                    }
                    return 0;
                }
                if (a.ExpressionIndex < b.ExpressionIndex)
                {
                    return -1;
                }

                if (a.ExpressionIndex > b.ExpressionIndex)
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            { 
            }
            return 0;
        }

        public void Refresh()
        {
            for (int i = _list.Count - 1; i >=0; i--)
            {
                ICell cell = _list[i];
                if (string.IsNullOrWhiteSpace(cell.Expression))
                {
                    cell.ExpressionIndex = 0;
                    _list.Remove(cell);
                }
            }
        }
    }


}
