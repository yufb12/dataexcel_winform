using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Print
{
    public partial class PrintPageHeader : Form
    {
        public PrintPageHeader()
        {
            InitializeComponent();
        }
        [NonSerialized]
        private DataExcel dataExcel2;
        
        public DataExcel PrintGrid
        {
            get { return this.dataExcel2; }
            set { 
                this.dataExcel2 = value; 
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = false;
            base.OnClosing(e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.PrintGrid.ShowColumnHeader = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new FontDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.PrintGrid.SetSelectCellFont(dlg.Font);
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.PrintGrid.SetSelectCellColorForeColor(dlg.Color);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.PrintGrid.SetSelectCellColorBackColor(dlg.Color);
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.PrintGrid.SetMergeCell();
        }

        private void PrintPageHeader_Load(object sender, EventArgs e)
        {

            //this.panel1.Controls.Add(dataExcel2);
            //this.dataExcel2.Dock = DockStyle.Fill;
            this.PrintGrid.ShowColumnHeader = true;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (this.PrintGrid.FocusedCell == null)
                return;
            if (this.PrintGrid.SelectCells != null)
            {
                this.PrintGrid.SetSelectCellAlignStringLeft();
                return;

            }
            this.PrintGrid.FocusedCell.HorizontalAlignment = StringAlignment.Near;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (this.PrintGrid.FocusedCell == null)
                return;
            if (this.PrintGrid.SelectCells != null)
            {
                this.PrintGrid.SetSelectCellAlignStringCenter();
                return;

            }
            this.PrintGrid.FocusedCell.HorizontalAlignment = StringAlignment.Center;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (this.PrintGrid.FocusedCell == null)
                return;
            if (this.PrintGrid.SelectCells != null)
            {
                this.PrintGrid.SetSelectCellAlignStringRight();
                return;

            }
            this.PrintGrid.FocusedCell.HorizontalAlignment = StringAlignment.Far;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (this.PrintGrid.FocusedCell == null)
                return;
            if (this.PrintGrid.SelectCells != null)
            {
                this.PrintGrid.SetSelectCellAlignLineTop();
                return;

            }
            this.PrintGrid.FocusedCell.VerticalAlignment = StringAlignment.Near;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (this.PrintGrid.FocusedCell == null)
                return;
            if (this.PrintGrid.SelectCells != null)
            {
                this.PrintGrid.SetSelectCellAlignLineCenter();
                return;

            }
            this.PrintGrid.FocusedCell.VerticalAlignment = StringAlignment.Center;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (this.PrintGrid.FocusedCell == null)
                return;
            if (this.PrintGrid.SelectCells != null)
            {
                this.PrintGrid.SetSelectCellAlignLineBottom();
                return;

            }
            this.PrintGrid.FocusedCell.VerticalAlignment = StringAlignment.Far;
        }
    }
}
