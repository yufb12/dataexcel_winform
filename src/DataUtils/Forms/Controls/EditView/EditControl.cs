using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Utils;
using Feng.Drawing;
using Feng.Forms.Views;

namespace Feng.Forms.Controls
{
    [ToolboxItem(true)]
    public class EditControl : ViewControl
    {
        public EditControl()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ContainerControl, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.UpdateStyles();
            Init();
        }
        public void Init()
        { 
        }
        public override string Text
        {
            get
            {
                return this.EditView.Text;
            }
            set
            {
                this.EditView.Text = value;
                this.Invalidate();
            }
        }
        private EditView editView = null;
        public EditView EditView
        {
            get
            {
                if (editView == null)
                {
                    editView = new EditView();
                    editView.BindingControl(this);
                }
                return editView;
            }
        }

        public virtual void AppendText(string text)
        {
            EditView.Append(text);
        }


    }
}
