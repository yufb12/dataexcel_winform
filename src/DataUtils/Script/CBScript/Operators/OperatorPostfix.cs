namespace Feng.Script.CBEexpress
{
    /// <summary> 
    /// 
    /// </summary>
    public class OperatorPostfix : OperatorBase
    {
        private static OperatorPostfix instance = null;
        public static OperatorPostfix Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorPostfix();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            return value1;
        }
    }
}