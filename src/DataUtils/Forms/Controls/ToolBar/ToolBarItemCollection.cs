using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;
using System.Windows.Forms.Layout;

namespace Feng.Forms.Controls
{
    [ToolboxItem(true)]
    [ListBindable(false)]
    public class ToolBarItemCollection : IList<ToolBarItem>, ICollection<ToolBarItem>, IEnumerable<ToolBarItem>
    {
        private List<ToolBarItem> list = new List<ToolBarItem>();
        public ToolBarItemCollection(ToolBar toolbar)
        {
            _toolbar = toolbar;
        }

        public int IndexOf(ToolBarItem item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, ToolBarItem item)
        {
            item.ToolBar = this.ToolBar;
            list.Insert(index, item);
            this.ToolBar.OnToolBarItemChanged(item, Feng.Forms.Controls.GridControl.ChangedReason.Add);
        }
 
        public ToolBarItem this[int index]
        {
            get
            {
                return list[index] as ToolBarItem;
            }
            set
            {
                list[index] = value;
            }
        }
        public ToolBarItem AddItem(ToolBarItem item)
        {
            item.ToolBar = this.ToolBar;
            list.Add(item);
            this.ToolBar.OnToolBarItemChanged(item, Feng.Forms.Controls.GridControl.ChangedReason.Add);
            this.ToolBar.Invalidate();
            return item;
        }
        public void Add(ToolBarItem item)
        {
            item.ToolBar = this.ToolBar;
            list.Add(item);
            this.ToolBar.OnToolBarItemChanged(item, Feng.Forms.Controls.GridControl.ChangedReason.Add);
            this.ToolBar.Invalidate(); 
        }
 
        public bool Contains(ToolBarItem item)
        {
            return list.Contains(item);
        }

        public void CopyTo(ToolBarItem[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ToolBarItem item)
        {
            bool res = list.Remove(item);
            this.ToolBar.OnToolBarItemChanged(item, Feng.Forms.Controls.GridControl.ChangedReason.Remove);
            return res;
        }

        private ToolBar _toolbar = null;

        public ToolBar ToolBar
        {
            get
            {
                return this._toolbar;
            } 
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public IEnumerator<ToolBarItem> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
 
        public void RemoveAt(int index)
        {
            if (index < list.Count)
            {
                ToolBarItem item = list[index] as ToolBarItem;
                Remove(item);
            }
        }
        public ToolBarItem[] ToArray()
        {
            return list.ToArray();
        }
        public void Clear()
        {
            list.Clear();
            this.ToolBar.OnToolBarItemChanged(null, Feng.Forms.Controls.GridControl.ChangedReason.Clear);
        }


    }

}
