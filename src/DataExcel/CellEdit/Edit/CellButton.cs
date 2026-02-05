using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Feng.Print;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellButton : CellBaseEdit
    {
        public CellButton(DataExcel grid)
            : base(grid)
        {
        }
        public override string ShortName { get { return "CellButton"; } set { } }

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            DrawRect(cell, g, cell.Rect, cell.Text);
            return true;
        }

        private Color _color1 = Color.White;
        [Category(CategorySetting.PropertyDesign)]
        public Color Color1
        {
            get { return _color1; }
            set { _color1 = value; }
        }
        private Color _color2 = Color.Lavender;
        [Category(CategorySetting.PropertyDesign)]

        public Color Color2
        {
            get { return _color2; }
            set { _color2 = value; }
        }
        private LinearGradientMode _GradientMode = LinearGradientMode.Vertical;
        [Category(CategorySetting.PropertyDesign)]
        public LinearGradientMode GradientMode
        {
            get { return _GradientMode; }
            set { _GradientMode = value; }
        }
        private bool _drawborder = true;
        [Category(CategorySetting.PropertyUI)]
        public virtual bool DrawBorder
        {
            get { return _drawborder; }
            set { _drawborder = value; }
        }
        private int _borderwidth = 1;
        [Category(CategorySetting.PropertyUI)]
        public int BorderWidth
        {
            get { return _borderwidth; }
            set { _borderwidth = value; }
        }
        private Color _bordercolor = Color.DarkGray;
        [Category(CategorySetting.PropertyUI)]
        public Color BorderColor
        {
            get { return _bordercolor; }
            set { _bordercolor = value; }
        }
        private int _radius = 6;
        [Category(CategorySetting.PropertyUI)]
        public int Radius
        {

            get { return _radius; }
            set { _radius = value; }
        }

        private bool _gradient = false;
        [Category(CategorySetting.PropertyUI)]
        public virtual bool Gradient
        {
            get { return _gradient; }
            set { _gradient = value; }
        }

        private System.Windows.Forms.Padding _padding = System.Windows.Forms.Padding.Empty;
        [Category(CategorySetting.PropertyUI)]
        public virtual System.Windows.Forms.Padding Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }

        private void DrawRect(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
            GraphicsState gs = g.Graphics.Save();
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle rect = bounds;
            rect.X = rect.X + this.Padding.Left;
            rect.Y = rect.Y + this.Padding.Top;
            rect.Width = rect.Width - this.Padding.Left - this.Padding.Right;
            rect.Height = rect.Height - this.Padding.Top - this.Padding.Bottom;
            Point pt = g.ClientPoint;
            if (g.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {
                if (rect.Contains(pt))
                {
                    if (this.Gradient)
                    {
                        GraphicsHelper.FillRectangleLinearGradient(g.Graphics, Color2, Color1,
                            rect.Left, rect.Top, rect.Width, rect.Height,
                            GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
                    }
                    else
                    {
                        GraphicsHelper.FillRectangle(g.Graphics, Color2,
                      rect.Left, rect.Top, rect.Width, rect.Height,
                       DrawBorder, BorderWidth, BorderColor, Radius);
                    }
                }
                else
                {
                    if (this.Gradient)
                    {
                        GraphicsHelper.FillRectangleLinearGradient(g.Graphics, Color1, Color2,
                        rect.Left, rect.Top, rect.Width, rect.Height,
                        GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
                    }
                    else
                    {
                        GraphicsHelper.FillRectangle(g.Graphics, Color1,
                      rect.Left, rect.Top, rect.Width, rect.Height,
                       DrawBorder, BorderWidth, BorderColor, Radius);
                    }
                }
            }
            else
            {
                if (rect.Contains(pt))
                {
                    if (this.Gradient)
                    {
                        GraphicsHelper.FillRectangleLinearGradient(g.Graphics,  Color1, Color2,
                        rect.Left, rect.Top, rect.Width, rect.Height,
                        GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
                    }
                    else
                    {
                        GraphicsHelper.FillRectangle(g.Graphics,  Color1,
 rect.Left, rect.Top, rect.Width, rect.Height,
  DrawBorder, BorderWidth, BorderColor, Radius);
                    }
                }
                else
                {
                    if (this.Gradient)
                    {
                        GraphicsHelper.FillRectangleLinearGradient(g.Graphics, Color1, Color2,
                        rect.Left, rect.Top, rect.Width, rect.Height,
                        GradientMode, DrawBorder, BorderWidth, BorderColor, Radius);
                    }
                    else
                    {
                        GraphicsHelper.FillRectangle(g.Graphics, Color1,
        rect.Left, rect.Top, rect.Width, rect.Height,
         DrawBorder, BorderWidth, BorderColor, Radius);
                    }
                }
            }
            DrawCell(cell, g, bounds, value);
            g.Graphics.Restore(gs);
        }

        private void DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
            if (cell.Text == string.Empty)
            {
                return;
            }

            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(
                 cell.HorizontalAlignment, cell.VerticalAlignment, cell.DirectionVertical);
            string text = value.ToString();

            Rectangle rect = bounds;
            rect.X = rect.X + this.Padding.Left;
            rect.Y = rect.Y + this.Padding.Top;
            rect.Width = rect.Width - this.Padding.Left - this.Padding.Right;
            rect.Height = rect.Height - this.Padding.Top - this.Padding.Bottom;

            Color forecolor = cell.ForeColor;
            if (cell.Grid.FocusedCell == this)
            {
                forecolor = cell.FocusForeColor;
            }

            if (cell.Rect.Contains(g.ClientPoint))
            {
                forecolor = cell.MouseOverForeColor;
            }
            if (g.MouseButtons == MouseButtons.Left && this.Grid.FocusedCell == this)
            {
                forecolor = cell.MouseDownForeColor;
            }
            if (cell.Selected)
            {
                forecolor = cell.SelectForceColor;
            } 
            if (forecolor == Color.Empty)
            {
                forecolor = Color.Black;
            }
            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                rect.Location = new Point(rect.Location.X, rect.Location.Y + 2);
                if (cell.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g, text, cell.Font, sb, rect);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g, text, cell.Font, sb, rect, sf);

                }
            }
        }

        public override bool DrawCellBack(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }

        public virtual bool PrintCellBack(IBackCell cell, PrintArgs g)
        {
            return false;
        }

        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0);
                this._gradient = bw.ReadIndex(2, _gradient);
                this._color1 = bw.ReadIndex(3, _color1);
                this._color2 = bw.ReadIndex(4, _color2);
                this._borderwidth = bw.ReadIndex(5, _borderwidth);
                this._radius = bw.ReadIndex(6, _radius);
                this._bordercolor = bw.ReadIndex(7, _bordercolor);
                this._drawborder = bw.ReadIndex(8, _drawborder);
                this._GradientMode = (LinearGradientMode)bw.ReadIndex(9, (int)_GradientMode);
                this._padding = bw.ReadIndex(10, _padding);

            }
        }

        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = t.FullName,
                    Name = t.Name,
                };

                using (Feng.Excel.IO.BinaryWriter bw = new Feng.Excel.IO.BinaryWriter())
                {
                    bw.Write(1, this.AddressID);
                    bw.Write(2, _gradient);
                    bw.Write(3, _color1);
                    bw.Write(4, _color2);
                    bw.Write(5, _borderwidth);
                    bw.Write(6, _radius);
                    bw.Write(7, _bordercolor);
                    bw.Write(8, _drawborder);
                    bw.Write(9, (int)_GradientMode);
                    bw.Write(10, _padding);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (e.Alt || e.Control || e.Shift)
            {

            }
            else
            {
                if (e.KeyData == Keys.Right)
                {
                    cell.Grid.MoveFocusedCellToRightCell();
                }
                if (e.KeyData == Keys.Left)
                {
                    cell.Grid.MoveFocusedCellToLeftCell();
                }
            }
            return false;
        }

        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            cell.Grid.BeginReFresh();
            cell.Grid.EndReFresh();
            return base.OnMouseMove(sender, e, ve);
        }

        public override bool PrintCellBack(IBaseCell cell, PrintArgs e)
        {
            return false;
        }

        public override bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawRect(cell, gob, rect, cell.Value);
            return true;
        }

        public override bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawRect(cell, gob, rect, cell.Value);
            return true;
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellButton celledit = new CellButton(grid);
            celledit._bordercolor = this._bordercolor;
            celledit._borderwidth = this._borderwidth;
            celledit._color1 = this._color1;
            celledit._color2 = this._color2;
            celledit._drawborder = this._drawborder;
            celledit._GradientMode = this._GradientMode;
            celledit._radius = this._radius;
            celledit._gradient = this._gradient;
            celledit._padding = this._padding;
            return celledit;
        }

    }

}
