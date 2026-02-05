using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Complier
{  
 
    public class Real : Token
    {
        public float Value;
        public Real(float v)
            : base(Tag.REAL)
        {
            Value = v;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    } 
    public class Token
    {
        public int tag;
        public Token(int t)
        {
            tag = t;
        }
        public override string ToString()
        {
            return tag.ToString() + " " + (char)tag;
        }
    }
    public class Num : Token
    {
        public int Value;
        public Num(int value)
            : base(Tag.NUM)
        {
            Value = value;
        }
    }
    public class Word : Token
    {
        public string lexeme = string.Empty;
        public Word(string s, int tag)
            : base(tag)
        {
            lexeme = s;
        }
        public override string ToString()
        {
            return "lexeme=【"+lexeme + "】" + base.ToString();
        }
        public static Word and = new Word("&&", Tag.AND);
        public static Word or = new Word("||", Tag.OR);
        public static Word eq = new Word("==", Tag.EQ);
        public static Word ne = new Word("!=", Tag.NE);
        public static Word le = new Word("<=", Tag.LE);
        public static Word ge = new Word(">=", Tag.AND);
        public static Word minus = new Word("minus", Tag.MINUS);
        public static Word TRUE = new Word("true", Tag.AND);
        public static Word FALSE = new Word("false", Tag.AND);
        public static Word temp = new Word("t", Tag.AND);

    } 
 
    public class TYPE : Word
    {
        public int Width = 0;
        public TYPE(string s, int tag, int w)
            : base(s, tag)
        {
            Width = w;
        }
        public static TYPE INT = new TYPE("int", Tag.BASIC, 4);
        public static TYPE FLOAT = new TYPE("float", Tag.BASIC, 8);
        public static TYPE Char = new TYPE("char", Tag.BASIC, 1);
        public static TYPE Bool = new TYPE("bool", Tag.BASIC, 1);
        public static bool IsNumeric(TYPE p)
        {
            if (p == TYPE.INT || p == TYPE.FLOAT || p == TYPE.Char)
            {
                return true;
            }
            return false;
        }
        public static TYPE Max(TYPE p1, TYPE p2)
        {
            if (!IsNumeric(p1))
            {
                return null;
            }
            if (!IsNumeric(p2))
            {
                return null;
            }
            if (p1 == TYPE.FLOAT || p2 == TYPE.FLOAT)
            {
                return TYPE.FLOAT;
            }
            else if (p1 == TYPE.INT || p2 == TYPE.INT)
            {
                return TYPE.INT;
            }
            else
            {
                return TYPE.Char;
            }
        }
    }
    public class Array : TYPE
    {
        public TYPE of;
        public int size = 1;
        public Array(int sz, TYPE p)
            : base("[]", Tag.INDEX, sz * p.Width)
        {
            size = sz;
            of = p;
        }
        public override string ToString()
        {
            return "[" + size + "]" + of.ToString();
        }
    } 

}
