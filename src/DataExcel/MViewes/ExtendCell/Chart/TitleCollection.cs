using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace Feng.Excel.Chart
{ 
    [Serializable]
    public class TitleCollection : ITitleCollection
    {
        public TitleCollection(IDataExcelChart chart)
        {
            _Chart = chart;
        }
        private List<IChartTitle> list = new List<IChartTitle>();

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            foreach (IChartTitle cl in list)
            {
                cl.OnDraw(this, g);
            }
            return false;
        }

        public int IndexOf(IChartTitle item)
        {
            return list.IndexOf(item);
        }
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        public void Insert(int index, IChartTitle item)
        {
            this.list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.Remove(list[index]);
        }

        public IChartTitle this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                this.list[index] = value;

            }
        }

        public void Add(IChartTitle item)
        {

            list.Add(item);

        }

        public void Clear()
        {
            this.list.Clear();
        }

        public virtual bool Contains(IChartTitle item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(IChartTitle[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return true; }
        }

        public virtual bool Remove(IChartTitle item)
        {
            return this.list.Remove(item);
        }

        public IEnumerator<IChartTitle> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public void AddRange(params IChartTitle[] ts)
        {
            foreach (IChartTitle c in ts)
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

        #region IChart 成员
        private IDataExcelChart _Chart = null;
        public virtual IDataExcelChart Chart
        {
            get
            {
                return _Chart;
            }
        }

        #endregion

        #region ITitleCollection 成员

        public int GetWidth()
        {
            if (this.list.Count < 1)
            {
                return 10;
            }
            return 10;
        }

        public int GetHeight()
        {
            if (this.list.Count < 1)
            {
                return 10;
            }
            return 10;
        }

        #endregion
    }
}
