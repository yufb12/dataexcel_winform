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
    //代码生成，生成查询代码。
    //查询代码前面加A.
    //生成参数查询代码

    //按项目生成代码
    //增强 附件上传，附件预览功能。
        public class ColorValue
        {
            private Color _forececolor = Color.Empty;
            private Color _backcolor = Color.Empty;
            public virtual Color ForceColor
            {
                get
                {
                    return _forececolor;
                }
                set
                {
                    _forececolor = value;
                }
            }
            public virtual Color BackColor
            {
                get
                {
                    return _backcolor;
                }
                set
                {
                    _backcolor = value;
                }
            }

            private string _Text = string.Empty;
            [Browsable(true)]
            [ReadOnly(true)]
            public virtual string Text
            {
                get
                {
                    return _Text;
                }
                set
                {
                    _Text = value;
                }
            }
            private object databounditem = null;
            public virtual object DataBoundItem
            {
                get
                {
                    return databounditem;
                }
            }
            internal void InitDataBoundItem(object item)
            {
                databounditem = item;
            }

            [Browsable(false)]
            public virtual object Tag
            {
                get;
                set;
            }
            private object _value = null;
            public virtual object Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = value;
                }
            }
            private bool _isVisible = false;
            public virtual bool IsVisible
            {
                get
                {
                    return _isVisible;
                }
            }
        }

        public class ColorValueCollection : IList<ColorValue>
        {
            private List<ColorValue> list = new List<ColorValue>();
            public int IndexOf(ColorValue item)
            {
                return list.IndexOf(item);
            }

            public void Insert(int index, ColorValue item)
            {
                list.Insert(index, item);
            }

            public void RemoveAt(int index)
            {
                list.RemoveAt(index);
            }

            public ColorValue this[int index]
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

            public void Add(ColorValue item)
            {
                list.Add(item);
            }

            public void Clear()
            {
                list.Clear();
            }

            public bool Contains(ColorValue item)
            {
                return list.Contains(item);
            }

            public void CopyTo(ColorValue[] array, int arrayIndex)
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

            public bool Remove(ColorValue item)
            {
                return list.Remove(item);
            }

            public IEnumerator<ColorValue> GetEnumerator()
            {
                return list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return list.GetEnumerator();
            }

            public override string ToString()
            {
                return list.Count.ToString();
            }
        }

        //System.Data.DataTable table = GetTable();
        //datatree.KeyField = "ID";
        //datatree.KeyParentField = "ParentID";
        //datatree.Top = 0;
        //datatree.Width = 300;
        //datatree.Height = 800;
        //datatree.Left = 10;
        //datatree.Position = 2;
        //datatree.RefreshDataTable(table);
        //datatree.RefreshVisible();
        //string text = datatree.GetVisibleTreeText();
        //System.IO.File.WriteAllText(@"C:\PerfLogs\" + DateTime.Now.ToString("MM-dd HHmmssfff") + ".txt", text);
}
