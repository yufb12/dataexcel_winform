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
using System.Collections;
using static System.Windows.Forms.ComboBox;

namespace Feng.Excel.Edits
{
    public class CellComboBox : CellBaseEdit, IEdit, ICellEditControl
    {
        public override string ShortName { get { return "CellComboBox"; } set { } }
        private ComboBox edit = null;

        public CellComboBox(DataExcel grid) :
            base(grid)
        {
            edit = new ComboBox();
            edit.TextChanged += Edit_TextChanged;
            edit.SelectedIndexChanged += Edit_SelectedIndexChanged; 
        }

        private void Edit_MouseDoubleClick(object sender, MouseEventArgs e)
        { 
          
        }

        private bool lck = false;
        private void Edit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lck)
                    return;
                EndEdit();
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
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
                lck = true;
                this.Left = (int)cell.Left + 1;
                this.Top = (int)cell.Top + 1;
                this.Width = (int)cell.Width - 1;
                this.Height = (int)cell.Height - 1;
                this.edit.Visible = true;
                this.Grid.AddEdit(this);
                this.Grid.AddControl(this.edit);
                Point viewloaction = this.Grid.PointViewToControl(cell.Location);
                this.edit.Left = (int)viewloaction.X + 1;
                this.edit.Top = (int)viewloaction.Y + 1;
                this.edit.Width = (int)cell.Width - 1;
                this.edit.Height = (int)cell.Height - 1;
                this.edit.Visible = true;
                this.Text = cell.Text;
                this.edit.Text = cell.Text;
                this.edit.Show();
                this.edit.Focus();
                this._inedit = true;
                lck = false;
                return true;
            }
            finally
            {
                lck = false;
            }
        }

        public override void EndEdit()
        {
            try
            {
                if (this.edit.Visible)
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
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this._inedit = false;
            }
        }

        public virtual bool DrawCell(IBaseCell cell, PrintArgs g)
        {
            return false;
        }

        public virtual bool DrawCellBack(IBaseCell cell, PrintArgs g)
        {
            return false;
        }

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (this.Grid.InDesign)
                { 
                    using (ComboxEditForm dlg = new ComboxEditForm())
                    {
                        dlg.StartPosition = FormStartPosition.CenterScreen; 
                        List<string> items = new List<string>();
                        foreach (object m in edit.Items)
                        {
                            if (m != null)
                            {
                                items.Add(m.ToString());
                            }
                        }
                        dlg.SetText(items);
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            edit.Items.Clear();
                            List<string> list = dlg.GetText();
                            foreach (string item in list)
                            {
                                edit.Items.Add(item);
                            }
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
            return false;
        }

        public override bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            return base.OnDoubleClick(sender, e, ve);
        }

        [Editor(typeof(ComboxEdit), typeof(UITypeEditor))]
        public ObjectCollection Items
        {
            get
            {
                return this.edit.Items;
            }
        }
        public ComboBoxStyle DropDownStyle
        {
            get { return this.edit.DropDownStyle; }
            set { this.edit.DropDownStyle = value; }
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
                int count = bw.ReadIndex(2, 0);
                for (int i = 0; i < count; i++)
                {
                    this.edit.Items.Add(bw.ReadString());
                }
                this.edit.DropDownStyle = (ComboBoxStyle)bw.ReadIndex(3, 1);
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
                    bw.Write(2, this.edit.Items.Count);
                    foreach (string str in this.edit.Items)
                    {
                        bw.Write(str);
                    }
                    bw.Write(3, (int)this.edit.DropDownStyle);
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
            CellComboBox celledit = new CellComboBox(grid);
            foreach (var item in this.Items)
            {
                celledit.Items.Add(item);
            }
            return celledit;
        }

    }

}
