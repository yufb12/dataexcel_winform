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
using Feng.Enums;
using Feng.Forms.Controls.GridControl.Edits;
using Feng.Forms.Controls.GridControl;
using Feng.Forms.Interface;

namespace Feng.Forms.Views.Vector
{
    public partial class Layer : BaseRectView, IIndex ,IName
    {
        public Layer()
        {

        }

        private int _index = 0;
        public virtual int Index
        {
            get
            {
                return _index;
            }
            set
            {
                this._index = value;
            }
        }

        private string _name = string.Empty;
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override DataStruct Data {
            get {
                return null;
            }
        }
 
    }
}

