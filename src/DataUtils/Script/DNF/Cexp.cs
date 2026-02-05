using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feng.Script.C
{
    public interface IToken
    {
        bool Contains(string text);
        string Text { get; set; }
    }
    public interface IInterface
    {
        bool Has(IToken token);
    }
    public static class assignment_operator
    {
        public static bool Has(IToken token)
        {
            return true;
        }
    }
    public interface IResult
    {
        object Value { get; set; }
    }
    public class Cexp
    {
        public IToken NextToken()
        {
            return null;
        }

        /// <summary>
        ///  // exp			: assignment_exp
        ///  //            | exp ',' assignment_exp
        ///  //            ;
        /// </summary>
        public IResult exp()
        {
            IResult result = assignment_exp();
            //if (result.Do)
            //{
            //    IToken token = NextToken();
            //    if (token != null)
            //    {
            //        if (token.Contains(","))
            //        {

            //        }
            //    }
            //}
            throw new Exception();

        }

        /// //assignment_exp		: conditional_exp
        /// //            | unary_exp assignment_operator assignment_exp						
        /// //            ;
        /// //      //assignment_operator	: '=' | '*=' | '/=' | '%=' | '+=' | '-=' | '<<='
        /// //        | '>>=' | '&=' | '^=' | '|='
        /// //        ;
        public IResult assignment_exp()
        {
            //IResult result = conditional_exp();
            //if (result.Sucess)
            //{
            //    return result;
            //}
            //unary_exp();
            //IToken token = NextToken();
            //assignment_operator.Has(token);
            //assignment_exp();
            throw new Exception();
        }

        /// //conditional_exp		: logical_or_exp
        /// //            | logical_or_exp '?' exp ':' conditional_exp
        /// //            ;
        public IResult conditional_exp()
        {
            IResult result = logical_or_exp();
            IToken token = NextToken();
            if (token.Contains("?"))
            {
                if (result.Value.Equals(true))
                {
                    return exp();
                }
                if (result.Value.Equals(false))
                {
                    return conditional_exp();
                }
            }
            throw new Exception();
        }

        /// //logical_or_exp		: logical_and_exp
        /// //            | logical_or_exp '||' logical_and_exp
        /// //            ;
        public IResult logical_or_exp()
        {
            IResult result = logical_and_exp();
            IToken token = NextToken();

            if (token.Contains("||"))
            {
                logical_and_exp();
            }
            throw new Exception();
        }

        /// <summary>
        ///          // logical_and_exp		: inclusive_or_exp
        ///           //         | logical_and_exp '&&' inclusive_or_exp
        ///           // ;
        /// </summary>
        /// <returns></returns>
        public IResult logical_and_exp()
        {
            IResult result = inclusive_or_exp();
            IToken token = NextToken();

            if (token.Contains("&&"))
            {
                inclusive_or_exp();
            }
            throw new Exception();
        }

        /// //inclusive_or_exp	: exclusive_or_exp
        /// //            | inclusive_or_exp '|' exclusive_or_exp
        /// //            ;
        public IResult inclusive_or_exp()
        {
            IResult result = exclusive_or_exp();
            IToken token = NextToken();

            if (token.Contains("|"))
            {
                exclusive_or_exp();
            }
            throw new Exception();
        }

        /// //exclusive_or_exp	: and_exp
        /// //            | exclusive_or_exp '^' and_exp
        /// //            ;
        public IResult exclusive_or_exp()
        {
            IResult result = and_exp();
            IToken token = NextToken();

            if (token.Contains("^"))
            {
                and_exp();
            }
            throw new Exception();
        }

        /// //and_exp			: equality_exp
        /// //            | and_exp '&' equality_exp
        /// //            ;
        public IResult and_exp()
        {
            IResult result = equality_exp();
            IToken token = NextToken();

            if (token.Contains("&"))
            {
                equality_exp();
            }
            throw new Exception();
        }

        /// //equality_exp		: relational_exp
        /// //            | equality_exp '==' relational_exp
        /// //            | equality_exp '!=' relational_exp
        /// //            ;
        public IResult equality_exp()
        {
            IResult result = relational_exp();
            IToken token = NextToken();

            if (token.Contains("==") || token.Contains("!="))
            {
                relational_exp();
            }
            throw new Exception();
        }

        /// //relational_exp		: shift_expression
        /// //            | relational_exp '<' shift_expression
        /// //            | relational_exp '>' shift_expression
        /// //            | relational_exp '<=' shift_expression
        /// //            | relational_exp '>=' shift_expression
        /// //            ;
        public IResult relational_exp()
        {
            IResult result = shift_expression();
            IToken token = NextToken();

            if (token.Contains(">") || token.Contains("<") || token.Contains("<=") || token.Contains(">="))
            {
                shift_expression();
            }
            throw new Exception();
        }

        /// //shift_expression	: additive_exp
        /// //            | shift_expression '<<' additive_exp
        /// //            | shift_expression '>>' additive_exp
        /// //            ;
        public IResult shift_expression()
        {
            IResult result = additive_exp();
            IToken token = NextToken();

            if (token.Contains(">>") || token.Contains("<<"))
            {
                additive_exp();
            }
            throw new Exception();
        }

        /// //additive_exp		: mult_exp
        /// //            | additive_exp '+' mult_exp
        /// //            | additive_exp '-' mult_exp
        /// //            ;
        public IResult additive_exp()
        {
            IResult result = mult_exp();
            IToken token = NextToken();

            if (token.Contains("+") || token.Contains("-"))
            {
                additive_exp();
            }
            throw new Exception();
        }

        /// //mult_exp		: cast_exp
        /// //            | mult_exp '*' cast_exp
        /// //            | mult_exp '/' cast_exp
        /// //            | mult_exp '%' cast_exp
        /// //            ;
        public IResult mult_exp()
        {
            IResult result = cast_exp();
            IToken token = NextToken();

            if (token.Contains("*") || token.Contains("/") || token.Contains("%"))
            {
                result = cast_exp();
            }
            throw new Exception();
        }

        /// //cast_exp		: unary_exp
        /// //            | '(' type_name ')' cast_exp
        /// //            ;
        public IResult cast_exp()
        {
            IResult result = cast_exp();
            IToken token = NextToken();

            if (token.Contains("*") || token.Contains("/") || token.Contains("%"))
            {
                result = cast_exp();
            }
            throw new Exception();
        }

        /// //unary_exp		: postfix_exp
        /// //            | '++' unary_exp
        /// //            | '--' unary_exp
        /// //            | unary_operator cast_exp 
        /// //            ;
        /// 
        /// //unary_operator		: '&' | '*' | '+' | '-' | '~' | '!'
        /// //            ;
        public IResult unary_exp()
        {
            IResult result = postfix_exp();
            IToken token = NextToken();

            if (token.Contains("++") || token.Contains("--"))
            {
                result = unary_exp();
            }
            if (token.Contains("&") || token.Contains("*") || token.Contains("+")
                || token.Contains("-") || token.Contains("~") || token.Contains("!"))
            {
                result = unary_exp();
            }
            throw new Exception();
        }

        /// //postfix_exp		: primary_exp
        /// //            | postfix_exp '[' exp ']'
        /// //            | postfix_exp '(' argument_exp_list ')'
        /// //            | postfix_exp '('			')'
        /// //            | postfix_exp '.' id
        /// //            | postfix_exp '->' id
        /// //            | postfix_exp '++'
        /// //            | postfix_exp '--'
        /// //            ;
        public IResult postfix_exp()
        {
            IResult result = primary_exp();
            IToken token = NextToken();

            if (token.Contains("++") || token.Contains("--"))
            {
                result = unary_exp();
            }
            if (token.Contains("&") || token.Contains("*") || token.Contains("+")
                || token.Contains("-") || token.Contains("~") || token.Contains("!"))
            {
                result = unary_exp();
            }
            throw new Exception();
        }

        /// //primary_exp		: id
        /// //            | const
        /// //            | string
        /// //            | '(' exp ')'
        /// //            ;
        public IResult primary_exp()
        {
            IResult result = primary_exp();
            IToken token = NextToken();

            if (token.Contains("++") || token.Contains("--"))
            {
                result = unary_exp();
            }
            if (token.Contains("&") || token.Contains("*") || token.Contains("+")
                || token.Contains("-") || token.Contains("~") || token.Contains("!"))
            {
                result = unary_exp();
            }
            throw new Exception();
        }

        /// //argument_exp_list	: assignment_exp
        /// //            | argument_exp_list ',' assignment_exp
        /// //            ;
        public IResult argument_exp_list()
        {

            IResult result = assignment_exp();
            IToken token = NextToken();

            List<IResult> list = new List<IResult>();
            list.Add(result);
            if (token.Contains(","))
            {
                result = assignment_exp();
                list.Add(result);
            }
            return null;
        }

        //const			: int_const
        //            | char_const
        //            | float_const
        //            | enumeration_const
        //            ;           ;
        public IResult const_exp()
        {
            IToken token = NextToken();
            if (token == null)
                return null;
            return TokenToValue(token);
        }

        public IResult TokenToValue(IToken token)
        {
            return null;
        }

    }
}