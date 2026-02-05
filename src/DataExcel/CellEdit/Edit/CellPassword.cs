using Feng.Data;
using Feng.Drawing;
using Feng.Excel.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellPassword : CellEdit
    {
        public CellPassword(DataExcel grid)
            : base(grid)
        {
        }

        public override bool DrawCell(IBaseCell cell, GraphicsObject g)
        {
            try
            {
                //bool res = base.DrawCell(cell, g);
                //if (!res)
                //{
                string str = new string('*', cell.Text.Length);
                if (cell.InEdit)
                {
                    str = new string('*', this.Text.Length);
                }

                //if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                //{
                //    str = this.Text;
                //}

                string text = str;
                SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(cell.ForeColor);
                StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(cell.HorizontalAlignment, cell.VerticalAlignment, false);
                sf.Trimming = StringTrimming.None;
                sf.FormatFlags = sf.FormatFlags | StringFormatFlags.MeasureTrailingSpaces;
                Feng.Drawing.GraphicsHelper.DrawString(g, text, cell.Font, solidbrush, cell.Rect, sf);

                //}

            }
            catch (Exception ex)
            {

            }
            return true;
        }


        public override void Read(DataExcel grid, int version, DataStruct data)
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

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellPassword celledit = new CellPassword(grid);
            return celledit;
        }

    }

}
