using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Generic
{
    [Serializable]
    public class DataException
    {
        
    }

    [Serializable]
    public class CellSizeSetException : Exception 
    {
        public CellSizeSetException(string message)
            : base(message)
        {
            
        }
    }
}
