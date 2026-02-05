
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Feng.Data; 
using Feng.Excel.Styles;
using Feng.Forms.Interface;


namespace Feng.Excel
{
    [Serializable]
    public class ReadInfo  
    { 
        public ReadInfo()
        { 

        }

        public bool ExecuteExpress { get; set; }
        public bool LoadCompleted { get; set; }
        public bool PropertyLoadCompleted { get; set; }
        public bool FileLoadFinishedFreshCellDataBase { get; set; }
    }
}
