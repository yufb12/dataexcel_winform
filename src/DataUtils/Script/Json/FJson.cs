using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Json
{

    public abstract class FJsonBase
    {
        public abstract FJsonBase this[int index]
        {
            get;
        }
        public abstract FJsonBase this[string key]
        {
            get;
            set;
        }
        public FJsonBase Parent { get; set; }
        public abstract object Value { get; set; }
        public virtual object BaseValue { get; }
        public virtual bool HasItem(string key)
        {
            return false;
        }

        public virtual bool AddItem(string key,object value)
        {
            return false;
        }

        public static FJsonBase FromFile(string file)
        {
            if (!System.IO.File.Exists(file))
                return new Feng.Json.FJson();
            string txt = Feng.IO.FileHelper.ReadAllText(file);
            FJsonBase fJson = Feng.Json.FJsonParse.Parese(txt);
            return fJson;
        }
        public static void WriteFile(string file, FJsonBase json)
        {
            string txt = json.ToString();
            Feng.IO.FileHelper.WriteAllText(file, txt);
        }

        public static string ToJsonValue(object obj)
        {
            if (obj is bool)
            {
                return obj.ToString();
            }
            if (Feng.Utils.ConvertHelper.IsNumber(obj))
            {
                return obj.ToString();
            }
            if (obj is null)
            {
                return "null";
            }
            string s = obj.ToString().Replace("\\","\\\\");
            string ss = s.Replace("\"", "\\\"");
            string txtt = "\"" + ss + "\""; 
            string strvalue = txtt.Replace("\n", "\\n"); 
            strvalue = strvalue.Replace("\r", "");
            return strvalue;
        }
    }

    public class FJsonValue : FJsonBase
    {
        public FJsonValue()
        {

        }
        public FJsonValue(object value)
        {
            this._value = value;
        }
        public override FJsonBase this[int index]
        {
            get
            {
                return this;
            }
        }
        public override FJsonBase this[string key]
        {
            get
            {
                return this;
            }
            set { }
        }
        private object _value = null;
        public override object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public override object BaseValue { get { return _value; } }
        public override string ToString()
        {
            return ToJsonValue(Value);
        }

    }

    public class FJson : FJsonBase
    {
        public FJson()
        {
            Items = new List<FJsonItem>();
        }
        public List<FJsonItem> Items { get; set; }

        public override FJsonBase this[int index]
        {
            get
            {
                if (index < Items.Count)
                {
                    return Items[index].Value;
                }
                return null;
            }
        }
        public override FJsonBase this[string key]
        {
            get
            {
                foreach (FJsonItem item in Items)
                {
                    if (item.Key.ToUpper() == key.ToUpper())
                    {
                        return item.Value;
                    }
                } 
                return null;
            
            }
            set {
                FJsonItem item = GetItem(key);
                if (item == null)
                {
                    item = new FJsonItem() { Key = key, Value = value };
                    Add(item);
                }
                else
                {
                    item.Value = value;
                }
            }
        }

        public virtual FJsonItem GetItem(string key)
        {
            foreach (FJsonItem item in Items)
            {
                if (item.Key.ToUpper() == key.ToUpper())
                {
                    return item;
                }
            }
            return null;
        }
        public string GetString(string key)
        {
            foreach (FJsonItem item in Items)
            {
                if (item.Key.ToUpper() == key.ToUpper())
                {
                    Json.FJsonBase fJsonBase = item.Value as Json.FJsonBase;
                    if (fJsonBase != null)
                    {
                        return Feng.Utils.ConvertHelper.ToString(fJsonBase.BaseValue);
                    }
                }
            }
            return string.Empty;
        }
        public int GetInt(string key)
        {
            foreach (FJsonItem item in Items)
            {
                if (item.Key.ToUpper() == key.ToUpper())
                {
                    return Feng.Utils.ConvertHelper.ToInt32(item.Value);
                }
            }
            return 0;
        }
        public bool GetBool(string key)
        {
            foreach (FJsonItem item in Items)
            {
                if (item.Key.ToUpper() == key.ToUpper())
                {
                    return Feng.Utils.ConvertHelper.ToBoolean(item.Value);
                }
            }
            return false;
        }
        public decimal GetNumber(string key)
        {
            foreach (FJsonItem item in Items)
            {
                if (item.Key.ToUpper() == key.ToUpper())
                {
                    return Feng.Utils.ConvertHelper.ToDecimal(item.Value);
                }
            }
            return 0;
        }
        public object GetKey(string key)
        {
            foreach (FJsonItem item in Items)
            {
                if (item.Key.ToUpper() == key.ToUpper())
                {
                    return item.Value;
                }
            }
            return null;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (FJsonItem item in Items)
            { 
                string text = string.Format("{0},", item.ToString());
                sb.Append(text);
            }
            if (sb.Length > 1)
            {
                sb.Length = sb.Length - 1;
            }
            sb.Append("}");
            return sb.ToString();
        }

        public virtual void Add(FJsonItem obj)
        {
            Items.Add(obj);
        }
        public virtual FJsonItem Add(string key, object value)
        {
            FJsonItem item = new FJsonItem()
            {
                Key = key 
            };
            FJsonValue jsonValue = new FJsonValue() { Value = value, Parent = this };
            item.Value = jsonValue;
            Add(item);
            return item;
        }

        public virtual void Remove(string key)
        {
            foreach (FJsonItem item in Items)
            {
                if (item.Key == key)
                {
                    Items.Remove(item);
                    break;
                }
            }
        }

        public virtual void Add(string key, FJsonBase value)
        {
            FJsonItem item = new FJsonItem()
            {
                Key = key 
            };
            item.Value = value;
            Add(item);
        }

        public override object Value { get { return Items; } set { } }

        public override bool HasItem(string key)
        {
            foreach (FJsonItem item in Items)
            {
                if (item.Key.ToUpper() == key.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }
        public override bool AddItem(string key, object value)
        {
            Add(key, value);
            return true;
        }
    }

    public class FJsonItem 
    {
        public FJsonItem()
        {

        }
        public string Key { get; set; }
        public override string ToString()
        {

            string value = Value.ToString();
            return string.Format("\"{0}\":{1}", Key, value);
        }

        private FJsonBase _value = null;
        public FJsonBase Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

    }

    public class FJsonArray : FJsonBase
    {
        public FJsonArray()
        {
            Jsones = new List<FJsonBase>();
        }
        public List<FJsonBase> Jsones { get; set; }
        public override FJsonBase this[int index]
        {
            get
            {
                if (index < Jsones.Count)
                {
                    return Jsones[index];
                }
                return null;
            }
        }
        public override FJsonBase this[string key]
        {
            get
            {
                return null;
            }
            set { }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (FJsonBase item in Jsones)
            {
                string value = item.ToString();
                string text = string.Format("{0},", value);
                sb.Append(text);
            }
            if (sb.Length > 1)
            {
                sb.Length = sb.Length - 1;
            }
            sb.Append("]");
            return sb.ToString();
        }
        public virtual int Count
        {
            get
            {
                return Jsones.Count;
            }
        }
        public virtual void Add(FJsonBase obj)
        {
            Jsones.Add(obj);
        }
        public virtual void Add(object value)
        {
            FJsonValue jsonValue = new FJsonValue() { Value = value, Parent = this };
            Add(jsonValue);
        }
        public virtual void Add(string key, object value)
        {
            Feng.Json.FJson jsonValue = new FJson() { Parent = this };
            jsonValue.Add(key, value);
            Add(jsonValue);
        }
        public virtual void Remmove(int index)
        {
            Jsones.RemoveAt(index);
        }
        public virtual void Remmove(FJsonBase obj)
        {
            Jsones.Remove(obj);
        }

        public bool Contains(object value)
        { 
            foreach (var item in Jsones)
            {
                if (item.Value == value)
                {
                    return true;
                }
                if (item.Value.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public override object Value { get { return Jsones; } set { } }
    }
}
