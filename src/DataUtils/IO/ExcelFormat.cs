using System;

namespace Feng.Excel.IO
{ 
    public class ExcelFormat
    {
        public string Text { get; set; }
        public string FileExtension { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
