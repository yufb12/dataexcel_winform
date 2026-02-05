using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Collections
{
    [Serializable]
    public class FunctionCell : IFunctionCell
    {
        public FunctionCell(DataExcel grid)
        {
            this._grid = grid;
            Init();
        }

        private List<ICell> _list = null;

        private void Init()
        {
            if (_list == null)
                _list = new List<ICell>();
        }
        public void Add(ICell cell)
        {
            _list.Add(cell);
        }
        public void Remove(ICell cell)
        {
            if (this._list.Contains(cell))
            {
                this._list.Remove(cell);
            }
        }

        public void Clear()
        {
            foreach (ICell key in this._list)
            {
                key.Clear();
            }
            this._list.Clear();
        }

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return _grid; }
        }

        #endregion

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        #endregion

        #region IFunctionCell 成员

        public virtual bool Contains(ICell cell)
        {
            return this._list.Contains(cell);
        }

        #endregion
 
 
    }
}
