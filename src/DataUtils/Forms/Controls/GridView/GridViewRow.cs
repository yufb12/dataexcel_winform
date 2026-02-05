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
using Feng.Forms.Views;
using Feng.Forms.Interface;

namespace Feng.Forms.Controls.GridControl
{
    [Serializable]
    public class GridViewRow : DivView, IDataBoundItem, IChecked, IIndex
    {
        public GridViewRow(GridView grid)
        {
            _grid = grid;
        }

        private CellCollection _cells = null;
        [Browsable(false)]
        public virtual CellCollection Cells
        {
            get
            {
                if (_cells == null)
                {
                    _cells = new CellCollection();
                }
                return _cells;
            }

        }
        private GridView _grid = null;
        public GridView Grid
        {
            get
            {
                return _grid;
            }
        }
        public override int Left 
        { 
            get { return 0; }
            set {
            }
        }
        public override int Top
        { 
            get { 
                return base.Top;
            }
            set { 
                base.Top = value;
            }
        }
        public override int Width { get { return this.Grid.Width; } set { } }
        public override Font Font { get { return this.Grid.Font; }  set {   } }

        public override Rectangle Rect { get { return new Rectangle( this.Left ,this.Top ,this.Width ,this.Height); } }
        [Browsable(false)]
        public virtual bool CellSelect
        {
            get
            {
                if (this.Grid.SelectCells != null)
                {
                    foreach (GridViewCell cell in this.Grid.SelectCells)
                    {
                        if (cell.Row == this)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

        }

        private int _index = 0;
        [Browsable(false)]
        public virtual int Index
        {
            get
            {
                return _index;
            }

            set { _index = value; }
        }

        private bool _checked = false;
        [Browsable(true)]
        public virtual bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        private object databounditem = null;
        public virtual object DataBoundItem
        {
            get
            {
                return databounditem;
            }
            set
            {
                databounditem = value;
            }

        }

        internal void InitDataBoundItem(object item)
        {
            databounditem = item;
        }

        private void DrawCellText(Feng.Drawing.GraphicsObject g, RectangleF rect)
        { 
            if (Index < 1)
            {
                return;
            }
            rect.Width = this.Grid.RowHeaderWidth;
            //g.Graphics.FillRectangle(Brushes.SandyBrown, rect);
            StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
            sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;

            string text = Index.ToString();

            if (text != string.Empty)
            {
                Color forecolor = this.Grid.ForeColor;
                if (forecolor == Color.Empty)
                {
                    forecolor = Color.Black;
                }
                using (SolidBrush sb = new SolidBrush(forecolor))
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g, text, this.Grid.Font, sb, rect, sf);
                }
            }
        }
        private CheckView checkView = null;
        private CheckView CheckView {
            get {
                if (checkView == null)
                {
                    checkView = new CheckView();
                }
                return checkView;
            }
        }
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.Grid.ShowCheckBox)
            {
                Point pt = e.Location;
                pt.Offset(this.Grid.Left * -1, this.Grid.Top * -1);
                bool check = CheckView.OnMouseDown(sender, e, this, pt);
                if (check)
                {
                    this.Checked = !this.Checked;
                    return true;
                }
            }
            return base.OnMouseDown(sender, e, ve);
        }

        public override string Text { get { return this.Index.ToString(); } }
        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (Index < 1)
            {
                return false;
            }
            Rectangle rect = this.Rect;
            //if (Index % 2 == 0)
            //{
            //    g.Graphics.FillRectangle(Brushes.AntiqueWhite,rect);
            //}
            //else
            //{
            //    g.Graphics.FillRectangle(Brushes.LightGray, rect);
            //}
            if (this.Grid.ShowLines)
            {
                g.Graphics.DrawLine(PenCache.BorderGray, this.Left, this.Bottom,
                    this.Grid.RowHeaderWidth, this.Bottom);
            }
            if (this.Grid.ShowRowHeader)
            {
                if (this.CellSelect)
                {
                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, this.Grid.CellSelectBrush, new Rectangle(this.Left, this.Top, this.Grid.RowHeaderWidth, this.Height));
                }
            }
            if (this.Grid.ShowCheckBox)
            {
                CheckView.OnDraw(g, this.Rect, this, this, this, this);
            }
            else if (this.Grid.ShowRowHeader)
            {
                DrawCellText(g, this.Rect);
            }
            return false;
        }

        public override DataStruct Data { get { return null; } }

 
    }
}

