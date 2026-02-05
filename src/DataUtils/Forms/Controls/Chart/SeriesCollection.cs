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
    public class SeriesCollection : CollectionBase// , IList<Series>
    {
        public SeriesCollection()
        {

        }

        public int IndexOf(Series item)
        {
            return base.List.IndexOf(item);
        }

        public void Insert(int index, Series item)
        {
            base.List.Insert(index, item);
        }

        public Series this[int index]
        {
            get
            {
                return base.List[index] as Series;
            }
            set
            {
                base.List[index] = value;
            }
        }

        public void Add(Series item)
        {
            base.List.Add(item);
        }

        public bool Contains(Series item)
        {
            return base.List.Contains(item);
        }

        public void CopyTo(Series[] array, int arrayIndex)
        {
            base.List.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(Series item)
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
