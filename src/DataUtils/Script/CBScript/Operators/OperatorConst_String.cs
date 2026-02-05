namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// "abc"
    /// </summary>
    public class OperatorConst_String : OperatorBase
    {
        private static OperatorConst_String instance = null;
        public static OperatorConst_String Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorConst_String();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            return token.Value;
        }
    }
}