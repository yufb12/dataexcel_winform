
using System.Text;

namespace Feng.Script.CB
{
    public class Lexer
    {
        private Lexer()
        {

        }
        public Lexer(string txt)
        {
            text = txt;
            ScriptFile = new ScriptFile() { Contents = txt, Path = string.Empty };
        }
        public TokenPool TokenPool { get; private set; }
        public ScriptFile ScriptFile { get; private set; }
        private string text = string.Empty;
        public string GetText()
        {
            return text;
        }
        private int position = -1;
        public void Parse()
        {
            TokenPool = new TokenPool();
            while (this.HasNext())
            {
                ClearEmpty();
                if (!HasNext())
                {
                    break;
                }
                char c = Peek();
                Token token = GetToken(c);
                if (token.Type == TokenType.SignComment)
                    continue;
                TokenPool.Push(token);
            }
        }
        private Token GetToken()
        {
            position = -1;
            if (this.HasNext())
            {
                ClearEmpty();
                if (!HasNext())
                {
                    return Token.End;
                }
                char c = Peek();
                Token token = GetToken(c);
                return token;
            }
            return Token.End;
        }
        private char Pop()
        {
            position++;
            return text[position];
        }
        private void Back()
        {
            position--;
        }

        private int ID = 0;

        private int rowindex = 0;
        private Token GetToken(char c)
        {
            int startindex = this.position;
            switch (c)
            {
                case '\"':
                    Pop();
                    string value = ReadString();
                    return NewToken(ID++, TokenType.CONST_STRING, value, rowindex, startindex, this.position);
                case '{':
                    Pop();
                    return NewToken(ID++, TokenType.SignLeftBRACE, "{", rowindex, startindex, this.position);
                case '}':
                    Pop();
                    return NewToken(ID++, TokenType.SignRightBRACE, "}", rowindex, startindex, this.position);

                case '(':
                    Pop();
                    return NewToken(ID++, TokenType.SignLeftParenthesis, "(", rowindex, startindex, this.position);

                case '（':
                    Pop();
                    return NewToken(ID++, TokenType.SignLeftParenthesis, "(", rowindex, startindex, this.position);
                case ')':
                    Pop();
                    return NewToken(ID++, TokenType.SignRightParenthesis, ")", rowindex, startindex, this.position);
                case '）':
                    Pop();
                    return NewToken(ID++, TokenType.SignRightParenthesis, ")", rowindex, startindex, this.position);
                case ',':
                    Pop();
                    return NewToken(ID++, TokenType.SignComma, ",", rowindex, startindex, this.position);
                case ';':
                    Pop();
                    return NewToken(ID++, TokenType.SignSemicolon, ";", rowindex, startindex, this.position);
                case '；':
                    Pop();
                    return NewToken(ID++, TokenType.SignSemicolon, ";", rowindex, startindex, this.position);
                //case '\n':
                //    Pop();
                //    return NewToken(ID++, TokenType.NewLineEnter, ";", rowindex, startindex, this.position);
                case '*':
                    Pop();
                    return NewToken(ID++, TokenType.SignMultiplied, "*", rowindex, startindex, this.position);
                case '/':
                    Pop();
                    if (Peek() == '/')
                    {
                        Pop();
                        string valueComments = ReadComments();
                        return NewToken(ID++, TokenType.SignComment, valueComments, rowindex, startindex, this.position);
                    }
                    return NewToken(ID++, TokenType.SignDivided, "/", rowindex, startindex, this.position);
                case '%':
                    Pop();
                    return NewToken(ID++, TokenType.SignModulus, "%", rowindex, startindex, this.position);
                case '^':
                    Pop();
                    throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "不支持的操作符,使用函数代替:" + c, CBEexpressExCode.ERRORCODE_11109);
                    return NewToken(ID++, TokenType.SingXOR, "^", rowindex, startindex, this.position);
                case '~':
                    Pop();
                    throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "不支持的操作符,使用函数代替:" + c, CBEexpressExCode.ERRORCODE_11109);
                    return NewToken(ID++, TokenType.SignbitwiseNOT, "~", rowindex, startindex, this.position);
                case '|':
                    Pop();
                    if (Peek() == '|')
                    {
                        Pop();
                        return NewToken(ID++, TokenType.SignlogicalOR, "||", rowindex, startindex, this.position);
                    }
                    throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "不支持的操作符,使用函数代替:" + c, CBEexpressExCode.ERRORCODE_11109);
                    return NewToken(ID++, TokenType.SignbitwiseOR, "|", rowindex, startindex, this.position);
                case '&':
                    Pop();
                    if (Peek() == '&')
                    {
                        Pop();
                        return NewToken(ID++, TokenType.SignlogicalAND, "&&", rowindex, startindex, this.position);
                    }
                    throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "不支持的操作符,使用函数代替:" + c, CBEexpressExCode.ERRORCODE_11109);
                    return NewToken(ID++, TokenType.SignbitwiseAND, "&", rowindex, startindex, this.position);
                case '+':
                    Pop();
                    if (Peek() == '+')
                    {
                        Pop();
                        return NewToken(ID++, TokenType.SignIncrement, "++", rowindex, startindex, this.position);
                    }

                    return NewToken(ID++, TokenType.SignPlus, "+", rowindex, startindex, this.position);
                case '-':
                    Pop();
                    if (Peek() == '-')
                    {
                        Pop();
                        return NewToken(ID++, TokenType.SignDecrement, "--", rowindex, startindex, this.position);
                    }
                    return NewToken(ID++, TokenType.SignMinus, "-", rowindex, startindex, this.position);

                case '!':
                    Pop();
                    if (Peek() == '=')
                    {
                        Pop();
                        return NewToken(ID++, TokenType.SignNotEqualTo, "!=", rowindex, startindex, this.position);
                    }

                    return NewToken(ID++, TokenType.SignlogicalNOT, "!", rowindex, startindex, this.position);
                case '=':
                    Pop();
                    if (Peek() == '=')
                    {
                        Pop();
                        return NewToken(ID++, TokenType.SignEqualTo, "==", rowindex, startindex, this.position);
                    }
                    return NewToken(ID++, TokenType.SignEuality, "=", rowindex, startindex, this.position);
                case '>':
                    Pop();
                    if (Peek() == '=')
                    {
                        Pop();
                        return NewToken(ID++, TokenType.SignRelationalGreaterThanOrEqual, ">=", rowindex, startindex, this.position);
                    }
                    if (Peek() == '>')
                    {
                        Pop();
                        throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "不支持的操作符,使用函数代替:" + c, CBEexpressExCode.ERRORCODE_11109);
                        return NewToken(ID++, TokenType.SignShiftRight, ">>", rowindex, startindex, this.position);
                    }
                    return NewToken(ID++, TokenType.SignRelationalGreaterThan, ">", rowindex, startindex, this.position);
                case '<':
                    Pop();
                    if (Peek() == '=')
                    {
                        Pop();
                        return NewToken(ID++, TokenType.SignRelationalLessThan, "<=", rowindex, startindex, this.position);
                    }
                    if (Peek() == '<')
                    {
                        Pop();
                        throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "不支持的操作符,使用函数代替:" + c, CBEexpressExCode.ERRORCODE_11109);
                        return NewToken(ID++, TokenType.SignShiftLeft, "<<", rowindex, startindex, this.position);
                    }

                    return NewToken(ID++, TokenType.SignRelationalLessThan, "<", rowindex, startindex, this.position);
                case 'F':
                case 'f':
                    Pop();
                    if (HasLenChar("false".Length))
                    {
                        string strTrue = NextString("false".Length).ToLower();
                        if (strTrue == "false")
                        {
                            Forword("false".Length - 1);
                            return NewToken(ID++, TokenType.CONST_FALSE, strTrue, rowindex, startindex, this.position);
                        }
                    }
                    Back();
                    return ReadVar(c);
                case 'T':
                case 't':
                    Pop();
                    if (HasLenChar("true".Length))
                    {
                        string strTrue = NextString("true".Length).ToLower();
                        if (strTrue == "true")
                        {
                            Forword("true".Length - 1);
                            return NewToken(ID++, TokenType.CONST_TRUE, strTrue, rowindex, startindex, this.position);
                        }
                    }
                    Back();
                    return ReadVar(c);
                case 'N':
                case 'n':
                    Pop();
                    if (HasLenChar("null".Length))
                    {
                        string strTrue = NextString("null".Length).ToLower();
                        if (strTrue == "null")
                        {
                            Forword("null".Length - 1);
                            return NewToken(ID++, TokenType.CONST_NULL, strTrue, rowindex, startindex, this.position);
                        }
                    }
                    Back();
                    return ReadVar(c);
                default:
                    if (char.IsNumber(c))
                    {
                        string dvalue = ReadDecimal();
                        return NewToken(ID++, TokenType.CONST_NUMBER, dvalue, rowindex, startindex, this.position);
                    }
                    return ReadVar(c);
            }
        }
        private Token ReadVar(char c)
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
                    Pop();

                    int startindex = this.position;
                    string str = ReadVarText(c);
                    Token token = IDToken(str, startindex);
                    if (token != null)
                    {
                        return token;
                    }
                    int typeid = 0;
                    if (IsKeyID(str, out typeid))
                    {
                        return NewToken(ID++, typeid, str, rowindex, startindex, this.position);
                    }
                    return NewToken(ID++, TokenType.ID, str, rowindex, startindex, this.position);
                default:
                    throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "输入的字符不正确:" + c, CBEexpressExCode.ERRORCODE_11109);

            }
        }
        private bool IsKeyID(string id, out int type)
        {
            type = -1;
            string ID = id.ToUpper();
            //public const int Key_THIS = 9;         
            //public const int Key_ME = 10;          
            //public const int Key_FOREACH = 21;     
            //public const int Key_FOR = 22;         
            //public const int Key_WHILE = 23;       
            //public const int Key_VAR = 24;         
            //public const int Key_IF = 25;          
            //public const int Key_ELSE = 26;        
            //public const int Key_CONTINUE = 27;    
            //public const int Key_BREAK = 28;       
            //public const int Key_RETURN = 29;      
            //public const int Key_FUNCTION = 30;    
            //public const int Key_IN = 31;          
            switch (ID)
            {
                case "BREAK":
                    type = TokenType.Key_BREAK;
                    return true;
                case "CONTINUE":
                    type = TokenType.Key_CONTINUE;
                    return true;
                case "ELSE":
                    type = TokenType.Key_ELSE;
                    return true;
                case "FOR":
                    type = TokenType.Key_FOR;
                    return true;
                case "FOREACH":
                    type = TokenType.Key_FOREACH;
                    return true;
                case "FUNCTION":
                    type = TokenType.Key_FUNCTION;
                    return true;
                case "IF":
                    type = TokenType.Key_IF;
                    return true;
                case "IN":
                    type = TokenType.Key_IN;
                    return true;
                case "ME":
                    type = TokenType.Key_ME;
                    return true;
                case "RETURN":
                    type = TokenType.Key_RETURN;
                    return true;
                case "THIS":
                    type = TokenType.Key_THIS;
                    return true;
                case "VAR":
                    type = TokenType.Key_VAR;
                    return true;
                case "WHILE":
                    type = TokenType.Key_WHILE;
                    return true;
                case "OBJECT":
                    type = TokenType.Key_Object;
                    return true;
                case "RPC":
                    type = TokenType.Key_RPC;
                    return true;
                case "INCLUDE":
                    type = TokenType.Key_Include;
                    return true;
                case "TRY":
                    type = TokenType.Key_Try;
                    return true;
                case "CATCH":
                    type = TokenType.Key_Catch;
                    return true;
                case "THROW":
                    type = TokenType.Key_Throw;
                    return true;
                default:
                    break;
            }
            return false;
        }
        private Token IDToken(string str, int startindex)
        {
            switch (str)
            {
                case "TRUE":
                    return NewToken(ID++, TokenType.CONST_TRUE, str, rowindex, startindex, this.position);
                case "FALSE":
                    return NewToken(ID++, TokenType.CONST_FALSE, str, rowindex, startindex, this.position);
                case "THIS":
                    return NewToken(ID++, TokenType.Key_THIS, str, rowindex, startindex, this.position);
                case "NULL":
                    return NewToken(ID++, TokenType.CONST_NULL, str, rowindex, startindex, this.position);
                case "FOREACH":
                    return NewToken(ID++, TokenType.Key_FOREACH, str, rowindex, startindex, this.position);

                case "FOR":
                    return NewToken(ID++, TokenType.Key_FOR, str, rowindex, startindex, this.position);
                case "WHILE":
                    return NewToken(ID++, TokenType.Key_WHILE, str, rowindex, startindex, this.position);
                case "VAR":
                    return NewToken(ID++, TokenType.Key_VAR, str, rowindex, startindex, this.position);
                case "IF":
                    return NewToken(ID++, TokenType.Key_IF, str, rowindex, startindex, this.position);
                case "ELSE":
                    return NewToken(ID++, TokenType.Key_ELSE, str, rowindex, startindex, this.position);
                case "CONTINUE":
                    return NewToken(ID++, TokenType.Key_CONTINUE, str, rowindex, startindex, this.position);
                case "BREAK":
                    return NewToken(ID++, TokenType.Key_BREAK, str, rowindex, startindex, this.position);
                case "RETURN":
                    return NewToken(ID++, TokenType.Key_RETURN, str, rowindex, startindex, this.position);
                case "ME":
                    return NewToken(ID++, TokenType.Key_ME, str, rowindex, startindex, this.position);
                case "OBJECT":
                    return NewToken(ID++, TokenType.Key_Object, str, rowindex, startindex, this.position);
                case "FUNCTION":
                    return NewToken(ID++, TokenType.Key_FUNCTION, str, rowindex, startindex, this.position);
                case "RPC":
                    return NewToken(ID++, TokenType.Key_RPC, str, rowindex, startindex, this.position);
                case "INCLUDE":
                    return NewToken(ID++, TokenType.Key_Include, str, rowindex, startindex, this.position);
                case "TRY":
                    return NewToken(ID++, TokenType.Key_Try, str, rowindex, startindex, this.position);
                case "CATCH":
                    return NewToken(ID++, TokenType.Key_Catch, str, rowindex, startindex, this.position);
                default:
                    break;
            }
            return null;
        }
        private Token NewToken(int id, int type, string value, int line, int start, int end)
        {
            Token token = new Token(this.ScriptFile, id, type, value, line, start, end);
            return token;
        }

        private string GetErrorText()
        {
            string text = string.Empty;
            return text.Trim();
        }
        private string ReadVarText(char c)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(c);
            while (HasNext())
            {
                c = Peek();
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
                Pop();
            }
            return sb.ToString();
        }
        private void Forword(int count)
        {
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

        private bool HasNext()
        {
            return position < text.Length - 1;
        }
        private char Peek()
        {
            if (position >= text.Length)
            {
                return ' ';
            }
            return text[position + 1];
        }
        private string ReadDecimal()
        {
            int startindex = this.position;
            decimal d = decimal.Zero;
            sb.Length = 0;
            char c = this.Pop();
            sb.Append(c);
            while (this.HasNext())
            {
                c = Peek();
                if (char.IsNumber(c))
                {
                    c = Pop();
                    sb.Append(c);
                    continue;
                }
                break;
            }
            if (HasNext())
            {
                c = Peek();
                if (c == '.')
                {
                    c = Pop();
                    sb.Append(c);
                    if (!this.HasNext())
                        throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "输入的数字不正确:" + c, CBEexpressExCode.ERRORCODE_11111);

                    c = this.Peek();
                    if (!char.IsNumber(c))
                    {
                        throw new SyntacticException(new Token(this.ScriptFile, -1, TokenType.ID, c.ToString(), rowindex, this.position - 1, this.position), "输入的数字不正确:" + c, CBEexpressExCode.ERRORCODE_11112);
                    }
                    c = Pop();
                    sb.Append(c);
                    while (this.HasNext())
                    {
                        c = Peek();
                        if (char.IsNumber(c))
                        {
                            c = Pop();
                            sb.Append(c);
                            continue;
                        }
                        break;
                    }
                }

            }
            d = decimal.Parse(sb.ToString());
            return sb.ToString();
        }
        private StringBuilder sb = new StringBuilder();
        private string ReadString()
        {
            sb.Length = 0;
            while (this.HasNext())
            {
                char c = this.Pop();
                if (c == '\"')
                {
                    break;
                }
                if (c == '\\')
                {
                    char c2 = this.Peek();
                    switch (c2)
                    {
                        case '\b':
                            sb.Append('\b');
                            Pop();
                            break;
                        case '\n':
                            sb.Append('\n');
                            Pop();
                            break;
                        case '\r':
                            sb.Append('\r');
                            Pop();
                            break;
                        case '\t':
                            sb.Append('\t');
                            Pop();
                            break;
                        case '\f':
                            sb.Append('\f');
                            Pop();
                            break;
                        case '\\':
                            sb.Append('\\');
                            Pop();
                            break;
                        case '\"':
                            sb.Append('\"');
                            Pop();
                            break;
                        case '/':
                            sb.Append(c2);
                            Pop();
                            break;
                        default:
                            sb.Append(c);
                            break;
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }
            string value = sb.ToString();
            return value;
        }
        private string ReadComments()
        {
            sb.Length = 0;
            Pop();
            while (this.HasNext())
            {
                char c = this.Pop();
                if (c == '\n')
                {
                    break;
                }
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
                c = Peek();
                if (c == '\n')
                {
                    rowindex++;
                }
                if (!char.IsWhiteSpace(c))
                {
                    break;
                }
                Pop();
            }
        }

    }
}
