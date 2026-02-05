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
    public class RowCollection : IList<GridViewRow>
    {
        private List<GridViewRow> list = new List<GridViewRow>();
        public virtual int IndexOf(GridViewRow item)
        {
            return list.IndexOf(item);
        }

        public virtual void Insert(int index, GridViewRow item)
        {
            list.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public virtual GridViewRow this[int index]
        {
            get
            {
                if (index < this.list.Count)
                {
                    return list[index];
                }
                return null;
            }
            set
            {
                list[index] = value;
            }
        }

        public virtual void Add(GridViewRow item)
        {
            list.Add(item);
        }

        public virtual void Clear()
        {
            list.Clear();
        }

        public virtual bool Contains(GridViewRow item)
        {
            return list.Contains(item);
        }

        public virtual void CopyTo(GridViewRow[] array, int arrayIndex)
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

        public virtual bool Remove(GridViewRow item)
        {
            return list.Remove(item);
        }

        public virtual IEnumerator<GridViewRow> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}

