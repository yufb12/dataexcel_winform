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
    public class DataTreeNode : Feng.Forms.Interface.IDataStruct
    {
        DataTreeViewBase treeview = null;
        public DataTreeNode(string name)
        {
            _name = name;
        }
        public DataTreeNode(string name, DataTreeViewBase treview)
        {
            treeview = treview;
            _name = name;
            _Text = name;
        }

        public DataTreeNode(DataTreeViewBase treview)
        {
            treeview = treview;
        }
        private bool _Checked = false;
        public virtual bool Check
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
            }
        }

        public virtual string GetFullPath()
        {
            if (this.Parent != null)
            {
                return this.Parent.GetFullPath() + "\\" + this.Text;
            }
            return this.Text;

        }
        private bool _IsExpanded = false;
        public virtual bool IsExpanded
        {
            get
            {
                return _IsExpanded;
            }
            set
            {
                this.TreeView.BeginRefreshNodes();
                _IsExpanded = value; 
                this.TreeView.EndRefreshNodes();
            }
        }
        private bool _isSelect = false;
        public virtual bool IsSelected
        {
            get
            {
                return this._isSelect;
            }
        }

        private CheckStates _showcheckbox = CheckStates.Inhert;
        public virtual CheckStates ShowCheckBox
        {
            get { return _showcheckbox; }
            set
            {
                _showcheckbox = value;
            }
        }

        public virtual int Level
        {
            get
            {
                if (this.Parent == null)
                {
                    return 1;
                }
                else
                {
                    return this.Parent.Level + 1;
                }
                //return  this._level; 
            }
        }
        private string _name = string.Empty;
        public virtual string Name { get { return _name; } set { _name = value; } }
        private int _ImageIndex = -1;
        public virtual int ImageIndex
        {
            get
            {
                return this._ImageIndex;
            }
            set
            {
                this._ImageIndex = value;
            }
        }
        private string _ImageKey = string.Empty;
        public virtual string ImageKey
        {
            get
            {
                return this._ImageKey;
            }
            set
            {
                this._ImageKey = value;
            }
        }

        private string _Text = string.Empty;
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
        private string _tooltiptext = string.Empty;
        public virtual string ToolTipText
        {
            get
            {
                return _tooltiptext;
            }
            set
            {
                _tooltiptext = value;
            }
        }

        public int IndexKey
        {
            get;
            set;
        }
        public string TextKey
        {
            get;
            set;
        }
        private int _left = 0;
        public virtual int Left
        {
            get
            {
                return this._left;
            }
            set
            {
                this._left = value;
            }
        }
        private int _top = 0;
        public virtual int Top
        {
            get
            {
                return this._top;
            }
            set
            {
                this._top = value;
            }
        }
        private int _width = 0;
        public virtual int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }
        private int _height = 0;
        public virtual int Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }
        public virtual int Right
        {
            get
            {
                return this.Left + this.Width;
            }
        }
        public virtual int Bootom
        {
            get
            {
                return this.Top + this.Height;
            }
        }


        private Bitmap _Image = null;
        public Bitmap Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
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

        public bool IsLeaf
        {
            get {
                if (this.Nodes.Count < 1)
                {
                    return true;
                }
                return false;
            }
        }


        private DataTreeNodeCollection _nodes = null;
        public virtual DataTreeNodeCollection Nodes
        {
            get
            {
                if (this._nodes == null)
                {
                    this._nodes = new DataTreeNodeCollection(treeview, this);
                }
                return this._nodes;
            }
        }
        private bool _haschild = false;
        public virtual bool HasChild {

            get {
                return this._haschild;
            }
            set
            {
                this._haschild = value;
            }
        }
        private DataTreeNode parent = null;
        public virtual DataTreeNode Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }
        public virtual DataTreeNode FirstNode
        {

            get
            {
                return this.TreeView.GetFirstNode(this);
            }
        }
        public virtual DataTreeNode LastNode
        {

            get
            {
                return this.TreeView.GetLastNode(this);
            }
        }

        public virtual DataTreeNode NextNode
        {

            get
            {
                return this.TreeView.GetNextNode(this);
            }
        }
        public virtual DataTreeNode NextVisibleNode
        {
            get
            {
                return this.TreeView.GetNextVisibleNode(this);
            }
        }

        public virtual DataTreeNode PrevNode
        {
            get
            {
                return this.TreeView.GetPrevNode(this);
            }
        }
        public virtual DataTreeNode PrevVisibleNode
        {
            get
            {
                return this.TreeView.GetPrevVisibleNode(this);
            }
        }
        public virtual void ExpandAll()
        {
            this.TreeView.BeginRefreshNodes();
            Expand(this);
            this.TreeView.EndRefreshNodes();
        }
        public virtual void Expand(DataTreeNode node)
        {
            foreach (DataTreeNode treenode in node.Nodes)
            {
                node.IsExpanded = true;
                Expand(treenode);
            }
        }

        public virtual void Expand()
        {
            this.TreeView.BeginRefreshNodes();
            this.IsExpanded = true;
            this.TreeView.EndRefreshNodes();
        }
        public virtual object Tag
        {
            get;
            set;
        }
        public DataTreeViewBase TreeView
        {
            get
            {
                return treeview;
            }
        }
        public virtual Rectangle Rect
        {
            get
            {
                return Rectangle.Empty;
            }
        }
        public override string ToString()
        {
            if (this.Value != null)
            {
                return this.Value.ToString();
            }
            return this.Text;
        }

        public void Clone(DataTreeNode newnode)
        {
            newnode.Text = this.Text;
            newnode.Image = this.Image;
            newnode.ImageIndex = this.ImageIndex;
            newnode.ImageKey = this.ImageKey;
            newnode.IsExpanded = this.IsExpanded;
            newnode.Name = this.Name;
            newnode.Tag = this.Tag;
            newnode.ToolTipText = this.ToolTipText;
            newnode.Value = this.Value;
            newnode.HasChild = this.HasChild;
        }

        public virtual DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = string.Empty,
                    Version = string.Empty,
                    AessemlyDownLoadUrl = string.Empty,
                    FullName = string.Empty,
                    Name = string.Empty,
                };

                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, this._Checked);
                    bw.Write(2, this._height);
                    bw.Write(3, this._Image);
                    bw.Write(4, this._ImageIndex);
                    bw.Write(5, this._ImageKey);
                    bw.Write(6, this._IsExpanded);
                    bw.Write(7, this._isSelect);
                    bw.Write(8, this._left);
                    bw.Write(9, this._name);
                    bw.Write(10, this.Nodes.Count);
                    foreach (var node in this.Nodes)
                    {
                        bw.Write(node.Data);
                    }
                    bw.Write(13, this._Text);
                    bw.Write(14, this._tooltiptext);
                    bw.Write(15, this._top);
                    bw.WriteBaseValue(17, this._value);
                    bw.Write(18, this._width);
                    bw.Write(19, this._haschild);
                    data.Data = bw.GetData();
                    bw.Close();
                }

                return data;
            }
        }
        public static void AddNode(DataTreeNode n, DataTreeNode nnode)
        {
            foreach (DataTreeNode node in n.Nodes)
            {
                DataTreeNode newnode = new DataTreeNode(nnode.TreeView);
                node.Clone(newnode);
                nnode.Nodes.Add(newnode);
                newnode.Parent = nnode;
            }
        }
        public object ParentValue { get; set; }

        public void ReadDataStruct(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new IO.BufferReader(data.Data))
            {
                reader.ReadIndex(1, this._Checked);
                reader.ReadIndex(2, this._height);
                reader.ReadIndex(3, this._Image);
                reader.ReadIndex(4, this._ImageIndex);
                reader.ReadIndex(5, this._ImageKey);
                reader.ReadIndex(6, this._IsExpanded);
                reader.ReadIndex(7, this._isSelect);
                reader.ReadIndex(8, this._left);
                reader.ReadIndex(9, this._name); 
                int count = reader.ReadIndex(10, 0);
                for (int i = 0; i < count; i++)
                {
                    DataTreeNode node = new DataTreeNode(this.treeview);
                    DataStruct ds = reader.ReadDataStruct();
                    node.ReadDataStruct(ds);
                }
                reader.ReadIndex(13, this._Text);
                reader.ReadIndex(14, this._tooltiptext);
                reader.ReadIndex(15, this._top);
                reader.ReadBaseValueIndex(17, this._value);
                reader.ReadIndex(18, this._width);
                reader.ReadIndex(19, this._haschild);
            }
        }
    }

    public class DataTreeNodeCollection : IList<DataTreeNode>
    {
        public DataTreeNodeCollection(DataTreeViewBase treeview, DataTreeNode node)
        {
            treeView = treeview;
            parentnode = node;
        }
        private List<DataTreeNode> list = new List<DataTreeNode>();
        private DataTreeViewBase treeView = null;
        private DataTreeNode parentnode = null;
        public DataTreeNodeCollection(DataTreeViewBase treeview)
        {
            treeView = treeview;
        }
        public int IndexOf(DataTreeNode item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, DataTreeNode item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public DataTreeNode this[int index]
        {
            get
            {
                if (index >= list.Count)
                {
                    return null;
                }
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(DataTreeNode item)
        { 
            list.Add(item);
        }
        public DataTreeNode Add(string name)
        {
            DataTreeNode item = new DataTreeNode(treeView);
            item.Name = name;
            item.Value = name;
            item.Text = name;
            list.Add(item);
            item.Parent = parentnode;
            
            return item;
        }
        public DataTreeNode Add(string name, object value)
        {
            DataTreeNode item = new DataTreeNode(treeView);
            item.Name = name;
            item.Value = value;
            item.Text = Feng.Utils.ConvertHelper.ToString(value);
            list.Add(item);
            item.Parent = parentnode;
            return item;
        }
        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(DataTreeNode item)
        {
            return list.Contains(item);
        }

        public void CopyTo(DataTreeNode[] array, int arrayIndex)
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

        public bool Remove(DataTreeNode item)
        {
            return list.Remove(item);
        }

        public IEnumerator<DataTreeNode> GetEnumerator()
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

        public virtual Feng.Forms.Controls.TreeView.DataTreeNode GetNodeByTag(object tag)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                DataTreeNode node = this[i];
                if (node.Tag.Equals(tag))
                {
                    return node;
                }
            }
            return null;
        }
    }




}
