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
    public class SelectCellCollection : IList<GridViewCell>
    {
        private List<GridViewCell> list = new List<GridViewCell>();
        public int IndexOf(GridViewCell item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, GridViewCell item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public GridViewCell this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(GridViewCell item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(GridViewCell item)
        {
            return list.Contains(item);
        }

        public void CopyTo(GridViewCell[] array, int arrayIndex)
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

        public bool Remove(GridViewCell item)
        {
            return list.Remove(item);
        }

        public IEnumerator<GridViewCell> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    } 
}

