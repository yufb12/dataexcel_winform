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

namespace Feng.Forms.Views
{
    public class CheckView 
    {
        private CheckStyle style = null;
        private CheckModel model = null;
        public virtual bool OnDraw(Feng.Drawing.GraphicsObject g, RectangleF rect, IForeColor foreColor,IFont font,IChecked check, IText text)
        {
            if (style == null)
            {
                style = new CheckStyle();
            }
            style.Font = font.Font;
            style.ForeColor = foreColor.ForeColor;
            style.StringFormat = StringFormat.GenericDefault;

            if (model == null)
            {
                model = new  CheckModel();
            }
            model.Text = text.Text;
            model.Checked = check.Checked;
            CheckSkin.Default.Draw(g, rect, style, model);
            return false;
        }

        public virtual bool OnDraw(Feng.Drawing.GraphicsObject g, RectangleF rect, CheckStyle style, CheckModel model)
        { 
            CheckSkin.Default.Draw(g, rect, style, model);
            return false;
        }

        public virtual bool OnMouseDown(object sender, MouseEventArgs e,IRect bounds,Point pt)
        {
            RectangleF rect = new RectangleF(new PointF(bounds.Left + 2, bounds.Top + (bounds.Height / 2 - 13 / 2 - 1)),
              new Size(13, 13));
            if (rect.Contains(pt))
            {
                return true;
            }
            return false;
        }
    }

    public class CheckSkin
    {
        private CheckSkin()
        {
        }
        private static CheckSkin checkSkin = null;
        public static CheckSkin Default
        {
            get
            {
                if (checkSkin == null)
                {
                    checkSkin = new CheckSkin();
                }
                return checkSkin;
            }
        }
        public void Draw(Feng.Drawing.GraphicsObject g, RectangleF rect,CheckStyle style, CheckModel model)
        {
            GraphicsHelper.DrawCheckBox(g.Graphics, rect, model.Checked ? 1 : 0, model.Text, StringFormat.GenericDefault, style.ForeColor, style.Font);

        }
    }


    public class CheckStyle
    {
        public StringFormat StringFormat { get; set; }

        public Color ForeColor { get; set; }

        public Font Font { get; set; }
    }
    public class CheckModel
    {
        public virtual bool Checked { get; set; }

        public string Text { get; set; }
    }
}

