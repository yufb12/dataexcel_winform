namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// null
    /// </summary>
    public class OperatorConst_False : OperatorBase
    {
        private static OperatorConst_False instance = null;
        public static OperatorConst_False Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorConst_False();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            return false;
        }
    }
}