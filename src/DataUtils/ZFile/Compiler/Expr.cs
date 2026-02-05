using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Complier
{  
    public class TEMP : Expr
    {
        static int count = 0;
        int number = 0;
        public TEMP(TYPE p)
            : base(Word.temp, p)
        {
            number = ++count;
        }
    }
 
    public class Expr : Node
    {
        public Token op;
        public TYPE type;
        public Expr(Token tok, TYPE p)
            : base()
        {
            op = tok; ;
            type = p;
        }
        public virtual Expr gen()
        {
            return this;
        }
        public virtual Expr reduce()
        {
            return this;
        }
        public void jumping(int t, int f)
        {

        }
        public void emitjump(string test, int t, int f)
        {
            if (t != 0 && f != 0)
            {
                emit("if" + test + "goto l" + t);
                emit("goto l" + f);
            }
            else if (t != 0)
            {
                emit("if" + test + "goto l" + t);
            }
            else if (f != 0)
            {
                emit("iffalse" + test + "goto l" + f);
            }
        }
        public override string ToString()
        {
            return op.ToString();
        }
    }
    public class OutStream
    {
        public static void Print(string s)
        {
            Console.WriteLine(s);
        }
    }
    public class ID : Expr
    {
        public int offset = 0;
        public ID(Word id, TYPE p, int b)
            : base(id, p)
        {
            offset = b;
        }
    }
    public class OP : Expr
    {
        public OP(Token tok, TYPE p)
            : base(tok, p)
        {

        }
        public Expr reduce()
        {
            Expr x = gen();
            TEMP t = new TEMP(type);
            emit(t.ToString() + "=" + x.ToString());
            return t;
        }
    }
    public class Arith : Expr
    {
        public Expr expr1, expr2;
        public Arith(Token tok, Expr x1, Expr x2)
            : base(tok, null)
        {
            expr1 = x1;
            expr2 = x2;
            type = TYPE.Max(x1.type, x2.type);
            if (type == null)
            {
                error("type error");
            }
        }

        public override Expr gen()
        {
            return new Arith(op, expr1.reduce(), expr2.reduce());
        }
        public override string ToString()
        {
            return expr1.ToString() + " " + op.ToString() + " " + expr2.ToString();
        }
    }
    public class Unary : OP
    {
        public Expr expr;
        public Unary(Token tok, Expr r)
            : base(tok, r.type)
        {
            if (type == null)
            {
                error("type error ");
            }
        }
        public override Expr gen()
        {
            return new Unary(op, expr.reduce());
        }

    }
    public class Constant : Expr
    {
        public Constant(Token tok, TYPE p)
            : base(tok, p)
        {
        }
        public Constant(int i)
            : base(new Num(1), TYPE.INT)
        {
        }
        public static Constant TRUE = new Constant(Word.TRUE, TYPE.Bool);
        public static Constant FALSE = new Constant(Word.FALSE, TYPE.Bool);
        public void jumping(int t, int f)
        {
            if (this == TRUE && t != 0)
            {
                emit("goto L" + t);
            }
            else if (this == FALSE && f != 0)
            {
                emit("goto L" + f);
            }
        }
    }
    public class Access : OP
    {

        public ID array;
        public Expr index;

        public Access(ID a, Expr i, TYPE p)
            : base(new Word("[]", Tag.INDEX), p)
        {
            array = a; index = i;
        }

        public Expr gen() { return new Access(array, index.reduce(), type); }

        public void jumping(int t, int f) { emitjump(reduce().ToString(), t, f); }

        public String ToString()
        {
            return array.ToString() + " [ " + index.ToString() + " ]";
        }
    }
    public class And : Logical
    {

        public And(Token tok, Expr x1, Expr x2) : base(tok, x1, x2) { }

        public void jumping(int t, int f)
        {
            int label = f != 0 ? f : newlabel();
            expr1.jumping(0, label);
            expr2.jumping(t, f);
            if (f == 0) emitlabel(label);
        }
    }
 
    public class Logical : Expr
    {

        public Expr expr1, expr2;

        public Logical(Token tok, Expr x1, Expr x2) :
            base(tok, null)
        {// null type to start
            expr1 = x1; expr2 = x2;
            type = check(expr1.type, expr2.type);
            if (type == null) error("type error");
        }

        public TYPE check(TYPE p1, TYPE p2)
        {
            if (p1 == TYPE.Bool && p2 == TYPE.Bool) return TYPE.Bool;
            else return null;
        }

        public Expr gen()
        {
            int f = newlabel(); int a = newlabel();
            TEMP temp = new TEMP(type);
            this.jumping(0, f);
            emit(temp.ToString() + " = true");
            emit("goto L" + a);
            emitlabel(f); emit(temp.ToString() + " = false");
            emitlabel(a);
            return temp;
        }

        public String ToString()
        {
            return expr1.ToString() + " " + op.ToString() + " " + expr2.ToString();
        }
    }
    public class Not : Logical
    {

        public Not(Token tok, Expr x2) : base(tok, x2, x2) { }

        public void jumping(int t, int f) { expr2.jumping(f, t); }

        public String ToString() { return op.ToString() + " " + expr2.ToString(); }
    }
    public class Or : Logical
    {

        public Or(Token tok, Expr x1, Expr x2) : base(tok, x1, x2) { }

        public void jumping(int t, int f)
        {
            int label = t != 0 ? t : newlabel();
            expr1.jumping(label, 0);
            expr2.jumping(t, f);
            if (t == 0) emitlabel(label);
        }
    }
    public class Rel : Logical
    {

        public Rel(Token tok, Expr x1, Expr x2) : base(tok, x1, x2) { }

        public TYPE check(TYPE p1, TYPE p2)
        {
            if (p1 is Array || p2 is Array) return null;
            else if (p1 == p2) return TYPE.Bool;
            else return null;
        }

        public void jumping(int t, int f)
        {
            Expr a = expr1.reduce();
            Expr b = expr2.reduce();
            String test = a.ToString() + " " + op.ToString() + " " + b.ToString();
            this.emitjump(test, t, f);
        }
    }
 

}
