using Feng.Data;
using Feng.Drawing;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using Feng.Print;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellLabel : CellBaseEdit
    {
        public CellLabel(DataExcel grid)
            : base(grid)
        {

        }
        public override string ShortName { get { return "CellLabel"; } set { } }

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

        public override bool InitEdit(object obj)
        {
            return false;
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
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

        private void DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, string text)
        {
            Rectangle rect = bounds;
            Feng.Excel.DataExcel.DrawCellText(cell, g, rect, text);

        }

        public override bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            DrawCell(cell, e.Graphic, cell.Rect, cell.Text);
            return true;
        }

        public override bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            if (value != null)
            {
                DrawCell(cell, e.Graphic, cell.Rect, value.ToString());
            }
            return true;
        }
        public override ICellEditControl Clone(DataExcel grid)
        {
            CellLabel celledit = new CellLabel(grid);
            return celledit;
        }
    }

}
