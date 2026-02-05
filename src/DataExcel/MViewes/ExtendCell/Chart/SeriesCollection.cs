using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace Feng.Excel.Chart
{

 
    [Serializable]
    public class SeriesCollection : ISeriesCollection
    {
        public SeriesCollection(IDataExcelChart chart)
        {
            _Chart = chart;
        }
        private List<ISeries> list = new List<ISeries>();

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            foreach (ISeries cl in list)
            {
                cl.OnDraw(this, g);
            }
            return false;
        }

        public int IndexOf(ISeries item)
        {
            return list.IndexOf(item);
        }
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        public void Insert(int index, ISeries item)
        {
            this.list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.Remove(list[index]);
        }

        public ISeries this[int index]
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

        public void Add(ISeries item)
        {

            list.Add(item);

        }

        public void Clear()
        {
            this.list.Clear();
        }

        public virtual bool Contains(ISeries item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(ISeries[] array, int arrayIndex)
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

        public virtual bool Remove(ISeries item)
        {
            return this.list.Remove(item);
        }

        public IEnumerator<ISeries> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public void AddRange(params ISeries[] ts)
        {
            foreach (ISeries c in ts)
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
    }
}
