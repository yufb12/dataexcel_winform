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

namespace Feng.Forms.Skins
{
    public class ToolBarItemSkin
    {
        private static ToolBarItemSkin defaultskin = new ToolBarItemSkin();
        public static ToolBarItemSkin Default
        {
            get
            {
                return defaultskin;
            }
        }

        private Color _barbackcolor = Color.White;//
        public virtual Color BarBackColor { get { return _barbackcolor; } set { _barbackcolor = value; } }

        private Color _barbackcolor2 = Color.Empty;
        public virtual Color BarBackColor2 { get { return _barbackcolor2; } set { _barbackcolor2 = value; } }

        private Color _itembackcolor = Color.SeaShell;
        public virtual Color ItemBackColor { get { return _itembackcolor; } set { _itembackcolor = value; } }


        private Color _itemfocuscolor = Color.White;
        public virtual Color ItemFocusColor { get { return _itemfocuscolor; } set { _itemfocuscolor = value; } }

        private Color _itemmouseinbackcolor = Color.Peru;
        public virtual Color ItemMouseInBackColor { get { return _itemmouseinbackcolor; } set { _itemmouseinbackcolor = value; } }

        private Color _itemmouseinbordercolor = Color.Gold;
        public virtual Color ItemMouseInBorderColor { get { return _itemmouseinbordercolor; } set { _itemmouseinbordercolor = value; } }

        private Color _splitcolor = Color.DarkGray;
        public virtual Color SplitColor { get { return _splitcolor; } set { _splitcolor = value; } }

        private Color _forecolor = Color.Black;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(typeof(Color), "Black")]
        public Color ForeColor
        {
            get
            {
                return _forecolor;
            }
            set
            {
                _forecolor = value;
            }
        }

        private Font _font = new Font("微软雅黑", 9.0f);
        public virtual Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }

        public virtual void DrawBarBack(Graphics g, Rectangle rect)
        {
            if (BarBackColor2 == Color.Empty)
            {
                SolidBrush brush = SolidBrushCache.GetSolidBrush(this.BarBackColor);
                Feng.Drawing.GraphicsHelper.FillRectangle(g, brush, rect);
            }
            else
            {
                Feng.Drawing.GraphicsHelper.FillRectangleLinearGradient(g, BarBackColor, BarBackColor2, rect, LinearGradientMode.Vertical);
            }
        }

        public virtual void DrawBack(Graphics g, Rectangle rect, bool mousein,bool focus)
        {
            if (mousein)
            {
                SolidBrush brush = SolidBrushCache.GetSolidBrush(ItemMouseInBackColor);
                Feng.Drawing.GraphicsHelper.FillRectangle(g, brush, rect);
                Pen pen = PenCache.GetPen(ItemMouseInBorderColor);
                g.DrawRectangle(pen, rect);
            }
            else if (focus)
            {

                Feng.Drawing.GraphicsHelper.FillRectangleLinearGradient(g, ItemFocusColor, BarBackColor, rect, LinearGradientMode.Vertical);
                SolidBrush brush = SolidBrushCache.GetSolidBrush(Color.White);
                Feng.Drawing.GraphicsHelper.FillRectangle(g, brush, rect);
            }
        }

        public virtual void DrawBack(Graphics g, Rectangle rect, bool mousein)
        {
            Color color = Color.LightGray  ;
            if (mousein)
            {
                color = ItemMouseInBackColor;
            }
            SolidBrush brush = SolidBrushCache.GetSolidBrush(color);
            Feng.Drawing.GraphicsHelper.FillRectangle(g, brush, rect);
 
        }

        public virtual void DrawText(Graphics g, Rectangle rect, Feng.Forms.Controls.ToolBarItem item)
        {

            SolidBrush brush = SolidBrushCache.GetSolidBrush(ForeColor);
            g.DrawString(item.Text, Font, brush, rect, StringFormatCache.GetStringFormatAlignLeftNoWrap());

        }

        public virtual void DrawEmptyVSplit(Graphics g, Rectangle rect)
        {
            g.DrawLine(PenCache.GetPen(SplitColor), rect.Left + rect.Width / 2, rect.Top, rect.Left + rect.Width / 2, rect.Bottom);
        }

        public virtual void SizeItem(Graphics g, Feng.Forms.Controls.ToolBarItem item)
        {
            if (item != null)
            {
                int imagewidth = 0;
                if (item.ShowImage)
                {
                    if (item.Image != null)
                    {
                        imagewidth = item.ToolBar.ImageSize;
                    }
                }
                if (item.ShowClose)
                {
                    imagewidth = imagewidth + item.CloseImage.Width * 3;
                }
                if (item.ShowText)
                {
                    imagewidth = imagewidth + Feng.Drawing.GraphicsHelper.Sizeof(item.Text+"--", Font, g, TextFormatFlags.Default).Width + item.ToolBar.PaddingLeft;
                }
                item.Width = imagewidth;
            }
        }

        public virtual void DrawImage(Graphics g, Rectangle rect, Feng.Forms.Controls.ToolBarItem item)
        {
            if (item.Image != null)
            {
                g.DrawImage(item.Image, rect);
            }
        }
        public virtual void DrawCloseImage(Graphics g, Rectangle rect, Feng.Forms.Controls.ToolBarItem item)
        {
            if (item.Image != null)
            {
                g.DrawImage(item.CloseImage, rect);
            }
        }
        public virtual void DrawHeader(Graphics g, Rectangle rect)
        {
            Pen pen = PenCache.GetPen("ToolBarItemSkin_DrawHeader", SplitColor);
            pen.DashStyle = DashStyle.Dash;
            g.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom - 2);
            g.DrawLine(pen, rect.Left + rect.Width / 2, rect.Top + 2, rect.Left + rect.Width / 2, rect.Bottom);
            g.DrawLine(pen, rect.Left + rect.Width, rect.Top, rect.Left + rect.Width, rect.Bottom - 2);
        }

        public virtual void DrawFooter(Graphics g, Rectangle rect)
        {
            rect.Y = rect.Bottom - 7;
            rect.X = rect.X + 1;
            rect.Height = 6;
            rect.Width = 10; 
            g.DrawLine(PenCache.GetPen(SplitColor), rect.Left, rect.Top - 3, rect.Right, rect.Top - 3);
            SolidBrush brush = SolidBrushCache.GetSolidBrush(SplitColor);
            GraphicsPath path = Feng.Drawing.GraphicsHelper.GetTriangle(rect, Drawing.Orientation.Top);
            g.FillPath(brush, path);
        }

        public virtual void DrawEmptyHSplit(Graphics g, Rectangle rect)
        {
            g.DrawLine(PenCache.GetPen(SplitColor), rect.Left, rect.Top + rect.Height / 2, rect.Right, rect.Top + rect.Height / 2);
        }
    }

}
