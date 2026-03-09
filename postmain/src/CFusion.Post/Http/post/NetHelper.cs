using System.IO;
using System.Net;
using System.Text;

namespace CFusion.Http.post
{
    public class HttpHelper
    {

        public static string HttpPost(string url, string method, string token, string json)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            byte[] data = Encoding.UTF8.GetBytes(json);
            request.Headers.Add("token", token);
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);
            postStream.Close();

            WebResponse webres = null;
            try
            {
                webres = request.GetResponse();
            }
            catch (WebException ex)
            {
                string msg = ex.Message;
                return msg;
            }

            HttpWebResponse response = webres as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            string content = sr.ReadToEnd();
            return content;
        }

        public static string HttpPost(string url, string token, string json)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            byte[] data = Encoding.UTF8.GetBytes(json);
            request.Headers.Add("token", token);
            request.AllowAutoRedirect = true;
            request.Method = "post";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);
            postStream.Close();

            WebResponse webres = null;
            try
            {
                webres = request.GetResponse();
            }
            catch (WebException ex)
            {
                string msg = ex.Message;
                return msg;
            }

            HttpWebResponse response = webres as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            string content = sr.ReadToEnd();
            return content;
        }

        public static string HttpGet(string url, string token)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers.Add("X-Access-Token", token);
            request.AllowAutoRedirect = true;
            request.Method = "get";

            WebResponse webres = null;
            try
            {
                webres = request.GetResponse();
            }
            catch (WebException ex)
            {
                string msg = ex.Message;
                return msg;
            }

            HttpWebResponse response = webres as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            string content = sr.ReadToEnd();
            return content;
        }

        public static string HttpPUT(string url, string token, string json)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            byte[] data = Encoding.UTF8.GetBytes(json);
            request.Headers.Add("X-Access-Token", token);
            request.AllowAutoRedirect = true;
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);
            postStream.Close();

            WebResponse webres = null;
            try
            {
                webres = request.GetResponse();
            }
            catch (WebException ex)
            {
                string msg = ex.Message;
                WebHeaderCollection wc = ex.Response.Headers;
                foreach (string key in wc.Keys)
                {
                    string name = wc.Get(key);
                }
                return msg;
            }

            HttpWebResponse response = webres as HttpWebResponse; 
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8); 
            string content = sr.ReadToEnd();
            return content;
        }

        public static string HttpPost(string url, string json)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            byte[] data = Encoding.UTF8.GetBytes(json);
            request.AllowAutoRedirect = true;
            request.Method = "post";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);
            postStream.Close();

            WebResponse webres = null;
            try
            {
                webres = request.GetResponse();
            }
            catch (WebException ex)
            {
                string msg = ex.Message;
                return msg;
            }

            HttpWebResponse response = webres as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            string content = sr.ReadToEnd();
            return content;
        }
    }
}
