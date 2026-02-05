namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// list<object> 
    /// </summary>
    public class OperatorArgument_list : OperatorBase
    {
        private static OperatorArgument_list instance = null;
        public static OperatorArgument_list Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorArgument_list();
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