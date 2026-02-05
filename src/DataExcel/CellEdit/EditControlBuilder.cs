using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Edits
{
    public class EditControlBuilder
    {
        public EditControlBuilder()
        {

        }

        public static ICellEditControl Build(DataExcel grid, string name)
        {
            foreach (var item in grid.Edits)
            {
                if (name == item.ShortName)
                {
                    return item.Clone(grid);
                }
            }
            return null;
        }

    }


}
