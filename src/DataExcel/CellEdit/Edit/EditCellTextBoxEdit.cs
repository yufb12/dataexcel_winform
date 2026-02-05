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
using Feng.Forms.Controls;
using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.Designer;
using Feng.Excel.Commands;
using Feng.Forms.Views;

namespace Feng.Excel.Edits
{ 
    public class CellTextBoxEdit : CellBaseEdit, IEdit,ICellEditControl
    {
        public override string ShortName { get { return "CellTextBoxEdit"; } set { } }
        private TextBox edit = null;

        public CellTextBoxEdit(DataExcel grid):
            base(grid)
        {
            edit = new NumTextBox();
            edit.TextChanged += Edit_TextChanged;
        }

        private void Edit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Grid.OnCellEditControlValueChanged(this.Cell, this.Text);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
        }
 
        public override bool InitEdit(object obj)
        {
            try
            {
                ICell cell = obj as ICell;
                if (cell == null)
                    return false; 
                this.Cell = cell;
                if (this.Cell == null)
                    return false;
                this.Left = (int)cell.Left + 1;
                this.Top = (int)cell.Top + 1;
                this.Width = (int)cell.Width - 1;
                this.Height = (int)cell.Height - 1;
                this.edit.Visible = true;
                this.edit.Multiline = true;
                this.Grid.AddEdit(this);
                this.Grid.AddControl(this.edit);
                Point viewloaction = this.Grid .PointViewToControl(cell.Location);
                this.edit.Left = (int)viewloaction.X + 1;
                this.edit.Top = (int)viewloaction.Y + 1;
                this.edit.Width = (int)cell.Width - 1;
                this.edit.Height = (int)cell.Height - 1;
                this.edit.Visible = true;
                if (!string.IsNullOrEmpty(cell.Expression))
                {
                    this.Text = "=" + cell.Expression;
                }
                else
                {
                    this.Text = cell.Text;
                }
                this.edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.edit.Focus();
                this._inedit = true;
                return true;
            }
            finally
            {
            }
        }
 
        public override void EndEdit()
        {
            try
            {
                this.edit.Hide();
                if (this.Cell != null)
                {
                    if (this.Cell.Grid.CanUndoRedo)
                    {
                        CellValueCommand cmd = new CellValueCommand();
                        cmd.Text = this.Cell.Text;
                        cmd.Value = this.Cell.Value;
                        cmd.Cell = this.Cell;
                        this.Cell.Grid.Commands.Add(cmd);
                    }
                    this.Cell.Value = this.edit.Text;
                    this.Cell.Text = this.edit.Text;
                    this.Grid.EndEditClear();
                    this.Grid.EndEdit();
                }

                this.Cell = null;
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                this._inedit = false;
            }
        }
 
 
        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this._id = bw.ReadIndex(1, 0);
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
 
        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            if (e.Alt || e.Control || e.Shift)
            {

            }
            else
            {
                if (e.KeyData == Keys.Right)
                {
                    this.Cell.Grid.MoveFocusedCellToRightCell();
                }
                if (e.KeyData == Keys.Left)
                {
                    this.Cell.Grid.MoveFocusedCellToLeftCell();
                }
            }
            return false;
        }
 
        private int _id = -1;
        [Browsable(false)]
        public override int AddressID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
 
        public override ICellEditControl Clone(DataExcel grid)
        {
            CellCnNumber celledit = new CellCnNumber(grid);  
            return celledit;
        }
 
    }

}
