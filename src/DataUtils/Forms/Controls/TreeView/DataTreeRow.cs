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
    public class DataTreeRow : GridViewRow
    {
        public DataTreeRow(DataTreeViewBase tree)
            : base(tree)
        {

        }
        private DataTreeCellCollection _cells = null;
        public override CellCollection Cells
        {
            get
            {
                if (_cells == null)
                {
                    _cells = new DataTreeCellCollection();
                }
                return _cells;
            }
        }

        private DataTreeNode _node = null;
        public virtual DataTreeNode Node
        {
            get {
                return _node;
            }
            set {
                _node = value;
            }
        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            if (this.Node == null)
            {
                return true;
            }
            return base.OnDraw(this, g);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public void DrawDebugRect(Graphics g)
        {
            if (GraphicsHelper.ShowDebugDrawRect)
            {
                GraphicsHelper.DrawDebugRect(g,this.Rect, this.GetType().Name,"");
            }
        }
    }
 
}
