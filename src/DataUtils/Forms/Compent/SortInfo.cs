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
using Feng.Data;

namespace Feng.Forms.ComponentModel
{ 

    public class SortInfo : IList<SortColumn>
    {
        private List<SortColumn> list = new List<SortColumn>();

        #region IList<SortColumn> 成员

        public int IndexOf(SortColumn item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, SortColumn item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public SortColumn this[int index]
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

        #region ICollection<SortColumn> 成员

        public void Add(SortColumn item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public virtual bool Contains(SortColumn item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(SortColumn[] array, int arrayIndex)
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

        public virtual bool Remove(SortColumn item)
        {
            return this.list.Remove(item);
        }

        #endregion

        #region IEnumerable<SortColumn> 成员

        public IEnumerator<SortColumn> GetEnumerator()
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

    public class SortColumn
    {
        public SortColumn()
        {

        }
        public SortColumn(string field, SortOrder sortorder, byte type)
        {
            this._field = field;
            this._sortorder = sortorder;
            this._type = type;
        }

        private string _field = string.Empty;
        public string Field
        {
            get { return this._field; }
            set { this._field = value; }
        }

        private byte _type = TypeEnum.TNull;
        public byte Type
        {
            get { return _type; }
            set { this._type = value; }
        }
        private SortOrder _sortorder = SortOrder.Default;
        public SortOrder SortOrder
        {
            get { return _sortorder; }
            set { this._sortorder = value; }
        }
    }

    public enum SortOrder
    {
        Null,
        Default,
        Ascending,
        Descending
    };
}

