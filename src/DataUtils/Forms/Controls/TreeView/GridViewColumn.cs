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
using Feng.Forms.ComponentModel;
using Feng.Data;
using Feng.Forms.Interface;
using Feng.Enums;
using Feng.Forms.Controls.TreeView;

namespace Feng.Forms.Controls.GridControl
{

    [Serializable]
    public class TreeViewColumn : GridViewColumn
    {
        public TreeViewColumn(DataTreeViewBase grid)
            : base(grid)
        {

        }
        internal TreeViewColumn(GridView grid)
            : base(grid)
        {

        } 
    }


    [Serializable]
    public class TreeViewSingleColumn : GridViewColumn
    {
        public TreeViewSingleColumn(DataTreeViewBase grid)
            : base(grid)
        {

        }
        internal TreeViewSingleColumn(GridView grid)
            : base(grid)
        {

        }
        public override int Width
        {
            get
            {
                if (this.Grid != null)
                {
                    return this.Grid.Width;
                }
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }
    }
}

