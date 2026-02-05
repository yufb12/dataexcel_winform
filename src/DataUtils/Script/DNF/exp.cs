using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feng.Script.C
{
  

    public class ep
    {
        public string text = string.Empty;
        public int index = 0;
        public class Token : IToken
        {
            public Token(char c)
            {
                Text = c.ToString();
            }
            public Token(string c)
            {
                Text = c.ToString();
            }
            public string Text { get; set; }
            public bool Contains(string text)
            {
                return text == Text;
            }
            public override string ToString()
            {
                return Text;
            }
        }
        public class ReaultList: IResult
        {
            private List<IResult> reslist = new List<IResult>();
            public List<IResult> ResList
            {
                get {
                    return reslist;
                }
            }

            public object Value
            {
                get
                {
                    return reslist;
                }
                set
                { 

                }
            }
        }
        public class Result : IResult
        {

            public object Value
            {
                get;
                set;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
        public IResult ToResult(IToken token)
        {
            Result result = new Result();
            result.Value = token.Text;
            return result;
        }
        public IResult ToResult(object value)
        {
            Result result = new Result();
            result.Value = value;
            return result;
        }
        public IToken ToToken(char text)
        {
            return new Token(text);
        }
        public IToken ToToken(string text)
        {
            return new Token(text);
        }
        public IToken NextToken()
        {
            if ((index + 1) < text.Length)
            {
                return ToToken(text[index + 1]);
            }
            return null;
        }
        public IToken CurrentToken()
        {
            if (index < text.Length)
            {
                return ToToken(text[index]);
            }
            return null;
        }

        public IToken Peek()
        {
            index++;
            if (index < text.Length)
            {
                IToken token = ToToken(text[index]);
                return token;
            }
            return null;
        }

        public IResult Exp()
        {
            IResult result = null;
            result = additive_exp2();
            return result;
        }

        //additive_exp2		: mult_exp2
        //            | additive_exp '+' mult_exp
        //            | additive_exp '-' mult_exp
        //            ;
        public IResult additive_exp2()
        {
            IResult result = mult_exp2();
            IToken token = CurrentToken();
            if (token != null)
            {
                if (token.Contains("+") || token.Contains("-"))
                {
                    IToken t2 = Peek();
                    IResult result2 = additive_exp2();
                    result = ToResult(Convert.ToInt32(result2.Value) + Convert.ToInt32(result.Value));
                } 
            }
            return result;
        }

        /// //mult_exp		: cast_exp
        /// //            | mult_exp '*' cast_exp
        /// //            | mult_exp '/' cast_exp
        /// //            | mult_exp '%' cast_exp
        /// //            ;
        public IResult mult_exp2()
        {
            IResult result = argument_exp_list();
            IToken token = CurrentToken();
            if (token != null)
            {
                if (token.Contains("*") || token.Contains("/") || token.Contains("%"))
                {
                    IToken t2 = Peek();
                    IResult result2 = mult_exp2();
                    return ToResult(Convert.ToInt32(result2.Value) * Convert.ToInt32(result.Value));
                } 
            }
            return result;
        }

        /// //argument_exp_list	: assignment_exp
        /// //            | argument_exp_list ',' assignment_exp
        /// //            ;
        public IResult argument_exp_list()
        { 
            ReaultList reslist = new ReaultList();
            IResult result = const_exp();
            IToken token = CurrentToken();
            if (token != null)
            {
                if (token.Contains(","))
                {
                    IToken t2 = Peek();
                    IResult result2 = Exp();
                    token = CurrentToken();
                    if (token != null)
                    {
                        reslist.ResList.Add(result);
                        return ToResult(Convert.ToInt32(result2.Value) * Convert.ToInt32(result.Value));
                    }
                }
            }
            return result;
        }

        public IResult const_exp()
        {
            IToken token = CurrentToken();
            IToken t1 = Peek();
            return ToResult(token);
        }

        public void Error()
        {

        }

        public IResult Parse()
        {
            return Exp();
        }
    }
    //3+4*6+6
    //additive_exp2		: mult_exp2
    //            | additive_exp '+' mult_exp
    //            | additive_exp '-' mult_exp
    //            ;
    //mult_exp2		: const_exp
    //            | mult_exp '*' const_exp
    //            | mult_exp '/' const_exp
    //            | mult_exp '%' const_exp
    //            ;
}