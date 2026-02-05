
using System.Collections.Generic;

namespace Feng.Script.CB
{
    public class TokenPool
    {
        public TokenPool()
        {
            Tokens = new List<Token>();
        }
        public List<Token> Tokens { get; set; }
        int position = 0;
        public Token Pop()
        {
            if (position < Tokens.Count)
            {
                Token token = Tokens[position];
                position++;
                return token;
            }
            return Token.End;
        }
        public Token Peek()
        {
            if (position < Tokens.Count)
            {
                Token token = Tokens[position];
                return token;
            }
            return Token.End;
        }
        public Token Back()
        {
            if (position < Tokens.Count)
            {
                Token token = Tokens[position];
                position--;
                return token;
            }
            return Token.End;
        }
        public void Push(Token token)
        {
            Tokens.Add(token);
        }
        public virtual void ReSetPosition()
        {
            position = 0;
        }
        public virtual int Count
        {
            get
            {
                return Tokens.Count;
            }
        }
        public bool HasNext()
        {
            return position < this.Tokens.Count;
        }
        public int Line { get; set; }
        public int TextLine { get; set; }
        public int TextColumn { get; set; }
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Line:" + Line + " ");
            for (int i = 0; i < Tokens.Count; i++)
            {
                Token item = Tokens[i];
                Token itemnext = null;
                if (i < (Tokens.Count - 1))
                {
                    itemnext = Tokens[i + 1];
                }
                if (itemnext != null)
                {
                    if (item.Type == TokenType.CONST_STRING)
                    {
                        sb.Append("\"" + item.Value + "\"");
                    }
                    else if (item.Type == TokenType.SignLeftParenthesis)
                    {
                        sb.Append(item.Value);
                    }
                    else if (item.Type == TokenType.SignRightParenthesis)
                    {
                        sb.Append(item.Value);
                    }
                    else if (item.Type == TokenType.ID && itemnext.Type == TokenType.ID)
                    {
                        sb.Append(item.Value + " ");
                    }
                    else
                    {
                        sb.Append(item.Value);
                    }
                }
                else
                {
                    sb.Append(item.Value);
                }

            }
            return sb.ToString();
        }
        public string GetText()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < Tokens.Count; i++)
            {
                Token item = Tokens[i];
                Token itemnext = null;
                if (i < (Tokens.Count - 1))
                {
                    itemnext = Tokens[i + 1];
                }
                if (itemnext != null)
                {
                    if (item.Type == TokenType.CONST_STRING)
                    {
                        sb.Append("\"" + item.Value + "\"");
                    }
                    else if (item.Type == TokenType.SignLeftParenthesis)
                    {
                        sb.Append(item.Value);
                    }
                    else if (item.Type == TokenType.SignRightParenthesis)
                    {
                        sb.Append(item.Value);
                    }
                    else if (item.Type == TokenType.ID && itemnext.Type == TokenType.ID)
                    {
                        sb.Append(item.Value + " ");
                    }
                    else
                    {
                        sb.Append(item.Value);
                    }
                }
                else
                {
                    if (item.Type == TokenType.CONST_STRING)
                    {
                        sb.Append("\"" + item.Value + "\"");
                    }
                    else if (item.Type == TokenType.SignLeftParenthesis)
                    {
                        sb.Append(item.Value);
                    }
                    else if (item.Type == TokenType.SignRightParenthesis)
                    {
                        sb.Append(item.Value);
                    }
                    else
                    {
                        sb.Append(item.Value);
                    }
                }

            }
            return sb.ToString();
        }
    }
}
