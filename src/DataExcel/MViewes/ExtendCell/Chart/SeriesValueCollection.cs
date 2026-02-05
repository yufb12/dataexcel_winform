using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace Feng.Excel.Chart
{

 
    [Serializable]
    public class SeriesValueCollection : ISeriesValueCollection
    {
        public SeriesValueCollection(ISeries ownerSeries)
        {
            _ownerSeries = ownerSeries;
        }
        private List<ISeriesValue> list = new List<ISeriesValue>();

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            foreach (ISeriesValue cl in list)
            {
                cl.OnDraw(this, g);
            }
            return false;
        }

        public int IndexOf(ISeriesValue item)
        {
            return list.IndexOf(item);
        }
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        public void Insert(int index, ISeriesValue item)
        {
            this.list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.Remove(list[index]);
        }

        public ISeriesValue this[int index]
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

        public void Add(ISeriesValue item)
        {

            list.Add(item);

        }

        public void Clear()
        {
            this.list.Clear();
        }

        public virtual bool Contains(ISeriesValue item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(ISeriesValue[] array, int arrayIndex)
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

        public virtual bool Remove(ISeriesValue item)
        {
            return this.list.Remove(item);
        }

        public IEnumerator<ISeriesValue> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public void AddRange(params ISeriesValue[] ts)
        {
            foreach (ISeriesValue c in ts)
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
        public virtual IDataExcelChart Chart
        {
            get
            {
                return this._ownerSeries.Chart;
            }
        }

        #endregion

        #region IOwnerSeries 成员
        private ISeries _ownerSeries = null;
        public ISeries OwnerSeries
        {
            get
            {
                return _ownerSeries;
            }
            set
            {
                _ownerSeries = value;
            }
        }

        #endregion
    }
}
