namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// !
    /// </summary>
    public class OperatorPrimary : OperatorBase
    {
        private static OperatorPrimary instance = null;
        public static OperatorPrimary Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorPrimary();
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