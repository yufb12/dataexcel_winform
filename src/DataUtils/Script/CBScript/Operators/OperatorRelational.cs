namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// | relational_exp '<' shift_expression
    ///	| relational_exp '>' shift_expression
    ///	| relational_exp '<=' shift_expression
    ///	| relational_exp '>=' shift_expression
    /// </summary>
    public class OperatorRelational : OperatorBase
    {
        private static OperatorRelational instance = null;
        public static OperatorRelational Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorRelational();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            switch (token.Value)
            {
                case "<":
                    return operatorexec.LessThan(value1, value2);
                case ">":
                    return operatorexec.MoreThan(value1, value2);
                case ">=":
                    return operatorexec.MoreThanEqual(value1, value2);
                case "<=":
                    return operatorexec.LessThanEqual(value1, value2);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}