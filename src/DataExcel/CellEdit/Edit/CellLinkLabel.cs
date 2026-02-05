using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using Feng.Print;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellLinkLabel : CellBaseEdit
    {
        public CellLinkLabel(DataExcel grid)
            : base(grid)
        {
        }
        public override string ShortName { get { return "CellLinkLabel"; } set { } }

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            string text = cell.Text;
            if (string.IsNullOrEmpty(text))
            {
                text = cell.Text;
            }
            DrawCell(cell, g, cell.Rect, text);
            return true;
        }

        private void DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, string text)
        {
            Rectangle rect = bounds;
            Feng.Excel.DataExcel.DrawCellText(cell, g, rect, text);

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

        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            cell.Grid.BeginSetCursor(Cursors.Hand);
            return false;
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            Point viewloaction = this.Grid.PointControlToView(e.Location);
            if (!cell.Rect.Contains(viewloaction))
            {
                return false;
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(cell.Url))
                {
                    System.Diagnostics.Process.Start(cell.Url);
                }

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
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

        public override bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCell(cell, gob, cell.Rect, cell.Text);
            return true;
        }

        public override bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            if (value != null)
            {
                Feng.Drawing.GraphicsObject gob = e.Graphic;
                DrawCell(cell, gob, cell.Rect, value.ToString());
            }
            return true;
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellLinkLabel celledit = new CellLinkLabel(grid);
            return celledit;
        }
    }

}
