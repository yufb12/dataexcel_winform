using Feng.Data;
using Feng.Excel.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Fillter
{
    public class Sorter
    {
        public DataExcel Grid { get; set; }
        public int BeginRowIndex { get; set; }
        public int EndRowIndex { get; set; }
        public byte Type { get; set; }
        public Sorter()
        { 
        }

        public IColumn SortColumn = null;
        public void SortAsc(IColumn column)
        {
            SortColumn = column;
            List<IRow> listrows = new List<IRow>();
            for (int i = BeginRowIndex; i <= EndRowIndex; i++)
            {
                IRow row = this.Grid.GetRow(i);
                listrows.Add(row);
            }
            switch (Type)
            {
                case TypeEnum.TDateTime:
                    listrows.Sort(CompareTime);
                    break;
                case TypeEnum.Tdecimal:
                    listrows.Sort(CompareNum);
                    break;
                default:
                    listrows.Sort(CompareText);
                    break;
            }
            int index = 0;
            for (int i = BeginRowIndex; i <= EndRowIndex; i++)
            {
                IRow row = listrows[index];
                row.Index = i;
                index++;
            }
            this.Grid.Rows.Refresh();
        }
        public void SortDesc(IColumn column)
        {
            SortColumn = column;
            List<IRow> listrows = new List<IRow>();
            for (int i = BeginRowIndex; i <= EndRowIndex; i++)
            {
                IRow row = this.Grid.GetRow(i);
                listrows.Add(row);
            }
            switch (Type)
            {
                case TypeEnum.TDateTime:
                    listrows.Sort(CompareTime);
                    break;
                case TypeEnum.Tdecimal:
                    listrows.Sort(CompareNum);
                    break;
                default:
                    listrows.Sort(CompareText);
                    break;
            }
            int index = listrows.Count -1;
            for (int i = BeginRowIndex; i <= EndRowIndex; i++)
            {
                IRow row = listrows[index];
                row.Index = i;
                index--;
            }
            this.Grid.Rows.Refresh();
        }
        public int CompareText(IRow rowa, IRow rowb)
        {
            ICell cella = rowa[SortColumn];
            string valuea = string.Empty;
            if (cella != null)
            {
                valuea = Feng.Utils.ConvertHelper.ToString(cella.Value);
            }
            ICell cellb = rowb[SortColumn];
            string valueb = string.Empty;
            if (cellb != null)
            {
                valueb = Feng.Utils.ConvertHelper.ToString(cellb.Value);
            }
            return string.Compare(valuea, valueb);
        }
        public int CompareNum(IRow rowa, IRow rowb)
        {
            ICell cella = rowa[SortColumn];
            decimal valuea = decimal.Zero;
            if (cella != null)
            {
                valuea = Feng.Utils.ConvertHelper.ToDecimal(cella.Value);
            }
            ICell cellb = rowb[SortColumn];
            decimal valueb = decimal.Zero;
            if (cellb != null)
            {
                valueb = Feng.Utils.ConvertHelper.ToDecimal(cellb.Value);
            }
            return decimal.Compare(valuea, valueb);
        }
        public int CompareTime(IRow rowa, IRow rowb)
        {
            ICell cella = rowa[SortColumn];
            DateTime valuea = DateTime.MinValue;
            if (cella != null)
            {
                valuea = Feng.Utils.ConvertHelper.ToDateTime(cella.Value);
            }
            ICell cellb = rowb[SortColumn];
            DateTime valueb = DateTime.MinValue;
            if (cellb != null)
            {
                valueb = Feng.Utils.ConvertHelper.ToDateTime(cellb.Value);
            }
            return DateTime.Compare(valuea, valueb);
        }
    }

}
