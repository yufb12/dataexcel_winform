namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// null
    /// </summary>
    public class OperatorConst_True : OperatorBase
    {
        private static OperatorConst_True instance = null;
        public static OperatorConst_True Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorConst_True();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            return true;
        }
    }
}