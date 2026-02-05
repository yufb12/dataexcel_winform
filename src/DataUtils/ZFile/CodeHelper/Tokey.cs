using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Feng.DevTools.CExtend
{ 
    public class CodeFile {
        private string _file = string.Empty;
        public string File { get { return this._file; } set { this._file = value; } }

        public TokeyCollection GetTokeys()
        {

            TokeyCollection ts = new TokeyCollection();
            if (System.IO.File.Exists(this._file))
            {
                string text = System.IO.File.ReadAllText(_file);
                Tokey tk = null;
                for (int i = 0; i < text.Length; i++)
                {
                    char c=text [i ];
  
                    if (char.IsWhiteSpace(c))
                    {
                        tk = null;
                    }
                    else
                    {
                        bool sysfol = false;
                        if (char.IsPunctuation(c))
                        {
                            tk = new Tokey();
                            ts.Add(tk);
                            sysfol = true;
                        }
                        if (char.IsSeparator(c))
                        {
                            tk = new Tokey();
                            ts.Add(tk);
                            sysfol = true;
                        }
                        if (char.IsSymbol(c))
                        {
                            tk = new Tokey();
                            ts.Add(tk);
                            sysfol = true;
                            if (i < (text.Length - 1))
                            {
                                char nc = text[i + 1];
                                if (nc != ' ')
                                {

                                }
                                switch (c)
                                {
                                    case '|':
                                    case '=':
                                    case '/':
                                    case '&':
                                        if (nc == c)
                                        {
                                            sysfol = false;
                                        }
                                        break;

                                    case '+':
                                        if (nc == '=' || nc == '+')
                                        {
                                            sysfol = false;
                                        }
                                        break;
                                    case '-':
                                        if (nc == '=' || nc == '-')
                                        {
                                            sysfol = false;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        if (tk == null)
                        {
                            tk = new Tokey();
                            ts.Add(tk);
                        }
                        tk.AddText(c);
                        if (sysfol)
                        {
                            tk = null;
                        }
                    }
                }
            }
            return ts;
        }
        public void DoExpress(TokeyCollection ts)
        {
            Tokey tokey = null;
            if (ts.Count > 0)
            {
                tokey = ts[0];
            }
            
        }

        public void DoNextProcess(TokeyCollection ts, Tokey tokey)
        {

        }
    }

    public static class KeyWords
    {
        static KeyWords()
        {
            _keys = new List<string>();
            _keys.Add("int");
            _keys.Add("short");
            _keys.Add("long");
            _keys.Add("UInt16");
            _keys.Add("UInt32");
            _keys.Add("Uint64");
            _keys.Add("Int6");
            _keys.Add("Int32");
            _keys.Add("Int64");
            _keys.Add("double");
            _keys.Add("float");
            _keys.Add("decimal");
            _keys.Add("string");
            _keys.Add("bool"); 
        }
        private static List<string> _keys = null;
        public static List<string> Keys { get { return _keys; } }
        public static bool IsKeyWord(string text)
        {
            return Keys.Contains(text);
        }
    }

    public class Tokey
    {
        private System.Text.StringBuilder sb = new System.Text.StringBuilder();
        public void AddText(string text)
        {
            sb.Append(text);
        }
        public void AddText(char text)
        {
            sb.Append(text);
        } 
        public string Text { get { return sb.ToString (); } }
        private int _line = 0;
        public int Line { get { return this._line; } set { this._line = value; } }
        private CodeFile _file = null;
        public CodeFile File { get { return this._file; } set { this._file = value; } }
        public override string ToString()
        {
            return sb.ToString();
        }
        public Tokey Next { get; set; }
        public Tokey Previous { get; set; }
        public bool Isterminator
        {
            get
            {
                if (sb.ToString() == ";")
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsKey
        {
            get 
            {
                return KeyWords.IsKeyWord(this.sb.ToString());
            }
        }
            
    }
 
    public class TokeyCollection : IList<Tokey>
    {
        List<Tokey> list = new List<Tokey>();
        public int IndexOf(Tokey item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, Tokey item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Tokey this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(Tokey item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Tokey item)
        {
            return list.Contains(item);
        }

        public void CopyTo(Tokey[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Tokey item)
        {
            return list.Remove(item);
        }

        public IEnumerator<Tokey> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

    public enum TokeyType
    {
        Tint,
        Tshort,
        Tdecimal,
        Tfloat,
        Tdouble,
        Tchar,
        Tstring, 
    }

    //public class Token
    //{
    //    public string Text { get; set; }

    //    public Token Parse(string text, int index, out int outindex)
    //    {
    //        Token tk = new Token();
    //        outindex = 0;
    //        return tk;
    //    }
    //}
 

    //public class KeyTokey:Token
    //{

    //}

    //public class SysolTokey : Token
    //{

    //}

    public class Parse
    {

    }
}
