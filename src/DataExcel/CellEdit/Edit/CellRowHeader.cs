using Feng.Data;
using Feng.Drawing;
using Feng.Excel.App;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellRowHeader : CellBaseEdit
    {
        public CellRowHeader(DataExcel grid)
            : base(grid)
        {
        }
        public override string ShortName { get { return "CellRowHeader"; } set { } }

        private static CellRowHeader _instance = null;
        public static CellRowHeader Instance(DataExcel grid)
        {
            if (_instance == null)
                _instance = new CellRowHeader(grid);
            return _instance;
        }
 
        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            OnDraw(cell, g);
            if (cell.Grid.Selectmode == SelectMode.RowHeightChangedMode)
            {
                if (cell.Row.Selected)
                {
                    string px = cell.Row.Height + "px";
                    Rectangle bounds2 = new Rectangle(cell.Rect.Right, cell.Rect.Bottom, 40, 25);
                    g.Graphics.FillRectangle(Brushes.Green, bounds2);
                    g.Graphics.DrawString(px, g.DefaultFont, Brushes.Red, bounds2.Location);
                }

            }

            return true;
        }

        public virtual bool OnDraw(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            string text = cell.Text;
            if (cell.Row.Index > 0 && cell.Column.Index == 0)
            {
                text = cell.Row.Name;
            }
            DrawRect(cell, g, cell.Rect, text, false);
            //if (cell.Row.Index == 13)
            //{
            //    g.Graphics.ResetClip();
            //    Rectangle rect = new Rectangle() { X = cell.Left, Y = cell.Top, Height = cell.Height, Width = cell.Width * 2 };
            //    g.Graphics.FillRectangle(Brushes.Red, rect);
            //}
            return true;
        }
        public virtual bool OnDrawBack(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return true;
        }
        private void DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
            if (value == null)
                return;
            StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
            sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
            sf.Alignment =  StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            if (cell.DirectionVertical)
            {
                sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DirectionVertical;
            }
            string text = value.ToString();

            if (cell.Column.Index != 0 && cell.Row.Index == 0)
            {
                text = cell.Row.Name;
            }
            //if (cell.Row.LockVersion == null)
            //{
            //    text = text + "#";
            //}
            if (text != string.Empty)
            {
                Rectangle rect = bounds;

                Color forecolor = cell.ForeColor;
                if (cell.Grid.FocusedCell == this)
                {
                    forecolor = cell.FocusForeColor;
                }

                if (cell.Rect.Contains(g.ClientPoint))
                {
                    forecolor = cell.MouseOverForeColor;
                }
                if (g.MouseButtons == MouseButtons.Left && cell.Grid.FocusedCell == this)
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
                if (cell.Grid.ShowCheckBox)
                {
                    bool check = cell.Row.Checked;
                    GraphicsHelper.DrawCheckBox(g.Graphics, bounds, check ? 1 : 0, text, sf, cell.ForeColor, cell.Font);
                }
                else
                {
                    if (cell.Row.LockVersion != null)
                    {

                        forecolor = Feng.Drawing.ColorHelper.Dark(Color.GreenYellow);
                        Feng.Drawing.GraphicsHelper.DrawString(g, "*", cell.Font, Brushes.Gold, rect);
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
            }
        }
        bool mousemoverefresh = false;


        Point _prepoint = Point.Empty;
        int _preheight = 0;
        public virtual bool ContainsSplit(IBaseCell cell, Point pt)
        {
            bool bolIn = false;
            int top = cell.Top + cell.Height - ConstantValue.RowHeaderSplit;
            if (top < 0)
            {
                top = 0;
            }
            Rectangle rect = new System.Drawing.Rectangle(cell.Left, top,
                cell.Width, ConstantValue.RowHeaderSplit);
            bolIn = rect.Contains(pt);
            if (bolIn)
            {
                cell.Grid.BeginSetCursor(System.Windows.Forms.Cursors.HSplit);
            }
            //else
            //{
            //    rect = new System.Drawing.Rectangle(cell.Left, cell.Top,
            //    cell.Width, ConstantValue.RowHeaderSplit);
            //    bolIn = rect.Contains(pt);
            //    if (bolIn)
            //    { 
            //        cell.Grid.BeginSetCursor(System.Windows.Forms.Cursors.HSplit);
            //    }
            //}
            return bolIn;
        }
        public void DrawRect(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool isprint)
        {
            Color backcolor = Drawing.DataColors.HeaderColor;
            if (cell.Column.Index == 0)
            {
                if (cell.Row.CellSelect)
                {
                    backcolor = cell.Row.SelectBackColor;
                } 
            }
            if (!isprint)
            {
                if (cell.Row.Index == 0 && cell.Column.Index == 0)
                {
                    GraphicsHelper.FillRectangleLinearGradient(g.Graphics, backcolor, cell.Rect
        , System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
                }
                else
                {
                    if (mousemoverefresh && bounds.Contains(cell.Grid.PointToClient(System.Windows.Forms.Control.MousePosition)))
                    {
                        backcolor = ColorHelper.Light(backcolor);
                    }
                    else
                    {
                        mousemoverefresh = false;
                    }
                    GraphicsHelper.FillRectangleLinearGradient(g.Graphics, backcolor, bounds
        , System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
                }
            }

            Rectangle rect = bounds;
            cell.Grid.DrawGridRectangle(g, rect);
            DrawCell(cell, g, bounds, value);
        }

        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (cell.Grid.EndRow == cell.Row)
            {
                if (cell.Rect.Contains(ve.ControlPoint))
                {
                    int index = cell.Grid.Height / cell.Grid.DefaultRowHeight;
                    if (index > 30)
                    {
                        //index = 30;
                    }
                    cell.Grid.FirstDisplayedRowIndex = cell.Grid.EndRow.Index - index;
                    return true;
                }
            }
            Point viewloaction = cell.Grid.PointControlToView(e.Location);
            bool _sizechanged = ContainsSplit(cell, viewloaction);
            IRow row = cell.Row;
            if (_sizechanged)
            {
                _prepoint = viewloaction;
                _preheight = cell.Height;
                cell.Grid.Selectmode = SelectMode.RowHeightChangedMode;
                cell.Grid.CellEvent = cell;
                row.Selected = true;
                return _sizechanged;
            }
            else
            {
                if (cell.Column.Index == 0)
                {
                    if (cell.Row.Index > 0)
                    {
                        cell.Row.Selected = true;
                        if (cell.Grid.ShowCheckBox)
                        {
                            Rectangle rect = new Rectangle(new Point(cell.Left + 2, cell.Top + (cell.Height / 2 - 13 / 2 - 1)),
                         new Size(13, 13));

                            if (rect.Contains(viewloaction))
                            {
                                return true;
                            }

                        }
                    }
                }
            }
            return false;
        }

        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
 
            Point viewloaction = cell.Grid.PointControlToView(e.Location);
            //if (cell.Grid.DebugName == "CellEditDataExcel")
            //{
            //    string text = "e.Location:" + e.Location.ToString() + "\r\n" +
            //        "viewloaction:" + viewloaction.ToString() + "\r\n" +
            //         "Control.MousePosition:" + System.Windows.Forms.Control.MousePosition.ToString() + "\r\n" +
            //         "cell.Rect:" + cell.Rect.ToString();
            //    Feng.Drawing.GraphicsObject.DebugText = text;
            //}
            if (cell.Grid.Selectmode == SelectMode.RowHeightChangedMode)
            {
                int rh = 0;
                rh = viewloaction.Y - this._prepoint.Y; 
                cell.Row.Height = _preheight + rh;
                cell.Grid.HeightChangedRow = cell.Row;
                cell.Grid.ReFreshFirstDisplayRowIndex();
                return false;
            }
            else
            {
                if (ContainsSplit(cell, viewloaction))
                {
                    cell.Grid.CellEvent = cell;
                    return true;
                }
            }
            return false;
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            //IBaseCell cell = sender as IBaseCell;
            //if (cell == null)
            //    return false;
            //if (cell.Grid.Selectmode == SelectMode.RowHeightChangedMode)
            //{
            //    cell.Grid.Selectmode = 0;
            //}
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
            return base.OnKeyDown(sender, e, ve);
        }
        public override ICellEditControl Clone(DataExcel grid)
        {
            CellRowHeader celledit = new CellRowHeader(grid);
            celledit._preheight = this._preheight;
            celledit._prepoint = this._prepoint;
            return celledit;
        }


    }

}
