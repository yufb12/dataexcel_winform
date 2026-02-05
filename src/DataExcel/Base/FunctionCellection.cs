#define TestValid

using System;

using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Script
{
    [Serializable]
    public class FunctionCellection : IFunctionCellection
    {
        private List<IBaseCell> _list = new List<IBaseCell>();

        public FunctionCellection(DataExcel grid)
        {
            this._grid = grid;
        }

        public int IndexOf(IBaseCell item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, IBaseCell item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.Remove(_list[index]);
        }

        public void Add(IBaseCell item)
        {
            if (this._list.Contains(item))
                return;
            _list.Add(item);
        }

        public void Clear()
        {
            this._list.Clear();
        }

        public virtual bool Contains(IBaseCell item)
        {
            return this._list.Contains(item);
        }

        public void CopyTo(IBaseCell[] array, int arrayIndex)
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

        public virtual bool Remove(IBaseCell item)
        {
            return this._list.Remove(item);
        }

        public IEnumerator<IBaseCell> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        #region IList<ICell> 成员


        public IBaseCell this[int index]
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

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        #endregion

        #region IAddrangle<ICell> 成员


        public void AddRange(params IBaseCell[] ts)
        {
            foreach (IBaseCell c in ts)
            {
                this.Add(c);
            }
        }
        #endregion

        #region IExecuteExpress 成员

        public void ExecuteExpression()
        {
            foreach (IBaseCell cell in this._list)
            {
                cell.ExecuteExpression();
            }
        }

        #endregion
    }


}
