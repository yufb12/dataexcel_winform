using System;

namespace Feng.Json
{
    public class Lexer
    {
        private string text = string.Empty;
        private int position = 0;
        private SymbolTable table = null;
        private const char empty = ' ';
        private int rowindex = 0;
        private int columnindex = 0;
        private static Lexer lexer = null;
        public static SymbolTable GetSymbolTable(string text)
        {
            if (lexer == null)
            {
                lexer = new Lexer();
            }
            return lexer.Parse(text);
        }
        public SymbolTable Parse(string input)
        {
            if (table == null)
            {
                table = new SymbolTable();
            }
            table.Tokens.Clear();
            table.ReSetPosition();
            text = input;
            position = -1;
            rowindex = 0;
            columnindex = 0;
            while (!IsEnd())
            {
                ClearEmpty(); 
                if (!HasNext())
                {
                    break;
                }
                char c = NextChar();
                Token token = GetToken(c);
                table.Tokens.Add(token); 
            }
            return table;
        }
        public void Forword()
        {
            columnindex++;
            position++;
            if (!IsEnd())
            {
                char c = text[position];
                if (c == '\n')
                {
                    rowindex++;
                }
            }
        }
        public void Back()
        {
            columnindex--;
            position--;
        }
        public bool IsEnd()
        {
            return position >= text.Length-1;
        }
        private  int ID = 0;
        private Token GetToken(char c)
        {
            switch (c)
            {
                case '\"':
                    Forword();
                    string value = ReadString();
                    return new Token(ID++, value, rowindex, columnindex);

                case '\'':
                    Forword();
                    string value2 = ReadString2();
                    return new Token(ID++, value2, rowindex, columnindex);
                case '{':
                    Forword();
                    return new Token(ID++, TokenType.LBRACE, rowindex, columnindex);
                case '}':
                    Forword();
                    return new Token(ID++, TokenType.RBRACE, rowindex, columnindex);
                case '[':
                    Forword();
                    return new Token(ID++, TokenType.LBRACKET, rowindex, columnindex);
                case ']':
                    Forword();
                    return new Token(ID++, TokenType.RBRACKET, rowindex, columnindex);
                case ',':
                    Forword();
                    return new Token(ID++, TokenType.COMMA, rowindex, columnindex);
                case ':':
                    Forword();
                    return new Token(ID++, TokenType.COLON, rowindex, columnindex); 
                case 'F':
                case 'f':
                    Forword();
                    if (HasLenChar("false".Length))
                    {
                        string strTrue = NextString("false".Length).ToLower();
                        if (strTrue == "false")
                        {
                            Forword("false".Length-1);
                            return new Token(ID++, TokenType.FALSE, strTrue, rowindex, columnindex);
                        }
                    }
                    Back();
                    return ReadVar(c);
                case 'T':
                case 't':
                    Forword();
                    if (HasLenChar("true".Length))
                    {
                        string strTrue = NextString("true".Length).ToLower();
                        if (strTrue == "true")
                        {
                            Forword("true".Length-1);
                            return new Token(ID++, TokenType.TRUE, strTrue, rowindex, columnindex);
                        }
                    }
                    Back();
                    return ReadVar(c);
                case 'N':
                case 'n':
                    Forword();
                    if (HasLenChar("null".Length))
                    {
                        string strTrue = NextString("null".Length).ToLower();
                        if (strTrue == "null")
                        {
                            Forword("null".Length-1);
                            return new Token(ID++, TokenType.NULL, strTrue, rowindex, columnindex);
                        }
                    }
                    Back();
                    return ReadVar(c);
                case '-':
                    Forword(); 
                    decimal dvalue2 = ReadDecimal();
                    return new Token(ID++, dvalue2, rowindex, columnindex);
                default:
                    if (char.IsNumber(c))
                    {
                        Forword();
                        decimal dvalue = ReadDecimal();
                        return new Token(ID++, dvalue, rowindex, columnindex);
                    }
                    else if (char.IsLetter(c))
                    {
                        return ReadVar(c);
                    }
                    throw new Exception("error Row=" + rowindex + " Column=" + columnindex);
            }
        }
        public Token ReadVar(char c)
        {
            switch (c)
            {
                case '_':
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                    Forword();
                    string str = ReadVarText(c); 
                    return new Token(ID++, TokenType.STRING, str, rowindex, columnindex);
                default:
                    throw new Exception("error Row=" + rowindex + " Column=" + columnindex);

            }
        }
        public string ReadVarText(char c)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(c);
            while (HasNext())
            {
                c = NextChar();
                if (char.IsLetter(c))
                {
                    sb.Append(c);
                }
                else if (char.IsNumber(c))
                {
                    sb.Append(c);
                }
                else if (c == '_')
                {
                    sb.Append(c);
                }
                else
                {
                    break;
                }
                Forword();
            }
            return sb.ToString();
        }
        private char[] ReadChars(int count)
        {
            char[] cs = new char[count];
            for (int i = 0; i < count; i++)
            {
                cs[i] = text[position + i + 1];
            }
            return cs;
        }
        private void Forword(int count)
        {
            columnindex = columnindex + count;
            position = position + count;
        }
        private bool HasLenChar(int count)
        {
            return position + count < text.Length;
        }
        private string NextString(int count)
        {
            sb.Length = 0;
            for (int i = 0; i < count; i++)
            {
                sb.Append(text[position + i]);
            }
            return sb.ToString();
        }

        private char ReadNextChar()
        {
            Forword();
            if (IsEnd())
            {
                throw new Exception("error Row=" + rowindex + " Column=" + columnindex);
            }
            return text[position];
        }

        private bool HasNext()
        {
            return (position +1)< text.Length;
        }
        private char NextChar()
        {
            return text[position + 1];
        }
        private char currentChar { get {
            return text[position];
        } }
        private decimal ReadDecimal()
        {
            decimal d = decimal.Zero;
            sb.Length = 0;
            char c = currentChar;
            sb.Append(c); 
            while (!IsEnd())
            {
                if (HasNext())
                {
                    c = NextChar();
                    if (char.IsNumber(c))
                    {
                        Forword();
                        c = currentChar;
                        sb.Append(c);
                        continue;
                    }
                    break;
                }
            }
            if (HasNext())
            {
                c = NextChar();
                if (c == '.')
                {
                    Forword();
                    c = currentChar;
                    sb.Append(c);
                    c = ReadNextChar();
                    if (!char.IsNumber(c))
                    {
                        throw new Exception("number error row:" + rowindex + " column:" + columnindex);
                    }
                    sb.Append(c); 
                }
                while (!IsEnd())
                {
                    if (HasNext())
                    {
                        c = NextChar();
                        if (char.IsNumber(c))
                        {
                            Forword();
                            c = currentChar;
                            sb.Append(c);
                            continue;
                        }
                        break;
                    }
                } 
            }
            d = decimal.Parse(sb.ToString());
            return d;
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        private string ReadString()
        {
            sb.Length = 0;
            Forword();
            while (!IsEnd())
            {
                char c = text[position];
                if (c == '\\')
                {
                    char c2 = ReadNextChar();
                    switch (c2)
                    {
                        case 'b':
                            sb.Append('\b');
                            Forword();
                            break;
                        case 'n':
                            sb.Append('\n');
                            Forword();
                            break;
                        case 'r':
                            sb.Append('\r');
                            Forword();
                            break;
                        case 't':
                            sb.Append('\t');
                            Forword();
                            break;
                        case 'f':
                            sb.Append('\f');
                            Forword();
                            break;
                        case '\\':
                            sb.Append('\\');
                            Forword();
                            break;
                        case '\"':
                            sb.Append('\"');
                            Forword();
                            break;
                        case '/':
                            sb.Append(c2);
                            Forword();
                            break;
                        case 'u':
                            string s = "" + ReadNextChar();
                            s = s + ReadNextChar();
                            s = s + ReadNextChar();
                            s = s + ReadNextChar();
                            int uchar = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
                            sb.Append((char)uchar);
                            Forword();
                            break;
                        default:
                            sb.Append(c);
                            sb.Append(c2);
                            Forword();
                            break;
                    }
                    continue;
                }
                if (c == '\"')
                {
                    break;
                }
                Forword();
                sb.Append(c); 
            }
            string value = sb.ToString();
            return value; 
        }
        private string ReadString2()
        {
            sb.Length = 0;
            Forword();
            while (!IsEnd())
            {
                char c = text[position];
                if (c == '\\')
                {
                    char c2 = ReadNextChar();
                    switch (c2)
                    {
                        case '\b':
                            sb.Append('\b');
                            Forword();
                            break;
                        case '\n':
                            sb.Append('\n');
                            Forword();
                            break;
                        case '\r':
                            sb.Append('\r');
                            Forword();
                            break;
                        case '\t':
                            sb.Append('\t');
                            Forword();
                            break;
                        case '\f':
                            sb.Append('\f');
                            Forword();
                            break;
                        case '\\':
                            sb.Append('\\');
                            Forword();
                            break;
                        case '\"':
                            sb.Append('\"');
                            Forword();
                            break;
                        case '/':
                            sb.Append(c2);
                            Forword();
                            break;
                        case 'u':
                            string s = "" + ReadNextChar();
                            s = s + ReadNextChar();
                            s = s + ReadNextChar();
                            s = s + ReadNextChar();
                            int uchar = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
                            sb.Append((char)uchar);
                            Forword();
                            break;
                        default:
                            sb.Append(c);
                            sb.Append(c2);
                            Forword();
                            break;
                    }
                    continue;
                }
                if (c == '\'')
                {
                    break;
                }
                Forword();
                sb.Append(c);
            }
            string value = sb.ToString();
            return value;
        }
        private void ClearEmpty()
        {
            char c = ' ';
            while (HasNext())
            {
                c = NextChar();
                if (!char.IsWhiteSpace(c))
                {
                    break;
                }
                if (c == '\n')
                {
                    rowindex++;
                    columnindex = 0;
                }
                Forword(); 
            } 
        }
    }
}
