using System;

namespace Feng.Script.CBEexpress
{
    public class Token
    {
        public int Index { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public int Position { get; set; }
        public static readonly Token End = new Token(-1, -1, string.Empty, -1, -1, -1);
        public Token(int id, int type, string value, int line, int column,int postion)
        {
            this.Index = id;
            this.Type = type;
            this.Value = value;
            this.Line = line;
            this.Column = column;
            this.Position = postion;
        }
 
        public override string ToString()
        {
            return string.Format("Index={0},Type={1},Value=【{2}】,Line={3},Column={4},Position={5}", Index, Type, Value, Line, Column,Position);
        }
 
    }
}
