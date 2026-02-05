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
using System.Data;

using Feng.Data;
using System.Reflection;
using Feng.Enums;
using Feng.Forms.Controls.GridControl;

namespace Feng.Forms.Controls.TreeView
{ 
    public class DataTreeRowCollection : RowCollection 
    {
        public override void Add(GridViewRow item)
        {
            if (item is DataTreeRow)
            {
                base.Add(item);
            }
            else if (item.GetType().IsSubclassOf(typeof(DataTreeRow)))
            {
                base.Add(item);
            }
        }
        public int IndexOf(DataTreeRow item)
        {
            return base.IndexOf(item);
        }

        public void Insert(int index, DataTreeRow item)
        {
            if (item is DataTreeRow)
            {
                base.Insert(index, item);
            }
            else if (item.GetType().IsSubclassOf(typeof(DataTreeRow)))
            {
                base.Insert(index, item);
            }
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }

        public new virtual DataTreeRow this[int index]
        {
            get
            {
                if (index < this.Count)
                {
                    return this[index] as DataTreeRow;
                }
                return null;
            }
            set
            {
                this[index] = value;
            }
        }

        public void Add(DataTreeRow item)
        {
            base.Add(item);
        }

        public override void Clear()
        {
            base.Clear();
        }

        public bool Contains(DataTreeRow item)
        {
            return base.Contains(item);
        }

        public void CopyTo(DataTreeRow[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public override int Count
        {
            get
            {
                return base.Count;
            }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(DataTreeRow item)
        {
            return base.Remove(item);
        }
 
  
    }

}
