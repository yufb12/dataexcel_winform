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
    public partial class frmBrower : Form
    {
        public frmBrower()
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
        public void InitDataExcel()
        {
            Feng.Forms.Controls.ToolBar toolBar = new Feng.Forms.Controls.ToolBar();
            toolBar.Dock = DockStyle.Top;
            this.Controls.Add(toolBar);
            toolBar.Items.AddItem(new Feng.Forms.Controls.ToolBarItem("保存") { ToolBar = toolBar });
            toolBar.ItemClick += ToolBar_ItemClick;
            this.Controls.Add(this.dataExcel1);
            this.dataExcel1.BringToFront();
            this.dataExcel1.Dock = DockStyle.Fill;

        }

        private void ToolBar_ItemClick(object sender, Feng.Forms.Controls.ToolBarItem item)
        {
            try
            {
                if (item.Text == "保存")
                {
                    this.dataExcel1.EditView.Save();
                    this.dataExcel1.EditView.ShowToolTip("保存成功");
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public Feng.Excel.DataExcelControl dataExcel1 = null;
        public static void Show(string file)
        {

            try
            {

                frmBrower dlg = new frmBrower();
                Feng.Excel.DataExcelControl grid = new DataExcelControl();
                grid.EditView.Open(file);
                //dlg.Controls.Add(grid);
                dlg.FormBorderStyle = FormBorderStyle.None;
                //grid.Dock = DockStyle.Fill;
                dlg.StartPosition = FormStartPosition.CenterScreen;
                dlg.dataExcel1 = grid;
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
  
        }
    }
}
