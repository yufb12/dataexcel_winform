using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// !
    /// </summary>
    public class OperatorFunction : OperatorBase
    {
        private static OperatorFunction instance = null;
        public static OperatorFunction Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorFunction();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            try
            {
                return operatorexec.Function(value1, value2 as List<object>);
            }
            catch (System.Exception ex)
            {  
                throw new CBExpressException(ex.Message + " Row:" + token.Line + " Column:" + token.Column, token)
                { RowIndex = token.Line, ColumnIndex = token.Column };
            }
        }
    }
}