using Feng.Net.ProFiler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class frmNetProfiler : Form
    {
        public frmNetProfiler()
        {
            InitializeComponent();
            InitGrid();
        }
        public TcpClientProFiler Profile = null;
 
        public void InitGrid()
        {
            this.gridViewControl1.GridView.ReadOnly = true;
            this.gridViewControl1.GridView.AutoGenerateColumns = false;
            this.gridViewControl1.GridView.Columns.Add("Index");
            this.gridViewControl1.GridView.Columns.Add("Name");
            this.gridViewControl1.GridView.Columns.Add("IP");
            this.gridViewControl1.GridView.Columns.Add("Time");
            this.gridViewControl1.GridView.Columns.Add("Method");
            this.gridViewControl1.GridView.Columns.Add("DataLength");
        }
        public void Init(TcpClientProFiler profile)
        { 
            Profile = profile;
            if (Profile != null)
            {
                this.gridViewControl1.GridView.DataSource = Profile.List;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Profile != null)
            {
                this.gridViewControl1.GridView.RefreshColumns();
                this.gridViewControl1.GridView.RefreshRowValue();
        
            }
        }
    }
}
