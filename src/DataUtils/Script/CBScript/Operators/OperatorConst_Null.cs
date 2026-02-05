namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// null
    /// </summary>
    public class OperatorConst_Null : OperatorBase
    {
        private static OperatorConst_Null instance = null;
        public static OperatorConst_Null Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorConst_Null();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            return null;
        }
    }
}