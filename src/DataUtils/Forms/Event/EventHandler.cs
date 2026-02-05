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
namespace Feng.Forms.Events
{ 
    public static class EditEvent
    {
        public delegate void DataSourceChangedHandler(object sender, EventArgs e);
        public delegate void SelectValueChangedHandler(object sender, object value, object model);
        public delegate void DropDownFormFirstShowHandler(object sender, DropDownFormFirstShowEventArgs e);
        public delegate void DropDownBoxKeyClick();
        public delegate void BeforeValueChangedHandler(object sender, BaseCanceelEventArgs e);
        public delegate void DropDownButtonClickHandler(object sender, DropDownButtonClickEventArgs e);
        public delegate void MoreButtonClickHandler(object sender, MoreButtonClickEventArgs e);
        public delegate void DropDownBoxTextChangedHandler(object sender, DropDownBoxTextChangedEventArgs e);

        public delegate string CutHandler(object sender, string text);
        public delegate string CopyHandler(object sender, string text);
        public delegate string PasteHandler(object sender, string text);
        public delegate void BaseEventHandler(object sender, BaseEventArgs e);
    }
}

