namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// | additive_exp '+' mult_exp
    ///	| additive_exp '-' mult_exp
    /// </summary>
    public class OperatorAdditive : OperatorBase
    {
        private static OperatorAdditive instance = null;
        public static OperatorAdditive Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorAdditive();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        { 
            switch (token.Value)
            {
                case "+":
                    return operatorexec.Add(value1, value2); 
                case "-":
                    return operatorexec.Minus(value1, value2);
                default:
                    break;
            }
            throw new CBExpressException("运算符不正确"+" Row:"+token.Line +" Column:"+token.Column, token)
            { RowIndex = token .Line , ColumnIndex =token.Column };
        }
    }
}