using Feng.Excel.Interfaces;
using Feng.Forms.Base;

namespace Feng.Excel.Actions
{ 
    public class CellPropertyAction : PropertyAction
    {
        public CellPropertyAction()
        {

        }

        public ICell Cell { get; set; }
    }

    public class DataExcelPropertyAction : PropertyAction
    {
        public DataExcelPropertyAction()
        {

        }

        public DataExcel Grid { get; set; }
    }

}