namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// ||	逻辑或	表达式||表达式	左到右	双目运算符
    /// </summary>
    public class OperatorLogical_or : OperatorBase
    {
        private static OperatorLogical_or instance = null;
        public static OperatorLogical_or Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorLogical_or();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            switch (token.Value)
            {
                case "||":
                    return operatorexec.LogicalOR(value1, value2);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}