using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using Feng.Enums;


namespace Feng.Excel.Drawing
{
    [Serializable]
    public class DrawHelper
    {



        public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor)
        {
            System.Windows.Forms.TextRenderer.DrawText(dc, text, font, pt, foreColor);
        }

        public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor)
        {
            System.Windows.Forms.TextRenderer.DrawText(dc, text, font, bounds, foreColor);
        }

        public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor)
        {
            System.Windows.Forms.TextRenderer.DrawText(dc, text, font, pt, foreColor, backColor);
        }

        public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags)
        {
            System.Windows.Forms.TextRenderer.DrawText(dc, text, font, pt, foreColor, flags);
        }

        public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor)
        {
            System.Windows.Forms.TextRenderer.DrawText(dc, text, font, bounds, foreColor, backColor);
        }

        public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags)
        {
            System.Windows.Forms.TextRenderer.DrawText(dc, text, font, bounds, foreColor, flags);
        }

        public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags)
        {
            System.Windows.Forms.TextRenderer.DrawText(dc, text, font, pt, foreColor, backColor, flags);
        }

        public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags)
        {
            System.Windows.Forms.TextRenderer.DrawText(dc, text, font, bounds, foreColor, backColor, flags);
        }

        public static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state)
        {
            CheckBoxRenderer.DrawCheckBox(g, glyphLocation, state);
        }

        public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, bool focused, CheckBoxState state)
        {
            CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, focused, state);
        }

        public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, bool focused, CheckBoxState state)
        {
            CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, flags, focused, state);
        }

        public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
        {
            CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, image, imageBounds, focused, state);
        }

        public static void DrawCheckBox(Graphics g, Point glyphLocation, Rectangle textBounds, string checkBoxText, Font font, TextFormatFlags flags, Image image, Rectangle imageBounds, bool focused, CheckBoxState state)
        {
            CheckBoxRenderer.DrawCheckBox(g, glyphLocation, textBounds, checkBoxText, font, flags, image, imageBounds, focused, state);
        }

        public static void DrawCheckBox(Graphics g, Rectangle rect, string text, Font font, bool focused, CheckBoxState state)
        {
            CheckBoxRenderer.DrawCheckBox(g, new Point(rect.Left, rect.Top + (int)(rect.Height / 2)), rect, text, font, focused, state);
        }

        public static void DrawParentBackground(Graphics g, Rectangle bounds, Control childControl)
        {
            CheckBoxRenderer.DrawParentBackground(g, bounds, childControl);
        }

        public static void DrawDropDownButton(Graphics g, Rectangle rect, System.Windows.Forms.VisualStyles.ComboBoxState state)
        {
            System.Windows.Forms.ComboBoxRenderer.DrawDropDownButton(g, rect, state);
        }

        public static void DrawTextBox(Graphics g, Rectangle bounds, ComboBoxState state)
        {
            System.Windows.Forms.ComboBoxRenderer.DrawTextBox(g, bounds, state);
        }

        public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, ComboBoxState state)
        {
            System.Windows.Forms.ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, state);
        }

        public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, ComboBoxState state)
        {
            System.Windows.Forms.ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, textBounds, state);
        }

        public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, TextFormatFlags flags, ComboBoxState state)
        {
            System.Windows.Forms.ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, flags, state);
        }

        public static void DrawTextBox(Graphics g, Rectangle bounds, string comboBoxText, Font font, Rectangle textBounds, TextFormatFlags flags, ComboBoxState state)
        {
            System.Windows.Forms.ComboBoxRenderer.DrawTextBox(g, bounds, comboBoxText, font, textBounds, flags, state);
        }

        public static void DrawRect(Graphics g, Rectangle rect, Pen p)
        {
            g.DrawRectangle(p, rect);
        }

        public static void DrawRect(Graphics g, Rectangle rect, Color c)
        {
            using (Pen p = new Pen(c))
            {
                g.DrawRectangle(p, rect);
            }
        }

        public static void DrawRect(Graphics g, Rectangle rect, Pen p, float width)
        {
            p.Width = width;
            g.DrawRectangle(p, rect);
        }

        public static void DrawRect(Graphics g, Rectangle rect, Color c, float width)
        {
            using (Pen p = new Pen(c))
            {
                p.Width = width;
                g.DrawRectangle(p, rect);
            }
        }

        public static void DrawRect(Graphics g, Rectangle rect, Pen p, DashStyle DashStyle)
        {
            p.DashStyle = DashStyle;
            g.DrawRectangle(p, rect);
        }

        public static void DrawRect(Graphics g, Rectangle rect, Color c, DashStyle DashStyle)
        {
            using (Pen p = new Pen(c))
            {
                p.DashStyle = DashStyle;
                g.DrawRectangle(p, rect);
            }
        }

        public static void DrawRect(Graphics g, Rectangle rect, Pen p, float width, DashStyle DashStyle)
        {
            p.Width = width;
            p.DashStyle = DashStyle;
            g.DrawRectangle(p, rect);
        }

        public static void DrawRect(Graphics g, Rectangle rect, Color c, float width, DashStyle DashStyle)
        {
            using (Pen p = new Pen(c))
            {
                p.Width = width;
                p.DashStyle = DashStyle;
                g.DrawRectangle(p, rect);
            }
        }

        public static void DrawRect(Graphics g, RectangleF rect, Color c, float width, DashStyle DashStyle)
        {
            using (Pen p = new Pen(c))
            {
                p.Width = width;
                p.DashStyle = DashStyle;
                g.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
            }

        }

        public static void DrawRect(Graphics g, RectangleF rect, Pen p, float width, DashStyle DashStyle)
        {
            p.Width = width;
            p.DashStyle = DashStyle;
            g.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static void DrawRect(Graphics g, RectangleF rect, Pen p, DashStyle DashStyle)
        {
            p.DashStyle = DashStyle;
            g.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static void DrawRect(Graphics g, RectangleF  rect, Color c, DashStyle DashStyle)
        {
            using (Pen p = new Pen(c))
            {
                p.DashStyle = DashStyle;
                g.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
            }

        }

        public static void DrawRect(Graphics g, RectangleF rect, Color c, float width)
        {
            using (Pen p = new Pen(c))
            {
                p.Width = width;
                g.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
            }

        }

        public static void DrawRect(Graphics g, RectangleF rect, Pen p, float width)
        {
            p.Width = width;
            g.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static void DrawRect(Graphics g, RectangleF rect, Color c)
        {
            using (Pen p = new Pen(c))
            {
                g.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
            }

        }

        public static void DrawRect(Graphics g, RectangleF rect, Pen p)
        {
            g.DrawRectangle(p, rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static void DrawRectLine(Graphics g, Rectangle rect, enumDrawRectLineMode mo, Pen p)
        {
            if ((mo & enumDrawRectLineMode.CenterHorzLine) == enumDrawRectLineMode.CenterHorzLine)
            {
                Point p1 = new Point(rect.Location.X, rect.Top + rect.Height / 2);
                Point p2 = new Point(rect.Location.X + rect.Width, rect.Top + rect.Height / 2);
                g.DrawLine(p, p1, p2);
            }
            if ((mo & enumDrawRectLineMode.CenterVerzLine) == enumDrawRectLineMode.CenterVerzLine)
            {
                Point p1 = new Point(rect.Location.X + rect.Width / 2, rect.Top);
                Point p2 = new Point(rect.Location.X + rect.Width / 2, rect.Bottom);
                g.DrawLine(p, p1, p2);
            }
            if ((mo & enumDrawRectLineMode.TopLeftToBoomRight) == enumDrawRectLineMode.TopLeftToBoomRight)
            {
                Point p1 = new Point(rect.Location.X, rect.Top);
                Point p2 = new Point(rect.Location.X + rect.Width, rect.Bottom);
                g.DrawLine(p, p1, p2);
            }
            if ((mo & enumDrawRectLineMode.BoomLeftToTopRight) == enumDrawRectLineMode.BoomLeftToTopRight)
            {
                Point p1 = new Point(rect.Location.X + rect.Width, rect.Bottom);
                Point p2 = new Point(rect.Location.X, rect.Top);
                g.DrawLine(p, p1, p2);
            }
        }

        public static void DrawRectLine(Graphics g, Rectangle rect, enumDrawRectLineMode mo, Color c, float width)
        {
            using (Pen p = new Pen(c))
            {
                p.Width = width;
                DrawRectLine(g, rect, mo, p);
            }

        }

        public static void DrawRectLine(Graphics g, Rectangle rect, enumDrawRectLineMode mo, Color c, float width, DashStyle DashStyle)
        {
            using (Pen p = new Pen(c))
            {
                p.Width = width;
                p.DashStyle = DashStyle;
                DrawRectLine(g, rect, mo, p);
            }

        }

        public static void DrawCorssSelectRect(Graphics g, Rectangle rectf)
        {
            Rectangle rect = new Rectangle();
            rect.Width = 6;
            rect.Height = 6;
            rect.Location = new Point(rectf.Right - 3, rectf.Bottom - 3);
            Feng.Drawing.GraphicsHelper.FillRectangle(g,Brushes.Black, rect);
            DrawHelper.DrawRectLine(g, rect, enumDrawRectLineMode.CenterHorzLine | enumDrawRectLineMode.CenterVerzLine, Color.Gray, 0.03f);
            g.DrawRectangle(Pens.White, rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static void DrawCheckBox(Graphics g, Rectangle rect, System.Windows.Forms.VisualStyles.CheckBoxState bolchecked)
        {
            System.Windows.Forms.CheckBoxRenderer.DrawCheckBox(g, rect.Location, bolchecked);
        }

        public static void DrawTextBox(Graphics g, Rectangle rect, System.Windows.Forms.VisualStyles.TextBoxState state)
        {
            System.Windows.Forms.TextBoxRenderer.DrawTextBox(g, rect, state);
        }

        public static void DrawRadioBox(Graphics g, Rectangle rect, RadioButtonState state)
        {
            System.Windows.Forms.RadioButtonRenderer.DrawRadioButton(g, rect.Location, state);
        }
    }
}
