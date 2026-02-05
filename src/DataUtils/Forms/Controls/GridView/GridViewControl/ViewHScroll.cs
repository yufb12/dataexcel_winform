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

namespace Feng.Forms.Controls.GridControl
{ 

    public class GridViewControlViewHScroll : HScrollerView
    {
        public GridViewControlViewHScroll(Feng.Forms.Controls.GridControl.GridView grid)
        {
            _Grid = grid;
        }
        private Feng.Forms.Controls.GridControl.GridView _Grid = null;
        public Feng.Forms.Controls.GridControl.GridView Grid
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
                return Grid.Width - this.Height;
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


    public class GridViewControlViewVScroll : VScrollerView
    {
        public GridViewControlViewVScroll(Feng.Forms.Controls.GridControl.GridView grid)
        {
            _Grid = grid;
        }
        private Feng.Forms.Controls.GridControl.GridView _Grid = null;
        public Feng.Forms.Controls.GridControl.GridView Grid
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
                return Grid.Height - this.Width - this.Grid.ColumnHeaderHeight;
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
                return this.Grid.ColumnHeaderHeight;
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

