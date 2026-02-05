//using Feng.Data;
//using Feng.Excel.Interfaces;
//using Feng.Forms.Views;
//using System;
//using System.ComponentModel;
//using System.Windows.Forms;

//namespace Feng.Excel.Edits
//{
//    [Serializable]
//    public class CellColumnFooter : CellBaseEdit
//    {
//        public CellColumnFooter(DataExcel grid)
//            : base(grid)
//        {
//        }
//        public override string ShortName { get { return "CellColumnFooter"; } set { } } 


//        private static CellColumnFooter _instance = null;
//        public static CellColumnFooter Instance(DataExcel grid)
//        {
//            if (_instance == null)
//                _instance = new CellColumnFooter(grid);
//            return _instance;
//        }


//        public override void Read(DataExcel grid, int version, DataStruct data)
//        {

//        }
//        public override void ReadDataStruct(DataStruct data)
//        {
             
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


//                return data;
//            }
//        }

//        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
//        {
//            IBaseCell cell = sender as IBaseCell;
//            if (cell == null)
//                return false;
//            if (e.Alt || e.Control || e.Shift)
//            {

//            }
//            else
//            {
//                if (e.KeyData == Keys.Right)
//                {
//                    cell.Grid.MoveFocusedCellToRightCell();
//                }
//                if (e.KeyData == Keys.Left)
//                {
//                    cell.Grid.MoveFocusedCellToLeftCell();
//                }
//            }
//            return false;
//        }

//        public override ICellEditControl Clone(DataExcel grid)
//        {
//            CellColumnFooter celledit = new CellColumnFooter(grid);

//            return celledit;
//        }
//    }

//}
