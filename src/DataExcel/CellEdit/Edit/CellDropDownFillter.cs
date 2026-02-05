using Feng.Data;
using Feng.Excel.Edits.EditForms;
using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using Feng.Forms.Popup;
using Feng.Forms.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{ 
    public class CellDropDownFillter : CellEdit, IPopupEdit
    {
        public CellDropDownFillter(DataExcel grid)
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
 

        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                ICell cell = sender as ICell;
                if (cell == null)
                    return false;
                this.Cell = cell; 
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
            if (base.DrawCell(cell, g))
                return true;
            if (cell.ReadOnly)
                return false;

            if (true)
            {
                if (cell.InEdit)
                {
                    int left = GetDropDownRectangle(cell);
                    Rectangle rect = new Rectangle(left, cell.Bottom - 16 - 2, 16, 16);
                    g.Graphics.DrawImage(Feng.Drawing.Images.celleditdropdown, rect);
                }
            }

            return true;
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
                this._griddata = bw.ReadIndex(2, this._griddata);
            }
        }

        public override void EndEdit()
        { 
        }


        private int GetDesRectangle(IBaseCell cell)
        {
            if (false)
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
                    bw.Write(2, this._griddata);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellDropDownFillter celledit = new CellDropDownFillter(grid);
            celledit._griddata = this._griddata;
            return celledit;
        }

        private byte[] _griddata = null;

        private CellDropDownFillterForm dropdownpopupform = null;
        public void InitDropDownForm()
        {
            dropdownpopupform = new CellDropDownFillterForm();
            dropdownpopupform.ParentEditForm = this.Grid.FindForm();
            dropdownpopupform.ParentEditForm.VisibleChanged += ParentEditForm_VisibleChanged;
            dropdownpopupform.ParentEditForm.FormClosed += ParentEditForm_FormClosed;
     

        }

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
            //DateTime dt = Feng.Utils.ConvertHelper.ToDateTime(Cell.Value);
            //if (dt == DateTime.MinValue)
            //{
            //    dt = DateTime.Now;
            //}
            //dropdownpopupform.Value = dt;
            Point point = this.Grid.PointToScreen(new Point(this.Cell.Left, this.Cell.Bottom));
            dropdownpopupform.Grid = this.Cell.Grid;
            dropdownpopupform.cell = this.Cell;
            dropdownpopupform.Popup(point);
            dropdownpopupform.InitPopup(this);
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
                DateTime dateTime = (DateTime)value;
                this.Cell.Value = dateTime;
                this.Cell.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                HidePopup();
                this.Grid.EndEdit();
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
