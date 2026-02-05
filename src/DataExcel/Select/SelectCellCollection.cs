#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Feng.Utils;
using Feng.Excel.Interfaces;
using Feng.Excel.Drawing;

namespace Feng.Excel.Collections
{
    public class SelectCellCollection : ISelectCellCollection
    {

        public SelectCellCollection()
        {

        }

        public int MaxRow()
        {
            if (this.BeginCell == null)
            {
                return 0;
            }
            if (this.EndCell == null)
            {
                return 0;
            }

            int rmax = this.BeginCell.Row.Index;
            int rmin = this.BeginCell.Row.Index;

            if (this.BeginCell.IsMergeCell)
            {
                IMergeCell cell = this.BeginCell as IMergeCell;
                if (cell != null)
                {
                    rmax = cell.EndCell.Row.Index;
                    rmin = cell.BeginCell.Row.Index;
                }
            }
            int rmax1 = this.EndCell.Row.Index;
            int rmin1 = this.EndCell.Row.Index;
            if (this.EndCell.IsMergeCell)
            {
                IMergeCell cell2 = this.EndCell as IMergeCell;
                if (cell2 != null)
                {
                    rmax1 = cell2.EndCell.Row.Index;
                    rmin1 = cell2.BeginCell.Row.Index;
                }
            }
            rmax = System.Math.Max(rmax1, rmax);
            rmin = System.Math.Min(rmin1, rmin);
            return rmax;
        }

        public int MaxColumn()
        {
            if (this.BeginCell == null)
            {
                return 0;
            }
            if (this.EndCell == null)
            {
                return 0;
            }

            int cmax = this.BeginCell.Column.Index;
            int cmin = this.BeginCell.Column.Index;

            if (this.BeginCell.IsMergeCell)
            {
                IMergeCell cell = this.BeginCell as IMergeCell;
                if (cell != null)
                {
                    cmax = cell.EndCell.Column.Index;
                    cmin = cell.BeginCell.Column.Index;
                }
            }
            int cmax1 = this.EndCell.Column.Index;
            int cmin1 = this.EndCell.Column.Index;
            if (this.EndCell.IsMergeCell)
            {
                IMergeCell cell2 = this.EndCell as IMergeCell;
                if (cell2 != null)
                {
                    cmax1 = cell2.EndCell.Column.Index;
                    cmin1 = cell2.BeginCell.Column.Index;
                }
            }
            cmax = System.Math.Max(cmax1, cmax);
            cmin = System.Math.Min(cmin1, cmin);
            return cmax;
        }

        public int MinRow()
        {
            if (this.BeginCell == null)
            {
                return 0;
            }
            if (this.EndCell == null)
            {
                return 0;
            }
            int rmax = this.BeginCell.Row.Index;
            int rmin = this.BeginCell.Row.Index;

            if (this.BeginCell.IsMergeCell)
            {
                IMergeCell cell = this.BeginCell as IMergeCell;
                if (cell != null)
                {
                    rmax = cell.EndCell.Row.Index;
                    rmin = cell.BeginCell.Row.Index;
                }
            }
            int rmax1 = this.EndCell.Row.Index;
            int rmin1 = this.EndCell.Row.Index;
            if (this.EndCell.IsMergeCell)
            {
                IMergeCell cell2 = this.EndCell as IMergeCell;
                if (cell2 != null)
                {
                    rmax1 = cell2.EndCell.Row.Index;
                    rmin1 = cell2.BeginCell.Row.Index;
                }
            }
            rmax = System.Math.Max(rmax1, rmax);
            rmin = System.Math.Min(rmin1, rmin);
            return rmin;
        }

        public int MinColumn()
        {
            if (this.BeginCell == null)
            {
                return 0;
            }
            if (this.EndCell == null)
            {
                return 0;
            }
            int cmax = this.BeginCell.Column.Index;
            int cmin = this.BeginCell.Column.Index;

            if (this.BeginCell.IsMergeCell)
            {
                IMergeCell cell = this.BeginCell as IMergeCell;
                if (cell != null)
                {
                    cmax = cell.EndCell.Column.Index;
                    cmin = cell.BeginCell.Column.Index;
                }
            }
            int cmax1 = this.EndCell.Column.Index;
            int cmin1 = this.EndCell.Column.Index;
            if (this.EndCell.IsMergeCell)
            {
                IMergeCell cell2 = this.EndCell as IMergeCell;
                if (cell2 != null)
                {
                    cmax1 = cell2.EndCell.Column.Index;
                    cmin1 = cell2.BeginCell.Column.Index;
                }
            }
            cmax = System.Math.Max(cmax1, cmax);
            cmin = System.Math.Min(cmin1, cmin);
            return cmin;
        }

        public ICell MinCell { get { return this.Grid[MinRow(), MinColumn()]; } }
        public ICell MaxCell { get { return this.Grid[MaxRow(), MaxColumn()]; } }
        public List<IRow> GetAllRows()
        {
            List<IRow> list = new List<IRow>();
            int minrow = this.MinRow();
            int maxrow = this.MaxRow();
            for (int i = minrow; i < maxrow; i++)
            {
                IRow item = this.Grid.Rows[i];
                if (item != null)
                {
                    list.Add(item);
                }
            }

            return list;
        }
        public List<IColumn> GetAllColumns()
        {
            List<IColumn> list = new List<IColumn>();
            int minrow = this.MinColumn();
            int maxrow = this.MaxColumn();
            for (int i = minrow; i < maxrow; i++)
            {
                IColumn item = this.Grid.Columns[i];
                if (item != null)
                {
                    list.Add(item);
                }
            }

            return list;
        }
        public List<ICell> GetAllCells()
        {
            List<ICell> lists = new List<ICell>();
            List<ICell> listtemp = new List<ICell>();

            int maxrow = MaxRow();
            int minrow = MinRow();
            int maxcolumn = MaxColumn();
            int mincolumn = MinColumn();
            if (maxrow == 0)
            {
                return lists;
            }
            for (int i = minrow; i <= maxrow; i++)
            {
                for (int j = mincolumn; j <= maxcolumn; j++)
                {
                    ICell cell = this.Grid[i, j];
                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            if (listtemp.Contains(cell.OwnMergeCell))
                            {
                                continue;
                            }
                            listtemp.Add(cell.OwnMergeCell);
                        }
                        if (!lists.Contains(cell))
                        {
                            lists.Add(cell);
                        }
                    }
                }
            }
            return lists;
        }

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (this.Grid.OnBeforeDrawSelectCell(g))
            {
                return false;
            }
            //if (this.Grid.ShowSelectBorder)
            //{
            //    g.Graphics.DrawRectangle(new Pen(this.Grid.SelectBorderColor), this.Left, this.Top, this.Width, this.Height);
            //}

            //if (!this.Grid.ShowSelectRect)
            //{
            //    return false;
            //}
            Rectangle rectf = new Rectangle(this.Rect.Location, this.Rect.Size);
            if (this.BeginCell == null)
                return false;
            if (this.EndCell == null)
                return false;
            if (DataRectHelper.IsEmpty(rectf))
            {
                return false;
            }

            if (!this.Grid.InEdit)
            {
                int x = rectf.Location.X, y = rectf.Location.Y, w = rectf.Size.Width, h = rectf.Size.Height;
                Color color = Color.FromArgb(50, System.Drawing.SystemColors.AppWorkspace);
                //color = Color.Red;
                g.Graphics.FillRectangle(new SolidBrush(color), rectf);
                DrawHelper.DrawRect(g.Graphics, rectf, this.BackColor, 3f, System.Drawing.Drawing2D.DashStyle.Solid);
                DrawHelper.DrawCorssSelectRect(g.Graphics, rectf);

            }
            else
            {
                rectf.Inflate(1, 1);
                DrawHelper.DrawRect(g.Graphics, rectf, this.BackColor, 1.5f, System.Drawing.Drawing2D.DashStyle.Solid);
                DrawHelper.DrawCorssSelectRect(g.Graphics, rectf);
            }
            return false;
        }

        public override string ToString()
        {
            string str = string.Format("Left:{0},Top{1},Width{2},Height{3}", this.Left, this.Top, this.Width, this.Height);
            return str;
        }

        public void ReFreshSize()
        {
            Refresh();
        }

        public void Refresh()
        {
            int maxrow = this.MaxRow();
            int maxcolumn = this.MaxColumn();
            int minrow = this.MinRow();
            int mincolumn = this.MinColumn();
            List<IRow> rows = this.Grid.VisibleRows;
            List<IColumn> columns = this.Grid.VisibleColumns;
            int top = this.Grid.Height + 2000;
            int left = this.Grid.Width + 2000;
            int width = 0;
            int height = 0;
            foreach (IRow item in rows)
            {
                if (item.Index >= minrow && item.Index <= maxrow)
                {
                    height = height+ item.Height;
                    if (item.Top < top)
                    {
                        top = item.Top;
                    }
                }
            }

            foreach (IColumn item in columns)
            {
                if (item.Index >= mincolumn && item.Index <= maxcolumn)
                {
                    width = width + item.Width;
                    if (item.Left < left  )
                    {
                        left = item.Left;
                    }
                }
            }
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;

            //Rectangle bounds = Rectangle.Empty;
            //DataExcel.GetRect(this.BeginCell, this.EndCell, ref bounds);

            //this.Top = bounds.Top;
            //this.Left = bounds.Left;
            //this.Width = bounds.Width;
            //this.Height = bounds.Height;



        }

        public void CalcColumn()
        {
            if (this.BeginCell == null)
                return;
            int cmax = System.Math.Max(this.BeginCell.MaxColumnIndex, this.EndCell.MaxColumnIndex);
            int cmin = System.Math.Min(this.BeginCell.Column.Index, this.EndCell.Column.Index);
            int w = 0;
            IColumn column = null;
            for (int i = cmin; i <= cmax; i++)
            {
                column = this.Grid.Columns[i];
                if (column != null)
                {
                    w = w + column.Width;
                }
            }
            int enddisplayrowindex = this.Grid.EndDisplayedColumnIndex;
            int firstdisplayrowindex = this.Grid.FirstDisplayedColumnIndex;
            if (cmin >= firstdisplayrowindex && cmin <= enddisplayrowindex)
            {
                column = this.Grid.Columns[cmin];
                if (column != null)
                {
                    this.Left = column.Left;
                }
            }
            if (cmin < firstdisplayrowindex && cmax > enddisplayrowindex)
            {
                int th = 0;
                for (int i = cmin; i < firstdisplayrowindex; i++)
                {
                    column = this.Grid.Columns[i];
                    if (column != null)
                    {
                        th = th + column.Width;
                    }
                }
                column = this.Grid.Columns[cmin];
                if (column != null)
                {
                    this.Left = column.Left - th;
                }
            }
            else if (cmin < firstdisplayrowindex && cmax < firstdisplayrowindex)
            {
                this.Left = this.Width * -1;
            }
            else if (cmin > enddisplayrowindex && cmax > enddisplayrowindex)
            {
                this.Left = this.Width * -1;
            }
            else if (cmin < firstdisplayrowindex && cmax < enddisplayrowindex)
            {
                column = this.Grid.Columns[cmax];
                if (column != null)
                {
                    this.Left = column.Right - w;
                }
            }
            else if (cmin > firstdisplayrowindex && cmax > enddisplayrowindex)
            {
                column = this.Grid.Columns[cmin];
                if (column != null)
                {
                    this.Left = this.Grid.Columns[cmin].Left;
                }
            }

            this.Width = w;
        }

        public void CalcRow()
        {
            if (this.BeginCell == null)
                return;

            int rmax = System.Math.Max(this.BeginCell.MaxRowIndex, this.EndCell.MaxRowIndex);
            int rmin = System.Math.Min(this.BeginCell.Row.Index, this.EndCell.Row.Index);
            int h = 0;
            for (int i = rmin; i <= rmax; i++)
            {
                h = h + this.Grid.Rows[i].Height;
            }
            int enddisplayrowindex = this.Grid.EndDisplayedRowIndex;
            int firstdisplayrowindex = this.Grid.FirstDisplayedRowIndex;
            if (rmin >= firstdisplayrowindex && rmin <= enddisplayrowindex)
            {
                this.Top = this.Grid.Rows[rmin].Top;
            }
            if (rmin < firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                int th = 0;
                for (int i = rmin; i < firstdisplayrowindex; i++)
                {
                    th = th + this.Grid.Rows[i].Height;
                }
                this.Top = this.Grid.Rows[rmin].Top - th;
            }
            else if (rmin < firstdisplayrowindex && rmax < firstdisplayrowindex)
            {
                this.Top = this.Height * -1;
            }
            else if (rmin > enddisplayrowindex && rmax > enddisplayrowindex)
            {
                this.Top = this.Height * -1;
            }
            else if (rmin < firstdisplayrowindex && rmax < enddisplayrowindex)
            {
                this.Top = this.Grid.Rows[rmax].Bottom - h;
            }
            else if (rmin > firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                this.Top = this.Grid.Rows[rmin].Top;
            }

            this.Height = h;
        }


        public void SetSizeTopRow()
        {

            CalcRowTopRow();
            CalcColumnTopRow();
        }

        public void CalcColumnTopRow()
        {
            if (this.BeginCell == null)
                return;
            int cmax = System.Math.Max(this.BeginCell.MaxColumnIndex, this.EndCell.MaxColumnIndex);
            int cmin = System.Math.Min(this.BeginCell.Column.Index, this.EndCell.Column.Index);
            int w = 0;
            IColumn column = null;
            for (int i = cmin; i <= cmax; i++)
            {
                column = this.Grid.Columns[i];
                if (column != null)
                {
                    w = w + this.Grid.Columns[i].Width;
                }
            }
            column = this.Grid.Columns[cmin];
            if (column != null)
            {
                this.Left = column.Left;
            }
            this.Width = w;
        }

        public void CalcRowTopRow()
        {
            if (this.BeginCell == null)
                return;

            int rmax = System.Math.Max(this.BeginCell.MaxRowIndex, this.EndCell.MaxRowIndex);
            int rmin = System.Math.Min(this.BeginCell.Row.Index, this.EndCell.Row.Index);
            int h = 0;
            for (int i = rmin; i <= rmax; i++)
            {
                h = h + this.Grid.Rows[i].Height;
            }
            this.Top = this.Grid.Rows[rmin].Top;
            this.Height = h;
        }

        public virtual bool AddRectContains(Point pt)
        {
#warning 此处可以优化
            Rectangle rect = new Rectangle();
            rect.Width = 6;
            rect.Height = 6;
            rect.Location = new Point(this.Right - 3, this.Bottom - 3);
            rect.Inflate(3, 3);
            return rect.Contains(pt);
        }

        public void ReSetTop()
        {

        }


        #region ICollection<clsxieyi> 成员

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

         

        #region ICellRange 成员

        private ICell _firstcell = null;
        public virtual ICell BeginCell
        {
            get { return this._firstcell; }
            set
            {
                ClearCellSelect();
                this._firstcell = value;
                this._endcell = value;
                Refresh();
                SetCellSelect();
            }
        }

        private ICell _endcell = null;


        public virtual ICell EndCell
        {
            get
            {
                if (this._endcell == null)
                {
                    return this._firstcell;
                }
                return this._endcell;
            }

            set
            {
                this.Grid.BeginReFresh();
                ClearCellSelect();
                this._endcell = value;
                Refresh();
                SetCellSelect();
                this.Grid.EndReFresh();
            }
        }


        #endregion

        public void ClearCellSelect()
        {
            List<ICell> lists = new List<ICell>();
            int maxrow = MaxRow();
            int minrow = MinRow();
            int maxcolumn = MaxColumn();
            int mincolumn = MinColumn();
            if (maxrow == 0)
            {
                return;
            }
        }

        public void SetCellSelect()
        {
            if (this.Grid == null)
                return;
            if (this.Grid.SelectCells != this)
            {
                return;
            }
            List<ICell> lists = new List<ICell>();
            int maxrow = MaxRow();
            int minrow = MinRow();
            int maxcolumn = MaxColumn();
            int mincolumn = MinColumn();
            if (maxrow == 0)
            {
                return;
            }
            for (int i = minrow; i <= maxrow; i++)
            {
                for (int j = mincolumn; j <= maxcolumn; j++)
                {
                    ICell cell = this.Grid[i, j];
                    if (cell != null)
                    {
                        cell.Selected = true;
                    }
                }
            }
        }

        #region IBounds 成员

        private int _left = 0;
        public virtual int Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        private int _height = 0;
        public virtual int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public virtual int Right
        {
            get
            {

                return _width + _left;
            }

        }
        public virtual int Bottom
        {
            get
            {

                return _top + _height;
            }

        }
        private int _top = 0;
        public virtual int Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
            }
        }

        private int _width = 0;
        public virtual int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public virtual Rectangle Rect
        {
            get
            {
                return new Rectangle(this._left, this._top, this._width, this._height);
            }
        }
        #endregion

        #region IDataExcelGrid 成员

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual DataExcel Grid
        {
            get
            {
                return this.BeginCell.Grid;
            }
        }
        #endregion

        #region IControlColor 成员
        private Color _backcolor = Color.SlateBlue;
        public virtual Color BackColor
        {
            get
            {
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }
        #endregion
    } 
}
