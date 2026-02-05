using Feng.Enums;
using Feng.Excel.Collections;
using Feng.Excel.Edits;
using Feng.Excel.Interfaces;
using Feng.Excel.Styles;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        #region SetSelect        
        public virtual void SetSelectCellBorder(
            bool isall, bool clearall,
            bool isLeftTopRightbottom, bool LeftTopRightbottom,
            bool isAllLeftLine, bool AllLeftLine,
            bool isAllTopLine, bool AllTopLine,
            bool isAllRightLine, bool AllRightLine,
            bool isAllBottom, bool AllBottom,
            bool isLeftTopToRightBottom, bool LeftTopToRightBottom,
            bool isAllLeftBoomToRightTop, bool AllLeftBoomToRightTop
            )
        {
            Feng.Excel.Commands.CellCommands cmds = new Excel.Commands.CellCommands();

            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                if (isall)
                {
                    if (clearall)
                    {
                        if (cell.BorderStyle != null)
                        {

                            cell.BorderStyle.BottomLineStyle.Visible = false;
                            cell.BorderStyle.LeftLineStyle.Visible = false;
                            cell.BorderStyle.RightLineStyle.Visible = false;
                            cell.BorderStyle.TopLineStyle.Visible = false;
                        }
                    }
                    else
                    {
                        cell.SetSelectCellBorderBorderOutside();
                    }
                }
            }
        }

        public virtual List<ICell> GetSelectCells()
        {
            List<ICell> list = new List<ICell>();
            if (this.SelectRange.Count < 1)
            {
                if (this.SelectCells != null)
                {
                    list = SelectCells.GetAllCells();
                }
                else if (this.FocusedCell != null)
                {
                    if (this.FocusedCell.OwnMergeCell != null)
                    {
                        list.Add(this.FocusedCell.OwnMergeCell);
                    }
                    else
                    {
                        list.Add(this.FocusedCell);
                    }
                }

                List<IColumn> columns = new List<IColumn>();
                foreach (IColumn item in this.Columns)
                {
                    if (item.Selected)
                    {
                        columns.Add(item);
                    }
                }
                if (columns.Count > 0)
                {
                    foreach (IColumn item in columns)
                    {
                        foreach (IRow row in this.Rows)
                        {
                            if (row.Index > 0)
                            {
                                ICell cell = row.GetCellByIndex(item.Index);
                                if (cell != null)
                                {
                                    if (cell.OwnMergeCell != null)
                                    {
                                        cell = cell.OwnMergeCell;
                                    }
                                    list.Add(cell);
                                }
                            }
                        }
                    }
                }


                List<IRow> rows = new List<IRow>();
                foreach (IRow item in this.Rows)
                {
                    if (item.Selected)
                    {
                        rows.Add(item);
                    }
                }
                if (rows.Count > 0)
                {
                    foreach (IRow row in rows)
                    {
                        foreach (IColumn item in this.Columns)
                        {
                            if (row.Index > 0 || item.Index > 0)
                            {
                                ICell cell = row.GetCellByIndex(item.Index);
                                if (cell != null)
                                {
                                    if (cell.OwnMergeCell != null)
                                    {
                                        cell = cell.OwnMergeCell;
                                    }
                                    list.Add(cell);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                list.AddRange(this.SelectRange.ToArray());
            }
            return list;
        }

        public virtual List<IRow> GetSelectRows()
        {
            List<IRow> rows = new List<IRow>();
            foreach (IRow item in this.Rows)
            {
                if (item.CellSelect)
                {
                    rows.Add(item);
                }
            }

            return rows;
        }
        public virtual void SetSelectCellReadOnly(bool isreadonly)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellReadOnly(list, isreadonly);
        }
        public virtual void SetSelectCellReadOnly(SelectCellCollection cells, bool isreadonly)
        {
            List<ICell> list = cells.GetAllCells();
            SetSelectCellReadOnly(list, isreadonly);
        }
        public virtual void SetSelectCellReadOnly(List<ICell> list,bool isreadonly)
        {
            if (list.Count < 1)
            {
                return;
            }
            foreach (ICell cell in list)
            {
                cell.ReadOnly = isreadonly;
                cell.InhertReadOnly = false;
            }
        }


        public virtual void SetSelectCellBorderOutsideClear(bool clearall)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellBorderOutsideClear(list, clearall);
        }
        public virtual void SetSelectCellBorderOutsideClear(List<ICell> list, bool clearall)
        {
            if (list.Count < 1)
            {
                return;
            }
            Feng.Excel.Commands.CellCommands cmds = new Excel.Commands.CellCommands();
            int minrow = -1;
            int mincolumn = -1;
            int maxrow = -1;
            int maxcolumn = -1;
            foreach (ICell item in list)
            {
                if (minrow == -1)
                {
                    minrow = item.Row.Index;
                }
                if (minrow > item.Row.Index)
                {
                    minrow = item.Row.Index;
                }
                if (mincolumn == -1)
                {
                    mincolumn = item.Column.Index;
                }
                if (mincolumn > item.Column.Index)
                {
                    mincolumn = item.Column.Index;
                }

                if (maxrow == -1)
                {
                    maxrow = item.MaxRowIndex;
                }
                if (maxrow < item.MaxRowIndex)
                {
                    maxrow = item.MaxRowIndex;
                }
                if (maxcolumn == -1)
                {
                    maxcolumn = item.MaxColumnIndex;
                }
                if (maxcolumn < item.MaxColumnIndex)
                {
                    maxcolumn = item.MaxColumnIndex;
                }
            }

            foreach (ICell cell in list)
            {
                if (cell.MaxColumnIndex == maxcolumn)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.RightLineStyle.Visible = false;
                    }

                }
                if (cell.Column.Index == mincolumn)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.LeftLineStyle.Visible = false;
                    }
                }


                if (cell.MaxRowIndex == maxrow)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.BottomLineStyle.Visible = false;
                    }
                }
                if (cell.Row.Index == minrow)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.TopLineStyle.Visible = false;
                    }
                }
            }
        }

        public virtual void SetSelectCellBorderClear(bool clearall)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellBorderClear(list, clearall);
        }
        public virtual void SetSelectCellBorderClear(List<ICell> list, bool clearall)
        {
            if (list.Count < 1)
            {
                return;
            }
            foreach (ICell cell in list)
            {
                if (clearall)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.BottomLineStyle.Visible = false;
                        cell.BorderStyle.LeftLineStyle.Visible = false;
                        cell.BorderStyle.RightLineStyle.Visible = false;
                        cell.BorderStyle.TopLineStyle.Visible = false;
                    }
                }
                else
                {
                    cell.SetSelectCellBorderBorderOutside();

                }
            }
        }
        public virtual void CellBorderClear(ICell cell, bool clearall)
        {
            if (clearall)
            {
                if (cell.BorderStyle != null)
                {
                    cell.BorderStyle.BottomLineStyle.Visible = false;
                    cell.BorderStyle.LeftLineStyle.Visible = false;
                    cell.BorderStyle.RightLineStyle.Visible = false;
                    cell.BorderStyle.TopLineStyle.Visible = false;
                }
            }
            else
            {
                cell.SetSelectCellBorderBorderOutside();
            }
        }

        public virtual void SetSelectCellBorderTop(bool clearall)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellBorderTop(list, clearall);
        }
        public virtual void SetSelectCellBorderTop(List<ICell> list, bool clearall)
        {
            if (list.Count < 1)
            {
                return;
            }
            Feng.Excel.Commands.CellCommands cmds = new Excel.Commands.CellCommands();

            foreach (ICell cell in list)
            {
                if (clearall)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.TopLineStyle.Visible = false;
                    }
                }
                else
                {
                    this.CreateCellBorderTop(cell);
                }

            }
        }
        public virtual void SetSelectCellBorderBottom(bool clearall)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellBorderBottom(list, clearall);
        }
        public virtual void SetSelectCellBorderBottom(List<ICell> list, bool clearall)
        {
            if (list.Count < 1)
            {
                return;
            }
            foreach (ICell cell in list)
            {

                if (clearall)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.BottomLineStyle.Visible = false;
                    }
                }
                else
                {
                    this.CreateCellBorderBottom(cell);
                }
            }
        }

        public virtual void SetSelectCellBorderRight(bool clearall)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellBorderRight(list, clearall);
        }
        public virtual void SetSelectCellBorderRight(List<ICell> list, bool clearall)
        {
            if (list.Count < 1)
            {
                return;
            }
            Feng.Excel.Commands.CellCommands cmds = new Excel.Commands.CellCommands();

            foreach (ICell cell in list)
            {

                if (clearall)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.RightLineStyle.Visible = false;
                    }
                }
                else
                {
                    this.CreateCellBorderRight(cell);
                }
            }
        }
        public virtual void SetSelectCellBorderLeft(bool clearall)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellBorderLeft(list, clearall);
        }
        public virtual void SetSelectCellBorderLeft(List<ICell> list, bool clearall)
        {
            if (list.Count < 1)
            {
                return;
            }
            Feng.Excel.Commands.CellCommands cmds = new Excel.Commands.CellCommands();
            foreach (ICell cell in list)
            {
                if (clearall)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.LeftLineStyle.Visible = false;
                    }
                }
                else
                {
                    this.CreateCellBorderLeft(cell);
                }
            }
        }

        public virtual void SetSelectCellBorderLeftBottomToRightTop(bool clearall)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellBorderLeftBottomToRightTop(list, clearall);
        }
        public virtual void SetSelectCellBorderLeftBottomToRightTop(List<ICell> list, bool clearall)
        {
            if (list.Count < 1)
            {
                return;
            }
            Feng.Excel.Commands.CellCommands cmds = new Excel.Commands.CellCommands();
            foreach (ICell cell in list)
            {
                if (clearall)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.Visible = false;
                    }
                }
                else
                {
                    this.CreateCellBorderLeftBottomToRightTop(cell);
                }
            }
        }
        public virtual void SetSelectCellBorderLeftTopToRightBottom(bool clearall)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellBorderLeftTopToRightBottom(list, clearall);
        }
        public virtual void SetSelectCellBorderLeftTopToRightBottom(List<ICell> list, bool clearall)
        {
            if (list.Count < 1)
            {
                return;
            }
            foreach (ICell cell in list)
            {
                if (clearall)
                {
                    if (cell.BorderStyle != null)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = false;
                    }
                }
                else
                {
                    this.CreateCellBorderLeftTopToRightBottom(cell);
                }
            }
        }
        public virtual void SetSelectCellBoarderNull()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle.Visible = false;
                cell.BorderStyle.TopLineStyle.Visible = false;
                cell.BorderStyle.RightLineStyle.Visible = false;
                cell.BorderStyle.BottomLineStyle.Visible = false;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderBorderOutside()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.SetSelectCellBorderBorderOutside();
            }
            this.EndReFresh();
        }


        public virtual void SetSelectCellBorderLeftTopRightbottomWidth(int width)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }

                cell.BorderStyle.LeftLineStyle.Width = width;
                cell.BorderStyle.TopLineStyle.Width = width;
                cell.BorderStyle.RightLineStyle.Width = width;
                cell.BorderStyle.BottomLineStyle.Width = width;

                cell.BorderStyle.LeftLineStyle.Visible = true;
                cell.BorderStyle.TopLineStyle.Visible = true;
                cell.BorderStyle.RightLineStyle.Visible = true;
                cell.BorderStyle.BottomLineStyle.Visible = true;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderLeftTopRightbottomColor(Color color)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle.Visible = true;
                cell.BorderStyle.TopLineStyle.Visible = true;
                cell.BorderStyle.RightLineStyle.Visible = true;
                cell.BorderStyle.BottomLineStyle.Visible = true;
                cell.BorderStyle.LeftLineStyle.Color = color;
                cell.BorderStyle.TopLineStyle.Color = color;
                cell.BorderStyle.RightLineStyle.Color = color;
                cell.BorderStyle.BottomLineStyle.Color = color;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftLineBorder()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }

                cell.BorderStyle.LeftLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftLineBorderWidth(int width)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }

                cell.BorderStyle.LeftLineStyle.Visible = true;
                cell.BorderStyle.LeftLineStyle.Width = width;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftLineBorderColor(Color color)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }

                cell.BorderStyle.LeftLineStyle.Visible = true;
                cell.BorderStyle.LeftLineStyle.Color = color;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTopLineBorder()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.TopLineStyle.Visible = true;

            }
            this.EndReFresh();
        }

        public virtual void CreateCellBorderTop(ICell cell)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.TopLineStyle.Visible = true;
        }
        public virtual void CreateCellBorderTop(ICell cell,bool value)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.TopLineStyle.Visible = value;
        }
        public virtual void CreateCellBorderBottom(ICell cell)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.BottomLineStyle.Visible = true;
        }
        public virtual void CreateCellBorderBottom(ICell cell,bool value)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.BottomLineStyle.Visible = value;
        }
        public virtual void CreateCellBorderRight(ICell cell)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.RightLineStyle.Visible = true;
        }
        public virtual void CreateCellBorderRight(ICell cell,bool value)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.RightLineStyle.Visible = value;
        }

        public virtual void CreateCellBorderLeft(ICell cell)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.LeftLineStyle.Visible = true;
        }
        public virtual void CreateCellBorderLeft(ICell cell,bool value)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.LeftLineStyle.Visible = value;
        }
        public virtual void CreateCellBorderLeftTopToRightBottom(ICell cell)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = true;
        }
        public virtual void CreateCellBorderLeftBottomToRightTop(ICell cell)
        {
            if (cell.BorderStyle == null)
            {
                cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
            }
            cell.BorderStyle.LeftBottomToRightTopLineStyle.Visible = true;
        }

        public virtual void SetSelectCellAllTopLineBorderWidth(int width)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }

                cell.BorderStyle.TopLineStyle.Visible = true;
                cell.BorderStyle.TopLineStyle.Width = width;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTopLineBorderColor(Color color)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }

                cell.BorderStyle.TopLineStyle.Visible = true;
                cell.BorderStyle.TopLineStyle.Color = color;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllRightLineBorder()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.RightLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllRightLineBorderColor(Color color)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.RightLineStyle.Visible = true;
                cell.BorderStyle.RightLineStyle.Color = color;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllRightLineBorderWidth(int width)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.RightLineStyle.Visible = true;
                cell.BorderStyle.RightLineStyle.Width = width;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllBottomLineBorder()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.BottomLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllBottomLineBorderWidth(int width)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.BottomLineStyle.Visible = true;
                cell.BorderStyle.BottomLineStyle.Width = width;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllBottomLineBorderColor(Color color)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.BottomLineStyle.Visible = true;
                cell.BorderStyle.BottomLineStyle.Color = color;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderLeftTopToRightBottom()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = true;

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellBorderLeftTopToRightBottomColor(Color color)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = true;
                cell.BorderStyle.LeftTopToRightBottomLineStyle.Color = color;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderLeftTopToRightBottomWidth(int width)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }

                cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = true;
                cell.BorderStyle.LeftTopToRightBottomLineStyle.Width = width;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftBoomToRightTopLineBorder()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftBottomToRightTopLineStyle.Visible = true;

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellAllBorder(LineStyle style)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                style.Visible = true;
                cell.BorderStyle.LeftLineStyle = style;
                cell.BorderStyle.TopLineStyle = style;
                cell.BorderStyle.RightLineStyle = style;
                cell.BorderStyle.BottomLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftLineBorder(LineStyle style)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                style.Visible = true;
                cell.BorderStyle.LeftLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTopLineBorder(LineStyle style)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                style.Visible = true;
                cell.BorderStyle.TopLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllRightLineBorder(LineStyle style)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                style.Visible = true;
                cell.BorderStyle.RightLineStyle = style;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllBottomLineBorder(LineStyle style)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                style.Visible = true;
                cell.BorderStyle.BottomLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellLineBorder(bool iscolor, Color Color,
            bool islinecap, LineCap StartCap,
            bool isendcap, LineCap EndCap,
            bool isdashstyle, DashStyle DashStyle,
            bool isDashCap, DashCap DashCap,
            bool isCompoundArray, float[] CompoundArray,
            bool isLineJoin, LineJoin LineJoin,
            bool isMiterLimit, int MiterLimit,
            bool isAlignment, PenAlignment Alignment,
            bool isWidth, int Width,
            bool isleft,
            bool isright,
            bool istop,
            bool isBottom,
            bool isLeftTopRightbottom,
            bool isRightTopLeftbottom
            )
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cel in list)
            {
                ICell cell = cel;
                #region Left
                if (isleft)
                {

                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }

                    if (cell.BorderStyle == null)
                    {
                        cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                    }
                    if (cell.BorderStyle.LeftLineStyle == null)
                    {
                        cell.BorderStyle.LeftLineStyle = this.ClassFactory.CreateLineStyle();
                    }
                    if (islinecap)
                    {
                        cell.BorderStyle.LeftLineStyle.StartCap = StartCap;
                    }
                    if (isendcap)
                    {
                        cell.BorderStyle.LeftLineStyle.EndCap = EndCap;
                    }
                    if (isdashstyle)
                    {
                        cell.BorderStyle.LeftLineStyle.DashStyle = DashStyle;
                    }
                    if (isDashCap)
                    {
                        cell.BorderStyle.LeftLineStyle.DashCap = DashCap;
                    }
                    if (isCompoundArray)
                    {
                        cell.BorderStyle.LeftLineStyle.CompoundArray = CompoundArray;
                    }
                    if (isLineJoin)
                    {
                        cell.BorderStyle.LeftLineStyle.LineJoin = LineJoin;
                    }
                    if (isMiterLimit)
                    {
                        cell.BorderStyle.LeftLineStyle.MiterLimit = MiterLimit;
                    }
                    if (isAlignment)
                    {
                        cell.BorderStyle.LeftLineStyle.Alignment = Alignment;
                    }
                    if (isWidth)
                    {
                        cell.BorderStyle.LeftLineStyle.Width = Width;
                    }
                    cell.BorderStyle.LeftLineStyle.Visible = true;

                }
                #endregion
                #region Right
                if (isright)
                {
                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }
                    if (cell.BorderStyle == null)
                    {
                        cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                    }
                    if (cell.BorderStyle.RightLineStyle == null)
                    {
                        cell.BorderStyle.RightLineStyle = this.ClassFactory.CreateLineStyle();
                    }
                    if (islinecap)
                    {
                        cell.BorderStyle.RightLineStyle.StartCap = StartCap;
                    }
                    if (isendcap)
                    {
                        cell.BorderStyle.RightLineStyle.EndCap = EndCap;
                    }
                    if (isdashstyle)
                    {
                        cell.BorderStyle.RightLineStyle.DashStyle = DashStyle;
                    }
                    if (isDashCap)
                    {
                        cell.BorderStyle.RightLineStyle.DashCap = DashCap;
                    }
                    if (isCompoundArray)
                    {
                        cell.BorderStyle.RightLineStyle.CompoundArray = CompoundArray;
                    }
                    if (isLineJoin)
                    {
                        cell.BorderStyle.RightLineStyle.LineJoin = LineJoin;
                    }
                    if (isMiterLimit)
                    {
                        cell.BorderStyle.RightLineStyle.MiterLimit = MiterLimit;
                    }
                    if (isAlignment)
                    {
                        cell.BorderStyle.RightLineStyle.Alignment = Alignment;
                    }
                    if (isWidth)
                    {
                        cell.BorderStyle.RightLineStyle.Width = Width;
                    }
                    cell.BorderStyle.RightLineStyle.Visible = true;

                }
                #endregion
                #region Top
                if (istop)
                {
                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }
                    if (cell.BorderStyle == null)
                    {
                        cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                    }
                    if (cell.BorderStyle.TopLineStyle == null)
                    {
                        cell.BorderStyle.TopLineStyle = this.ClassFactory.CreateLineStyle();
                    }
                    if (islinecap)
                    {
                        cell.BorderStyle.TopLineStyle.StartCap = StartCap;
                    }
                    if (isendcap)
                    {
                        cell.BorderStyle.TopLineStyle.EndCap = EndCap;
                    }
                    if (isdashstyle)
                    {
                        cell.BorderStyle.TopLineStyle.DashStyle = DashStyle;
                    }
                    if (isDashCap)
                    {
                        cell.BorderStyle.TopLineStyle.DashCap = DashCap;
                    }
                    if (isCompoundArray)
                    {
                        cell.BorderStyle.TopLineStyle.CompoundArray = CompoundArray;
                    }
                    if (isLineJoin)
                    {
                        cell.BorderStyle.TopLineStyle.LineJoin = LineJoin;
                    }
                    if (isMiterLimit)
                    {
                        cell.BorderStyle.TopLineStyle.MiterLimit = MiterLimit;
                    }
                    if (isAlignment)
                    {
                        cell.BorderStyle.TopLineStyle.Alignment = Alignment;
                    }
                    if (isWidth)
                    {
                        cell.BorderStyle.TopLineStyle.Width = Width;
                    }
                    cell.BorderStyle.TopLineStyle.Visible = true;

                }
                #endregion
                #region Bottom
                if (isBottom)
                {
                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }
                    if (cell.BorderStyle == null)
                    {
                        cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                    }
                    if (cell.BorderStyle.BottomLineStyle == null)
                    {
                        cell.BorderStyle.BottomLineStyle = this.ClassFactory.CreateLineStyle();
                    }
                    if (islinecap)
                    {
                        cell.BorderStyle.BottomLineStyle.StartCap = StartCap;
                    }
                    if (isendcap)
                    {
                        cell.BorderStyle.BottomLineStyle.EndCap = EndCap;
                    }
                    if (isdashstyle)
                    {
                        cell.BorderStyle.BottomLineStyle.DashStyle = DashStyle;
                    }
                    if (isDashCap)
                    {
                        cell.BorderStyle.BottomLineStyle.DashCap = DashCap;
                    }
                    if (isCompoundArray)
                    {
                        cell.BorderStyle.BottomLineStyle.CompoundArray = CompoundArray;
                    }
                    if (isLineJoin)
                    {
                        cell.BorderStyle.BottomLineStyle.LineJoin = LineJoin;
                    }
                    if (isMiterLimit)
                    {
                        cell.BorderStyle.BottomLineStyle.MiterLimit = MiterLimit;
                    }
                    if (isAlignment)
                    {
                        cell.BorderStyle.BottomLineStyle.Alignment = Alignment;
                    }
                    if (isWidth)
                    {
                        cell.BorderStyle.BottomLineStyle.Width = Width;
                    }
                    cell.BorderStyle.BottomLineStyle.Visible = true;

                }
                #endregion
                #region LeftTopToRightBoomLineStyle
                if (isLeftTopRightbottom)
                {
                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }
                    if (cell.BorderStyle == null)
                    {
                        cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                    }
                    if (cell.BorderStyle.LeftTopToRightBottomLineStyle == null)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle = this.ClassFactory.CreateLineStyle();
                    }
                    if (islinecap)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.StartCap = StartCap;
                    }
                    if (isendcap)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.EndCap = EndCap;
                    }
                    if (isdashstyle)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.DashStyle = DashStyle;
                    }
                    if (isDashCap)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.DashCap = DashCap;
                    }
                    if (isCompoundArray)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.CompoundArray = CompoundArray;
                    }
                    if (isLineJoin)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.LineJoin = LineJoin;
                    }
                    if (isMiterLimit)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.MiterLimit = MiterLimit;
                    }
                    if (isAlignment)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.Alignment = Alignment;
                    }
                    if (isWidth)
                    {
                        cell.BorderStyle.LeftTopToRightBottomLineStyle.Width = Width;
                    }
                    cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = true;

                }
                #endregion
                #region RightTopLeftbottom
                if (isRightTopLeftbottom)
                {
                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }
                    if (cell.BorderStyle == null)
                    {
                        cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                    }
                    if (cell.BorderStyle.LeftBottomToRightTopLineStyle == null)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle = this.ClassFactory.CreateLineStyle();
                    }
                    if (islinecap)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.StartCap = StartCap;
                    }
                    if (isendcap)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.EndCap = EndCap;
                    }
                    if (isdashstyle)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.DashStyle = DashStyle;
                    }
                    if (isDashCap)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.DashCap = DashCap;
                    }
                    if (isCompoundArray)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.CompoundArray = CompoundArray;
                    }
                    if (isLineJoin)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.LineJoin = LineJoin;
                    }
                    if (isMiterLimit)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.MiterLimit = MiterLimit;
                    }
                    if (isAlignment)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.Alignment = Alignment;
                    }
                    if (isWidth)
                    {
                        cell.BorderStyle.LeftBottomToRightTopLineStyle.Width = Width;
                    }
                    cell.BorderStyle.LeftBottomToRightTopLineStyle.Visible = true;

                }
                #endregion
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellLineBorder(LineStyle style)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cel in list)
            {
                ICell cell = cel;

                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }

                cell.BorderStyle.LeftLineStyle = style;

                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.RightLineStyle = style;

                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.TopLineStyle = style;

                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.BottomLineStyle = style;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderLine()
        {
            SetSelectCellLineBorder(this.ClassFactory.CreateLineStyle());
        }

        public virtual void SetSelectCellBorderLineColor(Color color)
        {
            LineStyle BorderStyle = this.ClassFactory.CreateLineStyle();
            BorderStyle.Color = color;
            SetSelectCellLineBorder(BorderStyle);
        }
        public virtual void SetSelectCellBorderLineWidth(int width)
        {
            LineStyle BorderStyle = this.ClassFactory.CreateLineStyle();
            BorderStyle.Width = width;
            SetSelectCellLineBorder(BorderStyle);
        }
        public virtual void SetAllDisplayColumnWidth(IColumn column)
        {
            if (column == null)
                return;
            if (!column.AllowChangedSize)
                return;
            
            int width = column.Width;
            foreach (IRow rh in this.AllVisibleRows)
            {
                ICell cell = rh[column];
                if (cell == null)
                {
                    continue;
                }
                if (cell.OwnMergeCell != null)
                {
                    continue;
                }
                cell.FreshContens();
                if (width < cell.ContensWidth)
                {
                    if (cell.ContensWidth < this.Width / 2)
                    {
                        width = cell.ContensWidth;
                    }
                }
            }
            column.Width = width;
        }

        public virtual void SetSelectCellAlignLineTop()
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(StringAlignment.Near);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignLineCenter()
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(StringAlignment.Center);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignLineBottom()
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(StringAlignment.Far);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLineAlign(StringAlignment mode)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.VerticalAlignment = mode;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringLeft()
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(StringAlignment.Near);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringCenter()
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(StringAlignment.Center);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringRight()
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(StringAlignment.Far);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringAlign(StringAlignment mode)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.HorizontalAlignment = mode;
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellTextBoxEdit(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellTextBoxEdit(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditNull()
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellEditNull(list);
        }
        public virtual void SetSelectCellEditNull(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellEdit(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFileSelectEdit(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellFileSelectEdit(this);
            }
            this.EndReFresh();
        }
 
        public virtual void SetSelectCellFileSelectEdit()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellFileSelectEdit(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditLabelCell()
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellEditLabelCell(list);
        }
        public virtual void SetSelectCellEditLabelCell(List<ICell> list)
        {

            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellLabel(this); ;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellToolBar(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellToolBar(cell);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditLinkLabelCell()
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellEditLinkLabelCell(list);
        }
        public virtual void SetSelectCellEditLinkLabelCell(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellLinkLabel(this); ;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCheckBoxCell()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellCheckBox();
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCellVector()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellVector(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditRadioBoxCell()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            string groupname = string.Empty;
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                if (groupname == string.Empty)
                {
                    groupname = cell.Caption;
                }
                CellRadioCheckBox edit = new CellRadioCheckBox(this);
                edit.ID = groupname;
                cell.OwnEditControl = edit;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditComboBoxCell()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.EditMode = EditMode.Default;
                cell.OwnEditControl = new CellComboBox(this);
            }
            this.EndReFresh();
        }
 
        public virtual void SetSelectCellDropDownDataExcel()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownDataExcel(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellDropDownDateTime()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownDateTime(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellDropDownFillter()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownFillter(this);
            }
            this.EndReFresh();
        }
        //public virtual void SetSelectCellDropDownDate()
        //{
        //    List<ICell> list = GetSelectCells();
        //    if (list.Count < 1)
        //    {
        //        return;
        //    }
        //    this.BeginReFresh();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellDropDownDate(this);
        //    }
        //    this.EndReFresh();
        //}
        public virtual void SetSelectCellSplitRow()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellSplitRow(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellSplitColumn()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellSplitColumn(this);
            }
            this.EndReFresh();
        }
 
        public virtual void SetSelectCellEditComboBoxCell(string[] items)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                CellComboBox ccb = new CellComboBox(this);
                cell.Caption = cell.Text;
                ccb.Items.AddRange(items);
                cell.EditMode = EditMode.Focused | EditMode.KeyDown;
                cell.OwnEditControl = ccb;
            }
            this.EndReFresh();
        }
        //public virtual void SetSelectCellEditDateTimeCell()
        //{
        //    List<ICell> list = GetSelectCells();
        //    if (list.Count < 1)
        //    {
        //        return;
        //    }
        //    this.BeginReFresh();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellDateTime(this);

        //    }
        //    this.EndReFresh();
        //}
        public virtual void SetSelectCellFileSelectEdit(CellFileSelectEdit ctl)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = ctl;

            }
            this.EndReFresh();
        }


        public virtual void SetSelectCellExcel(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                if (string.IsNullOrWhiteSpace(cell.Text))
                {
                    cell.Value = DateTime.Now;
                }
                cell.OwnEditControl = new CellExcel(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllButton()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellButton(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllSwitch()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellSwitch(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellGridView()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellGridView(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellVisible(bool visible)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Visible = false;
            }
            this.EndReFresh();
        }
 
        public virtual void SetSelectCellEditImageCell()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellImage(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCellTimer()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellTimer(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellCellFolderBrowserEdit()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellFolderBrowserEdit(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellMoveForm(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellMoveForm(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditColorCell()
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellEditColorCell(list);
        }
        public virtual void SetSelectCellProcessCell()
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellProcessCell(list);
        }
        public virtual void SetSelectCellEditColorCell(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellColor(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellProcessCell(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellProcess(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellCnNumber()
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellCnNumber(list);
        }
        public virtual void SetSelectCellCnNumber(List<ICell> list)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellCnNumber(this);
            }
            this.EndReFresh();
        }
 
 
        public virtual void SetSelectCellEditPasswordCell()
        {
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellPassword(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditNumberCell()
        {
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellNumber(this);

            }
            this.EndReFresh();
        }
 
        public virtual void SetSelectCellEditCnNumberCell()
        {
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellCnNumber(this);

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellEditCellColumnHeader()
        {
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = CellColumnHeader.Instance(this);

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellEditCnNumber2Cell()
        {
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellCnCurrency(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCellRowHeader()
        {
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = CellRowHeader.Instance(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellSwitchCell()
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellSwitch(this);
            }
            this.EndReFresh();
        }
         
        //public virtual void SetSelectCellAllColorTextCell()
        //{
        //    if (_SelectCells == null)
        //        return;
        //    this.BeginReFresh();
        //    List<ICell> list = _SelectCells.GelAllCells();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellBaseColorText( );

        //    }
        //    this.EndReFresh();
        //}
        //public virtual void SetSelectCellAllHTreeCell()
        //{
        //    if (_SelectCells == null)
        //        return;
        //    this.BeginReFresh();
        //    List<ICell> list = _SelectCells.GelAllCells();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellHTree( );

        //    }
        //    this.EndReFresh();
        //}

        public virtual void SetSelectCellEditControl(ICellEditControl ctrl)
        {
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = ctrl;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellPassWordEditControl()
        {
            if (_SelectCells == null)
            {
                if (this.FocusedCell != null)
                {
                    this.FocusedCell.OwnEditControl = new CellPassword(this);
                }
                return;
            }
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellPassword(this);

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellSpText(List<ICell> list)
        {

            if (list.Count < 1)
                return;
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                CellSpText st = new CellSpText(this);
                cell.Caption = cell.Text;
                cell.OwnEditControl = st;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellTextOrientationRotateDown(bool vertical)
        {

            if (_SelectCells == null)
                return;
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.DirectionVertical = vertical;

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellAutoMultiline(bool vertical)
        {

            if (_SelectCells == null)
                return;
            this.BeginReFresh();
            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.AutoMultiline = vertical;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFont(Font font)
        {
            if (SelectCells == null)
                return;
            this.BeginReFresh();

            List<ICell> list = GetSelectCells();
            foreach (ICell cell in list)
            {
                cell.Font = font;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFont(SelectCellCollection cells, Font font)
        {
            if (cells == null)
                return;
            this.BeginReFresh();

            List<ICell> list = cells.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Font = font;
            }
            this.EndReFresh();
        }
        private List<ICell> listtempfontapply = null;
        public virtual void SetSelectCellFont(List<ICell> list)
        {
            if (list.Count < 1)
                return;
            using (FontDialog dlg = new FontDialog())
            {
                dlg.ShowColor = true;
                dlg.ShowApply = true;
                dlg.ShowEffects = true;
                dlg.Apply += Dlg_Apply;
                listtempfontapply = list;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.BeginReFresh();
                    foreach (ICell cell in list)
                    {
                        cell.Font = dlg.Font;
                    }
                    this.EndReFresh();
                }
            }
        }

        private void Dlg_Apply(object sender, EventArgs e)
        {
            try
            {
                if (listtempfontapply != null)
                {
                    FontDialog dlg = sender as FontDialog;
                    if (dlg != null)
                    {
                        this.BeginReFresh();
                        foreach (ICell cell in listtempfontapply)
                        {
                            cell.Font = dlg.Font;
                        }
                        this.EndReFresh();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Dlg_Apply", ex);
            }
        }

        public virtual void SetSelectCellFont(bool isbold, bool bold
            , bool isitalic, bool italic
            , bool isunderline, bool underline
            , bool isstrikeout, bool strikeout
            , bool isfontsizeup, bool isup)
        {

            List<ICell> list = GetSelectCells();

            SetSelectCellFont(list, isbold, bold
            , isitalic, italic
            , isunderline, underline
            , isstrikeout, strikeout
            , isfontsizeup, isup);
        }
        public virtual void SetSelectCellFont(List<ICell> list, bool isbold, bool bold
            , bool isitalic, bool italic
            , bool isunderline, bool underline
            , bool isstrikeout, bool strikeout
            , bool isfontsizeup, bool isup)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                FontStyle fs = cell.Font.Style;
                if (isbold)
                {
                    if (bold)
                    {
                        fs = (cell.Font.Style | FontStyle.Bold);
                    }
                    else
                    {
                        fs = (cell.Font.Style ^ FontStyle.Bold);
                    }
                }
                if (isitalic)
                {
                    if (italic)
                    {
                        fs = (cell.Font.Style | FontStyle.Italic);
                    }
                    else
                    {
                        fs = (cell.Font.Style ^ FontStyle.Italic);
                    }
                }
                if (isunderline)
                {
                    if (underline)
                    {
                        fs = (cell.Font.Style | FontStyle.Underline);
                    }
                    else
                    {
                        fs = (cell.Font.Style ^ FontStyle.Underline);
                    }
                }
                if (isstrikeout)
                {
                    if (strikeout)
                    {
                        fs = (cell.Font.Style | FontStyle.Strikeout);
                    }
                    else
                    {
                        fs = (cell.Font.Style ^ FontStyle.Strikeout);
                    }
                }
                float emsize = cell.Font.Size;
                if (isfontsizeup)
                {
                    if (isup)
                    {
                        emsize = emsize + 1;
                    }
                    else
                    {
                        emsize = emsize - 1;
                        if (emsize < 1)
                        {
                            emsize = 1;
                        }
                    }
                }

                cell.Font = new Font(cell.Font.FontFamily, emsize, fs);
            }

            this.EndReFresh();
        }


        public virtual void SetSelectCellTextAlign(bool isHorizontal, StringAlignment horizontal
    , bool isVertical, StringAlignment vertical)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellTextAlign(list, isHorizontal, horizontal
            , isVertical, vertical);
        }
        public virtual void SetSelectCellTextAlign(List<ICell> list, bool isHorizontal, StringAlignment horizontal
, bool isVertical, StringAlignment vertical)
        {
            if (list.Count < 1)
            {
                return;
            }
            foreach (ICell cell in list)
            {
                FontStyle fs = cell.Font.Style;
                if (isHorizontal)
                {
                    cell.HorizontalAlignment = horizontal;
                }
                if (isVertical)
                {
                    cell.VerticalAlignment = vertical;
                }
            }
        }
        public virtual void SetSelectCellFontStyleBold(bool bold)
        {
            SetSelectCellFont(true, bold, false, false, false, false, false, false, false, false);
        }
        public virtual void SetSelectCellFontStyleItalic(bool italic)
        {
            SetSelectCellFont(false, false, true, italic, false, false, false, false, false, false);
        }
        public virtual void SetSelectCellFontStyleUnderline(bool underline)
        {

            SetSelectCellFont(false, false, false, false, true, underline, false, false, false, false);
        }
        public virtual void SetSelectCellFontStyleStrikeout(bool strikeout)
        {
            SetSelectCellFont(false, false, false, false, false, false, true, strikeout, false, false);
        }
        public virtual void SetSelectCellFontStyleFontSize(bool up)
        {
            SetSelectCellFont(false, false, false, false, false, false, false, false, true, up);
        }
        public virtual void SetSelectCellFontStyleBold(SelectCellCollection cells, bool bold)
        {
            SetSelectCellFont(cells.GetAllCells(), true, bold, false, false, false, false, false, false, false, false);
        }
        public virtual void SetSelectCellFontStyleItalic(SelectCellCollection cells, bool italic)
        {
            SetSelectCellFont(false, false, true, italic, false, false, false, false, false, false);
        }
        public virtual void SetSelectCellFontStyleUnderline(SelectCellCollection cells, bool underline)
        {

            SetSelectCellFont(false, false, false, false, true, underline, false, false, false, false);
        }
        public virtual void SetSelectCellFontStyleStrikeout(SelectCellCollection cells, bool strikeout)
        {
            SetSelectCellFont(false, false, false, false, false, false, true, strikeout, false, false);
        }
        public virtual void SetSelectCellFontStyleFontSize(SelectCellCollection cells, bool up)
        {
            SetSelectCellFont(false, false, false, false, false, false, false, false, true, up);
        }
        public virtual void SetSelectCellTextToUpper()
        {
            List<ICell> list = this.GetSelectCells();
            foreach (ICell item in list)
            {
                item.Caption = item.Caption.ToUpper();
                string text = Feng.Utils.ConvertHelper.ToString(item.Value).ToUpper();
                item.Value = text;
                item.Text = text;
            }
        }
        public virtual void SetSelectCellTextToUpper(SelectCellCollection cells)
        {
            List<ICell> list = cells.GetAllCells();
            foreach (ICell item in list)
            {
                item.Caption = item.Caption.ToUpper();
                string text = Feng.Utils.ConvertHelper.ToString(item.Value).ToUpper();
                item.Value = text;
                item.Text = text;
            }
        }
        public virtual void SetSelectCellTextToLower()
        {
            List<ICell> list = this.GetSelectCells();
            foreach (ICell item in list)
            {
                item.Caption = item.Caption.ToLower();
                string text = Feng.Utils.ConvertHelper.ToString(item.Value).ToLower();
                item.Value = text;
                item.Text = text;
            }
        }
        public virtual void SetSelectCellTextToLower(SelectCellCollection cells)
        {
            List<ICell> list = cells.GetAllCells();
            foreach (ICell item in list)
            {
                item.Caption = item.Caption.ToLower();
                string text = Feng.Utils.ConvertHelper.ToString(item.Value).ToLower ();
                item.Value = text;
                item.Text = text;
            }
        }
        public virtual void SetSelectCellColorForeColor(Color color)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.ForeColor = color;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellColorBackColor(Color color)
        {
            SetSelectCellColorBackColor(this.GetSelectCells(), color);
        }

        public virtual void SetSelectCellImageBackImage(Bitmap img)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.BackImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllMouseOverImage(Bitmap img)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.MouseOverImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllMouseDownImage(Bitmap img)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.MouseDownImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllReadOnlyImage(Bitmap img)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.ReadOnlyImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllDisableImage(Bitmap img)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.DisableImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllReadOnly(bool read)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)

            {
                cell.ReadOnly = read;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTextAutoMultLine(bool value)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)

            {
                cell.AutoMultiline = value;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTextAutoMultLine(SelectCellCollection cells, bool value)
        {
            List<ICell> list = cells.GetAllCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)

            {
                cell.AutoMultiline = value;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllShowSelectBorder(bool show)
        {
            List<ICell> list = GetSelectCells();
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.ShowFocusedSelectBorder = show;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFormat(string format, FormatType formattype)
        {
            List<ICell> list = GetSelectCells();
            SetSelectCellFormat(list, format, formattype);
        }
        public virtual void SetSelectCellFormat(List<ICell> list, string format, FormatType formattype)
        {
            if (list.Count < 1)
            {
                return;
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.FormatString = format;
                cell.FormatType = formattype;
                if (cell.Value == null)
                {
                    cell.Text = string.Empty;
                    continue;
                }
                if (formattype == FormatType.Numberic)
                {
                    decimal d = 0;
                    if (decimal.TryParse(cell.Value.ToString(), out d))
                    {
                        cell.Text = d.ToString(format);
                    }
                }
                else if (formattype == FormatType.DateTime)
                {
                    DateTime d = DateTime.MinValue;
                    if (DateTime.TryParse(cell.Value.ToString(), out d))
                    {
                        cell.Text = d.ToString(format);
                    }
                }
                else
                {
                    cell.Text = cell.Value.ToString();
                }

            }
            this.EndReFresh();
        }


        public virtual void SetSelectCellFormatNumber()
        {
            SetSelectCellFormat("#0.00", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberMoney()
        {
            SetSelectCellFormat("C", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberPercent()
        {
            SetSelectCellFormat("P", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberThousandths()
        {
            SetSelectCellFormat("N", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberDecimalPlaces1()
        {
            List<ICell> list = GetSelectCells();
            string format = "#0";
            if (list.Count > 0)
            {
                ICell cell = list[0];
                string format2 = cell.FormatString;
                string[] values = format2.Split('.');
                if (values.Length == 2)
                {
                    int zcount = values[1].Length;
                    zcount = zcount - 1;
                    if (zcount < 0)
                    {
                        zcount = 0;
                    }
                    format = "#0."+"".PadRight(zcount, '0');
                }
            }
            SetSelectCellFormat(format, FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberDecimalPlaces2()
        {
            List<ICell> list = GetSelectCells();
            string format = "#0.0";
            if (list.Count > 0)
            {
                ICell cell = list[0];
                string format2 = cell.FormatString;
                string[] values = format2.Split('.');
                if (values.Length == 2)
                {
                    int zcount = values[1].Length;
                    zcount = zcount + 1;
                    if (zcount < 0)
                    {
                        zcount = 0;
                    }
                    format = "#0." + "".PadRight(zcount, '0');
                }
            }
            SetSelectCellFormat(format, FormatType.Numberic);
        }


        public virtual void SetSelectCellFormatDateTimeDay()
        {
            SetSelectCellFormat("d", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimeg()
        {
            SetSelectCellFormat("g", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimeG()
        {
            SetSelectCellFormat("G", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimet()
        {
            SetSelectCellFormat("t", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatText()
        {
            SetSelectCellFormat(string.Empty, FormatType.Null);
        }
        public virtual void DeleteBackCell(IBackCell item)
        {
            this.BackCells.Remove(item);
        }


        public virtual void CopySelectCell(ISelectCellCollection fromcells, ICell tocell,
            bool isvalue,
            bool iswidth,
            bool isheight,
            bool isfont,
            bool isborder,
            bool ismerge,
            bool isbackcolor,
            bool isforecolor,
            bool isbackimage)
        {
            List<ICell> list = fromcells.GetAllCells();

        }
        #endregion

        #region SetSelect
        public virtual void SetSelectCellBoarderNull(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle.Visible = false;
                cell.BorderStyle.TopLineStyle.Visible = false;
                cell.BorderStyle.RightLineStyle.Visible = false;
                cell.BorderStyle.BottomLineStyle.Visible = false;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderLeftTopRightbottom(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.SetSelectCellBorderBorderOutside();
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftLineBorder(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTopLineBorder(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.TopLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllRightLineBorder(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.RightLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllBottomLineBorder(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.BottomLineStyle.Visible = true;

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellBorderLeftTopToRightBottom(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftBoomToRightTopLineBorder(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftBottomToRightTopLineStyle.Visible = true;

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellAllBorder(ISelectCellCollection select, LineStyle style)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle = style;
                cell.BorderStyle.TopLineStyle = style;
                cell.BorderStyle.RightLineStyle = style;
                cell.BorderStyle.BottomLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftLineBorder(ISelectCellCollection select, LineStyle style)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.BorderStyle.LeftLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTopLineBorder(ISelectCellCollection select, LineStyle style)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.BorderStyle.TopLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllRightLineBorder(ISelectCellCollection select, LineStyle style)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.BorderStyle.RightLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllBottomLineBorder(ISelectCellCollection select, LineStyle style)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.BorderStyle.BottomLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellLineBorder(ISelectCellCollection select, LineStyle style)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            int maxrow = select.MaxRow();
            int minrow = select.MinRow();
            int maxcolumn = select.MaxColumn();
            int mincolumn = select.MinColumn();

            for (int i = minrow; i <= maxrow; i++)
            {
                ICell cell = this[i, mincolumn];
                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle = style;
            }

            for (int i = minrow; i <= maxrow; i++)
            {
                ICell cell = this[i, maxcolumn];
                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.RightLineStyle = style;
            }

            for (int i = mincolumn; i <= maxcolumn; i++)
            {
                ICell cell = this[minrow, i];
                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.TopLineStyle = style;
            }

            for (int i = mincolumn; i <= maxcolumn; i++)
            {
                ICell cell = this[maxrow, i];
                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.BottomLineStyle = style;
            }


            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderLine(ISelectCellCollection select)
        {
            SetSelectCellLineBorder(select, this.ClassFactory.CreateLineStyle());
        }

        public virtual void SetSelectCellAlignLineTop(ISelectCellCollection select)
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(select, StringAlignment.Near);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignLineCenter(ISelectCellCollection select)
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(select, StringAlignment.Center);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignLineBottom(ISelectCellCollection select)
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(select, StringAlignment.Far);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLineAlign(ISelectCellCollection select, StringAlignment mode)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.VerticalAlignment = mode;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringLeft(ISelectCellCollection select)
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(select, StringAlignment.Near);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringCenter(ISelectCellCollection select)
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(select, StringAlignment.Center);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringRight(ISelectCellCollection select)
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(select, StringAlignment.Far);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringAlign(ISelectCellCollection select, StringAlignment mode)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.HorizontalAlignment = mode;
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellEditTextBoxCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = null;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCheckBoxCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellCheckBox();
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCellVector(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellVector(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditRadioBoxCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            string groupname = string.Empty;
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                if (groupname == string.Empty)
                {
                    groupname = cell.Caption;
                }
                CellRadioCheckBox edit = new CellRadioCheckBox(this);
                edit.ID = groupname;
                cell.OwnEditControl = edit;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditComboBoxCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellComboBox(this);
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellDropDownDataExcel(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownDataExcel(this);
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellDropDownFillter(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownFillter(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellDropDownDateTime(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownDateTime(this);
            }
            this.EndReFresh();
        }
        //public virtual void SetSelectCellDropDownDate(ISelectCellCollection select)
        //{
        //    if (select == null)
        //        return;
        //    this.BeginReFresh();
        //    List<ICell> list = select.GetAllCells();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellDropDownDate(this);
        //    }
        //    this.EndReFresh();
        //}
        public virtual void SetSelectCellSplitRow(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellSplitRow(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellSplitColumn(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellSplitColumn(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditComboBoxCell(ISelectCellCollection select, string[] text)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                CellComboBox ccb = new CellComboBox(this);
                cell.Caption = cell.Text;
                ccb.Items.AddRange(text);
                cell.OwnEditControl = ccb;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditComboBoxCell(ISelectCellCollection select,
            string[] text, ComboBoxStyle cbs)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                CellComboBox ccb = new CellComboBox(this);
                cell.Caption = cell.Text;
                ccb.DropDownStyle = cbs;
                ccb.Items.AddRange(text);
                cell.OwnEditControl = ccb;

            }
            this.EndReFresh();
        }
        //public virtual void SetSelectCellEditDateTimeCell(ISelectCellCollection select)
        //{
        //    if (select == null)
        //        return;
        //    this.BeginReFresh();
        //    List<ICell> list = select.GetAllCells();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellDateTime(this);

        //    }
        //    this.EndReFresh();
        //}
        public virtual void SetSelectCellSwitchCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellSwitch(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellGridView(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellGridView(this);

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellAllButton(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellButton(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditImageCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellImage(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCellTimer(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellTimer(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFolderBrowserEdit(List<ICell> list)
        {
            if (list.Count < 1)
                return;
            this.BeginReFresh();

            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellFolderBrowserEdit(this);
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellButton(List<ICell> list)
        {
            if (list.Count < 1)
                return;
            this.BeginReFresh();

            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellButton(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellCellMoveForm(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellMoveForm(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellGridView(List<ICell> list)
        {
            if (list.Count < 1)
                return;
            this.BeginReFresh();

            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellGridView(cell);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellTreeView(List<ICell> list)
        {
            if (list.Count < 1)
                return;
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellTreeView(cell);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditPasswordCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellPassword(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditNumberCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellNumber(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCnNumberCell(ISelectCellCollection select)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellCnNumber(this);

            }
            this.EndReFresh();
        }
        //public virtual void SetSelectCellAllColorTextCell(ISelectCellCollection select)
        //{
        //    if (select == null)
        //        return;
        //    this.BeginReFresh();
        //    List<ICell> list = select.GelAllCells();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellBaseColorText( );

        //    }
        //    this.EndReFresh();
        //}
        //public virtual void SetSelectCellAllHTreeCell(ISelectCellCollection select)
        //{
        //    if (select == null)
        //        return;
        //    this.BeginReFresh();
        //    List<ICell> list = select.GelAllCells();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellHTree( );

        //    }
        //    this.EndReFresh();
        //}

        public virtual void SetSelectCellEditControl(ISelectCellCollection select, ICellEditControl ctrl)
        {
            if (select == null)
            {
                if (this.FocusedCell != null)
                {
                    this.FocusedCell.OwnEditControl = ctrl;
                }
                return;
            }
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = ctrl;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellPassWordEditControl(ISelectCellCollection select)
        {
            if (select == null)
            {
                if (this.FocusedCell != null)
                {
                    this.FocusedCell.OwnEditControl = new CellPassword(this);
                }
                return;
            }
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellPassword(this);

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellAllSpText(ISelectCellCollection select)
        {

            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                CellSpText st = new CellSpText(this);
                cell.Caption = cell.Text;
                cell.OwnEditControl = st;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellDirectionVertical(ISelectCellCollection select, bool vertical)
        {

            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.DirectionVertical = vertical;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFont(ISelectCellCollection select, Font font)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.Font = font;
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellFontStyleBold(ISelectCellCollection select, bool bold)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (bold)
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style | FontStyle.Bold);
                }
                else
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style ^ FontStyle.Bold);
                }
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFontStyleItalic(ISelectCellCollection select, bool italic)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (italic)
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style | FontStyle.Italic);
                }
                else
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style ^ FontStyle.Italic);
                }
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFontStyleUnderline(ISelectCellCollection select, bool underline)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (underline)
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style | FontStyle.Underline);
                }
                else
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style ^ FontStyle.Underline);
                }
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFontStyleStrikeout(ISelectCellCollection select, bool strikeout)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                if (strikeout)
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style | FontStyle.Strikeout);
                }
                else
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style ^ FontStyle.Strikeout);
                }
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellColorForeColor(ISelectCellCollection select, Color color)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.ForeColor = color;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellColorBackColor(ISelectCellCollection select, Color color)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.BackColor = color;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellImageBackImage(ISelectCellCollection select, Bitmap img)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.BackImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllMouseOverImage(ISelectCellCollection select, Bitmap img)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.MouseOverImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllMouseDownImage(ISelectCellCollection select, Bitmap img)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.MouseDownImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllReadOnlyImage(ISelectCellCollection select, Bitmap img)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.ReadOnlyImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllDisableImage(ISelectCellCollection select, Bitmap img)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.DisableImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllReadOnly(ISelectCellCollection select, bool read)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.ReadOnly = read;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllShowSelectBorder(ISelectCellCollection select, bool show)
        {
            if (select == null)
                return;
            this.BeginReFresh();
            List<ICell> list = select.GetAllCells();
            foreach (ICell cell in list)
            {
                cell.ShowFocusedSelectBorder = show;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFormat(ISelectCellCollection select, string format, FormatType formattype)
        {
            if (select == null)
                return;
            List<ICell> list = select.GetAllCells();
            SetSelectCellFormat(list, format, formattype);
        }


        public virtual void SetSelectCellFormatNumber(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "#0.00", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberMoney(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "C", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberPercent(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "P", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberThousandths(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "N", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberDecimalPlaces1(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "#0.0", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberDecimalPlaces2(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "#0.00", FormatType.Numberic);
        }


        public virtual void SetSelectCellFormatDateTimeDay(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "d", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimeg(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "g", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimeG(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "G", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimet(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, "t", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatText(ISelectCellCollection select)
        {
            SetSelectCellFormat(select, string.Empty, FormatType.Null);
        }
        #endregion

        #region SetSelect
        public virtual void SetSelectCellBoarderNull(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle.Visible = false;
                cell.BorderStyle.TopLineStyle.Visible = false;
                cell.BorderStyle.RightLineStyle.Visible = false;
                cell.BorderStyle.BottomLineStyle.Visible = false;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderLeftTopRightbottom(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.SetSelectCellBorderBorderOutside();
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftLineBorder(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTopLineBorder(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.TopLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllRightLineBorder(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.RightLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllBottomLineBorder(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.BottomLineStyle.Visible = true;

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellBorderLeftTopToRightBottom(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = true;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftBoomToRightTopLineBorder(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftBottomToRightTopLineStyle.Visible = true;

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellAllBorder(List<ICell> list, LineStyle style)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle = style;
                cell.BorderStyle.TopLineStyle = style;
                cell.BorderStyle.RightLineStyle = style;
                cell.BorderStyle.BottomLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLeftLineBorder(List<ICell> list, LineStyle style)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.BorderStyle.LeftLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllTopLineBorder(List<ICell> list, LineStyle style)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.BorderStyle.TopLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllRightLineBorder(List<ICell> list, LineStyle style)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.BorderStyle.RightLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllBottomLineBorder(List<ICell> list, LineStyle style)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.BorderStyle.BottomLineStyle = style;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellLineBorder(List<ICell> list, LineStyle style)
        {
            this.BeginReFresh();
            foreach (ICell cel in list)
            {
                ICell cell = cel;
                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.LeftLineStyle = style;

                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.RightLineStyle = style;

                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.TopLineStyle = style;

                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                if (cell.BorderStyle == null)
                {
                    cell.BorderStyle = this.ClassFactory.CreateBorderStyle();
                }
                cell.BorderStyle.BottomLineStyle = style;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellBorderLine(List<ICell> list)
        {
            SetSelectCellLineBorder(list, this.ClassFactory.CreateLineStyle());
        }

        public virtual void SetSelectCellAlignLineTop(List<ICell> list)
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(list, StringAlignment.Near);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignLineCenter(List<ICell> list)
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(list, StringAlignment.Center);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignLineBottom(List<ICell> list)
        {
            this.BeginReFresh();
            SetSelectCellAllLineAlign(list, StringAlignment.Far);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllLineAlign(List<ICell> list, StringAlignment mode)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.VerticalAlignment = mode;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringLeft(List<ICell> list)
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(list, StringAlignment.Near);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringCenter(List<ICell> list)
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(list, StringAlignment.Center);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringRight(List<ICell> list)
        {
            this.BeginReFresh();
            SetSelectCellAlignStringAlign(list, StringAlignment.Far);
            this.EndReFresh();
        }
        public virtual void SetSelectCellAlignStringAlign(List<ICell> list, StringAlignment mode)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.HorizontalAlignment = mode;
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellEditTextBoxCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = null;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCheckBoxCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellCheckBox();
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellVector(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellVector(this);
            }
            this.EndReFresh();
        }
        public virtual void CommandSetValue(List<ICell> list, string arg)
        {
            this.BeginReFresh();
            if (string.IsNullOrWhiteSpace(arg))
            {
                foreach (ICell cell in list)
                {
                    cell.Value = RandomCache.Next(0, 100000).ToString();
                }
            }
            else
            {
                foreach (ICell cell in list)
                {
                    cell.Value = arg;
                    cell.Text = arg;
                }
            }
            this.EndReFresh();
        }
        public virtual void CommandSetExpress(List<ICell> list, string arg)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Expression = arg;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditRadioBoxCell(List<ICell> list)
        {
            this.BeginReFresh();
            string groupname = string.Empty;
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                if (groupname == string.Empty)
                {
                    groupname = cell.Caption;
                }
                CellRadioCheckBox edit = new CellRadioCheckBox(this);
                edit.ID = groupname;
                cell.OwnEditControl = edit;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditComboBoxCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellComboBox(this);
            }
            this.EndReFresh();
        }
 
        public virtual void SetSelectCellDropDownDataExcel(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownDataExcel(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellDropDownDateTime(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownDateTime(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellDropDownFillter(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellDropDownFillter(this);
            }
            this.EndReFresh();
        }
        //public virtual void SetSelectCellDropDownDate(List<ICell> list)
        //{
        //    this.BeginReFresh();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellDropDownDate(this);
        //    }
        //    this.EndReFresh();
        //}

        public virtual void SetSelectCellSplitRow(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellSplitRow(this);
            }
            this.EndReFresh();
        } 
        public virtual void SetSelectCellSplitColumn(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellSplitColumn(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditComboBoxCell(List<ICell> list, string[] text)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                CellComboBox ccb = new CellComboBox(this);
                cell.Caption = cell.Text;
                ccb.Items.AddRange(text);
                cell.OwnEditControl = ccb;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditComboBoxCell(List<ICell> list, string[] text, ComboBoxStyle cbs)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                CellComboBox ccb = new CellComboBox(this);
                cell.Caption = cell.Text;
                ccb.DropDownStyle = cbs;
                ccb.Items.AddRange(text);
                cell.OwnEditControl = ccb;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellSwitch(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                CellSwitch ccb = new CellSwitch(this);
                cell.Caption = cell.Text;
                cell.OwnEditControl = ccb;

            }
            this.EndReFresh();
        }

        //public virtual void SetSelectCellEditDateTimeCell(List<ICell> list)
        //{
        //    this.BeginReFresh();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellDateTime(this);

        //    }
        //    this.EndReFresh();
        //}
        public virtual void SetSelectCellAllButton(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellButton(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditImageCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellImage(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCellTimer(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellTimer(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellCellFolderBrowserEdit(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellFolderBrowserEdit(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellCellMoveForm(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellMoveForm(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditGridViewCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellGridView(cell);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditTreeViewCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellTreeView(cell);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditPasswordCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.OwnEditControl = new CellPassword(this);
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditNumberCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellNumber(this);

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellEditCnNumberCell(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellCnNumber(this);

            }
            this.EndReFresh();
        }
        //public virtual void SetSelectCellAllColorTextCell(List<ICell> list)
        //{
        //    if (select == null)
        //        return;
        //    this.BeginReFresh();
        //    List<ICell> list = select.GelAllCells();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellBaseColorText( );

        //    }
        //    this.EndReFresh();
        //}
        //public virtual void SetSelectCellAllHTreeCell(ISelectCellCollection select)
        //{
        //    if (select == null)
        //        return;
        //    this.BeginReFresh();
        //    List<ICell> list = select.GelAllCells();
        //    foreach (ICell cell in list)
        //    {
        //        cell.Caption = cell.Text;
        //        cell.OwnEditControl = new CellHTree( );

        //    }
        //    this.EndReFresh();
        //}

        public virtual void SetSelectCellEditControl(List<ICell> list, ICellEditControl ctrl)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = ctrl;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellPassWordEditControl(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Caption = cell.Text;
                cell.OwnEditControl = new CellPassword(this);

            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellAllSpText(List<ICell> list)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                CellSpText st = new CellSpText(this);
                cell.Caption = cell.Text;
                cell.OwnEditControl = st;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellTextOrientationRotateDown(List<ICell> list, bool vertical)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.DirectionVertical = vertical;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFont(List<ICell> list, Font font)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.Font = font;
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellFontStyleBold(List<ICell> list, bool bold)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (bold)
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style | FontStyle.Bold);
                }
                else
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style ^ FontStyle.Bold);
                }
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFontStyleItalic(List<ICell> list, bool italic)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (italic)
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style | FontStyle.Italic);
                }
                else
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style ^ FontStyle.Italic);
                }
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFontStyleUnderline(List<ICell> list, bool underline)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (underline)
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style | FontStyle.Underline);
                }
                else
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style ^ FontStyle.Underline);
                }
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellFontStyleStrikeout(List<ICell> list, bool strikeout)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                if (strikeout)
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style | FontStyle.Strikeout);
                }
                else
                {
                    cell.Font = new Font(cell.Font, cell.Font.Style ^ FontStyle.Strikeout);
                }
            }
            this.EndReFresh();
        }

        public virtual void SetSelectCellColorForeColor(List<ICell> list, Color color)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.ForeColor = color;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellColorBackColor(List<ICell> list, Color color)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.BackColor = color;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellImageBackImage(List<ICell> list, Bitmap img)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.BackImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllMouseOverImage(List<ICell> list, Bitmap img)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.MouseOverImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllMouseDownImage(List<ICell> list, Bitmap img)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.MouseDownImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllReadOnlyImage(List<ICell> list, Bitmap img)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.ReadOnlyImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllDisableImage(List<ICell> list, Bitmap img)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.DisableImage = img;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllReadOnly(List<ICell> list, bool read)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.ReadOnly = read;

            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellAllShowSelectBorder(List<ICell> list, bool show)
        {
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                cell.ShowFocusedSelectBorder = show;
            }
            this.EndReFresh();
        }



        public virtual void SetSelectCellFormatNumber(List<ICell> list)
        {
            SetSelectCellFormat(list, "#0.00", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberMoney(List<ICell> list)
        {
            SetSelectCellFormat(list, "C", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberPercent(List<ICell> list)
        {
            SetSelectCellFormat(list, "P", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberTs(List<ICell> list)
        {
            SetSelectCellFormat(list, "N", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberDecimalPlaces1(List<ICell> list)
        {
            SetSelectCellFormat(list, "#0.0", FormatType.Numberic);
        }
        public virtual void SetSelectCellFormatNumberDecimalPlaces2(List<ICell> list)
        {
            SetSelectCellFormat(list, "#0.00", FormatType.Numberic);
        }


        public virtual void SetSelectCellFormatDateTimeD(List<ICell> list)
        {
            SetSelectCellFormat(list, "d", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimeg(List<ICell> list)
        {
            SetSelectCellFormat(list, "g", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimeG(List<ICell> list)
        {
            SetSelectCellFormat(list, "G", FormatType.DateTime);
        }
        public virtual void SetSelectCellFormatDateTimet(List<ICell> list)
        {
            SetSelectCellFormat(list, "t", FormatType.DateTime);
        }

        #endregion

        public virtual void SetFoucsedCellSelect(ICell cell)
        {
            if (cell == this.FocusedCell)
                return;
            if (cell.OwnMergeCell != null)
            {
                cell = cell.OwnMergeCell;
            }
            SetFocuseddCell(cell);
            if (cell != null)
            {
                _SelectCells = new SelectCellCollection();
                this._SelectCells.BeginCell = cell;
                this._SelectCells.EndCell = cell;
            }

        }

        public virtual void ShowAndFocusedCell(ICell cell)
        {
            if (cell == this.FocusedCell)
                return;
            ICell precell = this.FocusedCell;
            SetFoucsedCellSelect(cell);
            ShowCell(precell, this.FocusedCell);
            RefreshRowHeaderWidth();
        }

        #region BookMark

        public void AddBookMark(ICell cell)
        {
            this.BookMarkList.Add(cell);
        }

        public void AddBookMark()
        {
            if (this.FocusedCell != null)
            {
                this.BookMarkList.Add(this.FocusedCell);
            }
        }
        public void RemoveBookMark()
        {
            if (this.FocusedCell != null)
            {
                this.BookMarkList.Remove(this.FocusedCell);
            }
        }
        public virtual void TurnFocusdToNextBookMark()
        {
            ICell cell = this.BookMarkList.Next;
            if (cell != null)
            {
                ShowAndFocusedCell(cell);
            }
        }
        public virtual void TurnFocusdToPrevBookMark()
        {
            ICell cell = this.BookMarkList.Prev;
            if (cell != null)
            {
                ShowAndFocusedCell(cell);
            }
        }
        public virtual void TurnFocusdToFirstBookMark()
        {
            ICell cell = this.BookMarkList.Header;
            if (cell != null)
            {
                ShowAndFocusedCell(cell);
            }
        }
        public virtual void TurnFocusdToFooterBookMark()
        {
            ICell cell = this.BookMarkList.Footer;
            if (cell != null)
            {
                ShowAndFocusedCell(cell);
            }
        }

        #endregion


        public virtual void SetSelectCellVisable(List<ICell> list)
        {
            if (list.Count < 1)
                return;
            this.BeginReFresh();

            foreach (ICell cell in list)
            {
                cell.Visible = true;
            }
            this.EndReFresh();
        }
        public virtual void SetSelectCellHide(List<ICell> list)
        {
            if (list.Count < 1)
                return;
            this.BeginReFresh();

            foreach (ICell cell in list)
            {
                cell.Visible = false;
            }
            this.EndReFresh();
        }
        #region FocsedCellMark

        public void AddFocsedCellMark(ICell cell)
        {
            this.FocsedCellList.Add(cell);
        }

        public void AddFocsedCellMark()
        {
            if (this.FocusedCell != null)
            {
                this.FocsedCellList.Add(this.FocusedCell);
            }
        }
        public void RemoveFocsedCellMark()
        {
            if (this.FocusedCell != null)
            {
                this.FocsedCellList.Remove(this.FocusedCell);
            }
        }
        public virtual void TurnFocusdToNextFocsedCellMark()
        {
            ICell cell = this.FocsedCellList.Next;
            if (cell != null)
            {
                ShowAndFocusedCell(cell);
            }
        }
        public virtual void TurnFocusdToPrevFocsedCellMark()
        {
            ICell cell = this.FocsedCellList.Prev;
            if (cell != null)
            {
                ShowAndFocusedCell(cell);
            }
        }
        public virtual void TurnFocusdToFirstFocsedCellMark()
        {
            ICell cell = this.FocsedCellList.Header;
            if (cell != null)
            {
                ShowAndFocusedCell(cell);
            }
        }
        public virtual void TurnFocusdToFooterFocsedCellMark()
        {
            ICell cell = this.FocsedCellList.Footer;
            if (cell != null)
            {
                ShowAndFocusedCell(cell);
            }
        }

        #endregion
    }
}
