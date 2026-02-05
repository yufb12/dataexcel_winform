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
using Feng.Data;
using Feng.Forms.Interface;

namespace Feng.Forms.Views
{ 
    public class ToolBarItemViewCollection : IList<ToolBarItemView>
    {
        private List<ToolBarItemView> list = new List<ToolBarItemView>();
        public int IndexOf(ToolBarItemView item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, ToolBarItemView item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public ToolBarItemView this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list[index] = value;
            }
        }

        public void Add(ToolBarItemView item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(ToolBarItemView item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(ToolBarItemView[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ToolBarItemView item)
        {
            return this.list.Remove(item);
        }

        public IEnumerator<ToolBarItemView> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
    }
     
}
