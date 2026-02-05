using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;

namespace Feng.Excel.Script
{
    [Serializable]
    public class SelectFunctionContainer : DataExcelMethodContainer
    {
        public const string Function_Name = "SelectMethod";
        public const string Function_Description = "选择";
        public override string Name
        {
            get { return Function_Name; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public SelectFunctionContainer()
        {
            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "SelectDown";
            model.Description = "指定单元格向下搜索  SelectDown(ME,1,10)向下搜索不为空的10条记录";
            model.Eg = @"SelectDown(ME,1,10)";
            model.Function = SelectDown;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SelectUp";
            model.Description = "指定单元格向上搜索  SelectUp(ME,1,10)向上搜索不为空的10条记录";
            model.Eg = @"SelectUp(ME,1,10)";
            model.Function = SelectUp;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SelectLeft";
            model.Description = "指定单元格向左搜索  SelectLeft(ME,1,10)向左搜索不为空的10条记录";
            model.Eg = @"SelectLeft(ME,1,10)";
            model.Function = SelectLeft;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SelectRight";
            model.Description = "指定单元格向右搜索  SelectRight(ME,1,10)向右搜索不为空的10条记录";
            model.Eg = @"SelectRight(ME,1,10)";
            model.Function = SelectRight;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SelectText";
            model.Description = "全局或指定集合中搜索指定文本的单元格  SelectText(this,\"名称\",0)模糊搜索 名称";
            model.Eg = @"SelectText(this,""名称"",0)";
            model.Function = SelectText;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SelectNum";
            model.Description = "全局或指定集合中搜索指数值的单元格  SelectNum(this,\"名称\",0)模糊搜索 名称";
            model.Eg = @"SelectNum(this,""名称"",0)";
            model.Function = SelectNum;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "SelectID";
            model.Description = "全局或指定集合中搜索指数值的单元格  SelectID(SELECT,\"名称\",0)模糊搜索 名称";
            model.Eg = @"SelectID(this,""名称"",0)";
            model.Function = SelectID;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SelectScript";
            model.Description = "全局或指定集合中搜索指数值的单元格  SelectScript(SELECT,\"名称\")模糊搜索 名称";
            model.Eg = @"SelectScript(this,""名称"",0)";
            model.Function = SelectScript;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "SelectExpress";
            model.Description = "全局或指定集合中搜索指数值的单元格  SelectExpress(SELECT,\"名称\")模糊搜索 名称";
            model.Eg = @"SelectExpress(this,""名称"",0)";
            model.Function = SelectExpress;
            MethodList.Add(model);
        }
        public object SelectExpress(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = base.GetCell(1, args);
                ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
                int selecttype = base.GetIntValue(2, 1, args);
                int defaultlen = 100;
                int length = base.GetIntValue(3, defaultlen, args);
                for (int i = 0; i < length; i++)
                {
                    ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                    if (Stop(targetcell, selecttype))
                    {
                        break;
                    }
                    list.Add(targetcell);
                } 
            return list;
        }
        public object SelectScript(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;

            ICell cell = base.GetCell(1, args);
            ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
            int selecttype = base.GetIntValue(2, 1, args);
            int defaultlen = 100;
            int length = base.GetIntValue(3, defaultlen, args);
            for (int i = 0; i < length; i++)
            {
                ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                if (Stop(targetcell, selecttype))
                {
                    break;
                }
                list.Add(targetcell);
            }
            return list;
        }
        public object SelectID(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = base.GetCell(1, args);
            ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
            int selecttype = base.GetIntValue(2, 1, args);
            int defaultlen = 100;
            int length = base.GetIntValue(3, defaultlen, args);
            for (int i = 0; i < length; i++)
            {
                ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                if (Stop(targetcell, selecttype))
                {
                    break;
                }
                list.Add(targetcell);
            }

            return list;
        }
        public object SelectNum(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = base.GetCell(1, args);
            ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
            int selecttype = base.GetIntValue(2, 1, args);
            int defaultlen = 100;
            int length = base.GetIntValue(3, defaultlen, args);
            for (int i = 0; i < length; i++)
            {
                ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                if (Stop(targetcell, selecttype))
                {
                    break;
                }
                list.Add(targetcell);
            }

            return list;
        }
        public object SelectText(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = base.GetCell(1, args);
                ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
                int selecttype = base.GetIntValue(2, 1, args);
                int defaultlen = 100;
                int length = base.GetIntValue(3, defaultlen, args);
                for (int i = 0; i < length; i++)
                {
                    ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                    if (Stop(targetcell, selecttype))
                    {
                        break;
                    }
                    list.Add(targetcell);
                } 
            return list;
        }
        public object SelectDown(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = base.GetCell(1, args);
                ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
                int selecttype = base.GetIntValue(2, 1, args);
                int defaultlen = 100;
                int length = base.GetIntValue(3, defaultlen, args);
                for (int i = 0; i < length; i++)
                {
                    ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                    if (Stop(targetcell, selecttype))
                    {
                        break;
                    }
                    list.Add(targetcell);
                } 
            return list;
        }
        public object SelectUp(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = base.GetCell(1, args);
                ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
                int selecttype = base.GetIntValue(2, 1, args);
                int defaultlen = 100;
                int length = base.GetIntValue(3, defaultlen, args);
                for (int i = 0; i < length; i++)
                {
                    ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                    if (Stop(targetcell, selecttype))
                    {
                        break;
                    }
                    list.Add(targetcell);
                } 
            return list;
        }
        public object SelectLeft(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = base.GetCell(1, args);
                ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
                int selecttype = base.GetIntValue(2, 1, args);
                int defaultlen = 100;
                int length = base.GetIntValue(3, defaultlen, args);
                for (int i = 0; i < length; i++)
                {
                    ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                    if (Stop(targetcell, selecttype))
                    {
                        break;
                    }
                    list.Add(targetcell);
                } 
            return list;
        }
        public object SelectRight(params object[] args)
        {
#warning 需要优化
            Feng.Collections.ListEx<ICell> list = new Feng.Collections.ListEx<ICell>();
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = base.GetCell(1, args);
                ///搜索停止条件 0 不限制 1 空 2 文本 3 数字
                int selecttype = base.GetIntValue(2, 1, args);
                int defaultlen = 100;
                int length = base.GetIntValue(3, defaultlen, args);
                for (int i = 0; i < length; i++)
                {
                    ICell targetcell = proxy.Grid.GetNextEditCell(cell, Enums.NextCellType.Down);
                    if (Stop(targetcell, selecttype))
                    {
                        break;
                    }
                    list.Add(targetcell);
                } 
            return list;
        }

        protected bool Stop(ICell cell,int type)
        {
            switch (type)
            {
                case 0:
                    return false;
                case 1:
                    return string.IsNullOrWhiteSpace(cell.Text);
                case 2:
                    decimal d = 0;
                    return !Feng.Utils.MathHelper.StringIsNumber(cell.Text,out d);
                case 3:
                    decimal d1 = 0;
                    return Feng.Utils.MathHelper.StringIsNumber(cell.Text, out d1);
                default:
                    return true;
            }
        }

 

    }
}
