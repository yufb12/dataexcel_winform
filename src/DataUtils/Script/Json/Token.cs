using System;
using System.Collections.Generic;
using System.Linq; 

namespace Feng.Json
{
    public class Token
    {
        public int Index { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public static readonly Token End = new Token(-1, -1, string.Empty, -1, -1);
        public Token(int index, int type, int line, int column)
        {
            this.Index = index;
            this.Type = type;
            this.Value = "" + (char)type;
            this.Line = line;
            this.Column = column;
        }
        public Token(int index, string value, int line, int column)
        {
            this.Index = index;
            this.Type = TokenType.STRING;
            this.Value = value;
            this.Line = line;
            this.Column = column;
        }
        public Token(int index, int type, string value, int line, int column)
        {
            this.Index = index;
            this.Type = type;
            this.Value = value;
            this.Line = line;
            this.Column = column;
        }
        public Token(int index, decimal value, int line, int column)
        {
            this.Index = index;
            this.Type = TokenType.NUMBER;
            this.Value = value.ToString();
            this.Line = line;
            this.Column = column;
        }
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3},{4}", Index, Type, Value, Line, Column);
        }
        public object ToValue()
        {
            if (Type == TokenType.FALSE)
            {
                return false;
            } 
            if (Type == TokenType.TRUE)
            {
                return true;
            }
            if (Type == TokenType.NULL)
            {
                return null;
            }
            if (Type == TokenType.NUMBER)
            {
                return Convert.ToDecimal(Value);
            }
            if (Type == TokenType.STRING)
            {
                return (Value);
            }
            return null;
        }
    }
}
