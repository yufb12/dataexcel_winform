using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;

namespace Feng.Forms.Controls
{
    [ToolboxItem(false)]
    public class PaneCollection : CollectionBase// , IList<Pane>
    {
        public PaneCollection()
        {

        }

        public int IndexOf(Pane item)
        {
            return base.List.IndexOf(item);
        }

        public void Insert(int index, Pane item)
        {
            base.List.Insert(index, item);
        }

        public Pane this[int index]
        {
            get
            {
                return base.List[index] as Pane;
            }
            set
            {
                base.List[index] = value;
            }
        }

        public void Add(Pane item)
        {
            base.List.Add(item);
        }

        public bool Contains(Pane item)
        {
            return base.List.Contains(item);
        }

        public void CopyTo(Pane[] array, int arrayIndex)
        {
            base.List.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(Pane item)
        {
            base.List.Remove(item);
        }

        private Chart _toolbar = null;

        public Chart ToolBar
        {
            get
            {
                return this._toolbar;
            }
            set
            {
                this._toolbar = value;
            }
        }
    }

}
