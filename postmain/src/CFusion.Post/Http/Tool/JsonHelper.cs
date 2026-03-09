using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web; 
using System.IO;
using Newtonsoft.Json.Converters;

namespace  Tools
{ 

    public static class JsonHelper
    { 
        public static string SerializeObject(object o)
        {
            IsoDateTimeConverter convert = new IsoDateTimeConverter();
            convert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";  
            string json = JsonConvert.SerializeObject(o, Formatting.None, convert);
            return json;
        }
 
        public static T DeserializeObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        } 
 
    }
}