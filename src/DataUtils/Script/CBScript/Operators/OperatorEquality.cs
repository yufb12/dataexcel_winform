namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// equality_exp '==' relational_exp
    ///	equality_exp '!=' relational_exp
    /// </summary>
    public class OperatorEquality : OperatorBase
    {
        private static OperatorEquality instance = null;
        public static OperatorEquality Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorEquality();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {  
            switch (token.Value)
            {
                case "==":
                    return operatorexec.Equal(value1, value2);
                case "!=":
                    return operatorexec.NotEqual(value1, value2);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}