using Feng.Data;
using Feng.Drawing;
using Feng.Excel.Commands;
using Feng.Excel.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellProcess : CellBaseEdit
    {
        public CellProcess(DataExcel grid)
            : base(grid)
        {
        }

        public override string ShortName { get { return "CellProcess"; } set { } }
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

        public override bool DrawCell(IBaseCell cell, GraphicsObject g)
        {
            decimal d = Feng.Utils.ConvertHelper.ToDecimal(cell.Value);
            if (d > 1)
            {
                d = 1;
            }
            Color color = cell.FocusForeColor;
            if (color == Color.Empty)
            {
                color = Color.BlueViolet;
            }
            Rectangle rect = cell.Rect;
            rect.Width =(int)(rect.Width * d);
            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, rect, color);
            string text =cell.Text;
            Feng.Excel.Drawing.GraphicsHelper.DrawCellText(g, cell, cell.Rect,text);
            base.DrawCell(cell, g);
            return true;
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellProcess celledit = new CellProcess(grid);
            return celledit;
        }
    }

}
