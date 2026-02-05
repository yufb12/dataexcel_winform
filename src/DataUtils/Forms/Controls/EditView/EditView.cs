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

namespace Feng.Forms.Controls
{ 

    //[ToolboxItem(false)]
    //public class EditView
    //{
    //    public EditView()
    //    {
    //    }

    //    public virtual void OnTextChanged(EventArgs e)
    //    {

    //    }
  
    //    public bool AllowEnter { get; set; }
    //    private void SelectAll()
    //    {
    //        this.SelectionStart = 0;
    //        this.SelectionEnd = this.strsb.Length;
    //        this.SelectionStart = this.SelectionEnd;
    //    }
    //    public virtual void EndEdit()
    //    {
    //        try
    //        {
    //            string text = this.strsb.ToString();
    //            this.Text = text;
    //            this.FreshContens();
    //        }
    //        catch (Exception ex)
    //        {
    //            BugReport.Log(ex);
    //        }
    //    }
    //    public virtual bool OnMouseUp(object sender, MouseEventArgs e)
    //    {
    //        try
    //        { 
    //            downpoint = Point.Empty;
    //        }
    //        catch (Exception ex)
    //        {
    //            Feng.Utils.ExceptionHelper.ShowError(ex);
    //        }
    //        return false;
    //    }
    //    private Point downpoint = Point.Empty;
    //    public virtual bool OnMouseDown(object sender, MouseEventArgs e)
    //    {
    //        try
    //        {
    //            if (e.Button == MouseButtons.Left)
    //            {
    //                if (this.TextRegion == null && this.Text.Length > 0)
    //                {
    //                    GetTextSize(this.GetGraphics());
    //                }
    //                downpoint = e.Location;
    //                bool inthis = MoveCaretIndex(e.Location);
    //                this.SelectionEnd = -1;
    //                if (inthis)
    //                {
    //                    ShowCaret();
    //                    return true;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Feng.Utils.ExceptionHelper.ShowError(ex);
    //        }
    //        return false;
    //    }
    //    public virtual bool OnMouseMove(object sender, MouseEventArgs e)
    //    {
    //        try
    //        { 
    //            if (e.Button == MouseButtons.Left)
    //            {
    //                downpoint = e.Location;
    //                int index = GetIndex(downpoint);
    //                if (index != this.SelectionStart)
    //                {
    //                    this.SelectionEnd = index;
    //                }
    //                else
    //                {
    //                    this.SelectionEnd = -1;
    //                }

    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Feng.Utils.ExceptionHelper.ShowError(ex);
    //        }
    //        return false;
    //    }
    //    public Point GetColumn(int index)
    //    {
    //        int row = 0;
    //        int column = 0;
    //        for (int i = 0; i < index; i++)
    //        {
    //            if (i >= strsb.Length)
    //            {
    //                break;
    //            }
    //            if (strsb[i] == '\n')
    //            {
    //                column = 0;
    //                row++;
    //            }
    //            else
    //            {
    //                column++;
    //            }
    //        }
    //        return new Point() { X = row, Y = column };
    //    }
    //    public int GetUpIndex(Point pt)
    //    {
    //        int row = 0;
    //        int index = 0;
    //        for (int i = 0; i < strsb.Length; i++)
    //        {
    //            if (i >= strsb.Length)
    //            {
    //                break;
    //            }
    //            if (row == pt.X - 1)
    //            {
    //                int column = 0;
    //                for (index = i; index < strsb.Length; index++)
    //                {
    //                    if (strsb[i] == '\n')
    //                    {
    //                        return index;
    //                    }
    //                    if (column >= pt.Y)
    //                    {
    //                        return index;
    //                    }
    //                    column++;
    //                }
    //                return -1;
    //            }
    //            if (strsb[i] == '\n')
    //            {
    //                row++;

    //            }
    //        }
    //        return -1;
    //    }
    //    public int GetDownIndex(Point pt)
    //    {
    //        int row = 0;
    //        int index = 0;
    //        for (int i = 0; i < strsb.Length; i++)
    //        {
    //            if (i >= strsb.Length)
    //            {
    //                break;
    //            }
    //            if (row == pt.X + 1)
    //            {
    //                int column = 0;
    //                for (index = i; index < strsb.Length; index++)
    //                {
    //                    if (strsb[i] == '\n')
    //                    {
    //                        return index;
    //                    }
    //                    if (column >= pt.Y)
    //                    {
    //                        return index;
    //                    }
    //                    column++;
    //                }
    //                return -1;
    //            }
    //            if (strsb[i] == '\n')
    //            {
    //                row++;

    //            }
    //        }
    //        return -1;
    //    }
    //    private bool MoveUp()
    //    {
    //        bool res = false;
    //        if (this.strsb.Length > 0)
    //        {
    //            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //            {
    //                if (this.SelectionEnd < 0)
    //                {
    //                    this.SelectionEnd = this.SelectionStart;
    //                }
    //                int index = this.SelectionEnd;
    //                Point pt = GetColumn(index);
    //                index = GetUpIndex(pt);
    //                if (index >= 0)
    //                {
    //                    this.SelectionEnd = index;
    //                    res = true;
    //                }
    //            }
    //            else
    //            {
    //                this.SelectionEnd = -1;
    //                int index = this.SelectionStart;
    //                Point pt = GetColumn(index);
    //                index = GetUpIndex(pt);
    //                if (index >= 0)
    //                {
    //                    this.SelectionStart = index;
    //                    res = true;
    //                }
    //            }
    //        }
    //        return res;
    //    }
    //    private bool MoveDown()
    //    {
    //        bool res = false;
    //        if (this.strsb.Length > 0)
    //        {
    //            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //            {
    //                if (this.SelectionEnd < 0)
    //                {
    //                    this.SelectionEnd = this.SelectionStart;
    //                }
    //                int index = this.SelectionEnd;
    //                Point pt = GetColumn(index);
    //                index = GetDownIndex(pt);
    //                if (index >= 0)
    //                {
    //                    this.SelectionEnd = index;
    //                    res = true;
    //                }
    //            }
    //            else
    //            {
    //                this.SelectionEnd = -1;
    //                int index = this.SelectionStart;
    //                Point pt = GetColumn(index);
    //                index = GetDownIndex(pt);
    //                if (index >= 0)
    //                {
    //                    this.SelectionStart = index;
    //                    res = true;
    //                }
    //            }
    //        }
    //        return res;
    //    }
    //    private bool MoveRight()
    //    {
    //        bool res = false;
    //        if (this.strsb.Length > 0)
    //        {
    //            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //            {
    //                if (this.SelectionEnd < 0)
    //                {
    //                    this.SelectionEnd = this.SelectionStart;
    //                }
    //                this.SelectionEnd = this.SelectionEnd + 1;
    //                if (this.SelectionEnd > this.strsb.Length)
    //                {
    //                    this.SelectionEnd = this.strsb.Length;
    //                }
    //                //SelectionStart = this.SelectionEnd; 
    //                res = true;
    //            }
    //            else
    //            {
    //                this.SelectionEnd = -1; ;
    //                this.SelectionStart = this.SelectionStart + 1;
    //                if (this.SelectionStart > this.strsb.Length)
    //                {
    //                    this.SelectionStart = this.strsb.Length;
    //                    res = false;
    //                }
    //                else
    //                {
    //                    SelectionStart = this.SelectionStart;
    //                    res = true;
    //                }
    //            }
    //        }
    //        return res;
    //    }
    //    private bool MoveLeft()
    //    {
    //        bool res = false;
    //        if (this.strsb.Length > 0)
    //        {
    //            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //            {
    //                if (this.SelectionEnd < 0)
    //                {
    //                    this.SelectionEnd = this.SelectionStart;
    //                }
    //                this.SelectionEnd = this.SelectionEnd - 1;
    //                if (this.SelectionEnd <= 0)
    //                {
    //                    this.SelectionEnd = 0;
    //                }
    //                //SelectionStart = this.SelectionEnd; 
    //                res = true;
    //            }
    //            else
    //            {
    //                this.SelectionEnd = -1;
    //                this.SelectionStart = this.SelectionStart - 1;
    //                if (this.SelectionStart < 0)
    //                {
    //                    res = false;
    //                }
    //                else
    //                {
    //                    SelectionStart = this.SelectionStart;
    //                    res = true;
    //                }
    //            }
    //        }
    //        return res;
    //    }
    //    private bool MoveHome()
    //    {
    //        bool res = false;
    //        if (this.strsb.Length > 0)
    //        {
    //            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //            {
    //                if (this.SelectionEnd < 0)
    //                {
    //                    this.SelectionEnd = this.SelectionStart;
    //                }
    //                int index = 0;
    //                if (index >= 0)
    //                {
    //                    this.SelectionEnd = index;
    //                    res = true;
    //                }
    //            }
    //            else
    //            {
    //                this.SelectionEnd = -1;
    //                int index = 0;
    //                if (index >= 0)
    //                {
    //                    this.SelectionStart = index;
    //                    SelectionStart = this.SelectionEnd;
    //                    res = true;
    //                }
    //            }
    //        }
    //        return res;
    //    }
    //    private bool MoveEnd()
    //    {
    //        bool res = false;
    //        if (this.strsb.Length > 0)
    //        {
    //            if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //            {
    //                if (this.SelectionEnd < 0)
    //                {
    //                    this.SelectionEnd = this.SelectionStart;
    //                }
    //                int index = strsb.Length;
    //                if (index >= 0)
    //                {
    //                    this.SelectionEnd = index;
    //                    res = true;
    //                }
    //            }
    //            else
    //            {
    //                this.SelectionEnd = -1;
    //                int index = strsb.Length;
    //                if (index >= 0)
    //                {
    //                    this.SelectionStart = index;
    //                    SelectionStart = this.SelectionEnd;
    //                    res = true;
    //                }
    //            }
    //        }
    //        return res;
    //    }
    //    //private bool MoveUp()
    //    //{
    //    //    bool res = false;
    //    //    if (this.strsb.Length > 0)
    //    //    {
    //    //        if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //    //        {
    //    //            if (this.SelectionEnd < 0)
    //    //            {
    //    //                this.SelectionEnd = this.SelectionStart;
    //    //            }
    //    //            int index = this.SelectionEnd;
    //    //            Point pt = GetColumn(index);
    //    //            index = GetUpIndex(pt);
    //    //            if (index >= 0)
    //    //            {
    //    //                this.SelectionEnd = index;
    //    //                SelectionStart = this.SelectionEnd;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //        else
    //    //        {
    //    //            this.SelectionEnd = -1;
    //    //            int index = this.SelectionStart;
    //    //            Point pt = GetColumn(index);
    //    //            index = GetUpIndex(pt);
    //    //            if (index >= 0)
    //    //            {
    //    //                this.SelectionStart = index;
    //    //                SelectionStart = this.SelectionEnd;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //    }
    //    //    return res;
    //    //}
    //    //private bool MoveDown()
    //    //{
    //    //    bool res = false;
    //    //    if (this.strsb.Length > 0)
    //    //    {
    //    //        if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //    //        {
    //    //            if (this.SelectionEnd < 0)
    //    //            {
    //    //                this.SelectionEnd = this.SelectionStart;
    //    //            }
    //    //            int index = this.SelectionEnd;
    //    //            Point pt = GetColumn(index);
    //    //            index = GetDownIndex(pt);
    //    //            if (index >= 0)
    //    //            {
    //    //                this.SelectionEnd = index;
    //    //                SelectionStart = this.SelectionEnd;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //        else
    //    //        {
    //    //            this.SelectionEnd = -1;
    //    //            int index = this.SelectionStart;
    //    //            Point pt = GetColumn(index);
    //    //            index = GetDownIndex(pt);
    //    //            if (index >= 0)
    //    //            {
    //    //                this.SelectionStart = index;
    //    //                SelectionStart = this.SelectionEnd;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //    }
    //    //    return res;
    //    //}
    //    //private bool MoveRight()
    //    //{
    //    //    bool res = false;
    //    //    if (this.strsb.Length > 0)
    //    //    {
    //    //        if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //    //        {
    //    //            if (this.SelectionEnd < 0)
    //    //            {
    //    //                this.SelectionEnd = this.SelectionStart;
    //    //            }
    //    //            this.SelectionEnd = this.SelectionEnd + 1;
    //    //            if (this.SelectionEnd > this.strsb.Length)
    //    //            {
    //    //                this.SelectionEnd = this.strsb.Length;
    //    //            }
    //    //            SelectionStart = this.SelectionEnd;
    //    //            res = true;
    //    //        }
    //    //        else
    //    //        {
    //    //            this.SelectionEnd = -1; ;
    //    //            this.SelectionStart = this.SelectionStart + 1;
    //    //            if (this.SelectionStart > this.strsb.Length)
    //    //            {
    //    //                this.SelectionStart = this.strsb.Length;
    //    //                res = false;
    //    //            }
    //    //            else
    //    //            {
    //    //                SelectionStart = this.SelectionStart;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //    }
    //    //    return res;
    //    //}
    //    //private bool MoveLeft()
    //    //{
    //    //    bool res = false;
    //    //    if (this.strsb.Length > 0)
    //    //    {
    //    //        if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //    //        {
    //    //            if (this.SelectionEnd < 0)
    //    //            {
    //    //                this.SelectionEnd = this.SelectionStart;
    //    //            }
    //    //            this.SelectionEnd = this.SelectionEnd - 1;
    //    //            if (this.SelectionEnd <= 0)
    //    //            {
    //    //                this.SelectionEnd = 0;
    //    //            }
    //    //            SelectionStart = this.SelectionEnd;
    //    //            res = true;
    //    //        }
    //    //        else
    //    //        {
    //    //            this.SelectionEnd = -1;
    //    //            this.SelectionStart = this.SelectionStart - 1;
    //    //            if (this.SelectionStart < 0)
    //    //            {
    //    //                res = false;
    //    //            }
    //    //            else
    //    //            {
    //    //                SelectionStart = this.SelectionStart;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //    }
    //    //    return res;
    //    //}
    //    //private bool MoveHome()
    //    //{
    //    //    bool res = false;
    //    //    if (this.strsb.Length > 0)
    //    //    {
    //    //        if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //    //        {
    //    //            if (this.SelectionEnd < 0)
    //    //            {
    //    //                this.SelectionEnd = this.SelectionStart;
    //    //            }
    //    //            int index = 0;
    //    //            if (index >= 0)
    //    //            {
    //    //                this.SelectionEnd = index;
    //    //                SelectionStart = this.SelectionEnd;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //        else
    //    //        {
    //    //            this.SelectionEnd = -1;
    //    //            int index = 0;
    //    //            if (index >= 0)
    //    //            {
    //    //                this.SelectionStart = index;
    //    //                SelectionStart = this.SelectionEnd;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //    }
    //    //    return res;
    //    //}
    //    //private bool MoveEnd()
    //    //{
    //    //    bool res = false;
    //    //    if (this.strsb.Length > 0)
    //    //    {
    //    //        if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
    //    //        {
    //    //            if (this.SelectionEnd < 0)
    //    //            {
    //    //                this.SelectionEnd = this.SelectionStart;
    //    //            }
    //    //            int index = strsb.Length;
    //    //            if (index >= 0)
    //    //            {
    //    //                this.SelectionEnd = index;
    //    //                SelectionStart = this.SelectionEnd;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //        else
    //    //        {
    //    //            this.SelectionEnd = -1;
    //    //            int index = strsb.Length;
    //    //            if (index >= 0)
    //    //            {
    //    //                this.SelectionStart = index;
    //    //                SelectionStart = this.SelectionEnd;
    //    //                res = true;
    //    //            }
    //    //        }
    //    //    }
    //    //    return res;
    //    //}
    //    public virtual bool OnKeyDown(object sender, KeyEventArgs e)
    //    {
    //        try
    //        { 
    //            bool res = false;
    //            if (e.KeyCode == Keys.Home)
    //            {
    //                res = MoveHome();
    //            }
    //            if (e.KeyCode == Keys.End)
    //            {
    //                res = MoveEnd();
    //            }

    //            if (e.KeyCode == Keys.Delete)
    //            {
    //                Delete();
    //                res = true;
    //            }
    //            if (e.KeyCode == Keys.Enter)
    //            {
    //                if (!AllowEnter)
    //                {
    //                    Append('\n');
    //                    res = true;
    //                }
    //            }

    //            if (e.KeyCode == Keys.Right)
    //            {
    //                res = MoveRight();
    //            }
    //            if (e.KeyCode == Keys.Left)
    //            {
    //                res = MoveLeft();
    //            }
    //            if (e.KeyCode == Keys.Up)
    //            {
    //                res = MoveUp();
    //            }
    //            if (e.KeyCode == Keys.Down)
    //            {
    //                res = MoveDown();
    //            }
    //            if (e.KeyCode == Keys.V)
    //            {
    //                if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
    //                {
    //                    string text = Feng.Forms.ClipboardHelper.GetText();
    //                    this.Paste(text);
    //                    res = true;
    //                }
    //            }
    //            if (e.KeyCode == Keys.X)
    //            {
    //                if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
    //                {
    //                    this.Cut();
    //                    res = true;
    //                }
    //            }
    //            if (e.KeyCode == Keys.C)
    //            {
    //                if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
    //                {
    //                    this.Copy();
    //                    res = true;
    //                }
    //            }
    //            if (res)
    //            {
    //                GetTextSize(this.GetGraphics());
    //                this.ShowCaret();
    //            }
    //            return res;
    //        }
    //        catch (Exception ex)
    //        {
    //            Feng.Utils.ExceptionHelper.ShowError(ex);
    //        }
    //        return false;
    //    }


    //    public virtual bool ImeCharChanged(char c)
    //    { 
    //        Append(c);
    //        GetTextSize(this.GetGraphics());
    //        ShowCaret();
    //        return false;
    //    }

    //    StringBuilder strsb = new StringBuilder();

    //    public virtual void Append(char c)
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        switch (c)
    //        {
    //            case '\b':
    //                BackSpace();
    //                break;
    //            default:
    //                ClearSelect();
    //                Insert(c);
    //                break;
    //        }

    //        SelectionStart = SelectionStart;
    //    }
    //    public virtual void Append(string text)
    //    {
    //        Insert(text); 
    //    }
 
    //    public void Insert(char c)
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        int start = this.SelectionStart;
    //        if (this.Count < 1)
    //        {
    //            start = 0;
    //        }
    //        this.strsb.Insert(start, c);
    //        this.SelectionStart = start + 1;
    //    }
    //    public void Insert(string text)
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        int start = this.SelectionStart;
    //        if (this.Count < 1)
    //        {
    //            start = 0;
    //        }
    //        this.strsb.Insert(start, text);
    //        this.SelectionStart = start + text.Length;
    //    }
    //    public virtual int Count { get {
    //            return this.strsb.Length;
    //        } }
    //    public void Cut()
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        if (this.SelectionEnd > 0)
    //        {
    //            int min = Math.Min(this.SelectionStart, this.SelectionEnd);
    //            int max = Math.Max(this.SelectionStart, this.SelectionEnd);
    //            string text = this.strsb.ToString(min, max - min);
    //            this.strsb.Remove(min, max - min);
    //            this.SelectionEnd = -1;
    //            this.SelectionStart = min;
    //            Feng.Forms.ClipboardHelper.SetText(text);
    //        }
    //    }
    //    public void Copy()
    //    {
    //        if (this.SelectionEnd > 0 || this.SelectionStart > 0)
    //        {
    //            int min = Math.Min(this.SelectionStart, this.SelectionEnd);
    //            int max = Math.Max(this.SelectionStart, this.SelectionEnd);
    //            string text = this.strsb.ToString(min, max - min);
    //            Feng.Forms.ClipboardHelper.SetText(text);
    //        }
    //    }
    //    public void Paste()
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        ClearSelect();
    //        string text = Feng.Forms.ClipboardHelper.GetText();
    //        Insert(text);

    //        SelectionStart = SelectionStart;
    //    }
    //    public void Paste(string text)
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        ClearSelect();
    //        Insert(text);

    //        SelectionStart = SelectionStart;
    //    }
    //    public void BackSpace()
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        if (this.strsb.Length < 1)
    //            return;
    //        if (this.SelectionEnd < 0)
    //        {
    //            if (this.SelectionStart > 0)
    //            {
    //                this.SelectionStart = this.SelectionStart - 1;
    //                this.strsb.Remove(this.SelectionStart, 1);
    //            }
    //        }
    //        else
    //        {
    //            int min = Math.Min(this.SelectionStart, this.SelectionEnd);
    //            int max = Math.Max(this.SelectionStart, this.SelectionEnd);
    //            this.strsb.Remove(min, max - min);
    //            this.SelectionEnd = -1;
    //            this.SelectionStart = min;
    //        }
    //    }
    //    public void ClearSelect()
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        if (this.SelectionEnd >= 0)
    //        {
    //            if (this.SelectionStart >= 0)
    //            {
    //                int min = Math.Min(this.SelectionStart, this.SelectionEnd);
    //                int max = Math.Max(this.SelectionStart, this.SelectionEnd);
    //                this.strsb.Remove(min, max - min);
    //                this.SelectionEnd = -1;
    //                this.SelectionStart = min;
    //            }
    //        }
    //    }
    //    public void Delete()
    //    {
    //        if (this.ReadOnly)
    //            return;
    //        if (this.SelectionEnd < 0)
    //        {
    //            if (this.SelectionStart >= 0 && this.SelectionStart < this.strsb.Length)
    //            {
    //                this.strsb.Remove(this.SelectionStart, 1);
    //            }
    //        }
    //        else
    //        {
    //            int min = Math.Min(this.SelectionStart, this.SelectionEnd);
    //            int max = Math.Max(this.SelectionStart, this.SelectionEnd);
    //            this.strsb.Remove(min, max - min);
    //            this.SelectionEnd = -1;
    //            this.SelectionStart = min;
    //        }
    //    }
    //    private int _caretindex = 0;
    //    public virtual int SelectionStart
    //    {
    //        get
    //        {
    //            return _caretindex;
    //        }
    //        set
    //        {
    //            _caretindex = value;
    //        }
    //    }

    //    private List<RectangleF> TextRegion = null;
    //    public void ShowCaret()
    //    {
    //        Rectangle rect = GetCaretRect();
    //        if (TextHeight < rect.Height)
    //        {
    //            TextHeight = rect.Height;
    //        }
    //        Point pt = rect.Location; 
    //        this.ShowCaret(TextHeight, pt.X, pt.Y);
    //    }
    //    private int TextHeight = 0;
    //    private Rectangle GetCaretRect()
    //    {
    //        if (TextRegion != null)
    //        {
    //            RectangleF rectf = RectangleF.Empty;
    //            if (TextRegion.Count > 0)
    //            {
    //                if (SelectionStart < 0)
    //                {
    //                    SelectionStart = this.SelectionStart;
    //                }
    //                if (SelectionStart < TextRegion.Count)
    //                {
    //                    rectf = (TextRegion[SelectionStart]);

    //                }
    //                else
    //                {
    //                    RectangleF rf = TextRegion[TextRegion.Count - 1];
    //                    rectf = new RectangleF(rf.Right, rf.Top, rf.Width, rf.Height);
    //                }
    //                return Rectangle.Round(rectf);
    //            }
    //        }
    //        return Rectangle.Empty;
    //    }
    //    private int GetIndex(Point pt)
    //    {
    //        if (TextRegion != null)
    //        {
    //            for (int i = 0; i < TextRegion.Count; i++)
    //            {
    //                RectangleF rect = TextRegion[i];
    //                //if (rect.Contains(pt))
    //                //{
    //                //    return i;
    //                //}
    //                rect.Offset(rect.Width / 2 * -1, 0);
    //                if (rect.Contains(pt))
    //                {
    //                    return i;
    //                }
    //                rect.Offset(rect.Width, 0);
    //                if (rect.Contains(pt))
    //                {
    //                    return i + 1;
    //                }
    //            }
    //        }
    //        return -1;
    //    }
    //    private bool MoveCaretIndex(Point pt)
    //    {
    //        bool res = false;
    //        if (TextRegion != null)
    //        {
    //            for (int i = 0; i < TextRegion.Count; i++)
    //            {
    //                RectangleF rect = TextRegion[i];
    //                Rectangle rc = Rectangle.Round(rect);
    //                rc.X = rc.X + rc.Width / 2 * -1;
    //                if (rc.Contains(pt))
    //                {
    //                    SelectionStart = i;
    //                    this.SelectionStart = SelectionStart;
    //                    return true;
    //                }
    //                rc.X = rc.X + rc.Width;
    //                if (rc.Contains(pt))
    //                {
    //                    SelectionStart = i + 1;
    //                    this.SelectionStart = SelectionStart;
    //                    return true;
    //                }
    //                if (rc.Top < pt.Y && rc.Bottom > pt.Y)
    //                { 
    //                    SelectionStart = i+1;
    //                    this.SelectionStart = SelectionStart;
    //                    res = true;
    //                }
    //            }
    //        }
    //        return res; 
    //    }
 
    //    public void GetTextSize(Graphics g)
    //    {
    //        if (g == null)
    //            return;
    //        string text = this.strsb.ToString();
    //        string mtext = text;
    //        if (text.Length < 1)
    //        {
    //            mtext = "|";
    //        }
    //        else if (text[text.Length - 1] == '\n')
    //        {
    //            mtext = text + "|";
    //        }
    //        g.ResetClip();
    //        StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(this.HorizontalAlignment, this.VerticalAlignment, false);
    //        SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(this.ForeColor);
    //        SizeF sizef = SizeF.Empty;
    //        Rectangle rect = Rectangle.Empty;
    //        List<RectangleF> list = null;
    //        if (this.AutoMultiline)
    //        {
    //            sizef = g.MeasureString(mtext, this.Font, this.Width);
    //            rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
    //            if (sizef.Height > this.Height)
    //            {
    //                rect.Height = this.Height - this.Top;
    //                sf.LineAlignment = StringAlignment.Near;
    //            }
    //            rect.Offset(1, 1);
    //            rect.Inflate(-1, -1);
    //            list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, this.Font, sf, rect);
    //        }
    //        else
    //        {
    //            sf.FormatFlags = StringFormatFlags.NoWrap;
    //            sizef = g.MeasureString(mtext, this.Font, Point.Empty, sf);
    //            rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
    //            if (sizef.Height > this.Height)
    //            {
    //                rect.Height = this.Height - this.Top;
    //                sf.LineAlignment = StringAlignment.Near;
    //            }
    //            if (sizef.Width > this.Width)
    //            {
    //                rect.Width = this.Width - this.Left;
    //                sf.Alignment = StringAlignment.Near;
    //            }
    //            //rect.Offset(1, 1);
    //            //rect.Inflate(-1, -1);
    //            list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, this.Font, sf, rect);
    //        }
    //        TextRegion = list;
    //    }

    //    public virtual bool OnDraw(Feng.Drawing.GraphicsObject g)
    //    {
    //        if (this.EditControl == null)
    //        {
    //            this.EditControl = g.Control;
    //        } 
    //        string text = this .Text; 
    //        string mtext = text;
    //        if (text.Length < 1)
    //        {
    //            mtext = "|";
    //        }
    //        else if (text[text.Length - 1] == '\n')
    //        {
    //            mtext = text + "|";
    //        }
    //        g.Graphics.ResetClip();
    //        StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(this.HorizontalAlignment, this.VerticalAlignment, false);
    //        sf.Trimming = StringTrimming.None;
    //        sf.FormatFlags = sf.FormatFlags | StringFormatFlags.MeasureTrailingSpaces;
    //        SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(this.ForeColor);
    //        SizeF sizef = SizeF.Empty;
    //        Rectangle rect = Rectangle.Empty;
    //        if (this.AutoMultiline)
    //        {
    //            sizef = g.Graphics.MeasureString(mtext, this.Font, this.Width);
    //            rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
    //            if (sizef.Height > this.Height)
    //            {
    //                rect.Height = this.Height - this.Top;
    //                sf.LineAlignment = StringAlignment.Near;
    //            }
    //            //rect.Offset(1, 1);
    //            //rect.Inflate(-1, -1); 
    //        }
    //        else
    //        {
    //            sf.FormatFlags = StringFormatFlags.NoWrap;
    //            sizef = g.Graphics.MeasureString(mtext, this.Font, Point.Empty, sf);
    //            rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
    //            if (sizef.Height > this.Height)
    //            {
    //                rect.Height = this.Height - this.Top;
    //                sf.LineAlignment = StringAlignment.Near;
    //            }
    //            if (sizef.Width > this.Width)
    //            {
    //                rect.Width = this.Width - this.Left;
    //                sf.Alignment = StringAlignment.Near;
    //            }
    //            //rect.Offset(1, 1);
    //            //rect.Inflate(-1, -1); 
    //        }
    //        if (this.SelectionEnd >= 0)
    //        {
    //            int min = Math.Min(this.SelectionStart, this.SelectionEnd);
    //            int max = Math.Max(this.SelectionStart, this.SelectionEnd);
    //            Color selcolor = Color.FromArgb(100, Feng.Drawing.ColorHelper.MidColor(this.ForeColor, this.BackColor));
    //            SolidBrush selbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(selcolor);
    //            for (int i = min; i < max; i++)
    //            {
    //                if (i < TextRegion.Count)
    //                {
    //                    RectangleF recttext = TextRegion[i];
    //                    recttext.Offset(0, -1);
    //                    recttext.Inflate(0, 2);
    //                    Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, selbrush, recttext);
    //                }
    //            }
    //        }
    //        Feng.Drawing.GraphicsHelper.DrawString(g, text, this.Font, solidbrush, rect, sf);
    //        return true;

    //    }
    //    public void GetthisCharBounds(Graphics g)
    //    {
    //        string text = this.strsb.ToString();

    //        string mtext = text;
    //        if (text.Length < 1)
    //        {
    //            mtext = "|";
    //        }
    //        else if (text[text.Length - 1] == '\n')
    //        {
    //            mtext = text + "|";
    //        }
    //        g.ResetClip();
    //        StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(this.HorizontalAlignment, this.VerticalAlignment, false);
    //        SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(this.ForeColor);
    //        SizeF sizef = SizeF.Empty;
    //        Rectangle rect = Rectangle.Empty;
    //        List<RectangleF> list = null;
    //        if (this.AutoMultiline)
    //        {
    //            sizef = g.MeasureString(mtext, this.Font, this.Width);
    //            rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
    //            if (sizef.Height > this.Height)
    //            {
    //                rect.Height = this.Height - this.Top;
    //                sf.LineAlignment = StringAlignment.Near;
    //            }
    //            rect.Offset(1, 1);
    //            rect.Inflate(-1, -1);
    //            list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, this.Font, sf, rect);
    //        }
    //        else
    //        {
    //            sf.FormatFlags = StringFormatFlags.NoWrap;
    //            sizef = g.MeasureString(mtext, this.Font, Point.Empty, sf);
    //            rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
    //            if (sizef.Height > this.Height)
    //            {
    //                rect.Height = this.Height - this.Top;
    //                sf.LineAlignment = StringAlignment.Near;
    //            }
    //            if (sizef.Width > this.Width)
    //            {
    //                rect.Width = this.Width - this.Left;
    //                sf.Alignment = StringAlignment.Near;
    //            }
    //            //rect.Offset(1, 1);
    //            //rect.Inflate(-1, -1);
    //            list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, this.Font, sf, rect);
    //        }
    //        TextRegion = list;
    //    }

    //    private bool ContentsHasPoint(Point pt)
    //    {
    //        if (TextRegion != null)
    //        {
    //            foreach (RectangleF recft in TextRegion)
    //            {
    //                if (recft.Contains(pt))
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //        return false;
    //    }

    //    private Size lastcaretsize = Size.Empty;

    //    public virtual bool DrawthisBack(object sender, Feng.Drawing.GraphicsObject g)
    //    {
    //        return false;
    //    }

    //    public virtual string Text
    //    {
    //        get
    //        {
    //            return this.strsb.ToString();
    //        }
    //        set
    //        {
    //            this.SelectionStart = 0;
    //            this.SelectionEnd = 0;
    //            this.strsb.Length = 0;
    //            this.strsb.Append(value);
    //            if (this.EditControl != null)
    //            {
    //                this.EditControl.Invalidate();
    //            }
    //        }
    //    }
    //    public int SelectionEnd { get; set; }

    //    private Font _font = null;
    //    [Browsable(true)]
    //    [Category(CategorySetting.Design)]
    //    public Font Font
    //    {
    //        get
    //        {
    //            if (this._font == null)
    //            {
    //                return this.EditControl.Font;
    //            }
    //            return _font;
    //        }
    //        set
    //        {
    //            _font = value;
    //        }
    //    }


    //    private int _top = 0;
    //    [Browsable(true)]
    //    public virtual int Top
    //    {
    //        get
    //        {
    //            return _top;
    //        }

    //        set
    //        {
    //            _top = value;
    //        }
    //    }
    //    private int _left = 0;
    //    [Browsable(true)]
    //    public int Left
    //    {
    //        get
    //        {
    //            return _left;
    //        }
    //        set
    //        {
    //            _left = value;
    //        }
    //    }
    //    [Browsable(true)]
    //    public int Bottom
    //    {
    //        get { return Top + this.Height; }
    //    }
    //    [Browsable(true)]
    //    public int Right
    //    {
    //        get { return Left + this.Width; }
    //    }
    //    private int _width = 0;
    //    [Browsable(true)]
    //    public int Width
    //    {
    //        get
    //        {
    //            return _width;
    //        }
    //        set { _width = value; }
    //    }
    //    private int _height = 0;
    //    [Browsable(true)]
    //    public int Height
    //    {
    //        get
    //        {
    //            return _height;
    //        }
    //        set
    //        {
    //            this._height = value;
    //        }
    //    }

    //    [Browsable(true)]
    //    public Rectangle Rect
    //    {
    //        get
    //        {
    //            Rectangle rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
    //            return rect;
    //        }
    //    }

    //    private bool _AutoMultiline = false;
    //    /// <summary>
    //    /// 是否自动绘制多行。True时绘制多行，False时不绘制多行。
    //    /// </summary>
    //    [DefaultValue(true)]
    //    [Browsable(true)]
    //    [Category(CategorySetting.Design)]
    //    public virtual bool AutoMultiline
    //    {
    //        get { return this._AutoMultiline; }

    //        set { this._AutoMultiline = value; }
    //    }
         
    //    private static Feng.Forms.Caret Caret = null;
    //    public virtual void ShowCaret(int heigth, int x, int y)
    //    {
    //        if (Caret == null)
    //        {
    //            Caret = new Feng.Forms.Caret();
    //            Caret.Handle = EditControl.Handle;
    //        }
    //        if (Caret != null)
    //        {
    //            Caret.Show(EditControl.Handle, heigth, x, y);
    //        }
    //    }

    //    public Control EditControl
    //    {
    //        get; set;
    //    }

    //    private StringAlignment _HorizontalAlignment = StringAlignment.Near;
    //    [Browsable(true)]
    //    [Category(CategorySetting.Design)]
    //    public virtual StringAlignment HorizontalAlignment
    //    {
    //        get
    //        {
    //            return _HorizontalAlignment;
    //        }
    //        set
    //        {
    //            _HorizontalAlignment = value;
    //        }
    //    }

    //    private StringAlignment _VerticalAlignment = StringAlignment.Center;
    //    [Browsable(true)]
    //    [Category(CategorySetting.Design)]
    //    public virtual StringAlignment VerticalAlignment
    //    {
    //        get
    //        {
    //            return _VerticalAlignment;
    //        }
    //        set
    //        {
    //            _VerticalAlignment = value;
    //        }
    //    }

    //    private Color _ForeColor = Color.Empty;
    //    [Browsable(true)]
    //    [Category(CategorySetting.Design)]
    //    public Color ForeColor
    //    {
    //        get
    //        {
    //            if (this._ForeColor == Color.Empty)
    //            {
    //                return this.EditControl.ForeColor;
    //            }

    //            return _ForeColor;
    //        }
    //        set
    //        {
    //            _ForeColor = value;
    //        }
    //    }

    //    private Color _BackColor = Color.Empty;
    //    [Browsable(true)]
    //    [Category(CategorySetting.Design)]
    //    public Color BackColor
    //    {
    //        get
    //        {
    //            return _BackColor;
    //        }
    //        set
    //        {
    //            _BackColor = value;
    //        }
    //    }

    //    public virtual void FreshContens()
    //    {
    //        if (string.IsNullOrWhiteSpace(this.Text))
    //            return;
    //        Graphics g = this.GetGraphics();
    //        StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
    //        sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
    //        if (this.DirectionVertical)
    //        {
    //            sf.FormatFlags = sf.FormatFlags | StringFormatFlags.DirectionVertical;
    //        }
    //        Size Size = Feng.Utils.ConvertHelper.ToSize(g.MeasureString(this.Text + "A", this.Font, Point.Empty, sf));
    //        this.Width = Size.Width;
    //        this.Height = Size.Height;
    //    }

    //    private bool _DirectionVertical = false;
    //    [Browsable(true)]
    //    [Category(CategorySetting.Design)]
    //    public virtual bool DirectionVertical
    //    {
    //        get
    //        {
    //            return _DirectionVertical;
    //        }
    //        set
    //        {
    //            _DirectionVertical = value;
    //        }
    //    }
    //    private Graphics graphics = null;
    //    public Graphics GetGraphics()
    //    {
    //        if (this.EditControl == null)
    //            return null;
    //        if (graphics == null)
    //        {
    //            graphics = this.EditControl.CreateGraphics();
    //        }
    //        return graphics;
    //    }


    //    private bool _readonly = false;
    //    [Browsable(true)]
    //    [Category(CategorySetting.Design)]
    //    public virtual bool ReadOnly
    //    {
    //        get
    //        {
    //            return _readonly;
    //        }
    //        set
    //        {
    //            _readonly = value;
    //        }
    //    }
    //}
}
