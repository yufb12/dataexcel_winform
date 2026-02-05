using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;

namespace Feng.Forms.Controls.GridControl
{ 
    public class ColumnCollection : IList<GridViewColumn>
    {
        private GridView _grid = null;
        public GridView Grid { get { return _grid; } }
        private List<GridViewColumn> list = new List<GridViewColumn>();
        public ColumnCollection(GridView  grid)
        {
            _grid = grid;
        }
        public int IndexOf(GridViewColumn item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, GridViewColumn item)
        {
            list.Insert(index, item);
            Grid.OnColumnChanged(item, ChangedReason.Add);
        }

        public void RemoveAt(int index)
        {
            GridViewColumn item = this[index];
            list.RemoveAt(index);
            Grid.OnColumnChanged(item, ChangedReason.Remove); 
        }

        public GridViewColumn this[int index]
        {
            get
            {
                if (index < this.list.Count)
                {
                    return list[index];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                list[index] = value;
            }
        }

        public GridViewColumn this[string name]
        {
            get
            {
                foreach (GridViewColumn item in list)
                {
                    if (item.FieldName == name)
                        return item;
                }
                return null;
            }
            set
            { 
            }
        }
        public void Add(GridViewColumn item)
        {
            list.Add(item);
            Grid.OnColumnChanged(item, ChangedReason.Add);
        }
        public GridViewColumn Add(string name)
        {
            GridViewColumn col = new GridViewColumn(this.Grid);
            col.Caption = name;
            col.FieldName = name;
            Add(col);
            return col;
        }
        public GridViewColumn Add(string name,string caption)
        {
            GridViewColumn col = new GridViewColumn(this.Grid);
            col.Caption = caption;
            col.FieldName = name;
            Add(col);
            return col;
        }
        public void Clear()
        {
            list.Clear();
            Grid.OnColumnChanged(null, ChangedReason.Clear);
        }

        public bool Contains(GridViewColumn item)
        {
            return list.Contains(item);
        }

        public void CopyTo(GridViewColumn[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(GridViewColumn item)
        {
            bool res = list.Remove(item);
            Grid.OnColumnChanged(item, ChangedReason.Remove);
            return res;
        }

        public IEnumerator<GridViewColumn> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    } 
}

