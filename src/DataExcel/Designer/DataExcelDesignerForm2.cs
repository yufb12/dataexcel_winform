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
    public partial class DataExcelDesignerForm2 : Form
    {
        public DataExcelDesignerForm2()
        {
            InitializeComponent();
        }
        public string File { get; set; }
        private DataExcel _grid = null;
        public DataExcel Grid { get { return _grid; } }
        public void Init()
        {
            DataExcel grid = new DataExcel();
            grid.Init();
            _grid = grid;
            //this.panel1.Controls.Add(grid);
            //grid.Dock = DockStyle.Fill;
        }
        private void ToolStripButton_New_Click(object sender, EventArgs e)
        {

            try
            {
                if (Grid != null)
                {
                    Grid.Clear();
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
                    Grid.Open();
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
                        Grid.Save(bw);
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
