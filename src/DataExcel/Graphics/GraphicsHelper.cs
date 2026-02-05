using Feng.Excel.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Drawing
{
    public class GraphicsHelper
    {
        public void DrawCell(Feng.Drawing.GraphicsObject g, Rectangle bounds, IBaseCell cell, Color forecolor)
        {
            string text = cell.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(cell.HorizontalAlignment, cell.VerticalAlignment, cell.DirectionVertical);

            bounds.Offset(1, 1);
            bounds.Inflate(-1, -1);
            Rectangle rect = bounds;
            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                if (cell.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, cell.Font, sb, rect);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, cell.Font, sb, rect, sf);

                }
            }
        }

        public static void DrawCellText(Feng.Drawing.GraphicsObject g, IBaseCell cell, Rectangle bounds, string text)
        {  
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(cell.HorizontalAlignment, cell.VerticalAlignment, cell.DirectionVertical);

            bounds.Offset(1, 1);
            bounds.Inflate(-1, -1);
            Rectangle rect = bounds;

            Color forecolor = Color.Empty;
            if (cell.Grid.FocusedCell == cell)
            {
                forecolor = cell.FocusForeColor;
            }
            if (cell.Rect.Contains (g.ClientPoint))
            {
                forecolor = cell.MouseOverForeColor;
            }

            if (g.MouseButtons == MouseButtons.Left && cell.Grid.FocusedCell == cell)
            {
                forecolor = cell.MouseDownForeColor;
            }
            if (cell.Selected)
            {
                forecolor = cell.SelectForceColor;
            }
            if (forecolor == Color.Empty)
            {
                forecolor = cell.ForeColor;
            }

            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                if (cell.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, cell.Font, sb, rect);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, cell.Font, sb, rect, sf);

                }
            }
        }
    }
}