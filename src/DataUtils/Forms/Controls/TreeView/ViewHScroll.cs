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
using Feng.Forms.Interface;
using Feng.Print;

namespace Feng.Forms.Controls.TreeView
{ 

    public class DataTreeViewHScroll : HScrollerView
    {
        public DataTreeViewHScroll(DataTreeView grid)
        {
            _Grid = grid;
        }
        private DataTreeView _Grid = null;
        public DataTreeView Grid
        {
            get
            {
                return _Grid;
            }
        }

        public override int Top
        {
            get
            {
                return Grid.Height - this.Height;
            }
            set
            {
            }
        }

        public override int Left
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public override int Width
        {
            get
            {
                int rowwidth = 0; 
                int vscrollwidth = 0;
                if (this.Grid.VScroll.Visible)
                {
                    vscrollwidth = this.Grid.VScroll.Width;
                }
                return Grid.Width - vscrollwidth - rowwidth;
            }
            set
            {
            }
        }

        public override void OnValueChanged(int value)
        {
            Grid.SetFirstColumn(value);
            Grid.RefreshColumns();
            base.OnValueChanged(value);
        }
    }


    public class DataTreeViewVScroll : VScrollerView
    {
        public DataTreeViewVScroll(DataTreeView grid)
        {
            _Grid = grid;
        }
        private DataTreeView _Grid = null;
        public DataTreeView Grid
        {
            get
            {
                return _Grid;
            }
        }
        public override int Height
        {
            get
            {
                int colheight = 0; 
                int hscrollheight = 0;
                if (this.Grid.HScroll.Visible)
                {
                    hscrollheight = this.Grid.HScroll.Height;
                }
                return Grid.Height - hscrollheight - colheight;
            }
            set
            {
            }
        }

        public override int Left
        {
            get
            {
                return Grid.Width - this.Width;
            }
            set
            {
            }
        }

        public override int Top
        {
            get
            {
                int top = 0; 
                return top;
            }
            set
            {
            }
        }

        public override void OnValueChanged(int value)
        {
            Grid.SetPosition(value);
            base.OnValueChanged(value);
        }
    }
}

