//using Feng.Excel.Collections;
//using Feng.Excel.Edits;
//using Feng.Excel.Interfaces;
//using Feng.Forms.Controls.GridControl;
//using Feng.Script.CBEexpress;
//using Feng.Script.Method;
//using Feng.Utils;
//using System;
//using System.Data;

//namespace Feng.Excel.Script
//{
//    [Serializable]
//    public class CellEditFunctionContainer : DataExcelMethodContainer
//    {

//        public const string Function_Category = "CellEditFunction";
//        public const string Function_Description = "单元格编辑控件函数";
//        public override string Name
//        {
//            get { return Function_Category; }

//        }
//        public override string Description
//        {
//            get { return Function_Description; }
//        }

//        public CellEditFunctionContainer()
//        {
//            BaseMethod model = null;


//            model = new BaseMethod();
//            model.Name = "CellEditDropDownGrid";
//            model.Description = @"创建单元格下拉控件 CellEditDropDownGrid(""CELLID"")";
//            model.Eg = @"CellEditDropDownGrid(""CELLID"")";
//            model.Function = CellEditDropDownGrid;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "CellEditGet";
//            model.Description = @"从单元格获取单元格编辑控件 CellEditGet(Cell(""CELLID""))";
//            model.Eg = @"CellEditGet(""CELLID"")";
//            model.Function = CellEditGet;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "CellEditExcel";
//            model.Description = @"从单元格获取单元格编辑控件 CellEditExcel(Cell(""CELLID""))";
//            model.Eg = @"CellEditExcel(""CELLID"")";
//            model.Function = CellEditExcel;
//            MethodList.Add(model);
//        }
 
//        public virtual object CellEditDropDownGrid(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy == null)
//            {
//                return null;
//            }
//            ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
//            if (cell == null)
//                return null;
//            IDropDownGrid dropDownGrid = cell.OwnEditControl as IDropDownGrid;
//            if (dropDownGrid != null)
//            {
//                return dropDownGrid.GetDropDownGrid();
//            }
//            return null;
//        }
//        public virtual object CellEditGet(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy == null)
//            {
//                return null;
//            }
//            ICell cell = base.GetCell(1, args);
//            return cell.OwnEditControl; 
//        }
//        public virtual object CellEditExcel(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy == null)
//            {
//                return null;
//            }
//            CellExcel cellExcel = null;
//            ICell cell = base.GetCell(1, args);
 
//            if (cell != null)
//            {
//                cellExcel = cell.OwnEditControl as CellExcel;
//            }
//            else
//            {
//                cellExcel = base.GetArgIndex(1, args) as CellExcel;
//            }
//            if(cellExcel==null )
//            {
//                return null;
//            }
//            return cellExcel.Edit;
//        }
//    }
//}
