using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Designer
{
    public partial class DataExcelDesignerForm : System.Windows.Forms.Form
    {
        public DataExcelDesignerForm()
        {
            InitializeComponent();
        }
        public string File { get; set; }
        private DataExcelControl _grid = null;
        public DataExcelControl Grid { get { return _grid; } }
        public void Init()
        {
            DataExcelControl grid = new DataExcelControl();

            _grid = grid;
            Feng.Excel.Extend.RightKeyExtend extend = new Extend.RightKeyExtend();
            extend.Init(grid.EditView);
        }       
        public void Init(DataExcelControl excel)
        {
            Grid.InitEditView(excel.EditView);
        }
        private void ToolStripButton_New_Click(object sender, EventArgs e)
        {

            try
            {
                if (Grid != null)
                {
                    Grid.EditView.Clear();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        
        }

        private void ToolStripButton_Open_Click(object sender, EventArgs e)
        {

            try
            {
                if (Grid != null)
                {
                    Grid.EditView.Open();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        public byte[] Data { get; set; }
        private void ToolStripButton_Save_Click(object sender, EventArgs e)
        {

            try
            {
                if (Grid != null)
                {
                    using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                    {
                        Grid.EditView.Save(bw);
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        Data = bw.GetData();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
    }
}
