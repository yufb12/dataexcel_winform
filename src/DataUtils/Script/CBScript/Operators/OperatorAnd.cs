namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// and_exp '&' equality_exp  &	按位与	整型表达式&整型表达式	左到右	双目运算符
    /// </summary>
    public class OperatorAnd : OperatorBase
    {
        private static OperatorAnd instance = null;
        public static OperatorAnd Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorAnd();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            switch (token.Value)
            {
                case "&":
                    return operatorexec.BitwiseAnd(value1, value2);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}