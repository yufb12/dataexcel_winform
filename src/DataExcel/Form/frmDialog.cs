using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Forms
{
    public partial class frmDialog : Form
    {
        public frmDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            
            if (this.dataExcel1.EditView.DisplayArea != null)
            {
                int minrow = this.dataExcel1.EditView.DisplayArea.MinCell.Row.Index;
                int maxrow = this.dataExcel1.EditView.DisplayArea.MaxCell.Row.Index;
                int mincolumn = this.dataExcel1.EditView.DisplayArea.MinCell.Column.Index;
                int maxcolumn = this.dataExcel1.EditView.DisplayArea.MaxCell.Column.Index;
                float height = 0;
                for (int i = minrow; i <= maxrow; i++)
                {
                    height = height + this.dataExcel1.EditView.Rows[i].Height;
                }
                float width = 0;
                for (int i = mincolumn; i <= maxcolumn; i++)
                {
                    width = width + this.dataExcel1.EditView.Columns[i].Width;
                }
                this.dataExcel1.EditView.FirstDisplayedColumnIndex = mincolumn;
                this.dataExcel1.EditView.FirstDisplayedRowIndex = minrow;
                this.dataExcel1.EditView.AutoShowScroller = false;
                //this.dataExcel1.EditView.ShowColumnHeader = false;
                //this.dataExcel1.EditView.ShowGridColumnLine = false;
                //this.dataExcel1.EditView.ShowGridRowLine = false;
                this.dataExcel1.EditView.ShowHorizontalRuler = false;
                this.dataExcel1.EditView.ShowHorizontalScroller = false;
                this.dataExcel1.EditView.ShowRowHeader = false;
                this.dataExcel1.EditView.ShowSelectAddRect = false;
                this.dataExcel1.EditView.ShowVerticalRuler = false;
                this.dataExcel1.EditView.ShowVerticalScroller = false;
                if (width >= this.dataExcel1.EditView.DefaultRowHeight)
                {
                    this.Width = (int)width;
                }
                int CaptionHeight = SystemInformation.CaptionHeight;
                if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
                {
                    CaptionHeight = 0;
                }
                this.Height = (int)height + CaptionHeight; 
            }

            base.OnLoad(e);
        }
        public void InitData(Feng.Excel.DataExcel excel)
        {
            this.dataExcel1.InitEditView(excel);
        }

         
   
    }
}
