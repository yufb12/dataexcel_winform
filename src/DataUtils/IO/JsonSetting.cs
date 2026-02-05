using System;
using System.Collections.Generic;
using System.Reflection;

namespace Feng.IO
{
    public class JsonSetting<T>
    { 

        public JsonSetting()
        { 

        }


        private string _file = string.Empty;
        public virtual string File
        {
            get { return _file; }
            set
            {
                _file = value;
            }
        }
        public void Init(string file)
        {
            _file = file;
            if (System.IO.File.Exists(file))
            {
                string txt = System.IO.File.ReadAllText(File);
                Feng.Json.FJsonBase fJson = Feng.Json.FJsonParse.Parese(txt);
                object obj = this.Item;
                System.Reflection.PropertyInfo[] propertyInfoes = this.Item.GetType().GetProperties();
                foreach (PropertyInfo info in propertyInfoes)
                {
                    info.SetValue(obj, fJson[info.Name], null);
                }
            }
        }

        private T item;
        public T Item
        {
            get
            {
                if (item == null)
                { 
                    item = System.Activator.CreateInstance<T>();
                }
                return item;
            }
        }
    } 
}
