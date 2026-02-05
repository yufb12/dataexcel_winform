using System.Text;

namespace Feng.Net.Protocol
{
    public class DtpProtocolParse
    {
        //<用户名>:<密码>@<主机>:<端口>/<url路径>
        /// <summary>
        /// dtp://email.dtp.com/user/wang/view
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static DtpProtocol Parse(string url)
        {
            DtpProtocol dtpProtocol = new DtpProtocol();
            int start = 0;
            ClearSpace(url, ref start);
            string str = Match(url, ref start, "dtp");
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            ClearSpace(url, ref start);
            str = Match(url, ref start, ":");
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            str = Match(url, ref start, "//");
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            dtpProtocol.ProtocolType = "Dtp";
            ClearSpace(url, ref start);
            for (int i = 0; i < 5; i++)
            {
                string var = MatchVar(url, ref start);
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                ClearSpace(url, ref start);
                str = Match(url, ref start, ".");
                if (!string.IsNullOrEmpty(str))
                {
                    dtpProtocol.SetHostName(var);
                    continue;
                }
                str = Match(url, ref start, "/");
                if (!string.IsNullOrEmpty(str))
                {
                    dtpProtocol.DomainName = var;
                    break;
                }
                str = Match(url, ref start, ":");
                if (!string.IsNullOrEmpty(str))
                {
                    dtpProtocol.DomainName = var;
                    str = MatchVar(url, ref start);
                    if (!string.IsNullOrEmpty(str))
                    {
                        dtpProtocol.Port = Feng.Utils.ConvertHelper.ToInt32(str);
                        break;
                    }
                }
                return null;
            }
            str = GetPath(url, ref start);
            dtpProtocol.Path = str;
            return dtpProtocol;
        }
        private static void ClearSpace(string url, ref int start)
        {
            for (int i = start; i < url.Length; i++)
            {
                char c = url[i];
                if (char.IsWhiteSpace(c))
                {
                    start = i + 1;
                    continue;
                }
                break;
            }

        }
        private static string GetPath(string url, ref int start)
        {
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 100; i++)
            //{
            //    string var = MatchVar(url, ref start);
            //    if (string.IsNullOrEmpty(var))
            //    {
            //        break;
            //    }
            //    ClearSpace(url, ref start);
            //    string str = Match(url, ref start, "/");
            //}
            //return sb.ToString();
            if (start < url.Length)
            {
                return url.Substring(start);
            }
            return string.Empty;
        }
        private static string MatchVar(string url, ref int start)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = start; i < url.Length; i++)
            {
                char c = url[i];

                if (char.IsLetter(c) || char.IsDigit(c) || c == '_')
                {
                    sb.Append(c);
                    continue;
                }
                if (sb.Length > 0)
                {
                    start = i;
                    return sb.ToString();
                }
                break;
            }
            return string.Empty;
        }
        private static string Match(string url, ref int start, string txt)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = start; i < url.Length; i++)
            {
                char c = url[i];

                sb.Append(c);
                if (sb.Length == txt.Length)
                {
                    if (sb.ToString().ToUpper() == txt.ToUpper())
                    {
                        start = i + 1;
                        return sb.ToString();
                    }
                    break;
                }
                continue;
            }
            return string.Empty;
        }

        public static string ToHttpUrl(DtpProtocol dtpurl,string ip,string port)
        {
            string url = "http://"+ ip;
            if (!string.IsNullOrWhiteSpace(port))
            {
                url = url + ":" + port;
            }
            url = url + dtpurl.Path;
            return url;
        }

    }

}
