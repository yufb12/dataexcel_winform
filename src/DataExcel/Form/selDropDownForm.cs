using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Feng.Excel;
using Feng.Excel.Delegates;
using Feng.Excel.Interfaces;
using Feng.Forms.Events;

namespace Feng.Excel.Forms
{
    public partial class selDropDownForm : Feng.Forms.Popup.PopupForm
    {
        public selDropDownForm()
        {
            InitializeComponent();
            InitDataExcel();
        }
        public void excelsetting()
        {
            Feng.Excel.DataExcel dataExcel = this.dataExcel1.EditView;
            dataExcel.AllowChangedSize = false;
            dataExcel.AutoShowScroller = false;
            dataExcel.BingdingIndex = 0;
            dataExcel.BorderColor = System.Drawing.Color.Gray;
            dataExcel.DefaultCellFont = new System.Drawing.Font("Tahoma", 9F);
            dataExcel.EndDisplayedRowIndex = 15;
            dataExcel.Font = new System.Drawing.Font("Tahoma", 9F);
            dataExcel.FocusBackColor = System.Drawing.Color.White;
            dataExcel.FocusForeColor = System.Drawing.SystemColors.ControlText;
            dataExcel.LineColor = System.Drawing.Color.LightSkyBlue;
            dataExcel.Location = new System.Drawing.Point(0, 0);
            dataExcel.PrinterName = "";
            dataExcel.ReadOnlyBackColor = System.Drawing.Color.White;
            dataExcel.RowAutoSize = false;
            dataExcel.RowBackColor = System.Drawing.SystemColors.Window;
            dataExcel.ScrollStep = ((short)(3));
            dataExcel.SelectBorderColor = System.Drawing.Color.BlueViolet;
            dataExcel.TempSelectRect = null;
            dataExcel.Text = "dataExcel1";
        }
        public void InitDataExcel()
        {
            Feng.Excel.DataExcel dataExcel = this.dataExcel1.EditView;
            dataExcel.ShowBorder = false;
            dataExcel.ReadOnly = true;
            dataExcel.ShowRowHeader = true;
            dataExcel.ShowColumnHeader = true;
            dataExcel.ShowGridColumnLine = true;
            dataExcel.ShowGridRowLine = true;
            dataExcel.ShowSelectBorder = true;
            dataExcel.ShowSelectAddRect = false;
            //dataExcel.VScroller.Visible = true;
            //dataExcel.HScroller.Visible = false;
            dataExcel.AllowAdd = false;
            dataExcel.CellClick += new CellClickEventHandler(dataExcel1_CellClick);
            //dataExcel.KeyDown += new KeyEventHandler(dataExcel1_KeyDown);
        }

        void dataExcel1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataRow row = this.dataExcel1.EditView.GetDataRow(this.dataExcel1.EditView.FocusedCell);
                    this.OK(row, null);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        void dataExcel1_CellClick(object sender, ICell cell)
        {

            try
            {
                DataRow row = this.dataExcel1.EditView.GetDataRow(cell);
                this.OK(row, null);

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        public void ReSetFormSize()
        {
            int width = 2;
            foreach (IColumn col in this.dataExcel1.EditView.Columns)
            {
                if (col.Visible)
                {
                    width = width + (int)col.Width;
                }
            }


            int height = 2;
            int maxHeight = -1;
            foreach (IRow row in this.dataExcel1.EditView.Rows)
            {
                if (row.Visible)
                {
                    height = height + (int)row.Height;
                    if (maxHeight == -1)
                    {
                        maxHeight = height * 7;
                    }
                }
            }
            //if (maxHeight > height)
            //{
            //    this.Height = height;
            //    this.dataExcel1.EditView.VScroller.Visible = false;
            //}
            //else
            //{
            //    this.Height = maxHeight;
            //    this.dataExcel1.EditView.VScroller.Visible = true;
            //}
            this.Width = width;
        }

        public virtual DataTable QueryTable(DropDownBoxTextChangedEventArgs key)
        {
            return new DataTable();
        }
        public virtual DataTable QueryTable(DropDownButtonClickEventArgs e)
        {
            return new DataTable();
        }
        public override void DropDownButtonClick(object sender, DropDownButtonClickEventArgs e)
        {
            try
            {
                DataTable table = QueryTable(e);
                this.dataExcel1.EditView.Clear();
                this.dataExcel1.EditView.AutoGenerateColumns = false;
                this.dataExcel1.EditView.DataSource = table;
                ReSetFormSize();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            base.DropDownButtonClick(sender, e);
        }
        public override void KeyChanged(object sender, DropDownBoxTextChangedEventArgs key)
        {

            try
            {
                DataTable table = QueryTable(key);
                this.dataExcel1.EditView.Clear();
                this.dataExcel1.EditView.AutoGenerateColumns = false;
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    key.Cancel = true;
                    this.OK(row, null);
                    return;
                }
                this.dataExcel1.EditView.DataSource = table;
                ReSetFormSize();

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.KeyChanged(sender, key);
        }

        public ICell Cell { get; set; }
        public DataExcel MainGrid { get; set; }
        public string GetFilterValue(string filtervalue)
        {
            string txt = filtervalue.TrimStart();
            if (txt.Length > 2)
            {
                if (txt[0] == '&')
                {
                    string id = txt.Substring(1, txt.Length - 1);
                    if (this.MainGrid != null)
                    {
                        ICell cell = this.MainGrid.GetCellByID(id);
                        if (cell != null)
                        {
                            txt = Feng.Utils.ConvertHelper.ToString(cell.Value);
                            return txt;
                        }
                    }
                }
            }
            return filtervalue;
        }

        public override void SelectFirst()
        {

            try
            {
                DataRow row = this.dataExcel1.EditView.GetDataRow(1);
                if (row != null)
                {
                    this.OK(row, null);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.SelectFirst();
        }

        public override void MoveToFirst()
        {
            try
            {
                this.Focus();
                this.dataExcel1.EditView.Focus();
                this.dataExcel1.EditView.FocusedCell = this.dataExcel1.EditView[1, 1];
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            base.MoveToFirst();
        }
    }
}
