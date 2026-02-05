#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Feng.Utils;
using Feng.Excel.Drawing;
using Feng.Drawing;

namespace Feng.Excel.Collections
{
    [Serializable]
    public class CellRangeCollection : SelectCellCollection 
    { 
        public CellRangeCollection()
        {
            
        }
 
        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            //if (this.Grid.ShowSelectBorder)
            //{
            //    g.Graphics.DrawRectangle(new Pen(this.Grid.SelectBorderColor), this.Left, this.Top, this.Width, this.Height); 
            //}

            //if (!this.Grid.ShowSelectRect)
            //{ 
            //    return false;
            //}
            Rectangle rectf = new Rectangle(this.Rect.Location, this.Rect.Size);
            if (this.BeginCell == null)
                return false;
            if (this.EndCell == null)
                return false;
            if (DataRectHelper.IsEmpty(rectf))
            {
                return false;
            }

            if (!this.Grid.InEdit)
            {
                int x = rectf.Location.X, y = rectf.Location.Y, w = rectf.Size.Width, h = rectf.Size.Height;
                Color color = Color.FromArgb(50, System.Drawing.SystemColors.AppWorkspace);
                //color = Color.Red;
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, SolidBrushCache.GetSolidBrush(color), rectf);
                DrawHelper.DrawRect(g.Graphics, rectf, this.BackColor, 3, System.Drawing.Drawing2D.DashStyle.Dash);
                DrawHelper.DrawCorssSelectRect(g.Graphics, rectf);

            } 
            else
            {
                rectf.Inflate(1, 1);
                DrawHelper.DrawRect(g.Graphics, rectf, this.BackColor, 2, System.Drawing.Drawing2D.DashStyle.DashDot);
                DrawHelper.DrawCorssSelectRect(g.Graphics, rectf);
            }
            return false;
        }
 
    }





}
