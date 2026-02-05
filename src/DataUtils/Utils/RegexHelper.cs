using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace Feng.Utils
{
    public class RegexHelper
    {
        /// <summary>
        /// 提取信息中的中国电话号码（包括移动和固定电话）:(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}     
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhone(string input)
        {
            string txt = @"(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(input, txt);
            return match.Success;

        }
        /// <summary>
        /// 提取信息中的中国电话号码（包括移动和固定电话）:(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}     
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhone(string input, out string phone)
        {
            phone = string.Empty;

            string txt = @"(\(\d{3,4}\)|\d{3,4}-)?\d{7,14}";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(input, txt);
            if (match.Success)
            {
                phone = match.Value;
                if (phone.Length < 12)
                {
                    return true;
                }
            }

            string txt2 = @"\s+";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(txt2);
            input = regex.Replace(input, "");

            txt2 = @"-";
            regex = new System.Text.RegularExpressions.Regex(txt2);
            input = regex.Replace(input, "");

            txt = @"(\(\d{3,4}\)|\d{3,4}-)?\d{7,14}";
            match = System.Text.RegularExpressions.Regex.Match(input, txt);
            if (match.Success)
            {
                phone = match.Value;
                if (phone.Length < 12)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 提取信息中的中国电话号码（包括移动和固定电话）:(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}     
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplacePhone(string input)
        {
            string txt = @"(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(txt);
            txt = regex.Replace(input, "");
            return txt;
        }
        /// <summary>
        /// 提取信息中的中国电话号码（包括移动和固定电话）:(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}     
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Replace(string input, string pattern, string newstr)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            string txt = regex.Replace(input, newstr);
            return txt;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceSpace(string input)
        {
            string txt = @"\s\s+";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(txt);
            txt = regex.Replace(input, "");
            return txt;
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceSpace(string input, string newstr)
        {
            string txt = @"\s\s+";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(txt);
            txt = regex.Replace(input, newstr);
            return txt;
        }

        public static string GetPathFormUrl(string url)
        {
            url = url.Replace('/', '\\');
            return url;
        }

        public static string GetPathToUrl(string url)
        {
            url = url.Replace('\\', '/');
            return url;
        }
    }





}
