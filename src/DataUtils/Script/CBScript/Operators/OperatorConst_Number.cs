namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// 1234
    /// </summary>
    public class OperatorConst_Number : OperatorBase
    {
        private static OperatorConst_Number instance = null;
        public static OperatorConst_Number Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorConst_Number();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            return operatorexec.ToNumber(token.Value);
        }
    }
}