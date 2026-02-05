using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [ToolboxItem(false)]
    public class CellFolderBrowserEdit : CellEdit
    {
        public CellFolderBrowserEdit(DataExcel grid)
            : base(grid)
        {
        }

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.Cell.ReadOnly)
                return false;
            using (System.Windows.Forms.FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.SelectedPath = this.Cell.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.Cell.Value = dlg.SelectedPath;
                    this.Text = dlg.SelectedPath;
                    return true;
                }
            }
            return base.OnMouseDoubleClick(sender, e, ve);
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellFolderBrowserEdit celledit = new CellFolderBrowserEdit(grid);
            return celledit;
        }
    }

}
