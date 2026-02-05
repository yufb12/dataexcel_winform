using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class WebServiceFunctionContainer : CBMethodContainer
    {

        public const string Function_Category = "WebServiceFunction";
        public const string Function_Description = "网页函数";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
 

        public WebServiceFunctionContainer()
        {

            BaseMethod model = new BaseMethod();
            model.Name = "WebGet";
            model.Description = "WebGet";
            model.Function = WebGet;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "WebPost";
            model.Description = "WebPost";
            model.Function = WebPost;
            MethodList.Add(model);
        }

        public virtual object WebGet(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            string data = Http(url, "get", string.Empty, string.Empty, string.Empty, null);
            return data;
        }
 
        public virtual object WebPost(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            string method = base.GetTextValue(2,"post", args);
            string contenttype = base.GetTextValue(3, "application/json", args);
            string json = base.GetTextValue(4, args);
            string encodingname = base.GetTextValue(5, args);
            Feng.Collections.DictionaryEx<string, string> headers = null;
            for (int i = 6; i < args.Length; i++)
            {
                if (headers == null)
                {
                    headers = new Collections.DictionaryEx<string, string>();
                }
                string key = base.GetTextValue(i, args);
                string value = base.GetTextValue(i+1, args);
                i++;
            }
            string data = Http(url, method, contenttype, json, encodingname, headers);
            return data;
        }

        public static string Http(string url, string method, string contenttype, string json, 
            string encodingname, Feng.Collections.DictionaryEx<string, string> headers)
        { 
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            Encoding encoding = Encoding.UTF8;
            if (!string.IsNullOrWhiteSpace(encodingname))
            {
                Encoding ed = Encoding.GetEncoding(encodingname);
                if (ed != null)
                {
                    encoding = ed;
                }
                else
                {
                    string ENCODINGNAME = encodingname.ToUpper(); ;
                    switch (ENCODINGNAME)
                    {
                        case "UTF8":
                            encoding = Encoding.UTF8;
                            break;
                        case "DEFAULT":
                            encoding = Encoding.Default;
                            break;
                        case "ASCII":
                            encoding = Encoding.ASCII;
                            break;
                        case "UNICODE":
                            encoding = Encoding.Unicode;
                            break;
                        case "UTF32":
                            encoding = Encoding.UTF32;
                            break;
                        case "UTF7":
                            encoding = Encoding.UTF7;
                            break;
                        default:
                            break;
                    }
                }
            }
            string responseData = String.Empty;
            req.Method = method;
            req.ContentType = contenttype;
            if (!string.IsNullOrWhiteSpace(json))
            { 
                byte[] data = Encoding.UTF8.GetBytes(json);
                req.ContentLength = data.Length;
                Stream postStream = req.GetRequestStream();
                postStream.Write(data, 0, data.Length);
                postStream.Close();
            }
            else
            {
                req.ContentLength = 0;
            }
            if (headers != null)
            {
                foreach (System.Collections.Generic.KeyValuePair<string, string> item in headers)
                {
                    req.Headers.Add(item.Key, item.Value);
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    responseData = reader.ReadToEnd();
                }
                return responseData;
            }
        }

        //public static string HttpGet(string url,string contenttype, string encodingname, Feng.Collections.DictionaryEx<string, string> headers)
        //{
        //    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
        //    Encoding encoding = Encoding.UTF8;
        //    Encoding ed = Encoding.GetEncoding(encodingname);
        //    if (ed != null)
        //    {
        //        encoding = ed;
        //    }
        //    string responseData = String.Empty;
        //    req.Method = "get";
        //    req.ContentType = "application/xml";
        //    req.ContentLength = 0;
        //    foreach (System.Collections.Generic.KeyValuePair<string, string> item in headers)
        //    {
        //        req.Headers.Add(item.Key, item.Value);
        //    }

        //    using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
        //    {
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
        //        {
        //            responseData = reader.ReadToEnd();
        //        }
        //        return responseData;
        //    }
        //}

        //public static string HttpPost(string url, string json, Feng.Collections.DictionaryEx<string, string> headers)
        //{
        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    byte[] data = Encoding.UTF8.GetBytes(json);
        //    request.AllowAutoRedirect = true;
        //    request.Method = "post";
        //    request.ContentType = "application/json";
        //    request.ContentLength = data.Length;
        //    Stream postStream = request.GetRequestStream();
        //    postStream.Write(data, 0, data.Length);
        //    postStream.Close();

        //    WebResponse webres = null;
        //    try
        //    {
        //        webres = request.GetResponse();
        //    }
        //    catch (WebException ex)
        //    {
        //        string msg = ex.Message;
        //        WebHeaderCollection wc = ex.Response.Headers;
        //        foreach (string key in wc.Keys)
        //        {
        //            string name = wc.Get(key);
        //        }
        //        return msg;
        //    }

        //    HttpWebResponse response = webres as HttpWebResponse;
        //    Stream instream = response.GetResponseStream();
        //    StreamReader sr = new StreamReader(instream, Encoding.UTF8);
        //    string content = sr.ReadToEnd();
        //    return content;
        //}
    }
}
