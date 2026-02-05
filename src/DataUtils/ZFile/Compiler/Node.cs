using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Complier
{   
    public class Node
    {
        public int lexline = 0;
        public Node()
        {
            lexline = Lexer.Line;
        }
        public void error(string s)
        {
            throw new Exception("Near Line " + lexline + ":" + s);
        }
        static int labels = 0;
        public int newlabel()
        {
            return ++labels;
        }
        public void emitlabel(int i)
        {
            OutStream.Print("L" + i + ";");
        }
        public void emit(string s)
        {
            OutStream.Print("\t" + s);
        }
    } 
 

}
