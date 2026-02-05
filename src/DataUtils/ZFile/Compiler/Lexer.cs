using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Complier
{  
 
    public class Lexer
    {
        public static int Line = 1;
        char peek = ' ';
        public System.Collections.Hashtable words = new System.Collections.Hashtable();
        public void reserve(Word v)
        {
            words.Add(v.lexeme, v);
        }
        public Lexer()
        {
            reserve(new Word("if", Tag.IF));
            reserve(new Word("else", Tag.ELSE));
            reserve(new Word("while", Tag.WHILE));
            reserve(new Word("do", Tag.DO));
            reserve(new Word("break", Tag.BREAK));
            reserve(Word.TRUE);
            reserve(Word.FALSE);
            //toDo:
            reserve(TYPE.INT);
            reserve(TYPE.FLOAT);
            reserve(TYPE.Char);
            reserve(TYPE.Bool);
        }

        public void readch()
        {
            //peek = (char)Console.Read();
            peek = (char)reader.Read();
        }

        public void ReadchFile(string file)
        {
            reader = new System.IO.StreamReader(file);
        }
        private System.IO.StreamReader reader;
        public bool readch(char c)
        {
            if (peek != c)
            {
                return false;
            }
            peek = ' ';
            return true;
        }
        public Token Scan()
        {
            for (; ; readch())
            {
                if (peek == ' ' || peek == '\t' || peek == '\r')
                {
                    continue;
                }
                else if (peek == '\n')
                {
                    Line = Line + 1;
                }
                else
                {
                    break;
                }
            }
            switch (peek)
            {
                case '&':
                    if (readch('&'))
                    {
                        return Word.and;
                    }
                    else
                    {
                        return new Token('&');
                    }
                case '|':
                    if (readch('|'))
                    {
                        return Word.or;
                    }
                    else
                    {
                        return new Token('|');
                    }
                case '=':
                    if (readch('='))
                    {
                        return Word.eq;
                    }
                    else
                    {
                        return new Token('=');
                    }
                case '!':
                    if (readch('='))
                    {
                        return Word.ne;
                    }
                    else
                    {
                        return new Token('!');
                    }

                case '>':
                    if (readch('='))
                    {
                        return Word.ge;
                    }
                    else
                    {
                        return new Token('>');
                    }
                case '<':
                    if (readch('='))
                    {
                        return Word.le;
                    }
                    else
                    {
                        return new Token('<');
                    }
                default:
                    break;
            }

            if (char.IsDigit(peek))
            {
                int v = 0;
                do
                {
                    v = 10 * v + (int)char.GetNumericValue(peek);
                    readch();
                } while (char.IsDigit(peek));
                if (peek != '.')
                {
                    return new Num(v);
                }
                double x = v;
                float d = 10;
                for (; ; )
                {
                    readch();
                    if (!char.IsDigit(peek))
                    {
                        break;
                    }
                    x = x + char.GetNumericValue(peek) / d;
                    d = d * 10;
                }
                return new Real((float)x);
            }
            if (char.IsLetter(peek))
            {
                StringBuilder sb = new StringBuilder();
                do
                {
                    sb.Append(peek);
                    readch();
                }
                while (char.IsLetterOrDigit(peek));
                string s = sb.ToString();
                Word word = (Word)words[s];
                if (word != null)
                {
                    return word;
                }
                word = new Word(s, Tag.ID);
                words.Add(s, word);
                return word;
            }
            Token tok = new Token(peek);
            peek = ' ';
            return tok;
        }
    } 
 

}
