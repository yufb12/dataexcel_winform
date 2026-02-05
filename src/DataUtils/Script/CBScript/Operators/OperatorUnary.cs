namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// 	| '++' postfix_exp
	///		| '--' postfix_exp
	///		| '-' postfix_exp 
	///		| '~' postfix_exp 
	///		| '!' postfix_exp
    /// </summary>
    public class OperatorUnary : OperatorBase
    {
        private static OperatorUnary instance = null;
        public static OperatorUnary Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorUnary();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            switch (token.Value)
            {
                case "++":
                    return operatorexec.Increment(value1);
                case "--":
                    return operatorexec.Decrement(value1);
                case "-":
                    return operatorexec.Negative(value1);
                case "~":
                    return operatorexec.BitwiseNOT(value1);
                case "!":
                    return operatorexec.Not(value1);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}