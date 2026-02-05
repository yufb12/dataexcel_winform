using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Feng.Net.Http
{
    public class WebRequestTool
    {
        public static string QueryPostWebServiceText(string URL, string ContentType)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = ContentType;
            Hashtable Pars = new Hashtable();
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            return ReadTextResponse(request.GetResponse());
        }
        public static string QueryPostWebServiceText(string URL,string ContentType, string Authorization, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = ContentType; 
            request.Headers.Add("Authorization", Authorization); 
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            return ReadTextResponse(request.GetResponse());
        }
 
 
        public static string WebGet(string URL)
        { 
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.SystemDefault | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "GET"; 
            SetWebRequest(request);
            return ReadTextResponse(request.GetResponse());
        }

        public static string WebGet(string URL,string ContentType)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = ContentType;
            SetWebRequest(request);
            return ReadTextResponse(request.GetResponse());
        }
        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
        }

        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
        }
 
        private static byte[] EncodePars(Hashtable Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }

        private static String ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }
 
        private static string ReadTextResponse(WebResponse response)
        {
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            sr.Close();
            return retXml;
        }
  
        public static string Post(string url, System.Collections.Generic.Dictionary<string, string> args)
        {
            Uri address = new Uri(url);
            HttpWebRequest request1 = HttpWebRequest.Create(address) as HttpWebRequest;
            request1.Method = "POST";
            request1.ContentType = "application/soap+xml; charset=utf-8";


            StringBuilder data = new StringBuilder();
            //调用HttpUtility需要在.net 4.0框架下，并且添加System.web.dll引用，请自行谷歌
            foreach (var item in args)
            {
                data.Append("&" + item.Key + "=" + System.Web.HttpUtility.UrlEncode(item.Value));
            }
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
            request1.ContentLength = byteData.Length;
            using (Stream postStream = request1.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }
            string value = string.Empty;
            using (HttpWebResponse response1 = request1.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response1.GetResponseStream());
                value = reader.ReadToEnd();
            }
            return value;

        }

    }
}