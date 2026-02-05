using Feng.Forms.Controls.TreeView;
using Feng.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class TreeViewDialog : Form
    {
        public TreeViewDialog()
        {
            InitializeComponent();
        }
        public Feng.Forms.Controls.TreeView.DataTreeViewControl tree = null;
        Forms.Controls.GridControl.GridViewColumn column = null;
        public void Init()
        {
            tree = new Controls.TreeView.DataTreeViewControl();
            tree.Dock = DockStyle.Fill;
            tree.SizeChanged += tree_SizeChanged;
            column = new Controls.GridControl.GridViewColumn(tree.TreeView);
            tree.TreeView.ShowColumnHeader = false;
            tree.TreeView.ShowRowHeader = false;
            tree.TreeView.ShowRootLines = false;
            tree.TreeView.ShowLines = false;
            tree.TreeView.Columns.Add(column);  
            panelTree.Controls.Add(tree);
        }
        public void InitNodes(DataTreeNodeCollection nodes)
        {
            foreach (DataTreeNode node in nodes)
            {
                DataTreeNode newnode = new DataTreeNode(this.tree.TreeView);
                node.Clone(newnode);
                this.tree.TreeView.Nodes.Add(newnode);
                DataTreeNode.AddNode(node, newnode);
            }
            this.tree.TreeView.RefreshAll(); 
        }
        void tree_SizeChanged(object sender, EventArgs e)
        {
            if (column != null)
            {
                column.Width = tree.Width - tree.TreeView.LeftSapce;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
      
            base.OnSizeChanged(e);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                DataTreeNode node = new DataTreeNode(tree.TreeView);
                using (InputTextDialog dlg = new InputTextDialog())
                {

                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        node.Text = dlg.Value; 
                        tree.TreeView.Nodes.Add(node);
                        tree.TreeView.RefreshColumns();
                        tree.TreeView.RefreshRows();
                        tree.TreeView.RefreshRowHeight();
                        tree.TreeView.RefreshVisible();
                        tree.TreeView.RefreshRowValue();
                        int i = tree.TreeView.FirstColumn;
                        DataTreeRow row = tree.TreeView.GetRowByNode(node);
                        if (row.Cells.Count > 0)
                        { 
                            this.tree.TreeView.FocusedCell = row.Cells[0];
                        }
                        this.propertyGrid1.SelectedObject = node;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
  
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tree.TreeView.SelectedNode != null)
                {
                    DataTreeNode pnode = tree.TreeView.SelectedNode;
                    DataTreeNode node = new DataTreeNode(tree.TreeView);
                    using (InputTextDialog dlg = new InputTextDialog())
                    {
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            node.Text = dlg.Value;
                            pnode.Nodes.Add(node);
                            node.Parent = pnode;
                            tree.TreeView.RefreshAll();
                            this.propertyGrid1.SelectedObject = node;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Control.ModifierKeys != Keys.Shift)
                {
                    if (Feng.Utils.MsgBox.ShowQuestion("确定要删除?") != System.Windows.Forms.DialogResult.OK)
                    {

                        return;
                    }
                }
                DataTreeNode node = this.tree.TreeView.SelectedNode;
                if (node != null)
                {
                    if (node.Parent != null)
                    {
                        node.Parent.Nodes.Remove(node);
                    }
                    else
                    {
                        this.tree.TreeView.Nodes.Remove(node);
                    }
                    this.tree.TreeView.RefreshAll();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
  
        }
        
    }
}
