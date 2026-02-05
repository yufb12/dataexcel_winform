using Feng.Excel.Collections;
using Feng.Excel.Interfaces;
using Feng.Excel.Script;
using Feng.Forms;
using System;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {

        public virtual void UpdataSelectAdd()
        {

            SelectAddRectCollection selectionadd = this._SelectAddRectCollection;

            if (selectionadd == null)
                return;
            if (selectionadd.SelectCellCollection == null)
                return;
            if (selectionadd.EndCell == null)
                return;
            ICell target = selectionadd.EndCell;
            int selminrowindex = selectionadd.SelectCellCollection.MinRow();
            int selmincolumnindex = selectionadd.SelectCellCollection.MinColumn();

            int selmaxrowindex = selectionadd.SelectCellCollection.MaxRow();
            int selmaxcolumnindex = selectionadd.SelectCellCollection.MaxColumn();

            int targetrowindex = selectionadd.EndCell.Row.Index;
            int targetcolumnindex = selectionadd.EndCell.Column.Index;
            int rowcount = targetrowindex - selmaxrowindex;
            int columncount = targetcolumnindex - selmaxcolumnindex;

            if (rowcount >= 0)
            {
                if (columncount >= 0)
                {
                    if (rowcount >= columncount)
                    {
                        UpdataSelectRowsDown2(selectionadd, target);
                    }
                    else
                    {
                        UpdataSelectColumnsRight2(selectionadd, target);
                    }
                }
                else
                {
                    columncount = selmincolumnindex - targetcolumnindex;
                    if (rowcount >= columncount)
                    {
                        UpdataSelectRowsDown2(selectionadd, target);
                    }
                    else
                    {
                        UpdataSelectColumnsLeft2(selectionadd, target);
                    }
                }
            }
            else
            {
                rowcount = selminrowindex - targetrowindex;
                if (columncount >= 0)
                {
                    if (rowcount >= columncount)
                    {
                        UpdataSelectRowsUp2(selectionadd, target);
                    }
                    else
                    {
                        UpdataSelectColumnsRight2(selectionadd, target);
                    }
                }
                else
                {
                    columncount = selmincolumnindex - targetcolumnindex;
                    if (rowcount >= columncount)
                    {
                        UpdataSelectRowsUp2(selectionadd, target);
                    }
                    else
                    {
                        UpdataSelectColumnsLeft2(selectionadd, target);
                    }
                }
            }


            rowcount = targetrowindex - selminrowindex;
            columncount = targetcolumnindex - selmincolumnindex;

            this.EndEdit();
        }
        public void UpdataSelectRowsUp2(SelectAddRectCollection selectionadd, ICell target)
        {
            int selminrowindex = selectionadd.SelectCellCollection.MinRow();
            int selmincolumnindex = selectionadd.SelectCellCollection.MinColumn();

            int selmaxrowindex = selectionadd.SelectCellCollection.MaxRow();
            int selmaxcolumnindex = selectionadd.SelectCellCollection.MaxColumn();

            int targetrowindex = selectionadd.EndCell.Row.Index;
            int targetcolumnindex = selectionadd.EndCell.Column.Index;
            decimal ADDVALUE = 1;
            for (int i = selmincolumnindex; i <= selmaxcolumnindex; i++)
            {
                ICell cellend = this[selminrowindex, i];
                ICell cellbegin = this[selmaxrowindex, i];
                ICell target2 = this[targetrowindex, i];
                UpdataSelectRowsUp2(cellbegin, cellend, target2, ADDVALUE);
            }
        }
        public void UpdataSelectRowsDown2(SelectAddRectCollection selectionadd, ICell target)
        {
            int selminrowindex = selectionadd.SelectCellCollection.MinRow();
            int selmincolumnindex = selectionadd.SelectCellCollection.MinColumn();

            int selmaxrowindex = selectionadd.SelectCellCollection.MaxRow();
            int selmaxcolumnindex = selectionadd.SelectCellCollection.MaxColumn();

            int targetrowindex = selectionadd.EndCell.Row.Index;
            int targetcolumnindex = selectionadd.EndCell.Column.Index;
            decimal ADDVALUE = 1;
            for (int i = selmincolumnindex; i <= selmaxcolumnindex; i++)
            {
                ICell cellbegin = this[selminrowindex, i];
                ICell cellend = this[selmaxrowindex, i];
                ICell target2 = this[targetrowindex, i];
                if (selmaxrowindex > selminrowindex)
                {
                    ICell cellbeginnext = this[selminrowindex + 1, i];
                    decimal dv = decimal.Zero;
                    if (decimal.TryParse(cellbegin.Text, out dv))
                    {
                        decimal cellbeginvalue = dv;
                        if (decimal.TryParse(cellbeginnext.Text, out dv))
                        {
                            decimal cellbeginnextvalue = dv;
                            ADDVALUE = cellbeginnextvalue - cellbeginvalue;
                        }
                    }
                }

                UpdataSelectRowsDown2(cellbegin, cellend, target2, ADDVALUE);
            }
        }
        public void UpdataSelectColumnsRight2(SelectAddRectCollection selectionadd, ICell target)
        {
            int selminrowindex = selectionadd.SelectCellCollection.MinRow();
            int selmincolumnindex = selectionadd.SelectCellCollection.MinColumn();

            int selmaxrowindex = selectionadd.SelectCellCollection.MaxRow();
            int selmaxcolumnindex = selectionadd.SelectCellCollection.MaxColumn();

            int targetrowindex = selectionadd.EndCell.Row.Index;
            int targetcolumnindex = selectionadd.EndCell.Column.Index;
            decimal ADDVALUE = 1;
            for (int i = selminrowindex; i <= selmaxrowindex; i++)
            {
                ICell cellbegin = this[i, selmincolumnindex];
                ICell cellend = this[i, selmaxcolumnindex];
                ICell target2 = this[i, targetcolumnindex];
                if (selmaxcolumnindex > selmincolumnindex)
                {
                    ICell cellbeginnext = this[i,selmincolumnindex + 1];
                    decimal dv = decimal.Zero;
                    if (decimal.TryParse(cellbegin.Text, out dv))
                    {
                        decimal cellbeginvalue = dv;
                        if (decimal.TryParse(cellbeginnext.Text, out dv))
                        {
                            decimal cellbeginnextvalue = dv;
                            ADDVALUE = cellbeginnextvalue - cellbeginvalue;
                        }
                    }
                }
                UpdataSelectColumnsDown2(cellbegin, cellend, target2, ADDVALUE);
            }

        }
        public void UpdataSelectColumnsLeft2(SelectAddRectCollection selectionadd, ICell target)
        {

            int selminrowindex = selectionadd.SelectCellCollection.MinRow();
            int selmincolumnindex = selectionadd.SelectCellCollection.MinColumn();

            int selmaxrowindex = selectionadd.SelectCellCollection.MaxRow();
            int selmaxcolumnindex = selectionadd.SelectCellCollection.MaxColumn();

            int targetrowindex = selectionadd.EndCell.Row.Index;
            int targetcolumnindex = selectionadd.EndCell.Column.Index;
            decimal ADDVALUE = 1;
            for (int i = selminrowindex; i <= selmaxrowindex; i++)
            {
                ICell cellend = this[i, selmincolumnindex];
                ICell cellbegin = this[i, selmaxcolumnindex];
                ICell target2 = this[i, targetcolumnindex];
                UpdataSelectColumnsLeft2(cellbegin, cellend, target2, ADDVALUE);
            }
        }

         
        public static int DateType = 4;
        public static int DateLen = 1;
        private void SetSelectDateTimeAddType(ref int datetype, ref int datelen)
        {
            using (SelectInputDateTimeDialog frm = new SelectInputDateTimeDialog())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
        private void SetSelectDecimalAddType(ref int datetype, ref int datelen)
        {
            using (SelectInputDateTimeDialog frm = new SelectInputDateTimeDialog())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
        private DateTime GetSelectDataTimeAdd(DateTime dt, int DateType, int i, int DateLen)
        {
            int len = i * DateLen;
            switch (DateType)
            {
                case 1:
                    dt = dt.AddSeconds(len);
                    break;
                case 2:
                    dt = dt.AddMinutes(len);
                    break;
                case 3:
                    dt = dt.AddHours(len);
                    break;
                case 4:
                    dt = dt.AddDays(len);
                    break;
                case 5:
                    dt = dt.AddDays(len * 7);
                    break;
                case 6:
                    dt = dt.AddMonths(len);
                    break;
                case 7:
                    dt = dt.AddYears(len);
                    break;
                default:
                    dt = dt.AddDays(i * 1);
                    break;
            }
            return dt;
        }

        #region down
        public void UpdataSelectRowsDown2(ICell cellbegin, ICell cellend, ICell target, decimal ADDVALUE)
        {
            if (System.Windows.Forms.Control.ModifierKeys != Keys.Control)
            {
                UpdataSelectRows2DownAdd(cellbegin, cellend, target, false, ADDVALUE);
            }
            else
            {
                UpdataSelectRows2DownAdd(cellbegin, cellend, target, true, ADDVALUE);
            }
        }
        public void UpdataSelectRows2DownAdd(ICell cellbegin, ICell cellend, ICell target, bool copy,decimal ADDVALUE)
        {
            int minrow = cellbegin.Row.Index;
            int maxrow = cellend.MaxRowIndex;
            int targetrow = target.Row.Index;
            int tcolumn = cellbegin.Column.Index;
            int targetcount = targetrow - maxrow;
            int rowcounts = maxrow - minrow + 1;
            if (rowcounts < 1)
                return;
            int times = targetcount / rowcounts;
            bool hasSet = false;
            for (int timei = 0; timei < times; timei++)
            {
                for (int rowj = 0; rowj < rowcounts; rowj++)
                {
                    int trow = maxrow + timei * rowcounts + rowj + 1;
                    int srow = minrow + rowj;
                    if (tcolumn < 1 || trow < 1)
                    {
                        return;
                    }
                    ICell tcell = this[trow, tcolumn];
                    ICell scell = this[srow, tcolumn];
                    PasteCell(scell, tcell);
                    if (!copy)
                    {
                        if (!string.IsNullOrWhiteSpace(scell.Expression))
                        {
                            tcell.Expression = Function.GetNextRowExpress(scell.Expression, timei + 1);
                            continue;
                        }
                        DateTime? dt = Feng.Utils.ConvertHelper.ToDateTimeNullable(scell.Value);
                        if (dt.HasValue)
                        {
                            if (hasSet)
                            {
                                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                                {
                                    SetSelectDateTimeAddType(ref DateType, ref DateLen);
                                }
                            }
                            DateTime dttime = dt.Value;
                            tcell.Value = GetSelectDataTimeAdd(dttime, DateType, (timei + 1), DateLen);
                            continue;
                        }

                        decimal? dvalue = Feng.Utils.ConvertHelper.ToDecimalNullable(scell.Value);
                        if (dvalue.HasValue)
                        {
                            if (hasSet)
                            {
                                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                                {
                                    SetSelectDecimalAddType(ref DateType, ref DateLen);
                                }
                            }
                            decimal value = dvalue.Value + (((timei+1) * rowcounts)) * ADDVALUE;
                            tcell.Value = value;
                            continue;
                        }
                        else
                        {
                            if (scell.Text.Length < 20)
                            {
                                System.Text.RegularExpressions.MatchCollection  matches = System.Text.RegularExpressions.Regex.Matches(scell.Text, "[0-9]+");
                                if (matches.Count == 1)
                                {
                                    System.Text.RegularExpressions.Match match = matches[0];
                                    dvalue = Feng.Utils.ConvertHelper.ToDecimalNullable(match.Value);
                                    decimal value = dvalue.Value + (((timei + 1) * rowcounts)) * ADDVALUE;
                                    tcell.Value = scell.Text.Substring(0,match.Index)+ value+ scell.Text.Substring(match.Index+  match.Length);
                                    continue;
                                }
                            }
                        }
                        tcell.Value = scell.Value;
                    }

                }
            }
        }

        #endregion

        #region up

        public void UpdataSelectRowsUp2(ICell cellbegin, ICell cellend, ICell target, decimal ADDVALUE)
        {
            if (System.Windows.Forms.Control.ModifierKeys != Keys.Control)
            {
                UpdataSelectRows2UpAdd(cellbegin, cellend, target, ADDVALUE);
            }
            else
            {
                UpdataSelectRows2UpCopy(cellbegin, cellend, target);
            }
        }
        public void UpdataSelectRows2UpAdd(ICell cellbegin, ICell cellend, ICell target, decimal ADDVALUE)
        {
            int minrow = cellend.Row.Index;
            int maxrow = cellbegin.MaxRowIndex;
            int targetrow = target.Row.Index;
            int tcolumn = cellbegin.Column.Index;
            int targetcount = maxrow - targetrow;
            int scount = maxrow - minrow + 1;
            if (scount < 1)
                return;
            int count = targetcount / scount;
            bool hasSet = false;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < scount; j++)
                {
                    int trow = maxrow - i * scount - j - 1;
                    int srow = minrow - j;
                    if (tcolumn < 1 || trow < 1)
                    {
                        return;
                    }
                    ICell tcell = this[trow, tcolumn];
                    ICell scell = this[srow, tcolumn];
                    PasteCell(scell, tcell);
                    if (!string.IsNullOrWhiteSpace(scell.Expression))
                    {
                        tcell.Expression = Function.GetNextRowExpress(scell.Expression, -1 * (i + 1));
                        continue;
                    }
                    DateTime? dt = Feng.Utils.ConvertHelper.ToDateTimeNullable(scell.Value);
                    if (dt.HasValue)
                    {
                        if (hasSet)
                        {
                            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                            {
                                SetSelectDateTimeAddType(ref DateType, ref DateLen);
                            }
                        }
                        DateTime dttime = dt.Value;
                        tcell.Value = GetSelectDataTimeAdd(dttime, DateType, -1 * (i + 1), DateLen);
                        continue;
                    }

                    decimal? dvalue = Feng.Utils.ConvertHelper.ToDecimalNullable(scell.Value);
                    if (dvalue.HasValue)
                    {
                        if (hasSet)
                        {
                            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                            {
                                SetSelectDecimalAddType(ref DateType, ref DateLen);
                            }
                        }
                        decimal value = dvalue.Value + -1 * (i + 1) * ADDVALUE;
                        tcell.Value = value;
                        continue;
                    }
                    tcell.Value = scell.Value;

                }
            }
        }
        public void UpdataSelectRows2UpCopy(ICell cellbegin, ICell cellend, ICell target)
        {
            int minrow = cellend.MaxRowIndex;
            int maxrow = cellbegin.Row.Index;
            int targetrow = target.Row.Index;
            int tcolumn = cellbegin.Column.Index;
            int targetcount = maxrow - targetrow;
            int scount = maxrow - minrow + 1;
            if (scount < 1)
                return;
            int count = targetcount / scount;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < scount; j++)
                {
                    int trow = minrow - i * scount - j - 1;
                    int srow = maxrow - j;
                    if (tcolumn < 1 || trow < 1)
                    {
                        return;
                    }
                    ICell tcell = this[trow, tcolumn];
                    ICell scell = this[srow, tcolumn];
                    if (tcolumn < 1 || trow < 1)
                    {
                        return;
                    }
                    PasteCell(scell, tcell);
                }
            }
        }
        #endregion

        #region Right
        public void UpdataSelectColumnsDown2(ICell cellbegin, ICell cellend, ICell target, decimal ADDVALUE)
        {
            if (System.Windows.Forms.Control.ModifierKeys != Keys.Control)
            {
                UpdataSelectColumns2DownAdd(cellbegin, cellend, target, ADDVALUE);
            }
            else
            {
                UpdataSelectColumns2DownCopy(cellbegin, cellend, target);
            }
        }
        public void UpdataSelectColumns2DownAdd(ICell cellbegin, ICell cellend, ICell target, decimal ADDVALUE)
        {
            int minrow = cellbegin.Column.Index;
            int maxrow = cellend.MaxColumnIndex;
            int targetrow = target.Column.Index;
            int trow = cellbegin.Row.Index;
            int targetcount = targetrow - maxrow;
            int scount = maxrow - minrow + 1;
            if (scount < 1)
                return;
            int count = targetcount / scount;
            bool hasSet = false;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < scount; j++)
                {
                    int tcolumn = maxrow + i * scount + j + 1;
                    int srow = minrow + j;
                    if (tcolumn < 1 || trow < 1)
                    {
                        return;
                    }
                    ICell tcell = this[trow, tcolumn];
                    ICell scell = this[trow, srow];
                    PasteCell(scell, tcell);
                    if (!string.IsNullOrWhiteSpace(scell.Expression))
                    {
                        tcell.Expression = Function.GetNextColumnExpress(scell.Expression, i + 1);
                        continue;
                    }
                    DateTime? dt = Feng.Utils.ConvertHelper.ToDateTimeNullable(scell.Value);
                    if (dt.HasValue)
                    {
                        if (hasSet)
                        {
                            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                            {
                                SetSelectDateTimeAddType(ref DateType, ref DateLen);
                            }
                        }
                        DateTime dttime = dt.Value;
                        tcell.Value = GetSelectDataTimeAdd(dttime, DateType, (i + 1), DateLen);
                        continue;
                    }

                    decimal? dvalue = Feng.Utils.ConvertHelper.ToDecimalNullable(scell.Value);
                    if (dvalue.HasValue)
                    {
                        if (hasSet)
                        {
                            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                            {
                                SetSelectDecimalAddType(ref DateType, ref DateLen);
                            }
                        }
                        //        decimal value = dvalue.Value + (((timei+1) * rowcounts)) * ADDVALUE;
                        decimal value = dvalue.Value + (i + 1)* scount * ADDVALUE;
                        tcell.Value = value;
                        continue;
                    }
                    tcell.Value = scell.Value;

                }
            }
        }
        public void UpdataSelectColumns2DownCopy(ICell cellbegin, ICell cellend, ICell target)
        {
            int mincolumn = cellbegin.Column.Index;
            int maxcolumn = cellend.MaxColumnIndex;
            int targetcolumn = target.Column.Index;
            int trow = cellbegin.Row.Index;
            int targetcount = targetcolumn - maxcolumn;
            int scount = maxcolumn - mincolumn + 1;
            if (scount < 1)
                return;
            int count = targetcount / scount;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < scount; j++)
                {
                    int tcolumn = maxcolumn + i * scount + j + 1;
                    int srow = mincolumn + j;
                    if (tcolumn < 1 || trow < 1)
                    {
                        return;
                    }
                    ICell tcell = this[trow, tcolumn];
                    ICell scell = this[trow, srow];

                    PasteCell(scell, tcell);
                }
            }
        }
        #endregion

        #region Left

        public void UpdataSelectColumnsLeft2(ICell cellbegin, ICell cellend, ICell target, decimal ADDVALUE)
        {
            if (System.Windows.Forms.Control.ModifierKeys != Keys.Control)
            {
                UpdataSelectColumns2UpAdd(cellbegin, cellend, target, ADDVALUE);
            }
            else
            {
                UpdataSelectColumns2UpCopy(cellbegin, cellend, target);
            }
        }
        public void UpdataSelectColumns2UpAdd(ICell cellbegin, ICell cellend, ICell target, decimal ADDVALUE)
        {
            int minrow = cellend.Column.Index;
            int maxrow = cellbegin.MaxColumnIndex;
            int targetrow = target.Column.Index;
            int trow = cellbegin.Row.Index;
            int targetcount = maxrow - targetrow;
            int scount = maxrow - minrow + 1;
            if (scount < 1)
                return;
            int count = targetcount / scount;
            bool hasSet = false;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < scount; j++)
                {
                    int tcolumn = maxrow - i * scount - j - 1;
                    int srow = minrow - j;
                    if (tcolumn < 1 || trow < 1)
                    {
                        return;
                    }
                    ICell tcell = this[trow, tcolumn];
                    ICell scell = this[trow, srow];
                    PasteCell(scell, tcell);
                    if (!string.IsNullOrWhiteSpace(scell.Expression))
                    {
                        tcell.Expression = Function.GetNextColumnExpress(scell.Expression, -1 * (i + 1));
                        continue;
                    }
                    DateTime? dt = Feng.Utils.ConvertHelper.ToDateTimeNullable(scell.Value);
                    if (dt.HasValue)
                    {
                        if (hasSet)
                        {
                            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                            {
                                SetSelectDateTimeAddType(ref DateType, ref DateLen);
                            }
                        }
                        DateTime dttime = dt.Value;
                        tcell.Value = GetSelectDataTimeAdd(dttime, DateType, -1 * (i + 1), DateLen);
                        continue;
                    }

                    decimal? dvalue = Feng.Utils.ConvertHelper.ToDecimalNullable(scell.Value);
                    if (dvalue.HasValue)
                    {
                        if (hasSet)
                        {
                            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                            {
                                SetSelectDecimalAddType(ref DateType, ref DateLen);
                            }
                        }
                        decimal value = dvalue.Value + -1 * (i + 1) * ADDVALUE;
                        tcell.Value = value;
                        continue;
                    }
                    tcell.Value = scell.Value;

                }
            }
        }
        public void UpdataSelectColumns2UpCopy(ICell cellbegin, ICell cellend, ICell target)
        {
            int minrow = cellend.MaxColumnIndex;
            int maxrow = cellbegin.Column.Index;
            int targetrow = target.Column.Index;
            int trow = cellbegin.Row.Index;
            int targetcount = maxrow - targetrow;
            int scount = maxrow - minrow + 1;
            if (scount < 1)
                return;
            int count = targetcount / scount;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < scount; j++)
                {
                    int tcolumn = minrow - i * scount - j - 1;
                    int srow = maxrow - j;
                    if (tcolumn < 1 || trow < 1)
                    {
                        return;
                    }
                    ICell tcell = this[trow, tcolumn];
                    ICell scell = this[trow, srow];
                    PasteCell(scell, tcell);
                }
            }
        }
        #endregion

        private void UpdateSelectAddRect()
        {
            UpdataSelectAdd();
        }
        private decimal _AddSelect = 1;

        public virtual void CopyMegerCell(IMergeCell scell, ICell tcell)
        {
            int countrow = scell.MaxRowIndex - scell.Row.Index;
            int countcolumn = scell.MaxColumnIndex - scell.Column.Index;
            this.MergeCell(tcell, this[tcell.MaxRowIndex + countrow, tcell.MaxColumnIndex + countcolumn]);
        }

#if DEBUG
        public void test()
        {
            this.GetCellByID("");

        }
#endif
    }
}
