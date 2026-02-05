using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Design;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using Feng.Data;
using Feng.Excel.Edits;
using Feng.Excel.Interfaces;
using Feng.Excel.App;

namespace Feng.Excel
{
    partial class DataExcel
    {

        public static Rectangle GetPrintBounds(Point pt, ICell begincell, ICell endcell,
            ref Rectangle clip
            , int firstdisplaycolumnindex, int enddisplaycolumnindex
            , int firstdisplayrowindex, int enddisplayrowindex)
        {
            clip = GetPrintBoundsClip(pt, begincell, endcell, firstdisplaycolumnindex, enddisplaycolumnindex, firstdisplayrowindex, enddisplayrowindex);

            Rectangle rect = Rectangle.Empty;
            rect = GetPrintBounds(pt, begincell, endcell, firstdisplaycolumnindex, enddisplaycolumnindex, firstdisplayrowindex, enddisplayrowindex);
            return rect;
            //CalcPrintColumn(pt, begincell, endcell, ref rect, ref clip, firstdisplaycolumnindex, enddisplaycolumnindex);
            //CalcPrintRow(pt, begincell, endcell, ref rect, ref clip, firstdisplayrowindex, enddisplayrowindex);
            //return rect;
        }

        public static Rectangle GetPrintMergeCellRect(Point pt, IMergeCell mcell, ICell begincell, ICell endcell
    , int firstdisplaycolumnindex, int enddisplaycolumnindex
    , int firstdisplayrowindex, int enddisplayrowindex)
        {
            int left = GetPrintMergeCellRectLeft(pt, begincell, endcell
, firstdisplaycolumnindex, enddisplaycolumnindex
, firstdisplayrowindex, enddisplayrowindex);
            int top = GetPrintMergeCellRectTop(pt, begincell, endcell
, firstdisplaycolumnindex, enddisplaycolumnindex
, firstdisplayrowindex, enddisplayrowindex);
            return new Rectangle(left, top, mcell.Width, mcell.Height);
        }

        public static int GetPrintMergeCellRectLeft(Point pt, ICell begincell, ICell endcell
, int firstdisplaycolumnindex, int enddisplaycolumnindex
, int firstdisplayrowindex, int enddisplayrowindex)
        {
            if (begincell.Column.Index >= firstdisplaycolumnindex)
            {
                return pt.X;
            }
            else
            {
                int w = 0;
                for (int i = begincell.Column.Index; i < firstdisplaycolumnindex; i++)
                {
                    w = w + begincell.Grid.Columns[i].Width;
                }
                return pt.X - w;
            }
        }

        public static int GetPrintMergeCellRectTop(Point pt, ICell begincell, ICell endcell
, int firstdisplaycolumnindex, int enddisplaycolumnindex
, int firstdisplayrowindex, int enddisplayrowindex)
        {
            if (begincell.Row.Index >= firstdisplayrowindex)
            {
                return pt.Y;
            }
            else
            {
                int w = 0;
                for (int i = begincell.Row.Index; i < firstdisplayrowindex; i++)
                {
                    w = w + begincell.Grid.Rows[i].Height;
                }
                return pt.Y - w;
            }
        }

        public static Rectangle GetPrintBoundsClip(Point pt, ICell begincell, ICell endcell
            , int firstdisplaycolumnindex, int enddisplaycolumnindex
            , int firstdisplayrowindex, int enddisplayrowindex)
        {
            DataExcel grid = begincell.Grid;
            int width = 0;
            int height = 0;
            int mincol = begincell.Column.Index;
            int minrow = begincell.Row.Index;
            int maxcol = endcell.MaxColumnIndex;
            int maxrow = endcell.MaxRowIndex;
            if (mincol >= firstdisplaycolumnindex && maxcol <= enddisplaycolumnindex
                && minrow >= firstdisplayrowindex && maxrow <= enddisplayrowindex
                )
            {
                return Rectangle.Empty;
            }
            for (int i = firstdisplaycolumnindex; i < enddisplaycolumnindex; i++)
            {
                IColumn column = grid.Columns[i];
                if (column != null)
                {
                    width = width + grid.Columns[i].Width;
                }
            }
            for (int i = firstdisplayrowindex; i < enddisplayrowindex; i++)
            {
                height = height + grid.Rows[i].Height;
            }
            return new Rectangle(pt.X, pt.Y, width, height);

        }

        public static Rectangle GetPrintBounds(Point pt, ICell begincell, ICell endcell
      , int firstdisplaycolumnindex, int enddisplaycolumnindex
      , int firstdisplayrowindex, int enddisplayrowindex)
        {
            DataExcel grid = begincell.Grid;
            int mincol = begincell.Column.Index;
            int minrow = begincell.Row.Index;
            int maxcol = endcell.MaxColumnIndex;
            int maxrow = endcell.MaxRowIndex;

            int width = 0;
            int height = 0;
            for (int i = mincol; i <= maxcol; i++)
            {
                width = width + grid.Columns[i].Width;
            }
            for (int i = minrow; i <= maxrow; i++)
            {
                height = height + grid.Rows[i].Height;
            }

            int dwidth = 0;
            int dheight = 0;
            for (int i = mincol; i < firstdisplaycolumnindex; i++)
            {
                dwidth = dwidth + grid.Columns[i].Width;
            }
            for (int i = minrow; i < firstdisplayrowindex; i++)
            {
                dheight = dheight + grid.Rows[i].Height;
            }
            return new Rectangle(pt.X - dwidth, pt.Y - dheight, width, height);

        }

        public static Rectangle GetRect(ICell begincell, ICell endcell, ref Rectangle clipbounds)
        {
            if (begincell == null || endcell == null)
                return Rectangle.Empty ;
            if (begincell == endcell)
            {
                clipbounds = begincell.Rect;
                if (begincell.MaxColumnIndex < begincell.Grid.FirstDisplayedColumnIndex)
                {
                    clipbounds = Rectangle.Empty;
                }
                if (begincell.MaxRowIndex < begincell.Grid.FirstDisplayedRowIndex)
                {
                    clipbounds = Rectangle.Empty;
                }
                if (begincell.Row.Index > begincell.Grid.EndDisplayedRowIndex)
                {
                    clipbounds = Rectangle.Empty;
                }
                if (begincell.Column.Index > begincell.Grid.EndDisplayedColumnIndex)
                {
                    clipbounds = Rectangle.Empty;
                }
                return begincell.Rect;
            }
            byte cellmode = 0;
            Rectangle rect = Rectangle.Empty;
            if (begincell.Row.Index > 0 && endcell.Row.Index > 0)
            {
                if (begincell.Column.Index > 0 && endcell.Column.Index > 0)
                {
                    cellmode = RowColumn;

                }
                else if ((begincell.Column.Index < 0 && endcell.Column.Index < 0)
                    && (begincell.Column.Index > -100 && endcell.Column.Index > -100))
                {
                    cellmode = LeftColumn;

                }
                else if ((begincell.Column.Index < -100 && endcell.Column.Index < -100)
                    && (begincell.Column.Index > -200 && endcell.Column.Index > -200))
                {
                    cellmode = RightColumn;

                }
            }
            else if ((begincell.Row.Index < 0 && endcell.Row.Index < 0)
                && (begincell.Row.Index > -100 && endcell.Row.Index > -100))
            {
                cellmode = TopRow;
            }
            else if ((begincell.Row.Index < -100 && endcell.Row.Index < -100)
                && (begincell.Row.Index > -200 && endcell.Row.Index > -200))
            {
                cellmode = BottomRow;
            }
            switch (cellmode)
            {
                case RowColumn:
                    CalcRowColumn(begincell, endcell, ref rect, ref clipbounds);
                    //CalcColumn(begincell, endcell, ref rect, ref bounds);
                    //CalcRow(begincell, endcell, ref rect, ref bounds);
                    break;
                case TopRow:
                    CalcColumnTopRow(begincell, endcell, ref rect);
                    CalcRowTopRow(begincell, endcell, ref rect);
                    clipbounds = rect;
                    break;
                case LeftColumn:
                    CalcColumnTopRow(begincell, endcell, ref rect);
                    CalcRowTopRow(begincell, endcell, ref rect);
                    clipbounds = rect;
                    break;
                case BottomRow:
                    CalcColumn(begincell, endcell, ref rect, ref clipbounds);
                    CalcRow(begincell, endcell, ref rect, ref clipbounds);
                    clipbounds = rect;
                    break;
                case RightColumn:
                    CalcColumnTopRow(begincell, endcell, ref rect);
                    CalcRowTopRow(begincell, endcell, ref rect);
                    clipbounds = rect;
                    break;
                default:
                    break;
            }
            return rect;
        }
        private const byte RowColumn = 1;
        private const byte TopRow = 2;
        private const byte LeftColumn = 3;
        private const byte BottomRow = 4;
        private const byte RightColumn = 5;

        private static void CalcPrintColumn(Point pt, ICell begincell, ICell endcell, ref Rectangle rect, ref Rectangle bounds
            , int firstdisplaycolumnindex, int enddisplaycolumnindex)
        {
            DataExcel grid = begincell.Grid;
            int cmax = System.Math.Max(begincell.MaxColumnIndex, endcell.MaxColumnIndex);
            int cmin = System.Math.Min(begincell.Column.Index, endcell.Column.Index);
            int w = 0;
            int left = pt.X;
            IColumn column = null;
            for (int i = cmin; i <= cmax; i++)
            {
                column = grid.Columns[i];
                if (column != null)
                {
                    w = w + column.Width;
                }
            }
            rect.Width = w;
            if (cmin >= firstdisplaycolumnindex)
            {
                for (int i = firstdisplaycolumnindex; i < cmin; i++)
                {
                    column = grid.Columns[i];
                    if (column != null)
                    {
                        left = left + column.Width;
                    }
                }
            }
            else
            {
                int th = 0;
                for (int i = cmin; i < firstdisplaycolumnindex; i++)
                {
                    column = grid.Columns[i];
                    if (column != null)
                    {
                        th = th + column.Width;
                    }
                }
                left = left - th;
            }
            rect.X = left;


            cmax = System.Math.Max(begincell.MaxColumnIndex, endcell.MaxColumnIndex);
            cmin = System.Math.Min(begincell.Column.Index, endcell.Column.Index);
            cmin = System.Math.Max(cmin, firstdisplaycolumnindex);
            cmax = System.Math.Min(cmax, enddisplaycolumnindex);
            w = 0;
            left = pt.X;
            for (int i = cmin; i <= cmax; i++)
            {
                column = grid.Columns[i];
                if (column != null)
                {
                    w = w + column.Width;
                }
            }
            bounds.Width = w;
            if (cmin >= firstdisplaycolumnindex && cmin <= enddisplaycolumnindex)
            {
                for (int i = firstdisplaycolumnindex; i < cmin; i++)
                {
                    column = grid.Columns[i];
                    if (column != null)
                    {
                        left = left + column.Width;
                    }
                }
            }
            else if (cmin < firstdisplaycolumnindex && cmax > enddisplaycolumnindex)
            {
                int th = 0;
                for (int i = cmin; i < firstdisplaycolumnindex; i++)
                {
                    column = grid.Columns[i];
                    if (column != null)
                    {
                        th = th + column.Width;
                    }
                }
                left = left - th;
            }
            else if (cmin < firstdisplaycolumnindex && cmax < firstdisplaycolumnindex)
            {
                bounds = Rectangle.Empty;
                return;
            }
            else if (cmin > enddisplaycolumnindex && cmax > enddisplaycolumnindex)
            {
                bounds = Rectangle.Empty;
                return;
            }
            else if (cmin < firstdisplaycolumnindex && cmax < enddisplaycolumnindex)
            {
                int th = 0;
                for (int i = cmin; i < firstdisplaycolumnindex; i++)
                {
                    column = grid.Columns[i];
                    if (column != null)
                    {
                        th = th + column.Width;
                    }
                }
                left = left - th;
            }
            else if (cmin > firstdisplaycolumnindex && cmax > enddisplaycolumnindex)
            {
                for (int i = cmin; i < firstdisplaycolumnindex; i++)
                {
                    column = grid.Columns[i];
                    if (column != null)
                    {
                        left = left + column.Width;
                    }
                }
            }
            bounds.X = left;
        }
        private static void CalcPrintRow(Point pt, ICell begincell, ICell endcell, ref Rectangle rect, ref Rectangle bounds
            , int firstdisplayrowindex, int enddisplayrowindex)
        {
            DataExcel grid = begincell.Grid;
            int rmax = System.Math.Max(begincell.MaxRowIndex, endcell.MaxRowIndex);
            int rmin = System.Math.Min(begincell.Row.Index, endcell.Row.Index);

            int h = 0;
            int top = pt.Y;
            for (int i = rmin; i <= rmax; i++)
            {
                h = h + grid.Rows[i].Height;
            }
            rect.Height = h;

            if (rmin >= firstdisplayrowindex)
            {
                for (int i = firstdisplayrowindex; i < rmin; i++)
                {
                    top = top + grid.Rows[i].Height;
                }
            }
            else
            {
                int th = 0;
                for (int i = rmin; i < firstdisplayrowindex; i++)
                {
                    th = th + grid.Rows[i].Height;
                }
                top = top - th;
            }

            rect.Y = top;

            rmax = System.Math.Max(begincell.MaxRowIndex, endcell.MaxRowIndex);
            rmax = System.Math.Min(rmax, enddisplayrowindex);

            rmin = System.Math.Min(begincell.Row.Index, endcell.Row.Index);
            rmin = System.Math.Max(rmin, firstdisplayrowindex);
            h = 0;
            top = pt.Y;
            for (int i = rmin; i <= rmax; i++)
            {
                h = h + grid.Rows[i].Height;
            }
            bounds.Height = h;
            if (rmin >= firstdisplayrowindex && rmin <= enddisplayrowindex)
            {
                for (int i = firstdisplayrowindex; i < rmin; i++)
                {
                    top = top + grid.Rows[i].Height;
                }
            }
            else if (rmin < firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                int th = 0;
                for (int i = rmin; i < firstdisplayrowindex; i++)
                {
                    th = th + grid.Rows[i].Height;
                }
                top = top - th;
            }
            else if (rmin < firstdisplayrowindex && rmax < firstdisplayrowindex)
            {
                bounds = Rectangle.Empty;
                return;
            }
            else if (rmin > enddisplayrowindex && rmax > enddisplayrowindex)
            {
                bounds = Rectangle.Empty;
                return;
            }
            else if (rmin < firstdisplayrowindex && rmax < enddisplayrowindex)
            {
                int th = 0;
                for (int i = rmin; i < firstdisplayrowindex; i++)
                {
                    th = th + grid.Columns[i].Height;
                }
                top = top - th;
            }
            else if (rmin > firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                for (int i = firstdisplayrowindex; i < rmin; i++)
                {
                    top = top + grid.Rows[i].Height;
                }
            }
            bounds.Y = top;
        }

        private static void CalcColumn(ICell begincell, ICell endcell, ref Rectangle rect, ref Rectangle bounds)
        {
            DataExcel grid = begincell.Grid;

            int cmax = System.Math.Max(begincell.MaxColumnIndex, endcell.MaxColumnIndex);
            int cmin = System.Math.Min(begincell.Column.Index, endcell.Column.Index);
            IColumn column = null;
            for (int i = cmin; i <= cmax; i++)
            {
                column = grid.Columns[i];
                if (column != null)
                {
                    if (column.Visible)
                    {
                        cmin = i;
                        break;
                    }
                }
            }
            for (int i = cmax; i >= cmin; i--)
            {
                column = grid.Columns[i];
                if (column != null)
                {
                    if (column.Visible)
                    {
                        cmax = i;
                        break;
                    }
                }
            }
            int w = 0;
            for (int i = cmin; i <= cmax; i++)
            {
                column = grid.Columns[i];
                if (column != null)
                {
                    if (column.Visible)
                    {
                        w = w + column.Width;
                    }
                }
            }
            int enddisplayindex = grid.EndDisplayedColumnIndex;
            int firstdisplayindex = grid.FirstDisplayedColumnIndex;
            if (cmin >= firstdisplayindex && cmin <= enddisplayindex)
            {
                column = grid.Columns[cmin];
                if (column != null)
                {
                    rect.X = column.Left;
                }
            }
            if (cmin < firstdisplayindex && cmax > enddisplayindex)
            {
                int th = 0;
                for (int i = cmin; i < firstdisplayindex; i++)
                {
                    column = grid.Columns[i];
                    if (column != null)
                    {
                        if (column.Visible)
                        {
                            th = th + column.Width;
                        }
                    }
                }
                column = grid.Columns[cmin];
                if (column != null)
                {
                    rect.X = column.Left - th;
                }
            }
            else if (cmin < firstdisplayindex && cmax < firstdisplayindex)
            {
                rect.X = 0;
            }
            else if (cmin > enddisplayindex && cmax > enddisplayindex)
            {
                rect.X = 0;
            }
            else if (cmin < firstdisplayindex && cmax < enddisplayindex)
            {
                column = grid.Columns[cmax];
                if (column != null)
                {
                    rect.X = column.Right - w;
                }
            }
            else if (cmin > firstdisplayindex && cmax > enddisplayindex)
            {
                column = grid.Columns[cmin];
                if (column != null)
                {
                    rect.X = column.Left;
                }
            }

            rect.Width = w;



            cmax = System.Math.Max(begincell.MaxColumnIndex, endcell.MaxColumnIndex);
            cmin = System.Math.Min(begincell.Column.Index, endcell.Column.Index);
            cmin = System.Math.Max(cmin, grid.FirstDisplayedColumnIndex);
            cmax = System.Math.Min(cmax, grid.EndDisplayedColumnIndex);
            w = 0;
            for (int i = cmin; i <= cmax; i++)
            {
                column = grid.Columns[i];
                if (column != null)
                {
                    if (column.Visible)
                    {
                        w = w + column.Width;
                    }
                }
            }
            bounds.Width = w;
            if (cmin >= firstdisplayindex && cmin <= enddisplayindex)
            {
                column = grid.Columns[cmin];
                if (column != null)
                {
                    bounds.X = column.Left;
                }
            }
            if (cmin < firstdisplayindex && cmax > enddisplayindex)
            {
                column = grid.Columns[cmin];
                if (column != null)
                {
                    bounds.X = column.Left;
                }
            }
            else if (cmin < firstdisplayindex && cmax < firstdisplayindex)
            {
                bounds.Width = w;
                return;
            }
            else if (cmin > enddisplayindex && cmax > enddisplayindex)
            {
                bounds.Width = w;
                return;
            }
            else if (cmin < firstdisplayindex && cmax < enddisplayindex)
            {
                column = grid.Columns[cmax];
                if (column != null)
                {
                    bounds.X = column.Right - w;
                }
            }
            else if (cmin > firstdisplayindex && cmax > enddisplayindex)
            {
                column = grid.Columns[cmin];
                if (column != null)
                {
                    bounds.X = column.Left;
                }
            }
        }


        public static void CalcRowColumn(ICell begincell, ICell endcell, ref Rectangle rect, ref Rectangle clipbounds)
        {
            DataExcel grid = begincell.Grid;

            int columnmax = System.Math.Max(begincell.MaxColumnIndex, endcell.MaxColumnIndex);
            int columnmin = System.Math.Min(begincell.Column.Index, endcell.Column.Index);
            int rowmax = System.Math.Max(begincell.MaxRowIndex, endcell.MaxRowIndex);
            int rowmin = System.Math.Min(begincell.Row.Index, endcell.Row.Index);

            int firstrow = grid.FirstDisplayedRowIndex;
            int endrow = grid.EndDisplayedRowIndex;
            int firstcolumn = grid.FirstDisplayedColumnIndex;
            int endcolumn = grid.EndDisplayedColumnIndex;


            rect.Width = CalcWidth(grid, columnmin, columnmax);
            rect.Height = CalcHeight(grid, rowmin, rowmax);
            if (rowmax < grid.FrozenRow)
            {
                rect.Y = begincell.Top;
            }
            else
            {
                rect.Y = CalcTop(grid, rowmin, firstrow, firstrow);
            }
            if (columnmax < grid.FrozenColumn)
            {
                rect.X = begincell.Left;
            }
            else
            {
                rect.X = CalcLeft(grid, columnmin, firstcolumn, firstcolumn);
            }
 
            int showwidth = CalcClipWidth(grid, columnmin, columnmax, firstcolumn, endcolumn);
            int frozenwidth = 0;//CalcFrozenColumnWidth(grid, columnmin);
            clipbounds.Width = showwidth - frozenwidth;
            clipbounds.Height = CalcClipHeight(grid, rowmin, rowmax, firstrow, endrow);
            clipbounds.Y = CalcClipTop(grid, rowmin, rowmax, firstrow, endrow);
            clipbounds.X = CalcClipLeft(grid, columnmin, columnmax, firstcolumn, endcolumn);
        }
        public static int CalcFrozenColumnWidth(DataExcel grid,int index)
        {
            int w = 0;
            for (int i = index; i <= grid.FrozenColumn; i++)
            {
                IColumn column = grid.GetColumn(i);
                if (column.Visible)
                {
                    w = w + column.Width;
                }
            }
            return w;
        }
        public static int CalcHeight(DataExcel grid, int min, int max)
        {
            int h = 0;
            for (int i = min; i <= max; i++)
            {
                IRow row = grid.GetRow(i);
                if (row.Visible)
                {
                    h = h + row.Height;
                }
            }
            return h;
        }
        public static int CalcWidth(DataExcel grid, int min, int max)
        {
            int w = 0;
            for (int i = min; i <= max; i++)
            {
                IColumn column = grid.GetColumn(i);
                if (column.Visible)
                {
                    w = w + column.Width;
                }
            }
            return w;
        }
        public static int CalcTop(DataExcel grid, int min, int max, int firstrow)
        {
            int top = 0;
            if (min >= firstrow)
            {
                IRow row = grid.Rows[min];
                top = row.Top;
            }
            else
            {
                IRow row = grid.Rows[firstrow];
                if (row != null)
                {
                    top = row.Top;
                    for (int i = firstrow-1; i >= min; i--)
                    {
                        row = grid.Rows[i];
                        if (row != null)
                        {
                            top = top - row.Height;
                        }
                    }
                }
            }
            return top;
        }
        public static int CalcLeft(DataExcel grid, int min, int max, int firstcolumn)
        {
            int left = 0;
            if (min >= firstcolumn)
            {
                IColumn column = grid.Columns[min];
                left = column.Left;
            }
            else
            {
                IColumn column = grid.Columns[firstcolumn];
                if (column != null)
                {
                    left = column.Left;
                    for (int i = firstcolumn - 1; i >= min; i--)
                    {
                        column = grid.Columns[i];
                        if (column != null)
                        {
                            left = left - column.Width;
                        }
                    }
                }
            }
            return left;
        }


        public static int CalcClipHeight(DataExcel grid, int smin, int smax, int tmin, int tmax)
        {
            int h = 0;
            int min = Math.Max(smin, tmin);
            int max = Math.Min(smax, tmax);

            for (int i = min; i <= max; i++)
            {
                IRow row = grid.GetRow(i);
                if (row.Visible)
                {
                    h = h + row.Height;
                }
            }
            return h;
        }
        public static int CalcClipWidth(DataExcel grid, int factmincolumn, int factmaxcolumn, int showmincolumn, int showmaxcolumn)
        {
            int w = 0;
            int min = Math.Max(factmincolumn, showmincolumn);
            int max = Math.Min(factmaxcolumn, showmaxcolumn);

            for (int i = min; i <= max; i++)
            {
                IColumn column = grid.GetColumn(i);
                
                if (column.Visible)
                {
                    if (grid.VisibleColumns.Contains(column))
                    {
                        w = w + column.Width;
                    }
                }
            }
            return w;
        }
        public static int CalcClipTop(DataExcel grid, int smin, int smax, int tmin, int tmax)
        {
            int top = 0;
            int min = Math.Max(smin, tmin);
            IRow row = grid.GetRow(min);
            top = row.Top;
            return top;
        }
        public static int CalcClipLeft(DataExcel grid, int smin, int smax, int tmin, int tmax)
        {
            int left = 0;
            int min = Math.Max(smin, tmin);
            IColumn column = grid.GetColumn(min);
            left = column.Left;
            return left;
        }

        private static void CalcRow(ICell begincell, ICell endcell, ref Rectangle rect, ref Rectangle bounds)
        {
            DataExcel grid = begincell.Grid;
            int rmax = System.Math.Max(begincell.MaxRowIndex, endcell.MaxRowIndex);
            int rmin = System.Math.Min(begincell.Row.Index, endcell.Row.Index);

            int h = 0;
            for (int i = rmin; i <= rmax; i++)
            {
                IRow row = grid.GetRow(i);
                if (row.Visible)
                {
                    h = h + row.Height;
                }
            }
            rect.Height = h;



            int enddisplayrowindex = grid.EndDisplayedRowIndex;
            int firstdisplayrowindex = grid.FirstDisplayedRowIndex;

            if (rmin >= firstdisplayrowindex && rmin <= enddisplayrowindex)
            {
                rect.Y = grid.Rows[rmin].Top;
            }
            else if (rmin < firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                int th = 0;

                for (int i = rmin; i < firstdisplayrowindex; i++)
                {
                    IRow row = grid.Rows[i];
                    if (row.Visible)
                    {
                        th = th + row.Height;
                    }
                }
                rect.Y = grid.Rows[rmin].Top - th;
            }
            else if (rmin < firstdisplayrowindex && rmax < firstdisplayrowindex)
            {
                rect.Height = h;
            }
            else if (rmin > enddisplayrowindex && rmax > enddisplayrowindex)
            {
                rect.Height = h;
            }
            else if (rmin < firstdisplayrowindex && rmax < enddisplayrowindex)
            {
                rect.Y = grid.Rows[rmax].Bottom - h;
            }
            else if (rmin > firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                rect.Y = grid.Rows[rmin].Top;
            }

            rect.Height = h;


            rmax = System.Math.Max(begincell.MaxRowIndex, endcell.MaxRowIndex);
            rmax = System.Math.Min(rmax, grid.EndDisplayedRowIndex);

            rmin = System.Math.Min(begincell.Row.Index, endcell.Row.Index);
            rmin = System.Math.Max(rmin, grid.FirstDisplayedRowIndex);
            h = 0;
            for (int i = rmin; i <= rmax; i++)
            {
                IRow row = grid.Rows[i];
                if (row.Visible)
                {
                    h = h + row.Height;
                }
            }
            bounds.Height = h;
            if (rmin >= firstdisplayrowindex && rmin <= enddisplayrowindex)
            {
                IRow row = grid.Rows[rmin];
                if (row != null)
                {
                    bounds.Y = grid.Rows[rmin].Top;
                }
            }
            else if (rmin < firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                bounds.Y = grid.Rows[rmin].Top;
            }
            else if (rmin < firstdisplayrowindex && rmax < firstdisplayrowindex)
            {
                bounds.Height = h;
                return;
            }
            else if (rmin > enddisplayrowindex && rmax > enddisplayrowindex)
            {
                bounds.Height = h;
                return;
            }
            else if (rmin < firstdisplayrowindex && rmax < enddisplayrowindex)
            {
                bounds.Y = grid.Rows[rmax].Bottom - h;
            }
            else if (rmin > firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                bounds.Y = grid.Rows[rmin].Top;
            }
        }

        private static void CalcRow2(ICell begincell, ICell endcell, ref Rectangle rect)
        {
            int rmax = System.Math.Max(begincell.MaxRowIndex, endcell.MaxRowIndex);

            int rmin = System.Math.Min(begincell.Row.Index, endcell.Row.Index);
            int h = 0;
            DataExcel grid = begincell.Grid;
            for (int i = rmin; i <= rmax; i++)
            {
                h = h + grid.Rows[i].Height;
            }
            int enddisplayrowindex = grid.EndDisplayedRowIndex;
            int firstdisplayrowindex = grid.FirstDisplayedRowIndex;
            if (rmin >= firstdisplayrowindex && rmin <= enddisplayrowindex)
            {
                rect.Y = grid.Rows[rmin].Top;
            }
            if (rmin < firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                int th = 0;
                for (int i = rmin; i < firstdisplayrowindex; i++)
                {
                    th = th + grid.Rows[i].Height;
                }
                rect.Y = grid.Rows[rmin].Top - th;
            }
            else if (rmin < firstdisplayrowindex && rmax < firstdisplayrowindex)
            {
                rect.Y = 0;
            }
            else if (rmin > enddisplayrowindex && rmax > enddisplayrowindex)
            {
                rect.Y = 0;
            }
            else if (rmin < firstdisplayrowindex && rmax < enddisplayrowindex)
            {
                rect.Y = grid.Rows[rmax].Bottom - h;
            }
            else if (rmin > firstdisplayrowindex && rmax > enddisplayrowindex)
            {
                rect.Y = grid.Rows[rmin].Top;
            }

            rect.Height = h;
        }
        private static void SetSizeTopRow(ICell begincell, ICell endcell, ref Rectangle rect)
        {
            CalcRowTopRow(begincell, endcell, ref rect);
            CalcColumnTopRow(begincell, endcell, ref rect);
        }
        private static void CalcColumnTopRow(ICell begincell, ICell endcell, ref Rectangle rect)
        {
            DataExcel grid = begincell.Grid;
            int cmax = System.Math.Max(begincell.MaxColumnIndex, endcell.MaxColumnIndex);
            int cmin = System.Math.Min(begincell.Column.Index, endcell.Column.Index);
            int w = 0;
            IColumn column = null;
            for (int i = cmin; i <= cmax; i++)
            {
                column = grid.Columns[i];
                if (column != null)
                {
                    w = w + column.Width;
                }
            }
            column = grid.Columns[cmin];
            if (column != null)
            {
                rect.X = column.Left;
                rect.Width = w;
            }
        }
        private static void CalcRowTopRow(ICell begincell, ICell endcell, ref Rectangle rect)
        {
            DataExcel grid = begincell.Grid;
            int rmax = System.Math.Max(begincell.MaxRowIndex, endcell.MaxRowIndex);
            int rmin = System.Math.Min(begincell.Row.Index, endcell.Row.Index);
            int h = 0;
            for (int i = rmin; i <= rmax; i++)
            {
                h = h + grid.Rows[i].Height;
            }
            rect.Y = grid.Rows[rmin].Top;
            rect.Height = h;
        }

        public virtual void CheckUpdate()
        {
            if (this.UpdateInfo.UpdateMode == 1)
            {

            }
        }
        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerHidden]
        public unsafe static string GetMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Feng.IO.BitConver.GetBytes(input));
            string key = Convert.ToBase64String(data);
            return key;

        }

        public static string GetUser()
        {
            string keyname = @"Software\booxin\DataExcel\" + Feng.DataUtlis.SmallVersion.AssemblySecondVersion;
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyname);
            if (key != null)
            {
                object CopyRightName = null;
                CopyRightName = key.GetValue("CName", CopyRightName);
                if (CopyRightName != null)
                {
                    string strCopyRightName = CopyRightName.ToString();
                    return strCopyRightName;
                }
            }
            return ConstantValue.PirateEdition;
        }

        private static bool Legitimate
        {
            get
            {
                string keyname = @"Software\booxin\DataExcel\" + Feng.DataUtlis.SmallVersion.AssemblySecondVersion;
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyname);
                if (key != null)
                {
                    object CopyRightName = null;
                    CopyRightName = key.GetValue("CName", CopyRightName);
                    if (CopyRightName != null)
                    {
                        string strCopyRightName = CopyRightName.ToString();
                        object CopyRightKey = null;
                        CopyRightKey = key.GetValue("CKey", CopyRightKey);
                        if (CopyRightKey != null)
                        {
                            if (strCopyRightName == GetMd5Hash(CopyRightName + Product.AssemblyProduct + Feng.DataUtlis.SmallVersion.AssemblySecondVersion))
                            {
                                return true;
                            }

                        }
                    }

                }
                return false;
            }
        }

        public static bool GenuineValidation()
        {

            try
            {
                string keyname = @"Software\booxin\DataExcel\" + Feng.DataUtlis.SmallVersion.AssemblySecondVersion;
                //MessageBox.Show(keyname);
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyname + NewVersion.LastVersion);

                if (key != null)
                {
                    object CopyRightName = null;
                    CopyRightName = key.GetValue("CName", CopyRightName);
                    if (CopyRightName != null)
                    {
                        string strCopyRightName = CopyRightName.ToString();
                        object CopyRightKey = null;
                        CopyRightKey = key.GetValue("CKey", CopyRightKey);//CopyRightKey
                        if (CopyRightKey != null)
                        {
                            string strCopyRightKey = CopyRightKey.ToString();
                            string keyvalue = GetMd5Hash(CopyRightName + Product.AssemblyProduct + Feng.DataUtlis.SmallVersion.AssemblySecondVersion);
                            if (strCopyRightKey == keyvalue)
                            {
                                return true;
                            }
                        }
                    }

                }
            }
            catch
            {

            }

#warning Move Befor Publish

            return DateTime.Now < new DateTime(2020, 12, 31);

        }

        public static string GetFileName(string dllname)
        {
            return System.Windows.Forms.Application.StartupPath + "\\" + dllname;
        }

        public static object CreateInatance(string filename, string fullname, object[] args)
        {
            Assembly.LoadFrom(filename);
            Type type = Type.GetType(filename, false);
            if (type != null)
            {
                return Activator.CreateInstance(type, args);
            }
            return null;
        }

        public static T CreateInatance<T>(string filename, string fullname, string downloadurl, DataExcel grid, object[] args) where T : class
        {
            if (!System.IO.File.Exists(filename))
            {
                try
                {
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        wc.DownloadFile(downloadurl, filename);
                    }
                }
                catch (Exception ex)
                {
                    grid.OnException(ex);
                }

            }
            if (!System.IO.File.Exists(filename))
            {
                return null;
            }
            Assembly.LoadFrom(filename);
            Type type = Type.GetType(filename, false);
            if (type != null)
            {
                return Activator.CreateInstance(type, args) as T;
            }
            return null;
        }

        public static void Read(IO.BinaryReader stream, byte btype)
        {
            switch (btype)
            {
                case TypeEnum.Tbool:
                    stream.ReadBoolean();
                    break;
                case TypeEnum.Tbyte:
                    stream.ReadByte();
                    break;
                case TypeEnum.Tbytes:
                    stream.ReadBytes();
                    break;
                case TypeEnum.Tchar:
                    stream.ReadChar();
                    break;
                case TypeEnum.Tchars:
                    stream.ReadChars();
                    break;
                case TypeEnum.TColor:
                    stream.ReadColor();
                    break;
                case TypeEnum.TDateTime:
                    stream.ReadDateTime();
                    break;
                case TypeEnum.Tdecimal:
                    stream.ReadDecimal();
                    break;
                case TypeEnum.Tdouble:
                    stream.ReadDouble();
                    break;
                case TypeEnum.Tfloat:
                    stream.ReadSingle();
                    break;
                case TypeEnum.Tint:
                    stream.ReadInt32();
                    break;
                case TypeEnum.Tlong:
                    stream.ReadInt64();
                    break;
                case TypeEnum.TObject:
                    stream.ReadBuffer();
                    break;
                case TypeEnum.Tsbyte:
                    stream.ReadSByte();
                    break;
                case TypeEnum.Tshort:
                    stream.ReadInt16();
                    break;
                case TypeEnum.Tstring:
                    stream.ReadString();
                    break;
                case TypeEnum.Tuint:
                    stream.ReadUInt32();
                    break;
                case TypeEnum.Tulong:
                    stream.ReadUInt64();
                    break;
                case TypeEnum.Tushort:
                    stream.ReadUInt16();
                    break;
                default:
                    break;
            }
        }

        public static ICellEditControl GetCellEdit(DataExcel grid, DataStruct ds)
        {
            ICellEditControl owncontrol = null;
            if (!string.IsNullOrEmpty(ds.DllName))
            {
                owncontrol = DataExcel.CreateInatance<ICellEditControl>(ds.DllName, ds.FullName,
ds.AessemlyDownLoadUrl, grid, null);
            }
            else
            {
                string fullname = ds.FullName;
                owncontrol = GetCellEdit(grid, fullname);
            }
            return owncontrol;
        }

        public static ICellEditControl GetCellEdit(DataExcel grid, string fullname)
        {
            ICellEditControl owncontrol = null;
            if (fullname == typeof(CellButton).FullName)
            {
                owncontrol = new CellButton(grid);
            } 
            else if (fullname == typeof(CellTextBoxEdit).FullName)
            {
                owncontrol = new CellTextBoxEdit(grid);
            }
            else if (fullname == typeof(CellCheckBox).FullName)
            {
                owncontrol = new CellCheckBox();
            }
            else if (fullname == typeof(CellVector).FullName)
            {
                owncontrol = new CellVector(grid);
            }
            else if (fullname == typeof(CellComboBox).FullName)
            {
                owncontrol = new CellComboBox(grid);
            }
            else if (fullname == typeof(CellColumnHeader).FullName)
            {
                owncontrol = new CellColumnHeader(grid);
            }
            //else if (fullname == typeof(CellDateTime).FullName)
            //{
            //    owncontrol = new CellDateTime(grid);
            //}
            else if (fullname == typeof(CellRadioCheckBox).FullName)
            {
                owncontrol = new CellRadioCheckBox(grid);
            }
            else if (fullname == typeof(CellSpText).FullName)
            {
                owncontrol = new CellSpText(grid);
            }

            else if (fullname == typeof(CellPassword).FullName)
            {
                owncontrol = new CellPassword(grid);
            }
            else if (fullname == typeof(CellImage).FullName)
            {
                owncontrol = new CellImage(grid);
            }
            else if (fullname == typeof(CellTimer).FullName)
            {
                owncontrol = new CellTimer(grid);
            }
            else if (fullname == typeof(CellFolderBrowserEdit).FullName)
            {
                owncontrol = new CellFolderBrowserEdit(grid);
            }
            else if (fullname == typeof(CellMoveForm).FullName)
            {
                owncontrol = new CellMoveForm(grid);
            }
            else if (fullname == typeof(CellColor).FullName)
            {
                owncontrol = new CellColor(grid);
            }
            else if (fullname == typeof(CellProcess).FullName)
            {
                owncontrol = new CellProcess(grid);
            }
            else if (fullname == typeof(CellFileSelectEdit).FullName)
            {
                owncontrol = new CellFileSelectEdit(grid);
            }
            else if (fullname == typeof(CellNumber).FullName)
            {
                owncontrol = new CellNumber(grid);
            }
            else if (fullname == typeof(CellCnNumber).FullName)
            {
                owncontrol = new CellCnNumber(grid);
            }
 
            else if (fullname == typeof(CellColumnHeader).FullName)
            {
                owncontrol = CellColumnHeader.Instance(grid);
            }
            else if (fullname == typeof(CellRowHeader).FullName)
            {
                owncontrol = CellRowHeader.Instance(grid);
            }
            else if (fullname == typeof(CellToolBar).FullName)
            {
                owncontrol = new CellToolBar(grid);
            }
            else if (fullname == typeof(CellLabel).FullName)
            {
                owncontrol = new CellLabel(grid);
            }
            else if (fullname == typeof(CellLinkLabel).FullName)
            {
                owncontrol = new CellLinkLabel(grid);
            } 
            else if (fullname == typeof(CellCnCurrency).FullName)
            {
                owncontrol = new CellCnCurrency(grid);
            }
 
            else if (fullname == typeof(CellGridView).FullName)
            {
                owncontrol = new CellGridView(grid);
            } 
            else if (fullname == typeof(CellDropDownDataExcel).FullName)
            {
                owncontrol = new CellDropDownDataExcel(grid);
            }
            else if (fullname == typeof(CellDropDownDateTime).FullName)
            {
                owncontrol = new CellDropDownDateTime(grid);
            }
            else if (fullname == typeof(CellDropDownFillter).FullName)
            {
                owncontrol = new CellDropDownFillter(grid);
            }
            //else if (fullname == typeof(CellDropDownDate).FullName)
            //{
            //    owncontrol = new CellDropDownDate(grid);
            //}
            else if (fullname == typeof(CellSplitRow).FullName)
            {
                owncontrol = new CellSplitRow(grid);
            }
            else if (fullname == typeof(CellSplitColumn).FullName)
            {
                owncontrol = new CellSplitColumn(grid);
            }
            else if (fullname == typeof(CellTreeView).FullName)
            {
                owncontrol = new CellTreeView(grid);
            }
            else if (fullname == typeof(CellFileSelectEdit).FullName)
            {
                owncontrol = new CellFileSelectEdit(grid);
            } 
 
            //else if (fullname == typeof(CellTime).FullName)
            //{
            //    owncontrol = new CellTime(grid);
            //}
            else if (fullname == typeof(CellSwitch).FullName)
            {
                owncontrol = new CellSwitch(grid);
            }
            else if (fullname == typeof(CellExcel).FullName)
            {
                owncontrol = new CellExcel(grid);
            }
            return owncontrol;
        }

        public static ICellEditControl GetCellEdit(string dllname, string fullname, string AessemlyDownLoadUrl, DataExcel grid, ICell cell)
        {
            ICellEditControl owncontrol = null;
            if (!string.IsNullOrEmpty(dllname))
            {
                owncontrol = DataExcel.CreateInatance<ICellEditControl>(dllname, fullname,
AessemlyDownLoadUrl, grid, new object[] { cell });
            }
            else
            {
                owncontrol = GetCellEdit(grid, fullname);
            }
            return owncontrol;
        }

        public static ICellEditControl GetCellEdit(DataExcel grid, Type type)
        {
            if (type == null)
                return null;
            if (type.BaseType != null)
            {
                if (type.BaseType.FullName == "System.Enum")
                {
                    CellComboBox combobox = new CellComboBox(grid);
                    foreach (string str in Enum.GetNames(type))
                    {
                        combobox.Items.Add(str);
                    }
                    return combobox;
                }
            }
            //if (type == typeof(DateTime))
            //{
            //    return new CellDateTime(grid);
            //}
            else if (type == typeof(UInt32))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(UInt16))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(UInt64))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(Int32))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(Int16))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(Int64))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(decimal))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(int))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(double))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(bool))
            {
                return new CellCheckBox();
            }
            else if (type == typeof(bool))
            {
                return new CellVector(grid);
            }



            //if (type == typeof(DateTime?))
            //{
            //    return new CellDateTime(grid);
            //}
            else if (type == typeof(UInt32?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(UInt16?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(UInt64?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(Int32?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(Int16?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(Int64?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(decimal?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(int?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(double?))
            {
                return new CellNumber(grid);
            }
            else if (type == typeof(bool?))
            {
                return new CellCheckBox();
            }
            else if (type == typeof(Image))
            {
                return new CellImage(grid);
            }
            else if (type == typeof(Color))
            {
                return new CellColor(grid);
            }
            return null;
        }

        public virtual void ShowToolTip(string tooltip)
        {
            ShowToolTip(tooltip, ToolTipShowTime);
        }

        public virtual void ShowToolTip(string tooltip, int showtimes)
        {
            this.ToolTip = tooltip;
            tooltipshowtimes = 0;
            this.ToolTipShowTime = showtimes;
            System.Threading.Thread thread = new System.Threading.Thread(BackShowToolTip);
            thread.IsBackground = true;
            thread.Start();
        }

        public void ShowToolTip(string v1, int v2, Feng.Forms.EventHandlers.ObjectEventHandler closeForm)
        {
            closeForm(closeForm);
        }

        private void ThreadPropertyChanged()
        {
            try
            {
                Form form = this.FindForm();
                if (form == null)
                {
                    return;
                }

                if (form.Disposing)
                {
                    return;
                }
                if (form.IsDisposed)
                {
                    return;
                }
                this.Invalidate();
            }
            catch (Exception ex)
            { 
            }
        }

        private int tooltipshowtimes = 0;
        private bool runingshowtooltip = false;
        public void BackShowToolTip()
        {
            try
            {
                if (runingshowtooltip)
                    return;
                ToolTipVisible = true;
                runingshowtooltip = true;
                ThreadPropertyChanged();
                while (tooltipshowtimes < ToolTipShowTime)
                {
                    ToolTipVisible = true;
                    tooltipshowtimes++;
                    System.Threading.Thread.Sleep(100);
                }
                ToolTipVisible = false;
                ThreadPropertyChanged();
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
            finally
            {
                ToolTipVisible = false;
                runingshowtooltip = false;
            }
        }

        public virtual void CellMoveUp(ISelectCellCollection cells)
        {
            CellMoveUp(cells, 1);
            //if (cells != null)
            //{
            //    int minrow = cells.MinRow();
            //    int maxrow = cells.MaxRow();
            //    int mincolumn = cells.MinColumn();
            //    int maxcolumn = cells.MaxColumn();

            //    for (int i = minrow; i <= maxrow; i++)
            //    {
            //        for (int j = mincolumn; j <= maxcolumn; j++)
            //        {
            //            if (i < 2)
            //            {
            //                return;
            //            }
            //            ICell scell = this.GetCell(i - 1, j);
            //            ICell tcell = this[i, j];
            //            this.Swap(scell, tcell);
            //        }
            //    }
            //    RefreshExtendCells();
            //}
        }
        public virtual void CellMoveDown(ISelectCellCollection cells)
        {
            CellMoveDown(cells, 1);
            //if (cells != null)
            //{
            //    int minrow = cells.MinRow();
            //    int maxrow = cells.MaxRow();
            //    int mincolumn = cells.MinColumn();
            //    int maxcolumn = cells.MaxColumn();

            //    for (int i = maxrow; i >= minrow; i--)
            //    {
            //        for (int j = mincolumn; j <= maxcolumn; j++)
            //        {
            //            ICell scell = this.GetCell(i + 1, j);
            //            ICell tcell = this[i, j];
            //            this.Swap(scell, tcell);
            //        }
            //    }
            //    RefreshExtendCells();
            //}
        }
        public virtual void CellMoveLeft(ISelectCellCollection cells)
        {
            CellMoveLeft(cells, 1);
            //if (cells != null)
            //{
            //    int minrow = cells.MinRow();
            //    int maxrow = cells.MaxRow();
            //    int mincolumn = cells.MinColumn();
            //    int maxcolumn = cells.MaxColumn();

            //    for (int i = minrow; i <= maxrow; i++)
            //    {
            //        for (int j = mincolumn; j <= maxcolumn; j++)
            //        {
            //            if (j < 2)
            //            {
            //                return;
            //            }
            //            ICell scell = this.GetCell(i, j - 1);
            //            ICell tcell = this[i, j];
            //            this.Swap(scell, tcell);
            //        }
            //    }
            //    RefreshExtendCells();
            //}
        }
        public virtual void CellMoveRight(ISelectCellCollection cells)
        {
            CellMoveRight(cells, 1);
            //if (cells != null)
            //{
            //    int minrow = cells.MinRow();
            //    int maxrow = cells.MaxRow();
            //    int mincolumn = cells.MinColumn();
            //    int maxcolumn = cells.MaxColumn();

            //    for (int i = minrow; i <= maxrow; i++)
            //    {
            //        for (int j = maxcolumn; j >= mincolumn; j--)
            //        {

            //            ICell scell = this.GetCell(i, j + 1);
            //            ICell tcell = this[i, j];
            //            this.Swap(scell, tcell);
            //        }
            //    }
            //    RefreshExtendCells();
            //}
        }


        public virtual void CellMoveUp(ISelectCellCollection cells, int step)
        {
            if (cells != null)
            {
                int minrow = cells.MinRow();
                int maxrow = cells.MaxRow();
                int mincolumn = cells.MinColumn();
                int maxcolumn = cells.MaxColumn();

                for (int i = minrow; i <= maxrow; i++)
                {
                    for (int j = mincolumn; j <= maxcolumn; j++)
                    {
                        if (i < 2)
                        {
                            return;
                        }
                        ICell scell = this.GetCell(i - step, j);
                        ICell tcell = this[i, j];
                        this.Swap(scell, tcell);
                    }
                }
                RefreshExtendCells();
            }
        }
        public virtual void CellMoveDown(ISelectCellCollection cells, int step)
        {
            if (cells != null)
            {
                int minrow = cells.MinRow();
                int maxrow = cells.MaxRow();
                int mincolumn = cells.MinColumn();
                int maxcolumn = cells.MaxColumn();

                for (int i = maxrow; i >= minrow; i--)
                {
                    for (int j = mincolumn; j <= maxcolumn; j++)
                    {
                        ICell scell = this.GetCell(i + step, j);
                        ICell tcell = this[i, j];
                        this.Swap(scell, tcell);
                    }
                }
                RefreshExtendCells();
            }
        }
        public virtual void CellMoveLeft(ISelectCellCollection cells, int step)
        {
            if (cells != null)
            {
                int minrow = cells.MinRow();
                int maxrow = cells.MaxRow();
                int mincolumn = cells.MinColumn();
                int maxcolumn = cells.MaxColumn();

                for (int i = minrow; i <= maxrow; i++)
                {
                    for (int j = mincolumn; j <= maxcolumn; j++)
                    {
                        if (j < 2)
                        {
                            return;
                        }
                        ICell scell = this.GetCell(i, j - step);
                        ICell tcell = this[i, j];
                        this.Swap(scell, tcell);
                    }
                }
                RefreshExtendCells();
            }
        }
        public virtual void CellMoveRight(ISelectCellCollection cells, int step)
        {
            if (cells != null)
            {
                int minrow = cells.MinRow();
                int maxrow = cells.MaxRow();
                int mincolumn = cells.MinColumn();
                int maxcolumn = cells.MaxColumn();

                for (int i = minrow; i <= maxrow; i++)
                {
                    for (int j = maxcolumn; j >= mincolumn; j--)
                    {

                        ICell scell = this.GetCell(i, j + step);
                        ICell tcell = this[i, j];
                        this.Swap(scell, tcell);
                    }
                }
                RefreshExtendCells();
            }
        }

        public virtual void CellMoveUp(ISelectCellCollection cells, ICell cell)
        {
            if (cells == null)
                return;
            if (cell == null)
                return;
            int step = cells.MinRow() - cell.Row.Index;
            CellMoveUp(cells, step);
        }
        public virtual void CellMoveDown(ISelectCellCollection cells, ICell cell)
        {
            if (cells == null)
                return;
            if (cell == null)
                return;
            int step = cell.Row.Index - cells.MinRow();
            CellMoveDown(cells, step);
        }
        public virtual void CellMoveLeft(ISelectCellCollection cells, ICell cell)
        {
            if (cells == null)
                return;
            if (cell == null)
                return;
            int step = cells.MinColumn() - cell.Column.Index;
            CellMoveLeft(cells, step);
        }
        public virtual void CellMoveRight(ISelectCellCollection cells, ICell cell)
        {
            if (cells == null)
                return;
            if (cell == null)
                return;
            int step = cell.Column.Index - cells.MinColumn();
            CellMoveRight(cells, 1);
        }

        public static string GetRegeditCode(string web)
        {
            string txt = string.Empty;
            char[] cs = web.ToCharArray();
            List<char> list = new List<char>();
            list.AddRange(cs);
            char[] year = "2022".ToCharArray(); 
            list.Insert(3, year[0]);
            list.Insert(5, year[1]);
            list.Insert(7, year[2]);
            list.Insert(9, year[3]);
            int n = 0;
            for (int i = 0; i < list.Count; i++)
            {
                n =n + list[i] - 128;
            }
            int n2 = n % 36;
            int n1 = n % 7;
            List<char> list2 = new List<char>();
            for (int i = 7; i < list.Count; i++)
            {
                list2.Add(Convert.ToChar(list[i]));
            }
            return txt;
        }

        public static string GetRegeditCode2(string web)
        {
            string txt = web;
            txt = txt + " are registered to todataexcel";

            return txt;
        }
         
    }
}
