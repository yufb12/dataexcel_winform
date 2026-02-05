using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Utils.Script.CBScript.Forms
{
    public partial class FrmTable : Form
    {
        public FrmTable()
        {
            InitializeComponent();
        }

        public void InitDataSource(DataTable table)
        {
            this.dataGridView1.AutoGenerateColumns = true;
            this.bindingSource1.DataSource = table;
            
        }
    }
}
