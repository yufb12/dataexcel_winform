using Feng.Excel.Collections;
using Feng.Excel.Edits;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.GridControl;
using Feng.Forms.Controls.TreeView;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils;
using System;
using System.Data;

namespace Feng.Excel.Script
{
    [Serializable]
    public class CellEditTreeViewFunctionContainer : DataExcelMethodContainer
    {

        public const string Function_Category = "CellEditTreeView";
        public const string Function_Description = "树编辑控件";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public CellEditTreeViewFunctionContainer()
        {
            BaseMethod model = null;


            model = new BaseMethod();
            model.Name = "TreeViewNew";
            model.Description = @"创建单元格控件 TreeViewNew(""CELLID"")";
            model.Eg = @"TreeViewNew(tablename,datatable)";
            model.Function = TreeViewNew;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TreeView";
            model.Description = @"获取单元格控件 TreeView(""CELLID"")";
            model.Eg = @"TreeView(""CELLID"")";
            model.Function = TreeView;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TreeViewDataSource";
            model.Description = @"设置数据源 TreeViewDataSource(""CELLID"",DATATABLE)";
            model.Eg = @"TreeViewDataSource(""CELLID"",DATATABLE)";
            model.Function = TreeViewDataSource;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TreeViewFocusedNode";
            model.Description = @"获取焦点节点 TreeViewFocusedNode(""CELLID"")";
            model.Eg = @"TreeViewFocusedNode(""CELLID"")";
            model.Function = TreeViewFocusedNode;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TreeViewNodeText";
            model.Description = @"获取节点文本 TreeViewNodeText(node)";
            model.Eg = @"TreeViewNodeText(node)";
            model.Function = TreeViewNodeText;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TreeViewNodeBingdingItem";
            model.Description = @"获取或设置单元格的值 TreeViewNodeBingdingItem(node)";
            model.Eg = @" var row=TreeViewNodeBingdingItem(node)";
            model.Function = TreeViewNodeBingdingItem;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "TreeViewNode";
            model.Description = @"获取子节点 TreeViewNode(""node"",3)";
            model.Eg = @"var node=TreeViewNode(""node"")";
            model.Function = TreeViewNode;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TreeViewChildNodeCount";
            model.Description = @"获取子节点数 TreeViewChildNodeCount(node)";
            model.Eg = @"var nodecount=TreeViewChildNodeCount(node)";
            model.Function = TreeViewChildNodeCount;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TreeViewKeyField";
            model.Description = @"获取或设置节点键值字段 TreeViewKeyField(node)";
            model.Eg = @"TreeViewKeyField(node)";
            model.Function = TreeViewKeyField;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TreeViewKeyParentField";
            model.Description = @"获取或设置父节点键值字段 TreeViewKeyParentField(node)";
            model.Eg = @"TreeViewKeyParentField(node)";
            model.Function = TreeViewKeyParentField;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TreeViewKeyDisplayField";
            model.Description = @"获取或设置节点显示值字段 TreeViewKeyDisplayField(node)";
            model.Eg = @"TreeViewKeyDisplayField(node)";
            model.Function = TreeViewKeyDisplayField;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TreeViewNodeAdd";
            model.Description = @"添加节点 TreeViewNodeAdd(text,value)";
            model.Eg = @"TreeViewNodeAdd(text,value)";
            model.Function = TreeViewNodeAdd;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TreeViewNodeDelete";
            model.Description = @"删除节点 TreeViewNodeDelete(node)";
            model.Eg = @"TreeViewNodeDelete(node)";
            model.Function = TreeViewNodeDelete;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TreeViewNodeFind";
            model.Description = @"查找节点 TreeViewNodeFind(node)";
            model.Eg = @"TreeViewNodeFind(node)";
            model.Function = TreeViewNodeFind;
            MethodList.Add(model);
        }

        public virtual object TreeViewNew(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
            if (cell == null)
                return null;
            CellTreeView gridView = new CellTreeView(cell);
            cell.OwnEditControl = gridView;
            return gridView;
        }
        public virtual object TreeView(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
            if (cell == null)
                return null;
            return cell.OwnEditControl;
        }
        public virtual object TreeViewDataSource(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return Feng.Utils.Constants.Fail;
            }
 
            CellTreeView gridView = GetGridView(proxy, args);
            if (gridView == null)
                return Feng.Utils.Constants.Fail;
            DataTable dataTable = GetArgIndex(2, args) as DataTable;
            string KeyField = base.GetTextValue(3, args);
            string KeyParentField = base.GetTextValue(4, args);
            string DisplayField = base.GetTextValue(5, args);
            if (!string.IsNullOrWhiteSpace(KeyField))
            {
                gridView.KeyField = KeyField;
            }
            if (!string.IsNullOrWhiteSpace(KeyParentField))
            {
                gridView.KeyParentField = KeyParentField;
            }
            if (!string.IsNullOrWhiteSpace(DisplayField))
            {
                gridView.DisplayField = DisplayField;
            }
            gridView.DataSource = dataTable;  
            gridView.RefreshRowValue();
            return Feng.Utils.Constants.OK;
        }
        public virtual object TreeViewFocusedNode(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            CellTreeView cellGridView = GetGridView(proxy, args);
            if (cellGridView != null)
            {
                object value = cellGridView.FocusedNode;
                return value;
            }
            return null;
        }
        public virtual object TreeViewNodeText(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }

            DataTreeNode node = base.GetArgIndex(1, args) as DataTreeNode;
            if (node != null)
            {
                if (args.Length > 2)
                {
                    string text = base.GetTextValue(2, args);
                    node.Text = text;
                }
                return node.Text;
            }
            return null;
        }
        public virtual object TreeViewNodeBingdingItem(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }

            DataTreeNode node = base.GetArgIndex(1, args) as DataTreeNode;
            if (node != null)
            {
                return node.DataBoundItem;
            }
            return null;
        }
        public virtual object TreeViewChildNodeCount(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }

            DataTreeNodeCollection treeNodes = null;
            CellTreeView cellGridView = null;
            DataTreeNode node = base.GetArgIndex(1, args) as DataTreeNode;
            if (node != null)
            {
                treeNodes = node.Nodes;
                cellGridView = node.TreeView as CellTreeView;
            }
            else
            {
                cellGridView = GetGridView(proxy, args);
                if (cellGridView != null)
                {
                    treeNodes = cellGridView.Nodes;
                }
            }
            return treeNodes.Count;
        }
        public virtual object TreeViewNode(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }

            DataTreeNode node = base.GetArgIndex(1, args) as DataTreeNode;
            if (node != null)
            {
                int index = base.GetIntValue(2, args);
                return node.Nodes[index];
            }
            else
            {
                CellTreeView cellGridView = GetGridView(proxy, args);
                if (cellGridView != null)
                {
                    int index = base.GetIntValue(2, args);
                    return cellGridView.Nodes[index];
                }
            }
            return null;
        }
        public virtual object TreeViewKeyField(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }

            CellTreeView cellGridView = GetGridView(proxy, args);
            if (cellGridView != null)
            {
                if (args.Length > 2)
                {
                    string text = base.GetTextValue(2, args);
                    cellGridView.KeyField = text;
                }
                return cellGridView.KeyField;
            }
            return null;
        }
        public virtual object TreeViewKeyParentField(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }

            CellTreeView cellGridView = GetGridView(proxy, args);
            if (cellGridView != null)
            {
                if (args.Length > 2)
                {
                    string text = base.GetTextValue(2, args);
                    cellGridView.KeyParentField = text;
                }
                return cellGridView.KeyParentField;
            }
            return null;
        }
        public virtual object TreeViewKeyDisplayField(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }

            CellTreeView cellGridView = GetGridView(proxy, args);
            if (cellGridView != null)
            {
                if (args.Length > 2)
                {
                    string text = base.GetTextValue(2, args);
                    cellGridView.DisplayField = text;
                }
                return cellGridView.DisplayField;
            }
            return null;
        }
        public virtual object TreeViewNodeAdd(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            DataTreeNodeCollection treeNodes = null;
            CellTreeView cellGridView = null;
            DataTreeNode node = base.GetArgIndex(1, args) as DataTreeNode;
            if (node != null)
            {
                treeNodes = node.Nodes;
                cellGridView = node.TreeView as CellTreeView; 
            }
            else
            {
                cellGridView = GetGridView(proxy, args);
                if (cellGridView != null)
                {
                    treeNodes = cellGridView.Nodes;
                }
            }
            string text = base.GetTextValue(2, args);
            object value = base.GetArgIndex(3, args);
            object tag = base.GetArgIndex(4, args);
            if (value == null)
            {
                value = text;
            }
            DataTreeNode newNode = new DataTreeNode(cellGridView);
            newNode.Text = text;
            newNode.Value = value;
            newNode.Tag = tag;
            if (node != null)
            {
                newNode.Parent = node;
            } 
            treeNodes.Add(newNode);
            return newNode;
        }
        public virtual object TreeViewNodeDelete(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            } 
            DataTreeNode node = base.GetArgIndex(1, args) as DataTreeNode;
            if (node != null)
            {
                if (node.Parent == null)
                {
                    node.TreeView.Nodes.Remove(node);
                }
                else
                {
                    node.Parent.Nodes.Remove(node);
                }
                return Feng.Utils.Constants.OK;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object TreeViewNodeFind(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }

            DataTreeNodeCollection treeNodes = null;
            CellTreeView cellGridView = null;
            DataTreeNode node = base.GetArgIndex(1, args) as DataTreeNode;
            if (node != null)
            {
                treeNodes = node.Nodes;
                cellGridView = node.TreeView as CellTreeView;
            }
            else
            {
                cellGridView = GetGridView(proxy, args);
                if (cellGridView != null)
                {
                    treeNodes = cellGridView.Nodes;
                }
            }
            object value = base.GetArgIndex(2, args);
            bool haschild = base.GetBooleanValue(3, args);
            return FindNode(treeNodes, value, haschild); 
        }
        public DataTreeNode FindNode(DataTreeNodeCollection treeNodes,object value,bool haschild)
        {
            foreach (DataTreeNode item in treeNodes)
            {
                if (item.Value.Equals(value))
                {
                    return item;
                }
                if (haschild)
                {
                    return FindNode(item.Nodes, value, haschild);
                }
            }
            return null;
        }
        private CellTreeView GetGridView(ICBContext proxy, params object[] args)
        {
            CellTreeView cellGridView = base.GetArgIndex(1, args) as CellTreeView;
            if (cellGridView == null)
            {
                ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
                if (cell == null)
                    return null;
                cellGridView = cell.OwnEditControl as CellTreeView;
            }
            return cellGridView;
        }
    }
}
