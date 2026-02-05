#define NoTestReSetSize
using Feng.Excel.Interfaces;

namespace Feng.Excel.Data
{

    public class FieldCellInfo
    {
        public FieldCellInfo(FieldRowInfo row)
        {
            _Row = row;
        }
        private FieldRowInfo _Row = null;
        public FieldRowInfo Row
        {
            get
            {
                return _Row;
            }
        }
        public string ColumName { get; set; }
        public ICell Cell { get; set; }
    }
}