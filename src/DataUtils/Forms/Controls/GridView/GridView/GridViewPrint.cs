using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Data;
using System.Reflection;

namespace Feng.Forms.Controls.GridControl
{ 
    public partial class GridView  
    { 
        private bool _fixPrintHeight = false;

        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public bool FixPrintHeight
        {
            get
            {
                return _fixPrintHeight;
            }
            set
            {
                _fixPrintHeight = value;
            }
        }


        private bool _printRowHeader = false;

        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public bool PrintRowHeader
        {
            get
            {
                return _printRowHeader;
            }
            set
            {
                _printRowHeader = value;
            }
        }


        private bool _printPageTotal = false;

        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        public bool PrintPageTotal
        {
            get
            {
                return _printPageTotal;
            }
            set
            {
                _printPageTotal = value;
            }
        }

        public virtual bool Print(Print.PrintArgs e)
        {
            return false;
        }
    }
}

