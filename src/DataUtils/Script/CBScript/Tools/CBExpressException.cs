using System;

namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// &&
    /// </summary>
    public class CBExpressException : Exception
    {
        private CBExpressException()
        {
        }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public Token Token { get; set; }
        public CBExpressException(string msg,Token token):base(msg)
        {
            Token = token;
        }
        public CBExpressException(string msg, Token token,Exception ex) : base(msg, ex)
        {
            Token = token;
        }
    }
}