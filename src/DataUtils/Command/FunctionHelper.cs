using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Utils.Compiler
{
    //public class Tokey
    //{
    //    private string _symbol = string.Empty;
    //    public string Symbol
    //    {
    //        get
    //        {
    //            return this._symbol;
    //        }
    //        set
    //        {
    //            this._symbol = value;
    //        }
    //    }
    //    private string _text = string.Empty;
    //    public string Text
    //    {
    //        get
    //        {
    //            return this._text;
    //        }
    //        set
    //        {
    //            this._text = value;
    //        }
    //    }
    //    private int _level = 0;
    //    public int Level
    //    {
    //        get
    //        {
    //            return this._level;
    //        }
    //        set
    //        {
    //            this._level = value;
    //        }
    //    }
    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode();
    //    }
    //    public static Tokey operator >(Tokey c1, Tokey c2)
    //    {
    //        if (c1.Level > c2.Level)
    //        {
    //            return c1;
    //        }
    //        return c2;
    //    }
    //    public static Tokey operator ==(Tokey c1, Tokey c2)
    //    {
    //        if (c1.Level == c2.Level)
    //        {
    //            return c1;
    //        }
    //        return c2;
    //    }
    //    public static Tokey operator !=(Tokey c1, Tokey c2)
    //    {
    //        if (c1.Level == c2.Level)
    //        {
    //            return c2;
    //        }
    //        return c1;
    //    }
    //    public static Tokey operator <(Tokey c1, Tokey c2)
    //    {
    //        if (c1.Level == c2.Level)
    //        {
    //            return c1;
    //        }
    //        return c2;
    //    }

    //}


    //public class FunctionHelper
    //{
    //    public void InitTokey()
    //    {

    //    }
    //    private Dictionary<string, int> diclevels = new Dictionary<string, int>();
    //    public void InitLevel()
    //    {
    //        int i = 0;
    //        i++;
    //        diclevels.Add("+", i);
    //        diclevels.Add("-", i);
    //        diclevels.Add("*", i);
    //        diclevels.Add("/", i);
    //        diclevels.Add("%", i);
    //        diclevels.Add("^", i);
    //        i++;
    //        diclevels.Add(">", i);
    //        diclevels.Add("<", i);
    //        diclevels.Add("==", i);
    //        diclevels.Add("<=", i);
    //        diclevels.Add(">=", i);
    //        i++;
    //        diclevels.Add("(", i);
    //        diclevels.Add(")", i);
    //        i++;
    //        diclevels.Add("||", i);
    //        diclevels.Add("&&", i);

    //    }
    //    Stack<Tokey> Stack;
  
    //    public FunctionHelper()
    //    {
    //        Stack = new Stack<Tokey>();
    //    }

    //    public void Add(Tokey obj)
    //    { 
    //        Stack.Push(obj);
    //    }

    //    public void Clear()
    //    {
    //        Stack.Clear();
    //    }

    //    public void Execute()
    //    {


    //    }

    //}
}
