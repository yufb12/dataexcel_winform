using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Complier
{
    public class Test
    {
        public static void main()
        {
            Lexer lex = new Lexer();
            Parser parse = new Parser(lex);
            parse.program();
        }
    } 
}
