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
    public class CellCollection : IList<GridViewCell>
    {
        private List<GridViewCell> list = new List<GridViewCell>();


        public virtual GridViewCell this[GridViewColumn col]
        {
            get
            {
                foreach (GridViewCell cell in list)
                {
                    if (cell.Column == col)
                    {
                        return cell;
                    }
                }
                return null;
            }
        }
        public virtual GridViewCell this[string field]
        {
            get
            {
                foreach (GridViewCell cell in list)
                {
                    if (cell.Column.FieldName == field)
                    {
                        return cell;
                    }
                }
                return null;
            }
        }
        public virtual GridViewCell this[int index]
        {
            get
            {
                if (index >= list.Count)
                {
                    return null;
                }
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public virtual void Add(GridViewCell item)
        {
            list.Add(item);
        }

        public virtual void Clear()
        {
            list.Clear();
        }

        public virtual bool Contains(GridViewCell item)
        {
            return list.Contains(item);
        }

        public virtual void CopyTo(GridViewCell[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public virtual int Count
        {
            get
            {
                return list.Count;
            }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(GridViewCell item)
        {
            return list.Remove(item);
        }

        public virtual IEnumerator<GridViewCell> GetEnumerator()
        {
            return list.GetEnumerator();
        }


        public virtual int IndexOf(GridViewCell item)
        {
            return list.IndexOf(item);
        }

        public virtual void Insert(int index, GridViewCell item)
        {
            list.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    } 
}

