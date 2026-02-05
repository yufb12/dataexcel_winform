using System.Text;

namespace Feng.Net.Protocol
{
    public class DtpProtocol
    {
 
        /// <summary>
        ///  相对路径 Relative path
        /// [./] 当前目录 可以省略 ./file/test.fexm 等同 file/test.fexm
        /// [../]上级目录 
        /// [/] 根目录    /file/test.fexm       /// </summary> 
        /// <param name="currentpath">不包含文件</param>
        /// <returns></returns>
        public static string GetRelativePath(string url, string currentpath)
        {
            string path = url.Trim();
            if (path.StartsWith ("/"))
            {
                return url;
            }
            if (path.StartsWith("./"))
            {
                string text = Feng.Utils.TextHelper.RemoveStart(url,"./");
                return Comine(currentpath, text);
            }
            if (path.StartsWith("../"))
            {
                return GetRelativeParentPath(url, currentpath);
            }
            return Comine(currentpath, url);
        }
        /// <summary>
        /// 文件路径转URL路径
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetRelativeFile(string file)
        {
            string path = file.Replace("\\", "/"); 
            return path;
        }
        /// <summary>
        /// 获取文件路径 不包含文件名
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetRelativePath(string file)
        {
            string path = file.Replace("\\", "/"); 
            int lastdoma = path.LastIndexOf('/');
            if (lastdoma > 0)
            {
                path = path.Substring(0, lastdoma);
                return path;
            } 
            return path;
        }

        private static string GetRelativeParentPath(string url, string currentpath)
        {
            string parentpath = Feng.Utils.TextHelper.RemoveLast(currentpath,"/");
            string curl = Feng.Utils.TextHelper.RemoveStart(url, "../");
            if (curl .StartsWith ("../"))
            {
                return GetRelativeParentPath(curl, parentpath);
            }
            return Feng.Utils.TextHelper.Comine(parentpath, curl);
        }

        public static string FormatFullUrl(string url)
        {
            if (url.StartsWith("\\"))
            {
                return url;
            }
            return "\\" + url;
        }

        public static int GetParentText(string text, int value)
        {
            char sf = '/';
            string str = text.TrimEnd(sf);
            int len = 0;
            int start = text.LastIndexOf(sf);
            while (start > 0)
            {
                len = len + 1;
                start = start -1;
                start = text.LastIndexOf(sf);
            }
            return len;
        }

        public static string Comine(string url1,string url2)
        { 
            if (url1.EndsWith ("/"))
            {
                if (url2.StartsWith("/"))
                {
                    return url1 + url2.TrimStart('/');
                }
                return url1 + url2;
            }
            else
            {
                if (url2.StartsWith("/"))
                {
                    return url1 + url2;
                }
                return url1 +"/"+ url2;

            }
        }

        public string Url
        {
            get
            {
                string port = string.Empty;
                if (Port > 0)
                {
                    port = ":" + Port.ToString();
                }
                return ProtocolType + "://" + Host + port + Path;
            }
        }

        public string FileName
        {
            get
            {
                int lastdoma = Path.LastIndexOf('/');
                if (lastdoma > 0)
                {
                    return Path.Substring(lastdoma+1);
                }
                return string.Empty;
            }
        }
        public string CurrentDirectory
        {
            get
            {
                string port = string.Empty;
                if (Port > 0)
                {
                    port = ":" + Port.ToString();
                }
                return ProtocolType + "://" + Host + port + Directory;
            }
        }
        public string GetUrl(string file)
        {
            if (file.StartsWith("/"))
            {
                return GetRootUrl(file);
            }
            else if (file.StartsWith("./././././"))
            {
                return GetSamePrevUrl(GetPrevDirectory(GetPrevDirectory(GetPrevDirectory(GetPrevDirectory(GetPrevDirectory(Path))))), file);
            }
            else if (file.StartsWith("././././"))
            {
                return GetSamePrevUrl(GetPrevDirectory(GetPrevDirectory(GetPrevDirectory(GetPrevDirectory(Path)))), file);
            }
            else if (file.StartsWith("./././"))
            {
                return GetSamePrevUrl(GetPrevDirectory(GetPrevDirectory(GetPrevDirectory(Path))), file);
            }
            else if (file.StartsWith("././"))
            {
                return GetSamePrevUrl(GetPrevDirectory(GetPrevDirectory(Path)), file);
            }
            else if (file.StartsWith("./"))
            {
                return GetSamePrevUrl(GetPrevDirectory(Path), file);
            }
            file = file.ToLower();
            if (file.StartsWith("dtp://"))
            {
                return file;
            }
            return GetSameUrl(file);
        }
        public string GetSameUrl(string file)
        {
            string port = string.Empty;
            if (Port > 0)
            {
                port = ":" + Port.ToString();
            } 
            string prevdirectory = Directory;
            return ComBineUrl(ProtocolType + "://" + Host + port + prevdirectory, GetFileName(file));
        }
        public string GetSamePrevUrl(string file)
        {
            string port = string.Empty;
            if (Port > 0)
            {
                port = ":" + Port.ToString();
            }
            string prevdirectory = GetPrevDirectory(GetPrevDirectory(Path));
            return ComBineUrl(ProtocolType + "://" + Host + port + prevdirectory, GetFileName(file));
        }

        public string GetSamePrevUrl(string path, string file)
        {
            string port = string.Empty;
            if (Port > 0)
            {
                port = ":" + Port.ToString();
            }
            string prevdirectory = GetPrevDirectory(path);
            return ComBineUrl(ProtocolType + "://" + Host + port + prevdirectory, GetFileName(file));
        }
        public string GetFileName(string file)
        {
            return System.IO.Path.GetFileName(file);
        }
        public string ComBineUrl(string url1, string url2)
        {
            return url1.TrimEnd('/') + "/" + url2.TrimStart('/');
        }
        public string GetRootUrl(string file)
        {
            string port = string.Empty;
            if (Port > 0)
            {
                port = ":" + Port.ToString();
            } 
            return ComBineUrl(ProtocolType + "://" + Host + port, GetFileName(file));
        }
        public string Directory
        {
            get
            {
                int lastdoma = Path.LastIndexOf('/');
                if (lastdoma > 0)
                {
                    return Path.Substring(0,lastdoma);
                }
                return string.Empty;
            }
        }

        public static string GetPrevDirectory(string FullPath)
        { 
            string path = FullPath;
            int lastdoma = path.LastIndexOf('/');
            if (lastdoma > 0)
            {
                path = path.Substring(0, lastdoma);
                return path;
            }
            lastdoma = path.LastIndexOf('\\');
            if (lastdoma > 0)
            {
                path = path.Substring(0, lastdoma);
                return path;
            }
            return string.Empty;

        }
        public string Host
        {
            get
            {
                string fld = "";
                if (!string.IsNullOrWhiteSpace(FLD))
                {
                    fld = FLD + ".";
                }
                string tld = "";
                if (!string.IsNullOrWhiteSpace(TLD))
                {
                    tld = TLD + ".";
                }

                string sld = "";
                if (!string.IsNullOrWhiteSpace(SLD))
                {
                    sld = SLD + ".";
                }

                string hostname = "";
                if (!string.IsNullOrWhiteSpace(HostName))
                {
                    hostname = HostName + ".";
                }
                return fld + tld + sld + hostname + DomainName;
            }
        }

        public string ProtocolType { get; set; }

        public string HostName { get; set; }
        public int Port { get; set; }
        /// <summary>
        /// 二级域名
        /// </summary>
        public string SLD { get; set; }
        /// <summary>
        /// 三级域名
        /// </summary>
        public string TLD { get; set; }
        /// <summary>
        /// 四级域名
        /// </summary>
        public string FLD { get; set; }
        public string Path { get; set; }
        public string DomainName { get; set; }
        public void SetHostName(string hostname)
        {
            FLD = TLD;
            TLD = SLD;
            SLD = HostName;
            HostName = hostname;
        }
    }
}
