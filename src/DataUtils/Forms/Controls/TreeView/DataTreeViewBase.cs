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
using Feng.Forms.Views;

namespace Feng.Forms.Controls.TreeView
{
    public partial class DataTreeViewBase : GridView 
    {
        public DataTreeViewBase()
        {

        }
 
        #region GRIDVIEW 重载
 
        private DataTreeRowCollection _rows = null;
        public override RowCollection Rows
        {
            get
            {
                if (_rows == null)
                {
                    _rows = new DataTreeRowCollection();
                }
                return _rows;
            }
        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            bool result= base.OnDraw(this, g);
            DOperationTreeRect(g);
            return result;
        }
        public void AddNode(DataTreeNode n, DataTreeNode nnode)
        {
            foreach (DataTreeNode node in n.Nodes)
            {
                DataTreeNode newnode = new DataTreeNode(this);
                node.Clone(newnode);
                nnode.Nodes.Add(newnode);
            }
        }
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            Point pt = PointToClient(e.Location);
            if (this.OperationTreeRect.Contains(pt))
            {
                if (this.InDesign)
                {
                    using (Feng.Forms.TreeViewDialog frm = new Feng.Forms.TreeViewDialog())
                    {
                        frm.Init();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            this.Nodes.Clear();
                            DataTreeView treeview = frm.tree.TreeView;
                            foreach (DataTreeNode node in treeview.Nodes)
                            {
                                DataTreeNode newnode = new DataTreeNode(this);
                                node.Clone(newnode);
                                this.Nodes.Add(newnode);
                                AddNode(node, newnode);
                            }
                            this.Invalidate();
                        }
                    }
                    return true;
                }
            }
            return base.OnMouseDown(sender, e, ve);
        }
        public override bool OnGridViewCellClick(MouseEventArgs e, GridViewCell cell)
        {
            try
            {
                if (cell.Column.Index == 0)
                {
                    DataTreeRow row = cell.Row as DataTreeRow;
                    if (row != null)
                    {
                        if (row.Node == null)
                        {
                            return false;
                        }

                        row.Node.IsExpanded = !row.Node.IsExpanded;

                        row.Node.TreeView.RefreshAll();
                        row.Node.TreeView.Invalidate();
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnGridViewCellClick(e, cell);
        }
        public override void ReFreshScroll()
        {
            int rowcount = this.Rows.Count;
            int datacount = this.GetVisibleNodes().Count;

            if (datacount > rowcount)
            {
                this.VScroll.Visible = true;
            }
            else
            {
                this.VScroll.Visible = false;
            }
             
        }
        #endregion
  
        private int _NodeHeight = 20;
        public virtual int NodeHeight
        {
            get { return _NodeHeight; }
            set { _NodeHeight = value; }
        }
        private DataTreeNodeCollection _AllNodes = null;
        public virtual DataTreeNodeCollection AllNodes
        {
            get
            {
                if (_AllNodes == null)
                {
                    _AllNodes = new DataTreeNodeCollection(this);
                }
                return _AllNodes;
            }
        }
        private DataTreeNodeCollection _nodes = null;
        public virtual DataTreeNodeCollection Nodes
        {
            get
            {
                if (this._nodes == null)
                {
                    this._nodes = new DataTreeNodeCollection(this);
                }
                return this._nodes;
            }
        }

        public virtual DataTreeNode GetLastNode(DataTreeNode node)
        {
            DataTreeNode treenode = null;
            if (node.Parent != null)
            {
                int count = node.Parent.Nodes.Count;
                int index = node.Parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    treenode = node.Parent.Nodes[count - 1];
                }
            }
            else
            {
                int count = this.Nodes.Count;
                int index = this.Nodes.IndexOf(node);
                if (index > 0)
                {
                    treenode = this.Nodes[count - 1];
                }
            }
            return treenode;
        }
        public virtual DataTreeNode GetPrevNode(DataTreeNode node)
        {
            DataTreeNode treenode = null;
            if (node.Parent != null)
            {
                int count = node.Parent.Nodes.Count;
                int index = node.Parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    index = index - 1;
                    if (index >= 0)
                    {
                        treenode = node.Parent.Nodes[index];
                    }
                }
            }
            else
            {
                int count = this.Nodes.Count;
                int index = this.Nodes.IndexOf(node);
                if (index > 0)
                {
                    index = index - 1;
                    if (index >= 0)
                    {
                        treenode = this.Nodes[index];
                    }
                }
            }
            return treenode;
        }
        public virtual DataTreeNode GetNextNode(DataTreeNode node)
        {
            DataTreeNode treenode = null;
            if (node.Parent != null)
            {
                int count = node.Parent.Nodes.Count;
                int index = node.Parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    index = index + 1;
                    if (index < count)
                    {
                        treenode = node.Parent.Nodes[index];
                    }
                }
            }
            else
            {
                int count = this.Nodes.Count;
                int index = this.Nodes.IndexOf(node);
                if (index > 0)
                {
                    index = index + 1;
                    if (index < count)
                    {
                        treenode = this.Nodes[index];
                    }
                }
            }
            return treenode;
        }
        public virtual DataTreeNode GetFirstNode(DataTreeNode node)
        {
            DataTreeNode treenode = null;
            if (node.Parent != null)
            {
                int count = node.Parent.Nodes.Count;
                if (count > 0)
                {
                    treenode = node.Parent.Nodes[0];
                }
            }
            else
            {
                int count = this.Nodes.Count;
                if (count > 0)
                {
                    treenode = this.Nodes[0];
                }
            }
            return treenode;
        }
        public virtual DataTreeNode GetPrevVisibleNode(DataTreeNode node)
        {
            DataTreeNode treenode = null;
            if (node.Parent != null)
            {
                int count = node.Parent.Nodes.Count;
                int index = node.Parent.Nodes.IndexOf(node);
                if (index >= 0)
                {
                    for (int i = 0; i < index; i++)
                    {
                        DataTreeNode temp = node.Parent.Nodes[i];
                        treenode = temp;
                        break;
                    }
                }
            }
            else
            {
                int count = this.Nodes.Count;
                int index = this.Nodes.IndexOf(node);
                if (index > 0)
                {
                    for (int i = 0; i < index; i++)
                    {
                        DataTreeNode temp = this.Nodes[i];
                        treenode = temp;
                        break;
                    }
                }
            }
            return treenode;
        }
        public virtual DataTreeNode GetNextVisibleNodeChild(DataTreeNode node)
        {
            if (node.IsExpanded)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    DataTreeNode tnode = node.Nodes[i];
                    return tnode;
                }
            }
            return null;
        }
        public virtual DataTreeNode GetNextVisibleNodeSameLevel(DataTreeNode node)
        {
            if (node.Parent != null)
            {
                int count = node.Parent.Nodes.Count;
                int index = node.Parent.Nodes.IndexOf(node);
                if (index >= 0)
                {
                    for (int i = index + 1; i < count; i++)
                    {
                        DataTreeNode temp = node.Parent.Nodes[i];
                        return temp;
                    }
                }
                return GetNextVisibleNodeSameLevel(node.Parent);
            }
            else
            {
                int count = this.Nodes.Count;
                int index = this.Nodes.IndexOf(node);
                if (index >= 0)
                {
                    for (int i = index + 1; i < count; i++)
                    {
                        DataTreeNode temp = this.Nodes[i];
                        return temp;
                    }
                }
            }
            return null;
        }
        public virtual DataTreeNode GetNextVisibleNode(DataTreeNode node)
        {
            DataTreeNode treenode = GetNextVisibleNodeChild(node);
            if (treenode != null)
            {
                return treenode;
            }
            else
            {
                treenode = GetNextVisibleNodeSameLevel(node);
                if (treenode != null)
                {
                    return treenode;
                } 
            }
            return null;
        }
        public virtual DataTreeNode GetNextVisibleNode2(DataTreeNode node)
        {
            if (node.IsExpanded)
            {
                if (node.Nodes.Count > 0)
                {
                    for (int i = 0; i < node.Nodes.Count; i++)
                    {
                        DataTreeNode tnode = node.Nodes[i];

                        return tnode;
                    }
                }
            }
            DataTreeNode treenode = null;
            if (node.Parent != null)
            {
                int count = node.Parent.Nodes.Count;
                int index = node.Parent.Nodes.IndexOf(node);
                if (index >= 0)
                {
                    for (int i = index + 1; i < count; i++)
                    {
                        DataTreeNode temp = node.Parent.Nodes[i];
                        treenode = temp;
                        break;
                    }
                }
            }
            else
            {
                if (this.Nodes.Contains(node))
                {

                }
                else
                {
                    //throw new Exception("aaaa");
                }
                int count = this.Nodes.Count;
                int index = this.Nodes.IndexOf(node);
                if (index >= 0)
                {
                    for (int i = index + 1; i < count; i++)
                    {
                        DataTreeNode temp = this.Nodes[i];
                        treenode = temp;
                        break;
                    }
                }
            }
            return treenode;
        }
        private List<DataTreeNode> visibleNodes = new List<DataTreeNode>();
        public virtual List<DataTreeNode> GetVisibleNodes()
        {
            return visibleNodes;
        }

        public virtual DataTreeNode TopNode { get; set; }

        public virtual DataTreeNode AddNode(string name)
        {
            DataTreeNode node = new DataTreeNode(this);
            node.Name = name;
            node.Value = name;
            node.Text = name;
            this.Nodes.Add(node);
            return node;
        }
        public virtual DataTreeNode AddNode(string name, object value)
        {
            DataTreeNode item = new DataTreeNode(this);
            item.Name = name;
            item.Value = value;
            item.Text = Feng.Utils.ConvertHelper.ToString(value);
            this.Nodes.Add(item);
            return item;
        }
        public override void InitDataSource()
        {
            if (this.DataSource is DataTable)
            {
                RefreshDataTable(this.DataSource as DataTable);
            }
            base.InitDataSource();
        }
        public override bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            GridViewCell cell = this.FocusedCell;
            if (cell == null)
                return false;
            if (cell.Row != null)
            {
                DataTreeRow treerow = cell.Row as DataTreeRow;
                if (treerow != null)
                {
                    if (treerow.Node != null)
                    {
                        base.OnDoubleClick(sender, e, ve);
                        this.FocusedNode = treerow.Node;
                        this.OnNodeDoubleClick(treerow.Node); 
                    }
                }
            }
            this.FocusedNode = null;
            this.OnFocusedNodeChanged(null);
            return false;
        }
        public override void OnFocusedCellChanged(GridViewCell cell)
        {
            if (cell.Row != null)
            {
                DataTreeRow treerow = cell.Row as DataTreeRow;
                if (treerow != null)
                {
                    if (treerow.Node != null)
                    {
                        base.OnFocusedCellChanged(cell);
                        this.FocusedNode = treerow.Node;
                        this.OnFocusedNodeChanged(treerow.Node);
                        return;
                    }
                }
            }
            this.FocusedNode = null;
            this.OnFocusedNodeChanged(null);
        }
        public override GridViewCell FocusedCell
        {
            get
            {
                if (base.FocusedCell != null)
                {
                    if (base.FocusedCell.Row != null)
                    {
                        DataTreeRow treerow = base.FocusedCell.Row as DataTreeRow;
                        if (treerow != null)
                        {
                            if (treerow.Node == this.FocusedNode)
                            {
                                return base.FocusedCell;
                            }
                        }
                    }
                }
                return null;
            }
            set
            {
                base.FocusedCell = value;
            }
        }
        public virtual DataTreeRow GetRowByNode(DataTreeNode node)
        {
            foreach (DataTreeRow row in this.Rows)
            {
                if (row.Node == node)
                {
                    return row;
                }
            }
            return null;
        }
        public virtual void RefreshDataTable(DataTable table)
        { 
            Dictionary<object, DataTreeNode> dicsP = new Dictionary<object, DataTreeNode>();
            AllNodes.Clear();
            this.Nodes.Clear();
            if (table.Columns.Contains(KeyField) && table.Columns.Contains(KeyParentField))
            {
                foreach (DataRow row in table.Rows)
                {
                    object value = row[KeyField];
                    object parentValue = row[KeyParentField];
                    DataTreeNode node = new DataTreeNode(this);
                    node.InitDataBoundItem(row);
                    node.Value = value; 
                    string text = Feng.Utils.ConvertHelper.ToString(row[DisplayField]);
                    node.Text = text;
                    node.ParentValue = parentValue;
                    if (!dicsP.ContainsKey(value))
                    {
                        dicsP.Add(value, node);
                    }
                    if (dicsP.ContainsKey(parentValue))
                    {
                        DataTreeNode n = dicsP[parentValue];
                        node.Parent = n;
                        n.Nodes.Add(node);
                        continue;
                    }
                    if (parentValue == null)
                    {
                        this.Nodes.Add(node);
                        continue;
                    } 
                    string pvalue = Feng.Utils.ConvertHelper.ToString(parentValue);
                    if (string.IsNullOrWhiteSpace(pvalue))
                    {
                        this.Nodes.Add(node);
                        continue;
                    }

                    AllNodes.Add(node); 


                }

                foreach (DataTreeNode node in AllNodes)
                {
                    if (dicsP.ContainsKey(node.ParentValue))
                    {
                        DataTreeNode n = dicsP[node.ParentValue];
                        node.Parent = n;
                        n.Nodes.Add(node);
                    }
                    else
                    {
                        this.Nodes.Add(node);
                    }
                }
            }
        }

        private int beginrefreshnodes = 0;
        public virtual void BeginRefreshNodes()
        {
            beginrefreshnodes++;
        }

        private int endrefreshnodes = 0;
        public virtual void EndRefreshNodes()
        {
            endrefreshnodes++;
            if (beginrefreshnodes == endrefreshnodes)
            {
                this.RefreshVisible(); 
                this.RefreshRowValue();
            }
        } 
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
            }
        }
        public override void RefreshVisible()
        {
            visibleNodes.Clear();
            _count = this.Nodes.Count;
            foreach (DataTreeNode node in this.Nodes)
            {
                visibleNodes.Add(node);
                RefreshCount(node, ref  _count);
            }
            this.VScroll.Max = this.VScroll.Min + Count-this.Rows.Count;
            OnVisibleNodesChanged();
        }
        public event EventHandler VisibleNodesChanged;
        public void OnVisibleNodesChanged()
        {
            if (VisibleNodesChanged != null)
            {
                VisibleNodesChanged(this, new EventArgs());
            }
        }
        public virtual void RefreshCount(DataTreeNode node, ref  int count)
        {
            if (node.IsExpanded)
            {
                count = count + node.Nodes.Count;
                foreach (DataTreeNode treenode in node.Nodes)
                {
                    visibleNodes.Add(treenode);
                    RefreshCount(treenode, ref  count);
                }
            }
        }
        private int _count = 0;
        public override int Count
        {
            get
            {
                return _count;
            }
        }
        public override void GenerateColumns(object datasource)
        {
            if (AutoGenerateColumns)
            {
                FirstColumn = 0;
                this.Columns.Clear();
                if (datasource is DataSet)
                {
                    if (string.IsNullOrEmpty(this.DataMember))
                    {
                        InitDefaultOneColumnFields(((DataSet)datasource).Tables[0]);
                    }
                    else
                    {
                        InitDefaultOneColumnFields(((DataSet)datasource).Tables[this.DataMember]);
                    }
                }
                else if (datasource is DataTable)
                {
                    InitDefaultOneColumnFields((DataTable)datasource);
                }
                else if (IsIlistDataSource(datasource))
                {
                    InitDefaultOneColumnFields(datasource as System.Collections.IList);
                }
            }
            else
            {
                base.GenerateColumns(datasource);
            }
        }
        public void InitDefaultOneColumnFields(System.Collections.IList datasource)
        {
            Type t = datasource.GetType().GetGenericArguments()[0];
            System.Reflection.PropertyInfo[] ps = t.GetProperties();

            for (int i = 0; i < ps.Length; i++)
            {
                int columnindex = i + 1;
                string name = ps[i].Name;
                if (name != this.DisplayField)
                {
                    continue;
                }
                GridViewColumn col = new GridViewColumn(this);
                col.Caption = ps[i].Name;
                col.FieldName = ps[i].Name;
                col.Width = this.Width;
            }
        }

        public void InitDefaultOneColumnFields(DataTable table)
        {
            DataColumn dataColumn = table.Columns[this.DisplayField] ;
            int left = RowHeaderWidth;
            int DefaultColumnWidth = 72;
            GridViewColumn col = null; 
            col = new GridViewColumn(this);
            this.Columns.Add(col);
            col.Caption = dataColumn.Caption;
            col.Width = this.Width;
            col.AutoWidth = false;
            if (string.IsNullOrWhiteSpace(col.Caption))
            {
                col.Caption = dataColumn.ColumnName;
            }
            col.FieldName = dataColumn.ColumnName; 
            col.Left = left;
            left = left + DefaultColumnWidth;
            col.DataType = dataColumn.DataType.FullName;

            if (dataColumn.DataType == typeof(decimal))
            {
                col.TotalMode = TotalMode.Sum;
            }
            if (dataColumn.DataType == typeof(int))
            {
                col.TotalMode = TotalMode.Sum;
            } 
        }

        public override void RefreshRowValue()
        {
            int position = this.Position;
            if (visibleNodes.Count <= position)
            {
                return;
            }
 
            DataTreeNode node = visibleNodes[position];
            for (int i = 0; i < this.Rows.Count; i++)
            {

                DataTreeRow row = this.Rows[i] as DataTreeRow;
                row.Node = node;
                DataRow datarow = null;
                if (node != null)
                {
                    if (node.DataBoundItem == null)
                    {
                        GridViewColumn col = this.Columns[0];
                        if (col == null)
                        {
                            col = new TreeViewColumn(this);
                            col.Width = this.LeftSapce;
                            this.Columns.Add(col);
                            this.visibleColumns.Add(col);
                        }
                        GridViewCell cell = row.Cells[col];
                        if (cell == null)
                        {
                            cell = new DataTreeCell(row);
                            cell.Column = col;
                            row.Cells.Add(cell);
                        }
                        cell.Value = node.Text;
                        cell.InitValue(node.Text);
                    }
                    else
                    {
                        datarow = node.DataBoundItem as DataRow;
                        RefreshCellValue(datarow, i);
                    }
                    node = node.NextVisibleNode;
                }
                else
                {
                    foreach (GridViewCell gridviewcell in row.Cells)
                    { 
                        gridviewcell.InitValue(string.Empty);
                    }
                }
                row.Index = position + i + 1;
            }
        }

        public override void AddNewRow()
        {
            DataTreeRow row = new DataTreeRow(this);
            this.Rows.Add(row);
        }
        public override void RefreshRows()
        {
            base.RefreshRows();
             
        }
        public override GridViewCell AddGridViewCell(int rowindex, int colindex)
        {
            DataTreeRow row = this.Rows[rowindex] as DataTreeRow;
            GridViewColumn col = this.Columns[colindex];
            DataTreeCell cell = new DataTreeCell(row);
            cell.Column = col;
            row.Cells.Add(cell);
            return cell;
        }
        private DataTreeNode focusednode = null;
        public DataTreeNode FocusedNode
        {
            get
            {
                return focusednode;
            }
            set
            {
                focusednode = value;
            }
        }
        public DataTreeNode SelectedNode
        {
            get
            {
                if (this.FocusedCell != null)
                {
                    DataTreeRow row = this.FocusedCell.Row as DataTreeRow;
                    if (row != null)
                    {
                        return row.Node;
                    }
                }
                return null;
            }
        }

        public Rectangle OperationTreeRect
        {
            get
            {
                return new Rectangle(this.Width - 16 - 17-23, 4, 16, 16);
            }
        }

        public void DOperationTreeRect(Feng.Drawing.GraphicsObject g)
        {
            if (this.InDesign)
            {
                //Point pt = this.PointToClient(g.MousePoint);
                //if (this.OperationRect.Contains(pt))
                //{
                Rectangle rect = this.OperationTreeRect;
                rect.X = rect.X + this.Left;
                rect.Y = rect.Y + this.Top;
                g.Graphics.DrawRectangle(Pens.LightGray, rect);
                g.Graphics.DrawImage(Images.EditButton_More, rect);


                //g.Graphics.FillRectangle(Brushes.Red, new Rectangle(pt, new Size(10, 10)));
                //}
                //Feng.Drawing.GraphicsHelper.DrawString(g,"...", Font, Brushes.LightGray, this.OperationRect);
            } 
        }

 
        private bool _ShowNodeToolTips = true;
        public virtual bool ShowNodeToolTips { get { return this._ShowNodeToolTips; } set { this._ShowNodeToolTips = value; } }
        private bool _ShowPlusMinus = true;
        public virtual bool ShowPlusMinus { get { return this._ShowPlusMinus; } set { this._ShowPlusMinus = value; } }
        private bool _ShowRootLines = true;
        public virtual bool ShowRootLines { get { return this._ShowRootLines; } set { this._ShowRootLines = value; } }
        private int _Indent = 8;
        public virtual int Indent { get { return this._Indent; } set { this._Indent = value; } }
        private string _PathSeparator = @"\";
        public virtual string PathSeparator { get { return this._PathSeparator; } set { this._PathSeparator = value; } }
        public virtual int SelectedImageIndex { get; set; }
        public virtual string SelectedImageKey { get; set; }

        private string _keyField = string.Empty;
        public virtual string KeyField
        {
            get
            {
                return this._keyField;
            }
            set
            {
                this._keyField = value;
            }
        }

        private string _keyParentField = string.Empty;
        public virtual string KeyParentField
        {
            get
            {
                return this._keyParentField;
            }
            set
            {
                this._keyParentField = value;
            }
        }


        private string _DisplayField = string.Empty;
        public virtual string DisplayField
        {
            get
            {
                return this._DisplayField;
            }
            set
            {
                this._DisplayField = value;
            }
        }

        private string _ExpandImageKey = string.Empty;
        public string ExpandImageKey
        {
            get
            {
                return this._ExpandImageKey;
            }
            set
            {
                this._ExpandImageKey = value;
            }
        }
        private string _CollapseImageKey = string.Empty;
        public string CollapseImageKey
        {
            get
            {
                return this._CollapseImageKey;
            }
            set
            {
                this._CollapseImageKey = value;
            }
        }


        private int _ExpandImageIndex = -1;
        public int ExpandImageIndex
        {
            get
            {
                return this._ExpandImageIndex;
            }
            set
            {
                this._ExpandImageIndex = value;
            }
        }
        private int _CollapseImageIndex = -1;
        public int CollapseImageIndex
        {
            get
            {
                return this._CollapseImageIndex;
            }
            set
            {
                this._CollapseImageIndex = value;
            }
        }

        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.IO.BufferReader stream = new IO.BufferReader(data.Data))
            {
                stream.ReadCache();
                   DataStruct ds = stream.ReadIndex(1, DataStruct.DataStructNull);
                if (ds != null)
                {
                    base.ReadDataStruct(ds);
                }
                this._CollapseImageIndex = stream.ReadIndex(2, this._CollapseImageIndex);
                this._CollapseImageKey = stream.ReadIndex(3, this._CollapseImageKey);
                this._count = stream.ReadIndex(4, this._count);
                this._ExpandImageIndex = stream.ReadIndex(5, this._ExpandImageIndex);
                this._ExpandImageKey = stream.ReadIndex(6, this._ExpandImageKey);
                this._Indent = stream.ReadIndex(7, this._Indent);
                this._keyField = stream.ReadIndex(8, this._keyField);
                this._keyParentField = stream.ReadIndex(9, this._keyParentField);
                this._NodeHeight = stream.ReadIndex(10, this._NodeHeight);
                int count = stream.ReadIndex(11, 0);
                for (int i = 0; i < count; i++)
                {
                    DataStruct dsnode = stream.ReadDataStruct();
                    DataTreeNode node = new DataTreeNode(this);
                    node.ReadDataStruct(dsnode);
                    this.Nodes.Add(node);
                }
                this._PathSeparator = stream.ReadIndex(12, this._PathSeparator); 
                this._ShowNodeToolTips = stream.ReadIndex(15, this._ShowNodeToolTips);
                this._ShowPlusMinus = stream.ReadIndex(16, this._ShowPlusMinus);
                this._ShowRootLines = stream.ReadIndex(17, this._ShowRootLines); 

            } 
        }

        public override DataStruct Data
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
                    bw.Write(1, base.Data); 
                    bw.Write(2, this._CollapseImageIndex);
                    bw.Write(3, this._CollapseImageKey);
                    bw.Write(4, this._count);
                    bw.Write(5, this._ExpandImageIndex);
                    bw.Write(6, this._ExpandImageKey);
                    bw.Write(7, this._Indent);
                    bw.Write(8, this._keyField);
                    bw.Write(9, this._keyParentField);
                    bw.Write(10, this._NodeHeight);
                    bw.Write(11, this.Nodes.Count);
                    foreach (var node in this.Nodes)
                    {
                        bw.Write(node.Data);
                    }
                    bw.Write(12, this._PathSeparator);  
                    bw.Write(15, this._ShowNodeToolTips);
                    bw.Write(16, this._ShowPlusMinus);
                    bw.Write(17, this._ShowRootLines); 
   
 

                    data.Data = bw.GetData();
                    bw.Close();
                }

                return data;
            } 
        }

        #region 方法
        public virtual DataTreeNode GetNodeAt(int x, int y)
        {
            foreach (DataTreeNode node in visibleNodes)
            {
                if (node.Rect.Contains(x, y))
                {
                    return node;
                }
            }
            return null;
        }
        public virtual DataTreeNode GetNodeAt(Point pt)
        {
            return GetNodeAt(pt.X, pt.Y);
        }

        public virtual void Expand(DataTreeNode node)
        {
            foreach (DataTreeNode n in node.Nodes)
            {
                n.IsExpanded = true;
            }
        }

        public virtual void ExpandChild(DataTreeNode node)
        {
            foreach (DataTreeNode n in node.Nodes)
            {
                n.IsExpanded = true;
                ExpandChild(n);
            }
        }

        public virtual void ExpandAll()
        {
            foreach (DataTreeNode node in this.Nodes)
            {
                ExpandChild(node);
            }
        }

        public virtual void Collapse(DataTreeNode node)
        {
            foreach (DataTreeNode n in node.Nodes)
            {
                n.IsExpanded = false;
            }
        }

        public virtual void CollapseChild(DataTreeNode node)
        {
            foreach (DataTreeNode n in node.Nodes)
            {
                n.IsExpanded = false;
                CollapseChild(n);
            }
        }

        public virtual void CollapseAll()
        {
            foreach (DataTreeNode node in this.Nodes)
            {
                CollapseChild(node);
            }
        }

        public virtual void ToTreeText(DataTreeNode treenode, System.Text.StringBuilder sb, string Separator)
        {
            Separator = Separator + "-";
            foreach (DataTreeNode node in treenode.Nodes)
            {
                string text = node.Value.ToString();

                sb.Append(Separator);
                sb.AppendLine(text);
                ToTreeText(node, sb, Separator);
            }
        }
        public virtual string ToTreeText()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DataTreeNode node in this.Nodes)
            {
                string text = node.Value.ToString();
                sb.Append("-");
                sb.AppendLine(text);
                ToTreeText(node, sb, "-");
            }
            return sb.ToString();
        }

        public virtual string GetVisibleTreeText()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DataTreeNode node in this.visibleNodes)
            {
                string text = node.Value.ToString();
                text = text.PadLeft(node.Level + text.Length, '-');
                sb.AppendLine(text);
            }
            return sb.ToString();
        }
        #endregion
    }


}
