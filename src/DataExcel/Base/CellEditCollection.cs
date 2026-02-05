using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellEditCollection : IList<ICellEditControl>
    {
        private Dictionary<int, ICellEditControl> _dics = new Dictionary<int, ICellEditControl>();
        private List<ICellEditControl> list = new List<ICellEditControl>();

        public int IndexOf(ICellEditControl item)
        {
            return list.IndexOf(item);
        }

        public ICellEditControl GetItemByID(int id)
        { 
            if (this._dics.ContainsKey(id))
            {
                return this._dics[id];
            }
            return null;
        }

        public void Insert(int index, ICellEditControl item)
        {
            list.Insert(index, item);
            this._dics.Add(item.AddressID, item);
        }

        public void RemoveAt(int index)
        {
            ICellEditControl item = this[index];
            list.RemoveAt(index);
            if (item != null)
            {
                this._dics.Remove(item.AddressID);
            }
        }

        public ICellEditControl this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                if (value != null)
                {
                    list[index] = value;
                    this._dics.Add(value.AddressID, value);
                }
            }
        }

        public void Add(ICellEditControl item)
        {
            if (item != null)
            {
                if (!this._dics.ContainsKey(item.AddressID))
                {
                    this.list.Add(item);
                    this._dics.Add(item.AddressID, item);
                }
            }
        }

        public void Clear()
        {
            this.list.Clear();
            this._dics.Clear();
        }

        public virtual bool Contains(ICellEditControl item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(ICellEditControl[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(ICellEditControl item)
        {
            this._dics.Remove(item.AddressID);
            return list.Remove(item);
        }

        public IEnumerator<ICellEditControl> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
