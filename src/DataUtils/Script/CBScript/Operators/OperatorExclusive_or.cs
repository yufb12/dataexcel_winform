namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// inclusive_or_exp '^' exclusive_or_exp 按位或	整型表达式|整型表达式	左到右	双目运算符
    /// </summary>
    public class OperatorExclusive_or : OperatorBase
    {
        private static OperatorExclusive_or instance = null;
        public static OperatorExclusive_or Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorExclusive_or();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            switch (token.Value)
            {
                case "^":
                    return operatorexec.BitwiseXOR(value1, value2);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}