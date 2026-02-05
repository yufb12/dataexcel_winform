using System.Text;

namespace Feng.Net.Protocol
{
    public class DtpDns
    {
        public const string Default_ServerPort = "1021";
        public const string Default_ServerWebPort = "1020";
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

        public string GetPrevDirectory(string FullPath)
        { 
            string path = FullPath;
            int lastdoma = path.LastIndexOf('/');
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
