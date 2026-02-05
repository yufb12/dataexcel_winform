using Feng.Excel.App;
using Feng.Excel.Args;
using Feng.Excel.Base;
using Feng.Excel.Collections;
using Feng.Excel.Designer;
using Feng.Excel.ExcelLicense;
using Feng.Excel.Forms;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Excel.Script;
using Feng.Excel.Styles;
using Feng.Forms;
using Feng.Forms.Interface;
using Feng.Forms.Views;
using Feng.Script.FunctionContainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
namespace Feng.Excel
{
     
    [Designer(typeof(DataExcelDesigner))]
    [DefaultProperty("FileName"), DefaultEvent("CellClick")] 
    [Guid(Feng.Excel.App.Product.AssemblyControlGuid)]
    [LicenseProvider(typeof(DataExcelLicenseProvider))] 
    //[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public partial class DataExcel : Feng.Forms.Views.DivView, IObjectSafety, IAllowInputExpress, IInit, IIOFileData, IViewID
    {
        #region 系统方法

        static DataExcel()
        {
            //Feng.Excel.Test.StaticTest.TestFile();
            //Feng.Excel.ExcelLicense.DataExcelLicense exLicense = LicenseManager.Validate(typeof(DataExcel), 0) as DataExcelLicense;
            //if (exLicense == null || exLicense.LicType == DataExcelLicenseType.Trial)
            //{
            //    AboutBox.Show();
            //}
        }

        public DataExcel()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.ContainerControl, true);
            //this.SetStyle(ControlStyles.StandardClick, true);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.Width = 800;
            this.Height = 600;
            //this.UpdateStyles();
            //#warning Move Befor Publish
            //            Init(); 
        }

        private void InitDefault()
        {
            this.BackgroundImage = __BackgroundImage;
            this.BackgroundImageLayout = __BackgroundImageLayout;
            //this.HScroller.Visible = this.ShowHorizontalScroller;
            //this.VScroller.Visible = this.ShowVerticalScroller;
        }



        ~DataExcel()
        {
            try
            {
                this.Clear();
            }
            catch (Exception)
            {
            }
        }

        public List<ICellEditControl> GetCellEditList()
        {
            List<ICellEditControl> list = new List<ICellEditControl>();
            foreach (IRow row in this.Rows)
            {
                foreach (IColumn column in this.Columns)
                {
                    ICell cell = row[column];
                    if (cell != null)
                    {
                        if (cell.OwnEditControl != null)
                        {
                            if (!list.Contains(cell.OwnEditControl))
                            {
                                list.Add(cell.OwnEditControl);
                            }
                        }
                    }
                }
            }
            return list;
        }

        #endregion

        #region 初 始 化

        private void InitRows()
        {
            this.BeginSetFirstDisplayRowIndex();

            IRow row = null;

            int rowindex = -100;
            if (!this.Rows.Contains(rowindex))
            {
                row = ClassFactory.CreateDefaultRow(this, rowindex);
                this.Rows.Add(row);
                row.Visible = false;
                row.Height = this.DefaultRowHeight;
            }

            rowindex = -2;
            if (!this.Rows.Contains(rowindex))
            {
                row = ClassFactory.CreateDefaultRow(this, rowindex);
                this.Rows.Add(row);
                row.Height = 0;
            }

            rowindex = -1;
            if (!this.Rows.Contains(rowindex))
            {
                row = ClassFactory.CreateDefaultRow(this, rowindex);
                this.Rows.Add(row);
                row.Height = 0;
            }
            rowindex = 0;
            if (!this.Rows.Contains(rowindex))
            {
                row = ClassFactory.CreateDefaultRow(this, rowindex);
                row.Top = 0;
                row.Height = this.DefaultRowHeight+5;
                this.Rows.Add(row);
            }
            rowindex = 50;
            if (!this.Rows.Contains(rowindex))
            {
                row = ClassFactory.CreateDefaultRow(this, rowindex);
                this.Rows.Add(row);
            }
            this.EndSetFirstDisplayRowIndex();
        }

        private void InitColumns()
        {
            this.BeginSetFirstDisplayColumnIndex();
            for (int columnindex = 0; columnindex < 5; columnindex++)
            {
                IColumn column = null;
                if (!this.Columns.Contains(columnindex))
                {
                    column = ClassFactory.CreateDefaultColumn(this, columnindex);
                    this.Columns.Add(column);
                }
            }
            this.EndSetFirstDisplayColumnIndex();
        }

        public void InitExtend()
        {
            //ReticleDrawExtend extend = new ReticleDrawExtend();
            //extend.Init(this);
        }
#if DEBUG
        private int initTimes = 0;
#endif
        public override void Init()
        {
#if DEBUG
            initTimes = initTimes + 1;
            if (initTimes > 1)
            {
                Feng.IO.LogHelper.Log("initTimes", "initTimes:" + initTimes);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Init", "initTimes", "initTimes:" + initTimes);
            }
#endif
            _IsInit = true;
            this.Font = new Font("Tahoma", 9);
            if (BeforeInit != null)
            {
                CancelEventArgs c = new CancelEventArgs();
                BeforeInit(this, c);
                if (c.Cancel)
                {
                    return;
                }
            }
            if (ClassFactory == null)
            {
                ClassFactory = new DefultClassFactory(this);
            }
            InitDefaultMulitKeyAction();
            //this.DefaultCursor = new Cursor(Application.StartupPath + "\\DataExcel.CUR");
            Rows = ClassFactory.CreateDefaultRows(this);
            Columns = ClassFactory.CreateDefaultColumns(this);
            InitRows();
            InitColumns();
            if (this.FunctionCells == null)
            {
                this.FunctionCells = ClassFactory.CreateDefaultFunctionCells(this);
            }
            if (this.Methods == null)
            {
                this.Methods = ClassFactory.CreateDefaultMethods(this);
                //FUNCTION
                this.Methods.Add(new ScriptFunctionContainer());
                this.Methods.Add(new StringFunctionContainer());
                this.Methods.Add(new DataExcelFunctionContainer());
                this.Methods.Add(new DataExcelRowFunctionContainer());
                this.Methods.Add(new DataExcelColumnFunctionContainer());
                this.Methods.Add(new DataExcelCellFunctionContainer());
                this.Methods.Add(new CellEditGridViewFunctionContainer());
                this.Methods.Add(new CellEditTreeViewFunctionContainer());
                //this.Methods.Add(new CellEditFunctionContainer());
                //this.Methods.Add(new DataExcelFileFunctionContainer());
                //this.Methods.Add(new DataExcelTableFunctionContainer());
                this.Methods.Add(new DateTimeFunctionContainer());
                this.Methods.Add(new SelectFunctionContainer());
                this.Methods.Add(new WebServiceFunctionContainer());
                this.Methods.Add(new JsonFunctionContainer());
                this.Methods.Add(new XMLFunctionContainer());
                this.Methods.Add(new FileFunctionContainer());
                this.Methods.Add(new ConvertFunctionContainer());
                this.Methods.Add(new MathematicsFunctionContainer());
                this.Methods.Add(new TrigonometricFunctionContainer()); 
                this.Methods.Add(new ListFunctionContainer());
                this.Methods.Add(new DataTableFunctionContainer()); 
                this.Methods.Add(new SqlServerFunctionContainer());
                this.Methods.Add(new FormFunctionContainer());
                this.Methods.Add(new StatisticsFunctionContainer());
                this.Methods.Add(new StyleFunctionContainer());
                //this.Methods.Add(new ConsoleFunctionContainer());
                this.Methods.Add(new ReflectionContainer());
                this.Methods.Add(new DataExcelStatisticsFunctionContainer());
                //this.Methods.Add(new DebugFunctionContainer());


            }
             
            this._VerticalRuler = new View.VerticalRuler(this);
            this._HorizontalRuler = new View.HorizontalRuler(this);

            if (Inited != null)
            {
                Inited(this, new EventArgs());
            }
            if (!this.InDesign)
            {
                _createtime = DateTime.Now;
                _dataexcel = this;
            }
            InitExtend(); 
        }


        void HScroller_ValueChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    int position = HScroller.Value;
            //    if (position < this.Columns.Max)
            //    {
            //        this.SetFirstColumnShowIndex(position);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Feng.Utils.ExceptionHelper.ShowError(ex);
            //}


        }

        //void VScroller_ValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int position = VScroller.Value;
        //        if (position < this.Rows.Max)
        //        {
        //            this.SetFirstRowShowIndex(position);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Feng.Utils.ExceptionHelper.ShowError(ex);
        //    }
        //}

        //void HScroller_DownArrowAreaClick(object sender, MouseEventArgs e)
        //{
        //    this.FirstDisplayedColumnIndex = this.FirstDisplayedColumnIndex + 1;
        //}

        //void HScroller_UpArrowAreaClick(object sender, MouseEventArgs e)
        //{
        //    this.FirstDisplayedColumnIndex = this.FirstDisplayedColumnIndex - 1;
        //}

        //void VScroller_DownArrowAreaClick(object sender, MouseEventArgs e)
        //{
        //    this.FirstDisplayedRowIndex = this.FirstDisplayedRowIndex + 1;
        //}

        //void VScroller_UpArrowAreaClick(object sender, MouseEventArgs e)
        //{
        //    this.FirstDisplayedRowIndex = this.FirstDisplayedRowIndex - 1;
        //}
  

        #endregion

        #region 版    权

        [DebuggerHidden]
        internal void LegalCopyCheck()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                return;
            }
            if (this.InDesign)
            {
                return;
            }
            Feng.EventHelper.LegalCopyCheckEventHandler hl = new Feng.EventHelper.LegalCopyCheckEventHandler(LegalCopyCheck);
            hl.BeginInvoke(this, new EventArgs(), LegalCopyCheckAsyncCallback, hl);
        }

        [DebuggerHidden]
        internal void LegalCopyCheck(object sender, EventArgs e)
        {
            if (!DataExcel.GenuineValidation())
            {
                Feng.Excel.App.AboutBox.Show();
            }
        }

        [DebuggerHidden]
        internal void LegalCopyCheckAsyncCallback(IAsyncResult ar)
        {
            Feng.EventHelper.LegalCopyCheckEventHandler hl = (Feng.EventHelper.LegalCopyCheckEventHandler)ar.AsyncState;
            hl.EndInvoke(ar);
        }

        #endregion

        #region 系统函数
        public void ShowCell(ICell cell)
        { 
            this.BeginReFresh();
            int showcolumnindex= (cell.Column.Index - 5);
            int showrowindex = (cell.Row.Index - 5);
            if (showrowindex < 1)
            {
                showrowindex = 1;
            }
            if (showcolumnindex < 1)
            {
                showcolumnindex = 1;
            }
            this.FirstDisplayedColumnIndex = showcolumnindex;
            this.FirstDisplayedRowIndex = showrowindex;
            this.FocusedCell = cell;
            RefreshRowHeaderWidth(); 
            this.EndReFresh();
        }
        public void ShowCell(ICell precell, ICell nextcell)
        {
            if (precell == null)
            {
                precell = this[1, 1];
            }
            if (nextcell == null)
            {
                return;
            }
            if (precell == nextcell)
            {
                return;
            }
            if (precell.Row.Index == nextcell.Row.Index)
            {
                if ((precell.Column.Index - nextcell.Column.Index) == 1)
                {
                    if (this.FirstDisplayedColumnIndex > nextcell.Column.Index)
                    {
                        this.SetFirstDisplayColumn(this.FirstDisplayedColumnIndex - 1);
                    }
                }
                else if ((precell.Column.Index - nextcell.Column.Index) == -1)
                {
                    if (this.VisibleColumns != null)
                    {
                        if (this.VisibleColumns.Count > 0)
                        {
                            if ((this.VisibleColumns[this.VisibleColumns.Count - 1].Index) < nextcell.Column.Index)
                            {
                                this.SetFirstDisplayColumn(this.FirstDisplayedColumnIndex + 1);
                            }
                        }
                    }

                }
            }
            else
            {
                if ((precell.Row.Index - nextcell.Row.Index) == 1)
                {
                    if (this.FirstDisplayedRowIndex > nextcell.Row.Index)
                    {
                        this.SetFirstRowShowIndex(this.FirstDisplayedRowIndex - 1);
                    }
                }
                else if ((precell.Row.Index - nextcell.Row.Index) == -1)
                {
                    if (this.VisibleRows != null)
                    {
                        if (this.VisibleRows.Count > 0)
                        {
                            if ((this.VisibleRows[this.VisibleRows.Count - 1].Index) < nextcell.Row.Index)
                            {
                                this.SetFirstRowShowIndex(this.FirstDisplayedRowIndex + 1);
                            }
                        }
                    }

                }
            }
        }
        public virtual void Delete()
        {
            List<ICell> list = this.GetSelectCells();
            foreach (ICell cel in list)
            {
                if (!cel.ReadOnly)
                {
                    this.BeginReFresh();
                    cel.Value = null;
                    cel.Text = (string.Empty);
                    this.EndReFresh();
                }
            }
            this.EndEdit();
        }
        public virtual void ClearContents()
        {
            this.ListFilter = null;
            if (this.MergeCells != null)
            {
                this.MergeCells.Clear();
            }
            if (this.BackCells != null)
            {
                this.BackCells.Clear();
            }
            for (int i = 1; i < this.Rows.Max; i++)
            {
                this.Rows.RemoveAt(i);
            }
            //if (this.Rows != null)
            //{
            //    this.Rows.Clear();
            //}
            if (this.FunctionCells != null)
            {
                this.FunctionCells.Clear();
            }
            if (this.ListExtendCells != null)
            {
                this.ListExtendCells.Clear();
            }

            this.CellEdits.Clear();
            this.CellSaveEdits.Clear();
            this._FirstDisplayedRowIndex = 1;
            this._FirstDisplayedColumnIndex = 1;
        }
 
        public virtual void InitEdit()
        {
            this.CopyCells = null;
        }
        public virtual void EndEditClear()
        {
            if (this.EditCell != null)
            {
                this.EditCell.FreshContens();
            }
            this.EditCell = null;
            this.ReFresh();
        }
        public virtual void Cancel()
        {
            this.BeginReFresh();
            DoubleClear(); 
            this.EndEdit();
            this.EndReFresh();
        }
        public virtual void New()
        {
            this.Clear();
            this.Init();
            this.ReFreshFirstDisplayRowIndex();
            this.OnNew();
        }
        public virtual void Clear()
        {  
            this.BookMarkList.Clear();
            this.FocsedCellList.Clear();
            this.SelectCells = null;
            this.Selectmode = SelectMode.Null;
            this._password = string.Empty;
            this._filename = string.Empty;
            this.ListFilter = null;
            this.FilterExcel = null;

            if (this.ExpressionCells != null)
            {
                this.ExpressionCells.Clear();
            }
            if (this.MergeCells != null)
            {
                this.MergeCells.Clear();
            }
            if (this.BackCells != null)
            {
                this.BackCells.Clear();
            }
            if (this.Rows != null)
            {
                this.Rows.Clear();
            }
            if (this.Columns != null)
            {
                this.Columns.Clear();
            }
            if (this.FunctionCells != null)
            {
                this.FunctionCells.Clear();
            }
            if (this.ListExtendCells != null)
            {
                this.ListExtendCells.Clear();
            }
            if (this.FieldCells != null)
            {
                this.FieldCells.Clear();
            }
            this.IDCells.Clear();
            this.BackgroundImage = null;
            this._code = string.Empty;
            this.UserDefineExtensData.Clear();
            this.CellEdits.Clear();
            this._ICellEvents = null;
            this.CellSaveEdits.Clear();
            this._DataSource = null;
            this._datamember = null;
            this._FirstDisplayedRowIndex = 1;
            this._FirstDisplayedColumnIndex = 1;
            this._showgridcolumnline = true;
            this._showgridrowline = true;
            this._ShowSelectBorder = true;
            this._showselectaddrect = true;
            this._password = string.Empty;
            this._maxRow = int.MaxValue;
            this._maxColumn = MAXCOLUMNINDEX;
            this._showgridrowline = true;
            this._showrowheader = true;
            this._showgridrowline = true;
            this._showgridcolumnline = true;
            this._showcolumnheader = true;
            this._focusedcell = null;
            this.VisibleColumns.Clear();
            this.VisibleRows.Clear();
            this.ReFresh();
        }
        public Feng.Collections.ListEx<string> CopyText = new Feng.Collections.ListEx<string>();
        public virtual string GetCopyText()
        {

            try
            { 
                CopyCells = null;
                if (this.InEdit)
                    return string.Empty;
                Feng.Forms.ClipboardHelper.Clear();
                StringBuilder sb = new StringBuilder();
                List<ICell> list = this.GetSelectCells();
                IRow row = null;
                foreach (ICell cell in list)
                {
                    if (row == null)
                    {
                        row = cell.Row;
                    }
                    else if (row != cell.Row)
                    {
                        row = cell.Row;
                        sb.Append(ConstantValue.CopySplitSymbolRow);
                    }
                    string celltext = string.Empty;
                    celltext = cell.Text;
                    sb.Append(celltext);
                    if (cell != list[list.Count - 1])
                    {
                        sb.Append(ConstantValue.CopySplitSymbolColumn);
                    }

                }


                string text = this.OnCopying(sb.ToString());
                CopyText.Add(text);
                if (this.SelectCells != null)
                {
                    CopyCells = new SelectCellCollection();
                    CopyCells.BeginCell = this.SelectCells.BeginCell;
                    CopyCells.EndCell = this.SelectCells.EndCell;
                }
                //ClipboardHelper.SetClip(text);
                return (text);
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }
            return string.Empty;
        }
        public virtual void CopyNew(string text, byte[] data)
        {
            //var dataObject = new DataObject();
            //dataObject.SetData(DataFormats.Text, true, text);
            //dataObject.SetData("COPYCELLS", false, data);
            //Clipboard.SetDataObject(dataObject, true);
            ClipboardHelper.SetClipDataObject(text, "COPYCELLS", data);
        }
        public virtual void Copy()
        {

            try
            {
                string text = GetCopyText();
                byte[] data = GetCopyData();

                CopyNew(text, data);
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }
        }
        public struct CopyStruct
        {
            public List<ICell> list { get; set; }
            public List<IMergeCell> merges { get; set; }
            public List<ICellEditControl> edits { get; set; }
        }

        public virtual byte [] GetCopyData()
        {
            List<ICell> list = this.GetSelectCells();
            int count = list.Count;
            byte[] copydata = null;
            int minrow = int.MaxValue;
            int mincolumn = int.MaxValue;
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            { 
                int editaddress = 1;
                List<IMergeCell> merges = new List<IMergeCell>();
                List<ICellEditControl> edits = new List<ICellEditControl>();
                for (int i = 0; i < count; i++)
                {
                    ICell cell = list[i];
                    if (cell != null)
                    {
                        if (cell is IMergeCell)
                        { 
                            cell = (cell as IMergeCell).BeginCell;
                        }
                    }
                    if (minrow >cell.Row.Index)
                    {
                        minrow = cell.Row.Index;
                    }
                    if (mincolumn > cell.Column.Index)
                    {
                        mincolumn = cell.Column.Index;
                    }
                    if (cell.OwnEditControl == null)
                        continue;
                    if (!edits.Contains(cell.OwnEditControl))
                    {
                        cell.OwnEditControl.AddressID = editaddress;
                        edits.Add(cell.OwnEditControl);
                        editaddress++;
                    } 
                }
                count = edits.Count;
                bw.Write(count);
                for (int i = 0; i < count; i++)
                {
                    ICellEditControl cell = edits[i];
                    bw.Write((cell as ICellEditControl).GetType().FullName);
                    bw.Write((cell as ICellEditControl).Data);
                }
                count = list.Count;
                bw.Write(count);
                bw.Write(minrow);
                bw.Write(mincolumn);
                for (int i = 0; i < count; i++)
                {
                    ICell cell = list[i];
                    if (cell != null)
                    {
                        if (cell is IMergeCell)
                        {
                            merges.Add(cell as IMergeCell);
                            cell = (cell as IMergeCell).BeginCell;
                        }
                        else
                        {
                            if (cell.OwnMergeCell != null)
                            {
                                merges.Add(cell.OwnMergeCell);
                            }
                        }
                    }
                    bw.Write(cell.Row.Index);
                    bw.Write(cell.Column.Index);
                    bw.Write(cell.Data);
                    if (cell.OwnEditControl == null)
                    {
                        bw.Write(-1);
                    }
                    else
                    {
                        if (cell.OwnEditControl.GetType() != this.DefaultEdit.GetType())
                        {
                            bw.Write(cell.OwnEditControl.AddressID);
                        }
                        else
                        {
                            bw.Write(-1);
                        }
                    }
                }
                count = merges.Count;
                bw.Write(count);
                for (int i = 0; i < count; i++)
                {
                    IMergeCell cell = merges[i];
                    bw.Write(cell.BeginCell.Row.Index);
                    bw.Write(cell.BeginCell.Column.Index);
                    bw.Write(cell.EndCell.Row.Index);
                    bw.Write(cell.EndCell.Column.Index);
                    bw.Write(cell.Data);
                }
                copydata = bw.GetData();
            }
            return (copydata);
            //Feng.Forms.ClipboardHelper.SetData("COPYCELLS", copydata);

        }
        private ICell GetPasteCell(int beginrowindex, int begincolumnindex, int firstrowindex,
            int firstcolumnindex, int datarowindex, int datacolumnindex)
        {
            int rowindex = beginrowindex+ datarowindex - firstrowindex;
            int columnindex = begincolumnindex+ datacolumnindex - firstcolumnindex;
            return this[rowindex, columnindex];
        }

        public void RefreshMaxHasValueRow(int index)
        {
            if (this.Rows.MaxHasValueIndex < index)
            {
                this.Rows.MaxHasValueIndex = index;
            }
        }
        public void RefreshMaxHasValueColumn(int index)
        {
            if (this.MaxHasValueColumn < index)
            {
                this.MaxHasValueColumn = index;
            }
        }
        public virtual void PasteCell(ICell cell, byte[] data,bool pastevalue)
        {
            int count = 0;
            int beginrowindex = cell.Row.Index;
            int begincolumnindex = cell.Column.Index;
            int firstrowindex = 0;
            int firstcolumnindex = 0;
 
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data))
            {
                List<ICellEditControl> editControls = new List<ICellEditControl>();
                count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string editname = reader.ReadString();
                    ICellEditControl cellEditControl = GetCellEdit(this, editname);
                    if (cellEditControl == null)
                    {
                        cellEditControl = this.Edits[editname];
                        if (cellEditControl != null)
                        {
                            cellEditControl = cellEditControl.Clone(this);
                        }
                    }
                    Feng.Data.DataStruct datastruct = reader.ReadDataStruct();
                    IReadDataStruct readDataStruct = cellEditControl as IReadDataStruct;
                    if (readDataStruct != null)
                    {
                        readDataStruct.ReadDataStruct(datastruct);
                        editControls.Add(cellEditControl);
                    }

                }
                count = reader.ReadInt32();
                firstrowindex = reader.ReadInt32();
                firstcolumnindex = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    int datarowindex = reader.ReadInt32();
                    int datacolumnindex = reader.ReadInt32();
                    ICell targetcell = GetPasteCell(beginrowindex, begincolumnindex, firstrowindex,
                      firstcolumnindex, datarowindex, datacolumnindex);
                    Feng.Data.DataStruct datastruct = reader.ReadDataStruct();


                    int addressid = reader.ReadInt32();
                    IReadDataStruct readDataStruct = targetcell as IReadDataStruct;
                    if (datastruct != null)
                    {
                        datastruct.ReadValue = pastevalue;
                    }
                    IRow row = targetcell.Row;
                    IColumn column = targetcell.Column;
                    if (readDataStruct != null)
                    {
                        readDataStruct.ReadDataStruct(datastruct);
                    }
                    targetcell.Row = row;
                    targetcell.Column = column;
                    RefreshMaxHasValueRow(row.Index);
                    RefreshMaxHasValueColumn(column.Index);
                    foreach (ICellEditControl item in editControls)
                    {
                        if (item.AddressID == addressid)
                        {
                            targetcell.OwnEditControl = item;
                            item.Cell = targetcell;
                        }
                    }
                }
                count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    int databeginrowindex = reader.ReadInt32();
                    int databegincolumnindex = reader.ReadInt32();
                    ICell targetbegincell = GetPasteCell(beginrowindex, begincolumnindex, firstrowindex,
                      firstcolumnindex, databeginrowindex, databegincolumnindex);

                    int dataendrowindex = reader.ReadInt32();
                    int dataendcolumnindex = reader.ReadInt32();
                    ICell targetendcell = GetPasteCell(beginrowindex, begincolumnindex, firstrowindex,
                      firstcolumnindex, dataendrowindex, dataendcolumnindex);
                    IMergeCell mergeCell = this.MergeCell(targetbegincell, targetendcell);

                    Feng.Data.DataStruct datastruct = reader.ReadDataStruct();
                    if (mergeCell != null)
                    {
                        IReadDataStruct readDataStruct = mergeCell as IReadDataStruct;
                        readDataStruct.ReadDataStruct(datastruct);
                        mergeCell.Refresh();
                    }
                }
            } 

        }
        public virtual void CopyFormat()
        {

            try
            {
                if (this.SelectCells == null)
                {
                    return;
                }
                Feng.Forms.ClipboardHelper.Clear();
                ICell cellmin = this.SelectCells.MinCell;
                ICell cellmax = this.SelectCells.MaxCell;
                StringBuilder sb = new StringBuilder();
                for (int i = cellmin.Row.Index; i <= cellmax.Row.Index; i++)
                {
                    for (int j = cellmin.Column.Index; j <= cellmax.Column.Index; j++)
                    {
                        ICell cell = this[i, j];
                        string celltext = string.Empty;
                        if (cell != null)
                        {
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            if (cell.AllowCopy)
                            {
                                celltext = (cell.Text);
                            }
                        }
                        sb.Append(celltext);
                        sb.Append(ConstantValue.CopySplitSymbolColumn);
                    }

                    sb.Append(ConstantValue.CopySplitSymbolRow);

                }
                string text = this.OnCopying(sb.ToString());
                CopyText.Add(text);
                ClipboardHelper.SetClip(text);
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }
        }
        public virtual void CopyID()
        {

            try
            {
                if (this.SelectCells == null)
                {
                    return;
                }
                Feng.Forms.ClipboardHelper.Clear();
                ICell cellmin = this.SelectCells.MinCell;
                ICell cellmax = this.SelectCells.MaxCell;
                StringBuilder sb = new StringBuilder();
                for (int i = cellmin.Row.Index; i <= cellmax.Row.Index; i++)
                {
                    for (int j = cellmin.Column.Index; j <= cellmax.Column.Index; j++)
                    {
                        ICell cell = this[i, j];
                        string celltext = string.Empty;
                        if (cell != null)
                        {
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            if (cell.AllowCopy)
                            {
                                celltext = (cell.Name);
                            }
                        }
                        sb.Append(celltext);
                        sb.Append(ConstantValue.CopySplitSymbolColumn);
                    }

                    sb.Append(ConstantValue.CopySplitSymbolRow);

                }
                string text = this.OnCopying(sb.ToString());
                CopyText.Add(text);
                ClipboardHelper.SetClip(text);
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }
        }

        public virtual void Cut()
        {
            try
            {
                if (this.SelectCells == null)
                {
                    return;
                }
                Feng.Forms.ClipboardHelper.Clear();
                ICell cellmin = this.SelectCells.MinCell;
                ICell cellmax = this.SelectCells.MaxCell;
                StringBuilder sb = new StringBuilder();
                for (int i = cellmin.Row.Index; i <= cellmax.Row.Index; i++)
                {
                    for (int j = cellmin.Column.Index; j <= cellmax.Column.Index; j++)
                    {
                        ICell cell = this[i, j];
                        if (cell != null)
                        {
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            if (cell.AllowCopy)
                            {
                                sb.Append(cell.Text);
                            }
                            cell.Value = null;
                            cell.Text = string.Empty;
                        }
                        sb.Append(ConstantValue.CopySplitSymbolColumn);
                    }
                    sb.Append(ConstantValue.CopySplitSymbolRow);

                }
                string text = this.OnCuting(sb.ToString());
                CopyText.Add(text);
                ClipboardHelper.SetClip(text);

                this.EndEdit();
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }

        }

        public virtual void Paste()
        {
            try
            {
                if (this.FocusedCell == null)
                {
                    return;
                }
                if (CopyCells != null)
                {
                    PasteCell();
                    this.EndEdit();
                    this.ReFreshFirstDisplayRowIndex();
                    this.ReFreshFirstDisplayColumnIndex();
                    this.ReFresh();
                    return;
                }
                if (this.InEdit)
                {
                    return;
                }
                bool res = false;
                IDataObject iData = ClipboardHelper.GetDataObject();
                object data = ClipboardHelper.GetData("COPYCELLS");
                if (data is byte[])
                {
                    if (this.SelectRange.Count > 0)
                    {
                        for (int i = this.SelectRange.Count - 1; i >= 0; i--)
                        {
                            ICell cell = this.SelectRange[i];
                            PasteCell(cell, (byte[])data, true);
                        }
                    }
                    else
                    {
                        if (this.SelectCells != null)
                        {
                            List<ICell> list = this.SelectCells.GetAllCells();
                            int minrow = this.SelectCells.MinRow();
                            int mincolumn = this.SelectCells.MinColumn();
                            foreach (ICell item in list)
                            {
                                if (item.Column.Index == mincolumn)
                                {
                                    PasteCell(item, (byte[])data, true);
                                }
                            }
                        }
                    }
                }
                else if (iData.GetDataPresent(DataFormats.Text))
                {
                    //string text = Feng.Forms.ClipboardHelper.GetText();
                    string str = Feng.Forms.ClipboardHelper.GetText();
                    string text2 = this.OnPasting(str);
                    str = text2;
                    BeforeCellValueCancelArgs cancelargs = new BeforeCellValueCancelArgs();
                    cancelargs.Cell = this.FocusedCell;
                    cancelargs.Value = str;
                    this.OnBeforePaste(cancelargs);
                    if (cancelargs.Cancel)
                    {
                        return;
                    }
                    ParseText(str);
                }
                else if (iData.GetDataPresent(DataFormats.Bitmap))
                {
                    Bitmap bmp = new Bitmap(ClipboardHelper.GetImage());
                    ParseImage(bmp);
                }

                else if (iData.GetDataPresent(DataFormats.Dib))
                {
                    Bitmap bmp = new Bitmap(ClipboardHelper.GetImage());
                    ParseImage(bmp);
                }
                else if (iData.GetDataPresent(DataFormats.MetafilePict))
                {
                    Bitmap bmp = new Bitmap(ClipboardHelper.GetImage());
                    ParseImage(bmp);
                }
                else if (iData.GetDataPresent(DataFormats.FileDrop))
                {
                    object str1 = iData.GetData(DataFormats.FileDrop);
                    //return;
                }
                else
                {
                    //object data = Clipboard.GetData("COPYCELLS");
                    //if (data is byte[])
                    //{
                    //    if (this.SelectRange.Count > 0)
                    //    {
                    //        for (int i = this.SelectRange.Count - 1; i >= 0; i--)
                    //        {
                    //            ICell cell = this.SelectRange[i];
                    //            PasteCell(cell, (byte[])data, true);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        PasteCell(this.FocusedCell, (byte[])data, true);
                    //    }
                    //}
                }
                this.EndEdit();
                this.ReFreshFirstDisplayRowIndex();
                this.ReFreshFirstDisplayColumnIndex();
                this.ReFresh();
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }

        }

        public virtual void ParseImage(Bitmap bmp)
        {
            SetSelectCellImageBackImage(bmp);
        }

        public virtual void ParseTextLines(string[] strs)
        {
            int minrow = this.FocusedCell.Row.Index;
            int mincolumn = this.FocusedCell.Column.Index;
            int maxrow = this.FocusedCell.Row.Index + strs.Length;
            int rowindex = 0;
            int maxcolumn = 0;
            for (int i = minrow; i < maxrow; i++)
            {
                string s = strs[rowindex];
                rowindex++;
                string[] ss = s.Split(new string[] { ConstantValue.CopySplitSymbolColumn }, StringSplitOptions.None);
                maxcolumn = this.FocusedCell.MaxColumnIndex + ss.Length;
                int columnindex = 0;
                for (int j = mincolumn; j < maxcolumn; j++)
                {
                    ICell cell = this[i, j];
                    if (cell.OwnMergeCell != null)
                    {
                        if (cell == cell.OwnMergeCell.BeginCell)
                        {
                            cell = cell.OwnMergeCell;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    string text = ss[columnindex];
                    columnindex++;
                    PasteCellValue(cell, text);
                }
            }
            RefreshColumnWidth(mincolumn, maxcolumn - 1);
            this.VScroll.Max = maxrow;
            this.VScroll.ReFresh();
        }

        public virtual void ParseText(string str)
        { 
            string[] strs = str.Split(new string[] { ConstantValue.CopySplitSymbolRow }, StringSplitOptions.None);
            if (strs.Length > 1)
            {
                ParseTextLines(strs);
            }
            else if (str.Contains(ConstantValue.CopySplitSymbolColumn))
            {
                ParseTextLines(new string[] { str });
            }
            else
            {
                if (this.SelectCells != null)
                {
                    int minrow = this.SelectCells.MinRow();
                    int mincolumn = this.SelectCells.MinColumn();
                    int maxrow = this.SelectCells.MaxRow();
                    int maxcolumn = this.SelectCells.MaxColumn();
                    for (int i = minrow; i <= maxrow; i++)
                    {
                        for (int j = mincolumn; j <= maxcolumn; j++)
                        {
                            ICell cell = this[i, j];
                            if (cell.OwnMergeCell != null)
                            {
                                if (cell == cell.OwnMergeCell.BeginCell)
                                {
                                    cell = cell.OwnMergeCell;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            PasteCellValue(cell, str);
                        }
                    }
                    RefreshColumnWidth(mincolumn, maxcolumn);
                }
                else if (this.FocusedCell != null)
                {
                    PasteCellValue(this.FocusedCell, str);
                    RefreshColumnWidth(this.FocusedCell.Column.Index, this.FocusedCell.Column.Index);
                }
            }
        }
        public virtual void PasteBorder()
        {
            try
            {
                if (this.FocusedCell == null)
                {
                    return;
                }
                if (CopyCells != null)
                {
                    if (this.SelectCells != null)
                    {
                        //int minrow = this.SelectCells.MinRow();
                        //int mincolumn = this.SelectCells.MinColumn();
                        //int maxrow = this.SelectCells.MaxRow();
                        //int maxcolumn = this.SelectCells.MaxColumn();
                        //for (int i = minrow; i <= maxrow; i++)
                        //{
                        //    for (int j = mincolumn; j <= maxcolumn; j++)
                        //    {
                        //        ICell cell = this[i, j]; 
                        //    }
                        //} 
                    }
                    else if (this.FocusedCell != null)
                    {
#warning must fix
                        //if (CopyCells.Count > 0)
                        //{
                        //    this.FocusedCell.BorderStyle = CopyCells[0].BorderStyle.Clone() as CellBorderStyle;
                        //}
                    }
                }
                this.ReFresh();
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }

        }
        public virtual void PasteFormat()
        {
            try
            {
                object data = ClipboardHelper.GetData("COPYCELLS");
                if (data is byte[])
                {
                    if (this.SelectRange.Count > 0)
                    {
                        for (int i = this.SelectRange.Count - 1; i >= 0; i--)
                        {
                            ICell cell = this.SelectRange[i];
                            PasteCell(cell, (byte[])data, false);
                        }
                    }
                    else
                    {
                        if (SelectCells != null)
                        {
                            IList<ICell> list = this.SelectCells.GetAllCells();
                            if (list.Count > 0)
                            {
                                foreach (ICell item in list)
                                {
                                    PasteCell(item, (byte[])data, false);
                                }
                            }
                        }
                        else
                        {
                            PasteCell(this.FocusedCell, (byte[])data, false);
                        }
                    }
                }
                this.ReFresh();
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }

        }
        private ISelectCellCollection CopyCells;
        public virtual void PasteCell()
        {
            if (this.CopyCells != null)
            {
                ICell focusedcell = this.FocusedCell;
                if (focusedcell == null)
                    return;
                ISelectCellCollection cells = this.CopyCells;
                int minrow = cells.MinRow();
                int maxrow = cells.MaxRow();
                int mincolumn = cells.MinColumn();
                int maxcolumn = cells.MaxColumn();
                int rowcount = maxrow - minrow + 1;
                int columncount = maxcolumn - mincolumn + 1;
                List<ICell> pased = new List<ICell>();
                if (this.SelectRange.Count > 0)
                {
                    List<ICell> selectcells = this.GetSelectCells();
      
                    foreach (ICell cell in selectcells)
                    {
                        for (int i = 0; i < rowcount; i++)
                        {
                            for (int j = 0; j < columncount; j++)
                            {

                                ICell targetcell = this[cell.MaxRowIndex + i, cell.MaxColumnIndex + j];
                                if (pased.Contains(targetcell))
                                {
                                    continue;
                                }
                                pased.Add(targetcell);
                                ICell sourcecell = this[minrow + i, mincolumn + j];
                                IRow row = targetcell.Row;
                                IColumn column = targetcell.Column;

                                PasteCell(sourcecell, targetcell);
                                targetcell.Row = row;
                                targetcell.Column = column;
                            }
                        }
                    }
                }
                if (this.SelectCells !=null )
                {
                    int minselrow = this.SelectCells.MinRow();
                    int minselcolumn = this.SelectCells.MinColumn();
                    int maxselrow = this.SelectCells.MaxRow();
                    int maxselcolumn = this.SelectCells.MaxColumn();
                    if (minselrow == maxselrow)
                    {
                        maxselrow = minselrow + rowcount - 1;
                    }
                    if (minselcolumn == maxselcolumn)
                    {
                        maxselcolumn = minselcolumn + columncount - 1;
                    }
                    for (int m = minselrow; m <= maxselrow; m = m + rowcount)
                    {
                        for (int n = minselcolumn; n <= maxselcolumn; n = n + columncount)
                        {
                            ICell cell = this[m, n];
                            for (int i = 0; i < rowcount; i++)
                            {
                                for (int j = 0; j < columncount; j++)
                                {

                                    ICell targetcell = this[cell.MaxRowIndex + i, cell.MaxColumnIndex + j];
                                    if (pased.Contains(targetcell))
                                    {
                                        continue;
                                    }
                                    if (targetcell.Row.Index >maxselrow)
                                    {
                                        continue;
                                    }
                                    if (targetcell.Column.Index > maxselcolumn)
                                    {
                                        continue;
                                    }
                                    pased.Add(targetcell);
                                    ICell sourcecell = this[minrow + i, mincolumn + j];
                                    IRow row = targetcell.Row;
                                    IColumn column = targetcell.Column;

                                    PasteCell(sourcecell, targetcell);
                                    targetcell.Row = row;
                                    targetcell.Column = column;
                                }
                            }
                        }

                    }
                }
                RefreshColumnWidth(focusedcell.Column.Index, focusedcell.Column.Index + columncount);
                this.RefreshExtendCells();
            }
        }
        public virtual void RefreshColumnWidth(int mincolumn, int maxcolumn)
        { 
            if (!this.ColumnAutoWidth)
            {
                return;
            }
            for (int i = mincolumn; i <= maxcolumn; i++)
            {
                this.RefreshColumnWidth(this.Columns[i]);
            }
        }
        public virtual void PasteCellBorder()
        {
            if (this.CopyCells != null)
            {
                ISelectCellCollection cells = this.CopyCells;
                int minrow = cells.MinRow();
                int maxrow = cells.MaxRow();
                int mincolumn = cells.MinColumn();
                int maxcolumn = cells.MaxColumn();
                int rowcount = maxrow - minrow + 1;
                int columncount = maxcolumn - mincolumn + 1;

                ICell focusedcell = this.FocusedCell;
                for (int i = 0; i < rowcount; i++)
                {
                    for (int j = 0; j < columncount; j++)
                    {
                        ICell targetcell = this[this.FocusedCell.MaxRowIndex + i, this.FocusedCell.MaxColumnIndex + j];
                        ICell sourcecell = this[minrow + i, mincolumn + j];
                        IRow row = targetcell.Row;
                        IColumn column = targetcell.Column;
                        PasteBorder(sourcecell, targetcell);
                    }
                }
                this.RefreshExtendCells();
            }
        }
        public virtual void PasteColorAndImage()
        {
            if (this.CopyCells != null)
            {
                ISelectCellCollection cells = this.CopyCells;
                int minrow = cells.MinRow();
                int maxrow = cells.MaxRow();
                int mincolumn = cells.MinColumn();
                int maxcolumn = cells.MaxColumn();
                int rowcount = maxrow - minrow + 1;
                int columncount = maxcolumn - mincolumn + 1;

                ICell focusedcell = this.FocusedCell;
                for (int i = 0; i < rowcount; i++)
                {
                    for (int j = 0; j < columncount; j++)
                    {
                        ICell targetcell = this[this.FocusedCell.MaxRowIndex + i, this.FocusedCell.MaxColumnIndex + j];
                        ICell sourcecell = this[minrow + i, mincolumn + j];
                        IRow row = targetcell.Row;
                        IColumn column = targetcell.Column;
                        CopyColorAndImage(sourcecell, targetcell);
                    }
                }
                this.RefreshExtendCells();
            }
        }
        public virtual void PasteText()
        {
            if (this.CopyCells != null)
            {
                ISelectCellCollection cells = this.CopyCells;
                int minrow = cells.MinRow();
                int maxrow = cells.MaxRow();
                int mincolumn = cells.MinColumn();
                int maxcolumn = cells.MaxColumn();
                int rowcount = maxrow - minrow + 1;
                int columncount = maxcolumn - mincolumn + 1;

                ICell focusedcell = this.FocusedCell;
                for (int i = 0; i < rowcount; i++)
                {
                    for (int j = 0; j < columncount; j++)
                    {
                        ICell targetcell = this[this.FocusedCell.MaxRowIndex + i, this.FocusedCell.MaxColumnIndex + j];
                        ICell sourcecell = this[minrow + i, mincolumn + j];
                        IRow row = targetcell.Row;
                        IColumn column = targetcell.Column;
                        PasteCellValue(sourcecell, targetcell);
                    }
                }
                this.RefreshExtendCells();
            }
        }
        public virtual void PasteCell(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            PasteCellMerger(sourcecell, targetcell);
            PasteAction(sourcecell, targetcell);
            PasteProperty(sourcecell, targetcell);
            PasteBorder(sourcecell, targetcell);
            PasteCellEdit(sourcecell, targetcell);
            PasteCellValue(sourcecell, targetcell);
            PasteExpression(sourcecell, targetcell);
        }
        public virtual void PasteCellEdit(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            if (sourcecell.OwnEditControl == null)
            {
                return;
            }
            targetcell.OwnEditControl = sourcecell.OwnEditControl.Clone(this);
        }
        public virtual void PasteExpression(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            if (string.IsNullOrWhiteSpace(targetcell.Expression) && string.IsNullOrWhiteSpace(sourcecell.Expression))
                return;
            targetcell.Expression = sourcecell.Expression;
        }
        public virtual void PasteCellValue(ICell targetcell, string value)
        {
            if (targetcell.ReadOnly)
                return;
            targetcell.Value = value;
            targetcell.Text = value;
            targetcell.FreshContens();
        }
        public virtual void PasteCellValue(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            targetcell.Value = sourcecell.Value;
            targetcell.Text = sourcecell.Text;
            targetcell.FreshContens();
        }
        public virtual void CopyColorAndImage(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            targetcell.BackColor = sourcecell.BackColor;
            targetcell.ForeColor = sourcecell.ForeColor;
            targetcell.BackImage = sourcecell.BackImage;
            targetcell.BackImgeSizeMode = sourcecell.BackImgeSizeMode;
            targetcell.DisableImage = sourcecell.DisableImage;
            targetcell.DisableImageSizeMode = sourcecell.DisableImageSizeMode;
            targetcell.FocusBackColor = sourcecell.FocusBackColor;
            targetcell.FocusForeColor = sourcecell.FocusForeColor;
            targetcell.FocusImage = sourcecell.FocusImage;
            targetcell.FocusImageSizeMode = sourcecell.FocusImageSizeMode;
            targetcell.MouseDownBackColor = sourcecell.MouseDownBackColor;
            targetcell.MouseDownForeColor = sourcecell.MouseDownForeColor;
            targetcell.MouseDownImage = sourcecell.MouseDownImage;
            targetcell.MouseDownImageSizeMode = sourcecell.MouseDownImageSizeMode;
            targetcell.MouseOverBackColor = sourcecell.MouseOverBackColor;
            targetcell.MouseOverForeColor = sourcecell.MouseOverForeColor;
            targetcell.MouseOverImage = sourcecell.MouseOverImage;
            targetcell.MouseOverImageSizeMode = sourcecell.MouseOverImageSizeMode;
            targetcell.ReadOnlyImage = sourcecell.ReadOnlyImage;
            targetcell.ReadOnlyImageSizeMode = sourcecell.ReadOnlyImageSizeMode;
            targetcell.SelectBackColor = sourcecell.SelectBackColor;
            targetcell.SelectBorderColor = sourcecell.SelectBorderColor;
            targetcell.SelectForceColor = sourcecell.SelectForceColor;
            targetcell.ShowFocusedSelectBorder = sourcecell.ShowFocusedSelectBorder;
        }
        public virtual void PasteAction(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            targetcell.PropertyOnCellEndEdit = sourcecell.PropertyOnCellEndEdit;
            targetcell.PropertyOnCellInitEdit = sourcecell.PropertyOnCellInitEdit;
            targetcell.PropertyOnCellValueChanged = sourcecell.PropertyOnCellValueChanged;
            targetcell.PropertyOnClick = sourcecell.PropertyOnClick;
            targetcell.PropertyOnDoubleClick = sourcecell.PropertyOnDoubleClick;
            targetcell.PropertyOnDrawBack = sourcecell.PropertyOnDrawBack;
            targetcell.PropertyOnDrawCell = sourcecell.PropertyOnDrawCell;
            targetcell.PropertyOnKeyDown = sourcecell.PropertyOnKeyDown;
            targetcell.PropertyOnKeyPress = sourcecell.PropertyOnKeyPress;
            targetcell.PropertyOnKeyUp = sourcecell.PropertyOnKeyUp;
            targetcell.PropertyOnMouseCaptureChanged = sourcecell.PropertyOnMouseCaptureChanged;
            targetcell.PropertyOnMouseClick = sourcecell.PropertyOnMouseClick;
            targetcell.PropertyOnMouseDoubleClick = sourcecell.PropertyOnMouseDoubleClick;
            targetcell.PropertyOnMouseDown = sourcecell.PropertyOnMouseDown;
            targetcell.PropertyOnMouseEnter = sourcecell.PropertyOnMouseEnter;
            targetcell.PropertyOnMouseHover = sourcecell.PropertyOnMouseHover;
            targetcell.PropertyOnMouseLeave = sourcecell.PropertyOnMouseMove;
            targetcell.PropertyOnMouseUp = sourcecell.PropertyOnMouseUp;
            targetcell.PropertyOnMouseWheel = sourcecell.PropertyOnMouseWheel;
            targetcell.PropertyOnPreviewKeyDown = sourcecell.PropertyOnPreviewKeyDown;
        }
        public virtual void PasteProperty(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            targetcell.BackColor = sourcecell.BackColor;
            targetcell.ForeColor = sourcecell.ForeColor;
            targetcell.FormatType = sourcecell.FormatType;
            targetcell.FormatString = sourcecell.FormatString;
            targetcell.FieldName = sourcecell.FieldName;
            targetcell.Font = sourcecell.Font.Clone() as Font;
            targetcell.BackImage = sourcecell.BackImage;
            targetcell.BackImgeSizeMode = sourcecell.BackImgeSizeMode;
            targetcell.DirectionVertical = sourcecell.DirectionVertical;
            targetcell.DisableImage = sourcecell.DisableImage;
            targetcell.DisableImageSizeMode = sourcecell.DisableImageSizeMode;
            targetcell.DisplayMember = sourcecell.DisplayMember;
            targetcell.FocusBackColor = sourcecell.FocusBackColor;
            targetcell.FocusForeColor = sourcecell.FocusForeColor;
            targetcell.FocusImage = sourcecell.FocusImage;
            targetcell.FocusImageSizeMode = sourcecell.FocusImageSizeMode;
            targetcell.FunctionBorder = sourcecell.FunctionBorder;
            targetcell.HorizontalAlignment = sourcecell.HorizontalAlignment;
            targetcell.InhertReadOnly = sourcecell.InhertReadOnly;
            targetcell.MouseDownBackColor = sourcecell.MouseDownBackColor;
            targetcell.MouseDownForeColor = sourcecell.MouseDownForeColor;
            targetcell.MouseDownImage = sourcecell.MouseDownImage;
            targetcell.MouseDownImageSizeMode = sourcecell.MouseDownImageSizeMode;
            targetcell.MouseOverBackColor = sourcecell.MouseOverBackColor;
            targetcell.MouseOverForeColor = sourcecell.MouseOverForeColor;
            targetcell.MouseOverImage = sourcecell.MouseOverImage;
            targetcell.MouseOverImageSizeMode = sourcecell.MouseOverImageSizeMode;

            targetcell.ReadOnly = sourcecell.ReadOnly;
            targetcell.ReadOnlyImage = sourcecell.ReadOnlyImage;
            targetcell.ReadOnlyImageSizeMode = sourcecell.ReadOnlyImageSizeMode;
            targetcell.SelectBackColor = sourcecell.SelectBackColor;
            targetcell.SelectBorderColor = sourcecell.SelectBorderColor;
            targetcell.SelectForceColor = sourcecell.SelectForceColor;
            targetcell.ShowFocusedSelectBorder = sourcecell.ShowFocusedSelectBorder;
            targetcell.Value = sourcecell.Value;
            targetcell.ValueMember = sourcecell.ValueMember;
            targetcell.VerticalAlignment = sourcecell.VerticalAlignment;
            targetcell.Visible = sourcecell.Visible;
        }
        public virtual void PasteBorder(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            if (sourcecell.BorderStyle != null)
            {
                targetcell.BorderStyle = sourcecell.BorderStyle.Clone() as CellBorderStyle;
            }
        }
        public virtual void PasteCellMerger(ICell sourcecell, ICell targetcell)
        {
            if (targetcell.ReadOnly)
                return;
            if (sourcecell.OwnMergeCell != null)
            {
                IMergeCell mcell = sourcecell.OwnMergeCell;
                if (mcell.BeginCell == sourcecell)
                {
                    int minrow = mcell.Row.Index;
                    int maxrow = mcell.MaxRowIndex;
                    int mincolumn = mcell.Column.Index;
                    int maxcolumn = mcell.MaxColumnIndex;
                    int rowcount = maxrow - minrow;
                    int columncount = maxcolumn - mincolumn;
                    ICell endcell = this[targetcell.MaxRowIndex + rowcount, targetcell.MaxColumnIndex + columncount];
                    this.MergeCell(targetcell, endcell);
                }
            }
        }
        public virtual void Paste(string cliptext)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cliptext))
                {
                    return;
                }
                if (this.FocusedCell == null)
                {
                    return;
                }

                string str = cliptext;
                string text2 = this.OnPasting(str);
                str = text2;
                BeforeCellValueCancelArgs cancelargs = new BeforeCellValueCancelArgs();
                cancelargs.Cell = this.FocusedCell;
                cancelargs.Value = str;
                this.OnBeforePaste(cancelargs);
                if (cancelargs.Cancel)
                {
                    return;
                }

                string[] strs = str.Split(new string[] { ConstantValue.CopySplitSymbolRow }, StringSplitOptions.None);

                if (strs.Length > 0)
                {
                    int minrow = this.FocusedCell.Row.Index;
                    int mincolumn = this.FocusedCell.Column.Index;
                    int maxrow = this.FocusedCell.Row.Index + strs.Length;
                    List<ICell> list = new List<ICell>();
                    int rowindex = 0;

                    for (int i = minrow; i < maxrow; i++)
                    {

                        string s = strs[rowindex];
                        rowindex++;
                        if (string.IsNullOrEmpty(s))
                        {
                            continue;
                        }
                        string[] ss = s.Split(new string[] { ConstantValue.CopySplitSymbolColumn }, StringSplitOptions.None);

                        int maxcolumn = this.FocusedCell.MaxColumnIndex + ss.Length;
                        int columnindex = 0;
                        for (int j = mincolumn; j < maxcolumn; j++)
                        {
                            ICell cell = this[i, j];

                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            if (list.Contains(cell))
                            {
                                continue;
                            }
                            string text = ss[columnindex];
                            columnindex++;
                            list.Add(cell);
                            cell.Value = text;
                            cell.Text = (text);
                        }
                    }
                }
                else
                {
                    this.FocusedCell.Value = str;
                    this.FocusedCell.Text = (str);
                }
                this.ReFresh();
            }
            catch (Exception ex)
            {
                this.OnException(ex);
            }

        }

        public virtual void Undo()
        {
            this.Commands.Execute();
        }

        public virtual void Redo()
        {
            this.Commands.Redo();
        }

        public virtual void GoToLine()
        {
            FrmGOLine frm = new FrmGOLine();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int topline = Feng.Utils.ConvertHelper.ToInt32(frm.txtNum.Text);
                if (topline > 0)
                {
                    this.FirstDisplayedRowIndex = topline;
                }
            }
        }

        public virtual void GoToFind()
        {
            FrmFind frm = new FrmFind();
            frm.Grid = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (frm.ShowDialog() == DialogResult.OK)
            {
            }
        }
        public virtual void GoToReplace()
        {
            FrmReplace frm = new FrmReplace();
            frm.Grid = this;
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (frm.ShowDialog() == DialogResult.OK)
            {
            }
        }
        public virtual void GoToFirstCell()
        {
            this.FirstDisplayedRowIndex = 1;
            this.FirstDisplayedColumnIndex = 1;
            this.FocusedCell = this[1, 1];
            RefreshRowHeaderWidth();
            this.SelectCell(this.FocusedCell);
        }

        public virtual void AddEdit(IEdit edit)
        {
            this.CurrentEdit = edit;
        }
        private List<Control> editcontrols = new List<Control>();
        public virtual void AddControl(Control edit)
        {
            Control ctl = this.FindControl();
            if (ctl != null)
            {
                foreach (Control item in editcontrols)
                { 
                    ctl.Controls.Remove(item); 
                }
                editcontrols.Clear();
                editcontrols.Add(edit);
                ctl.Controls.Add(edit);
            }
        }
        public virtual void ClearCaretEdit()
        {
            if (Caret != null)
            {
                Caret.Hide();
            }
        }

        //public virtual ICell GetCellByName(string value)
        //{
        //    value = value.Trim();
        //    string pat = "(([a-zA-Z]+)(\\d+))";
        //    System.Text.RegularExpressions.MatchCollection ms = System.Text.RegularExpressions.Regex.Matches(value, pat);
        //    foreach (System.Text.RegularExpressions.Match m in ms)
        //    {
        //        if (m.Success)
        //        {
        //            int col = GetColumnIndexByColumnHeader(m.Groups[2].Value);
        //            int row = int.Parse(m.Groups[3].Value);
        //            return this[row, col];
        //        }
        //    }

        //    return this.GetCellByID(value);

        //}

        public void ClearSelect()
        {
#warning 选择太多时需要优化
            List<ICell> cs = this.GetSelectCells();
            foreach (ICell cell in cs)
            {
                cell.Selected = false;
            }
            this.FunctionSelectCells = null;
            //this.CopyCells = null;
            this._SelectCells = null;
            this._MergeCellCollectionRect = null;
            this._SelectAddRectCollection = null;
            this.TempSelectRect = null;
            int count = this.Selecteds.Count;
            for (int index = count - 1; index >= 0; index--)
            {
                ISelected row = this.Selecteds[index];
                row.Selected = false; 
            }
            this.SelectRows.Clear();
            this.SelectColumns.Clear();
            this.Selecteds.Clear();
            if (this.MergeCells != null)
            {
                count = this.MergeCells.Count;
                for (int index = count - 1; index >= 0; index--)
                {
                    IMergeCell imc = this.MergeCells[index];
                    imc.Selected = false;
                }
            }
            if (this.ListExtendCells != null)
            {
                count = this.ListExtendCells.Count;
                for (int index = count - 1; index >= 0; index--)
                {
                    IExtendCell imc = this.ListExtendCells[index];
                    imc.Selected = false;
                }
            }
        }

        public virtual bool InVisible(ICell cell)
        {
            if (cell == null)
                return false;
            if (cell.Row.Index >= this.FirstDisplayedRowIndex && cell.Row.Index <= this.EndDisplayedRowIndex)
            {

                if (cell.Column.Index >= this.FirstDisplayedColumnIndex && cell.Column.Index <= this.EndDisplayedColumnIndex)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void Swap(ICell cell1, ICell cell2)
        {
            IColumn column1 = cell1.Column;
            IColumn column2 = cell2.Column;


            IRow row1 = cell1.Row;
            IRow row2 = cell2.Row;

            cell1.Row.Cells.Remove(cell1);
            cell2.Row.Cells.Remove(cell2);


            cell1.Column = column2;
            cell2.Column = column1;


            cell1.Row = row2;
            cell2.Row = row1;

            row1.Cells.Add(cell2);
            row2.Cells.Add(cell1);
        }

        public virtual void RefreshCellContentsWidth(List<IBaseCell> list)
        {
            foreach (IBaseCell cell in list)
            {
                Size sf = Feng.Drawing.GraphicsHelper.Sizeof(cell.Text, cell.Font, this.FindControl());
                cell.ContensWidth = sf.Width;
            }
        }

        public virtual void RefreshCellContentsWidth(List<ICell> list)
        {
            foreach (ICell cell in list)
            {
                Size sf = Feng.Drawing.GraphicsHelper.Sizeof(cell.Text, cell.Font, cell.Grid.FindControl());
                cell.ContensWidth = sf.Width;
            }
        }

        public virtual void RefreshVisibleCellContentsWidth()
        {
            for (int i = this.FirstDisplayedRowIndex; i < this.EndDisplayedRowIndex; i++)
            {
                for (int j = this.FirstDisplayedColumnIndex; j < this.EndDisplayedColumnIndex; j++)
                {
                    ICell cell = this[i, j];
                    Size sf = Feng.Drawing.GraphicsHelper.Sizeof(cell.Text + "一", cell.Font, cell.Grid.FindControl());
                    cell.ContensWidth = sf.Width + 12;
                }
            }
        }

        public virtual void RefreshVisibleColumnWidth()
        {
            foreach (IColumn column in this.AllVisibleColumns)
            {
                RefreshColumnWidth(column);
            }
        }

        public virtual void RefreshColumnWidth(IColumn column)
        {
            if (column == null)
                return;
            if (!column.AllowChangedSize)
                return;
            if (!column.AutoWidth)
                return;

            int width = this.DefaultColumnWidth;
            for (int i = this.FirstDisplayedRowIndex; i < this.EndDisplayedRowIndex; i++)
            {
                ICell cell = this[i, column.Index];
                if (cell != null)
                {
                    if (cell.ContensWidth > width)
                    {
                        width = cell.ContensWidth;
                    }
                }
            }
            if (width > 1 && width < 800)
            {
                column.Width = width;
            }

        }

        public override DivView Clone()
        {
            DataExcel grid = new DataExcel();
            byte[] datas = this.GetFileData();
            grid.Open(datas);
            return grid;
        }
        #endregion

        #region 其他函数

        private bool endediting = false;
        public virtual void EndEdit()
        {
            if (endediting)
                return;
            endediting = true;
            try
            {

                CloseEdit();
                this.ExecuteExpress();
                this.OnEndCellValueEdit();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "EndEdit", ex);
            }
            finally
            {
                endediting = false;
            }
        }
        private bool closeedit = false;
        public virtual bool CloseEdit()
        {
            bool res = false;
            try
            {
                if (closeedit)
                    return false;
                closeedit = true;
                if (this.FocusedCell != null)
                {
                    if (this.FocusedCell.InEdit)
                    {
                        res = true;
                        this.FocusedCell.EndEdit();
                    }
                }
                this.EditCell = null;
                if (this.CurrentEdit != null)
                {
                    //this.CurrentEdit.EndEdit();
                    this.CurrentEdit = null;
                }
                this.ClearCaretEdit();
            }
            finally
            {
                closeedit = false;
            }
            return res;
        }


        #endregion

        #region IObjectSafety 成员

        public void GetInterfacceSafyOptions(int riid, out int pdwSupportedOptions, out int pdwEnabledOptions)
        {
            pdwSupportedOptions = 1;
            pdwEnabledOptions = 2;
        }

        public void SetInterfaceSafetyOptions(int riid, int dwOptionsSetMask, int dwEnabledOptions)
        {

        }

        #endregion

        public void Append(string id, object value)
        {

        }

        public void Append(int row, int column, object value)
        {
            int index = row;
            while (true)
            {
                ICell cell = this[index, column];
                if (cell.Value != null)
                {
                    cell.Value = value;
                    index++;
                    continue;
                }
                break;
            }
        }

        public void SetValue(string id, object value)
        {
            ICell cell = this.GetCellByID(id);
            if (cell != null)
            {
                cell.Value = value;
            }
        }

        public virtual Form FindForm()
        {
            return this.Control.FindForm();
        }

        public virtual Point PointToScreen(Point pt)
        {
            return this.Control.PointToScreen(pt);
        }
        public virtual Point PointToClient(Point pt)
        {
            return pt;
        }
        public virtual Control FindControl()
        {
            return this.Control;
        }
        public virtual Point PointControlToView(Point pt)
        {
            pt.Offset(this.Left*-1, this.Top * -1);
            return pt;
        }
        public virtual Point PointViewToControl(Point pt)
        {
            pt.Offset(this.Left, this.Top);
            return pt;
        }
        private IntPtr handle = IntPtr.Zero;
        public virtual IntPtr Handle
        {
            get
            {
                if (handle == IntPtr.Zero)
                {
                    return this.Control.Handle;
                }
                return handle;
            }
            set
            {
                handle = value;
            }

        }
    }

}