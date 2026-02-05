using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Feng.Utils;
using System;
using Feng.Forms; 
using Feng.Forms.Events;
namespace Feng.Forms.Popup
{
    public class PopupBaseForm : Form
    {   
        public PopupBaseForm()
        { 
        } 
 
        public virtual void Cancel()
        { 
            this.Visible = false;
        }

        public virtual void OK(object value,object model)
        { 
            this.Visible = false;
        }

        public virtual void KeyChanged(object sender, DropDownBoxTextChangedEventArgs e)
        {

        }

        public virtual void DropDownButtonClick(object sender, DropDownButtonClickEventArgs e)
        {

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= Utils.UnsafeNativeMethods.WS_EX_NOACTIVATE;
                return cp;
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
    }


}