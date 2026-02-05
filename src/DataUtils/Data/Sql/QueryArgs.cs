using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace Feng.Data
{
    public class QueryArgs
    {
        public const int QueryMode_Default = 0;
        public const int QueryMode_Equals = 1;
        public const int QueryMode_LessThan = 2;
        public const int QueryMode_MoreThan = 3;
        public const int QueryMode_Like = 4;
        public const int QueryMode_LeftLike = 5;
        public const int QueryMode_RightLike = 6;
        public const int QueryMode_LessThanAndEquals = 7;
        public const int QueryMode_MoreThanAndEquals = 8;
        public int QueryMode { get; set; }
        public string ColumnName { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }

}
