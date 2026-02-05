using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Feng.Forms.Controls.Designer;
namespace Feng.Forms
{
    [ToolboxItem(false)]
    public class BaseEdit : System.Windows.Forms.Control
    {
        public BaseEdit()
        {
            this.SetStyle(ControlStyles.DoubleBuffer

| ControlStyles.ResizeRedraw
| ControlStyles.SupportsTransparentBackColor
| ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ContainerControl, true);
            this.UpdateStyles();
        }

        ~BaseEdit()
        {

        }
        private Caret caret = new Caret();
        private string _text = string.Empty;

        public override string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                this.Cells.Clear();
                this.AppendText(value);
            }
        }
        public void PropertyChanged()
        {
        }
        private bool _drawborder = true;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(true)]
        public bool DrawBorder
        {
            get { return _drawborder; }
            set { _drawborder = value; PropertyChanged(); }
        }
        private int _borderwidth = 1;
        [DefaultValue(1)]
        [Category(CategorySetting.PropertyDesign)]
        public int BorderWidth
        {
            get { return _borderwidth; }
            set { _borderwidth = value; PropertyChanged(); }
        }
        private Color _bordercolor = Color.Silver;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(typeof(Color), "System.Drawing.Color.Silver")]
        public Color BorderColor
        {
            get { return _bordercolor; }
            set { _bordercolor = value; PropertyChanged(); }
        }
        private int _radius = 6;
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
                base.BackColor = value;
            }
        }

        private Color _editbackcolor = Color.White;
        [Category(CategorySetting.PropertyDesign)]
        public virtual Color EditBackColor
        {
            get
            {
                return _editbackcolor;
            }
            set
            {
                _editbackcolor = value;
            }
        }
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(6)]
        public int Radius
        {

            get { return _radius; }
            set { _radius = value; PropertyChanged(); }
        }
        private Rectangle _textrect = new Rectangle();
        public virtual Rectangle TextRect
        {
            get
            {

                return new Rectangle(1, 1, this.Width - 1, this.Height - 1);

                return _textrect;
            }
            set
            {
                _textrect = value; PropertyChanged();
            }
        }

        private StringAlignment _alignment = StringAlignment.Near;
        public virtual StringAlignment Alignment
        {
            get { return _alignment; }
            set
            {
                _alignment = value;
                PropertyChanged();
            }
        }
        private StringAlignment _lineAlignment = StringAlignment.Center;
        public virtual StringAlignment LineAlignment
        {
            get { return _lineAlignment; }

            set
            {
                _lineAlignment = value;
                PropertyChanged();
            }
        }
        private StringFormat _GenericDefault = null;
        private bool _multiline = false;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(false)]
        public virtual bool MultiLine
        {
            get
            {
                return _multiline;
            }
            set
            {
                _multiline = value;
                PropertyChanged();
            }
        }
        private int _position = 0;
        [Category(CategorySetting.PropertyDesign)]
        [DefaultValue(0)]
        public int Position
        {
            get
            {
                return this._position;
            }
            set
            {
                if (this.Cells.Count > 0)
                {
                    if (this._position < 0)
                    {
                        this._position = 0;
                    }
                }
                else if (this._position >= this.Cells.Count)
                {
                    this._position = this.Cells.Count - 1;
                }
                this._position = value;
            }
        }
        private TextCellViewCollection _cells = new TextCellViewCollection();
        protected TextCellViewCollection Cells
        {
            get
            {
                return _cells;
            }
        }
        public TextCellView CurrnetCell
        {
            get
            {
                if (this.Position >= 0 && this.Cells.Count > this.Position)
                {
                    return this.Cells[Position];
                }
                return null;
            }
        }
        public TextCellView LastCell
        {
            get
            {
                if (this.Cells.Count > 0)
                {
                    return this.Cells[this.Cells.Count - 1];
                }
                return null;
            }
        }
        public TextCellView FirstCell
        {
            get
            {
                if (this.Cells.Count > 0)
                {
                    return this.Cells[0];
                }
                return null;
            }
        }
        public virtual void OnDrawBackGround(Graphics g)
        {
            if (this.DrawBorder)
            {
                Feng.Drawing.GraphicsHelper.FillRectangle(g, this.EditBackColor,
                    this.TextRect.Left, this.TextRect.Top, this.TextRect.Width, this.TextRect.Height, this.DrawBorder,
                    this.BorderWidth, this.BorderColor, this.Radius);
            }
            else
            {
                Feng.Drawing.GraphicsHelper.FillRectangle(g, this.EditBackColor,
    this.TextRect.Left, this.TextRect.Top, this.TextRect.Width, this.TextRect.Height,
    this.DrawBorder,
    this.BorderWidth, this.BorderColor, 0);
            }
        }
        public void OnDrawBorder(Graphics g)
        {
            if (this.DrawBorder)
            {
                Feng.Drawing.GraphicsHelper.DrawRectangle(g,
                    this.TextRect.Left, this.TextRect.Top, this.TextRect.Width, this.TextRect.Height,
                    this.BorderWidth, this.BorderColor, this.Radius);
            }
        }
        protected override void WndProc(ref Message m)
        {

            try
            {
                OnWndProc(ref m);
            }
            catch (Exception)
            {
            }

            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //return base.CreateParams;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT 
                return cp;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            _BeginReFresh = 0;
            _EndReFresh = 0;
            _BeginRefreshCellLocation = _endrefreshcelllocation = 0;
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            OnDrawBackGround(g);
            OnDrawText(g);
            OnDrawBorder(g);
            base.OnPaint(e);

        }
        protected override void OnMouseDown(MouseEventArgs e)
        {

            try
            {
                this.Focus();
                Point pt = e.Location;
                SetPositionByPoint(pt);
                SetCaret();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnMouseDown(e);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            this.caret.Handle = this.Handle;
            base.OnHandleCreated(e);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            this.caret.Destroy();
            base.OnHandleDestroyed(e);
        }
        protected override void OnGotFocus(EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            base.OnGotFocus(e);
        }
        public bool OnWndProc(ref Message m)
        {
            if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_IME_SETCONTEXT && m.WParam.ToInt32() == 1)
            {
                Feng.Utils.UnsafeNativeMethods.ImmAssociateContext(this.Handle, this.Handle);
            }

            switch (m.Msg)
            {
                case Feng.Utils.UnsafeNativeMethods.WM_CHAR:
                    KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | System.Windows.Forms.Control.ModifierKeys);
                    if (e.Modifiers == Keys.Control || e.Modifiers == Keys.Alt)
                    {
                        break;
                    }
                    if (e.KeyCode == Keys.Back)
                    {
                        this.Backspace();
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        this.DeleteText();
                    }
                    else
                    {
                        string text = ((char)e.KeyData).ToString();
                        InsertText(text);
                    }
                    this.SetCaret();
                    break;
                case Feng.Utils.UnsafeNativeMethods.WM_PASTE:
                    this.Paste();
                    break;
                case Feng.Utils.UnsafeNativeMethods.WM_IME_CHAR:
                    if (m.WParam.ToInt32() == Feng.Utils.UnsafeNativeMethods.PM_REMOVE) //如果不做这个判断.会打印出重复的中文 
                    {
                        StringBuilder str = new StringBuilder();
                        MessageBox.Show(str.ToString());
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
        private int _BeginRefreshCellLocation = 0;
        public virtual void BeginRefreshCellLocation()
        {
            _BeginRefreshCellLocation++;
        }
        private int _endrefreshcelllocation = 0;
        public virtual void EndRefreshCellLocation(int position)
        {
            _endrefreshcelllocation++;
            RefreshCellLocation(position);
        }

        public virtual void RefreshCellLocation(int position)
        {
            if (this._BeginRefreshCellLocation == _endrefreshcelllocation)
            {
                RefreshCellLoaction(position);
                _BeginRefreshCellLocation = _endrefreshcelllocation = 0;
            }
        }
        private int _BeginReFresh = 100;
        public virtual void BeginReFresh()
        {
            _BeginReFresh++;
        }
        private int _EndReFresh = 0;
        public virtual void EndReFresh()
        {
            _EndReFresh++;
            this.RePaint();

        }
        public virtual void RePaint()
        {

            if (this._BeginReFresh == _EndReFresh)
            {
                this.Invalidate(true);
                _BeginReFresh = _EndReFresh = 0;
            }

        }
        public virtual void OnDrawText(Graphics g)
        {
            if (_GenericDefault == null)
            {
                _GenericDefault = StringFormat.GenericDefault.Clone() as StringFormat;
                _GenericDefault.Alignment = this.Alignment;
                _GenericDefault.LineAlignment = this.LineAlignment;
            }

            foreach (TextCellView cell in this.Cells)
            {
                cell.OnDraw(g);
            }
            //using (SolidBrush sb = new SolidBrush(this.ForeColor))
            //{
            //    g.DrawString(this.Text, this.Font, sb, this.TextRect, _GenericDefault);
            //}
        }
        public virtual void OnTextChanged()
        {
            this.BeginReFresh();

            this.EndReFresh();
        }
        public virtual void InsertText(string text)
        {
            this.BeginReFresh();
            for (int i = 0; i < text.Length; i++)
            {
                InsertText(text[i]);
            }
            this.EndReFresh();
        }
        public virtual void InsertText(char ch)
        {
            if (this.CurrnetCell != null)
            {
                this.BeginReFresh();
                TextCellView cell = CreateCellView(ch);
                CopyCell(this.CurrnetCell, cell);
                this.Cells.Insert(this.Position, cell);
                OnTextChanged();
                this.EndReFresh();
            }
            else
            {
                this.AppendText(ch);
            }
        }
        public void Paste()
        {

        }
        public virtual void AppendText(string text)
        {
            this.BeginReFresh();
            for (int i = 0; i < text.Length; i++)
            {
                AppendText(text[i]);
            }
            this.EndReFresh();
        }
        public virtual void AppendText(char ch)
        {
            this.BeginReFresh();
            TextCellView cell = CreateCellView(ch);
            if (this.LastCell != null)
            {
                CopyCell(this.LastCell, cell);
            }
            this.Cells.Add(cell);
            this.Position = this.Cells.Count;
            this.EndReFresh();
        }
        public virtual void SetCaret()
        {
            SetCaret(this.Position);
        }
        public virtual void SetCaret(int position)
        {
            TextCellView cell = GetPositionCell(position);
            if (cell != null)
            {
                this.caret.Show(this.Handle, (int)cell.Height, (int)cell.Right, (int)cell.Top);
            }
        }
        private void CopyCell(TextCellView precell, TextCellView cell)
        {
            cell.Left = precell.Right;
            cell.Top = precell.Top;
            cell.ForeColor = this.ForeColor;
            cell.BackColor = this.BackColor;
            cell.Font = this.Font;
        }
        public virtual TextCellView GeTextCellByPoint(Point pt)
        {
            for (int i = 0; i < this.Cells.Count; i++)
            {
                if (this.Cells[i].Rect.Contains(pt))
                {
                    return this.Cells[i];
                }
            }
            return null;
        }
        public virtual void SetPositionByPoint(Point pt)
        {
            for (int i = 0; i < this.Cells.Count; i++)
            {
                if (this.Cells[i].Rect.Contains(pt))
                {
                    float width = this.Cells[i].Rect.Left + this.Cells[i].Rect.Width / 2;
                    if (pt.X > width)
                    {
                        this.Position = i;
                    }
                    else
                    {
                        this.Position = i - 1;
                    }
                    return;
                }
            }
        }
        public virtual TextCellView GetPositionCell(int position)
        {
            if (position >= 0 && this.Cells.Count > position)
            {
                return this.Cells[position];
            }
            return null;
        }
        public virtual TextCellView CreateCellView(char ch)
        {
            TextCellView cell = new TextCellView();
            cell.Text = ch.ToString();
            Size size = GetTextSize(ch);
            cell.Width = size.Width;
            cell.Height = size.Height;
            cell.Left = this.BorderWidth;
            cell.Top = this.BorderWidth;
            cell.Font = this.Font;
            return cell;
        }
        public virtual void DeleteText()
        {
            this.BeginReFresh();
            if (this.Position >= 0 && this.Cells.Count > this.Position)
            {
                this.Cells.RemoveAt(this.Position);
            }
            this.EndReFresh();
        }
        public virtual void Backspace()
        {
            this.BeginReFresh();
            if (this.Position > 0 && this.Cells.Count >= this.Position)
            {
                int position = this.Position - 1;
                this.Cells.RemoveAt(position);
                this.Position = position;
            }
            this.EndReFresh();
        }

        public virtual void RefreshCellLoaction(int position)
        {
            _BeginRefreshCellLocation = _endrefreshcelllocation = 0;
            TextCellView cell = this.GetPositionCell(position);
            if (cell != null)
            {
                float left = cell.Left;
                for (int i = position + 1; i < this.Cells.Count; i++)
                {
                    TextCellView c = this.Cells[i];
                    c.Left = left;
                    left = c.Right;
                }
            }
        }
        public Size GetTextSize(char ch)
        {
            Graphics g = this.CreateGraphics();
            Size sf = Feng.Drawing.GraphicsHelper.Sizeof(ch, this.Font, g);
            return sf;
        }
    }
    public class TextCellViewCollection : IList<TextCellView>
    {
        private List<TextCellView> list = new List<TextCellView>();
        public int IndexOf(TextCellView item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, TextCellView item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public TextCellView this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(TextCellView item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(TextCellView item)
        {
            return list.Contains(item);
        }

        public void CopyTo(TextCellView[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TextCellView item)
        {
            return this.list.Remove(item);
        }

        public IEnumerator<TextCellView> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
    public class TextCellView
    {
        private string _text = string.Empty;
        public virtual string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        private Color _forecolor = Color.Black;
        public virtual Color ForeColor
        {
            get
            {
                return this._forecolor;
            }
            set
            {
                this._forecolor = value;
            }
        }

        private Color _backcolor = Color.White;
        public virtual Color BackColor
        {
            get
            {
                return this._backcolor;
            }
            set
            {
                this._backcolor = value; ;
            }
        }

        private Font _font = null;
        public virtual Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
            }
        }

        private float _left = 0f;
        public virtual float Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        private float _height = 0f;
        public virtual float Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public virtual float Right
        {
            get
            {

                return _width + _left;
            }

        }

        public virtual float Bottom
        {
            get
            {
                return Top + _height;
            }

        }

        private float _top = 0;
        public virtual float Top
        {
            get
            {
                return this._top;
            }
            set
            {
                this._top = value;
            }
        }

        private float _width = 0f;
        public virtual float Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public virtual RectangleF Rect
        {
            get
            {
                return new RectangleF(this._left, this._top, this._width, this._height);
            }
        }

        public virtual void OnDraw(Graphics g)
        {
            using (SolidBrush sb = new SolidBrush(this.ForeColor))
            {
                g.DrawString(this.Text, this.Font, sb, new PointF(this.Left, this.Top));
            }

        }
    }
}
