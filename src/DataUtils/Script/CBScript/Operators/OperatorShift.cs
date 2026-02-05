namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// | shift_expression '<<' additive_exp
    ///	| shift_expression '>>' additive_exp
    /// </summary>
    public class OperatorShift : OperatorBase
    {
        private static OperatorShift instance = null;
        public static OperatorShift Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorShift();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            switch (token.Value)
            {
                case "<<":
                    return operatorexec.BitwiseShiftLeft(value1, value2);
                case ">>":
                    return operatorexec.BitwiseShiftRight(value1, value2); 
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }
    }
}