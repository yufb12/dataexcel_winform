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
using Feng.Forms.Controls.TreeView;
using Feng.Forms.Controls.GridControl;

namespace Feng.Forms.Controls.TreeView
{
    public partial class DataTreeViewBase  
    {
        public event BeforeNodeExpandHandler BeforeNodeExpand;
        public virtual void OnBeforeNodeExpand(BaseTreeViewNodeCancelArgs e)
        {
            if (BeforeNodeExpand != null)
            {
                BeforeNodeExpand(this, e);
            }
        }
        public event NodeExpandHandler NodeExpand;
        public virtual void OnNodeExpand(DataTreeNode node)
        {
            if (NodeExpand != null)
            {
                NodeExpand(this, node);
            }
        }
        public virtual void OnFocusedNodeChanged(DataTreeNode node)
        {
            if (FocusedNodeChanged != null)
            {
                FocusedNodeChanged(this, node);
            }
        }
        public virtual void OnNodeDoubleClick(DataTreeNode node)
        {
            if (NodeDoubleClick != null)
            {
                NodeDoubleClick(this, node);
            }
        }
        public event FocusedNodeChangedHandler FocusedNodeChanged;
        public event NodeDoubleClickHandler NodeDoubleClick;

    }
    public delegate void FocusedNodeChangedHandler(object sender, DataTreeNode node);
    public delegate void BeforeNodeExpandHandler(object sender, BaseTreeViewNodeCancelArgs e);
    public delegate void NodeExpandHandler(object sender,DataTreeNode node);
    public delegate void NodeDoubleClickHandler(object sender, DataTreeNode node);
    public class BaseTreeViewNodeCancelArgs : CancelEventArgs
    {
        public DataTreeNode Node { get; set; }
    }
    public class BeforeNodeExpandArgs : BaseTreeViewNodeCancelArgs
    {

    }
}

