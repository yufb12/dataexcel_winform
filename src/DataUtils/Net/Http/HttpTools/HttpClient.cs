using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Feng.Net.Http
{

    public class PostMainHttpHelper
    {
        public PostMainHttpHelper()
        {
            ShowDebugInfo = false;
        }
        private CookieContainer _cookieContainer = new CookieContainer();
        public bool ShowDebugInfo { get; set; }
        private int _timeout = 30000;

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public int ResponseTime { get; set; }
        public string SendRequest2(string url, string method, Dictionary<string, string> headers, byte[] requestBody, string contentType)
        {
            try
            {

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;

                if (!string.IsNullOrEmpty(contentType))
                {
                    request.ContentType = contentType;
                }

                request.CookieContainer = _cookieContainer;
                request.Timeout = _timeout;

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                if (requestBody != null && (method == "POST" || method == "PUT" || method == "PATCH" || method == "DELETE"))
                {
                    request.ContentLength = requestBody.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(requestBody, 0, requestBody.Length);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                _cookieContainer.Add(response.Cookies);
                string result = string.Empty;
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    result = reader.ReadToEnd();
                }
                stopwatch.Stop();
                string responsetext = GetResponseText(response, result, (int)stopwatch.ElapsedMilliseconds, url);
                return responsetext;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    _cookieContainer.Add(response.Cookies);

                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        return "Error: " + ex.Message + "\r\nStatus Code: " + (int)response.StatusCode + " " + response.StatusDescription + "\r\nResponse: " + reader.ReadToEnd();
                    }
                }
                else
                {
                    return "Error: " + ex.Message + "\r\n" + ex.StackTrace;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        public static byte[] GetFile(string url)
        { 
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                const SecurityProtocolType Tls11 = (SecurityProtocolType)768;
                const SecurityProtocolType Tls12 = (SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | Tls11 | Tls12;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";  
 
                HttpWebResponse response = (HttpWebResponse)request.GetResponse(); 
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        byte[] data;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // 定义缓冲区，每次读取4KB（最佳实践）
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            // 循环读取直到流结束
                            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, bytesRead);
                            }
                            // 将MemoryStream中的内容转换为byte数组
                            data = ms.ToArray();
                        }
                        return data;
                    }
                }
                return null;
            }
            catch (WebException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string SendRequest(string url, string method, Dictionary<string, string> headers,
            byte[] requestBody, string contentType)
        {
            string result = string.Empty;
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                const SecurityProtocolType Tls11 = (SecurityProtocolType)768;
                const SecurityProtocolType Tls12 = (SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | Tls11 | Tls12;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;

                if (!string.IsNullOrEmpty(contentType))
                {
                    request.ContentType = contentType;
                }

                request.CookieContainer = _cookieContainer;
                request.Timeout = _timeout;

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                        {
                            request.ContentType = header.Value;
                        }
                        else
                        {
                            request.Headers.Add(header.Key, header.Value);
                        }
                    }
                }

                if (requestBody != null && (method == "POST" || method == "PUT" || method == "PATCH" || method == "DELETE"))
                {
                    request.ContentLength = requestBody.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(requestBody, 0, requestBody.Length);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                _cookieContainer.Add(response.Cookies);
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                stopwatch.Stop();
                if (!ShowDebugInfo)
                    return result;
                ResponseTime = (int)stopwatch.ElapsedMilliseconds;
                string responsetext = GetResponseText(response, result, (int)stopwatch.ElapsedMilliseconds, url);
                return responsetext;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    _cookieContainer.Add(response.Cookies);

                    using (Stream stream = response.GetResponseStream())
                    {
                        string responseContent = string.Empty;
                        if (stream != null)
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                responseContent = reader.ReadToEnd();
                            }
                        }
                        return $"Error: {ex.Message}\r\nStatus Code: {(int)response.StatusCode} {response.StatusDescription}\r\nResponse: {responseContent}";
                    }
                }
                else
                {
                    return $"Error: {ex.Message}\r\n{ex.StackTrace}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Get(string url, Dictionary<string, string> headers = null)
        {
            return SendRequest(url, "GET", headers, null, null);
        }

        public string PostRaw(string url, string rawData, string contentType, Dictionary<string, string> headers = null)
        {
            byte[] data = Encoding.UTF8.GetBytes(rawData);
            return SendRequest(url, "POST", headers, data, contentType);
        }

        public string PostFormData(string url, Dictionary<string, string> formData, Dictionary<string, string> headers = null)
        {
            string boundary = "----WebKitFormBoundary" + DateTime.Now.Ticks.ToString("x");
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> pair in formData)
            {
                sb.Append("--" + boundary + "\r\n");
                sb.Append("Content-Disposition: form-data; name=\"" + pair.Key + "\"\r\n\r\n");
                sb.Append(pair.Value + "\r\n");
            }

            sb.Append("--" + boundary + "--\r\n");
            byte[] data = Encoding.UTF8.GetBytes(sb.ToString());
            return SendRequest(url, "POST", headers, data, "multipart/form-data; boundary=" + boundary);
        }

        public string PostFile(string url, string filePath, string fileName, Dictionary<string, string> headers = null)
        {
            string boundary = "----WebKitFormBoundary" + DateTime.Now.Ticks.ToString("x");

            // 构建表单头部
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"file\"; filename=\"" + fileName + "\"\r\n");
            sb.Append("Content-Type: application/octet-stream\r\n\r\n");
            byte[] headerBytes = Encoding.UTF8.GetBytes(sb.ToString());

            // 读取文件内容
            byte[] fileBytes = File.ReadAllBytes(filePath);

            // 构建表单尾部
            string footer = "\r\n--" + boundary + "--\r\n";
            byte[] footerBytes = Encoding.UTF8.GetBytes(footer);

            // 合并所有字节
            byte[] requestBody = new byte[headerBytes.Length + fileBytes.Length + footerBytes.Length];
            Buffer.BlockCopy(headerBytes, 0, requestBody, 0, headerBytes.Length);
            Buffer.BlockCopy(fileBytes, 0, requestBody, headerBytes.Length, fileBytes.Length);
            Buffer.BlockCopy(footerBytes, 0, requestBody, headerBytes.Length + fileBytes.Length, footerBytes.Length);

            return SendRequest(url, "POST", headers, requestBody, "multipart/form-data; boundary=" + boundary);
        }

        public string Put(string url, string data, string contentType, Dictionary<string, string> headers = null)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return SendRequest(url, "PUT", headers, dataBytes, contentType);
        }

        public string Delete(string url, string data = null, string contentType = null, Dictionary<string, string> headers = null)
        {
            byte[] dataBytes = null;
            if (!string.IsNullOrEmpty(data))
            {
                dataBytes = Encoding.UTF8.GetBytes(data);
            }
            return SendRequest(url, "DELETE", headers, dataBytes, contentType);
        }

        public string Head(string url, Dictionary<string, string> headers = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";
                request.CookieContainer = _cookieContainer;
                request.Timeout = _timeout;

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                _cookieContainer.Add(response.Cookies);

                StringBuilder sb = new StringBuilder();
                sb.Append("Status Code: " + (int)response.StatusCode + " " + response.StatusDescription + "\r\n");
                foreach (string key in response.Headers.AllKeys)
                {
                    sb.Append(key + ": " + response.Headers[key] + "\r\n");
                }
                return sb.ToString();
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    _cookieContainer.Add(response.Cookies);

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Error: " + ex.Message + "\r\n");
                    sb.Append("Status Code: " + (int)response.StatusCode + " " + response.StatusDescription + "\r\n");
                    foreach (string key in response.Headers.AllKeys)
                    {
                        sb.Append(key + ": " + response.Headers[key] + "\r\n");
                    }
                    return sb.ToString();
                }
                else
                {
                    return "Error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string Options(string url, Dictionary<string, string> headers = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "OPTIONS";
                request.CookieContainer = _cookieContainer;
                request.Timeout = _timeout;

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                _cookieContainer.Add(response.Cookies);

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    _cookieContainer.Add(response.Cookies);

                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        return "Error: " + ex.Message + "\r\nStatus Code: " + (int)response.StatusCode + " " + response.StatusDescription + "\r\nResponse: " + reader.ReadToEnd();
                    }
                }
                else
                {
                    return "Error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string Trace(string url, Dictionary<string, string> headers = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "TRACE";
                request.CookieContainer = _cookieContainer;
                request.Timeout = _timeout;

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                _cookieContainer.Add(response.Cookies);

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    _cookieContainer.Add(response.Cookies);

                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        return "Error: " + ex.Message + "\r\nStatus Code: " + (int)response.StatusCode + " " + response.StatusDescription + "\r\nResponse: " + reader.ReadToEnd();
                    }
                }
                else
                {
                    return "Error: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string Patch(string url, string data, string contentType, Dictionary<string, string> headers = null)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return SendRequest(url, "PATCH", headers, dataBytes, contentType);
        }

        public void ClearCookies()
        {
            _cookieContainer = new CookieContainer();
        }

        private string GetResponseText(HttpWebResponse response, string content, int responseTime, string url)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine("url: " + url);
                sb.AppendLine("ResponseTime: " + responseTime.ToString("F0") + "ms");
                sb.AppendLine("DateTime: " + DateTime.Now);
                sb.AppendLine("Content:Begin#");
                sb.AppendLine("###############################################################################B");
                sb.AppendLine(content);
                sb.AppendLine("###############################################################################E");
                sb.AppendLine("Content:End#");

                sb.AppendLine("ContentEncoding: " + response.ContentEncoding);
                sb.AppendLine("ProtocolVersion: " + response.ProtocolVersion.ToString());
                sb.AppendLine("StatusDescription: " + response.StatusDescription);
                sb.AppendLine("LastModified: " + response.LastModified);
                sb.AppendLine("Server: " + response.Server);
                sb.AppendLine("CharacterSet: " + response.CharacterSet);
                sb.AppendLine("ContentType: " + response.ContentType);
                sb.AppendLine("Method: " + response.Method);
                sb.AppendLine("StatusCode: " + response.StatusCode);
                sb.AppendLine("ContentLength: " + response.ContentLength.ToString() + " bytes");


                if (response.Headers != null)
                {
                    sb.AppendLine("Headers:#");
                    foreach (string key in response.Headers.AllKeys)
                    {
                        sb.AppendLine(key + ":" + response.Headers[key]);
                    }
                }

                if (response.Cookies != null && response.Cookies.Count > 0)
                {
                    sb.AppendLine("Cookies:");
                    foreach (Cookie cookie in response.Cookies)
                    {
                        sb.AppendLine(string.Format("Name:[{0}],Value:[{1}], Domain:[{2}], Path:[{3}]", cookie.Name, cookie.Value, cookie.Domain, cookie.Path));
                    }
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("Exception: " + ex.Message);
            }

            return sb.ToString();
        }
    }
}