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
using Feng.Forms.Interface;
using Feng.Forms.Views;

namespace Feng.Forms.Controls.GridControl
{
    public partial class GridView : DivView, Feng.Forms.Interface.IDataStruct, Feng.Print.IPrint, IShowCheck
    { 

        #region 绘制
        private int _BeginReFresh = 100;
        public override void BeginReFresh()
        {
            _BeginReFresh++;
        }
        private int _EndReFresh = 100;
        public override void EndReFresh()
        {
            _EndReFresh++;
            this.ReFresh();

        }

        public override void BeginReFresh(RectangleF rect)
        {
            _region.Union(rect);
            _BeginReFresh++;
        }
        public override void EndReFresh(RectangleF rect)
        {
            _EndReFresh++;
            this.RePaint(rect);
        }
        private System.Drawing.Region _region = new Region();
        public override void RePaint(RectangleF rect)
        {

            if (this._BeginReFresh == _EndReFresh)
            {
                this.Invalidate(_region);
                _region.MakeEmpty();
                _BeginReFresh = _EndReFresh = 0;
            }

        }
        public override void Invalidate()
        {

        }
 
        public override void Invalidate(Rectangle rc)
        {

        }
        public override void Invalidate(Region region)
        {

        }
        public override void ReFresh()
        {
            if (this._BeginReFresh == _EndReFresh)
            {
                this.Invalidate();
                _BeginReFresh = _EndReFresh = 0;
            }
        }

        public Rectangle ColumnHeaderRect
        {
            get
            {
                return new Rectangle(0, 0, this.Width, this.ColumnHeaderHeight);
            }
        }

        private void DrawRow()
        {
            foreach (GridViewRow row in Rows)
            {

            }
        }
        public Rectangle tempMoveRect = Rectangle.Empty;
        public virtual void DrawColumn(Feng.Drawing.GraphicsObject g)
        {
            foreach (GridViewColumn column in this.visibleColumns)
            {
                column.OnDraw(g);
            }
        }
        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (this.Width < 1)
            {
                return true;
            }
            if (this.Height < 1)
            {
                return true;
            }
            GraphicsState gs = g.Graphics.Save();
            g.Graphics.TranslateTransform(this.Left, this.Top);
            g.Graphics.SetClip(new Rectangle(0, 0, this.Width, this.Height));
            //Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, Brushes.Red, this.Rect);

            DrawColumn(g);
            if (this.ShowLines)
            {
                if (this.ShowColumnHeader)
                {
                    g.Graphics.DrawLine(PenCache.BorderGray, 0, this.ColumnHeaderHeight, this.Width, this.ColumnHeaderHeight);
                }

                if (this.ShowRowHeader)
                {
                    g.Graphics.DrawLine(PenCache.BorderGray, this.RowHeaderWidth, 0, this.RowHeaderWidth, this.Height);
                }
            }
            foreach (GridViewRow row in this.Rows)
            {
                row.OnDraw(this, g);
                foreach (GridViewColumn col in this.visibleColumns)
                {
                    GridViewCell cell = row.Cells[col];
                    if (cell != null)
                    {
                        cell.OnDraw(g);
                    }
                }
            }

            if (ShowFooter)
            {
                this.Footer.OnDraw(this, g);
                foreach (GridViewColumn col in this.visibleColumns)
                {
                    col.FooterCell.OnDraw(this, g);
                }
            }
            this.HScroll.OnDraw(g.Graphics);
            this.VScroll.OnDraw(g.Graphics);
            DrawOperation(g);
            g.Graphics.DrawRectangle(new Pen(Color.FromArgb(192, 192, 192)), 0, 0, this.Width - 1, this.Height - 1);
            g.Graphics.Restore(gs);
            return true;
        }
 
        public void DrawOperation(Feng.Drawing.GraphicsObject g)
        {

            if (this.InDesign)
            {
                g.Graphics.DrawRectangle(Pens.LightGray, this.OperationRect);
                g.Graphics.DrawImage(Images.EditButton_More, this.OperationRect);
            }
        }

        public static void DrawCellText(GridViewCell cell, Feng.Drawing.GraphicsObject g, RectangleF rect, string text)
        {
            if (cell == null)
                return;
            if (string.IsNullOrEmpty(text))
                return;
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(
                cell.Column.HorizontalAlignment, cell.Column.VerticalAlignment, cell.Column.DirectionVertical);

            Color forecolor = Color.Empty;
            if (cell.Grid.FocusedCell == cell)
            {
                forecolor = cell.Grid.FocusForeColor;
            }

       
            if (forecolor == Color.Empty)
            {
                forecolor = cell.Grid.ForeColor;
            }

            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                rect.Location = new PointF(rect.Location.X, rect.Location.Y + 2);
                if (cell.AutoMultiline)
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, cell.Grid.Font, sb, rect);
                }
                else
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,text, cell.Grid.Font, sb, rect, sf);
                }
            }
        }
        #endregion
         
    }
}

