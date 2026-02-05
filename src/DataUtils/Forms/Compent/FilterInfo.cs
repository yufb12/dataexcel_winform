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

using Feng.Utils;

namespace Feng.Forms.ComponentModel
{
    public class FilterInfo : IList<Filter>
    {
        private List<Filter> list = new List<Filter>();

        #region IList<Filter> 成员

        public int IndexOf(Filter item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, Filter item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Filter this[int index]
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

        #endregion

        #region ICollection<Filter> 成员

        public void Add(Filter item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public virtual bool Contains(Filter item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(Filter[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(Filter item)
        {
            return this.list.Remove(item);
        }

        #endregion

        #region IEnumerable<Filter> 成员

        public IEnumerator<Filter> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion
    }

    public class Filter
    {
        public byte ValueType { get; set; }
        public object Value { get; set; }
        public string Field { get; set; }
    }
}

