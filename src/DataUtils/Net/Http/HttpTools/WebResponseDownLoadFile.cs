using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;

namespace Feng.Net.Http
{

    public class WebResponseDownLoadFile
    {

        public static bool DownloadFile(string URL, string filename)
        {
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                long totalBytes = response.ContentLength;
                if (response.ContentLength != 0)
                {
                    System.IO.Stream st = response.GetResponseStream();
                    System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                    long totalDownloadedByte = 0;
                    byte[] by = new byte[1024];
                    int osize = st.Read(by, 0, (int)by.Length);
                    while (osize > 0)
                    {
                        totalDownloadedByte = osize + totalDownloadedByte;
                        so.Write(by, 0, osize);
                        osize = st.Read(by, 0, (int)by.Length);
                    }
                    so.Close();
                    st.Close();
                    return true;
                }
            }
            catch (System.Exception)
            {
            }
            return false;
        }


    }
}
