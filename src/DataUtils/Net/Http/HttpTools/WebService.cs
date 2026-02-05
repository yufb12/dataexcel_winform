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
    public class WebService
    {
        public static string QueryPostWebServiceData(String URL, String MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //Basic YWRtaW46MTIzNDU2

            //request.Headers.Add("Authorization", "Basic YWRtaW46MTIzNDU2");
            //request.Headers.Add("Content-Type", "application/json;charset=UTF-8");
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            string retXml = ReadTextResponse(request.GetResponse());
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXml);
            return doc.Value; 
        }

        public static XmlDocument QueryPostWebService(String URL, String MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //Basic YWRtaW46MTIzNDU2

            request.Headers.Add("Authorization", "Basic YWRtaW46MTIzNDU2");
            //request.Headers.Add("Content-Type", "application/json;charset=UTF-8");
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            return ReadXmlResponse(request.GetResponse());
        }
        public static string QueryPostWebServiceText(String URL, String MethodName, string Authorization, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //Basic YWRtaW46MTIzNDU2


            request.Headers.Add("Authorization", Authorization);
            //request.Headers.Add("Content-Type", "application/json;charset=UTF-8");
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            return ReadTextResponse(request.GetResponse());
        }
        public static string QueryPostWebServiceText(String URL, String MethodName, string contenttype, string Authorization, string text)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = contenttype;
            //Basic YWRtaW46MTIzNDU2

            request.Headers.Add("Authorization", Authorization);
            //request.Headers.Add("Content-Type", "application/json;charset=UTF-8");
            SetWebRequest(request);
            byte[] data = EncodePars(text);
            WriteRequestData(request, data);
            return ReadTextResponse(request.GetResponse());
        }


        public static string QueryGetWebService(String URL, String MethodName, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName + "?" + ParsToString(Pars));
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            return ReadTextResponse(request.GetResponse());
        }
        public static string QueryGetWebService(String URL)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "GET"; 
            SetWebRequest(request);
            return ReadTextResponse(request.GetResponse());
        }
        public static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars)
        {
            if (_xmlNamespaces.ContainsKey(URL))
            {
                return QuerySoapWebService(URL, MethodName, Pars, _xmlNamespaces[URL].ToString());
            }
            else
            {
                return QuerySoapWebService(URL, MethodName, Pars, GetNamespace(URL));
            }
        }

        private static XmlDocument QuerySoapWebService(String URL, String MethodName, Hashtable Pars, string XmlNs)
        {
            _xmlNamespaces[URL] = XmlNs;//加入缓存，提高效率
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("SOAPAction", "\"" + XmlNs + (XmlNs.EndsWith("/") ? "" : "/") + MethodName + "\"");
            SetWebRequest(request);
            byte[] data = EncodeParsToSoap(Pars, XmlNs, MethodName);
            WriteRequestData(request, data);
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();
            doc = ReadXmlResponse(request.GetResponse());

            XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            String RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;
            doc2.LoadXml("<root>" + RetXml + "</root>");
            AddDelaration(doc2);
            return doc2;
        }
        private static string GetNamespace(String URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "?WSDL");
            SetWebRequest(request);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sr.ReadToEnd());
            sr.Close();
            return doc.SelectSingleNode("//@targetNamespace").Value;
        }

        private static byte[] EncodeParsToSoap(Hashtable Pars, String XmlNs, String MethodName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");
            AddDelaration(doc);
            //XmlElement soapBody = doc.createElement_x_x("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            //XmlElement soapMethod = doc.createElement_x_x(MethodName);
            XmlElement soapMethod = doc.CreateElement(MethodName);
            soapMethod.SetAttribute("xmlns", XmlNs);
            foreach (string k in Pars.Keys)
            {
                //XmlElement soapPar = doc.createElement_x_x(k);
                XmlElement soapPar = doc.CreateElement(k);
                soapPar.InnerXml = ObjectToSoapXml(Pars[k]);
                soapMethod.AppendChild(soapPar);
            }
            soapBody.AppendChild(soapMethod);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }
        private static string ObjectToSoapXml(object o)
        {
            XmlSerializer mySerializer = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            mySerializer.Serialize(ms, o);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
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

        private static byte[] EncodePars(string Pars)
        {
            return Encoding.UTF8.GetBytes(Pars);
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

        private static XmlDocument ReadXmlResponse(WebResponse response)
        {
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            sr.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXml);
            return doc;
        }
        private static string ReadTextResponse(WebResponse response)
        {
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            sr.Close();
            return retXml;
        }
        private static void AddDelaration(XmlDocument doc)
        {
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
        }

        private static Hashtable _xmlNamespaces = new Hashtable();//缓存xmlNamespace，避免重复调用GetNamespace

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