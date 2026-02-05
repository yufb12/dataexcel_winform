using Feng.Excel.Builder;
using Feng.Excel.Collections;
using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Feng.Excel.Script
{
    [Serializable]
    public class ScriptFunctionContainer : DataExcelMethodContainer
    {

        public const string Function_Name = "DataExcelScript";
        public const string Function_Description = "脚本函数";
        public override string Name
        {
            get { return Function_Name; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public ScriptFunctionContainer()
        {
            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "ScriptExec";
            model.Description = "执行脚本";
            model.Eg = @"=ScriptExec(""var netresult=sum(1, 2, 3, 4);"") 结果等于10";
            model.Function = ScriptExec;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ScriptExecExpress";
            model.Description = "执行脚本";
            model.Eg = @"=ScriptExecExpress(""sum(1, 2, 3, 4)"") 结果等于10";
            model.Function = ScriptExecExpress;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ScriptIF";
            model.Description = "第一参数为True返回第二个参数，否则返加第三个参数";
            model.Eg = @"ScriptIF(1>2,10,30)结果等于30";
            model.Function = ScriptIF;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ScriptIsNull";
            model.Description = "第一参数为null返回第二个参数，否则返加第一个参数";
            model.Eg = @"ScriptIsNull(null,1)";
            model.Function = ScriptIsNull;
            MethodList.Add(model);


            //model = new BaseMethod();
            //model.Name = "ScriptArg";
            //model.Description = @"获取或设置参数 ScriptArg(""argname"")";
            //model.Eg = @"ScriptArg(""argname"")";
            //model.Function = ScriptArg;
            //MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ScriptExecG";
            model.Description = @"在传第一个参数表格上执行脚本 ScriptExecG(grid)";
            model.Eg = @"ScriptExecG(grid)";
            model.Function = ScriptExecG;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptSaveContext";
            model.Description = @"保存当前执行上下文 ScriptSaveContext()";
            model.Eg = @"var context=ScriptSaveContext()";
            model.Function = ScriptSaveContext;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptReStoreContext";
            model.Description = @"还原当前上下文 ScriptReStoreContext(context)";
            model.Eg = @"ScriptReStoreContext(context)";
            model.Function = ScriptReStoreContext;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptSwitchContext";
            model.Description = @"切换当前上下文 ScriptSwitchContext(grid,cell)";
            model.Eg = @"ScriptSwitchContext(grid,cell)";
            model.Function = ScriptSwitchContext;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptCellValueChanged";
            model.Description = @"获取或设置单元格值变化时脚本 ScriptCellValueChanged(cell,script)";
            model.Eg = @"ScriptCellValueChanged(cell,script)";
            model.Function = ScriptCellValueChanged;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ScriptCellClick";
            model.Description = @"获取或设置单元格单击时脚本 ScriptCellClick(cell,script)";
            model.Eg = @"ScriptCellClick(cell,script)";
            model.Function = ScriptCellClick;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptCellDoubleClick";
            model.Description = @"获取或设置单元格双击时脚本 ScriptCellDoubleClick(cell,script)";
            model.Eg = @"ScriptCellDoubleClick(cell,script)";
            model.Function = ScriptCellDoubleClick;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ScriptCellKeyDown";
            model.Description = @"获取或设置单元格键盘按下时脚本 ScriptCellKeyDown(cell,script)";
            model.Eg = @"ScriptCellKeyDown(cell,script)";
            model.Function = ScriptCellKeyDown;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptCellKeyUp";
            model.Description = @"获取或设置单元格键盘抬起时脚本 ScriptCellKeyUp(cell,script)";
            model.Eg = @"ScriptCellKeyUp(cell,script)";
            model.Function = ScriptCellKeyUp;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "ScriptGridEndEdit";
            model.Description = @"获取或设置表格结束编辑时脚本 ScriptGridEndEdit(grid,script)";
            model.Eg = @"ScriptGridEndEdit(grid,script)";
            model.Function = ScriptGridEndEdit;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ScriptGridLoadCompleted";
            model.Description = @"获取或设置表格加载结束时脚本 ScriptGridLoadCompleted(grid,script)";
            model.Eg = @"ScriptGridLoadCompleted(grid,script)";
            model.Function = ScriptGridLoadCompleted;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptGridClick";
            model.Description = @"获取或设置表格单击脚本 ScriptGridClick(grid,script)";
            model.Eg = @"ScriptGridClick(grid,script)";
            model.Function = ScriptGridClick;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptGridDoubleClick";
            model.Description = @"获取或设置表格双击脚本 ScriptGridDoubleClick(grid,script)";
            model.Eg = @"ScriptGridDoubleClick(grid,script)";
            model.Function = ScriptGridDoubleClick;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptGridValueChanged";
            model.Description = @"获取或设置表格值改变时脚本 ScriptGridValueChanged(grid,script)";
            model.Eg = @"ScriptGridValueChanged(grid,script)";
            model.Function = ScriptGridValueChanged;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ScriptGridKeyDown";
            model.Description = @"获取或设置表格键按下时脚本 ScriptGridKeyDown(grid,script)";
            model.Eg = @"ScriptGridKeyDown(grid,script)";
            model.Function = ScriptGridKeyDown;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ScriptGridKeyUp";
            model.Description = @"获取或设置表格键抬起时脚本 ScriptGridKeyUp(grid,script)";
            model.Eg = @"ScriptGridKeyUp(grid,script)";
            model.Function = ScriptGridKeyUp;
            MethodList.Add(model);
        }
        public virtual object ScriptGridKeyUp(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                DataExcel cell = base.GetArgIndex(1, args) as DataExcel;
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyKeyUp;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyKeyUp = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptGridKeyDown(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                DataExcel cell = base.GetArgIndex(1, args) as DataExcel;
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyKeyDown;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyKeyDown = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptGridValueChanged(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                DataExcel cell = base.GetArgIndex(1, args) as DataExcel;
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyValueChanged;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyValueChanged = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptGridDoubleClick(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                DataExcel cell = base.GetArgIndex(1, args) as DataExcel;
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyDoubleClick;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyDoubleClick = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptGridClick(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                DataExcel cell = base.GetArgIndex(1, args) as DataExcel;
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyClick;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyClick = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptGridLoadCompleted(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                DataExcel cell = base.GetArgIndex(1, args) as DataExcel;
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyDataLoadCompleted;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyDataLoadCompleted = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptFormClosing(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                DataExcel cell = base.GetArgIndex(1, args) as DataExcel;
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyFormClosing;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyFormClosing = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptGridEndEdit(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                DataExcel cell = base.GetArgIndex(1, args) as DataExcel;
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyEndEdit;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyEndEdit = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptCellKeyUp(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyOnKeyUp;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyOnKeyUp = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptCellKeyDown(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyOnKeyDown;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyOnKeyDown = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptCellDoubleClick(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyOnDoubleClick;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyOnDoubleClick = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptCellClick(params object[] args)
        {  
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyOnClick;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyOnClick = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }

        public virtual object ScriptCellValueChanged(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                if (args.Length == 2)
                {
                    return cell.PropertyOnCellValueChanged;
                }
                string script = base.GetTextValue(2, args);
                cell.PropertyOnCellValueChanged = script;
                return Feng.Utils.Constants.OK;
            }
            return null;
        }
 
        public virtual object ScriptIsNull(params object[] args)
        {
            if (args.Length > 1)
            {
                object value1 = args[1];
                object value2 = args[2];
                if (value1 == null)
                {
                    return value2;
                }
                else
                {
                    return value1;
                }
            }
            return null;
        }

        public virtual object ScriptIF(params object[] args)
        {
            if (args.Length > 2)
            {
                object value1 = args[1];
                object value2 = args[2];
                object value3 = args[3];
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value1);
                if (res)
                {
                    return value2;
                }
                else
                {
                    return value3;
                }
            }
            return null;
        }

        public virtual object ScriptExec(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string txt = base.GetTextValue(1, args);
                int len = args.Length - 2;
                if (len > 0)
                {
                    object[] values = new object[len];
                    for (int i = 0; i < len; i++)
                    {
                        values[i] = args[i + 2];
                    }
                    return ScriptBuilder.Exec(proxy.Grid, proxy.CurrentCell, txt, values);
                }
                return ScriptBuilder.Exec(proxy.Grid, proxy.CurrentCell, txt);
            }
            return null;
        }


        public virtual object ScriptExecExpress(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string txt = base.GetTextValue(1, args);
                int len = args.Length - 2;
                if (len > 0)
                {
                    object[] values = new object[len];
                    for (int i = 0; i < len; i++)
                    {
                        values[i] = args[i + 2];
                    }
                    return ScriptBuilder.ExecExpress(proxy.Grid, proxy.CurrentCell, txt, values);
                }
                return ScriptBuilder.ExecExpress(proxy.Grid, proxy.CurrentCell, txt);
            }
            return null;
        }


        public virtual object ScriptExecG(params object[] args)
        {
            DataExcel grid = base.GetArgIndex(1, args) as DataExcel;
            string txt = base.GetTextValue(2, args);
            int len = args.Length - 3;
            if (len > 0)
            {
                object[] values = new object[len];
                for (int i = 0; i < len; i++)
                {
                    values[i] = args[i + 3];
                }
                return ScriptBuilder.Exec(grid, null, txt, values);
            }
            return ScriptBuilder.Exec(grid, null, txt);

        }

        public virtual object ScriptSaveContext(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                Feng.Collections.HashtableEx scriptContext = new Feng.Collections.HashtableEx();
                scriptContext.Add("Grid", proxy.Grid);
                scriptContext.Add("Cell", proxy.CurrentCell);
                return scriptContext;
            }
            return null;
        }

        public virtual object ScriptReStoreContext(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                Feng.Collections.HashtableEx scriptContext = base.GetArgIndex(1, args) as Feng.Collections.HashtableEx;
                if (scriptContext != null)
                {
                    proxy.Grid = scriptContext["Grid"] as DataExcel;
                    proxy.CurrentCell= scriptContext["Cell"] as ICell;
                    return Feng.Utils.Constants.OK;
                }
            }
            return Feng.Utils.Constants.Fail;
        }

        public virtual object ScriptSwitchContext(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                proxy.Grid = base.GetArgIndex(1, args) as DataExcel;
                proxy.CurrentCell = base.GetArgIndex(2, args) as ICell;
                return Feng.Utils.Constants.OK; 
            }
            return Feng.Utils.Constants.Fail;
        }
    }
}
