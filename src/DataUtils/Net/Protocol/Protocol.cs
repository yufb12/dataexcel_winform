using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Feng.Net.Tcp;
using System.Drawing;
using Feng.Data;
using Feng.Net.EventHandlers;
using Feng.Net.Base;

namespace Feng.Net.Packets
{
    public class Protocol
    {
        public Protocol()
        { 

        }
 
    }
    public class UrlParse
    {
        public string Text { get; set; }
        private int position = 0;
        string protocol = string.Empty;
        string address = string.Empty;
        string port = string.Empty;
        string path = string.Empty;
        string arg = string.Empty;
        public char Read()
        { 
            return Text[position++];
        }
        public void Back()
        {
            position--;
        }
        private bool End()
        {
            return position >= Text.Length;
        }
        public void Parse()
        {
            char c = ' ';
            while (!End())
            {
                c = Read();
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }
                break;
            }
            if (char.IsLetter(c))
            {
                ReadProtocol();
            }
        }
        private bool ReadProtocol()
        {
            StringBuilder sb = new StringBuilder();
            bool result = false;
            while (!End())
            {
                char c = Read();
                if (char.IsLetter(c))
                {
                    sb.Append(c); 
                    result = true;
                    continue;
                }
                protocol = sb.ToString();
                Back();
                break;
            }
            return result;
        }
    }
    public class NetUrl
    {
        public string Url { get; set; }
        public string Protocol { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public string Arg { get; set; }
        public System.Collections.Specialized.StringDictionary GetArgs()
        {
            System.Collections.Specialized.StringDictionary dic = new System.Collections.Specialized.StringDictionary();
            
            return dic;
        }
        
        public bool Parse()
        {
            int index = 0;
            StringBuilder sb = new StringBuilder();
            string protocol = string.Empty;
            string address = string.Empty;
            string port = string.Empty;
            string path = string.Empty;
            string arg = string.Empty;
            bool sucess = false;
            string url = Url.Trim();
            #region protocol
            for (; index < url.Length; index++)
            {
                char c = url[index];
                if (char.IsLetter(c))
                {
                    sb.Append(c);
                    index++;
                    for (; index < url.Length; index++)
                    {
                        if (char.IsLetter(c))
                        {
                            sb.Append(c);
                        }
                        else
                        {
                            index--;
                            break;
                        }
                    }
                }
                else if (c == ':')
                {
                    index++;
                    for (; index < url.Length; index++)
                    {

                    }
                } 
                else
                {
                    return false;
                }
            }
            if (!sucess)
            {
                return false;
            }
            #endregion
            protocol = sb.ToString();
            sb.Length = 0;
            sucess = false;
            bool hasport = false;
            #region address
            for (; index < url.Length; index++)
            {
                char c = url[index];
                if (char.IsLetter(c))
                {
                    sb.Append(c);
                    index++;
                    for (; index < url.Length; index++)
                    {
                        if (char.IsLetter(c))
                        {
                            sb.Append(c);
                        }
                        else
                        {
                            index--;
                            break;
                        }
                    }
                }
                else if (c == ' ')
                {
                    continue;
                }
                else if (c == ':')
                {
                    if (sb.Length > 1)
                    {
                        hasport = true;
                        sucess = true;
                    }
                    break;
                }
                else if (c == '/')
                {
                    index++;
                    if (index < url.Length)
                    {
                        c = url[index];
                        if (c == '/')
                        {
                            sucess = true;
                            break;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            #endregion
            if (!sucess)
            {
                return false;
            }
            address = sb.ToString();
            sb.Length = 0;
            sucess = false;
            #region port
            if (hasport)
            {
                for (; index < url.Length; index++)
                {
                    char c = url[index];
                    if (char.IsNumber(c))
                    {
                        sb.Append(c);
                        index++;
                        for (; index < url.Length; index++)
                        {
                            if (char.IsNumber(c))
                            {
                                sb.Append(c);
                            }
                            else
                            {
                                index--;
                                break;
                            }
                        }
                    }
                    else if (c == ' ')
                    {
                        continue;
                    } 
                    else if (c == '/')
                    {
                        index++;
                        if (index < url.Length)
                        {
                            c = url[index];
                            if (c == '/')
                            {
                                sucess = true;
                                break;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            #endregion

            return true;
        }
    }

    public class LexingTool
    {
        public string ReadString(string text, int startposition, out int endposition)
        {
            endposition = -1;
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }
    }
}


 