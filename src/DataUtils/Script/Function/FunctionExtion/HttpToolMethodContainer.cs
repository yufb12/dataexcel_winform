using Feng.Collections;
using Feng.Net.Http;
using Feng.Script.Method;

namespace Feng.Script.FunctionContainer
{

    public class HttpToolMethodContainer : CBMethodContainer
    {
        public string FullName { get; set; }
        public const string Function_Category = "HttpTool";
        public const string Function_Description = "HttpTool";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public HttpToolMethodContainer()
        {

            BaseMethod model = null;


            model = new BaseMethod();
            model.Name = "HttpDebugMode";
            model.Description = @"HttpDebugMode()";
            model.Eg = @"HttpDebugMode(true)";
            model.Function = this.HttpDebugMode;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "HttpGet";
            model.Description = @"HttpGet()";
            model.Eg = @"HttpGet(url,headers)";
            model.Function = this.HttpGet;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "HttpPostFile";
            model.Description = @"HttpPostFile()";
            model.Eg = @"HttpPostFile(url,headers)";
            model.Function = this.HttpPostFile;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "HttpPostFormData";
            model.Description = @"HttpPostFormData()";
            model.Eg = @"HttpPostFormData(url,headers)";
            model.Function = this.HttpPostFormData;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "HttpPostRaw";
            model.Description = @"HttpPostRaw()";
            model.Eg = @"HttpPostRaw(url,headers)";
            model.Function = this.HttpPostRaw;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "HttpPut";
            model.Description = @"HttpPut()";
            model.Eg = @"HttpPut(url,headers)";
            model.Function = this.HttpPut;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "HttpDelete";
            model.Description = @"HttpDelete()";
            model.Eg = @"HttpDelete(url,headers)";
            model.Function = this.HttpDelete;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "HttpHead";
            model.Description = @"HttpHead()";
            model.Eg = @"HttpHead(url,headers)";
            model.Function = this.HttpHead;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "HttpOptions";
            model.Description = @"HttpOptions()";
            model.Eg = @"HttpOptions(url,headers)";
            model.Function = this.HttpOptions;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "HttpTrace";
            model.Description = @"HttpTrace()";
            model.Eg = @"HttpTrace(url,headers)";
            model.Function = this.HttpTrace;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "HttpPatch";
            model.Description = @"HttpPatch()";
            model.Eg = @"HttpPatch(url,headers)";
            model.Function = this.HttpPatch;
            MethodList.Add(model);
        }

        public bool DebugMode { get; set; }
        public virtual object HttpDebugMode(params object[] args)
        {
            bool debugmode = base.GetBooleanValue(1, args);
            DebugMode = debugmode;
            return DebugMode;
        }

        public virtual object HttpGet(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string response = postMainHttp.Get(url, headers);
            return response;
        }
        public virtual object HttpPostFile(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string filePath = base.GetTextValue(3, args);
            string fileName = base.GetTextValue(4, args);
            string response = postMainHttp.PostFile(url, filePath, fileName, headers);
            return response;
        }
        private Feng.Collections.DictionaryEx<string, string> GetData(object value)
        {
            Feng.Collections.DictionaryEx<string, string> dics = new DictionaryEx<string, string>();
            if (value != null)
            {
                Feng.Collections.DictionaryEx<object, object> tempdata = value as Feng.Collections.DictionaryEx<object, object>;
                if (tempdata != null)
                {
                    foreach (var item in tempdata)
                    {
                        dics.Add(Feng.Utils.ConvertHelper.ToString(item.Key), Feng.Utils.ConvertHelper.ToString(item.Value));
                    }
                }
            }
            return dics;
        }
        public virtual object HttpPostFormData(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            Feng.Collections.DictionaryEx<string, string> formData = GetData(base.GetArgIndex(3, args));
            string response = postMainHttp.PostFormData(url, formData, headers);
            return response;
        }
        public virtual object HttpPostRaw(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string rawData = base.GetTextValue(3, args);
            string contentType = base.GetTextValue(4, args);
            string response = postMainHttp.PostRaw(url, rawData, contentType, headers);
            return response;
        }
        public virtual object HttpPut(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string rawData = base.GetTextValue(3, args);
            string contentType = base.GetTextValue(4, args);
            string response = postMainHttp.Put(url, rawData, contentType, headers);
            return response;
        }
        public virtual object HttpDelete(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string rawData = base.GetTextValue(3, args);
            string contentType = base.GetTextValue(4, args);
            string response = postMainHttp.Delete(url, rawData, contentType, headers);
            return response;
        }
        public virtual object HttpHead(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string response = postMainHttp.Head(url, headers);
            return response;
        }
        public virtual object HttpOptions(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string response = postMainHttp.Options(url, headers);
            return response;
        }
        public virtual object HttpTrace(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string response = postMainHttp.Trace(url, headers);
            return response;
        }
        public virtual object HttpPatch(params object[] args)
        {
            string url = base.GetTextValue(1, args);
            Feng.Collections.DictionaryEx<string, string> headers = GetData(base.GetArgIndex(2, args));
            PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
            postMainHttp.ShowDebugInfo = DebugMode;
            string rawData = base.GetTextValue(3, args);
            string contentType = base.GetTextValue(4, args);
            string response = postMainHttp.Patch(url, rawData, contentType, headers);
            return response;
        }
    }
}
