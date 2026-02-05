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
    [ToolboxItem(true)]
    public class ListImageViewItemCollection : CollectionBase// , IList<ListImageViewItem>
    {
        public ListImageViewItemCollection()
        {

        }

        public int IndexOf(ListImageViewItem item)
        {
            return base.List.IndexOf(item);
        }

        public void Insert(int index, ListImageViewItem item)
        {
            base.List.Insert(index, item);
        }
 
        public ListImageViewItem this[int index]
        {
            get
            {
                return base.List[index] as ListImageViewItem;
            }
            set
            {
                base.List[index] = value;
            }
        }

        public void Add(ListImageViewItem item)
        {
            base.List.Add(item);
        }
 
        public bool Contains(ListImageViewItem item)
        {
            return base.List.Contains(item);
        }

        public void CopyTo(ListImageViewItem[] array, int arrayIndex)
        {
            base.List.CopyTo(array, arrayIndex);
        }
 
        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(ListImageViewItem item)
        {
            base.List.Remove(item);
        }

        private ListImageView _parent = null;

        public ListImageView Parnt
        {
            get {
                return this._parent;
            }
            set
            {
                this._parent = value;
            }
        }
    }

}
