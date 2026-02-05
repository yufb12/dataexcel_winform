using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.App
{
 
#if DEBUG
    public class FileInvalidException : Exception
    {
        public FileInvalidException(string msg)
        {
            this._msg = msg;
        }
        public string _msg = string.Empty;
        public override string Message
        {
            get
            {
                return base.Message + " FileInvalid " + _msg;
            }
        }
    }
#endif
}
