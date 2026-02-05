
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
  
    public class DeleteInfo
    {
        public DeleteInfo()
        {

        }
        public DeleteInfo(string v_key, string v_value)
        {
            this.Key = v_key;
            this.Value = v_value;
        }
        public string Key = string.Empty;
        public string Value = string.Empty;
    }


 

}
