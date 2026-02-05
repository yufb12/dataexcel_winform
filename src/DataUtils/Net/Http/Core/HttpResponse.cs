using Feng.Excel.IO;
using Feng.Net.Extend;
using Feng.Net.Http.Base;
using Feng.Net.Tools;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Feng.Net.Http
{
    public class HttpResponse
    {
        private Net.Http.HttpRequest _request = null;
        public HttpResponse(Net.Http.HttpRequest request)
        {
            _request = request;
            HeaderBuilder = new StringBuilder();
        }
        public HttpTcpServerProxy HttpProxy { get; set; }
        public Net.Http.HttpRequest Request { get { return this._request; } }
        public IHttpResponseHeaderFilter HeaderFilter { get; set; }
        public bool hasResponse { get; set; }
        public StringBuilder HeaderBuilder { get; set; }
        public void HeaderBuilderReSet()
        {
            HeaderBuilder.Clear();
        }
        public void AppendLine(string txt)
        {
            if (HeaderFilter != null)
            {
                if (HeaderFilter.AppendLine(this,txt))
                {
                    return;
                }
            }
            HeaderBuilder.AppendLine(txt);
        }
        public string GetHeaderText()
        {
            return HeaderBuilder.ToString();
        }
#if DEBUG
        private List<HttpRequest> HttpRequestes = new List<HttpRequest>();
#endif

        private int responsetimes = 0;
        public int ResponseTimes { get { return responsetimes; } }

        private List<string> headeres = new List<string>();
        public virtual void AddHeader(string header)
        {
            headeres.Add(header);
        }

        public virtual void Send(byte[] data)
        {
            if (responsetimes > 1)
            {

            }
            responsetimes++;
            HttpProxy.Send(data);
        }

        public virtual void Send(string txt)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(txt);
            Send(data);
        }

        public virtual void DoHttpRequestJson(HttpRequest request, Feng.Json.FJsonBase json)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif
            string jsontxt = json.ToString();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsontxt);
            string txt = string.Empty;
            HeaderBuilderReSet();
            AppendLine("HTTP/1.1 200 OK");
            AppendLine("Content-Type: application/json; charset=utf-8");
            //sb.AppendLine("Content-Encoding: gzip");
            AppendLine("ETag: " + "\"" + ConvertHelper.GetTimeStamp() + "\"");
            AppendLine("Vary: Accept-Encoding");
            //sb.AppendLine("Server: Microsoft-IIS/10.0");
            AppendLine(HttpHeader_Type.Connection_Server_Value);
            AppendLine("Access-Control-Allow-Origin: *");
            //sb.AppendLine("X-Powered-By: ASP.NET");  
            //sb.AppendLine(HttpHeader_Type.Connection_X_Powered_By);
            AppendLine("Date: " + DateTime.UtcNow.ToString("r"));
            foreach (string header in headeres)
            {
                AppendLine(header);
            }
            AppendLine("Content-Length: " + data.Length + "\r\n"); 
            txt = GetHeaderText();
            this.hasResponse = true;
            Send(txt);
            Send(data);

        }

        public virtual void DoHttpRequestJson(HttpRequest request, string jsontxt)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif 
            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsontxt);
            string txt = string.Empty;
            HeaderBuilderReSet();
            AppendLine("HTTP/1.1 200 OK");
            AppendLine("Content-Type: application/json; charset=utf-8");
            //sb.AppendLine("Content-Encoding: gzip");
            AppendLine("ETag: " + "\"" + ConvertHelper.GetTimeStamp() + "\"");
            AppendLine("Vary: Accept-Encoding");
            //sb.AppendLine("Server: Microsoft-IIS/10.0");
            AppendLine(HttpHeader_Type.Connection_Server_Value);
            AppendLine("Access-Control-Allow-Origin: *");
            //sb.AppendLine("X-Powered-By: ASP.NET");  
            //sb.AppendLine(HttpHeader_Type.Connection_X_Powered_By);
            AppendLine("Date: " + DateTime.UtcNow.ToString("r"));
            foreach (string header in headeres)
            {
                AppendLine(header);
            }
            AppendLine("Content-Length: " + data.Length + "\r\n");
            txt = GetHeaderText();
            this.hasResponse = true;
            Send(txt);
            Send(data);

        }
        public virtual void DoHttpRequestTxt(HttpRequest request, string value)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            HeaderBuilderReSet();
            AppendLine("HTTP/1.1 200 OK");
            AppendLine("Content-Type: text/html; charset=utf-8");
            //sb.AppendLine("Content-Encoding: gzip");
            AppendLine("Vary: Accept-Encoding");
            AppendLine("ETag: " + "\"" + ConvertHelper.GetTimeStamp() + "\"");
            //sb.AppendLine("Server: Microsoft-IIS/10.0");
            AppendLine(HttpHeader_Type.Connection_Server_Value);
            AppendLine("Access-Control-Allow-Origin: *");
            //sb.AppendLine("X-Powered-By: ASP.NET");
            //sb.AppendLine(HttpHeader_Type.Connection_X_Powered_By);
            AppendLine("Date: "+ DateTime.UtcNow.ToString("r"));
            foreach (string header in headeres)
            {
                AppendLine(header);
            }
            AppendLine("Content-Length: " + data.Length + "\r\n"); 
            string txt = this.GetHeaderText();
            this.hasResponse = true;
            Send(txt);
            Send(data);
        }

        public virtual void DoHttpRequestCORS(HttpRequest request)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif
            var originHeader = request.Header.GetHeader("Origin");
            var origin = "*"; // 默认值为"*"，可以根据实际需求进行更改
            if (originHeader != null)
            {
                //origin = originHeader.Substring(8).Trim();
            }
            // 设置相关响应头
            StringBuilder responseHeaders = new StringBuilder();

            responseHeaders.AppendLine("Access-Control-Allow-Origin: " + origin);
            responseHeaders.AppendLine("Access-Control-Allow-Methods: POST, GET");
            responseHeaders.AppendLine("Access-Control-Allow-Headers: X-Requested-With, Content-Type");

            // 返回响应
            string response = "HTTP/1.1 200 OK\r\n" +
                              responseHeaders.ToString() +
                              "\r\n";
            this.hasResponse = true;
            this.Send(response);
        }

        public virtual void DoHttpRequest404(HttpRequest request, string info)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif
            string value = "<h5>Check that the link is valid<h5>" + info;
            string json = Get404Html(request.Header.FilePath, request.HttpServer.FSRM) + value;
            value = json;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            HeaderBuilderReSet();
            AppendLine("HTTP/1.1 404 Not Found");
            AppendLine("Content-Type: text/html; charset=utf-8"); 
            AppendLine("Accept-Ranges: bytes");
            AppendLine(HttpHeader_Type.Connection_Server_Value);
            //sb.AppendLine(HttpHeader_Type.Connection_X_Powered_By);
            AppendLine("Date: "+ DateTime.UtcNow.ToString("r"));
            AppendLine("Connection: " + HttpHeader_Type.Connection_keep_alive + "");
            AppendLine("Content-Length: " + data.Length + "\r\n");

            string txt = this.GetHeaderText();
            //string txt = "HTTP/1.1 404 Not Found\r\n" +
            //    "Content-Type: text/html; charset=utf-8\r\n" +
            //    "Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT\r\n" +
            //    "Accept-Ranges: bytes\r\n" +
            //    "ETag: \"323a3b1910dcd91: 0\"\r\n" +
            //    "Server: Microsoft-IIS/7.5\r\n" +
            //    "X-Powered-By: ASP.NET\r\n" +
            //    "Date: Thu, 31 Aug 2023 14:22:07 GMT\r\n" +
            //    "Connection: "+ HttpHeader_Type.Connection_keep_alive + "\r\n" +
            //    "Content-Length: " + data.Length + "\r\n\r\n";
            this.hasResponse = true;
            this.Send(txt);
            this.Send(data);
        }

        public virtual void DoHttpRequestFile(HttpRequest request)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif
            if (!System.IO.File.Exists(request.Header.FilePath))
            {
                this.DoHttpRequest404(request, "");
            }
            else
            {
                DoHttpRequestFile(request, request.Header.FilePath);
            }
        }

        public virtual void DoHttpRequestFile(HttpRequest request, string file)
        {

#if DEBUG
            HttpRequestes.Add(request);
#endif
            byte[] data = System.IO.File.ReadAllBytes(file);
            string contenttype = HttpRequest.GetContentType(file);
            string filename = Path.GetFileName(file);
            HeaderBuilderReSet();

            AppendLine("HTTP /1.1 200 OK");
            AppendLine("Content-Type: " + contenttype + "; charset=utf-8");
            AppendLine("Content-Disposition: " + "inline; filename=\"" + filename + "\"" + "");
            //AppendLine("Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT");
            AppendLine("ETag: " + "\"" + ConvertHelper.GetTimeStamp() + "\"");
            AppendLine("Accept-Ranges: bytes");
            AppendLine(HttpHeader_Type.Connection_Server_Value);
            //sb.AppendLine(HttpHeader_Type.Connection_X_Powered_By);
            AppendLine("Date: "+ DateTime.UtcNow.ToString("r"));
            AppendLine("Connection: " + HttpHeader_Type.Connection_keep_alive + "");
            AppendLine("Content-Length: " + data.Length + "\r\n"); 
            //string txt = "HTTP /1.1 200 OK\r\n" +
            //    "Content-Type: " + contenttype + "; charset=utf-8\r\n" +
            //    "Content-Disposition: " + "inline; filename=\"" + filename + "\"" + "\r\n" +
            //    "Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT\r\n" +
            //    "Accept-Ranges: bytes\r\n" +
            //    "ETag: \"323a3b1910dcd91: 0\"\r\n" +
            //    "Server: Microsoft-IIS/7.5\r\n" +
            //    "X-Powered-By: ASP.NET\r\n" +
            //    "Date: Thu, 31 Aug 2023 14:22:07 GMT\r\n" +
            //    "Connection: " + HttpHeader_Type.Connection_keep_alive + "\r\n" +
            //    "Content-Length: " + data.Length + "\r\n\r\n";

            string txt = this.GetHeaderText();
            this.hasResponse = true;
            this.Send(txt);
            this.Send(data);
        }

        public virtual void DoHttpRequestRedirectUrl(HttpRequest request, string url)
        {

#if DEBUG
            HttpRequestes.Add(request);
#endif
            string file = request.HttpServer.FSRM.GetPath(url);
            byte[] data = System.IO.File.ReadAllBytes(file);
            string contenttype = HttpRequest.GetContentType(file);
            string filename = Path.GetFileName(file);
            HeaderBuilderReSet();

            AppendLine("HTTP /1.1 200 OK");
            AppendLine("Content-Type: " + contenttype + "; charset=utf-8");
            AppendLine("Content-Disposition: " + "inline; filename=\"" + filename + "\"" + "");
            //AppendLine("Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT");
            AppendLine("ETag: " + "\"" + ConvertHelper.GetTimeStamp() + "\"");
            AppendLine("Accept-Ranges: bytes");
            AppendLine(HttpHeader_Type.Connection_Server_Value);
            //sb.AppendLine(HttpHeader_Type.Connection_X_Powered_By);
            AppendLine("Date: " + DateTime.UtcNow.ToString("r"));
            AppendLine("Connection: " + HttpHeader_Type.Connection_keep_alive + "");
            AppendLine("Content-Length: " + data.Length + "\r\n");

            //string txt = "HTTP /1.1 200 OK\r\n" +
            //    "Content-Type: " + contenttype + "; charset=utf-8\r\n" +
            //    "Content-Disposition: " + "inline; filename=\"" + filename + "\"" + "\r\n" +
            //    "Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT\r\n" +
            //    "Accept-Ranges: bytes\r\n" +
            //    "ETag: \"323a3b1910dcd91: 0\"\r\n" +
            //    "Server: Microsoft-IIS/7.5\r\n" +
            //    "X-Powered-By: ASP.NET\r\n" +
            //    "Date: Thu, 31 Aug 2023 14:22:07 GMT\r\n" +
            //    "Connection: " + HttpHeader_Type.Connection_keep_alive + "\r\n" +
            //    "Content-Length: " + data.Length + "\r\n\r\n";

            string txt = this.GetHeaderText();
            this.hasResponse = true;
            this.Send(txt);
            this.Send(data);
        }

        public virtual void DoHttpRequestDirectory(HttpRequest request)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif
            string value = "hello world";
            string json = GetDirctoryHtml(request.Header.FilePath, request.HttpServer.FSRM);
            value = json;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);

            HeaderBuilderReSet();

            AppendLine("HTTP/1.1 200 OK");
            AppendLine("Content-Type: text/html; charset=utf-8");
            AppendLine("Cache-Control: no-cache, no-store, must-revalidate");
            AppendLine("Pragma: no-cache");
            AppendLine("Expires: 0");
            AppendLine("ETag: " + "\"" + ConvertHelper.GetTimeStamp() + "\"");
            //AppendLine("Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT");
            AppendLine("Accept-Ranges: bytes"); 
            AppendLine(HttpHeader_Type.Connection_Server_Value);
            //sb.AppendLine(HttpHeader_Type.Connection_X_Powered_By);
            AppendLine("Date: "+ DateTime.UtcNow.ToString("r"));
            AppendLine("Connection: " + HttpHeader_Type.Connection_keep_alive + "");
            AppendLine("Content-Length: " + data.Length + "\r\n");


            //string txt = "HTTP/1.1 200 OK\r\n" +
            //    "Content-Type: text/html; charset=utf-8\r\n" +
            //    "Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT\r\n" +
            //    "Accept-Ranges: bytes\r\n" +
            //    "ETag: \"323a3b1910dcd91: 0\"\r\n" +
            //    "Server: Microsoft-IIS/7.5\r\n" +
            //    "X-Powered-By: ASP.NET\r\n" +
            //    "Date: Thu, 31 Aug 2023 14:22:07 GMT\r\n" +
            //    "Connection: " + HttpHeader_Type.Connection_keep_alive + "\r\n" +
            //    "Content-Length: " + data.Length + "\r\n\r\n";

            string txt = this.GetHeaderText();
            this.hasResponse = true;
            this.Send(txt);
            this.Send(data);
        }

        public virtual void DoHttpRequest502(HttpRequest request)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif
            string value = "";
            string json = Get500Html();
            value = json;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);


            HeaderBuilderReSet();

            AppendLine("HTTP/1.1 502 Bad Gateway");
            AppendLine("Content-Type: text/html; charset=utf-8");
            //AppendLine("Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT");
            AppendLine("Accept-Ranges: bytes");
            AppendLine(HttpHeader_Type.Connection_Server_Value);
            //sb.AppendLine(HttpHeader_Type.Connection_X_Powered_By);
            AppendLine("Date: "+ DateTime.UtcNow.ToString("r"));
            AppendLine("Connection: " + HttpHeader_Type.Connection_keep_alive + "");
            AppendLine("Content-Length: " + data.Length + "\r\n");

            //string txt = "HTTP/1.1 502 Bad Gateway\r\n" +
            //    "Content-Type: text/html; charset=utf-8\r\n" +
            //    "Last-Modified: Thu, 31 Aug 2023 13:36:08 GMT\r\n" +
            //    "Accept-Ranges: bytes\r\n" +
            //    "ETag: \"323a3b1910dcd91: 0\"\r\n" +
            //    "Server: Microsoft-IIS/7.5\r\n" +
            //    "X-Powered-By: ASP.NET\r\n" +
            //    "Date: Thu, 31 Aug 2023 14:22:07 GMT\r\n" +
            //    "Connection: " + HttpHeader_Type.Connection_keep_alive + "\r\n" +
            //    "Content-Length: " + data.Length + "\r\n\r\n";


            string txt = this.GetHeaderText();
            this.hasResponse = true;
            this.Send(txt);
            this.Send(data);
        }

        public virtual void DoHttpRequestRedirect301(HttpRequest request, string url)
        {
#if DEBUG
            HttpRequestes.Add(request);
#endif
            string txt = "HTTP/1.1 301 Moved Permanently\r\nLocation: " + url + "\r\n\r\n";
            this.hasResponse = true;
            this.Send(txt);
        }

        #region Other way

        public string GetDirctoryHtml(string path, FileServerResourceManager fsrm)
        {
            List<Excel.IO.PathInfo> list = FileTools.GetDirctoryPathInfos(path, fsrm);
            StringBuilder sb = new StringBuilder();
            string file = System.IO.Path.GetFileName(path);
            sb.Append(@"<html>
<head>
<title>" + file + @"</title>
</head>
<body>
<H1>" + file + @"</H1>
<hr>
<pre>
<A href=""../"">返回上级目录</A>
<br>
<br>");
            foreach (Excel.IO.PathInfo item in list)
            {
                string strpath = item.FullName.Replace("\\", "/");
                if (item.PathType == PathType.TYPE_DIRECTORY)
                {
                    sb.Append(string.Format("{0} &lt;dir &gt;<A HREF=\"{2}\">{3}</A>", item.CreationTime, item.Size, strpath, item.Name, item.LastWriteTime));
                    sb.Append(@"<br>");
                    continue;
                }
                sb.Append(string.Format("{0} {1}  <A HREF=\"{2}\">{3}</A>", item.CreationTime, item.Size, strpath, item.Name, item.LastWriteTime));
                sb.Append(@"<br>");
            }
            sb.Append(@"</pre>");
            sb.Append(@"<br>");
            sb.Append(@"</body>");
            sb.Append(@"</html>");

            return sb.ToString();
        }

        public string Get500Html()
        {
            StringBuilder sb = new StringBuilder();
            string file = "Error 500";
            sb.Append(@"<html>
<head>
<title>" + file + @"</title>
</head>
<body>
<H1>" + file + @"</H1> 
<hr> 
<br>");
            sb.Append(@"</body>");
            sb.Append(@"</html>");

            return sb.ToString();
        }

        private string Get404Html(string path, FileServerResourceManager fsrm)
        {
            StringBuilder sb = new StringBuilder();
            string file = "404 Not Found";
            sb.Append(@"<html>
<head>
<title>" + file + @"</title>
</head>
<body>
<H1>" + file + @"</H1>
<H2>" + fsrm.GetUrl(path) + @"</H2>
<hr> 
<br>");
            sb.Append(@"</body>");
            sb.Append(@"</html>");

            return sb.ToString();
        }
        #endregion
    }


    public class HttpResponseHeader
    {
        public HttpResponseHeader()
        {
            Dics = new Collections.DictionaryEx<string, string>();
            List = new Collections.ListEx<string>();
        }
        public string Method { get; set; }
        public string Url { get; set; }
        public string UrlDe { get; set; }
        public string FilePath { get; set; }
        public string HTTPVersion { get; set; }
        public string Host { get; set; }
        public int Content_Length { get; set; }
        public Feng.Collections.ListEx<string> List = null;
        public Feng.Collections.DictionaryEx<string, string> Dics = null;

        public string Content_Type
        {
            get
            {
                foreach (string line in List)
                {
                    if (line.StartsWith("Content-Type:", StringComparison.OrdinalIgnoreCase))
                    {
                        string value = line.Substring("Content-Type:".Length).Trim();
                        int index = value.IndexOf(";");
                        if (index > 0)
                        {
                            value = value.Substring(0, index);
                        }
                        return value;
                    }
                }
                return string.Empty;
            }
        }//"Content-Type: application/x-www-form-urlencoded"
    }

    public interface IHttpResponseHeaderFilter
    {
        bool AppendLine(HttpResponse httpResponse,string txt);
    }

    public class HttpResponseBody
    {
        public byte[] BodyData { get; set; }
        public int Index { get; set; }
    }
}