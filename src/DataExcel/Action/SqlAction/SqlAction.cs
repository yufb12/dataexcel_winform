using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Feng.Excel.Interfaces;
using Feng.Data;

namespace Feng.Excel.Actions
{ 

    public class SqlQueryAction : string
    {
        public SqlQueryAction()
        {

        }
        private string _sql = string.Empty;
        public string Sql
        {
            get
            {
                return _sql;
            }
            set
            {
                _sql = value;
            }
        }
        private QeuryInofArgsCollection _Args = null;
        public QeuryInofArgsCollection Args
        {
            get
            {
                return _Args;
            }
            set
            {
                _Args = value;
            }
        }
    }
     
}
