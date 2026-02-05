using System;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementLexer
    {
        public StatementLexer()
        {

        }
        public StatementLexer(string txt)
        {
            text = txt;
        }
        public List<SymbolTable> List = new List<SymbolTable>();
        private string text = string.Empty;
        private int position = 0;
        private SymbolTable table = null;
        private const char empty = ' ';
        private int rowindex = 0;
        private int columnindex = 0;
        private int symboltableindex = 0;
        private StatementLexer lexer = null;
        public SymbolTable GetSymbolTable(string text)
        {
            if (lexer == null)
            {
                lexer = new StatementLexer();
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
                if (c == '\n')
                {
                    rowindex++;
                }

                Token token = GetToken(c);
                if (token.Type == TokenType.Comments)
                    continue;
                if (token.Type == TokenType.SameLine)
                    continue;
                if (token.Type == TokenType.NewLine || token.Type == TokenType.NewLineEnter)
                {
                    if (table.Count > 0)
                    {
                        table.Line = symboltableindex;
                        symboltableindex++;
                        List.Add(table);
                    }
                    table = GetSymbolTable(table);
                    table.TextLine = token.Line;
                    continue;
                }

                if (token.Type == TokenType.LBRACE || token.Type == TokenType.RBRACE)
                {
                    if (table.Count > 0)
                    {
                        table.Line = symboltableindex;
                        symboltableindex++;
                        List.Add(table);
                    }
                    table = GetSymbolTable(table);
                    table.TextLine = token.Line;
                    table.Push(token);
                    symboltableindex++;
                    List.Add(table);

                    table = GetSymbolTable(table);
                    table.TextLine = token.Line;
                    table.TextColumn = token.Column;
                    continue;
                }
                table.Push(token);
            }
            if (table.Count > 0)
            {
                List.Add(table);
            }
            return table;
        }
        public string Format(string text)
        {
            SymbolTable table = Parse(text);
            return table.ToString();
        }
        public Token GetToken()
        {
            position = -1;
            rowindex = 0;
            columnindex = 0;
            if (!IsEnd())
            {
                ClearEmpty();
                if (!HasNext())
                {
                    return Token.End;
                }
                char c = NextChar();
                Token token = GetToken(c);
                return token;
            }
            return Token.End;
        }

        public void Forword()
        {
            columnindex++;
            position++;
        }
        public virtual SymbolTable GetSymbolTable(SymbolTable prevtable)
        {
            return new SymbolTable();
        }
        public void Back()
        {
            columnindex--;
            position--;
        }
        public bool IsEnd()
        {
            if (string.IsNullOrWhiteSpace(text))
                return true;
            return !(position < text.Length - 1);
        }
        private int ID = 0;
        private Token GetToken(char c)
        {
            switch (c)
            {
                case '\"':
                    Forword();
                    string value = ReadString();
                    return NewToken(ID++, TokenType.CONST_STRING, value, rowindex, columnindex, this.position);
                case '{':
                    Forword();
                    return NewToken(ID++, TokenType.LBRACE, "{", rowindex, columnindex, this.position);
                case '}':
                    Forword();
                    return NewToken(ID++, TokenType.RBRACE, "}", rowindex, columnindex, this.position);

                case '(':
                    Forword();
                    return NewToken(ID++, TokenType.function_exp_begin, "(", rowindex, columnindex, this.position);
                case ')':
                    Forword();
                    return NewToken(ID++, TokenType.function_exp_end, ")", rowindex, columnindex, this.position);
                case ',':
                    Forword();
                    return NewToken(ID++, TokenType.argument_exp_list, ",", rowindex, columnindex, this.position);
                case ';':
                    Forword();
                    return NewToken(ID++, TokenType.NewLine, ";", rowindex, columnindex, this.position);
                case '；':
                    Forword();
                    return NewToken(ID++, TokenType.NewLine, ";", rowindex, columnindex, this.position);
                case '\n':
                    Forword();
                    return NewToken(ID++, TokenType.NewLineEnter, ";", rowindex, columnindex, this.position);
                case '*':
                    Forword();
                    return NewToken(ID++, TokenType.mult_exp, "*", rowindex, columnindex, this.position);
                case '/':
                    Forword();
                    if (NextChar() == '/')
                    {
                        Forword();
                        string valueComments = ReadComments();
                        return NewToken(ID++, TokenType.Comments, valueComments, rowindex, columnindex, this.position);
                    }
                    return NewToken(ID++, TokenType.mult_exp, "/", rowindex, columnindex, this.position);
                case '%':
                    Forword();
                    return NewToken(ID++, TokenType.mult_exp, "%", rowindex, columnindex, this.position);
                case '^':
                    Forword();
                    return NewToken(ID++, TokenType.exclusive_or_exp, "^", rowindex, columnindex, this.position);
                case '~':
                    Forword();
                    return NewToken(ID++, TokenType.unary_exp, "~", rowindex, columnindex, this.position);
                case '|':
                    Forword();
                    if (NextChar() == '|')
                    {
                        Forword();
                        return NewToken(ID++, TokenType.logical_or_exp, "||", rowindex, columnindex, this.position);
                    }

                    return NewToken(ID++, TokenType.inclusive_or_exp, "|", rowindex, columnindex, this.position);
                case '&':
                    Forword();
                    if (NextChar() == '&')
                    {
                        Forword();
                        return NewToken(ID++, TokenType.logical_and_exp, "&&", rowindex, columnindex, this.position);
                    }

                    return NewToken(ID++, TokenType.and_exp, "&", rowindex, columnindex, this.position);
                case '+':
                    Forword();
                    //if (NextChar() == '+')
                    //{
                    //    Forword();
                    //    return NewToken(ID++, TokenType.unary_exp, "++", rowindex, columnindex, this.position);
                    //}

                    return NewToken(ID++, TokenType.additive_exp, "+", rowindex, columnindex, this.position);
                case '-':
                    Forword();
                    //if (NextChar() == '-')
                    //{
                    //    Forword();
                    //    return NewToken(ID++, TokenType.unary_exp, "--", rowindex, columnindex, this.position);
                    //}
                    //else if (char.IsNumber(c))
                    //{
                    //    Forword();
                    //    string dvalue = "-" + ReadDecimal();
                    //    return NewToken(ID++, TokenType.CONST_NUMBER, dvalue, rowindex, columnindex, this.position);
                    //}
                    return NewToken(ID++, TokenType.additive_exp, "-", rowindex, columnindex, this.position);

                case '!':
                    Forword();
                    if (NextChar() == '=')
                    {
                        Forword();
                        return NewToken(ID++, TokenType.equality_exp, "!=", rowindex, columnindex, this.position);
                    }

                    return NewToken(ID++, TokenType.unary_exp, "!", rowindex, columnindex, this.position);
                case '=':
                    Forword();
                    if (NextChar() == '=')
                    {
                        Forword();
                        return NewToken(ID++, TokenType.equality_exp, "==", rowindex, columnindex, this.position);
                    }
                    return NewToken(ID++, TokenType.equality, "=", rowindex, columnindex, this.position);
                case '>':
                    Forword();
                    if (NextChar() == '=')
                    {
                        Forword();
                        return NewToken(ID++, TokenType.relational_exp, ">=", rowindex, columnindex, this.position);
                    }
                    if (NextChar() == '>')
                    {
                        Forword();
                        return NewToken(ID++, TokenType.shift_expression, ">>", rowindex, columnindex, this.position);
                    }
                    return NewToken(ID++, TokenType.relational_exp, ">", rowindex, columnindex, this.position);
                case '<':
                    Forword();
                    if (NextChar() == '=')
                    {
                        Forword();
                        return NewToken(ID++, TokenType.relational_exp, "<=", rowindex, columnindex, this.position);
                    }
                    if (NextChar() == '<')
                    {
                        Forword();
                        return NewToken(ID++, TokenType.shift_expression, "<<", rowindex, columnindex, this.position);
                    }

                    return NewToken(ID++, TokenType.relational_exp, "<", rowindex, columnindex, this.position);
                case 'F':
                case 'f':
                    Forword();
                    if (HasLenChar("false".Length))
                    {
                        string strTrue = NextString("false".Length).ToLower();
                        if (strTrue == "false")
                        {
                            Forword("false".Length - 1);
                            return NewToken(ID++, TokenType.FALSE, strTrue, rowindex, columnindex, this.position);
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
                            Forword("true".Length - 1);
                            return NewToken(ID++, TokenType.TRUE, strTrue, rowindex, columnindex, this.position);
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
                            Forword("null".Length - 1);
                            return NewToken(ID++, TokenType.NULL, strTrue, rowindex, columnindex, this.position);
                        }
                    }
                    Back();
                    return ReadVar(c);
                case '\\':
                    Forword();
                    ClearEmpty();
                    if (NextChar() == '\n')
                    {
                        Forword();
                        while ((NextChar() == '\n'))
                        {
                            Forword();
                        }
                        return NewToken(ID++, TokenType.SameLine, "\\", rowindex, columnindex, this.position);
                    }
                    throw new Feng.Script.CBEexpress.CBExpressException("" + GetErrorText() + " Has error Row=" + rowindex + " Column=" + columnindex,
            NewToken(-1, TokenType.ERROR, c.ToString(), rowindex, columnindex, position))
                    { RowIndex = rowindex, ColumnIndex = columnindex };
                default:
                    if (char.IsNumber(c))
                    {
                        Forword();
                        string dvalue = ReadDecimal();
                        return NewToken(ID++, TokenType.CONST_NUMBER, dvalue, rowindex, columnindex, this.position);
                    }
                    return ReadVar(c);
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
                    string str = ReadVarText(c).ToUpper();
                    Token token = IDToken(str);
                    if (token != null)
                    {
                        return token;
                    }
                    return NewToken(ID++, TokenType.ID, str, rowindex, columnindex, this.position);
                default:
                    throw new Feng.Script.CBEexpress.CBExpressException("语法错误:【" + GetErrorText() + "】 后输入字符【"+c.ToString () + "】不正确行=" + rowindex + " 列=" + columnindex,
                        NewToken(-1, TokenType.ERROR, c.ToString(), rowindex, columnindex, position))
                    { RowIndex = rowindex, ColumnIndex = columnindex };

            }
        }
        public virtual Token IDToken(string str)
        {
            switch (str)
            {
                case "TRUE":
                    return NewToken(ID++, TokenType.TRUE, str, rowindex, columnindex, this.position);
                case "FALSE":
                    return NewToken(ID++, TokenType.FALSE, str, rowindex, columnindex, this.position);
                case "THIS":
                    return NewToken(ID++, TokenType.THIS, str, rowindex, columnindex, this.position);
                case "NULL":
                    return NewToken(ID++, TokenType.NULL, str, rowindex, columnindex, this.position);
                default:
                    break;
            }
            return null;
        }
        public Token Token1 { get; set; }
        public Token Token2 { get; set; }
        public Token Token3 { get; set; }
        public Token Token4 { get; set; }
        public Token Token5 { get; set; }
        public Token Token6 { get; set; }
        public Token Token7 { get; set; }
        public Token Token8 { get; set; }
        public Token Token9 { get; set; }
        public Token Token10 { get; set; }
        public Token NewToken(int id, int type, string value, int line, int column, int postion)
        {
            Token token = new Token(id, type, value, line, column, postion);

            Token10 = Token9;
            Token9 = Token8;
            Token8 = Token7;
            Token7 = Token6;
            Token6 = Token5;
            Token5 = Token4;
            Token4 = Token3;
            Token3 = Token2;
            Token2 = Token1;
            Token1 = token;
            return token;

        }
        public string GetTokenValue(Token token)
        {
            if (token == null)
                return string.Empty;
            if (token.Value == null)
                return string.Empty;
            if (token.Type == TokenType.CONST_STRING)
            {
                return "\""+token.Value+"\"";
            }
            return token.Value;
        }
        private string GetErrorText()
        {
            string text = GetTokenValue(Token10) 
                + GetTokenValue(Token9) 
                + GetTokenValue(Token8) 
                + GetTokenValue(Token7) 
                + GetTokenValue(Token6) 
                + GetTokenValue(Token5) 
                + GetTokenValue(Token4) 
                + GetTokenValue(Token3) 
                + GetTokenValue(Token2) 
                + GetTokenValue(Token1);
            return  text.Trim();
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
                throw new Feng.Script.CBEexpress.CBExpressException(""+GetErrorText()+" Has error Row=" + rowindex + " Column=" + columnindex,
     NewToken(-1, TokenType.ERROR, string.Empty, rowindex, columnindex, position))
                { RowIndex = rowindex, ColumnIndex = columnindex };
            }
            return currentChar;
        }
        private bool HasNext()
        {
            return position < text.Length - 1;
        }
        private char NextChar()
        {
            if (position >= text.Length)
            {
                return ' ';
            }
            return text[position + 1];
        }
        private char currentChar
        {
            get
            {
                return text[position];
            }
        }
        private string ReadDecimal()
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
                        throw new Feng.Script.CBEexpress.CBExpressException("" + GetErrorText() + " Has error Row=" + rowindex + " Column=" + columnindex,
    NewToken(-1, TokenType.ERROR, string.Empty, rowindex, columnindex, position))
                        { RowIndex = rowindex, ColumnIndex = columnindex };
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
            return sb.ToString();
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        private string ReadString()
        {
            sb.Length = 0;
            Forword();
            while (!IsEnd())
            {
                char c = currentChar;
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
        private string ReadComments()
        {
            sb.Length = 0;
            Forword();
            while (!IsEnd())
            {
                char c = currentChar;
                if (c == '\\')
                {
                    char c2 = ReadNextChar();
                    switch (c2)
                    {
                        case '\b':
                            sb.Append('\b');
                            break;
                        case '\n':
                            sb.Append('\n');
                            break;
                        case '\r':
                            sb.Append('\r');
                            break;
                        case '\t':
                            sb.Append('\t');
                            break;
                        case '\f':
                            sb.Append('\f');
                            break;
                        case '\\':
                            sb.Append('\\');
                            break;
                        case '\"':
                            sb.Append('\"');
                            break;
                        case '/':
                            sb.Append(c2);
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
                            break;
                    }
                    continue;
                }
                if (c == '\n')
                {
                    rowindex++;
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
                    break;
                }
                Forword();
            }
        }
    }
}
