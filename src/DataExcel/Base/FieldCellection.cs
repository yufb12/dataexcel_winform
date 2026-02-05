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
    public class FieldCellection : IList<ICell>
    {
        private List<ICell> _list = new List<ICell>();

        public FieldCellection()
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
         
    }


}
