namespace Feng.Script.CBEexpress
{

    public class OperatorSignlogicalAND : NetOperatorBase
    {
        private static OperatorSignlogicalAND instance = null;
        public static OperatorSignlogicalAND Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignlogicalAND();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.LogicalAND(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignlogicalAND; } }
        public override string ToString()
        {
            return "&&";
        }
    }


    public class OperatorSignlogicalOR : NetOperatorBase
    {
        private static OperatorSignlogicalOR instance = null;
        public static OperatorSignlogicalOR Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignlogicalOR();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.LogicalOR(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignlogicalOR; } }
        public override string ToString()
        {
            return "||";
        }
    }


    //public class OperatorSignbitwiseOR : NetOperatorBase
    //{
    //    private static OperatorSignbitwiseOR instance = null;
    //    public static OperatorSignbitwiseOR Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSignbitwiseOR();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        object value = methodProxy.netOperatorProxy.BitwiseOR(value1, value2);
    //        return value;
    //    }
    //    public override ushort Index { get { return TokenType.SignbitwiseOR; } }
    //}

    //public class OperatorSingXOR : NetOperatorBase
    //{
    //    private static OperatorSingXOR instance = null;
    //    public static OperatorSingXOR Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSingXOR();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        object value = methodProxy.netOperatorProxy.BitwiseXOR(value1, value2);
    //        return value;
    //    }
    //    public override ushort Index { get { return TokenType.SingXOR; } }
    //}

    //public class OperatorSignbitwiseAND : NetOperatorBase
    //{
    //    private static OperatorSignbitwiseAND instance = null;
    //    public static OperatorSignbitwiseAND Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSignbitwiseAND();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        object value = methodProxy.netOperatorProxy.BitwiseAnd(value1, value2);
    //        return value;
    //    }
    //    public override ushort Index { get { return TokenType.SignbitwiseAND; } }
    //}

    public class OperatorSignEqualTo : NetOperatorBase
    {
        private static OperatorSignEqualTo instance = null;
        public static OperatorSignEqualTo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignEqualTo();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Equal(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignEqualTo; } }
        public override string ToString()
        {
            return "==";
        }
    }

    public class OperatorSignNotEqualTo : NetOperatorBase
    {
        private static OperatorSignNotEqualTo instance = null;
        public static OperatorSignNotEqualTo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignNotEqualTo();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.NotEqual(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignNotEqualTo; } }
        public override string ToString()
        {
            return "!=";
        }
    }


    public class OperatorSignRelationalGreaterThan : NetOperatorBase
    {
        private static OperatorSignRelationalGreaterThan instance = null;
        public static OperatorSignRelationalGreaterThan Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignRelationalGreaterThan();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.MoreThan(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignRelationalGreaterThan; } }
        public override string ToString()
        {
            return ">";
        }
    }

    public class OperatorSignRelationalGreaterThanOrEqual : NetOperatorBase
    {
        private static OperatorSignRelationalGreaterThanOrEqual instance = null;
        public static OperatorSignRelationalGreaterThanOrEqual Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignRelationalGreaterThanOrEqual();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.MoreThanEqual(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignRelationalGreaterThanOrEqual; } }
        public override string ToString()
        {
            return ">=";
        }
    }

    public class OperatorSignRelationalLessThan : NetOperatorBase
    {
        private static OperatorSignRelationalLessThan instance = null;
        public static OperatorSignRelationalLessThan Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignRelationalLessThan();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.LessThan(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignRelationalLessThan; } }
        public override string ToString()
        {
            return "<";
        }
    }

    public class OperatorSignRelationalLessThanOrEqual : NetOperatorBase
    {
        private static OperatorSignRelationalLessThanOrEqual instance = null;
        public static OperatorSignRelationalLessThanOrEqual Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignRelationalLessThanOrEqual();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.LessThanEqual(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignRelationalLessThanOrEqual; } }
        public override string ToString()
        {
            return "<=";
        }
    }


    //public class OperatorSignShiftRight : NetOperatorBase
    //{
    //    private static OperatorSignShiftRight instance = null;
    //    public static OperatorSignShiftRight Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSignShiftRight();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        object value = methodProxy.netOperatorProxy.BitwiseShiftRight(value1, value2);
    //        return value;
    //    }
    //    public override ushort Index { get { return TokenType.SignShiftRight; } }
    //}


    //public class OperatorSignShiftLeft : NetOperatorBase
    //{
    //    private static OperatorSignShiftLeft instance = null;
    //    public static OperatorSignShiftLeft Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSignShiftLeft();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        object value = methodProxy.netOperatorProxy.BitwiseShiftLeft(value1, value2);
    //        return value;
    //    }
    //    public override ushort Index { get { return TokenType.SignShiftLeft; } }
    //}


    public class OperatorSignPlus : NetOperatorBase
    {
        private static OperatorSignPlus instance = null;
        public static OperatorSignPlus Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignPlus();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Add(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignPlus; } }
        public override string ToString()
        {
            return "+";
        }
    }


    public class OperatorSignMinus : NetOperatorBase
    {
        private static OperatorSignMinus instance = null;
        public static OperatorSignMinus Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignMinus();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Minus(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignMinus; } }
        public override string ToString()
        {
            return "-";
        }
    }


    public class OperatorSignNegative : NetOperatorBase
    {
        private static OperatorSignNegative instance = null;
        public static OperatorSignNegative Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignNegative();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Negative(value1);
            return value;
        }
        public override ushort Index { get { return TokenType.SignNegative; } }
        public override string ToString()
        {
            return "-";
        }
    }

    //public class OperatorSignLeftParenthesis : NetOperatorBase
    //{
    //    private static OperatorSignLeftParenthesis instance = null;
    //    public static OperatorSignLeftParenthesis Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSignLeftParenthesis();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        string msg = string.Empty;
    //        throw new OperatorException(msg);
    //    }
    //    public override ushort Index { get { return TokenType.SignLeftParenthesis; } }
    //}

    public class OperatorSignMultiplied : NetOperatorBase
    {
        private static OperatorSignMultiplied instance = null;
        public static OperatorSignMultiplied Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignMultiplied();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Multiply(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignMultiplied; } }
        public override string ToString()
        {
            return "*";
        }
    }

    public class OperatorSignDivided : NetOperatorBase
    {
        private static OperatorSignDivided instance = null;
        public static OperatorSignDivided Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignDivided();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Divide(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignDivided; } }
        public override string ToString()
        {
            return "/";
        }
    }

    public class OperatorSignModulus : NetOperatorBase
    {
        private static OperatorSignModulus instance = null;
        public static OperatorSignModulus Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignModulus();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Mod(value1, value2);
            return value;
        }
        public override ushort Index { get { return TokenType.SignModulus; } }
        public override string ToString()
        {
            return "%";
        }
    }

    public class OperatorSignIncrement : NetOperatorBase
    {
        private static OperatorSignIncrement instance = null;
        public static OperatorSignIncrement Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignIncrement();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Increment(value1);
            return value;
        }
        public override ushort Index { get { return TokenType.SignIncrement; } }
        public override string ToString()
        {
            return "++";
        }
    }

    public class OperatorSignDecrement : NetOperatorBase
    {
        private static OperatorSignDecrement instance = null;
        public static OperatorSignDecrement Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignDecrement();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Decrement(value1);
            return value;
        }
        public override ushort Index { get { return TokenType.SignDecrement; } }
        public override string ToString()
        {
            return "--";
        }
    }

    public class OperatorSignlogicalNOT : NetOperatorBase
    {
        private static OperatorSignlogicalNOT instance = null;
        public static OperatorSignlogicalNOT Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorSignlogicalNOT();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = methodProxy.netOperatorProxy.Not(value1);
            return value;
        }
        public override ushort Index { get { return TokenType.SignlogicalNOT; } }
        public override string ToString()
        {
            return "!";
        }
    }


    //public class OperatorSignbitwiseNOT : NetOperatorBase
    //{
    //    private static OperatorSignbitwiseNOT instance = null;
    //    public static OperatorSignbitwiseNOT Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSignbitwiseNOT();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        object value = methodProxy.netOperatorProxy.BitwiseNOT(value1);
    //        return value;
    //    }
    //    public override ushort Index { get { return TokenType.SignbitwiseNOT; } }
    //}


    public class OperatorNull : NetOperatorBase
    {
        private static OperatorNull instance = null;
        public static OperatorNull Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OperatorNull();
                }
                return instance;
            }
        }
        public override object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = value1;
            return value;
        }
        public override ushort Index { get { return TokenType.SignNULL; } }

        public override string ToString()
        {
            return string.Empty;
        }
    }

    //public class OperatorSignEuality : NetOperatorBase
    //{
    //    private static OperatorSignEuality instance = null;
    //    public static OperatorSignEuality Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSignEuality();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        string msg = string.Empty;
    //        throw new OperatorException(msg);
    //    }
    //    public override ushort Index { get { return TokenType.SignEuality; } }
    //}
    //public class OperatorSignComma : NetOperatorBase
    //{
    //    private static OperatorSignComma instance = null;
    //    public static OperatorSignComma Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new OperatorSignComma();
    //            }
    //            return instance;
    //        }
    //    }
    //    public override object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy)
    //    {
    //        string msg = string.Empty;
    //        throw new OperatorException(msg);
    //    }
    //    public override ushort Index { get { return TokenType.SignComma; } }
    //}
}