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
using Feng.Forms.Controls.GridControl.Edits;
using Feng.Forms.Controls.GridControl;
using Feng.Forms.Interface;

namespace Feng.Forms.Views.Vector
{
    public partial class LayerCollection:IList<Layer>
    {
        public LayerCollection()
        {

        }
        private List<Layer> list = new List<Layer>();
        public int IndexOf(Layer item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, Layer item)
        {
            if (list.Contains(item))
                return;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Layer this[int index]
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

        public void Add(Layer item)
        {
            if (list.Contains(item))
                return;
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Layer item)
        {
            return list.Contains(item);
        }

        public void CopyTo(Layer[] array, int arrayIndex)
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

        public bool Remove(Layer item)
        {
            return list.Remove(item);
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}

