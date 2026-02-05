using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Utils;
using Feng.Data;

namespace Feng.Forms.Views
{
    [ToolboxItem(false)]
    public class EditView : TextView
    {
        public EditView()
        {
        }
        public virtual void OnTextChanged(EventArgs e)
        {

        }
        public virtual void EndEdit()
        {
            try
            {
                string text = base.Text;
                this.Text = text;
                this.FreshContens();
            }
            catch (Exception ex)
            {
                BugReport.Log(ex);
            }
        }
        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            try
            {
                bool res = false;

                if (e.KeyCode == Keys.Delete)
                {
                    Delete();
                    res = true;
                    GetCellCharBounds();
                    this.ShowCaret();
                    this.Invalidate();
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (!AllowEnter)
                    {
                        Append("\r\n");
                        res = true;
                        GetCellCharBounds();
                        this.ShowCaret();
                        this.Invalidate();
                    }
                }

                if (e.KeyCode == Keys.V)
                {
                    if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                    {
                        string text = Feng.Forms.ClipboardHelper.GetText();
                        this.Paste(text);
                        res = true;
                        GetCellCharBounds();
                        this.ShowCaret();
                        this.Invalidate();
                    }
                }
                if (e.KeyCode == Keys.X)
                {
                    if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                    {
                        this.Cut();
                        res = true;
                        GetCellCharBounds();
                        this.ShowCaret();
                        this.Invalidate();
                    }
                } 
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnKeyDown(sender, e, ve);
        }
        public override bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            try
            {
                if (this.Control == null)
                    return false;
                int msg = m.Msg;
                if (msg == Feng.Utils.UnsafeNativeMethods.WM_IME_SETCONTEXT) //associated IME with our UserControl
                {
                    //if (!m.WParam.Equals(IntPtr.Zero))
                    //{
                    //    bool flag = Feng.Utils.UnsafeNativeMethods.ImmAssociateContextEx(m.HWnd, IntPtr.Zero, 16);
                    //    IntPtr hIMC = Feng.Utils.UnsafeNativeMethods.ImmGetContext(m.HWnd);
                    //    flag = Feng.Utils.UnsafeNativeMethods.ImmSetOpenStatus(hIMC, true);
                    //    flag = Feng.Utils.UnsafeNativeMethods.ImmReleaseContext(m.HWnd, hIMC);
                    //}
                }
                else if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_IME_STARTCOMPOSITION) //Intercept Message to get Unicode Char
                {
                    IntPtr hIMC = Feng.Utils.UnsafeNativeMethods.ImmGetContext(m.HWnd);
                    Feng.Utils.UnsafeNativeMethods.COMPOSITIONFORM CompForm = new Utils.UnsafeNativeMethods.COMPOSITIONFORM();
                    CompForm.dwStyle = Feng.Utils.UnsafeNativeMethods.CFS_POINT;
                    CompForm.dwStyle = Feng.Utils.UnsafeNativeMethods.CFS_POINT;
                    CompForm.ptCurrentPos = new Point();

                    Point MousePoint = this.Control.PointToClient(System.Windows.Forms.Control.MousePosition);


                    CompForm.ptCurrentPos.X = MousePoint.X;
                    CompForm.ptCurrentPos.Y = MousePoint.Y;
                    bool flag = Feng.Utils.UnsafeNativeMethods.ImmSetCompositionWindow(hIMC, ref CompForm);
                    flag = Feng.Utils.UnsafeNativeMethods.ImmReleaseContext(m.HWnd, hIMC);
                }
                else if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_CHAR)
                {
                    if (System.Windows.Forms.Control.ModifierKeys != Keys.Control
                        || System.Windows.Forms.Control.ModifierKeys != Keys.Control
                        || System.Windows.Forms.Control.ModifierKeys != Keys.Control)
                    {
                        char m_ImeChar = Convert.ToChar(m.WParam.ToInt32());
                        if (!char.IsControl(m_ImeChar) || m_ImeChar == '\b')
                        {
                            this.ImeCharChanged(m_ImeChar);
                        }
                    }
                }
                if (m.Msg == Feng.Utils.UnsafeNativeMethods.WM_IME_ENDCOMPOSITION) //Intercept Message to get Unicode Char
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
            }
            base.OnWndProc(sender, ref m, ve);
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
                if (key == Keys.Tab)
                {
                    string text = "    ";
                    this.Paste(text); 
                    GetCellCharBounds();
                    this.ShowCaret();
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
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            this.Width = this.Control.Width;
            this.Height = this.Control.Height;
            return base.OnSizeChanged(sender, e, ve);
        }
        public override void Invalidate()
        {
            try
            {
                if (this.Control != null)
                {
                    this.Control.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Forms.Views", "EditView", "Invalidate", ex);
            }
            base.Invalidate();
        }
        public virtual bool ImeCharChanged(char c)
       {
            Append(c);
            GetCellCharBounds();
            ShowCaret();
            this.Invalidate();
            return false;
        }
        public virtual void Append(char c)
        {
            if (this.ReadOnly)
                return;
            switch (c)
            {
                case '\b':
                    BackSpace();
                    break;
                default:
                    ClearSelect();
                    Insert(c);
                    break;
            }

            SelectionStart = SelectionStart;
        }
        public virtual void Append(string text)
        {
            if (this.ReadOnly)
                return;
            this.StringBuilder.Append(text);
            GetCellCharBounds();
            ShowCaret();
            SelectionStart = SelectionStart;

        }
        public void Insert(char c)
        {
            if (this.ReadOnly)
                return;
            this.StringBuilder.Insert(this.SelectionStart, c);
            this.SelectionStart = this.SelectionStart + 1;
            this.SelectionEnd = -1;
        }
        public void Insert(string text)
        {
            if (this.ReadOnly)
                return;
            this.StringBuilder.Insert(this.SelectionStart, text);
            this.SelectionStart = this.SelectionStart + text.Length;
        }
        public void Cut()
        {
            if (this.ReadOnly)
                return;
            int min = Math.Min(this.SelectionStart, this.SelectionEnd);
            int max = Math.Max(this.SelectionStart, this.SelectionEnd);
            int count = max - min;
            if (count > 0)
            { 
                string text = this.StringBuilder.ToString(min, max - min);
                this.StringBuilder.Remove(min, max - min);
                this.SelectionEnd = -1;
                this.SelectionStart = min;
                Feng.Forms.ClipboardHelper.SetText(text);
            } 
        }
        public void Paste()
        {
            if (this.ReadOnly)
                return;
            ClearSelect();
            string text = Feng.Forms.ClipboardHelper.GetText();
            Insert(text);
            GetCellCharBounds();
            SelectionStart = SelectionStart;
        }
        public void Paste(string text)
        {
            if (this.ReadOnly)
                return;
            ClearSelect();
            Insert(text);
            GetCellCharBounds();
            SelectionStart = SelectionStart;
        }
        public void BackSpace()
        {
            if (this.ReadOnly)
                return;
            if (this.StringBuilder.Length < 1)
                return;
            if (this.SelectionEnd < 0)
            {
                if (this.SelectionStart > 0)
                {
                    this.SelectionStart = this.SelectionStart - 1;
                    char c = this.StringBuilder[this.SelectionStart];
                    if (c == '\n')
                    {
                        this.StringBuilder.Remove(this.SelectionStart, 1);
                        if (this.StringBuilder.Length > 1)
                        {
                            c = this.StringBuilder[this.SelectionStart - 1];
                            if (c == '\r')
                            {
                                this.SelectionStart = this.SelectionStart - 1;
                                this.StringBuilder.Remove(this.SelectionStart, 1);
                            }
                            string text = this.Text;
                            return;
                        }
                    }
                    else if (this.StringBuilder.Length < 1)
                    {
                        return;
                    }
                    this.StringBuilder.Remove(this.SelectionStart, 1);
                }
            }
            else
            {
                int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                this.StringBuilder.Remove(min, max - min);
                this.SelectionEnd = -1;
                this.SelectionStart = min;
            }
        }
        public void ClearSelect()
        {
            if (this.ReadOnly)
                return;
            if (this.SelectionEnd >= 0)
            {
                if (this.SelectionStart >= 0)
                {
                    int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                    int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                    if (min >= 0 && max < this.StringBuilder.Length)
                    {
                        this.StringBuilder.Remove(min, max - min);
                        this.SelectionEnd = -1;
                        this.SelectionStart = min;
                    }
                    else
                    {
                        this.SelectionEnd = 0;
                        this.SelectionStart = 0;
                    }
                }
            }
        }
        public void Delete()
        {
            if (this.ReadOnly)
                return;
            if (this.SelectionEnd < 0)
            {
                if (this.SelectionStart >= 0 && this.SelectionStart < this.StringBuilder.Length)
                {
                    this.StringBuilder.Remove(this.SelectionStart, 1);
                }
            }
            else
            {
                int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                this.StringBuilder.Remove(min, max - min);
                this.SelectionEnd = -1;
                this.SelectionStart = min;
            }
        }
        private Size lastcaretsize = Size.Empty;
        public bool AllowEnter { get; set; }
        public override DataStruct Data { get { return null; } }
 
        //public override int Left { get { return 0; } set { } }
        //public override int Top { get { return 0; } set { } }
        //public override int Width { get { return this.Control.Width; } set { } }
        //public override int Height { get { return this.Control.Height; } set { } }
    }
}
