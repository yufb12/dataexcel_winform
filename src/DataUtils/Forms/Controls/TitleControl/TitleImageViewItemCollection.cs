using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;

namespace Feng.Forms.Views
{
    public class TitleImageViewItemCollection : IList<TitleImageViewItem>
    {
        public TitleImageViewItemCollection()
        {

        }

        private List<TitleImageViewItem> list = new List<TitleImageViewItem>();
        public int IndexOf(TitleImageViewItem item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, TitleImageViewItem item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public TitleImageViewItem this[int index]
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

        public void Add(TitleImageViewItem item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(TitleImageViewItem item)
        {
            return list.Contains(item);
        }

        public void CopyTo(TitleImageViewItem[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TitleImageViewItem item)
        {
            return list.Remove(item);
        }

        public IEnumerator<TitleImageViewItem> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

}
