//namespace Feng.Script.CBEexpress
//{
//    /// <summary>
//    /// null
//    /// </summary>
//    public class OperatorConst_Item : OperatorBase
//    {
//        private static OperatorConst_Item instance = null;
//        public static OperatorConst_Item Instance
//        {
//            get
//            {
//                if (instance == null)
//                {
//                    instance = new OperatorConst_Item();
//                }
//                return instance;
//            }
//        }
//        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
//        {
//            return operatorexec.GetKeyValue(Feng.Utils.ConvertHelper.ToString(value1));
//        }
//    }
//}