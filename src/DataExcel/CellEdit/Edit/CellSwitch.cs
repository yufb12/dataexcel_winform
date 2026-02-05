using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Views;
using Feng.Print;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellSwitch : CellBaseEdit
    {
        public CellSwitch(DataExcel grid)
            : base(grid)
        {
        }
        public override string ShortName { get { return "CellSwitch"; } set { } }


        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            DrawRect(cell, g, cell.Rect, cell.Text);
            return true;
        }

        private Color _bkcolor = Color.Gray;
        [Category(CategorySetting.PropertyDesign)]
        public Color SwitchBackColor
        {
            get { return _bkcolor; }
            set { _bkcolor = value; }
        }

        private Color _color1 = Color.DeepSkyBlue;
        [Category(CategorySetting.PropertyDesign)]
        public Color OnColor
        {
            get { return _color1; }
            set { _color1 = value; }
        }

        private Color _color2 = Color.LightSteelBlue;
        [Category(CategorySetting.PropertyDesign)]
        public Color OffColor
        {
            get { return _color2; }
            set { _color2 = value; }
        }

        private int _maxwidth = 60;
        [Category(CategorySetting.PropertyDesign)]
        public int MaxWidth
        {
            get { return _maxwidth; }
            set { _maxwidth = value; }
        }

        private int _maxheight = 18;
        [Category(CategorySetting.PropertyDesign)]
        public int MaxHeight
        {
            get { return _maxheight; }
            set { _maxheight = value; }
        }

        private void DrawRect(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
            GraphicsState gs = g.Graphics.Save();
            g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle rect = bounds;
            if (System.Windows.Forms.Control.MouseButtons == System.Windows.Forms.MouseButtons.Left)
            {

            }
            int width = this.MaxWidth;
            if (cell.Width < this.MaxWidth)
            {
                width = cell.Width;
            }
            int height = this.MaxHeight;
            if (cell.Height < this.MaxHeight)
            {
                height = cell.Height;
            }
            int left = GetLeft(width, cell.Rect, cell.HorizontalAlignment);
            int top = GetTop(height, cell.Rect, cell.VerticalAlignment);
            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics,
                Feng.Drawing.SolidBrushCache.GetSolidBrush(this.SwitchBackColor), left, top, width, height);
            bool v = Feng.Utils.ConvertHelper.ToBoolean(value);
            if (v)
            {
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics,
      Feng.Drawing.SolidBrushCache.GetSolidBrush(this.OnColor), left + width / 2, top, width / 2, height);
            }
            else
            {
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics,
      Feng.Drawing.SolidBrushCache.GetSolidBrush(this.OffColor), left, top, width / 2, height);
            }
            Feng.Drawing.GraphicsHelper.DrawRectangle(g.Graphics,
    Feng.Drawing.PenCache.GetPen(this.SwitchBackColor), left, top, width, height);
            g.Graphics.Restore(gs);
        }

        public int GetLeft(int width, Rectangle rect, StringAlignment align)
        {
            switch (align)
            {
                case StringAlignment.Near:
                    return rect.Left;
                case StringAlignment.Center:
                    return rect.Left + (rect.Width - width) / 2;
                case StringAlignment.Far:
                    return rect.Right - width;
                default:
                    return rect.Left + (rect.Width - width) / 2;
            }
        }

        public int GetTop(int height, Rectangle rect, StringAlignment align)
        {
            switch (align)
            {
                case StringAlignment.Near:
                    return rect.Top;
                case StringAlignment.Center:
                    return rect.Top + (rect.Height - height) / 2;
                case StringAlignment.Far:
                    return rect.Bottom - height;
                default:
                    return rect.Top + (rect.Height - height) / 2;
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
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            bool result = false;
            if (cell != null)
            {
                cell.Grid.BeginReFresh();
                int width = this.MaxWidth;
                if (cell.Width < this.MaxWidth)
                {
                    width = cell.Width;
                }
                int height = this.MaxHeight;
                if (cell.Height < this.MaxHeight)
                {
                    height = cell.Height;
                }
                int left = GetLeft(width, cell.Rect, cell.HorizontalAlignment);
                int top = GetTop(height, cell.Rect, cell.VerticalAlignment);
                Rectangle rect = new Rectangle(left + width / 2, top, width / 2, height);
                Point viewloaction = this.Grid.PointControlToView(e.Location);
                if (rect.Contains(viewloaction))
                {
                    cell.Value = true;
                }
                rect = new Rectangle(left, top, width / 2, height);
                if (rect.Contains(viewloaction))
                {
                    cell.Value = false;
                }
                cell.Grid.EndReFresh();
            }
            base.OnMouseClick(sender, e, ve);
            return result;
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
            CellSwitch celledit = new CellSwitch(grid);
            celledit._color1 = this._color1;
            celledit._color2 = this._color2;
            return celledit;
        }
    }

}
