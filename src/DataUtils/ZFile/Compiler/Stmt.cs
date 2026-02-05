using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Complier
{   
 
 
    public class Break : Stmt
    {

        Stmt stmt;

        public Break()
        {
            if (Stmt.Enclosing == Stmt.Null) error("unenclosed break");
            stmt = Stmt.Enclosing;
        }

        public void gen(int b, int a)
        {
            emit("goto L" + stmt.after);
        }
    }
    public class Do : Stmt
    {

        Expr expr; Stmt stmt;

        public Do() { expr = null; stmt = null; }

        public void init(Stmt s, Expr x)
        {
            expr = x; stmt = s;
            if (expr.type != TYPE.Bool) expr.error("boolean required in do");
        }

        public void gen(int b, int a)
        {
            after = a;
            int label = newlabel();   // label for expr
            stmt.gen(b, label);
            emitlabel(label);
            expr.jumping(b, 0);
        }
    }
    public class Else : Stmt
    {

        Expr expr; Stmt stmt1, stmt2;

        public Else(Expr x, Stmt s1, Stmt s2)
        {
            expr = x; stmt1 = s1; stmt2 = s2;
            if (expr.type != TYPE.Bool) expr.error("boolean required in if");
        }
        public void gen(int b, int a)
        {
            int label1 = newlabel();   // label1 for stmt1
            int label2 = newlabel();   // label2 for stmt2
            expr.jumping(0, label2);    // fall through to stmt1 on true
            emitlabel(label1); stmt1.gen(label1, a); emit("goto L" + a);
            emitlabel(label2); stmt2.gen(label2, a);
        }
    }
    public class If : Stmt
    {

        Expr expr; Stmt stmt;

        public If(Expr x, Stmt s)
        {
            expr = x; stmt = s;
            if (expr.type != TYPE.Bool) expr.error("boolean required in if");
        }

        public void gen(int b, int a)
        {
            int label = newlabel(); // label for the code for stmt
            expr.jumping(0, a);     // fall through on true, goto a on false
            emitlabel(label); stmt.gen(label, a);
        }
    }
 
    public class Seq : Stmt
    {

        Stmt stmt1; Stmt stmt2;

        public Seq(Stmt s1, Stmt s2) { stmt1 = s1; stmt2 = s2; }

        public void gen(int b, int a)
        {
            if (stmt1 == Stmt.Null) stmt2.gen(b, a);
            else if (stmt2 == Stmt.Null) stmt1.gen(b, a);
            else
            {
                int label = newlabel();
                stmt1.gen(b, label);
                emitlabel(label);
                stmt2.gen(label, a);
            }
        }
    }
    public class Set : Stmt
    {

        public ID id; public Expr expr;

        public Set(ID i, Expr x)
        {
            id = i; expr = x;
            if (check(id.type, expr.type) == null) error("type error");
        }

        public TYPE check(TYPE p1, TYPE p2)
        {
            if (TYPE.IsNumeric(p1) && TYPE.IsNumeric(p2)) return p2;
            else if (p1 == TYPE.Bool && p2 == TYPE.Bool) return p2;
            else return null;
        }

        public void gen(int b, int a)
        {
            emit(id.ToString() + " = " + expr.gen().ToString());
        }
    }
    public class SetElem : Stmt
    {

        public ID array; public Expr index; public Expr expr;

        public SetElem(Access x, Expr y)
        {
            array = x.array; index = x.index; expr = y;
            if (check(x.type, expr.type) == null) error("type error");
        }

        public TYPE check(TYPE p1, TYPE p2)
        {
            if (p1 is Array || p2 is Array) return null;
            else if (p1 == p2) return p2;
            else if (TYPE.IsNumeric(p1) && TYPE.IsNumeric(p2)) return p2;
            else return null;
        }

        public void gen(int b, int a)
        {
            String s1 = index.reduce().ToString();
            String s2 = expr.reduce().ToString();
            emit(array.ToString() + " [ " + s1 + " ] = " + s2);
        }
    }
    public class Stmt : Node
    {

        public Stmt() { }

        public static Stmt Null = new Stmt();

        public void gen(int b, int a) { } // called with labels begin and after

        public int after = 0;                   // saves label after
        public static Stmt Enclosing = Stmt.Null;  // used for break stmts
    }
    public class While : Stmt
    {

        Expr expr; Stmt stmt;

        public While() { expr = null; stmt = null; }

        public void init(Expr x, Stmt s)
        {
            expr = x; stmt = s;
            if (expr.type != TYPE.Bool) expr.error("boolean required in while");
        }
        public void gen(int b, int a)
        {
            after = a;                // save label a
            expr.jumping(0, a);
            int label = newlabel();   // label for stmt
            emitlabel(label); stmt.gen(label, b);
            emit("goto L" + b);
        }
    }
 

}
