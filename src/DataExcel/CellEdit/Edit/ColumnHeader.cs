using Feng.Data;
using Feng.Drawing;
using Feng.Excel.App;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellColumnHeader : CellBaseEdit
    {
        public CellColumnHeader(DataExcel grid)
            : base(grid)
        {
        }

        public override string ShortName { get { return "CellColumnHeader"; } set { } }

        private static CellColumnHeader _instance = null;
        public static CellColumnHeader Instance(DataExcel grid)
        {
            if (_instance == null)
                _instance = new CellColumnHeader(grid);
            return _instance;

        }

        bool mousemoverefresh = false;

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        { 
            string text = cell.Text;
            if (cell.Column.Index > 0 && cell.Row.Index == 0)
            {
                if (cell.Column.Caption != string.Empty)
                {
                    text = cell.Column.Caption;
                }
                else
                {
                    text = cell.Column.Name;
                }
            }
            DrawRect(cell, g, cell.Rect, text, false);

            if (cell.Grid.Selectmode == SelectMode.ColumnWidthChangedMode)
            {
                if (cell.Column.Selected)
                {
                    string px = cell.Column.Width + "px";
                    Rectangle bounds2 = new Rectangle(cell.Rect.Right, cell.Rect.Bottom, 40, 25);
                    g.Graphics.FillRectangle(Brushes.Green, bounds2);
                    g.Graphics.DrawString(px, g.DefaultFont, Brushes.Red, bounds2.Location);
                }

            }
            return true;
        }

        private void DrawRect(IBaseCell cel, Feng.Drawing.GraphicsObject g, Rectangle bounds, object value, bool isprint)
        {
            ICell cell = cel as ICell; 
            Color backcolor = Drawing.DataColors.HeaderColor;

            //this.BackColor = DataColors.HeaderColor;
            //this.HorizontalAlignment = StringAlignment.Center;
            //this.VerticalAlignment = StringAlignment.Center;
            if (cell.Row.Index == 0)
            {
                if (cell.Column.CellSelect)
                {
                    backcolor = cell.Column.SelectBackColor;
                } 
            }

            if (mousemoverefresh && bounds.Contains(cell.Grid.PointToClient(System.Windows.Forms.Control.MousePosition)))
            {
                backcolor = ColorHelper.Light(backcolor);
            }
            else
            {
                mousemoverefresh = false;
            }

            if (!isprint)
            {
                GraphicsHelper.FillRectangleLinearGradient(g.Graphics, backcolor, bounds
                    , System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            }

            Rectangle rect = bounds;
            Color c = ColorHelper.Dark(backcolor);
            cell.Grid.DrawGridRectangle(g, rect.Left, rect.Top, rect.Width, rect.Height);

            if (cell.Grid.DataSource != null)
            {
                if (cell.Column.ID != string.Empty)
                {
                    Rectangle rectsort = GetSortRect(cell);
                    Rectangle rectfilter = GetFilterRect(cell);
                    Rectangle rectf = new Rectangle(bounds.Left, bounds.Top, bounds.Width - rectsort.Width - rectfilter.Width, bounds.Height);
                    DrawCell(cell, g, rectf, value);
                    if (cell.Row.Index == 0)
                    {
                        SmoothingMode sm = g.Graphics.SmoothingMode;
                        g.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        if (cell.Column.Order == Feng.Forms.ComponentModel.SortOrder.Ascending)
                        {
                            GraphicsPath path = GraphicsHelper.GetTriangle(rectsort
                                , Feng.Drawing.Orientation.Top);
                            backcolor = ColorHelper.InvertColors(backcolor);
                            GraphicsHelper.FillColorPath(g.Graphics, backcolor, rectsort, path,
                                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Top);

                        }
                        else if (cell.Column.Order == Feng.Forms.ComponentModel.SortOrder.Descending)
                        {
                            GraphicsPath path = GraphicsHelper.GetTriangle(rectsort
                                , Feng.Drawing.Orientation.Bottom);

                            backcolor = ColorHelper.InvertColors(backcolor);
                            GraphicsHelper.FillColorPath(g.Graphics, backcolor, rectsort, path,
                                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Bottom);
                        }
                        g.Graphics.SmoothingMode = sm;
                        Point pt = cell.Grid.PointToClient(System.Windows.Forms.Control.MousePosition);

                        if (rectfilter.Contains(pt))
                        {
                            g.Graphics.DrawImage(Feng.Excel.Properties.Resources.AdvancedFilterDialog1, rectfilter);
                        }
                    }
                }
            }
            else
            {
                DrawCell(cell, g, rect, value);
            }
        }

        private Rectangle GetSortRect(IBaseCell cell)
        {

            if (cell.Column.CellSelect || cell.Column.Order != Feng.Forms.ComponentModel.SortOrder.Null)
            {
                Rectangle rectsort = new Rectangle(cell.Right - 18, cell.Top + 4, 9, 9);
                if (rectsort.Height > cell.Height)
                {
                    return Rectangle.Empty;
                }
                if (rectsort.Width > cell.Width)
                {
                    return Rectangle.Empty;
                }
                return rectsort;
            }
            return Rectangle.Empty;

        }

        private Rectangle GetFilterRect(IBaseCell cell)
        {
            Rectangle rectsort = new Rectangle(cell.Right - 11, cell.Top + 4, 9, 9);
            if (rectsort.Height > cell.Height)
            {
                return Rectangle.Empty;
            }
            if (rectsort.Width > cell.Width)
            {
                return Rectangle.Empty;
            }
            return rectsort;

        }
        private void DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle rect, object value)
        {
            string text = string.Empty;
            if (cell.Column.Index == 0 && cell.Row.Index != 0)
            {
                text = cell.Column.Name;
            }
            if (value != null)
            {
                text = value.ToString();
            }
            this.DrawCellText(cell, g, rect, text);
        }
        public void DrawCellText(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle rect, string text)
        {
            if (cell == null)
                return;
            if (string.IsNullOrEmpty(text))
                return;
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(StringAlignment.Center, StringAlignment.Center, cell.DirectionVertical);

            Color forecolor = cell.ForeColor;
            if (cell.Grid.FocusedCell == cell)
            {
                forecolor = cell.FocusForeColor;
            }

            if (cell.Rect.Contains(g.ClientPoint))
            {
                forecolor = cell.MouseOverForeColor;
            }
            if (g.MouseButtons == MouseButtons.Left && cell.Grid.FocusedCell == cell)
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

        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0);
            }
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

        Point _prepoint = Point.Empty;
        int _prewidth = 0;

        public virtual bool ContainsSplit(IBaseCell cell, Point pt)
        {
            bool bolIn = false;
            int right = cell.Right - ConstantValue.ColumnHeaderSplit;
            if (right < 0)
            {
                right = 0;
            }

            Rectangle rect = new System.Drawing.Rectangle(
                cell.Right - ConstantValue.ColumnHeaderSplit,
                cell.Top, ConstantValue.ColumnHeaderSplit, cell.Height);
            bolIn = rect.Contains(pt);
            if (bolIn)
            {
                cell.Grid.BeginSetCursor(System.Windows.Forms.Cursors.VSplit);
            }
            return bolIn;
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            //IBaseCell cell = sender as IBaseCell;
            //if (cell == null)
            //    return false;
            //if (cell.Grid.Selectmode == RowHeightChangedMode)
            //{
            //    cell.Grid.Selectmode = 0;
            //}
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
            //DataExcel.DebugText = string.Format("e.Location={0}, pt={1}", e.Location, viewloaction);
            if (cell.Grid.Selectmode == SelectMode.ColumnWidthChangedMode)
            {
                int rh = 0;
                rh = viewloaction.X - this._prepoint.X;
                cell.Column.Width = _prewidth + rh;
                cell.Column.AutoWidth = false;
                cell.Grid.WidthChangedColumn = cell.Column;
                cell.Grid.ReFreshFirstDisplayColumnIndex();
            }
            else
            {
                if (ContainsSplit(cell, viewloaction))
                {
                    cell.Grid.CellEvent = cell;
                    return true;
                }
                Rectangle rectfilter = GetFilterRect(cell);
                if (rectfilter.Contains(viewloaction))
                {
                    cell.Grid.ReFresh();
                    return true;
                }
            }


            return false;
        }

        private void FilterClick(MouseEventArgs e)
        {
            //if (_toolform == null)
            //{
            //    _toolform = new ToolFormComboxList(this);
            //}
            //this._toolform.Show(this.Grid.PointToScreen(e.Location));
            //downloadmode = 1;
        }

        private void SortClick(IBaseCell cell)
        {
            cell.Grid.SortInfo = new Feng.Forms.ComponentModel.SortInfo();
            Feng.Forms.ComponentModel.SortOrder so = cell.Column.Order;
            if (so == Feng.Forms.ComponentModel.SortOrder.Default)
            {
                so = Feng.Forms.ComponentModel.SortOrder.Ascending;
            }
            else if (so == Feng.Forms.ComponentModel.SortOrder.Ascending)
            {
                so = Feng.Forms.ComponentModel.SortOrder.Descending;
            }
            else
            {
                so = Feng.Forms.ComponentModel.SortOrder.Ascending;
            }
            cell.Grid.ClearOrder();
            cell.Column.Order = so;
            cell.Grid.SortOrders.Add(cell.Column);
            cell.Grid.SortInfo.Add(new Feng.Forms.ComponentModel.SortColumn(cell.Column.ID
                , so, Feng.Data.TypeEnum.GetTypeEnum(cell.Column.DataType)));
            cell.Grid.SortDataSource();
            cell.Grid.ReFreshFirstDisplayRowIndex();
        }
        private void SizeChanged(IBaseCell cell, MouseEventArgs e)
        {
            Point viewloaction = cell.Grid.PointControlToView(e.Location);
            _prepoint = viewloaction;
            IColumn column = cell.Column;
            _prewidth = cell.Width;
            if (column != null)
            {
                //if (cell.Grid.CanUndoRedo)
                //{
                //    ColumnWidthCommand cmd = new ColumnWidthCommand();
                //    cmd.Value = column.Width;
                //    cmd.Column = column;
                //    cell.Grid.Commands.Add(cmd);
                //}
            }
            cell.Grid.Selectmode = SelectMode.ColumnWidthChangedMode;
            cell.Grid.CellEvent = cell;
        }
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            Point viewloaction = cell.Grid.PointControlToView(e.Location);

            bool _sizechanged = ContainsSplit(cell, viewloaction);
            if (_sizechanged)
            {
                SizeChanged(cell, e);
            }
            else
            {
                if (cell.Row.Index == 0)
                {
                    if (cell.Column.ID != string.Empty)
                    {

                        if (this.GetFilterRect(cell).Contains(viewloaction))
                        {
                            FilterClick(e);
                            return true;
                        }
                    }
                }
                if (cell.Grid.DataSource != null)
                {
                    if (cell.Row.Index == 0)
                    {
                        if (cell.Rect.Contains(viewloaction) && cell.Column.ID != string.Empty)
                        {
                            this.SortClick(cell);
                            return true;
                        }
                    }
                }
            }
            if (cell.Row.Index == 0)
            {
                if (cell.Column.Index > 0)
                {
                    cell.Column.Selected = true;
                }
            }
            return _sizechanged;
        }

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (!cell.Column.AllowChangedSize)
            {
                return false;
            }
            //if (cell.Grid.ColumnAutoWidth)
            //{
            //    return false;
            //}
            Point viewloaction = cell.Grid.PointControlToView(e.Location);
            if (ContainsSplit(cell, viewloaction))
            {
                cell.Grid.RefreshColumnWidth(cell.Column);
            }
            return false;
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


        public override ICellEditControl Clone(DataExcel grid)
        {
            CellColumnHeader celledit = new CellColumnHeader(grid);
            celledit._prepoint = this._prepoint;
            celledit._prewidth = this._prewidth;
            return celledit;
        }
    }

}
