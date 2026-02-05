using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using Feng.Utils;
using Feng.Print;
using Feng.Data;

namespace Feng.Forms.Controls.GridControl.Edits
{ 
    [Serializable]
    public class CellLinkLabel : CellBaseEdit
    {
        public CellLinkLabel() 
        {  
        }
       
        private string _text = string.Empty;

        public virtual string Text
        {
            get {
     
                return _text;
            }
            set {
                _text = value;
            }
        }

        private string _url = string.Empty;
 
        public virtual string Url
        {
            get
            {

                return _url;
            }
            set
            {
                _url = value;
            }
        }

        public override bool DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            string text = this.Text;
            if (string.IsNullOrEmpty(text))
            {
                text = cell.Text;
            }
            DrawCell(cell, g, cell.Rect, text);
            return true;
        }

        private void DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g, RectangleF bounds, string text)
        {  
            RectangleF rect = bounds;
            GridView.DrawCellText(cell, g, rect, text);
            
        }

        public override bool DrawCellBack(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }
 

        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = t.FullName,
                    Name = t.Name,
                };
 
                return data;
            }
        }

        public override bool OnMouseMove(object sender, MouseEventArgs e)
        {
            GridViewCell cell = sender as GridViewCell;
            if (cell == null)
                return false;
            cell.Grid.BeginSetCursor(Cursors.Hand);
            return false;
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e)
        {
            GridViewCell cell = sender as GridViewCell;
            if (cell == null)
                return false;
            if (!cell.Rect.Contains(e.Location))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.Url))
            {
                return false;
            }

            try
            {
                System.Diagnostics.Process.Start(this.Url);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            return false;
        }


        public override bool OnKeyDown(object sender, KeyEventArgs e)
        {
            GridViewCell cell = sender as GridViewCell;
            if (cell == null)
                return false;
            if (e.Alt || e.Control || e.Shift)
            {

            }
            else
            {
                if (e.KeyData == Keys.Right)
                {
                    cell.Grid.MoveFocusedCellToRightCell();
                }
                if (e.KeyData == Keys.Left)
                {
                    cell.Grid.MoveFocusedCellToLeftCell();
                }
            }
            return false;
        }

        public override bool PrintCell(GridViewCell cell, PrintArgs e, RectangleF rect)
        {
            Feng.Drawing.GraphicsObject gob =  e.Graphic;
            DrawCell(cell, gob, cell.Rect, this.Text);
            return true;
        }

        public override bool PrintValue(GridViewCell cell, PrintArgs e, RectangleF rect, object value)
        {
            if (value != null)
            {
                Feng.Drawing.GraphicsObject gob =  e.Graphic;
                DrawCell(cell, gob, cell.Rect, value.ToString());
            }
            return true;
        }

 
    }

}
