using System;

namespace Feng.Script.CBEexpress
{
    public class Token
    { 
        public ScriptFile ScriptFile { get; private set; }
        public int Index { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public int Line { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public static readonly Token End = new Token(null,-1, -1, string.Empty, -1, -1, -1);
        public Token(ScriptFile scriptfile,int id, int type, string value, int line, int startindex, int endindex)
        {
            this.ScriptFile = scriptfile;
            this.Index = id;
            this.Type = type;
            this.Value = value;
            this.Line = line;
            this.StartIndex = startindex;
            this.EndIndex = endindex;
        }

        internal string GetPosition()
        {
            if (ScriptFile==null)
                return string.Format("Value={0},Line={1},Position={2}", Value, Line, EndIndex);
            return string.Format("Value={0},Line={1},Position={2},File={3}", Value, Line, EndIndex, ScriptFile.Path);
        }
 
        public override string ToString()
        {
            return string.Format("Index={0},Type={1},Value=【{2}】,Line={3},Column={4},Position={5}", Index, Type, Value, Line, StartIndex, EndIndex);
        }

    }
}
