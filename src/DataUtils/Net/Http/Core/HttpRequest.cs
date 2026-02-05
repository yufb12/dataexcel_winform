using System;
using System.Collections.Generic;
using System.Web;

namespace Feng.Net.Http
{

    public class HttpRequest
    {
        public HttpRequest()
        {

        }
        public HttpTcpServer HttpServer { get; set; }
        public System.Net.IPEndPoint RemoteEndPoint { get; set; }
        public HttpRequestHeader Header { get; set; }
        public HttpRequestBody Body { get; set; }
#if DEBUG
        public string DebugText { get; set; }
#endif
        public static string GetContentType(string filepath)
        {
            string extension = System.IO.Path.GetExtension(filepath).ToLower();
            switch (extension)
            {
                case ".txt":
                    return "text/plain";
                case ".html":
                    return "text/html";
                case ".css":
                    return "text/css";
                case ".js":
                    return "application/javascript";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                default:
                    return "application/octet-stream";
            }
        }

        private void GetUrl(HttpRequestHeader model, string txt)
        {
            //"GET /encrypt/htpasswd/%E4%B8%8D%E6%98%AF%E5%AF%B9.html?aaa=bbb HTTP/1.1\r\n"
            string[] txtes = Feng.Utils.TextHelper.Split(txt, " ");
            model.Method = txtes[0];
            model.Url = txtes[1];
            model.HTTPVersion = txtes[2].TrimEnd();
        }

        public virtual void Prepare(HttpRequest request, HttpResponse httpResponse)
        {
            try
            {
                if (request.Body != null)
                {
                    string txt = System.Text.Encoding.UTF8.GetString(request.Body.BodyData);
                }
                GetUrl(request.Header, request.Header.List[0]);
                string url = request.Header.Url;
                string[] txtes = Feng.Utils.TextHelper.Split(url, "?");
                string url1 = txtes[0];
                string urlarg = string.Empty;
                if (txtes.Length > 1)
                {
                    urlarg = txtes[1];
                }
                url1 = HttpUtility.UrlDecode(url1);
                request.Header.UrlDe = url1;
                request.Header.Args = urlarg;
                string fullpath = this.HttpServer.FSRM.GetPath(url1);
                request.Header.FilePath = fullpath;
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Http", "DataProjectHttpServerExtend", "DoHttpRequest", ex);
            }
        }
    }

    public class HttpRequestHeader
    {
        private Collections.DictionaryEx<string, string> _dics = null;
        public HttpRequestHeader()
        {
            _dics = new Collections.DictionaryEx<string, string>();
            List = new Collections.ListEx<string>();
        }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Args { get; set; }
        public string UrlDe { get; set; }
        public string Referer
        {
            get
            {
                foreach (string line in List)
                {
                    if (line.StartsWith("Referer:", StringComparison.OrdinalIgnoreCase))
                    {
                        string value = line.Substring("Referer:".Length).Trim();
                        int index = value.IndexOf(";");
                        if (index > 0)
                        {
                            value = value.Substring(0, index);
                        }
                        value = HttpUtility.UrlDecode(value);
                        return value;
                    }
                }
                return string.Empty;
            }
        } 
        public string FilePath { get; set; }
        public string HTTPVersion { get; set; }
        public string Host { get; set; }
        public int Content_Length { get; set; }
        public Feng.Collections.ListEx<string> List = null;
        public Feng.Collections.DictionaryEx<string, string> Dics { get { return _dics; } }
        public string GetHeader(string header)
        {
            foreach (string line in List)
            {
                if (line.StartsWith(header, StringComparison.OrdinalIgnoreCase))
                {
                    string value = line.Substring(header.Length + 1).Trim();
                    //int index = value.IndexOf(";");
                    //if (index > 0)
                    //{
                    //    value = value.Substring(0, index);
                    //}
                    Dics[header] = value;
                    return value;
                }
            }
            return string.Empty;
        }
        public string Origin
        {
            get
            {
                foreach (string line in List)
                {
                    if (line.StartsWith("Origin:", StringComparison.OrdinalIgnoreCase))
                    {
                        string value = line.Substring("Origin:".Length).Trim();
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
        }
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
        }//"Content-Type: application/x-www-form-urlencoded;boundary=xxxx"
        public string Content_Type_Boundary
        {
            get
            {
                foreach (string line in List)
                {
                    if (line.StartsWith("Content-Type:", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] values = line.Split(';');
                        foreach (string item in values)
                        {
                            string value = item.Trim();
                            if (value.StartsWith("boundary=", StringComparison.OrdinalIgnoreCase))
                            {
                                int index = item.IndexOf(";");
                                if (index > 0)
                                {
                                    value = value.Substring("boundary=".Length, index);
                                    return value;
                                }
                                else
                                {
                                    value = value.Substring("boundary=".Length);
                                    return value;
                                }
                            }
                        }
                    }
                }
                return string.Empty;
            }
        }//"Content-Type: application/x-www-form-urlencoded"
        private string cookie = string.Empty;
        public string Cookie
        {
            get
            {
                if (string.IsNullOrEmpty(cookie))
                {
                    cookie = GetHeader(HttpConst.HEADER_COOKIE);
                }
                return cookie;
            }
        }

        public Feng.Collections.DictionaryEx<string, string> GetArgs()
        {
            Feng.Collections.DictionaryEx<string, string> dic = new Feng.Collections.DictionaryEx<string, string>();
            string[] txts = this.Args.Split('&');
            foreach (string item in txts)
            {
                string[] ts = item.Split('=');
                if (ts.Length == 2)
                {
                    if (!dic.ContainsKey(ts[0]))
                    {
                        dic.Add(ts[0], ts[1]);
                    }
                }
            }
            return dic;
        }
    }
    public class HttpConst
    {
        public const string METHOD_GET = "GET";
        public const string METHOD_POST = "POST";
        public const string METHOD_OPTIONS = "OPTIONS";
        public const string HEADER_COOKIE = "Cookie";
    }
    public class Boundary
    {
        public Boundary()
        {
            Header = new List<string>();
        }
        public string HeaderText { get; set; }
        public string BoundaryText { get; set; }
        public List<string> Header { get; set; }
        public byte[] Data { get; set; }
        public bool LastOne { get; set; }
        public string Content_Disposition {
            get
            {
                foreach (string line in Header)
                {
                    if (line.StartsWith("Content-Disposition:", StringComparison.OrdinalIgnoreCase))
                    {
                        string value = line.Substring("Content-Disposition:".Length).Trim();
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
        }
        public string Content_Disposition_Name
        {
            get
            {
                foreach (string line in Header)
                {
                    if (line.StartsWith("Content-Disposition:", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] values = line.Split(';');
                        foreach (string item1 in values)
                        {
                            string item = item1.Trim();
                            if (item.StartsWith("name="))
                            {
                                string value = item.Substring("name=".Length);
                                value = value.Trim('"'); 
                                return value;
                            }
                        }
                    }
                }
                return string.Empty;
            }
        }
        public string Content_Disposition_Filename
        {
            get
            {
                foreach (string line in Header)
                {
                    if (line.StartsWith("Content-Disposition:", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] values = line.Split(';');
                        foreach (string item1 in values)
                        {
                            string item = item1.Trim();
                            if (item.StartsWith("filename="))
                            {
                                string value = item.Substring("filename=".Length);
                                value = value.Trim('"');
                                return value;
                            }
                        }
                    }
                }
                return string.Empty;
            }
        }
        public string Content_Type
        {
            get
            {
                foreach (string line in Header)
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
        }
    }
    public class HttpRequestBody
    {
        public HttpRequestBody(HttpRequest httprequest)
        {
            HttpRequest = httprequest;
        }
        public HttpRequest HttpRequest { get; set; }
        public byte[] BodyData { get; set; }
        public int Index { get; set; }
        public Feng.Collections.DictionaryEx<string, string> GetItems()
        {
            Feng.Collections.DictionaryEx<string, string> dics = new Collections.DictionaryEx<string, string>();
            string text = System.Text.Encoding.UTF8.GetString(BodyData);
            string[] items = Feng.Utils.TextHelper.Split(text, "&");
            foreach (string item in items)
            {
                string[] value = Feng.Utils.TextHelper.Split(item, "=");
                if (value.Length == 2)
                {
                    dics.Add(value[0], value[1]);
                }

            }
            return dics;
        }

        public Feng.Json.FJsonBase GetJson()
        {
            string text = System.Text.Encoding.UTF8.GetString(BodyData);
            Feng.Json.FJsonBase json = Feng.Json.FJsonParse.Parese(text);
            return json;
        }

        public List<Boundary> GetBoundaries()
        {
            return Parsemultipart_form_data(this.BodyData, this.HttpRequest.Header.Content_Type_Boundary);
        }

        public List<Boundary> Parsemultipart_form_data(byte[] data, string header)
        {
            List<Boundary> boundaries = new List<Boundary>();
            int index = 0;
            Boundary boundary = new Boundary();
            bool isfinished = false;
            int tindex = Parsemultipart_form_data(data, index, header, boundary, out isfinished);
            boundaries.Add(boundary);
            index = tindex;
            for (int i = 0; i < 256 && (!isfinished); i++)
            {
                boundary = new Boundary();
                isfinished = false;
                tindex = Parsemultipart_form_data(data, index, header, boundary, out isfinished);
                index = tindex;
                boundaries.Add(boundary);
            }
            return boundaries;
        }
        public int Parsemultipart_form_data(byte[] data, int startindex, string header, Boundary boundary, out bool isfinished)
        {
            isfinished = false;
            int index = startindex;
            string headertext = "--" + header;
            string txt2 = System.Text.Encoding.UTF8.GetString(data, startindex, data.Length - startindex);
            for (int i = 0; i < headertext.Length; i++)
            {
                if (data[startindex + i] != headertext[i])
                {
                    throw new Exception("");
                }
            }
            index = startindex + headertext.Length;
            if (data[index + 0] == '-')
            {
                if (data[index + 1] == '-')
                {
                    isfinished = true;
                    boundary.LastOne = true;
                    return index + 2;
                }
            }
            if (data[index + 0] == '\r')
            {
                if (data[index + 1] == '\n')
                {
                    index = index + 2;
                }
            }
            boundary.HeaderText = header;
            bool finished = false;
            for (int i = 0; i < 16 && (!finished); i++)
            {
                int tindex = 0;
                string txt = ReadLine(data, index, out tindex, out finished);
                boundary.Header.Add(txt);
                index = tindex;
            }
            for (int i = 0; i < data.Length - index; i++)
            {
                bool res = IsNewBoundary(data, index+i, headertext);
                if (res)
                {
                    boundary.Data = new byte[i - 2];
                    System.Buffer.BlockCopy(data, index, boundary.Data, 0, i - 2);
                    boundary.BoundaryText = System.Text.Encoding.UTF8.GetString(boundary.Data);
                    index = index + i - 2;
                    break;
                }
            }
            if (data[index + 0] == '\r')
            {
                if (data[index + 1] == '\n')
                {
                    index = index + 2;
                }
            }
            return index;
        }
        public bool IsNewBoundary(byte[] data, int startindex, string headertext)
        {
            for (int i = 0; i < headertext.Length; i++)
            {
                if (data[startindex + i] != headertext[i])
                {
                    return false;
                }
            }
            return true;
        }
        public string ReadLine(byte[] data, int startindex, out int index, out bool isfinished)
        {
            isfinished = false;
            index = startindex;
            for (int i = startindex; i < startindex + 512; i++)
            {
                if (data[i] == '\r')
                {
                    if (data[i + 1] == '\n')
                    {
                        string text = System.Text.Encoding.UTF8.GetString(data, startindex, i - startindex);
                        index = i + 2;
                        if (data[i + 2] == '\r')
                        {
                            if (data[i + 3] == '\n')
                            {
                                index = i + 4;
                                isfinished = true;
                            }
                        }
                        return text;
                    }
                }
            }
            throw new Exception("");
        }

    }
}