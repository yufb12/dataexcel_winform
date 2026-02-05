using Feng.Data;
using Feng.Excel.Actions;
using Feng.Excel.Commands;
using Feng.Excel.Interfaces;
using Feng.Forms.Base;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Views;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [ToolboxItem(false)]
    public class CellEdit : CellBaseEdit, IEdit, IImeCharChanged, IActionList
    {
        public CellEdit(DataExcel grid)
            : base(grid)
        {
        }
        public override string ShortName { get { return "CellEdit"; } set { } }

        public bool MoveToNext(Keys keydata)
        {
            if (keydata == Keys.Tab)
            {
                this.Cell.Grid.MoveFocusedCellToRightCell();
            }
            if (keydata == Keys.Right)
            {
                if (this.SelectionStart >= this.Text.Length)
                {
                    this.Cell.Grid.MoveFocusedCellToRightCell();
                    return true;
                }
            }
            if (keydata == Keys.Left)
            {
                if (this.SelectionStart < 1)
                {
                    this.Cell.Grid.MoveFocusedCellToLeftCell();
                    return true;
                }
            }
            if (keydata == Keys.Up)
            {
                //if (this.Lines.Length < 2)
                //{
                //    this.Cell.Grid.MoveFocusedCellToUpCell();
                //    return true;
                //}
                //int start = this.SelectionStart;
                //int lastindex = this.Lines[0].Length;

                //if (start <= lastindex)
                //{
                //    this.Cell.Grid.MoveFocusedCellToUpCell();
                //    return true;
                //}
            }
            if (keydata == Keys.Down)
            {
                //if (this.Lines.Length < 2)
                //{
                //    this.Cell.Grid.MoveFocusedCellToDownCell();
                //    return true;
                //}
                //int start = this.SelectionStart;
                //int lastindex = this.Text.Length - this.Lines[0].Length;

                //if (start >= lastindex)
                //{
                //    this.Cell.Grid.MoveFocusedCellToDownCell();
                //    return true;
                //}
            }
            return false;
        }
        public virtual void OnTextChanged(EventArgs e)
        {
            if (this.Cell != null)
            {
                this.Cell.Grid.OnCellEditControlValueChanged(this.Cell, this.Text);
                if (!string.IsNullOrWhiteSpace(this.PropertyTextChanged))
                {
                    ActionArgs ae = new ActionArgs(this.PropertyTextChanged, this.Grid, this.Cell);
                    ae.Arg = e;
                    this.Grid.ExecuteAction(ae, this.PropertyTextChanged, "Text", this.Text);
                    if (ae.Handle)
                    {
                    }
                }
            }
        }
        public override bool InitEdit(object obj)
        {
            if (this.InEdit)
                return false;
            TextHeight = 0;
            ICell cell = obj as ICell;
            if (cell == null)
                return false;

            Cell = cell;
            if (this.Cell == null)
                return false;
            base.InitEdit(cell);
            if (!string.IsNullOrEmpty(cell.Expression))
            {
                string txt = "=" + cell.Expression;
                this.strsb.Length = 0;
                this.strsb.Append(txt);
            }
            else
            {
                this.strsb.Length = 0;
                this.strsb.Append(cell.Text);
            }
            this.TextRegion = null;
            this.Grid.AddEdit(this);
            this.SelectAll();


            cell.Grid.Invalidate();
            GetCellCharBounds(cell, cell.Grid.GetGraphics());
            this.ShowCaret(cell);
            return true;
        }

        private void SelectAll()
        {
            this.SelectionStart = 0;
            this.SelectionEnd = this.strsb.Length;
            //this.SelectionStart = this.SelectionEnd;
        }
        public override void EndEdit()
        {
            try
            {

                if (this.Cell == null)
                    return;
                if (this.Grid.CanUndoRedo)
                {
                    CellValueCommand cmd = new CellValueCommand();
                    cmd.Value = this.Cell.Value;
                    cmd.Text = this.Cell.Text;
                    cmd.Cell = this.Cell;
                    this.Grid.Commands.Add(cmd);
                }

                string text = this.strsb.ToString();
                if (this.Grid.AlliowInputCode)
                {
                    if (text.StartsWith("=") && text.Length > 1)
                    {
                        if (Feng.Utils.TextHelper.StartEqual(text,3,"=@1"))
                        {
                            ICell cell = this.Grid.GetLeftCell(this.Cell);
                            if (cell == null)
                                return;

                            text = cell.Text;
                            this.Cell.ID = text;
                            this.Grid.EndEditClear();
                            this.Cell = null;
                            return;
                        }
                        else if (Feng.Utils.TextHelper.StartEqual(text, 3, "=@2"))
                        { 
                            ICell cell = this.Grid.GetRightCell(this.Cell);
                            if (cell == null)
                                return;
                            text = cell.Text;
                            this.Grid.EndEditClear();
                            this.Cell = null;
                            return;
                        }
                        else if (Feng.Utils.TextHelper.StartEqual(text, 3, "=@3"))
                        { 
                            ICell cell = this.Grid.GetAboveCell(this.Cell);
                            if (cell == null)
                                return;
                            text = cell.Text;
                            this.Cell.ID = text;
                            this.Grid.EndEditClear();
                            this.Cell = null;
                            return;
                        }
                        else if (Feng.Utils.TextHelper.StartEqual(text, 3, "=@4"))
                        { 
                            ICell cell = this.Grid.GetDownCell(this.Cell);
                            if (cell == null)
                                return;
                            text = cell.Text;
                            this.Cell.ID = text;
                            this.Grid.EndEditClear();
                            this.Cell = null;
                            return;
                        }
                        else if (this.Cell.AllowInputExpress == Feng.Forms.Base.YesNoInhert.Yes)
                        {
                            text = text.Substring(1, text.Length - 1);
                            this.Cell.Expression = text;
                            this.Grid.EndEditClear(); 
                            this.Cell = null;
                            return;
                        }
                    }
                    else if (text.StartsWith("\\="))
                    {
                        text = text.Substring(1, text.Length - 1);
                    }
                }
                this.Cell.Expression = string.Empty;
                this.Cell.Value = text;
                this.Cell.FreshContens();
                if (!this.Cell.IsMergeCell)
                {
                    if (this.Cell.ContensWidth > this.Grid.DefaultColumnWidth)
                    {
                        this.Grid.RefreshColumnWidth(this.Cell.Column);
                    }
                }
                this.Grid.EndEditClear();
                this.Cell = null;

                this.Grid.ClearCaretEdit();
                this.SelectionStart = 0;
                this.SelectionEnd = 0;
                this.strsb.Length = 0;
                this.TextRegion = null;
            }
            catch (Exception ex)
            {
                BugReport.Log(ex);
                this.Grid.OnException(ex);
            }
            finally
            {
                base.EndEdit();
            }
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (!this.InEdit)
                    return false;
                downpoint = Point.Empty;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnMouseUp(sender, e, ve);
        }
        private Point downpoint = Point.Empty;
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (!this.InEdit)
                    return false;
                Point viewloaction = this.Grid.PointControlToView(e.Location);
                downpoint = viewloaction;
                bool incell = MoveCaretIndex(viewloaction);
                this.SelectionEnd = -1;
                IBaseCell cell = sender as IBaseCell;
                if (cell == null)
                    return false;
                if (Feng.Forms.MouseDoubleClick.Default.CheckDoubleClick(viewloaction))
                {
                    cell.Grid.BeginReFresh();
                    this.SelectAll();
                    cell.Grid.EndReFresh();
                    return true;
                }
                if (incell)
                {
                    ShowCaret(cell);
                    cell.Grid.Invalidate();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnMouseDown(sender, e, ve);
        }
        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (!this.InEdit)
                    return false;
                IBaseCell cell = sender as IBaseCell;
                if (cell == null)
                    return false;
                if (e.Button == MouseButtons.Left)
                {
                    Point viewloaction = this.Grid.PointControlToView(e.Location);
                    downpoint = viewloaction;
                    int index = GetIndex(downpoint);
                    if (index != this.SelectionStart)
                    {
                        this.SelectionEnd = index;
                    }
                    else
                    {
                        this.SelectionEnd = -1;
                    }

                }
                cell.Grid.BeginReFresh();
                cell.Grid.EndReFresh();
                cell.Grid.Invalidate();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnMouseMove(sender, e, ve);
        }
        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (!this.InEdit)
                    return false;
                IBaseCell cell = sender as IBaseCell;
                if (cell == null)
                    return false;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return true;
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
        private bool MoveUp()
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
        private bool MoveDown()
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
        private bool MoveRight()
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
        private bool MoveLeft()
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
        private bool MoveHome()
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
        private bool MoveEnd()
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
        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            try
            {
                if (!this.InEdit)
                    return false;
                IBaseCell cell = sender as IBaseCell;
                bool res = false;
                if (cell != null)
                {
                    cell.Grid.BeginReFresh();

                    if (e.KeyCode == Keys.Home)
                    {
                        res = MoveHome();
                        res = true;
                    }
                    if (e.KeyCode == Keys.End)
                    {
                        res = MoveEnd();
                        res = true;
                    }

                    if (e.KeyCode == Keys.Delete)
                    {
                        Delete();
                        OnTextChanged(new EventArgs());
                        res = true;
                    }
                    if (e.KeyCode == Keys.Enter)
                    {
                        Append("\r\n");
                        OnTextChanged(new EventArgs());
                        res = true;
                    }

                    if (e.KeyCode == Keys.Right)
                    {
                        res = MoveRight();
                    }
                    if (e.KeyCode == Keys.Left)
                    {
                        res = MoveLeft();
                    }
                    if (e.KeyCode == Keys.Up)
                    {
                        res = MoveUp();
                    }
                    if (e.KeyCode == Keys.Down)
                    {
                        res = MoveDown();
                    }
                    if (e.KeyCode == Keys.V)
                    {
                        if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                        {
                            string text = Feng.Forms.ClipboardHelper.GetText();
                            this.Paste(text);
                            res = true;
                        }
                    }
                    if (e.KeyCode == Keys.X)
                    {
                        if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                        {
                            this.Cut();
                            res = true;
                        }
                    }
                    if (e.KeyCode == Keys.C)
                    {
                        if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                        {
                            this.Copy();
                            res = true;
                        }
                    }
                    cell.Grid.EndReFresh();
                    if (res)
                    {
                        GetCellCharBounds(cell, cell.Grid.GetGraphics());
                        this.ShowCaret(cell);
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.OnKeyDown(sender, e, ve);
        }
        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }
        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0);
                this._PropertyTextChanged = bw.ReadIndex(2, _PropertyTextChanged);
            }
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

                using (Feng.Excel.IO.BinaryWriter bw = new Feng.Excel.IO.BinaryWriter())
                {
                    bw.Write(1, this.AddressID);
                    bw.Write(2, this._PropertyTextChanged);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public virtual bool ImeCharChanged(IBaseCell cell, char c)
        {
            if (!this.InEdit)
                return false;
            Append(c);
            GetTextSize(cell, cell.Grid.GetGraphics());
            ShowCaret(cell);
            GetCellCharBounds(cell, cell.Grid.GetGraphics());
            OnTextChanged(new EventArgs());
            return true;
        }

        StringBuilder strsb = new StringBuilder();
        public virtual void Append(char c)
        {
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
            ClearSelect();
            Insert(text);

            SelectionStart = SelectionStart;
        }
        public void Insert(char c)
        {
            if (BeforeTextChanged())
                return;
            this.strsb.Insert(this.SelectionStart, c);
            this.SelectionStart = this.SelectionStart + 1;
        }
        public void Insert(string text)
        {
            if (BeforeTextChanged())
                return;
            this.strsb.Insert(this.SelectionStart, text);
            this.SelectionStart = this.SelectionStart + text.Length;
        }
        public virtual void Cut()
        {
            if (BeforeTextChanged())
                return;
            
            int min = Math.Min(this.SelectionStart, this.SelectionEnd);
            int max = Math.Max(this.SelectionStart, this.SelectionEnd);
            if (max - min < 1)
            {
                min = 0;
                max = this.strsb.Length;
            } 
            string text = this.strsb.ToString(min, max - min);
            this.strsb.Remove(min, max - min);
            this.SelectionEnd = -1;
            this.SelectionStart = min;
            OnTextChanged(new EventArgs());
            Feng.Forms.ClipboardHelper.SetText(text);
        }
        public virtual void Copy()
        {
            if (BeforeTextChanged())
                return;

            int min = Math.Min(this.SelectionStart, this.SelectionEnd);
            int max = Math.Max(this.SelectionStart, this.SelectionEnd);
            if (max - min < 1)
            {
                min = 0;
                max = this.strsb.Length;
            }
            string text = this.strsb.ToString(min, max - min);
            Feng.Forms.ClipboardHelper.SetText(text);

        }
        public void Paste(string text)
        {
            if (BeforeTextChanged())
                return;
            Append(text);
            OnTextChanged(new EventArgs());
        }
        public void BackSpace()
        {
            if (BeforeTextChanged())
                return;
            if (this.SelectionEnd < 0)
            {
                if (this.SelectionStart > 0)
                {
                    this.SelectionStart = this.SelectionStart - 1;
                    char c = this.strsb[this.SelectionStart];
                    if (c == '\n')
                    {
                        this.strsb.Remove(this.SelectionStart, 1);
                        if (this.strsb.Length > 1)
                        {
                            c = this.strsb[this.SelectionStart - 1];
                            if (c == '\r')
                            {
                                this.SelectionStart = this.SelectionStart - 1;
                                this.strsb.Remove(this.SelectionStart, 1);
                            }
                            string text = this.Text;
                            return;
                        }
                    }
                    else if (this.strsb.Length < 1)
                    {
                        return;
                    }
                    this.strsb.Remove(this.SelectionStart, 1);
                }
            }
            else
            {
                int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                this.strsb.Remove(min, max - min);
                this.SelectionEnd = -1;
                this.SelectionStart = min;
            }
        }
        public void ClearSelect()
        {
            if (this.SelectionEnd >= 0)
            {
                if (this.SelectionStart >= 0)
                {
                    int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                    int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                    int len = max - min;
                    if (this.strsb.Length > 0)
                    {
                        this.strsb.Remove(min, len);
                    }
                    this.SelectionEnd = -1;
                    this.SelectionStart = min;
                }
            }
        }
        public virtual bool BeforeTextChanged()
        {
            return false;
        }
        public virtual bool AfterTextChanged()
        {
            return false;
        }
        public void Delete()
        {
            if (BeforeTextChanged())
                return;
            if (this.SelectionEnd < 0)
            {
                if (this.SelectionStart >= 0 && this.SelectionStart < this.strsb.Length)
                {
                    this.strsb.Remove(this.SelectionStart, 1);
                }
            }
            else
            {
                int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                this.strsb.Remove(min, max - min);
                this.SelectionEnd = -1;
                this.SelectionStart = min;
            }
            AfterTextChanged();
        }

        public virtual void TextChanged()
        {

        }
        private int _caretindex = 0;
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
        public void ShowCaret(IBaseCell cell)
        {
            Rectangle rect = GetCaretRect();
            if (TextHeight < rect.Height)
            {
                TextHeight = rect.Height;
            }
            Point pt = cell.Grid.PointViewToControl(rect.Location);
            cell.Grid.ShowCaret(TextHeight, pt.X, pt.Y);
        }
        private int TextHeight = 0;
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
                        rectf = TextRegion[SelectionStart];

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
                int lastindex = -1;
                Rectangle lastrc = Rectangle.Empty;
                int endindex = -1;
                for (int i = 0; i < TextRegion.Count; i++)
                {
                    if (i == 60)
                    {

                    }
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
                            lastrc.Width = this.Cell.Right - lastrc.Right;
                            if (lastrc.Contains(pt))
                            {
                                endindex = lastindex; 
                                return endindex;
                            }
                        }
                    }
                    rc = Rectangle.Round(rect);
                    lastrc = rc;
                    rc.X = rc.X + rc.Width / 2 * -1;
                    lastindex = i;
                    if (rc.Contains(pt))
                    {
                        endindex = i; 
                        return endindex;
                    }

                    rc.X = rc.X + rc.Width;
                    if (rc.Contains(pt))
                    {
                        endindex = i + 1; 
                        return endindex;
                    }
                    if (i == TextRegion.Count - 1)
                    {
                        lastrc.Width = this.Cell.Right - lastrc.Right;
                        if (lastrc.Contains(pt))
                        {
                            endindex = lastindex + 1; 
                            return endindex;
                        }
                    }
                }
            }
            return -1;
        }
        private bool MoveCaretIndex(Point pt)
        {

            if (strsb.Length == 0)
            {
                this.SelectionStart = 0;
                return false;
            }
            if (TextRegion != null)
            {
                int lastindex = -1;
                Rectangle lastrc = Rectangle.Empty;
                for (int i = 0; i < TextRegion.Count; i++)
                {
                    if (i == 60)
                    {

                    }
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
                            lastrc.Width = this.Cell.Right - lastrc.Right;
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
                        lastrc.Width = this.Cell.Right - lastrc.Right;
                        if (lastrc.Contains(pt))
                        {
                            SelectionStart = lastindex + 1;
                            this.SelectionStart = SelectionStart;
                            return true;
                        }
                    }
                }

            }
            return false;
        }
        public void GetCellCharBounds(IBaseCell cell, Graphics g)
        {


            bool res = true;
            if (cell == this.Cell)
            {
                res = false;
            }
            else
            {
                ICell cell2 = cell as ICell;
                if (cell2 != null)
                {
                    if (cell2.OwnMergeCell == this.Cell)
                    {
                        res = false;
                    }

                }
            }
            if (res)
            {
                return;
            }
            string text = this.strsb.ToString();
            text = DoText(text);
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
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(cell.HorizontalAlignment, cell.VerticalAlignment, false);
            SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(cell.ForeColor);
            SizeF sizef = SizeF.Empty;
            Rectangle rect = Rectangle.Empty;
            List<RectangleF> list = null;
            if (cell.AutoMultiline)
            {
                sizef = g.MeasureString(mtext, cell.Font, cell.Width);
                rect = new Rectangle(cell.Left, cell.Top, cell.Width, cell.Height);
                if (sizef.Height > cell.Height)
                {
                    rect.Height = cell.Grid.Height - cell.Top;
                    sf.LineAlignment = StringAlignment.Near;
                }
                rect.Offset(1, 1);
                rect.Inflate(-1, -1);
                list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, cell.Font, sf, rect);
            }
            else
            {
                sf.FormatFlags = StringFormatFlags.NoWrap;
                sizef = g.MeasureString(mtext, cell.Font, Point.Empty, sf);
                rect = new Rectangle(cell.Left, cell.Top, cell.Width, cell.Height);
                if (sizef.Height > cell.Height)
                {
                    rect.Height = cell.Grid.Height - cell.Top;
                    sf.LineAlignment = StringAlignment.Near;
                }
                if (sizef.Width > cell.Width)
                {
                    rect.Width = cell.Grid.Width - cell.Left;
                    sf.Alignment = StringAlignment.Near;
                }
                //rect.Offset(1, 1);
                //rect.Inflate(-1, -1);
                list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, cell.Font, sf, rect);
            }
            TextRegion = list;

        }

        public void GetTextSize(IBaseCell cell, Graphics g)
        {
            if (cell == this.Cell)
            {
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

                StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(cell.HorizontalAlignment, cell.VerticalAlignment, false);
                SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(cell.ForeColor);
                SizeF sizef = SizeF.Empty;
                Rectangle rect = Rectangle.Empty;
                List<RectangleF> list = null;
                if (cell.AutoMultiline)
                {
                    sizef = g.MeasureString(mtext, cell.Font, cell.Width);
                    rect = new Rectangle(cell.Left, cell.Top, cell.Width, cell.Height);
                    if (sizef.Height > cell.Height)
                    {
                        rect.Height = cell.Grid.Height - cell.Top;
                        sf.LineAlignment = StringAlignment.Near;
                    }
                    rect.Offset(1, 1);
                    rect.Inflate(-1, -1);
                    list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, cell.Font, sf, rect);
                }
                else
                {
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    sizef = g.MeasureString(mtext, cell.Font, Point.Empty, sf);
                    rect = new Rectangle(cell.Left, cell.Top, cell.Width, cell.Height);
                    if (sizef.Height > cell.Height)
                    {
                        rect.Height = cell.Grid.Height - cell.Top;
                        sf.LineAlignment = StringAlignment.Near;
                    }
                    if (sizef.Width > cell.Width)
                    {
                        rect.Width = cell.Grid.Width - cell.Left;
                        sf.Alignment = StringAlignment.Near;
                    }
                    rect.Offset(1, 1);
                    rect.Inflate(-1, -1);
                    list = Feng.Drawing.MeasureStringHelper.MeasureString(g, mtext, cell.Font, sf, rect);
                }
                TextRegion = list;
            }
        }

        public virtual string DoText(string text)
        {
            return text;
        }

        public static Color GetMidColor(Color color1, Color color2)
        {
            // 获取两个颜色的RGB分量  
            int r1 = color1.R;
            int g1 = color1.G;
            int b1 = color1.B;

            int r2 = color2.R;
            int g2 = color2.G;
            int b2 = color2.B;

            // 计算每个颜色通道的中间值  
            int rMid = (r1 + r2) / 2;
            int gMid = (g1 + g2) / 2;
            int bMid = (b1 + b2) / 2;

            // 确保颜色值在有效范围内（0-255）  
            rMid = Math.Min(Math.Max(rMid, 0), 255);
            gMid = Math.Min(Math.Max(gMid, 0), 255);
            bMid = Math.Min(Math.Max(bMid, 0), 255);

            // 创建一个新的Color对象  
            return Color.FromArgb(128, rMid, gMid, bMid);
        }

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            if (!this.InEdit)
            {
                return false;
            }

            if (cell == this.Cell)
            {
                if (cell.Grid.celldrawEdit == null)
                { 
                    cell.Grid.celldrawEdit = cell;
                    cell.Grid.celldrawEdit2 = this;
                    return true;
                }
                string text = this.strsb.ToString();//= DoText(text);
                string mtext = text;
                if (text.Length < 1)
                {
                    mtext = "|";
                }
                else if (text[text.Length - 1] == '\n')
                {
                    mtext = text;
                }
                g.Graphics.ResetClip();
                StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(cell.HorizontalAlignment, cell.VerticalAlignment, false);
                sf.Trimming = StringTrimming.None;
                sf.FormatFlags = sf.FormatFlags | StringFormatFlags.MeasureTrailingSpaces;
                Color forecolor = cell.ForeColor;
                if (forecolor == Color.Empty)
                {
                    forecolor = Color.Black;
                }
                SolidBrush solidbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(forecolor);
                Brush solidbrushbakc = Feng.Drawing.SolidBrushCache.GetSolidBrush(cell.BackColor);
                if (cell.BackColor.IsEmpty)
                {
                    solidbrushbakc = Brushes.White;
                }
                SizeF sizef = SizeF.Empty;
                Rectangle rect = Rectangle.Empty;
                if (cell.AutoMultiline)
                {
                    sizef = g.Graphics.MeasureString(mtext, cell.Font, cell.Width);
                    rect = new Rectangle(cell.Left, cell.Top, cell.Width, cell.Height);
                    if (sizef.Height > cell.Height)
                    {
                        rect.Height = cell.Grid.Height - cell.Top;
                        sf.LineAlignment = StringAlignment.Near;
                    }
                }
                else
                {
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    sizef = g.Graphics.MeasureString(mtext, cell.Font, Point.Empty, sf);
                    rect = new Rectangle(cell.Left, cell.Top, cell.Width, cell.Height);
                    if (sizef.Height > cell.Height)
                    {
                        rect.Height = (int)sizef.Height + 30;
                        sf.LineAlignment = StringAlignment.Near;
                    }
                    if (sizef.Width > cell.Width)
                    {
                        rect.Width = (int)sizef.Width + 70;
                        sf.Alignment = StringAlignment.Near;
                    }
                }
                Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, solidbrushbakc, rect);
                if (this.SelectionEnd >= 0)
                {
                    if (TextRegion.Count > 0)
                    {
                        int min = Math.Min(this.SelectionStart, this.SelectionEnd);
                        int max = Math.Max(this.SelectionStart, this.SelectionEnd);
                        Color selcolor = GetMidColor(forecolor, cell.BackColor);
                        SolidBrush selbrush = Feng.Drawing.SolidBrushCache.GetSolidBrush(selcolor);
                        for (int i = min; i < max; i++)
                        {
                            Feng.Drawing.GraphicsHelper.FillRectangle(g.Graphics, selbrush, TextRegion[i]);
                        }
                    }
                }
                TextRegion = Feng.Drawing.MeasureStringHelper.MeasureString(g.Graphics, mtext, cell.Font, sf, rect);
                Feng.Drawing.GraphicsHelper.DrawString(g, text, cell.Font, solidbrush, rect, sf);
                //foreach (RectangleF rectangle in TextRegion)
                //{
                //    g.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(rectangle));
                //}
                return true;
            }
            return false;
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

        private Size lastcaretsize = Size.Empty;

        public override bool DrawCellBack(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        { 
            //g.Graphics.FillRectangle(Brushes.Red, this.Bounds);
            return false;
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellEdit celledit = new CellEdit(grid);
            return celledit;
        }

        public override string Text
        {
            get
            {
                return this.strsb.ToString();
            }
            set
            {
                this.strsb.Length = 0;
                this.strsb.Append(value);
            }
        }

        public int SelectionEnd { get; set; }
        private string _PropertyTextChanged = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyTextChanged
        {
            get
            {
                return _PropertyTextChanged;
            }
            set
            {
                _PropertyTextChanged = value;

            }
        }
        private TextChangedAction cellendeditaction = null;
        public virtual TextChangedAction CellEndEditAction
        {
            get
            {
                if (cellendeditaction == null)
                {
                    cellendeditaction = new TextChangedAction(this);
                }
                return cellendeditaction;
            }
        }

        public virtual List<PropertyAction> GetActiones()
        {
            List<PropertyAction> list = new List<PropertyAction>();
            list.Add(CellEndEditAction);
            return list;
        }
    }
    public class TextChangedAction : PropertyAction
    {
        public CellEdit Edit { get; set; }
        public TextChangedAction(CellEdit edit) : base()
        {
            Edit = edit;
        }
        public override string ActionName { get { return "CellEditTextChanged"; } set { } }
        public override string Descript { get { return "编辑框文本值改变"; } set { } }
        public override string ShortName { get { return "CellEditTextChanged"; } set { } }
        public override string Script
        {
            get
            {
                return Edit.PropertyTextChanged;
            }
            set { Edit.PropertyTextChanged = value; }
        }
    }
}
