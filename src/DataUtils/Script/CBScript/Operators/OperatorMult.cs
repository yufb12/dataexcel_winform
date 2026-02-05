namespace Feng.Script.CBEexpress
{
    /// <summary>
	///		| mult_exp '*' unary_exp
	///		| mult_exp '/' unary_exp
	///		| mult_exp '%' unary_exp
    /// </summary>
    public class OperatorMult : OperatorBase
    {
        private static OperatorMult instance = null;
        public static OperatorMult Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorMult();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            switch (token.Value)
            {
                case "*":
                    return operatorexec.Multiply(value1, value2);
                case "/":
                    return operatorexec.Divide(value1, value2);
                case "%":
                    return operatorexec.Mod(value1, value2);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}