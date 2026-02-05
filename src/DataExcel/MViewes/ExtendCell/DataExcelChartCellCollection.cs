using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Extend
{
    [Serializable]
    public class DataExcelChartCellCollection : IDataExcelChartCellCollection
    {
        private List<IExtendCell> list = new List<IExtendCell>();

        public DataExcelChartCellCollection(DataExcel grid)
        {
            _grid = grid;
        }


        #region IList<clsxieyi> 成员

        public int IndexOf(IExtendCell item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, IExtendCell item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public IExtendCell this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                list[index] = value;
            }
        }


        #endregion

        #region ICollection<clsxieyi> 成员

        public void Add(IExtendCell item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public virtual bool Contains(IExtendCell item)
        {
            return list.Contains(item);
        }

        public void CopyTo(IExtendCell[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(IExtendCell item)
        {
            return this.list.Remove(item);
        }

        #endregion

        #region IEnumerable<clsxieyi> 成员

        public IEnumerator<IExtendCell> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region ICol<clsxieyi> 成员
        public void AddRange(params IExtendCell[] ts)
        {
            foreach (IExtendCell c in ts)
            {
                this.Add(c);
            }
        }

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return _grid; }
        }

        #endregion

        #region IDraw 成员

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            foreach (IExtendCell mc in list)
            {
                mc.OnDraw(this, g);
            }
            return false;
        }

        #endregion

        #region ISetSize 成员

        public void Refresh()
        {
            foreach (IImageCell mc in list)
            {
                mc.FreshLocation();
            }
        }

        #endregion
    }
}
