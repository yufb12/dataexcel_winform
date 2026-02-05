//using Feng.Data;
//using Feng.Excel.Actions;
//using Feng.Excel.Commands;
//using Feng.Excel.Interfaces;
//using Feng.Forms.Base;
//using Feng.Forms.Controls.Designer;
//using Feng.Forms.Views;
//using Feng.Utils;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Drawing.Design;
//using System.Text;
//using System.Windows.Forms;

//namespace Feng.Excel.Edits
//{
//    [ToolboxItem(false)]
//    public class CellEdit : CellBaseEdit, IEdit, IImeCharChanged, IActionList
//    {
//        public CellEdit(DataExcel grid)
//            : base(grid)
//        {
//        }
//        public override string ShortName { get { return "CellEdit"; } set { } }
//        private static EditView editview = null;
//        private EditView EditView
//        {
//            get
//            {
//                if (editview == null)
//                {
//                    editview = new EditView();
//                }
//                return editview;
//            }
//        }
//        public bool MoveToNext(Keys keydata)
//        {
//            if (keydata == Keys.Tab)
//            {
//                this.Cell.Grid.MoveFocusedCellToRightCell();
//            }
//            if (keydata == Keys.Right)
//            {
//                if (this.EditView.SelectionStart >= this.Text.Length)
//                {
//                    this.Cell.Grid.MoveFocusedCellToRightCell();
//                    return true;
//                }
//            }
//            if (keydata == Keys.Left)
//            {
//                if (this.EditView.SelectionStart < 1)
//                {
//                    this.Cell.Grid.MoveFocusedCellToLeftCell();
//                    return true;
//                }
//            }
//            if (keydata == Keys.Up)
//            {
//                //if (this.Lines.Length < 2)
//                //{
//                //    this.Cell.Grid.MoveFocusedCellToUpCell();
//                //    return true;
//                //}
//                //int start = this.SelectionStart;
//                //int lastindex = this.Lines[0].Length;

//                //if (start <= lastindex)
//                //{
//                //    this.Cell.Grid.MoveFocusedCellToUpCell();
//                //    return true;
//                //}
//            }
//            if (keydata == Keys.Down)
//            {
//                //if (this.Lines.Length < 2)
//                //{
//                //    this.Cell.Grid.MoveFocusedCellToDownCell();
//                //    return true;
//                //}
//                //int start = this.SelectionStart;
//                //int lastindex = this.Text.Length - this.Lines[0].Length;

//                //if (start >= lastindex)
//                //{
//                //    this.Cell.Grid.MoveFocusedCellToDownCell();
//                //    return true;
//                //}
//            }
//            return false;
//        }
//        public virtual void OnTextChanged(EventArgs e)
//        {
//            if (this.Cell != null)
//            {
//                this.Cell.Grid.OnCellEditControlValueChanged(this.Cell, this.Text);
//                if (!string.IsNullOrWhiteSpace(this.PropertyTextChanged))
//                {
//                    ActionArgs ae = new ActionArgs(this.PropertyTextChanged, this.Grid, this.Cell);
//                    ae.Arg = e;
//                    this.Grid.ExecuteAction(ae, this.PropertyTextChanged, "Text", this.Text);
//                    if (ae.Handle)
//                    {
//                    }
//                }
//            }
//        }
//        public override bool InitEdit(object obj)
//        {
//            ICell cell = obj as ICell;
//            if (cell == null)
//                return false;

//            Cell = cell;
//            if (this.Cell == null)
//                return false;
//            this._inedit = true;
//            this.AddView(this.EditView);
//            this.EditView.Width = cell.Width;
//            this.EditView.Height = cell.Height;
//            this.EditView.Font = cell.Font;
//            this.EditView.ForeColor = cell.ForeColor;
//            this.EditView.BackColor = cell.BackColor;
//            if (!string.IsNullOrEmpty(cell.Expression))
//            {
//                string txt = "=" + cell.Expression;
//                this.EditView.Text = txt;
//            }
//            else
//            {
//                this.EditView.Text = cell.Text;
//            }
//            this.Grid.AddEdit(this);
//            this.EditView.SelectAll();


//            cell.Grid.Invalidate();
//            return true;
//        }

//        public override void EndEdit()
//        {
//            try
//            {
//                this._inedit = true;
//                this.RemoveView(this.EditView);
//                if (this.Cell == null)
//                    return;
//                if (this.Grid.CanUndoRedo)
//                {
//                    CellValueCommand cmd = new CellValueCommand();
//                    cmd.Value = this.Cell.Value;
//                    cmd.Text = this.Cell.Text;
//                    cmd.Cell = this.Cell;
//                    this.Grid.Commands.Add(cmd);
//                }

//                string text = this.EditView.Text;
//                if (text.StartsWith("=") && text.Length > 1)
//                {
//                    if (this.Cell.AllowInputExpress == Feng.Forms.Base.YesNoInhert.Yes)
//                    {
//                        text = text.Substring(1, text.Length - 1);
//                        this.Cell.Expression = text;
//                        this.Grid.EndEditClear();
//                        this.Grid.EndEdit();
//                        this.Cell = null;
//                        return;
//                    }
//                }
//                else if (text.StartsWith("\\="))
//                {
//                    text = text.Substring(1, text.Length - 1);
//                }
//                this.Cell.Expression = string.Empty;
//                this.Cell.Value = text;
//                this.Cell.FreshContens();
//                if (!this.Cell.IsMergeCell)
//                {
//                    if (this.Cell.ContensWidth > this.Grid.DefaultColumnWidth)
//                    {
//                        this.Grid.RefreshColumnWidth(this.Cell.Column);
//                    }
//                }
//                this.Grid.EndEditClear();
//                this.Cell = null;
//                this.Grid.ClearEdit();
//            }
//            catch (Exception ex)
//            {
//                BugReport.Log(ex);
//                this.Grid.OnException(ex);
//            }
//            finally
//            {
//                base.EndEdit();
//            }
//        }
//        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
//        {
//            try
//            {
//                if (!this.InEdit)
//                    return false;
//                downpoint = Point.Empty;
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//            return base.OnMouseUp(sender, e, ve);
//        }
//        private Point downpoint = Point.Empty;
//        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
//        {
//            try
//            {
//                if (!this.InEdit)
//                    return false;
//                return base.OnMouseDown(sender, e, ve);
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//            return base.OnMouseDown(sender, e, ve);
//        }
//        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
//        {
//            try
//            {
//                if (!this.InEdit)
//                    return false;
//                IBaseCell cell = sender as IBaseCell;
//                if (cell == null)
//                    return false;
//                return base.OnMouseMove(sender, e, ve);
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//            return base.OnMouseMove(sender, e, ve);
//        }

//        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
//        {
//            if (cell == this.Cell)
//            {
//                return true;
//            }
//            bool res = false;
//            try
//            {
//                if (!string.IsNullOrWhiteSpace(cell.Text))
//                {
//                    System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
//                    g.Graphics.TranslateTransform(cell.Left, cell.Top);
//                    g.Graphics.SetClip(cell.Rect);
//                    this.EditView.Text = cell.Text;
//                    base.OnDraw(this, g);
//                    g.Graphics.Restore(gs);
//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "CellEdit", "DrawCell", ex);
//            }

//            return res;
//        }

//        public override ICellEditControl Clone(DataExcel grid)
//        {
//            CellEdit celledit = new CellEdit(grid);
//            return celledit;
//        }
//        public override void Read(DataExcel grid, int version, DataStruct data)
//        {
//            ReadDataStruct(data);
//        }
//        public override void ReadDataStruct(DataStruct data)
//        {
//            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
//            {
//                this.AddressID = bw.ReadIndex(1, 0);
//                this._PropertyTextChanged = bw.ReadIndex(2, _PropertyTextChanged);
//            }
//        }
//        [Browsable(false)]
//        public override DataStruct Data
//        {
//            get
//            {
//                Type t = this.GetType();
//                DataStruct data = new DataStruct()
//                {
//                    DllName = this.DllName,
//                    Version = this.Version,
//                    AessemlyDownLoadUrl = this.DownLoadUrl,
//                    FullName = t.FullName,
//                    Name = t.Name,
//                };

//                using (Feng.Excel.IO.BinaryWriter bw = new Feng.Excel.IO.BinaryWriter())
//                {
//                    bw.Write(1, this.AddressID);
//                    bw.Write(2, this._PropertyTextChanged);
//                    data.Data = bw.GetData();
//                }
//                return data;
//            }
//        }
//        private string _PropertyTextChanged = string.Empty;
//        [Browsable(true)]
//        [Category(CategorySetting.PropertyEvent)]
//        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
//        public virtual string PropertyTextChanged
//        {
//            get
//            {
//                return _PropertyTextChanged;
//            }
//            set
//            {
//                _PropertyTextChanged = value;

//            }
//        }
//        private TextChangedAction cellendeditaction = null;
//        public virtual TextChangedAction CellEndEditAction
//        {
//            get
//            {
//                if (cellendeditaction == null)
//                {
//                    cellendeditaction = new TextChangedAction(this);
//                }
//                return cellendeditaction;
//            }
//        }

//        public virtual List<PropertyAction> GetActiones()
//        {
//            List<PropertyAction> list = new List<PropertyAction>();
//            list.Add(CellEndEditAction);
//            return list;
//        }

//        public virtual bool ImeCharChanged(IBaseCell cell, char c)
//        {
//            if (!this.InEdit)
//                return false;
//            this.EditView.ImeCharChanged(c);
//            //Append(c);
//            //GetTextSize(cell, cell.Grid.GetGraphics());
//            //ShowCaret(cell);
//            //GetCellCharBounds(cell, cell.Grid.GetGraphics());
//            //OnTextChanged(new EventArgs());
//            return false;
//        }
//    }
//    public class TextChangedAction : PropertyAction
//    {
//        public CellEdit Edit { get; set; }
//        public TextChangedAction(CellEdit edit) : base()
//        {
//            Edit = edit;
//        }
//        public override string ActionName { get { return "CellEditTextChanged"; } set { } }
//        public override string Descript { get { return "编辑框文本值改变"; } set { } }
//        public override string ShortName { get { return "CellEditTextChanged"; } set { } }
//        public override string Script
//        {
//            get
//            {
//                return Edit.PropertyTextChanged;
//            }
//            set { Edit.PropertyTextChanged = value; }
//        }
//    }
//}
