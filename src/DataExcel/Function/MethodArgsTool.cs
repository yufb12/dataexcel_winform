using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Script.Method
{ 
    public abstract class DataExcelMethodContainer: BaseMethodContainer
    {
        public DataExcelMethodContainer() 
        {

        }
        public virtual ICell GetCell(int index, params object[] args)
        {
            ICell value = GetArgIndex(index, args) as ICell;
            return value;
        }
        public override object GetValue(int index, params object[] args)
        {
            object value = GetArgIndex(index, args);
            ICell cell = value as ICell;
            if (cell != null)
                return cell.Value;
            return value;

        }
    }
}