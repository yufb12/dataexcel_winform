using Feng.Excel;
using Feng.Excel.Builder;
using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Feng.Excel.Forms
{
    public partial class frmPopupGrid : Feng.Forms.Popup.PopupForm
    {
        public frmPopupGrid()
        {
            InitializeComponent();
        }
 
        public override void Popup(Point pt)
        {
            base.Popup(pt);
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public void InitData(DataExcel dataExcel)
        {
            this.Controls.Clear();
            dataExcel.ReadOnly = true;
            int minrow = 1;
            int maxrow = 10;
            int mincolumn = 1;
            int maxcolumn = 3;
            if (dataExcel.DisplayArea != null)
            {
                minrow = dataExcel.DisplayArea.MinCell.Row.Index;
                maxrow = dataExcel.DisplayArea.MaxCell.Row.Index;
                mincolumn = dataExcel.DisplayArea.MinCell.Column.Index;
                maxcolumn = dataExcel.DisplayArea.MaxCell.Column.Index;

                float height = 0;
                for (int i = minrow; i <= maxrow; i++)
                {
                    IRow row = dataExcel.Rows[i];
                    if (row == null)
                        continue;
                    height = height + dataExcel.Rows[i].Height;
                }
                float width = 0;
                for (int i = mincolumn; i <= maxcolumn; i++)
                {
                    width = width + dataExcel.Columns[i].Width;
                }
                dataExcel.FirstDisplayedColumnIndex = mincolumn;
                dataExcel.FirstDisplayedRowIndex = minrow;
                if (width >= dataExcel.DefaultRowHeight)
                {
                    this.Width = (int)width;
                }
                int CaptionHeight = 0;
                this.Height = (int)height + CaptionHeight;
            }
            //this.Controls.Add(dataExcel);
            //dataExcel.Dock = System.Windows.Forms.DockStyle.Fill;
        }

    }
}
