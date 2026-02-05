//using Feng.Script.CBEexpress;
//using Feng.Script.Method;
//using System;
//using System.Collections.Generic;

//namespace Feng.Excel.Script
//{
//    [Serializable]
//    public class LogicalFunctionContainer : CBMethodContainer
//    {

//        public const string Function_Category = "LogicalFunction";
//        public const string Function_Description = "逻辑运算";
//        public override string Name
//        {
//            get { return Function_Category; }

//        }
//        public override string Description
//        {
//            get { return Function_Description; }
//        }
//        public LogicalFunctionContainer(IMethodCollection methods) :
//            base(methods)
//        {

//            BaseMethod model = new BaseMethod();
//            model.Name = "FunIF";
//            model.Description = "IF判断  参数1为真时 返回参数2 否则返回参数3";
//            model.Eg = @"FunIF(1,""Cell(""A5"")"",""Cell(""A5"")"")";
//            model.Function = FunIF;
//            MethodList.Add(model);

//            model = new BaseMethod();
//            model.Name = "FunIFScript";
//            model.Description = "执行判断脚本 参数1为真时 执行参数2脚本 否则执行参数3脚本";
//            model.Eg = @"FunIFScript(1,""SetCellValue(""A5"",1)"",""SetCellValue(""A5"",0)"")";
//            model.Function = FunIFScript;
//            MethodList.Add(model);

//            model = new BaseMethod();
//            model.Name = "FunSwithScript";
//            model.Description = "执行判断脚本 参数1为序号 为1执行参数2 为2时执行参数3 类推";
//            model.Eg = @"FunSwithScript(1,""SetCellValue(""A5"",1)"",""SetCellValue(""A5"",0)"")";
//            model.Function = FunSwithScript;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "FunForeach";
//            model.Description = "循环执行";
//            model.Eg = @"FunForeach(Cells(""A1:A10""),""SetCellValue(""A5"",Cell(""A5"")+Item)"")";
//            model.Function = FunForeach;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "SetVar";
//            model.Description = "设置临时变量值";
//            model.Eg = @"SetVar(""key"",Cell(""A5""))";
//            model.Function = SetVar;
//            MethodList.Add(model);



//            model = new BaseMethod();
//            model.Name = "GetVar";
//            model.Description = "获取临时变量值";
//            model.Eg = @"GetVar(""key"",Cell(""A5""))";
//            model.Function = GetVar;
//            MethodList.Add(model);

//            model = new BaseMethod();
//            model.Name = "IsEmpty";
//            model.Description = "值是否为空字符";
//            model.Eg = @"IsEmpty(Cell(""A5""))";
//            model.Function = IsEmpty;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "IsNull";
//            model.Description = "值是否为空或者NULL";
//            model.Eg = @"IsNull(Cell(""A5""))";
//            model.Function = IsNull;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "IsNumber";
//            model.Description = "值是否为数字类型";
//            model.Eg = @"IsNumber(Cell(""A5""))";
//            model.Function = IsNumber;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "IsDateTime";
//            model.Description = "值是否为数字类型";
//            model.Eg = @"IsDateTime(Cell(""A5""))";
//            model.Function = IsDateTime;
//            MethodList.Add(model);

//            model = new BaseMethod();
//            model.Name = "IsDateTime";
//            model.Description = "值是否为数字类型";
//            model.Eg = @"IsDateTime(Cell(""A5""))";
//            model.Function = IsDateTime;
//            MethodList.Add(model);
//        }

//        public virtual object FunIF(params object[] args)
//        {
//            if (args.Length > 2)
//            {
//                object value1 = args[1];
//                object value2 = args[2];
//                object value3 = args[3];
//                bool res = Feng.Utils.ConvertHelper.ToBoolean(value1);
//                if (res)
//                {
//                    return value2;
//                }
//                else
//                {
//                    return value3;
//                }
//            }
//            return null;
//        }

//        public virtual object FunIFScript(params object[] args)
//        {
//            Feng.Script.CBEexpress.proxy = args[0] as Feng.Excel.Script.DataExcelScriptStmtProxy;
//            if (proxy != null)
//            {
//                return null;
//            }
//            bool res = base.GetBooleanValue(1, args);
//            if (res)
//            {
//                return Function.RunExpress(proxy.Grid, proxy.CurrentCell, base.GetTextValue(2, args));
//            }
//            else
//            {
//                return Function.RunExpress(proxy.Grid, proxy.CurrentCell, base.GetTextValue(3, args));
//            }
//        }

//        public virtual object GetVar(params object[] args)
//        {
//            Feng.Excel.Script.DataExcelScriptStmtProxy proxy = args[0] as Feng.Excel.Script.DataExcelScriptStmtProxy;
//            if (proxy == null)
//            {
//                return null;
//            }
//            string key = base.GetTextValue(1, args);
//            return proxy.GetKeyValue(key);

//        }

//        public virtual object SetVar(params object[] args)
//        {
//            Feng.Excel.Script.DataExcelScriptStmtProxy proxy = args[0] as Feng.Excel.Script.DataExcelScriptStmtProxy;
//            if (proxy == null)
//            {
//                return null;
//            }
//            string key = base.GetTextValue(1, args);
//            object value = base.GetArgIndex(2, args);
//            if (proxy.KeyValues.ContainsKey(key))
//            {
//                proxy.KeyValues[key] = value;
//            }
//            else
//            {
//                proxy.KeyValues.Add(key, value);
//            }
//            return Feng.Utils.Constants.OK; ;
//        }

//        public virtual object FunForeach(params object[] args)
//        {
//            Feng.Excel.Script.DataExcelScriptStmtProxy proxy = args[0] as Feng.Excel.Script.DataExcelScriptStmtProxy;
//            if (proxy != null)
//            {
//                return null;
//            }
//            List<object> list = base.GetArgIndex(1, args) as List<object>;
//            string text = base.GetTextValue(2, args);
//            foreach (object item in list)
//            {
//                proxy.Item = item;
//                Function.RunExpress(proxy.Grid, proxy.CurrentCell, text);
//            }
//            return Feng.Utils.Constants.OK;
//        }

//        public virtual object FunSwithScript(params object[] args)
//        {

//            Feng.Excel.Script.DataExcelScriptStmtProxy proxy = args[0] as Feng.Excel.Script.DataExcelScriptStmtProxy;
//            if (proxy != null)
//            {
//                return null;
//            }

//            int value = base.GetIntValue(1, args);
//            string text = base.GetTextValue(value + 1, args);
//            return Function.RunExpress(proxy.Grid, proxy.CurrentCell, text);

//        }

//        public virtual object IsEmpty(params object[] args)
//        {
//            string value = base.GetTextValue(1, args);
//            return string.IsNullOrWhiteSpace(value);
//        }

//        public virtual object IsNull(params object[] args)
//        {
//            object value = base.GetArgIndex(1, args);
//            bool result = value is null;

//            if (args.Length == 3)
//            {
//                if (result)
//                {
//                    return base.GetArgIndex(2, args);
//                }
//            }
//            return value;
//        }

//        public virtual object IsNumber(params object[] args)
//        {
//            object value = base.GetArgIndex(1, args);
//            if (value == null)
//                return false;
//            return Feng.Utils.ConvertHelper.IsNumber(value.GetType());

//        }

//        public virtual object IsDateTime(params object[] args)
//        {
//            object value = base.GetArgIndex(1, args);
//            if (value == null)
//                return false;
//            return Feng.Utils.ConvertHelper.IsDateTime(value.GetType());

//        }


//    }
//}
