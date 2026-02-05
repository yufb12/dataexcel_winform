using Feng.Data;
using Feng.Forms.Controls.Designer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class TextView : DivView
    {
        public TextView()
        {

        }
         
        public override Font Font 
        {
            get {
                if (base.Font == null)
                {
                    if (this.Control != null)
                    {
                        return this.Control.Font;
                    }
                }
                return base.Font; }
            set {
                base.Font = value;
            }
        }

        private bool _AutoMultiline = false;
        /// <summary>
        /// 是否自动绘制多行。True时绘制多行，False时不绘制多行。
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool AutoMultiline
        {
            get { return this._AutoMultiline; }

            set { this._AutoMultiline = value; }
        }

        private StringAlignment _HorizontalAlignment = StringAlignment.Near;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual StringAlignment HorizontalAlignment
        {
            get
            {
                return _HorizontalAlignment;
            }
            set
            {
                _HorizontalAlignment = value;
            }
        }

        private StringAlignment _VerticalAlignment = StringAlignment.Center;
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual StringAlignment VerticalAlignment
        {
            get
            {
                return _VerticalAlignment;
            }
            set
            {
                _VerticalAlignment = value;
            }
        }
  
        private int _caretindex = 0;
        public int SelectionEnd { get; set; }
        private bool _DirectionVertical = false;

        public override string Text
        {
            get
            {
                return this.strsb.ToString();
            }
            set
            {
                this.BeginReFresh();
                this.TextRegion = null;
                this.SelectionStart = 0;
                this.SelectionEnd = 0;
                this.strsb.Length = 0;
                this.strsb.Append(value);
                this.EndReFresh();
            }
        }
        StringBuilder strsb = new StringBuilder(); 
        public virtual StringBuilder StringBuilder 
        {
            get { return strsb; }
            set { strsb = value; }
        }
        [Browsable(true)]
        [Category(CategorySetting.Design)]
        public virtual bool DirectionVertical
        {
            get
            {
                return _DirectionVertical;
            }
            set
            {
                _DirectionVertical = value;
            }
        }
        public virtual int SelectionStart
        {
            get
            {
                return _caretindex;
            }
            set
            {
                _caretindex = value;
            }
        }
        private List<RectangleF> TextRegion = null;
        private int TextHeight = 0;
        public void ShowCaret()
        {
            Rectangle rect = GetCaretRect();
            if (TextHeight < rect.Height)
            {
                TextHeight = rect.Height;
            }
            Point pt = rect.Location;
            if (this.Control != null)
            {
                this.Control.ShowCaret(TextHeight, pt.X, pt.Y);
            }
        }
 
        public virtual string GetSelectText()
        {
            int min = System.Math.Min(SelectionStart, SelectionEnd);
            int max = System.Math.Max(SelectionStart, SelectionEnd);
            if (max <this.Text .Length )
            {
                return Text.Substring(min, max - min);
            }
            return string.Empty;
        }

        private Point downpoint = Point.Empty;
        private Rectangle GetCaretRect()
        {
            if (TextRegion != null)
            {
                RectangleF rectf = RectangleF.Empty;
                if (TextRegion.Count > 0)
                {
                    if (SelectionStart < 0)
                    {
                        SelectionStart = 0;
                    }
                    if (SelectionStart < TextRegion.Count)
                    {
                        rectf = (TextRegion[SelectionStart]);

                    }
                    else
                    {
                        RectangleF rf = TextRegion[TextRegion.Count - 1];
                        rectf = new RectangleF(rf.Right, rf.Top, rf.Width, rf.Height);
                    }
                    return Rectangle.Round(rectf);
                }
            }
            return Rectangle.Empty;
        }
        private int GetIndex(Point pt)
        {
            if (TextRegion != null)
            {
                for (int i = 0; i < TextRegion.Count; i++)
                {
                    RectangleF rect = TextRegion[i];
                    rect.Offset(rect.Width / 2 * -1, 0);
                    if (rect.Contains(pt))
                    {
                        return i;
                    }
                    rect.Offset(rect.Width, 0);
                    if (rect.Contains(pt))
                    {
                        return i + 1;
                    }
                }
                for (int i = 0; i < TextRegion.Count; i++)
                {
                    if (TextRegion[i].Top > pt.Y)
                    {
                        return 0;
                    }
                    break;
                }
                for (int i = 0; i < TextRegion.Count; i++)
                {
                    if (TextRegion[TextRegion.Count-1].Bottom < pt.Y)
                    {
                        return TextRegion.Count;
                    }
                    break;
                }
            }
            return -1;
        }
        private bool MoveCaretIndex(Point pt)
        {
            bool res = false;
            if (TextRegion != null)
            {
                if (strsb.Length == 0)
                {
                    this.SelectionStart = 0;
                    return true;
                }
                int lastindex = -1;
                Rectangle lastrc = Rectangle.Empty;
                for (int i = 0; i < TextRegion.Count; i++)
                {
                    RectangleF rect = TextRegion[i];
                    Rectangle rc = Rectangle.Round(rect);
                    if (lastrc == Rectangle.Empty)
                    {
                        lastrc = rc;
                    }
                    else
                    {
                        if (lastrc.Top != rc.Top)
                        {
                            lastrc.Width = this.Right - lastrc.Right;
                            if (lastrc.Contains(pt))
                            {
                                SelectionStart = lastindex;
                                this.SelectionStart = SelectionStart;
                                return true;
                            }
                        }
                    }
                    rc = Rectangle.Round(rect);
                    lastrc = rc;
                    rc.X = rc.X + rc.Width / 2 * -1;
                    lastindex = i;
                    if (rc.Contains(pt))
                    {
                        SelectionStart = i;
                        this.SelectionStart = SelectionStart;
                        return true;
                    }

                    rc.X = rc.X + rc.Width;
                    if (rc.Contains(pt))
                    {
                        SelectionStart = i + 1;
                        this.SelectionStart = SelectionStart;
                        return true;
                    }
                    if (i == TextRegion.Count - 1)
                    {
                        lastrc.Width = this.Right - lastrc.Right;
                        if (lastrc.Contains(pt))
                        {
                            SelectionStart = lastindex + 1;
                            this.SelectionStart = SelectionStart;
                            return true;
                        }
                    }
                }
            }
            return res;
        }

        public void GetTextSize()
        {
            Graphics g = this.GetGraphics();
            if (g == null)
                return;
            string text = this.strsb.ToString();
            string mtext = text;
            if (text.Length < 1)
            {
                mtext = "|";
            }
            else if (text[text.Length - 1] == '\n')
            {
                mtext = text + "|";
            }
            g.ResetClip();
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(this.HorizontalAlignment, this.VerticalAlignment, false);
            SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(this.ForeColor);
            SizeF sizef = SizeF.Empty;
            Rectangle rect = Rectangle.Empty;
            List<RectangleF> list = null;
            if (this.AutoMultiline)
            {
                sizef = g.MeasureString(mtext, this.Font, this.Width);
                rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                if (sizef.Height > this.Height)
                {
                    rect.Height = this.Height - this.Top;
                    sf.LineAlignment = StringAlignment.Near;
                }
                rect.Offset(1, 1);
                rect.Inflate(-1, -1);
                list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, this.Font, sf, rect);
            }
            else
            {
                sf.FormatFlags = StringFormatFlags.NoWrap;
                sizef = g.MeasureString(mtext, this.Font, Point.Empty, sf);
                rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                if (sizef.Height > this.Height)
                {
                    rect.Height = this.Height - this.Top;
                    sf.LineAlignment = StringAlignment.Near;
                }
                if (sizef.Width > this.Width)
                {
                    rect.Width = this.Width - this.Left;
                    sf.Alignment = StringAlignment.Near;
                }
                rect.Offset(1, 1);
                rect.Inflate(-1, -1);
                list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, this.Font, sf, rect);
            }
            TextRegion = list;
        }
        public void GetCellCharBounds()
        {
            Graphics g = this.GetGraphics();
            if (g == null)
                return;
            string text = this.strsb.ToString();
            string mtext = text;
            if (text.Length < 1)
            {
                mtext = "|";
            }
            else if (text[text.Length - 1] == '\n')
            {
                mtext = text + "|";
            }
            g.ResetClip();
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(this.HorizontalAlignment, this.VerticalAlignment, false);
            SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(this.ForeColor);
            SizeF sizef = SizeF.Empty;
            Rectangle rect = this.Rect;
            List<RectangleF> list = null;
            if (this.AutoMultiline)
            {
                sizef = g.MeasureString(mtext, this.Font, this.Width);
                rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                if (sizef.Height > this.Height)
                {
                    rect.Height = this.Height - this.Top;
                    sf.LineAlignment = StringAlignment.Near;
                }
                rect.Offset(1, 1);
                rect.Inflate(-1, -1);
                list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, this.Font, sf, rect);
            }
            else
            {
                sf.FormatFlags = StringFormatFlags.NoWrap;
                sizef = g.MeasureString(mtext, this.Font, Point.Empty, sf);
                rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                if (sizef.Height > this.Height)
                {
                    rect.Height = this.Height - this.Top;
                    sf.LineAlignment = StringAlignment.Near;
                }
                if (sizef.Width > this.Width)
                {
                    rect.Width = this.Width - this.Left;
                    sf.Alignment = StringAlignment.Near;
                }
                //rect.Offset(1, 1);
                //rect.Inflate(-1, -1);
                list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, this.Font, sf, rect);
            }
            TextRegion = list;

        }
        public Point GetColumn(int index)
        {
            int row = 0;
            int column = 0;
            for (int i = 0; i < index; i++)
            {
                if (i >= strsb.Length)
                {
                    break;
                }
                if (strsb[i] == '\n')
                {
                    column = 0;
                    row++;
                }
                else
                {
                    column++;
                }
            }
            return new Point() { X = row, Y = column };
        }
        public int GetUpIndex(Point pt)
        {
            int row = 0;
            int index = 0;
            for (int i = 0; i < strsb.Length; i++)
            {
                if (i >= strsb.Length)
                {
                    break;
                }
                if (row == pt.X - 1)
                {
                    int column = 0;
                    for (index = i; index < strsb.Length; index++)
                    {
                        if (strsb[i] == '\n')
                        {
                            return index;
                        }
                        if (column >= pt.Y)
                        {
                            return index;
                        }
                        column++;
                    }
                    return -1;
                }
                if (strsb[i] == '\n')
                {
                    row++;

                }
            }
            return -1;
        }
        public int GetDownIndex(Point pt)
        {
            int row = 0;
            int index = 0;
            for (int i = 0; i < strsb.Length; i++)
            {
                if (i >= strsb.Length)
                {
                    break;
                }
                if (row == pt.X + 1)
                {
                    int column = 0;
                    for (index = i; index < strsb.Length; index++)
                    {
                        if (strsb[i] == '\n')
                        {
                            return index;
                        }
                        if (column >= pt.Y)
                        {
                            return index;
                        }
                        column++;
                    }
                    return -1;
                }
                if (strsb[i] == '\n')
                {
                    row++;

                }
            }
            return -1;
        }
        public virtual bool MoveUp()
        {
            bool res = false;
            if (this.strsb.Length > 0)
            {
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {
                    if (this.SelectionEnd < 0)
                    {
                        this.SelectionEnd = this.SelectionStart;
                    }
                    int index = this.SelectionEnd;
                    Point pt = GetColumn(index);
                    index = GetUpIndex(pt);
                    if (index >= 0)
                    {
                        this.SelectionEnd = index;
                        res = true;
                    }
                }
                else
                {
                    this.SelectionEnd = -1;
                    int index = this.SelectionStart;
                    Point pt = GetColumn(index);
                    index = GetUpIndex(pt);
                    if (index >= 0)
                    {
                        this.SelectionStart = index;
                        res = true;
                    }
                }
            }
            return res;
        }
        public virtual bool MoveDown()
        {
            bool res = false;
            if (this.strsb.Length > 0)
            {
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {
                    if (this.SelectionEnd < 0)
                    {
                        this.SelectionEnd = this.SelectionStart;
                    }
                    int index = this.SelectionEnd;
                    Point pt = GetColumn(index);
                    index = GetDownIndex(pt);
                    if (index >= 0)
                    {
                        this.SelectionEnd = index;
                        res = true;
                    }
                }
                else
                {
                    this.SelectionEnd = -1;
                    int index = this.SelectionStart;
                    Point pt = GetColumn(index);
                    index = GetDownIndex(pt);
                    if (index >= 0)
                    {
                        this.SelectionStart = index;
                        res = true;
                    }
                }
            }
            return res;
        }
        public virtual bool MoveRight()
        {
            bool res = false;
            if (this.strsb.Length > 0)
            {
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {
                    if (this.SelectionEnd < 0)
                    {
                        this.SelectionEnd = this.SelectionStart;
                    }
                    this.SelectionEnd = this.SelectionEnd + 1;
                    if (this.SelectionEnd > this.strsb.Length)
                    {
                        this.SelectionEnd = this.strsb.Length;
                    }
                    //SelectionStart = this.SelectionEnd; 
                    res = true;
                }
                else
                {
                    this.SelectionEnd = -1; ;
                    this.SelectionStart = this.SelectionStart + 1;
                    if (this.SelectionStart > this.strsb.Length)
                    {
                        this.SelectionStart = this.strsb.Length;
                        res = false;
                    }
                    else
                    {
                        SelectionStart = this.SelectionStart;
                        res = true;
                    }
                }
            }
            return res;
        }
        public virtual bool MoveLeft()
        {
            bool res = false;
            if (this.strsb.Length > 0)
            {
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {
                    if (this.SelectionEnd < 0)
                    {
                        this.SelectionEnd = this.SelectionStart;
                    }
                    this.SelectionEnd = this.SelectionEnd - 1;
                    if (this.SelectionEnd <= 0)
                    {
                        this.SelectionEnd = 0;
                    }
                    //SelectionStart = this.SelectionEnd; 
                    res = true;
                }
                else
                {
                    this.SelectionEnd = -1;
                    this.SelectionStart = this.SelectionStart - 1;
                    if (this.SelectionStart < 0)
                    {
                        res = false;
                    }
                    else
                    {
                        SelectionStart = this.SelectionStart;
                        res = true;
                    }
                }
            }
            return res;
        }
        public virtual bool MoveHome()
        {
            bool res = false;
            if (this.strsb.Length > 0)
            {
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {
                    if (this.SelectionEnd < 0)
                    {
                        this.SelectionEnd = this.SelectionStart;
                    }
                    int index = 0;
                    if (index >= 0)
                    {
                        this.SelectionEnd = index;
                        res = true;
                    }
                }
                else
                {
                    this.SelectionEnd = -1;
                    int index = 0;
                    if (index >= 0)
                    {
                        this.SelectionStart = index;
                        SelectionStart = this.SelectionEnd;
                        res = true;
                    }
                }
            }
            return res;
        }
        public virtual bool MoveEnd()
        {
            bool res = false;
            if (this.strsb.Length > 0)
            {
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {
                    if (this.SelectionEnd < 0)
                    {
                        this.SelectionEnd = this.SelectionStart;
                    }
                    int index = strsb.Length;
                    if (index >= 0)
                    {
                        this.SelectionEnd = index;
                        res = true;
                    }
                }
                else
                {
                    this.SelectionEnd = -1;
                    int index = strsb.Length;
                    if (index >= 0)
                    {
                        this.SelectionStart = index;
                        SelectionStart = this.SelectionEnd;
                        res = true;
                    }
                }
            }
            return res;
        }
        public void Copy()
        {
            if (this.SelectionEnd > 0 || this.SelectionStart > 0)
            {
                int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                if (min >= 0)
                {
                    if ((max - min) > 0)
                    {
                        string text = this.strsb.ToString(min, max - min);
                        Feng.Forms.ClipboardHelper.SetText(text);
                    }
                }
            }
        }
        public virtual void SelectAll()
        {
            this.SelectionStart = 0;
            this.SelectionEnd = this.strsb.Length;
            this.SelectionStart = this.SelectionEnd;
        }
        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                downpoint = Point.Empty;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return false;
        }
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (this.TextRegion == null && this.Text.Length > 0)
                    {
                        GetCellCharBounds();
                    }

                    GetCellCharBounds();
                    downpoint = e.Location;
                    bool inthis = MoveCaretIndex(e.Location);
                    this.SelectionEnd = -1;
                    if (inthis)
                    {
                        ShowCaret();
                        return base.OnMouseDown(sender, e, ve);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return false;
        }
        public override void Invalidate()
        {
            //this.Control.Invalidate();
            base.Invalidate();
        }
        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    downpoint = e.Location;
                    int index = GetIndex(downpoint);
                    if (index != this.SelectionStart && index>= 0)
                    {
                        this.SelectionEnd = index; 
                    }
              
                    this.Invalidate();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnMouseMove(sender ,e ,ve); 
        }
        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            string text = this.Text;
            string mtext = text;
            if (text.Length < 1)
            {
                mtext = "|";
            }
            else if (text[text.Length - 1] == '\n')
            {
                mtext = text + "|";
            }
            g.Graphics.ResetClip();
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(this.HorizontalAlignment, this.VerticalAlignment, false);
            sf.Trimming = StringTrimming.None;
            sf.FormatFlags = sf.FormatFlags | StringFormatFlags.MeasureTrailingSpaces;
            SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(this.ForeColor);
            SizeF sizef = SizeF.Empty;
            Rectangle rect = this.Rect;
            if (this.AutoMultiline)
            {
                sizef = g.Graphics.MeasureString(mtext, this.Font, this.Width);
                rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                if (sizef.Height > this.Height)
                {
                    rect.Height = this.Height - this.Top;
                    sf.LineAlignment = StringAlignment.Near;
                }
                //rect.Offset(1, 1);
                //rect.Inflate(-1, -1); 
            }
            else
            {
                sf.FormatFlags = StringFormatFlags.NoWrap;
                sizef = g.Graphics.MeasureString(mtext, this.Font, Point.Empty, sf);
                rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
                if (sizef.Height > this.Height)
                {
                    rect.Height = this.Height - this.Top;
                    sf.LineAlignment = StringAlignment.Near;
                }
                if (sizef.Width > this.Width)
                {
                    rect.Width = this.Width - this.Left;
                    sf.Alignment = StringAlignment.Near;
                }
                //rect.Offset(1, 1);
                //rect.Inflate(-1, -1); 
            }
            TextRegion = Feng.Drawing.MeasureStringHelper.MeasureString(g.Graphics, mtext, this.Font, sf, rect);
            if (this.SelectionEnd >= 0)
            {
                if (TextRegion.Count > 0)
                {
                    int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                    int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                    Color selcolor = Color.FromArgb(100, Feng.Drawing.ColorHelper.MidColor(this.ForeColor, this.BackColor));
                    SolidBrush selbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(selcolor);
                    for (int i = min; i < max; i++)
                    {
                        RectangleF recttext = TextRegion[i];
                        Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, selbrush, recttext);
                    }
                }
            }
         
            if (TextRegion != null)
            {
                //foreach (RectangleF item in TextRegion)
                //{
                //    g.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(item));
                //}
            }
            Feng.Drawing.GraphicsHelper.DrawString(g, text, this.Font, solidbrush, rect, sf);
            base.OnDraw(sender, g);
            return true;

        }

        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            try
            {
                bool res = false;
                if (e.KeyCode == Keys.C)
                {
                    if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                    {
                        this.Copy();
                        res = true;
                    }
                }
                if (res)
                {
                    GetCellCharBounds();
                    this.ShowCaret();
                }
                return res;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return false;
        }
        public override bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData, EventViewArgs ve)
        {
            try
            {
                Keys key = keyData ^ Keys.Shift;
                if ((keyData & Keys.Shift) != Keys.Shift)
                {
                    key = keyData;
                }
                if (key == Keys.Home)
                {
                    MoveHome();
                    ShowCaret();
                    this.Invalidate();
                    return true;
                }
                if (key == Keys.End)
                {
                    MoveEnd();
                    ShowCaret();
                    this.Invalidate();
                    return true;
                }

                if (key == Keys.Right)
                {
                    MoveRight();
                    ShowCaret();
                    this.Invalidate();
                    return true;
                }
                if (key == Keys.Left)
                {
                    MoveLeft();
                    ShowCaret();
                    this.Invalidate();
                    return true;
                }
                if (key == Keys.Up)
                {
                    MoveUp();
                    ShowCaret();
                    this.Invalidate();
                    return true;
                }
                if (key == Keys.Down)
                {
                    MoveDown();
                    ShowCaret();
                    this.Invalidate();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnProcessCmdKey(sender, ref msg, keyData, ve);
        }


        private bool ContentsHasPoint(Point pt)
        {
            if (TextRegion != null)
            {
                foreach (RectangleF recft in TextRegion)
                {
                    if (recft.Contains(pt))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public virtual void FreshContens()
        {
            if (string.IsNullOrWhiteSpace(this.Text))
                return;
            Graphics g = this.GetGraphics();
            StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            if (this.DirectionVertical)
            {
                sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DirectionVertical;
            }
            Size Size = Feng.Utils.ConvertHelper.ToSize(g.MeasureString(this.Text + "A", this.Font, Point.Empty, sf));
            this.Width = Size.Width;
            this.Height = Size.Height;
        }

    }
}

