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
    public class CellImage : CellBaseEdit
    {
        public CellImage()
        {
        }
  

        private ImageLayout _SizeMode = ImageLayout.Center;
        public virtual ImageLayout SizeMode
        {
            get
            {
                return this._SizeMode;
            }
            set
            {
                this._SizeMode = value;
            }
        }

        private void DrawImage(Feng.Drawing.GraphicsObject g, RectangleF bounds, Image image)
        {
            Rectangle rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(SizeMode, image, Rectangle.Round(bounds));
            g.Graphics.DrawImage(image, rect);
        }

        public override bool DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            Image image = cell.Value as Image;
            if (image != null)
            {
                DrawImage(g, cell.Rect, image);
                return true;
            }
            return false;
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
#warning 未完成
                return data;
            }
        }
 
        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridViewCell cell = sender as GridViewCell;
            if (cell == null)
                return false;
            if (!cell.Column.ReadOnly)
            {
                using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
                {
                    dlg.Filter = "(bmp,jpg,jpeg,png)|*.bmp;*.jpg;*.jpeg;*.png|*.bmp|*.bmp|*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string file = dlg.FileName;
                        Image img = Image.FromFile(file); 
                        cell.Value = img;
                        SizeF sf = img.Size;
                        //cell.ContensHeigth = sf.Height;
                        //cell.ContensWidth = sf.Width;
                    }
                }
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
 
    }

}
