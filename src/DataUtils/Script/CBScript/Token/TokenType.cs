namespace Feng.Script.CBEexpress
{
    public class TokenType
    {
        public const int ERROR = -1;
       /// <summary>
       /// var name
       /// </summary>
        public const int ID = 1; 
        /// <summary>
        /// "字符串"
        /// </summary>
        public const int CONST_STRING = 2;
        /// <summary>
        /// 124.34,0x124
        /// </summary>
        public const int CONST_NUMBER = 3;      
        /// <summary>
        /// null
        /// </summary>
 
        public const int TRUE = 6;         //true
        public const int FALSE = 7;        //false
        public const int NULL = 8;         //null         
        public const int THIS = 9;         //null 
 
        //public const int ME = 11;         //null 
        //public static int NULL = 12;         //null 
        /// <summary>
        /// | logical_or_exp '||' logical_and_exp
        /// </summary>
        public const int logical_or_exp = 61;

        /// <summary>
        /// | logical_and_exp '&&' inclusive_or_exp
        /// </summary>
        public const int logical_and_exp = 62;

        /// <summary>
        /// | inclusive_or_exp '|' exclusive_or_exp
        /// </summary>
        public const int inclusive_or_exp = 63;
        /// <summary>
        /// | exclusive_or_exp '^' and_exp
        /// </summary>
        public const int exclusive_or_exp = 64;
        /// <summary>
        /// | and_exp '&' equality_exp
        /// </summary>
        public const int and_exp = 65;

        /// <summary>
        /// | equality_exp '==' relational_exp
		///	| equality_exp '!=' relational_exp
        /// </summary>
        public const int equality_exp = 66;

        /// <summary>
        /// | relational_exp '<' shift_expression
		///	| relational_exp '>' shift_expression
		///	| relational_exp '<=' shift_expression
		///	| relational_exp '>=' shift_expression
        /// </summary>
        public const int relational_exp = 67;

        /// <summary>
        /// | shift_expression '<<' additive_exp
		///	| shift_expression '>>' additive_exp
        /// </summary>
        public const int shift_expression = 68;
        /// <summary>
        /// | additive_exp '+' mult_exp
		///	| additive_exp '-' mult_exp
        /// </summary>
        public const int additive_exp = 69;
        /// <summary>
        /// | mult_exp '*' unary_exp
		///	| mult_exp '/' unary_exp
		///	| mult_exp '%' unary_exp
        /// </summary>
        public const int mult_exp = 70;
        /// <summary>
        /// '++' postfix_exp
		///	| '--' postfix_exp
		///	| '-' postfix_exp 
		///	| '~' postfix_exp 
		///	| '!' postfix_exp
        /// </summary>
        public const int unary_exp = 71;

        public const int postfix_exp = 72;
        /// <summary>
        /// , function(arg1,arg2,arg3)
        /// </summary>
        public const int argument_exp_list = 99;

        /// <summary>
        /// ( function()
        /// </summary>
        public const int function_exp_begin = 76;
        /// <summary>
        /// ) function()
        /// </summary>
        public const int function_exp_end = 77;
        //public const int inclusive_or_exp = 3;

        /// <summary>
        /// equality '='   
        /// </summary>
        public const int equality = 78;
        /// <summary>
        /// equality '{'   
        /// </summary>
        public const int LBRACE = 79;
        /// <summary>
        /// equality '}'   
        /// </summary>
        public const int RBRACE = 80;

        /// <summary>
        /// ;
        /// </summary>
        public const int NewLine = 91;

        /// <summary>
        /// \n
        /// </summary>
        public const int NewLineEnter = 92;


        /// <summary>
        /// \
        /// </summary>
        public const int SameLine = 93;

        /// <summary>
        /// //
        /// </summary>
        public const int Comments = 199;




























    }
}
