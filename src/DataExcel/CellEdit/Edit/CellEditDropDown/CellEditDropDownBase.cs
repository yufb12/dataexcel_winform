using Feng.Data;
using Feng.Excel.Edits.EditForms;
using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using Feng.Forms.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellEditDropDownBase : CellEdit, IPopupEdit
    {
        public CellEditDropDownBase(DataExcel grid)
            : base(grid)
        {
            grid.CellMouseClick += Grid_CellMouseClick;
        }

        private void Grid_CellMouseClick(object sender, ICell cell, MouseEventArgs e)
        {
            try
            {
                if (cell != this.Cell)
                {
                    this.HidePopup();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
            }
        }

        public virtual void ShowDesign()
        {
            //frmCellDataFileDesign frmDesign = new frmCellDataFileDesign();
            //System.Collections.Generic.List<ICell> cells = this.Grid.IDCells.GetCells();
            //frmDesign.InitCells(cells);
            //frmDesign.InitValue(Dics);
            //if (frmDesign.ShowDialog() == DialogResult.OK)
            //{
            //    this.Dics.Clear();
            //    this.Path = frmDesign.Path;
            //    List<ICell> list = frmDesign.GetCells();
            //    foreach (ICell cell in list)
            //    {
            //        if (Dics.ContainsKey(cell.ID))
            //        {
            //            Dics[cell.ID] = cell.Text;
            //        }
            //        else
            //        {
            //            Dics.Add(cell.ID, cell.Text);
            //        }
            //    }
            //}
        }
 
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                ICell cell = sender as ICell;
                if (cell == null)
                    return false;
                this.Cell = cell;
                if (cell.Grid.InDesign)
                {
                    int left = GetDesRectangle(cell);
                    Rectangle rect = new Rectangle(left, cell.Bottom - 16 - 2, 16, 16);
                    Point viewloaction = this.Grid.PointControlToView(e.Location);
                    if (rect.Contains(viewloaction))
                    {
                        ShowDesign();
                        return true;
                    }
                }
                if (cell.ReadOnly)
                    return false;

                if (true)
                {
                    int left = GetDropDownRectangle(cell);
                    Rectangle rect = new Rectangle(left, cell.Bottom - 16 - 2, 16, 16);
                    Point viewloaction = this.Grid.PointControlToView(e.Location);
                    if (rect.Contains(viewloaction))
                    {
                        ShowPopup();
                        return true;
                    }
                }

                return base.OnMouseDown(sender, e, ve);
            }
            catch (Exception ex)
            {
                this.Grid.OnException(ex);
            }
            return base.OnMouseDown(sender, e, ve);
        }

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
    bool res = base.DrawCell(cell, g);
            if (cell.Grid.InDesign)
            {
                int left = GetDesRectangle(cell);
                Rectangle rect = new Rectangle(left, cell.Bottom - 16 - 2, 16, 16);
                g.Graphics.DrawImage(Feng.Utils.Properties.Resources.EditButton_More, rect);
            }
            if (cell.ReadOnly)
                return false;

            if (true)
            {
                int left = GetDropDownRectangle(cell);
                Rectangle rect = new Rectangle(left, cell.Bottom - 16 - 2, 16, 16);
                g.Graphics.DrawImage(Feng.Utils.Properties.Resources.EditButton_Drop, rect);
            }

            return res;
        }
 
        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0); 
            }
        }
 
        private int GetDesRectangle(IBaseCell cell)
        {
            if (this.Grid.InDesign)
            {
                return cell.Right - 16 - 2;
            }
            return cell.Right;
        }

        private int GetDropDownRectangle(IBaseCell cell)
        {
            int left = GetDesRectangle(cell);
            if (true)
            {
                return left - 16 - 2;
            }
            return left;
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
            CellEditDropDownBase celledit = new CellEditDropDownBase(grid);
            return celledit;
        }

        private CellEditDropDownBaseForm dropdownpopupform = null;
        public void InitDropDownForm()
        {
            dropdownpopupform = new CellEditDropDownBaseForm();
            dropdownpopupform.ParentEditForm = this.Grid.FindForm();
            dropdownpopupform.ParentEditForm.VisibleChanged += ParentEditForm_VisibleChanged;
            dropdownpopupform.ParentEditForm.FormClosed += ParentEditForm_FormClosed;
            dropdownpopupform.InitPopup(this);
            dropdownpopupform.Controls.Add(PopupControl);
            PopupControl.Dock = DockStyle.Fill;
        }

        private Control popupcontrol = null;
        public Control PopupControl { get; set; }
 
        private void ParentEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dropdownpopupform.ParentEditForm != null)
            {
                if (!dropdownpopupform.ParentEditForm.Visible)
                {
                    dropdownpopupform.Cancel();
                }
            }
        }

        private void ParentEditForm_VisibleChanged(object sender, EventArgs e)
        {
            if (dropdownpopupform.ParentEditForm != null)
            {
                if (!dropdownpopupform.ParentEditForm.Visible)
                {
                    dropdownpopupform.Cancel();
                }
            }
        }

        public virtual void ShowPopup()
        {
            if (dropdownpopupform == null)
            {
                InitDropDownForm();
            }
            if (dropdownpopupform.Visible)
            {
                dropdownpopupform.Hide();
            }
            else
            {
                Point point = this.Grid.PointToScreen(new Point(this.Cell.Left, this.Cell.Bottom));
                dropdownpopupform.Popup(point);
            }
        }

        public virtual void HidePopup()
        {
            if (dropdownpopupform == null)
            {
                return;
            }
            dropdownpopupform.Hide();
        }

        public virtual void OnOK(object value, object model)
        {
            try
            {
                this.Text = Feng.Utils.ConvertHelper.ToString(value);
                this.Cell.Value = value;
                HidePopup();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
            }
        }

        public virtual void OnCancel()
        {

        } 
    }

}
