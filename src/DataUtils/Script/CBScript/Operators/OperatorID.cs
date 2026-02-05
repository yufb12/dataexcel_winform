namespace Feng.Script.CBEexpress
{
    /// <summary>
    /// 关键字
    /// </summary>
    public class OperatorID : OperatorBase
    {
        private static OperatorID instance = null;
        public static OperatorID Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorID();
                }
                return instance;
            }
        }
        public override object Exec(Parse parse, OperatorExecBase operatorexec, object value1, object value2, Token token)
        {
            string key = Feng.Utils.ConvertHelper.ToString(value1);
            object value = operatorexec.GetKeyValue(key);
            if (value == null)
            {
                if (!operatorexec.HasKey(key))
                {
                    throw new Feng.Script.CBEexpress.CBExpressException(" 变量【" + key + "】未定义 Row:" + token.Line + " Column:" + token.Column, token) { RowIndex = token.Line, ColumnIndex = token.Column };
                }
            }
            return value;
        }
    }
}