using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Feng.Print;

namespace Feng.Excel.Print
{
    [Serializable]
    public class PrintHeader : IPrint 
    {
        public PrintHeader(Print.PrintSetting printpage)
        {
            _printpage = printpage;
        }
        private Print.PrintSetting _printpage = null;
        public Print.PrintSetting PrintPage
        {
            get { return _printpage; }
            set { _printpage = value; }
        }

        #region IDataExcelGrid 成员

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return _printpage.Grid; }
        }

        #endregion

        #region IPrint 成员

        public virtual bool Print(PrintArgs e)
        { 
            if (_printexcel != null)
            {
                PrintArgs phe = new PrintArgs();
                phe.PrintPageEventArgs = e.PrintPageEventArgs;
                phe.CurrentLocation = Point.Empty;
                phe.CurrentPage.Rect = new Rectangle(e.PrintPageEventArgs.PageBounds.Location, new Size(e.PrintPageEventArgs.PageBounds.Width, e.PrintPageEventArgs.MarginBounds.Top));
                phe.BeginColumnIndex = 0;
                _printexcel.Print();
            }

            return false;
        }


        public virtual bool PrintBorder(PrintArgs e)
        { 
            if (_printexcel != null)
            {
                PrintArgs phe = new PrintArgs();
                phe.PrintPageEventArgs = e.PrintPageEventArgs;
                phe.CurrentLocation = Point.Empty;
                phe.CurrentPage.Rect = new Rectangle(e.PrintPageEventArgs.PageBounds.Location, new Size(e.PrintPageEventArgs.PageBounds.Width, e.PrintPageEventArgs.MarginBounds.Top));
                phe.BeginColumnIndex = 0;
                _printexcel.Print();
            }

            return false;
        }


        #endregion
        [NonSerialized]
        private DataExcel _printexcel = null;
        private PrintPageHeader printpageheader = null;
        public virtual void ShowPrintHeaderDialog()
        {
#if DEBUG
            try
            {
#endif
                if (printpageheader == null)
                {
                    printpageheader = new PrintPageHeader();
                }
                printpageheader.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                if (this._printexcel == null)
                {
                    _printexcel = new DataExcel();
                    _printexcel.ShowRowHeader = false;
                    _printexcel.ShowColumnHeader = true;
                }
                printpageheader.PrintGrid = _printexcel;
                if (printpageheader.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                }
                _printexcel.ShowColumnHeader = false;
               
#if DEBUG
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
#endif

        }
    }
}
