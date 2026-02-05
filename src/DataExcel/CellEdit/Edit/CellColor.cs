using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Feng.Excel.Designer;
using System.Drawing.Design;
using Feng.Utils;
using Feng.Print;
using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Excel.Commands;
using Feng.Forms.Views;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellColor : CellBaseEdit
    {
        public CellColor(DataExcel grid)
            : base(grid)
        {
        }

        public override string ShortName { get { return "CellColor"; } set { } }
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
 
        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (!cell.ReadOnly)
            {
                using (System.Windows.Forms.ColorDialog dlg = new ColorDialog())
                {
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Color color = dlg.Color;
                        if (cell.Grid.CanUndoRedo)
                        {
                            CellBackColorCommand cmd = new CellBackColorCommand();
                            cmd.Value = cell.BackColor;
                            cmd.Cell = cell;
                            cell.Grid.Commands.Add(cmd);
                        }
                        cell.BackColor = color;
                        if (!this.Cell.IsMergeCell)
                        {
                            if (this.Cell.ContensWidth > this.Grid.DefaultColumnWidth)
                            {
                                this.Grid.RefreshColumnWidth(this.Cell.Column);
                            }
                        }
                        this.Grid.EndEditClear();
                    }
                    //return true;
                }
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
            CellColor celledit = new CellColor(grid);
            return celledit;
        }
    }

}
