using Feng.Collections;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class ListFunctionContainer : CBMethodContainer
    {

        public const string Function_Category = "ListFunction";
        public const string Function_Description = "集合";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public ListFunctionContainer()
        {

            BaseMethod model = null;


            model = new BaseMethod();
            model.Name = "List";
            model.Description = "创建一个集合 List()";
            model.Eg = @"var list =List(1,2,3,""aaa"",""bb"")";
            model.Function = List;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ListNew";
            model.Description = "创建一个集合 VAR LIST =ListNew()";
            model.Eg = @"var list =ListNew()";
            model.Function = ListNew;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ListCount";
            model.Description = "返回集合长度";
            model.Eg = @"ListCount(list)";
            model.Function = ListCount;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ListAdd";
            model.Description = "将数据添加至集合";
            model.Eg = @"ListAdd(list,5)";
            model.Function = ListAdd;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ListRemove";
            model.Description = "将数据从集合中删除";
            model.Eg = @"ListRemove(list,5)";
            model.Function = ListRemove;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ListFirst";
            model.Description = "返回集合的第一个值";
            model.Eg = @"var item=ListFirst(list)";
            model.Function = ListFirst;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ListLast";
            model.Description = "返回集合的第一个值 ListLast(list)";
            model.Eg = @"var item=ListLast(list)";
            model.Function = ListLast;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ListIndex";
            model.Description = @"返回集合的第某个值 ListIndex(list,3)";
            model.Eg = @"ListIndex(list,3)";
            model.Function = ListIndex;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ListNext";
            model.Description = "返回集合的下一个值";
            model.Eg = @"ListNext(list,Current())";
            model.Function = ListNext;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ListPrev";
            model.Description = "返回集合的上一个值";
            model.Eg = @"ListPrev(list,Current())";
            model.Function = ListPrev;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ListClear";
            model.Description = "返回集合的下一个值";
            model.Eg = @"ListClear(list,Current())";
            model.Function = ListClear;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ListCopy";
            model.Description = "复制一个集合";
            model.Eg = @"var list2=ListCopy(list)";
            model.Function = ListCopy;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "hashNew";
            model.Description = "创建一个Hash表 VAR LIST =ListNew()";
            model.Eg = @"VAR LIST =hashNew()";
            model.Function = hashNew;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "hashCount";
            model.Description = "返回长度 hashCount(HASH)";
            model.Eg = @"hashCount(HASH)";
            model.Function = hashCount;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "hashAdd";
            model.Description = @"添加数据添加至集合 hashAdd(HASH,""KEY"",5)";
            model.Eg = @"hashAdd(HASH,5)";
            model.Function = hashAdd;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "hashGetValue";
            model.Description = @"从集合中获取值 hashGetValue(HASH,""KEY"")";
            model.Eg = @"hashGetValue(HASH)";
            model.Function = hashGetValue;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "hashRemove";
            model.Description = @"将数据从集合中删除 hashRemove(HASH,""KEY"")";
            model.Eg = @"hashRemove(HASH,5)";
            model.Function = hashRemove;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "hashIndex";
            model.Description = @"返回集合的第某个值 ListIndex(HASH,3)";
            model.Eg = @"hashIndex(HASH,3)";
            model.Function = hashIndex;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "hashClear";
            model.Description = "返回集合的下一个值 hashClear(HASH)";
            model.Eg = @"hashClear(HASH)";
            model.Function = hashClear;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "hashCopy";
            model.Description = "复制一个集合 hashCopy(HASH)";
            model.Eg = @"hashCopy(HASH)";
            model.Function = hashCopy;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "hashKeyList";
            model.Description = "返回KEY列表 hashKeyList(HASH)";
            model.Eg = @"hashKeyList(HASH)";
            model.Function = hashKeyList;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ArrayInit";
            model.Description = "创建一个集合 VAR Array =ArrayInit(1,2,3,4,5,6)";
            model.Eg = @"VAR Array =ArrayInit(1,2,3,4,5,6)";
            model.Function = ArrayInit;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Array";
            model.Description = "创建一个集合 var Array =Array(1,2,3,4,5,6)";
            model.Eg = @"var arr =Array(1,2,3,4,5,6)";
            model.Function = ArrayInit;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ArrayNew";
            model.Description = "创建一个集合 VAR Array =ArrayNew(10)";
            model.Eg = @"VAR Array =ArrayNew(10)";
            model.Function = ArrayNew;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ArrayCount";
            model.Description = "返回集合长度";
            model.Eg = @"var len=ArrayCount(arr)";
            model.Function = ArrayCount;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ArrayIndex";
            model.Description = @"返回集合的第某个值 ListIndex(ARRAY,3)";
            model.Eg = @"ArrayIndex(ARRAY,3)";
            model.Function = ArrayIndex;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ArrayCopy";
            model.Description = "复制一个集合 ArrayCopy(ARRAY)";
            model.Eg = @"ArrayCopy(ARRAY)";
            model.Function = ArrayCopy;
            MethodList.Add(model);
        }
        public virtual object List(params object[] args)
        {
            Feng.Collections.ListEx<object> list = new Feng.Collections.ListEx<object>();
            for (int i = 1; i < args.Length; i++)
            {
                list.Add(args[i]);
            }
            return list;
        }
        public virtual object ListNew(params object[] args)
        {
            Feng.Collections.ListEx<object> list = new Feng.Collections.ListEx<object>();
            for (int i = 1; i < args.Length; i++)
            {
                list.Add(args[i]);
            }
            return list;
        }

        public virtual object ListCount(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            return list.Count;
        }
        public virtual object ListFirst(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public virtual object ListLast(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            if (list.Count > 0)
            {
                return list[list.Count - 1];
            }
            return null;
        }
        public virtual object ListClear(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            list.Clear();
            return Feng.Utils.Constants.OK;
        }
        public virtual object ListAdd(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            object value = base.GetArgIndex(2, args);
            bool res = base.GetBooleanValue(3, args);
            if (value == null)
            {
                return Feng.Utils.Constants.Fail;
            }
            if (res)
            {
                if (list.Contains(value))
                {
                    return Feng.Utils.Constants.OK;
                }
            }
            list.Add(value);
            return Feng.Utils.Constants.OK;
        }
        public virtual object ListRemove(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            object value = base.GetArgIndex(2, args);
            list.Remove(value);
            return Feng.Utils.Constants.OK;
        }
        public virtual object ListNext(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            object value = base.GetArgIndex(2, args);
            int index = list.IndexOf(value);
            if (index >= 0)
            {
                index = index + 1;
            }
            if (list.Count > index)
            {
                return list[index];
            }
            return Feng.Utils.Constants.OK;
        }
        public virtual object ListPrev(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            object value = base.GetArgIndex(2, args);
            int index = list.IndexOf(value);
            if (index > 0)
            {
                index = index - 1;
            }
            return list[index];
        }
        public virtual object ListIndex(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            if (list == null)
                return null;
            int index = base.GetIntValue(2, args);
            if (args.Length > 3)
            {
                object value = base.GetArgIndex(3, args);
                list[index] = value;
            }
            else
            {
                return list[index];
            }
            return null;
        }
        public virtual object ListCopy(params object[] args)
        {
            List<object> list = base.GetArgIndex(1, args) as List<object>;
            List<object> listtargget = new List<object>();
            listtargget.AddRange(list.ToArray());
            return listtargget;
        }



        public virtual object hashNew(params object[] args)
        {

            HashtableEx list = new HashtableEx();
            return list;
        }
        public virtual object hashCount(params object[] args)
        {
            HashtableEx list = base.GetArgIndex(1, args) as HashtableEx;
            return list.Count;
        }
        public virtual object hashClear(params object[] args)
        {
            HashtableEx list = base.GetArgIndex(1, args) as HashtableEx;
            list.Clear();
            return Feng.Utils.Constants.OK;
        }
        public virtual object hashAdd(params object[] args)
        {
            HashtableEx list = base.GetArgIndex(1, args) as HashtableEx;
            object key = base.GetArgIndex(2, args);
            object value = base.GetArgIndex(3, args);
            if (list.ContainsKey(key))
            {
                list[key] = value;
            }
            else
            {
                list.Add(key, value);
            }
            return Feng.Utils.Constants.OK;
        }
        public virtual object hashGetValue(params object[] args)
        {
            HashtableEx list = base.GetArgIndex(1, args) as HashtableEx;
            object key = base.GetArgIndex(2, args);
            return list[key];
        }
        public virtual object hashRemove(params object[] args)
        {
            HashtableEx list = base.GetArgIndex(1, args) as HashtableEx;
            object key = base.GetArgIndex(2, args);
            list.Remove(key);
            return Feng.Utils.Constants.OK;
        }
        public virtual object hashCopy(params object[] args)
        {
            HashtableEx list = base.GetArgIndex(1, args) as HashtableEx;
            HashtableEx listtargget = new HashtableEx();
            foreach (var item in list.Keys)
            {
                listtargget.Add(item, list[item]);
            }
            return listtargget;
        }
        public virtual object hashIndex(params object[] args)
        {
            HashtableEx list = base.GetArgIndex(1, args) as HashtableEx;
            if (list == null)
                return null;
            int index = base.GetIntValue(2, args);
            return list.Index(index);
        }
        public virtual object hashKeyList(params object[] args)
        {
            HashtableEx list = base.GetArgIndex(1, args) as HashtableEx;
            List<object> listtargget = new List<object>();
            foreach (var item in list.Keys)
            {
                listtargget.Add(item);
            }
            return listtargget;
        }



        public virtual object ArrayInit(params object[] args)
        {
            object[] list = new object[args.Length - 1];
            for (int i = 1; i < args.Length; i++)
            {
                list[i - 1] = args[i];
            }
            return list;
        }
        public virtual object ArrayNew(params object[] args)
        {
            int count = base.GetIntValue(1, args);
            return new object[count]; ;
        }
        public virtual object ArrayCount(params object[] args)
        {
            object[] list = base.GetArgIndex(1, args) as object[];
            return list.Length;
        }
        public virtual object ArrayIndex(params object[] args)
        {
            object[] list = base.GetArgIndex(1, args) as object[];
            if (list == null)
                return null;
            int index = base.GetIntValue(2, args);
            if (args.Length > 3)
            {
                object value = base.GetArgIndex(3, args);
                list[index] = value;
            }
            else
            {
                return list[index];
            }
            return null;
        }
        public virtual object ArrayCopy(params object[] args)
        {
            object[] list = base.GetArgIndex(1, args) as object[];

            return list.Clone();
        }
    }
}
