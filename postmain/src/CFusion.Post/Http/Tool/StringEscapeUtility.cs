using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Tools
{

    public static class StringEscapeUtility
    {
        public static string EscapeSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            StringBuilder result = new StringBuilder(input.Length);
            foreach (char c in input)
            {
                switch (c)
                {
                    case '\\':
                        result.Append("\\\\");
                        break;
                    case '"':
                        result.Append("\\\"");
                        break;
                    // 添加更多特殊字符的转义处理
                    //case '\n':
                    //    result.Append("\\n");
                    //    break;
                    //case '\r':
                    //    result.Append("\\r");
                    //    break;
                    //case '\t':
                    //    result.Append("\\t");
                    //    break;
                    default:
                        result.Append(c);
                        break;
                }
            }
            return result.ToString();
        }
    }
}