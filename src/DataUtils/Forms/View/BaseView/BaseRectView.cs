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

namespace Feng.Forms.Views
{
    public abstract class BaseRectView : DivView, Feng.Forms.Interface.IDataStruct, Feng.Print.IPrint
    {
        public BaseRectView()
        {

        }
 
        private Color _MouseOverBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual Color MouseOverBackColor
        {
            get
            {
                return _MouseOverBackColor;
            }
            set
            {
                _MouseOverBackColor = value;
            }
        }
 
        private Color _MouseDownBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual Color MouseDownBackColor
        {
            get
            {
                return _MouseDownBackColor;
            }
            set
            {
                _MouseDownBackColor = value;
            }
        }
 
        private Color _FocusBackColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual Color FocusBackColor
        {
            get
            {
                return _FocusBackColor;
            }
            set
            {
                _FocusBackColor = value;
            }
        }
 
        private Color _MouseOverForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual Color MouseOverForeColor
        {
            get
            {
                return _MouseOverForeColor;
            }
            set
            {
                _MouseOverForeColor = value;
            }
        }
 
        private Color _MouseDownForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual Color MouseDownForeColor
        {
            get
            {
                return _MouseDownForeColor;
            }
            set
            {
                _MouseDownForeColor = value;
            }
        }

 
        private Color _FocusForeColor = Color.Empty;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual Color FocusForeColor
        {
            get
            {
                return _FocusForeColor;
            }
            set
            {
                _FocusForeColor = value;
            }
        }




    }
}

