using Feng.Excel.Actions;
using Feng.Excel.Builder;
using Feng.Excel.Collections;
using Feng.Excel.Forms;
using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace Feng.Excel.Script
{
    [Serializable]
    public class DataExcelFunctionContainer : DataExcelMethodContainer
    {

        public const string Function_Name = "DataExcel";
        public const string Function_Description = "表格函数";
        public override string Name
        {
            get { return Function_Name; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public DataExcelFunctionContainer()
        {
            BaseMethod model = null;
 
            model = new BaseMethod();
            model.Name = "GridNew";
            model.Description = @"创建新的表格 GridNew()";
            model.Eg = @"var grid=GridNew();";
            model.Function = GridNew;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridOpen";
            model.Description = @"打开文件 GridOpen(""f:\temp.fexm"")";
            model.Eg = @"GridOpen(file);";
            model.Function = GridOpen;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridSave";
            model.Description = @"保存 GridSave()";
            model.Eg = @"GridSave()";
            model.Function = GridSave;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridSaveAs";
            model.Description = @"另保存 GridSaveAs()";
            model.Eg = @"GridSaveAs()";
            model.Function = GridSaveAs;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridRead";
            model.Description = @"打开文件 GridRead()";
            model.Eg = @"GridRead()";
            model.Function = GridRead;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridGetData";
            model.Description = @"返回文件字节数组 使用GridLoadData加载到文件 GridGetData()";
            model.Eg = @"GridGetData()";
            model.Function = GridGetData;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridLoadData";
            model.Description = @"加载GridGetData获得的字节数组到文件 GridLoadData()";
            model.Eg = @"GridLoadData()";
            model.Function = GridLoadData;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridExecCommand";
            model.Description = @"执行命令 GridExecCommand()";
            model.Eg = @"GridExecCommand()";
            model.Function = GridExecCommand;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridEditMode";
            model.Description = @"获取编辑模式 GridEditMode()";
            model.Eg = @"GridEditMode()";
            model.Function = GridEditMode;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridHashtable";
            model.Description = @"设置或返加表格附加信息 GridHashtable(""key"",value)";
            model.Eg = @"GridHashtable(""key"")";
            model.Function = GridHashtable;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridDialogOK";
            model.Description = @"关闭弹出窗口返回OK GridDialogOK()";
            model.Eg = @"GridDialogOK()";
            model.Function = GridDialogOK;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridDialogCancel";
            model.Description = @"关闭弹出窗口返回Cancel GridDialogCancel()";
            model.Eg = @"GridDialogCancel()";
            model.Function = GridDialogCancel;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridRefresh";
            model.Description = @"刷新 GridRefresh()";
            model.Eg = @"GridRefresh()";
            model.Function = GridRefresh;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridRefreshExtendCells";
            model.Description = @"刷新扩展单元格合并,浮动,背景单元格 GridRefreshExtendCells()";
            model.Eg = @"GridRefreshExtendCells()";
            model.Function = GridRefreshExtendCells;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridDoEvent";
            model.Description = @"刷新 GridDoEvent()";
            model.Eg = @"GridDoEvent()";
            model.Function = GridDoEvent;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "GridPopupGrid";
            model.Description = @"弹出表格 GridPopupGrid(""\teamp\test.fexm"")";
            model.Eg = @"GridPopupGrid(""\teamp\test.fexm"")";
            model.Function = GridPopupGrid;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridHidePopup";
            model.Description = @"隐藏弹出窗口 GridHidePopup()";
            model.Eg = @"GridHidePopup()";
            model.Function = GridHidePopup;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridShowSelectBorder";
            model.Description = @"显示或者隐藏选择边框 GridShowSelectBorder(False)";
            model.Eg = @"GridShowSelectBorder(False)";
            model.Function = GridShowSelectBorder;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridShowCell";
            model.Description = @"锚定到单元格 GridShowCell(CELL(""b30""))";
            model.Eg = @"GridShowCell(False)";
            model.Function = GridShowCell;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridShowRowHeader";
            model.Description = @"显示或者隐藏行头 GridShowRowHeader(False)";
            model.Eg = @"GridShowRowHeader(False)";
            model.Function = GridShowRowHeader;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridShowColumnHeader";
            model.Description = @"显示或者隐藏列头 GridShowRowHeader(False)";
            model.Eg = @"GridShowColumnHeader(False)";
            model.Function = GridShowColumnHeader;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridShowLine";
            model.Description = @"显示或者隐藏网格线 GridShowLine(False)";
            model.Eg = @"GridShowLine(False)";
            model.Function = GridShowLine;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridShowHScroller";
            model.Description = @"显示或者隐藏水平滚动条 GridShowHScroller(False)";
            model.Eg = @"GridShowHScroller(False)";
            model.Function = GridShowHScroller;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridShowVScroller";
            model.Description = @"显示或者隐藏垂直滚动条 GridShowVScroller(False)";
            model.Eg = @"GridShowVScroller(False)";
            model.Function = GridShowVScroller;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridFont";
            model.Description = @"获取或者设备表格字体 GridFont(""宋体"",2)";
            model.Eg = @"GridFont(""宋体"",2)";
            model.Function = GridFont;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridBackColor";
            model.Description = @"获取或者设备表格背景颜色 GridBackColor(123456)";
            model.Eg = @"GridBackColor(123456)";
            model.Function = GridBackColor;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridBackImage";
            model.Description = @"获取或者设备表格背景图片 GridBackColor()";
            model.Eg = @"GridBackImage()";
            model.Function = GridBackImage;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridCopy";
            model.Description = @"复制内容";
            model.Eg = @"GridCopy()";
            model.Function = GridCopy;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridShowToolTip";
            model.Description = @"显示提示内容";
            model.Eg = @"GridShowToolTip()";
            model.Function = GridShowToolTip;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridDisplayArea";
            model.Description = @"获取或设置表格可见范围";
            model.Eg = @"GridDisplayArea()";
            model.Function = GridDisplayArea;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridSelectRange";
            model.Description = @"获取或设置表格选择单元格";
            model.Eg = @"cells(GridSelectRange())";
            model.Function = GridSelectRange;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridDataTableFill";
            model.Description = @"填充行 GridDataTableFill(CellRange(""A1:F19""),DataTable)";
            model.Eg = @"GridDataTableFill(CellRange(""A1:F19""),DataTable)";
            model.Function = GridDataTableFill;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridDialogGrid";
            model.Description = @"弹出表格 GridDialogGrid(grid)";
            model.Eg = @"GridDialogGrid(grid)";
            model.Function = GridDialogGrid;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridPropertyEvent";
            model.Description = @"弹出表格 GridPropertyEvent(""PropertyClick"",""CODE"")";
            model.Eg = @"GridPropertyEvent(""PropertyClick"",""CODE"")";
            model.Function = GridPropertyEvent;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "GridPropertyEventExec";
            model.Description = @"触发事件 GridPropertyEventExec(""PropertyClick"",""参数"",值)";
            model.Eg = @"GridPropertyEventExec(""PropertyClick"",""参数"",值)";
            model.Function = GridPropertyEventExec;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridFunctionExec";
            model.Description = @"执行自定义函数 GridFunctionExec(""myfunction"",""参数"",值)";
            model.Eg = @"GridFunctionExec(""myfunction"",""参数"",值)";
            model.Function = GridFunctionExec;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridVar";
            model.Description = @"保存或者设置变量值 GridVar( ""变量名"",值)";
            model.Eg = @"GridVar(""变量名"",值)";
            model.Function = GridVar;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridGetDataTable";
            model.Description = @"将设置的表格转为DataTable GridGetDataTable()";
            model.Eg = @"GridGetDataTable()";
            model.Function = GridGetDataTable;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridCellsToTable";
            model.Description = @"将开始单元格到结束单元格转为DataTable GridCellsToTable(cell(""A3""),cell(""h30""))";
            model.Eg = @"var table=GridCellsToTable(cell(""a3""),cell(""m30"")); \r\n DataTableShow(table);";
            model.Function = GridCellsToTable;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "FormClose";
            model.Description = @"关闭表格所在窗口 FormClose()";
            model.Eg = @"FormClose()";
            model.Function = FormClose;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "FormCloseSave";
            model.Description = @"关闭表格所在窗口前保存数据 FormCloseSave()";
            model.Eg = @"FormCloseSave()";
            model.Function = FormCloseSave;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "FormCloseAskSave";
            model.Description = @"关闭表格所在窗口前询问是否保存数据 FormCloseAskSave()";
            model.Eg = @"FormCloseAskSave()";
            model.Function = FormCloseAskSave;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridRowCount";
            model.Description = @"最后一行有值的行 GridRowCount()";
            model.Eg = @"GridRowCount()";
            model.Function = GridRowCount;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridColumnCount";
            model.Description = @"最后一列有值的行 GridColumnCount()";
            model.Eg = @"GridColumnCount()";
            model.Function = GridColumnCount;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridRows";
            model.Description = @"所有已经创建的行 GridRows()";
            model.Eg = @"var rows=GridRows()";
            model.Function = GridRows;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridColumns";
            model.Description = @"所有已经创建的行 GridRows()";
            model.Eg = @"var columns=GridColumns()";
            model.Function = GridColumns;
            MethodList.Add(model);
        }
        public virtual object GridRows(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                return proxy.Grid.Rows;
            }
            return null;
        }
        public virtual object GridColumns(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                return proxy.Grid.Columns;
            }
            return null;
        }
        public virtual object GridRowCount(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                return proxy.Grid.Rows.MaxHasValueIndex;
            }
            return null;
        }
        public virtual object GridColumnCount(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                return proxy.Grid.MaxHasValueColumn;
            }
            return null;
        }

        public virtual object FormClose(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                System.Windows.Forms.Form form = proxy.Grid.FindForm();
                form.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                form.Close();
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object FormCloseSave(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                System.Windows.Forms.Form form = proxy.Grid.FindForm();
                form.DialogResult = System.Windows.Forms.DialogResult.OK;
                form.Close();
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object FormCloseAskSave(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                if (Feng.Utils.MsgBox.ShowQuestion("是否保存？")== System.Windows.Forms.DialogResult.OK)
                {
                    System.Windows.Forms.Form form = proxy.Grid.FindForm();
                    form.DialogResult = System.Windows.Forms.DialogResult.OK;
                    form.Close();
                    return Feng.Utils.Constants.TRUE;
                }
            }
            return Feng.Utils.Constants.Fail;
        }

        frmPopupGrid FrmPopup = null;
        public virtual object GridPopupGrid(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                DataExcel grid= base.GetArgIndex(1, args) as DataExcel;
                Point location = Point.Empty;
                string locationtype = base.GetTextValue(2, args);
                switch (locationtype)
                {
                    case "MOUSE":
                        location = System.Windows.Forms.Control.MousePosition;
                        break;
                    case "CELL":
                        location = proxy.CurrentCell.Location;
                        location.Y = proxy.CurrentCell.Height;
                        location = proxy.Grid.PointToScreen(location);
                        break;
                    default:
                        location = System.Windows.Forms.Control.MousePosition;
                        break;
                }
                if (FrmPopup == null)
                {
                    FrmPopup = new frmPopupGrid();
                }
                if (FrmPopup.IsDisposed)
                {
                    FrmPopup = new frmPopupGrid();
                }
                FrmPopup.InitData(grid);
                FrmPopup.Popup(location);
                grid.Save("d:\\bbb.fexm");
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridDialogGrid(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                DataExcel grid = base.GetArgIndex(1, args) as DataExcel; 
                frmDialog FrmPopup = new frmDialog();
                FrmPopup.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                FrmPopup.InitData(grid);
                if (FrmPopup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return Feng.Utils.Constants.OK;
                }
                return Feng.Utils.Constants.Cancel;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridHidePopup(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                DataExcel grid = proxy.Grid;
                System.Windows.Forms.Form form = grid.FindForm();
                if (form != null)
                {
                    form.Hide();
                } 
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridDoEvent(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                string eventtext = base.GetTextValue(2, args);
                if (cell != null)
                {
                    List<CellPropertyAction> list = PropertyActionTools.GetCellActions(cell);
                    foreach (CellPropertyAction item in list)
                    {
                        if (item.ShortName == eventtext)
                        {
                            ScriptBuilder.Exec(proxy.Grid, cell, item.Script);
                            break;
                        }
                    }
                }
                else
                {
                    List<DataExcelPropertyAction> list = PropertyActionTools.GetGridActions(proxy.Grid);
                    foreach (DataExcelPropertyAction item in list)
                    {
                        if (item.ShortName == eventtext)
                        {
                            ScriptBuilder.Exec(proxy.Grid, cell, item.Script);
                            break;
                        }
                    }
                }
                
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridRefresh(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                proxy.Grid.ReFresh();
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridRefreshExtendCells(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                proxy.Grid.RefreshExtendCells();
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridDialogOK(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                System.Windows.Forms.Form form = proxy.Grid.FindForm();
                form.DialogResult = System.Windows.Forms.DialogResult.OK;
                form.Close();
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridDialogCancel(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                System.Windows.Forms.Form form = proxy.Grid.FindForm();
                form.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                form.Close();
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridShowToolTip(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string tooltip = base.GetTextValue(1, args);
                int showtimes = base.GetIntValue(2, args);
                proxy.Grid.ShowToolTip(tooltip, showtimes);
            }
            return null;
        }

        public virtual object GridNew(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                DataExcel grid = new DataExcel();
                grid.Init();
                return grid;
            }
            return Feng.Utils.Constants.NULL;
        }
        public virtual object GridOpen(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string file = base.GetTextValue(1, args);
                if (System.IO.File.Exists(file))
                {
                    proxy.Grid.Open(file);
                }
                return Feng.Utils.Constants.YES;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridGetData(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                proxy.Grid.GetFileData();
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridLoadData(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                byte[] data = base.GetArgIndex(1, args) as byte[];
                bool result = proxy.Grid.Open(data);
                if (result)
                {
                    return Feng.Utils.Constants.YES;
                }
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridSave(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                proxy.Grid.Save();
                return Feng.Utils.Constants.YES;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridSaveAs(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string file = base.GetTextValue(1, args);
                if (!string.IsNullOrWhiteSpace(file))
                {
                    proxy.Grid.Save(file);
                }
                else
                {
                    proxy.Grid.SaveAs();
                }
                return Feng.Utils.Constants.YES;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridRead(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string file = base.GetTextValue(1, args);
                proxy.Grid.Open(file);
                return Feng.Utils.Constants.YES;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridExecCommand(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string commandexcute = base.GetTextValue(1, args);
                proxy.Grid.CommandExcute(commandexcute);
                return Feng.Utils.Constants.YES;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridEditMode(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            { 
                return proxy.Grid.FileEditMode;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridCopy(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                proxy.Grid.Copy();
                return Feng.Utils.Constants.OK;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridShowRowHeader(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                bool value = base.GetBooleanValue(1, args);
                proxy.Grid.ShowRowHeader = value;
                return Feng.Utils.Constants.OK;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridShowColumnHeader(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                bool value = base.GetBooleanValue(1, args);
                proxy.Grid.ShowColumnHeader = value;
                return Feng.Utils.Constants.OK;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridShowLine(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                if (!base.HasArgs(1, args))
                {
                    return proxy.Grid.ShowGridColumnLine && proxy.Grid.ShowGridRowLine;
                }
                else
                {
                    bool value = base.GetBooleanValue(1, args);
                    proxy.Grid.ShowGridColumnLine = value;
                    proxy.Grid.ShowGridRowLine = value;
                    return Feng.Utils.Constants.OK;
                }
            }
            return null;
        }
        public virtual object GridShowHScroller(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = null;
                if (args.Length == 3)
                {
                    ICell cel = null;
                    int column = -1;
                    cel = base.GetCell(2, args);
                    if (cel != null)
                    {
                        column = cel.Column.Index;
                    }
                    if (column < 0)
                    {
                        column = base.GetIntValue(2, -1, args);
                    }

                    int row = -1;
                    cel = base.GetCell(1, args);
                    if (cel != null)
                    {
                        row = cel.Row.Index;
                    }
                    if (row < 0)
                    {
                        row = base.GetIntValue(1, -1, args);
                    }

                    if (column > 0 && row > 0)
                    {
                        cell = proxy.Grid[row, column];
                        if (cell.OwnMergeCell != null)
                        {
                            return cell.OwnMergeCell;
                        }
                        return cell;
                    }
                }
                else
                {
                    cell = null;
                    string ct = base.GetTextValue(1, args);
                    cell = proxy.Grid.GetCellByNameAndID(ct);
                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            return cell.OwnMergeCell;
                        }
                        return cell;
                    }
                }
            }
            return null;
        }
        public virtual object GridShowVScroller(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string text = base.GetTextValue(1, args);
                int mincolumnindex = base.GetIntValue(2, 0, args);
                int minrowindex = base.GetIntValue(3, 0, args);
                int maxcolumnindex = base.GetIntValue(4, 0, args);
                int maxrowindex = base.GetIntValue(5, 0, args);

                Point pt = Point.Empty;
                if (maxrowindex == 0)
                {
                    if (minrowindex == 0)
                    {
                        pt = proxy.Grid.ReSetHasValue();
                        minrowindex = 1;
                        maxrowindex = pt.X;
                    }
                    else
                    {
                        maxrowindex = minrowindex;
                    }
                }
                if (maxcolumnindex == 0)
                {
                    if (mincolumnindex == 0)
                    {
                        pt = proxy.Grid.ReSetHasValue();
                        mincolumnindex = 1;
                        maxcolumnindex = pt.Y;
                    }
                    else
                    {
                        maxcolumnindex = mincolumnindex;
                    }
                }
                for (int row = minrowindex; row <= maxrowindex; row++)
                {
                    for (int column = mincolumnindex; column <= maxcolumnindex; column++)
                    {
                        ICell cell = proxy.Grid.GetCell(row, column);
                        if (cell != null)
                        {
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            string txt = Feng.Utils.ConvertHelper.ToString(cell.Value);
                            Feng.Utils.TraceHelper.WriteTrace("", "", "FindCell", true, txt);
                            if (text == txt)
                            {
                                return cell;
                            }
                        }
                    }
                }
            }
            return null;
        }
        public virtual object GridFont(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                if (args.Length == 3)
                {
                    Font value = base.GetArgIndex(2, args) as Font;
                    proxy.Grid.Font = value;
                    return Feng.Utils.Constants.OK;
                }
                else
                {
                    return proxy.Grid.Font;
                }
            }
            return null;
        }
        public virtual object GridBackColor(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                if (args.Length == 3)
                {
                    Color value = Feng.Utils.ConvertHelper.ToColor(base.GetIntValue(2, args));
                    proxy.Grid.BackColor = value;
                    return Feng.Utils.Constants.OK;
                }
                else
                {
                    return proxy.Grid.BackColor;
                }
            }
            return null;
        }
        public virtual object GridBackImage(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;

            ICell begincell = null;
            ICell endCell = null;
            SelectCellCollection selectcell = null;
            begincell = this.GetCell(1, args);
            endCell = this.GetCell(2, args);
            if (endCell == null)
            {
                endCell = begincell;
            }
            if (endCell != null)
            {
                selectcell = new SelectCellCollection();
                selectcell.BeginCell = begincell;
                selectcell.EndCell = endCell;
                return selectcell.GetAllCells();
            }
            List<ICell> list = new List<ICell>();
            for (int m = 1; m < args.Length; m++)
            {
                string text = base.GetTextValue(m, args);
                string[] textes = Feng.Utils.TextHelper.Split(text, ",");

                for (int i = 0; i < textes.Length; i++)
                {
                    string cellreange = textes[i];
                    if (!cellreange.Contains(":"))
                    {
                        list.Add(proxy.Grid.GetCellByNameAndID(cellreange));
                        continue;
                    }
                    string[] cells = cellreange.Split(':');
                    if (cells.Length != 2)
                    {
                        continue;
                    }
                    begincell = proxy.Grid.GetCellByNameAndID(cells[0]);
                    if (begincell == null)
                        continue;

                    endCell = proxy.Grid.GetCellByNameAndID(cells[1]);
                    if (endCell == null)
                    {
                        endCell = begincell;
                    }
                    selectcell = new SelectCellCollection();
                    selectcell.BeginCell = begincell;
                    selectcell.EndCell = endCell;
                    list.AddRange(selectcell.GetAllCells());
                }
            }
            return list;
        }
        public virtual object GridShowSelectBorder(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {

                ICell begincell = null;
                ICell endCell = null;
                SelectCellCollection selectcell = null;
                begincell = this.GetCell(1, args);
                endCell = this.GetCell(2, args);
                if (endCell == null)
                {
                    endCell = begincell;
                }
                if (endCell != null)
                {
                    selectcell = new SelectCellCollection();
                    selectcell.BeginCell = begincell;
                    selectcell.EndCell = endCell;
                    return selectcell;
                }

                string cellreange = base.GetTextValue(1, args);
                if (!cellreange.Contains(":"))
                {
                    return null;
                }
                string[] cells = cellreange.Split(':');
                if (cells.Length != 2)
                {
                    return null;
                }
                begincell = proxy.Grid.GetCellByNameAndID(cells[0]);
                if (begincell == null)
                    return null;

                endCell = proxy.Grid.GetCellByNameAndID(cells[1]);
                if (endCell == null)
                {
                    endCell = begincell;
                }
                selectcell = new SelectCellCollection();
                selectcell.BeginCell = begincell;
                selectcell.EndCell = endCell;
                return selectcell;
            }
            return null;
        }
        public virtual object GridShowCell(params object[] args)
        {
            ICell cell = this.GetArgIndex(1, args) as ICell;
            if (cell != null)
            {
                return cell.Name;
            }
            return null;
        }
        public virtual object GridReadFileCellValue(params object[] args)
        {
            string file = base.GetTextValue(1, args);
            string cellstext = base.GetTextValue(2, args);
#warning test
            DataExcel grid = StackList.Pop();
            if (grid == null)
            {
                grid = new DataExcel();
                grid.Init();
            }
            ICell cell = grid[cellstext];
            StackList.Push(grid);
            return cell.Value;
        }
        public virtual object GridReadFileCellJsonValue(params object[] args)
        {
            string file = base.GetTextValue(1, args);
            string cellstext = base.GetTextValue(2, args);
#warning test
            DataExcel grid = StackList.Pop();
            if (grid == null)
            {
                grid = new DataExcel();
                grid.Init();
            }
            //Feng.Json.JsonObj json = new Json.JsonObj();
            string[] cells = Feng.Utils.TextHelper.Split(cellstext, ",");
            foreach (string item in cells)
            {
                ICell cell = grid[item];
                if (cell != null)
                {
                    //json.Values.Add(new Json.JsonItem(item, cell.Value));
                }
            }
            StackList.Push(grid);
            return null;
        }
        public virtual object GridDisplayArea(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                if (args.Length == 3)
                {
                    SelectCellCollection value = base.GetArgIndex(2, args) as SelectCellCollection;
                    proxy.Grid.DisplayArea = value;
                    return Feng.Utils.Constants.OK;
                }
                else
                {
                    return proxy.Grid.DisplayArea;
                }
            }
            return null;
        }
        public virtual object GridSelectRange(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                if (args.Length == 3)
                {
                    SelectCellCollection value = base.GetArgIndex(2, args) as SelectCellCollection;
                    proxy.Grid.SelectCells = value;
                    return Feng.Utils.Constants.OK;
                }
                else
                {
                    return proxy.Grid.SelectCells;
                }
            }
            return null;
        }
        public virtual object GridHashtable(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                if (args.Length > 2)
                {
                    object key = base.GetArgIndex(1, args);
                    object value = base.GetArgIndex(2, args);
                    proxy.Grid.Hash[key] = value;
                    return Feng.Utils.Constants.OK;
                }
                else
                {
                    object key = base.GetArgIndex(1, args);
                    object value = proxy.Grid.Hash[key];
                    return value;
                }
            }
            return null;
        }
        public virtual object GridDataTableFill(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;

            if (proxy != null)
            {
                int minrow = 1;
                int mincolumn = 1;
                int maxrow = 1;
                int maxcolumn = 1;

                SelectCellCollection selectCelles = base.GetArgIndex(1, args) as SelectCellCollection;
                DataTable dataTable = base.GetArgIndex(2, args) as DataTable;
                string tablename = base.GetTextValue(3, "TABLENAME", args);
                bool autoclear = base.GetBooleanValue(4,true, args);
                if (autoclear)
                {
                    Feng.Excel.Table.TableTools.ClearTable(proxy.Grid, tablename);

                }
                if (selectCelles != null)
                {
                    minrow = selectCelles.MinRow();
                    mincolumn = selectCelles.MinColumn();
                    maxrow = selectCelles.MaxRow();
                    maxcolumn = selectCelles.MaxColumn();
                }
                else
                {
                    ICell cell = base.GetArgIndex(1, args) as ICell;
                    if (cell != null)
                    {
                        minrow = cell.Row.Index;
                        mincolumn = cell.Column.Index;
                    }

                    maxrow = minrow + dataTable.Rows.Count;
                    maxcolumn = mincolumn + dataTable.Columns.Count;

                }


                int row = 0;
                int column = 0;
                for (int irow = minrow; irow <= maxrow; irow++)
                {
                    for (int icolumn = mincolumn; icolumn <= maxcolumn; icolumn++)
                    {
                        DataRow dataRow = null;
                        DataColumn dataColumn = null;
                        object value = null;
                        string columnname = string.Empty;
                        if (row < dataTable.Rows.Count)
                        {
                            dataRow = dataTable.Rows[row];
                            if (column < dataTable.Columns.Count)
                            {
                                dataColumn = dataTable.Columns[column];
                                value = dataRow[dataColumn];
                                columnname = dataColumn.ColumnName;
                            }
                        }
                        ICell cell = proxy.Grid[irow, icolumn];
                        if (cell.OwnMergeCell != null)
                        {
                            cell = cell.OwnMergeCell;
                            icolumn = icolumn + cell.MaxColumnIndex - cell.Column.Index;
                        }
                        cell.TableName = tablename;
                        cell.TableRowIndex = row;
                        cell.TableColumnName = columnname;
                        cell.Value = value;
                        column = column + 1;
                    }
                    ICell cellmin = proxy.Grid[irow, mincolumn];
                    if (cellmin.OwnMergeCell != null)
                    {
                        cellmin = cellmin.OwnMergeCell;
                        irow = irow + cellmin.MaxRowIndex - cellmin.Row.Index;
                    }
                    row = row + 1;
                    column = 0;
                }
            }
            return Feng.Utils.Constants.OK;
        }
        public virtual object GridPropertyEvent(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string propertyevent = base.GetTextValue(1, args);
                string text = base.GetTextValue(2, args);
                 
                switch (propertyevent)
                {
                    case "PropertyClick":
                        proxy.Grid.PropertyClick = text;
                        break;
                    case "PropertyDataLoadCompleted":
                        proxy.Grid.PropertyDataLoadCompleted = text;
                        break;
                    case "PropertyDoubleClick":
                        proxy.Grid.PropertyDoubleClick = text;
                        break;
                    case "PropertyEdit":
                        proxy.Grid.PropertyEdit = text;
                        break;
                    case "PropertyNew":
                        proxy.Grid.PropertyNew = text;
                        break;
                    case "PropertyEndEdit":
                        proxy.Grid.PropertyEndEdit = text;
                        break;
                    case "PropertyFormClosing":
                        proxy.Grid.PropertyFormClosing = text;
                        break;
                    case "PropertyKeyDown":
                        proxy.Grid.PropertyKeyDown = text;
                        break;
                    case "PropertyKeyUp":
                        proxy.Grid.PropertyKeyUp = text;
                        break;
                    case "PropertyValueChanged":
                        proxy.Grid.PropertyValueChanged = text;
                        break;
                    //case "AAAAAAAAAAAAAAAAAAAAAAAAAA":
                    //    proxy.Grid.AAAAAAAAAAAAAAAAA = text;
                    //    break; 
                    default:
                        break;
                }
                return Feng.Utils.Constants.OK;

            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridPropertyEventExec(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string propertyevent = base.GetTextValue(1, args).ToUpper();
                string text = string.Empty;

                switch (propertyevent)
                {
                    case "PROPERTYCLICK":
                        text = proxy.Grid.PropertyClick;
                        break;
                    case "PROPERTYDATALOADCOMPLETED":
                        text = proxy.Grid.PropertyDataLoadCompleted;
                        break;
                    case "PROPERTYDOUBLECLICK":
                        text = proxy.Grid.PropertyDoubleClick;
                        break;
                    case "PROPERTYEDIT":
                        text = proxy.Grid.PropertyEdit;
                        break;
                    case "PROPERTYNEW":
                        text = proxy.Grid.PropertyNew;
                        break;
                    case "PROPERTYENDEDIT":
                        text = proxy.Grid.PropertyEndEdit;
                        break;
                    case "PROPERTYFORMCLOSING":
                        text = proxy.Grid.PropertyFormClosing;
                        break;
                    case "PROPERTYKEYDOWN":
                        text = proxy.Grid.PropertyKeyDown;
                        break;
                    case "PROPERTYKEYUP":
                        text = proxy.Grid.PropertyKeyUp;
                        break;
                    case "PROPERTYVALUECHANGED":
                        text = proxy.Grid.PropertyValueChanged;
                        break;
                    //case "AAAAAAAAAAAAAAAAAAAAAAAAAA":
                    //    proxy.Grid.AAAAAAAAAAAAAAAAA ;
                    //    break; 
                    default:
                        break;
                }
                if (!string.IsNullOrWhiteSpace(text))
                {
                    int len = args.Length - 2;
                    object[] array = new object[len];
                    Array.Copy(args, 2, array, 0, len);
                    proxy.Grid.ExecuteAction(new ActionArgs(text, proxy.Grid, proxy.Grid.FocusedCell), text, array);
                }
                return Feng.Utils.Constants.OK;

            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridFunctionExec(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string propertyevent = base.GetTextValue(1, args);
                object value = proxy.Grid.FunctionList[propertyevent];
                string text = Feng.Utils.ConvertHelper.ToString(value);
                if (text != null && !string.IsNullOrWhiteSpace(text))
                {
                    int len = args.Length - 2;
                    object[] array = new object[len];
                    Array.Copy(args, 2, array, 0, len);
                    proxy.Grid.ExecuteAction(new ActionArgs(text, proxy.Grid, proxy.Grid.FocusedCell), text, array);
                }
                return Feng.Utils.Constants.OK;

            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridGetDataTable(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                DataTable table = Table.CellTableTools.GetGirdDataTable(proxy.Grid);
                return table;
            }
            return null;
        }
        public virtual object GridSetDataTable(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                DataTable table = base.GetArgIndex(1, args) as DataTable;
                Table.CellTableTools.SetDataTable(proxy.Grid, table);
                return Constants.OK;
            }
            return Constants.Fail;
        }
        public virtual object GridCellsToTable(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cellbegin = base.GetCell(1,args);
                ICell cellend = base.GetCell(2, args);
                List<int> numcolumns = base.GetArgIndex(3, args) as List<int>;
                List<int> timecolumns = base.GetArgIndex(4, args) as List<int>;
                if (numcolumns == null)
                {
                    numcolumns = new List<int>();
                }
                if (timecolumns == null)
                {
                    timecolumns = new List<int>();
                }
                DataTable table = Table.CellTableTools.ConverSelectesToDataTable(
                    proxy.Grid, cellbegin, cellend, numcolumns, timecolumns);
                return table;
            }
            return null;
        }
        private Feng.Collections.DictionaryEx<string, object> varlist =  null;
        public virtual object GridVar(params object[] args)
        {

            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                if (varlist == null)
                {
                    varlist = new Feng.Collections.DictionaryEx<string, object>();
                }
                string varname = base.GetTextValue(1, args).ToUpper();
                string varvalue = base.GetTextValue(2, args);
                if (args.Length > 2)
                {
                    varlist[varname] = varvalue;
                    return Feng.Utils.Constants.OK;
                }
                else
                {
                    return varlist[varname];
                } 

            }
            return null;
        }
        private static Feng.Collections.StackList<DataExcel> stackList = null;
        protected static Feng.Collections.StackList<DataExcel> StackList
        {
            get
            {
                if (stackList == null)
                {
                    stackList = new Feng.Collections.StackList<DataExcel>();
                }
                return stackList;
            }
        }
    }
}
