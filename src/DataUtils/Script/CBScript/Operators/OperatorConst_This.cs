namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// null
    /// </summary>
    public class OperatorConst_This : OperatorBase
    {
        private static OperatorConst_This instance = null;
        public static OperatorConst_This Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorConst_This();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            return operatorexec.GetKeyValue(Feng.Utils.ConvertHelper.ToString(value1));
        }
    }
}