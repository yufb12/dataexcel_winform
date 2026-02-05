using System;

namespace Feng.Script.CBEexpress
{
    public class TokenText:Token
    {
        public TokenText(int id, int type, string value, int line, int column,int position)
            : base(id, type, value, line, column, position)
        {

        }

        public override string ToString()
        {
            return string.Format("Index={0},Type={1},Value=【{2}】,Line={3},Column={4}", Index, Type, Value, Line, Column);
        }
    }
}
