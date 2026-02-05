using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Feng.Excel.Print
{
    [Serializable]
    public class PrintSetting
    {

        public PrintSetting(DataExcel grid)
        {
            this._grid = grid;
            PrintHeader = new PrintHeader(this);
            //PrintBody = new PrintBody(this);
            PrintFooter = new PrintFooter(this);
        }

        public PrintHeader PrintHeader { get; set; }
 
        public PrintFooter PrintFooter { get; set; }
 
        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }
        }

        #endregion
    }
}
