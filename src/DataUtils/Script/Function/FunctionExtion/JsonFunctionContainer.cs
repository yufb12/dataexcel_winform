using Feng.Json;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class JsonFunctionContainer : CBMethodContainer
    {

        public const string Function_Category = "Json";
        public const string Function_Description = "Json函数";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public JsonFunctionContainer()  
        {
            BaseMethod model = null;


            model = new BaseMethod();
            model.Name = "JsonNew";
            model.Description = @"创建新的Json对象";
            model.Eg = @"JsonNew()";
            model.Function = JsonNew;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "JsonParse";
            model.Description = @"从字符串解析Json JsonParse(text)";
            model.Eg = @"JsonParse(text)";
            model.Function = JsonParse;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "JsonGet";
            model.Description = @"获取Json值 JsonGet(json,""KEY"")";
            model.Eg = @"JsonGet(json,""KEY"")";
            model.Function = JsonGet;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "JsonSet";
            model.Description = @"设置Json值 JsonSet(json,""KEY"",123)";
            model.Eg = @"JsonSet(json,""KEY"",123)";
            model.Function = JsonGet;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "JsonItem";
            model.Description = @"获取Json值 JsonItem(json,""KEY"")";
            model.Eg = @"JsonItem(json,""KEY"")";
            model.Function = JsonItem;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Json";
            model.Description = @"获取Json值(json,""KEY"") JsonItemKey与JsonArrayIndex的组合使用";
            model.Eg = @"Json(json,""KEY"")";
            model.Function = Json;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "JsonToString";
            model.Description = @"转为字符串 JsonToString(json)";
            model.Eg = @"JsonToString(json)";
            model.Function = JsonToString;
            MethodList.Add(model);

#warning 消息处理队列，超过的返回
            model = new BaseMethod();
            model.Name = "JsonObject";
            model.Description = @"返回Json对象 JsonObject(""key"",value)";
            model.Eg = @"JsonObject()";
            model.Function = JsonObject;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "JsonArrary";
            model.Description = @"返回Json数组 JsonArrary()";
            model.Eg = @"JsonArrary()";
            model.Function = JsonArrary;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "JsonValue";
            model.Description = @"返回常数Json JsonValue(123)";
            model.Eg = @"JsonValue()";
            model.Function = JsonValue;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "JsonItemCount";
            model.Description = @"获取值数量 JsonItemCount(json)";
            model.Eg = @"JsonItemCount(json,1)";
            model.Function = JsonItemCount;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "JsonItemIndex";
            model.Description = @"获取值 JsonItemIndex(json,1)";
            model.Eg = @"JsonItemIndex(json,1)";
            model.Function = JsonItemIndex;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "JsonItemKey";
            model.Description = @"获取值 JsonItemKey(Json,""KEY"")";
            model.Eg = @"JsonItemKey(Json,""KEY"")";
            model.Function = JsonItemKey;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "JsonItemAdd";
            model.Description = @"添加值到Json JsonItemAdd(Json,""KEY"",Cell(""AB""))";
            model.Eg = @"JsonItemAdd(Json,""KEY"",Cell(""AB""))";
            model.Function = JsonItemAdd;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "JsonItemRemove";
            model.Description = @"从jons属性移除 JsonItemRemove(json,""KEY"")";
            model.Eg = @"JsonItemRemove(json,""KEY"")";
            model.Function = JsonItemRemove;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "JsonArrayRemove";
            model.Description = @"从json数组中移除 JsonArrayRemove(jsonarr,[1,json])";
            model.Eg = @"JsonArrayRemove(jsonarr,1)";
            model.Function = JsonArrayRemove;
            MethodList.Add(model); 

            model = new BaseMethod();
            model.Name = "JsonArrayAdd";
            model.Description = @"从json数组中加入 JsonArrayAdd(jsonarr,[json,123,key:value])";
            model.Eg = @"JsonArrayAdd(jsonarr,json)";
            model.Function = JsonArrayAdd;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "JsonArrayCount";
            model.Description = @"返回json数组数量 JsonArrayCount(jsonarr)";
            model.Eg = @"JsonArrayCount(jsonarr)";
            model.Function = JsonArrayCount;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "JsonArrayIndex";
            model.Description = @"返回json数组中索引处的值 JsonArrayIndex(jsonarr,1)";
            model.Eg = @"JsonArrayIndex(jsonarr,1)";
            model.Function = JsonArrayIndex;
            MethodList.Add(model);
        }

        public virtual object JsonArrayCount(params object[] args)
        {
            Feng.Json.FJsonArray json = base.GetArgIndex(1, args) as Feng.Json.FJsonArray;
                  return json.Count;
        }

        public virtual object JsonArrayIndex(params object[] args)
        {
            Feng.Json.FJsonArray json = base.GetArgIndex(1, args) as Feng.Json.FJsonArray; 
            int index = base.GetIntValue(2, args);
            return json[index]; 
        }

        public virtual object JsonArrayAdd(params object[] args)
        {
            Feng.Json.FJsonArray json = base.GetArgIndex(1, args) as Feng.Json.FJsonArray;
            if (args.Length == 4)
            {
                string jsonkey = base.GetTextValue(2, args);
                object jsonvalue = base.GetArgIndex(3, args);
                json.Add(jsonkey, jsonvalue);
                return Feng.Utils.Constants.YES;
            }
            Feng.Json.FJsonBase obj = base.GetArgIndex(2, args) as Feng.Json.FJsonBase;
            if (obj != null)
            {
                json.Add(obj);
                return Feng.Utils.Constants.YES;
            }
            object value= base.GetArgIndex(2, args);
            if (value!=null)
            {
                json.Add(value);
                return Feng.Utils.Constants.YES;
            }
            return Feng.Utils.Constants.Fail;
        }

        public virtual object JsonArrayRemove(params object[] args)
        {
            Feng.Json.FJsonArray json = base.GetArgIndex(1, args) as Feng.Json.FJsonArray;
            Feng.Json.FJsonBase obj = base.GetArgIndex(2, args) as Feng.Json.FJsonBase;
            if (obj != null)
            {
                json.Remmove(obj);
                return Feng.Utils.Constants.YES;
            }
            int index = base.GetIntValue(2, -1, args);
            if (index >= 0)
            {
                json.Remmove(index);
                return Feng.Utils.Constants.YES;
            }
            return Feng.Utils.Constants.Fail;
        }

        public virtual object JsonToString(params object[] args)
        {
            Feng.Json.FJsonBase json = base.GetArgIndex(1, args) as Feng.Json.FJsonBase;
            return json.ToString();
        }

        public virtual object JsonObject(params object[] args)
        {
            Feng.Json.FJson json = new FJson();
            string key = base.GetTextValue(1, args);
            if (!string.IsNullOrWhiteSpace(key))
            {
                object value = base.GetArgIndex(2, args);
                json.Add(key, value);
            }

            return json;
        }

        public virtual object JsonArrary(params object[] args)
        {
            Feng.Json.FJsonArray json = new FJsonArray();
            return json;
        }

        public virtual object JsonValue(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            if (value is Feng.Json.FJsonValue)
            {
                return (value as Feng.Json.FJsonValue).Value;
            }
            Feng.Json.FJsonValue json = new FJsonValue();
            json.Value = value;
            return json;
        }

        public virtual object JsonItemCount(params object[] args)
        {
            Feng.Json.FJson json = base.GetArgIndex(1, args) as Feng.Json.FJson;
            if (json != null)
            {
                return json.Items.Count;
            }
            return 0;
        }

        public virtual object JsonItemIndex(params object[] args)
        {
            Feng.Json.FJsonBase json = base.GetArgIndex(1, args) as Feng.Json.FJsonBase;
            int index = base.GetIntValue(2, args);
            return json[index];
        }

        public virtual object JsonItemKey(params object[] args)
        {
            Feng.Json.FJsonBase json = base.GetArgIndex(1, args) as Feng.Json.FJsonBase;
            string key = base.GetTextValue(2, args);
            return json[key];
        }

        public virtual object JsonItemAdd(params object[] args)
        {
            Feng.Json.FJson json = base.GetArgIndex(1, args) as Feng.Json.FJson;
            string key = base.GetTextValue(2, args);
            object value = base.GetArgIndex(3, args);
            json.Add(key, value); 
            return json;
        }

        public virtual object JsonItemRemove(params object[] args)
        {
            Feng.Json.FJson json = base.GetArgIndex(1, args) as Feng.Json.FJson;
            string key = base.GetTextValue(2, args);
            json.Remove(key);
            return json;
        }

        public virtual object JsonParse(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            Feng.Json.FJsonBase json = Feng.Json.FJsonParse.Parese(text);
            return json;
        }

        public virtual object JsonNew(params object[] args)
        { 
            Feng.Json.FJson json = new FJson();
            return json;
        }


        public virtual object JsonSet(params object[] args)
        {
            object obj = base.GetArgIndex(1, args); 
            Feng.Json.FJson fJson = obj as Feng.Json.FJson;
            object value = base.GetTextValue(3, string.Empty, args);
            FJsonBase jsonvalue = value as FJsonBase;
            if (jsonvalue == null)
            {
                jsonvalue = new FJsonValue();
                jsonvalue.Value = value;
            }
            if (fJson != null)
            {
                string key = base.GetTextValue(2, string.Empty, args);
                FJsonItem item = fJson.GetItem(key);
                if (item==null)
                {
                    item = new FJsonItem();
                    item.Key = key;
                    item.Value = jsonvalue; 
                }
                else
                {
                    item.Value = jsonvalue;
                }
                return Feng.Utils.Constants.OK;
            }
            Feng.Json.FJsonItem fjsonitem = obj as Feng.Json.FJsonItem;
            if (fjsonitem != null)
            {
                fjsonitem.Value = jsonvalue;
                return Feng.Utils.Constants.OK;
            }

            Feng.Json.FJsonArray fjsonarray = obj as Feng.Json.FJsonArray;
            if (fjsonarray != null)
            {
                int index = base.GetIntValue(2, -1, args);
                if (index < fjsonarray.Jsones.Count)
                {
                    fjsonarray.Jsones[index] = jsonvalue;
                }
                else
                {
                    fjsonarray.Add(jsonvalue);
                }
                return Feng.Utils.Constants.OK;
            }
            return Feng.Utils.Constants.FALSE;
        }

        public virtual object JsonGet(params object[] args)
        {
            object obj = base.GetArgIndex(1, args);
            Feng.Collections.ListEx<object> list = base.GetArgIndex(2, args) as Feng.Collections.ListEx<object>;
            if (obj is Feng.Json.FJsonBase && list != null)
            {
                return JsonGet(obj as Feng.Json.FJsonBase, list);
            }
            
            Feng.Json.FJson fJson = obj as Feng.Json.FJson;
            if (fJson != null)
            {
                string key = base.GetTextValue(2, string.Empty, args);
                if (!string.IsNullOrWhiteSpace(key))
                {
                    object value = fJson.GetItem(key);
                    Feng.Json.FJsonItem json = value as Feng.Json.FJsonItem;
                    if (json == null)
                    {
                        return value;
                    }
                    return json.Value;
                }
                return fJson.Value;
            }
            Feng.Json.FJsonItem fjsonitem = obj as Feng.Json.FJsonItem;
            if (fjsonitem != null)
            {
                if (fjsonitem.Value != null)
                {
                    Feng.Json.FJsonBase json = fjsonitem.Value as Feng.Json.FJsonBase;
                    if (json != null)
                    {
                        return json.Value;
                    }
                }
                return null;
            }

            Feng.Json.FJsonValue fjsonvalue = obj as Feng.Json.FJsonValue;
            if (fjsonvalue != null)
            {
                if (fjsonvalue.Value != null)
                {
                    return fjsonvalue.Value;
                }
                return null;
            }

            Feng.Json.FJsonArray fjsonarray = obj as Feng.Json.FJsonArray;
            if (fjsonarray != null)
            {
                int index = base.GetIntValue(2, -1, args);
                if (index >= 0)
                {
                    if (index < fjsonarray.Count)
                    {
                        object value = fjsonarray[index];

                        Feng.Json.FJsonBase json = value as Feng.Json.FJsonBase;
                        if (json == null)
                        {
                            return value;
                        }
                        return json;
                    }
                }
                if (fjsonarray.Count > 0)
                {
                    FJsonBase jsonar = fjsonarray[0];
                    if (jsonar != null)
                    {
                        Feng.Json.FJsonBase json = jsonar.Value as Feng.Json.FJsonBase;
                        if (json != null)
                        {
                            return json.Value;
                        }
                    } 
                }
                return null;
            }
            return obj;
        }

        public object JsonGet(Feng.Json.FJsonBase obj, Feng.Collections.ListEx<object> list)
        {
            Feng.Json.FJsonBase json = obj;
            foreach (object item in list)
            {
                object ob = GetJson(json, item);
                json = ob as Feng.Json.FJsonBase;
                if (json == null)
                {
                    return ob;
                }
            }
            return json;
        }
        public object GetJson(Feng.Json.FJsonBase obj, object item)
        {
            Feng.Json.FJson fJson = obj as Feng.Json.FJson;
            if (fJson != null)
            {
                string key = Feng.Utils.ConvertHelper.ToString(item);
                if (!string.IsNullOrWhiteSpace(key))
                {
                    object value = fJson.GetItem(key);
                    Feng.Json.FJsonBase json = value as Feng.Json.FJsonBase;
                    if (json == null)
                    {
                        return value;
                    }
                    return json.Value;
                }
                return fJson.Value;
            }
 
            Feng.Json.FJsonValue fjsonvalue = obj as Feng.Json.FJsonValue;
            if (fjsonvalue != null)
            {
                if (fjsonvalue.Value != null)
                {
                    return fjsonvalue.Value;
                }
                return null;
            }

            Feng.Json.FJsonArray fjsonarray = obj as Feng.Json.FJsonArray;
            if (fjsonarray != null)
            {
                int index = Feng.Utils.ConvertHelper.ToInt32(item, -1);
                if (index >= 0)
                {
                    if (index < fjsonarray.Count)
                    {
                        object value = fjsonarray[index];

                        Feng.Json.FJsonBase json = value as Feng.Json.FJsonBase;
                        if (json == null)
                        {
                            return value;
                        }
                        return json;
                    }
                }
                if (fjsonarray.Count > 0)
                {
                    FJsonBase jsonar = fjsonarray[0];
                    if (jsonar != null)
                    {
                        Feng.Json.FJsonBase json = jsonar.Value as Feng.Json.FJsonBase;
                        if (json != null)
                        {
                            return json.Value;
                        }
                    }
                }
                return null;
            }
            return obj;
        }

        public virtual object JsonItem(params object[] args)
        {
            object obj = base.GetArgIndex(1, args);
            int index = base.GetIntValue(2, -1, args);
            if (index >= 0)
            {
                object value = JsonArrayIndex(args);
                return value;
            }
            string key = base.GetTextValue(2, string.Empty, args);
            if (!string.IsNullOrWhiteSpace(key))
            {
                object value = JsonItemKey(args);
                return value;
            }
            Feng.Json.FJson fJson = obj as Feng.Json.FJson;
            if (fJson != null)
            {
                return fJson;
            }
            Feng.Json.FJsonItem fjsonitem = obj as Feng.Json.FJsonItem;
            if (fjsonitem != null)
            {
                return fjsonitem.Value;
            }

            Feng.Json.FJsonArray fjsonarray = obj as Feng.Json.FJsonArray;
            if (fjsonarray != null)
            {
                FJsonBase json = fjsonarray[0];
                return json;
            }
            return obj;
        }

        public virtual object Json(params object[] args)
        {
            object obj = base.GetArgIndex(1, args);
            int index = base.GetIntValue(2, -1, args);
            if (index >=0)
            {
                return JsonArrayIndex(args);
            }
            string key = base.GetTextValue(2,string.Empty, args);
            if (!string.IsNullOrWhiteSpace(key))
            { 
                return JsonItemKey(args);
            }
            Feng.Json.FJson fJson = obj as Feng.Json.FJson;
            if (fJson != null)
            {
                return fJson.Value;
            }
            Feng.Json.FJsonItem fjsonitem = obj as Feng.Json.FJsonItem;
            if (fjsonitem != null)
            {
                return fjsonitem.Value;
            }

            Feng.Json.FJsonArray fjsonarray = obj as Feng.Json.FJsonArray;
            if (fjsonarray != null)
            {
                return fjsonarray[0].Value;
            }
            return obj;
        }
    }
}
