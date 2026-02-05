#define TestValid

using System;

using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Base
{
    [Serializable]
    public class CellCollection : ICellCollection 
    {
        private Dictionary<IColumn, ICell> list = new Dictionary<IColumn, ICell>();

        private IRow _row = null;

        public IRow Row
        {
            get { return this._row; }

            set { this._row = value; }
        }

        public CellCollection(IRow row)
        {
            this._row = row;
        }

        public void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            foreach (ICell cl in list.Values)
            {
                cl.OnDraw(this, g);
            }
        }

        public int IndexOf(ICell item)
        {
            return (item.Column.Index);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._row.Grid; }
        }

        public void Insert(int index, ICell item)
        {
#warning insert
            throw new Exception();
        }

        public void RemoveAt(int index)
        {
            IColumn column = this.Grid.Columns[index];
            if (column != null)
            {
                if (list.ContainsKey(column))
                {
                    list.Remove(column);
                }
            }
        }
 
        public ICell this[int index]
        {
            get
            { 

                IColumn column = this.Grid.Columns[index];
                if (column != null)
                {
                    if (list.ContainsKey(column))
                    {
                        ICell cell = list[column];
                        return cell;
                    }
                }

                return null;
            }
            set
            {
                IColumn column = this.Grid.Columns[index];
                if (column != null)
                {
                    if (list.ContainsKey(column))
                    {
                        this.list[column] = value;
                    }
                    else
                    {
                        this.list.Add(column, value);
                    }
                }
            }
        }

        public ICell this[IColumn column]
        {
            get
            {

                if (list.ContainsKey(column))
                {
                    return list[column];
                }
                return null;
            }

            set
            {
                if (list.ContainsKey(column))
                {
                    this.list[column] = value;
                }
                else
                {
                    this.list.Add(column, value);
                }
 
            }
        }

        public void Add(ICell item)
        {
#if DEBUG
            try
            {
#endif 
                if (!list.ContainsKey(item.Column))
                {
                    list.Add(item.Column, item);
                } 
#if DEBUG
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("CellCollection", "Add", "Add", ex);
            }
#endif
        }

        public ICell Add(int rowindex, int columnindex)
        {
            ICell cell = this.Grid.ClassFactory.CreateDefaultCell(this.Grid, rowindex, columnindex);
            return cell;
        }

        public void Clear()
        {
            foreach (KeyValuePair<IColumn, ICell> key in list)
            {
                key.Value.Clear();
            }
            this.list.Clear();
        }

        public virtual bool Contains(ICell item)
        {
            return this.list.ContainsKey(item.Column);
        }

        public void CopyTo(ICell[] array, int arrayIndex)
        {
            list.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return true; }
        }

        public virtual bool Remove(ICell item)
        {
            return this.list.Remove(item.Column);
        }

        public IEnumerator<ICell> GetEnumerator()
        {
            return this.list.Values.GetEnumerator();
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
            return this.list.Values.GetEnumerator();
        }

        #endregion
    }
}
