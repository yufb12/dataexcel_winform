
namespace Feng.Script.CB
{
    public class TokenType
    {
        /// <summary> 变量
        /// var name
        /// </summary>
        public const int ID = 1;
        /// <summary>
        /// 字符串常量
        /// </summary>
        public const int CONST_STRING = 2;
        /// <summary>
        /// 数字常量124.34,0x124 
        /// </summary>
        public const int CONST_NUMBER = 3;
        public const int CONST_TRUE = 6;
        public const int CONST_FALSE = 7;
        public const int CONST_NULL = 8;
        public const int Key_THIS = 9;
        public const int Key_ME = 10;
        public const int Key_FOREACH = 21;
        public const int Key_FOR = 22;
        public const int Key_WHILE = 23;
        public const int Key_VAR = 24;
        public const int Key_IF = 25;
        public const int Key_ELSE = 26;
        public const int Key_CONTINUE = 27;
        public const int Key_BREAK = 28;
        public const int Key_RETURN = 29;
        public const int Key_FUNCTION = 30;
        public const int Key_IN = 31;
        public const int Key_Object = 32;
        public const int Key_RPC = 33;
        public const int Key_Include = 35;
        public const int Key_Try = 36;
        public const int Key_Catch = 37;
        public const int Key_Throw = 38;

        /// <summary>
        ///  && 逻辑与运算
        /// </summary>
        public const int SignlogicalAND = 11001;
        /// <summary>
        /// || 逻辑或 
        /// </summary>
        public const int SignlogicalOR = 12001;
        /// <summary>
        ///  | 按位或运算
        /// </summary>
        public const int SignbitwiseOR = 10001;
        /// <summary>
        ///  ^ 异或运算
        /// </summary>
        public const int SingXOR = 9001;
        /// <summary>
        /// 按位与运算& 
        /// </summary>
        public const int SignbitwiseAND = 8001;
        /// <summary>
        /// == 等于 
        /// </summary>
        public const int SignEqualTo = 7001;
        /// <summary>
        /// != 不等于
        /// </summary>
        public const int SignNotEqualTo = 7002;
        /// <summary> 大于号 > 
        ///	 
        /// </summary>
        public const int SignRelationalGreaterThan = 6001;
        /// <summary>
        /// >=大于等于号
        /// </summary>
        public const int SignRelationalGreaterThanOrEqual = 6002;
        /// <summary> 小于号 < 
        /// 
        /// </summary>
        public const int SignRelationalLessThan = 6003;
        /// <summary>
        /// 小于等于号 <=
        /// </summary>
        public const int SignRelationalLessThanOrEqual = 6004;
        /// <summary>
        /// >> 右移运算符
        /// </summary>
        public const int SignShiftRight = 5002;
        /// <summary>
        /// 左移运算符<<左移运算符
        /// </summary>
        public const int SignShiftLeft = 5001;
        /// <summary>
        /// + 加法运算 
        /// </summary>
        public const int SignPlus = 4001;
        /// <summary>
        /// 减号- 减法运算
        /// </summary>
        public const int SignMinus = 4002;
        /// <summary>
        /// 负号- 取相反数运算
        /// </summary>
        public const int SignNegative = 2001;
        /// <summary>
        /// ( 左小括号
        /// </summary>
        public const int SignLeftParenthesis = 1002;
        /// <summary>
        /// ) 右小括号
        /// </summary>
        public const int SignRightParenthesis = 906;
        /// <summary>
        /// * 乘法
        /// </summary>
        public const int SignMultiplied = 3001;
        /// <summary>
        /// / 除法
        /// </summary>
        public const int SignDivided = 3002;
        /// <summary>
        /// % 余数
        /// </summary>
        public const int SignModulus = 3003;
        /// <summary>
        /// ++ 自增
        /// </summary>
        public const int SignIncrement = 2003;
        /// <summary>
        /// -- 自减
        /// </summary>
        public const int SignDecrement = 2004;
        /// <summary> 取反!
        /// bool ! 取反
        /// </summary>
        public const int SignlogicalNOT = 2007;
        /// <summary>
        /// ~按位取反
        /// </summary>
        public const int SignbitwiseNOT = 2008;
        /// <summary>
        /// = 附值运算
        /// </summary>
        public const int SignEuality = 14001;
        /// <summary>
        /// , 逗号
        /// </summary>
        public const int SignComma = 15001;
        /// <summary>
        /// equality '{'   
        /// </summary>
        public const int SignLeftBRACE = 901;
        /// <summary>
        /// equality '}'   
        /// </summary>
        public const int SignRightBRACE = 902;
        /// <summary>
        /// 注释 // 
        /// </summary>
        public const int SignComment = 903;
        /// <summary>
        /// ; 分号
        /// </summary>
        public const int SignSemicolon = 905;
        /// <summary>
        /// 左中括号 '['   
        /// </summary>
        public const int SignLeftSquareBrackets = 1001;
        /// <summary>
        /// 右中括号 ']'   
        /// </summary>
        public const int SignRightSquareBrackets = 907;
        /// <summary>
        /// 最后一个没有符号的
        /// </summary>
        public const int SignNULL = 65535;

    }
}
