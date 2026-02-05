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

    public class MultipleheaderCollection : IList<Multipleheader>
    {
        public MultipleheaderCollection(GridView grid)
        {
            _grid = grid;
        }

        private GridView _grid = null;
        [Category(CategorySetting.PropertyDesign)]
        [Browsable(false)]
        public GridView Grid
        {
            get
            {
                return _grid;
            }
        }
        private List<Multipleheader> list = new List<Multipleheader>();
        public int IndexOf(Multipleheader item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, Multipleheader item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Multipleheader this[int index]
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

        public void Add(Multipleheader item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Multipleheader item)
        {
            return list.Contains(item);
        }

        public void CopyTo(Multipleheader[] array, int arrayIndex)
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

        public bool Remove(Multipleheader item)
        {
            return list.Remove(item);
        }

        public IEnumerator<Multipleheader> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

}

