using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using Feng.Print;
using Feng.Forms.Controls.Designer;
using Feng.Excel.Args;
using Feng.Excel.Delegates;
using Feng.Excel.Interfaces;
using Feng.Excel.Collections;
using Feng.Excel.Print;
using Feng.Data;

namespace Feng.Excel
{
    [BaseControl("l6vS7ovkG03LB856gNIhhg==", "b3QnWaB5gxrhU4D/0A/4vQ==")]
    partial class DataExcel
    {
        public void Print()
        {

            if (_PrintDocument == null)
            {
                _PrintDocument = new DataPrintDocument();
                _PrintDocument.PrintPage += new PrintPageEventHandler(_PrintDocument_PrintPage);
                _PrintDocument.EndPrint += new PrintEventHandler(_PrintDocument_EndPrint);
                _PrintDocument.BeginPrint += new PrintEventHandler(_PrintDocument_BeginPrint);
                Size size = GetPaperSize(this.PaperName);
                _PrintDocument.DefaultPageSettings.PaperSize = new PaperSize(this.PaperName, size.Width, size.Height);
                _PrintDocument.DefaultPageSettings.Landscape = this.PrintLandScope;
                _PrintDocument.DefaultPageSettings.Margins = this.PrintMargins;
            }
            this.ReSetHasValue();
            this._PrintDocument.Print();

        }
        public Size GetPaperSize(string papername)
        {
            int width = 850;
            int height = 1100;
            foreach (PaperSize pz in _PrintDocument.DefaultPageSettings.PrinterSettings.PaperSizes)
            {
                if (pz.PaperName == papername)
                {
                    return new Size(pz.Width, pz.Height);
                }

            }
            return new Size(width, height);
        }
        [Browsable(false)]
        [DefaultValue(false)]
        public bool PrintViewMode { get; set; }
        public void PrintView()
        {
            _pageindex = 0;
            if (_PrintDocument == null)
            {
                _PrintDocument = new DataPrintDocument();
            }
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
            {
                PrintSetting();
            }
            _PrintDocument.PrintPage -= new PrintPageEventHandler(_PrintDocument_PrintPage);
            _PrintDocument.EndPrint -= new PrintEventHandler(_PrintDocument_EndPrint);
            _PrintDocument.BeginPrint -= new PrintEventHandler(_PrintDocument_BeginPrint);


            _PrintDocument.PrintPage += new PrintPageEventHandler(_PrintDocument_PrintPage);
            _PrintDocument.EndPrint += new PrintEventHandler(_PrintDocument_EndPrint);
            _PrintDocument.BeginPrint += new PrintEventHandler(_PrintDocument_BeginPrint);


            this._PrintDocument.DefaultPageSettings.Landscape = this.PrintLandScope;
            this._PrintDocument.DefaultPageSettings.Margins = this.PrintMargins;
            this._PrintDocument.DefaultPageSettings.PaperSize = new PaperSize(this.PaperName, this.PaperWidth, this.PaperHeight);
            this.ReSetHasValue();
            using (System.Windows.Forms.PrintPreviewDialog dlg = new PrintPreviewDialog())
            {
                dlg.Document = this._PrintDocument;
                dlg.WindowState = FormWindowState.Maximized;
                dlg.AllowTransparency = true;
                dlg.AutoScaleMode = AutoScaleMode.Dpi;
                dlg.Icon = this.ParentForm.Icon;
                PrintViewMode = true;
                dlg.ShowDialog();
                PrintViewMode = false;
            }
        }
        public void SetPrintArea(SelectCellCollection sel)
        {
            this.PrintArea = sel;
        }
        public virtual void PrintSetting()
        {
            using (System.Windows.Forms.PageSetupDialog dlg = new PageSetupDialog())
            {
                dlg.PrinterSettings = this.PrinterSettings;
                dlg.Document = this.PrintDocument;

                if (this.PrintDocument == null)
                {
                    this.PrintDocument = new DataPrintDocument();

                    //Size size = this.GetPaperSize(this.PaperName);
                    //this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(this.PaperName, size.Width, size.Height);
                    //this.PrintDocument.DefaultPageSettings.Landscape = this.PrintLandScope;
                    //this.PrintDocument.DefaultPageSettings.Margins = this.PrintMargins;
                }
                Size size = this.GetPaperSize(this.PaperName);
                this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(this.PaperName, size.Width, size.Height);
                this.PrintDocument.DefaultPageSettings.Landscape = this.PrintLandScope;
                this.PrintDocument.DefaultPageSettings.Margins = this.PrintMargins;

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.PaperName = this.PrintDocument.DefaultPageSettings.PaperSize.PaperName;
                    this.PrintLandScope = this.PrintDocument.DefaultPageSettings.Landscape;

                    this.PrintMargins = PrinterUnitConvert.Convert(dlg.PageSettings.Margins, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);

                }
            }
        }
        public virtual bool PrintStep()
        {
            bool res = true;
            _pageindex = 0;

            if (_PrintDocument == null)
            {
                _PrintDocument = new DataPrintDocument();
                _PrintDocument.PrintPage += new PrintPageEventHandler(_PrintDocument_PrintPage);
                _PrintDocument.EndPrint += new PrintEventHandler(_PrintDocument_EndPrint);
                _PrintDocument.BeginPrint += new PrintEventHandler(_PrintDocument_BeginPrint);
            }

            using (System.Windows.Forms.PrintDialog dlg = new PrintDialog())
            {
                if (this.PrinterSettings != null)
                {
                    dlg.PrinterSettings = this.PrinterSettings;
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.PrinterSettings = dlg.PrinterSettings;
                    this._PrintDocument.PrinterSettings = this.PrinterSettings;
                }
                else
                {
                    return false;
                }
            }
            using (System.Windows.Forms.PageSetupDialog dlg = new PageSetupDialog())
            {
                dlg.PrinterSettings = this.PrinterSettings;
                dlg.Document = this._PrintDocument;

                if (dlg.ShowDialog() == DialogResult.OK && RegionInfo.CurrentRegion.IsMetric
           && Environment.OSVersion.Platform != PlatformID.Unix)
                {
                    dlg.PageSettings.Margins = PrinterUnitConvert.Convert
                    (dlg.PageSettings.Margins, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
                }
                else
                {
                    return false;
                }
            }
            this.ReSetHasValue();
            using (System.Windows.Forms.PrintPreviewDialog dlg = new PrintPreviewDialog())
            {
                dlg.Document = this._PrintDocument;
                dlg.WindowState = FormWindowState.Maximized;
                dlg.AllowTransparency = true;
                dlg.AutoScaleMode = AutoScaleMode.Dpi;
                dlg.Icon = this.ParentForm.Icon;
                dlg.ShowDialog();
            }
            return res;
        }

        void _PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            this.IsPrinting = true;
            _pageindex = 0;
            _printindex++;
            _printargs = new PrintArgs();
            _printargs.BeginRowIndex = 0;
            _printargs.BeginColumnIndex = 0;
            int minrow = 1;
            int mincolumn = 1;
            int maxrow = 1;
            int maxcolumn = 1;
            Point pt = ReSetHasValue();
            maxrow = pt.X;
            maxcolumn = pt.Y;
            if (this.PrintArea != null)
            {
                minrow = PrintArea.MinRow();
                mincolumn = PrintArea.MinColumn();
                if (maxrow > this.PrintArea.MaxRow())
                {
                    maxrow = PrintArea.MaxRow();
                }
                if (maxcolumn > this.PrintArea.MaxColumn())
                {
                    maxcolumn = this.PrintArea.MaxColumn();
                }

            }
            else
            {
                mincolumn = 1;
                minrow = 1;
                Point ptt = ReSetHasValue();
                maxcolumn = ptt.Y;
                maxrow = ptt.X;
            }
            _printargs.MinColumnIndex = mincolumn;
            _printargs.MinRowIndex = minrow;
            _printargs.MaxColumnIndex = maxcolumn;
            _printargs.MaxRowIndex = maxrow;
            if (this.FirstPrintCell != null)
            {
                _printargs.BeginRowIndex = this.FirstPrintCell.Row.Index;
                _printargs.BeginColumnIndex = this.FirstPrintCell.Column.Index;
            }

        }

        void _PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            _printindex--;
            _printargs = null;
            this.IsPrinting = false;
        }

        public event PrintCellHandler PrintCell;
        public void OnPrintCell(PrintCellArgs e)
        {
            if (PrintCell != null)
            {
                PrintCell(this, e);
            }
        }
        public event PrintCellBackHandler PrintCellBack;
        public void OnPrintCellBack(PrintCellBackArgs e)
        {
            if (PrintCellBack != null)
            {
                PrintCellBack(this, e);
            }
        }

        [NonSerialized]
        private DataPrintDocument _PrintDocument = null;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataPrintDocument PrintDocument
        {
            get
            {
                if (_PrintDocument == null)
                {
                    _PrintDocument = new DataPrintDocument();
                    Size size = this.GetPaperSize(this.PaperName);
                    _PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(this.PaperName, size.Width, size.Height);
                    _PrintDocument.DefaultPageSettings.Landscape = this.PrintLandScope;
                    _PrintDocument.DefaultPageSettings.Margins = this.PrintMargins;
                }
                return _PrintDocument;
            }
            set
            {
                _PrintDocument = value;
            }
        }

        [NonSerialized]
        private PrinterSettings _PrinterSettings = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PrinterSettings PrinterSettings
        {
            get { return _PrinterSettings; }
            set { _PrinterSettings = value; }
        }

        private int _pageindex = 0;
        private int _printindex = 0;
        [DefaultValue(0)]
        public int PrintIndex
        {
            get { return _printindex; }
            set { _printindex = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public virtual bool IsPrinting
        {
            get;
            set;
        }

        private PrintArgs _printargs = null;


        void _PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

                _pageindex++;

                Feng.Excel.Print.Page _printpage = new Feng.Excel.Print.Page(this, _pageindex);

                _printpage.Rect = e.MarginBounds;

                _printpage.PrintList = new List<IPrint>();
                _printpage.PrintBorderList = new List<IPrintBorder>();
                _printargs.CurrentPage = _printpage;
                _printargs.PrintPageEventArgs = e;
                _printargs.HasMorePages = false;
                _printargs.BeginPrint(_printargs);
                _printpage.Print(_printargs);
                e.HasMorePages = _printargs.HasMorePages;
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            finally
            {
                this.IsPrinting = false;
            }
        }

        private ICell _firstprintcell = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICell FirstPrintCell
        {
            get
            {
                return this._firstprintcell;
            }
            set
            {
                this._firstprintcell = value;
            }
        }

        private ICell _endprintcell = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICell EndPrintCell
        {
            get
            {
                return this._endprintcell;
            }
            set
            {
                this._endprintcell = value;
            }
        }

        private SelectCellCollection _PrintArea = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SelectCellCollection PrintArea
        {
            get
            {
                return _PrintArea;
            }
            set
            {
                _PrintArea = value;
            }
        }

        private string _printdocumentname = string.Empty;
        [Browsable(true), DefaultValue("")]
        [Category(CategorySetting.PropertyPrint)]
        public string PrintDocumentName
        {
            get { return _printdocumentname; }
            set { _printdocumentname = value; }
        }


        private bool _PrintLandScope = false;
        [Browsable(true), DefaultValue(false)]
        [Category(CategorySetting.PropertyUI)]
        public virtual bool PrintLandScope
        {
            get { return _PrintLandScope; }
            set { _PrintLandScope = value; }
        }
        private Margins _PrintMargins = null;
        [Browsable(true)]
        [Category(CategorySetting.PropertyPrint)]
        [DefaultValue(typeof(Margins), "100, 100, 100, 100")]
        public virtual Margins PrintMargins
        {
            get
            {
                if (_PrintMargins == null)
                {
                    _PrintMargins = new Margins(100, 100, 100, 100);
                }
                return _PrintMargins;
            }
            set
            {
                _PrintMargins = value;
            }
        }

        private string _papername = "A4";
        [Browsable(true), DefaultValue("A4")]
        [Category(CategorySetting.PropertyPrint)]
        [TypeConverter(typeof(Feng.Excel.Designer.PaperNameConverter))]
        public virtual string PaperName
        {
            get
            {

                return _papername;
            }
            set
            {
                if (value == string.Empty)
                {
                    value = "A4";
                }
                _papername = value;
                PaperSize ps = GetPaperSizeByName(value);
                this.PrintDocument.DefaultPageSettings.PaperSize = ps;
                this.PaperHeight = ps.Height;
                this.PaperWidth = ps.Width;
            }
        }

        public PaperSize GetPaperSizeByName(string papername)
        {
            string defaultpapername = "A4";
            int width = 100;
            int height = 100;
            try
            {
                PrinterSettings.PaperSizeCollection pss = this.PrintDocument.PrinterSettings.PaperSizes;
                foreach (PaperSize ps in pss)
                {
                    if (papername == ps.PaperName)
                    {
                        return ps;
                    }
                    if (ps.PaperName == defaultpapername)
                    {
                        width = ps.Width;
                        height = ps.Height;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return new PaperSize(papername, width, height);

        }

        private int _paperwidth = 827;
        [Browsable(true), DefaultValue(827)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual int PaperWidth
        {
            get { return _paperwidth; }
            set { _paperwidth = value; }
        }


        private int _paperheight = 1169;
        [Browsable(true), DefaultValue(1169)]
        [Category(CategorySetting.PropertyPrint)]
        public virtual int PaperHeight
        {
            get { return _paperheight; }
            set { _paperheight = value; }
        }

        private string _PrinterName = string.Empty;
        [Browsable(true)]
        [DefaultValue("")]
        [Category(CategorySetting.PropertyPrint)]
        public virtual string PrinterName
        {
            get
            {
                return _PrinterName;
            }
            set
            {
                _PrinterName = value;
            }
        }

        #region Print

        public Point ReSetHasValue()
        {
            int rowindex = 0;
            int columnindex = 0;
            foreach (IRow row in this.Rows)
            {
                foreach (IColumn col in this.Columns)
                {

                    ICell cell = row[col];

                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            cell = cell.OwnMergeCell;
                        }
                        if (cell.Value != null
                            && cell.BorderStyle != null
                            && cell.Visible
                            && (!cell.Value.Equals(string.Empty))
                            && (!cell.Value.Equals(""))
                            )
                        {
                            if (row.Index > rowindex)
                            {
                                rowindex = row.Index;
                            }
                            if (col.Index > columnindex)
                            {
                                columnindex = col.Index;
                            }
                        }
                    }
                }
            }
            return new Point(rowindex, columnindex);
        }

        #endregion
    }
}
