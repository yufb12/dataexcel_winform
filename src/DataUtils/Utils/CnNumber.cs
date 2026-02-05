using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Utils
{
    public class CnNumber
    {
        public static string GetString(decimal d)
        {

            string text = d.ToString("#0.00");
            StringBuilder sbt = new StringBuilder();
            char c = '\0';
            for (int i = 0; i < text.Length; i++)
            {
                c = text[text.Length - i - 1];
                switch (i)
                {
                    case 0:
                        if (c != '0')
                        {
                            sbt.Insert(0, '分');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 1:
                        if (c != '0')
                        {
                            sbt.Insert(0, '角');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        if (c != '0')
                        {
                            sbt.Insert(0, '元');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        else if (text.Length > 4)
                        {
                            sbt.Insert(0, '元');
                        }
                        break;
                    case 4:
                        if (c != '0')
                        {
                            sbt.Insert(0, '拾');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 5:
                        if (c != '0')
                        {
                            sbt.Insert(0, '百');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 6:
                        if (c != '0')
                        {
                            sbt.Insert(0, '千');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 7:
                        if (c != '0')
                        {
                            sbt.Insert(0, '万');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 8:
                        if (c != '0')
                        {
                            sbt.Insert(0, '拾');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 9:
                        if (c != '0')
                        {
                            sbt.Insert(0, '百');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 10:
                        if (c != '0')
                        {
                            sbt.Insert(0, '千');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 11:
                        if (c != '0')
                        {
                            sbt.Insert(0, '亿');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 12:
                        if (c != '0')
                        {
                            sbt.Insert(0, '拾');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 13:
                        if (c != '0')
                        {
                            sbt.Insert(0, '百');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 14:
                        if (c != '0')
                        {
                            sbt.Insert(0, '千');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    case 15:
                        if (c != '0')
                        {
                            sbt.Insert(0, '万');
                            sbt.Insert(0, GetCNChar(c));
                        }
                        break;
                    default:
                        if (c != '0')
                        {
                            sbt.Append("Error");
                        }
                        break;
                }
            }
            return sbt.ToString();
        }
        public static string GetNumString(decimal d)
        {

            string text = d.ToString("#0.00");
            StringBuilder sbt = new StringBuilder();
            char c = '\0';
            for (int i = text.Length - 1; i >= 0; i--)
            {
                c = text[i];
                if (c != '.')
                {
                    sbt.Insert(0, (c));
                }
            }
            return sbt.ToString();
        }
        public static string GetBigWrite(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                sb.Append(GetCNChar(c));
            }
            return sb.ToString();
        }
        private static char GetCNChar(char c)
        {
            char[] dis = { '零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖' };
            int ct = c;
            switch (ct)
            {
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                    return dis[ct - 48];
                default:
                    return 'e';
            }
        }
    }

}
