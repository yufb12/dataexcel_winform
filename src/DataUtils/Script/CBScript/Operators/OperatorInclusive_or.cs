namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// |  |	按位或	整型表达式|整型表达式	左到右	双目运算符
    /// </summary>
    public class OperatorInclusive_or : OperatorBase
    {
        private static OperatorInclusive_or instance = null;
        public static OperatorInclusive_or Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorInclusive_or();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            switch (token.Value)
            {
                case "|":
                    return operatorexec.BitwiseOR(value1, value2);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}