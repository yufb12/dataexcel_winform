using Feng.Excel.Interfaces;
using Feng.Excel.Script;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;

namespace Feng.DataDesign
{

    [Serializable]
    public class CustomFunctions : DataExcelMethodContainer
    {
        public const string Function_Category = "自定义函数";
        public CustomFunctions()
        {
            BaseMethod model = new BaseMethod();
            model.Name = "WebService_GetInfo";
            model.Description = "自定义函数";
            model.Function = WebService_GetInfo;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MonthlySales";
            model.Description = "月销售额查询";
            model.Function = MonthlySales;
            MethodList.Add(model);
        }

        public override string Name
        {
            get { return "自定义函数"; }
        }
        public override string Description
        {
            get { return "自定义函数"; }
        }
        public virtual object WebService_GetInfo(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            return "这是自定义函数返回值"; 
        }



        public virtual object MonthlySales(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ///args第一个永远为 ICBContext
            ///ICBContext 包括属性 执行函数当前的表格与当前的单元格 
            ///Grid 属性与 CurrentCell属性

            ICell cell = proxy.CurrentCell;

            decimal d = base.GetDecimalValue(1, args);
            return 2345.78m * d; 
        }
    }

}
