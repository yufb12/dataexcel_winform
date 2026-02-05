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

namespace Feng.Forms.Views
{
    public class ReSizeView: DivView
    {
        public ReSizeView()
        {

        }

        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return base.OnMouseDown(sender, e, ve);
        }
        public virtual bool SetDataExcelMouseDown(MouseEventArgs mes)
        {
            Point pt = mes.Location;
            _MouseDownPoint = System.Windows.Forms.Control.MousePosition;
            _MouseDownsize = new Size(this.Width, this.Height);

            bool result = false;
            if (this.TopLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.TopLeft;
                result = true; 
            }
            else if (this.TopRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.TopRight;
                result = true;  
            }
            else if (this.BottomLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.BoomLeft;
                result = true; 
            }
            else if (this.BottomRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.BoomRight;
                result = true; 
            }
            else if (this.MidTop.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidTop;
                result = true; 
            }
            else if (this.MidBottom.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidBoom;
                result = true; 
            }
            else if (this.MidLeft.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidLeft;
                result = true;  
            }
            else if (this.MidRight.Contains(pt))
            {
                _SizeChangMode = SizeChangMode.MidRight;
                result = true; 
            } 
            return result;
        }
        public virtual void ChangedSize(MouseEventArgs e)
        {
            Point location = System.Windows.Forms.Control.MousePosition;// e.Location;
            Size sf = new Size(location.X - MouseDownPoint.X, location.Y - MouseDownPoint.Y);

            switch (this.SizeChangMode)
            {
                case SizeChangMode.Null:
                    break;
                case SizeChangMode.TopLeft:
                    this.Width = this.MouseDownSize.Width - sf.Width;
                    this.Height = this.MouseDownSize.Height - sf.Height;
                    this.Top = location.Y;// this.MouseDownPoint.Y + sf.Height;
                    this.Left = location.X;// this.MouseDownPoint.X + sf.Width;

                    break;
                case SizeChangMode.TopRight:
                    this.Width = this.MouseDownSize.Width + sf.Width;
                    this.Height = this.MouseDownSize.Height - sf.Height;
                    this.Top = location.Y;// this.MouseDownPoint.Y + sf.Height;

                    break;
                case SizeChangMode.MidLeft:
                    this.Width = this.MouseDownSize.Width - sf.Width;
                    this.Left = this.MouseDownPoint.X + sf.Width;
                    break;
                case SizeChangMode.MidRight:
                    this.Width = this.MouseDownSize.Width + sf.Width;
                    break;
                case SizeChangMode.BoomLeft:
                    this.Width = this.MouseDownSize.Width - sf.Width;
                    this.Height = this.MouseDownSize.Height + sf.Height;
                    this.Left = this.MouseDownPoint.X + sf.Width;

                    break;
                case SizeChangMode.BoomRight:

                    this.Width = this.MouseDownSize.Width + sf.Width;
                    this.Height = this.MouseDownSize.Height + sf.Height;
                    break;
                case SizeChangMode.MidTop:
                    this.Height = this.MouseDownSize.Height - sf.Height;
                    this.Top = this.MouseDownPoint.Y + sf.Height;
                    break;
                case SizeChangMode.MidBoom:
                    this.Height = this.MouseDownSize.Height + sf.Height;
                    break;
                default:
                    break;
            }
        }
        public override DataStruct Data { get { return new DataStruct(); } }
 
        public virtual void OnDrawMoveBorder(Feng.Drawing.GraphicsObject g)
        {
            Rectangle rectf = this.Rect;
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddRectangle(rectf);
                rectf.Inflate(this.SelectBorderWidth * -1, this.SelectBorderWidth * -1);
                path.AddRectangle(rectf);
                using (System.Drawing.Drawing2D.HatchBrush hb = new HatchBrush(HatchStyle.Percent20, Color.Gray, Color.White))
                {
                    g.Graphics.FillPath(hb, path);
                }
            }
            Brush brush = Brushes.Blue;

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.TopLeft);

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.MidTop);

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.MidLeft);

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.TopRight);

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.MidRight);

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.BottomLeft);

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.MidBottom);

            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, brush, this.BottomRight);

        }

        private Point _MouseDownPoint = Point.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point MouseDownPoint
        {
            get { return _MouseDownPoint; }
            set { _MouseDownPoint = value; }
        }

        private Size _MouseDownsize = Size.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size MouseDownSize
        {
            get { return _MouseDownsize; }
            set { _MouseDownsize = value; }
        }

        private SizeChangMode _SizeChangMode = SizeChangMode.Null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SizeChangMode SizeChangMode
        {
            get { return _SizeChangMode; }
            set { _SizeChangMode = value; }
        }

 
        [Browsable(false)]
        public Rectangle TopLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left, rectf.Top, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public Rectangle TopRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right - _SelectBorderWidth, rectf.Top, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public Rectangle BottomLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left, rectf.Bottom - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;

            }
        }
        [Browsable(false)]
        public Rectangle BottomRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right - _SelectBorderWidth, rectf.Bottom - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;


            }
        }
        [Browsable(false)]
        public Rectangle MidTop
        {
            get
            {
                Rectangle rectf = this.Rect;


                rectf = new Rectangle(
                 Feng.Utils.ConvertHelper.ToInt32(rectf.Left + rectf.Width / 2 - _SelectBorderWidth / 2),
                    rectf.Top,
                    _SelectBorderWidth,
                    _SelectBorderWidth);

                return rectf;


            }
        }
        [Browsable(false)]
        public Rectangle MidBottom
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left + rectf.Width / 2 - _SelectBorderWidth / 2, rectf.Bottom - _SelectBorderWidth, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }
        [Browsable(false)]
        public Rectangle MidLeft
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Left, rectf.Top + rectf.Height / 2 - _SelectBorderWidth / 2, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }
        [Browsable(false)]
        public Rectangle MidRight
        {
            get
            {
                Rectangle rectf = this.Rect;

                rectf = new Rectangle(rectf.Right - _SelectBorderWidth, rectf.Top + rectf.Height / 2 - _SelectBorderWidth / 2, _SelectBorderWidth, _SelectBorderWidth);

                return rectf;
            }
        }

        private System.Drawing.Color _SelectBorderColor = Color.BlueViolet;
        [DefaultValue(typeof(Color), "BlueViolet")]
        public System.Drawing.Color SelectBorderColor
        {
            get { return _SelectBorderColor; }
            set { _SelectBorderColor = value; }
        }
        private int _SelectBorderWidth = 6;
        [Browsable(true)]
        [DefaultValue(6)]
        public virtual int SelectBorderWidth
        {
            get { return _SelectBorderWidth; }
            set { _SelectBorderWidth = value; }
        }
    }
}

