using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
#warning 未实现工具，命令
    [ToolboxItem(false)]
    public class CellFileSelectEdit : CellEdit
    {
        public CellFileSelectEdit(DataExcel grid)
            : base(grid)
        {
        }

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.Cell.ReadOnly)
                return false;
            using (System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.InitialDirectory = this.Cell.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.Cell.Value = dlg.FileName;
                    this.Text = dlg.FileName;
                }
            }
            return base.OnMouseDoubleClick(sender, e, ve);
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellFileSelectEdit celledit = new CellFileSelectEdit(grid);
            return celledit;
        }
    }

}
