using System;
using System.IO;
using System.Net;
namespace Feng.Net.Http
{ 
    public class FileDownloader
    {
        public bool DownloadFileWithResume(string url, string localFilePath)
        {
            try
            {
                // 检查文件是否存在以及需要从哪里开始下载  
                long startByte = 0;
                if (File.Exists(localFilePath))
                {
                    FileInfo fileInfo = new FileInfo(localFilePath);
                    startByte = fileInfo.Length;
                }

                // 创建 HttpWebRequest  
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                // 设置 Range 头部以请求部分文件  
                if (startByte > 0)
                {
                    request.AddRange(startByte);
                }

                // 发送请求并获取响应  
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        // 检查服务器是否支持范围请求  
                        if (response.StatusCode == HttpStatusCode.PartialContent ||
                            response.StatusCode == HttpStatusCode.OK)
                        {
                            // 读取响应内容长度（如果可用）  
                            long contentLength = response.ContentLength;
                            if (contentLength == -1 && startByte > 0)
                            {
                                // 如果服务器没有提供内容长度，并且这是范围请求，  
                                // 则我们需要另一种方式来知道何时完成下载（例如，读取直到结束）  
                                throw new Exception("Unable to determine content length for range request.");
                            }

                            // 打开文件以追加内容  
                            using (FileStream fileStream = File.Open(localFilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                            {
                                // 读取响应流并写入文件  
                                byte[] buffer = new byte[4096];
                                int bytesRead;
                                long totalDownloaded = startByte;

                                using (Stream responseStream = response.GetResponseStream())
                                {
                                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        fileStream.Write(buffer, 0, bytesRead);
                                        totalDownloaded += bytesRead;
                                    }
                                }

                                // 如果需要，可以在这里处理完成下载的逻辑  
                            }
                            return true;
                        }
                        else
                        {
                            // 处理不支持范围请求的情况  
                            throw new Exception("Server does not support range requests.");
                        }
                    }
                }
                catch (WebException ex)
                {
                    // 处理网络异常  
                    if (ex.Response != null)
                    {
                        using (HttpWebResponse errorResponse = (HttpWebResponse)ex.Response)
                        {
                            // 可以在这里根据HTTP状态码处理错误  
                            Console.WriteLine("Error Code : {0}", errorResponse.StatusCode);
                        }
                    }
                    else
                    {
                        // 处理没有响应的异常（如连接问题）  
                        Console.WriteLine("An error occurred while sending the request.");
                    }
                }
                catch (Exception ex)
                {
                    // 处理其他异常  
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

            }
            catch (Exception)
            { 
            }
            return false;
        }

        // HttpWebRequest 扩展方法以添加 Range 头部  
        private void HttpWebRequestAddRange(HttpWebRequest request, long startByte)
        {
            request.Headers.Add("Range", string.Format("bytes={0}-", startByte));
        }
    } 
}