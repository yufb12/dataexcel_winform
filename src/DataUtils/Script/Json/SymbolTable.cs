using System.Collections.Generic;

namespace Feng.Json
{

    public class SymbolTable
    {
        public SymbolTable()
        {
            Tokens = new List<Token>();
        }
        //public Queue<Token> Tokens { get; set; }
        public List<Token> Tokens { get; set; }
        int position = 0;
        //[System.Diagnostics.DebuggerStepThrough]
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
        //[System.Diagnostics.DebuggerStepThrough]
        public Token Peek()
        {
            if (position < Tokens.Count)
            {
                Token token = Tokens[position];
                return token;
            }
            return Token.End;
        }
        //[System.Diagnostics.DebuggerStepThrough]
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
        public int Line { get; set; }
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Line:" + Line + " ");
            foreach (Token item in Tokens)
            {
                sb.Append(item.Value);
            }
            return sb.ToString();
        }
    }
}

