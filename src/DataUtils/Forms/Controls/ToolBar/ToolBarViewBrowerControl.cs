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
using Feng.Forms.Views;

namespace Feng.Forms.Controls
{
    [ToolboxItem(true)]
    public class ToolBarViewBrowerControl : ToolBarViewControl
    {
        public ToolBarViewBrowerControl()
            : base()
        { 
        }

        ToolBarViewBrower toolbarview = null;

        public override ToolBarView ToolBarView
        {
            get {
                return toolbarview;
            }
        }
        public override void Init()
        {
            toolbarview = new ToolBarViewBrower();
            ToolBarView.BackColor = Color.White;
            toolbarview.Width = this.Width;
            toolbarview.Height = this.Height;
        }

 
    
 
    }
     

}
