namespace Feng.Script.CBEexpress
{
    public class ExpressOperator
    {

        public static object Run(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object result = null;
            expressBases.ReSet();
            result = SignlogicalOR(expressBases, varStack, methodProxy);
            if (!(expressBases.Current.Operator.Index == TokenType.SignNULL||
                expressBases.Current.Operator.Index == TokenType.SignIncrement ||
                expressBases.Current.Operator.Index == TokenType.SignDecrement))
            { 
                throw new SyntacticException(expressBases.Current.Token, "语句未正确缺结束", CBEexpressExCode.ERRORCODE_11292);
            }
            expressBases.Pop();
            if (expressBases.HasNext())
                throw new SyntacticException(expressBases.Current.Token, "语句未正确缺结束", CBEexpressExCode.ERRORCODE_11291);
            return result;
        }


        private static object SignlogicalOR(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignlogicalAND(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignlogicalOR)
            {
                expressBases.Pop();
                object rightvalue = SignlogicalAND(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignlogicalOR.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignlogicalAND(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignEqualTo(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignlogicalAND)
            {
                expressBases.Pop();
                object rightvalue = SignEqualTo(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignlogicalAND.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignEqualTo(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignNotEqualTo(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignEqualTo)
            {
                expressBases.Pop();
                object rightvalue = SignNotEqualTo(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignEqualTo.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignNotEqualTo(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignRelationalLessThan(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignNotEqualTo)
            {
                expressBases.Pop();
                object rightvalue = SignRelationalLessThan(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignNotEqualTo.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignRelationalLessThan(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignRelationalLessThanOrEqual(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignRelationalLessThan)
            {
                expressBases.Pop();
                object rightvalue = SignRelationalLessThanOrEqual(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignRelationalLessThan.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignRelationalLessThanOrEqual(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignRelationalGreaterThan(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignRelationalLessThanOrEqual)
            {
                expressBases.Pop();
                object rightvalue = SignRelationalGreaterThan(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignRelationalLessThanOrEqual.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignRelationalGreaterThan(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignRelationalGreaterThanOrEqual(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignRelationalGreaterThan)
            {
                expressBases.Pop();
                object rightvalue = SignRelationalGreaterThanOrEqual(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignRelationalGreaterThan.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignRelationalGreaterThanOrEqual(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignPlus(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignRelationalGreaterThanOrEqual)
            {
                expressBases.Pop();
                object rightvalue = SignPlus(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignRelationalGreaterThanOrEqual.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignPlus(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignMinus(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignPlus)
            {
                expressBases.Pop();
                object rightvalue = SignMinus(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignPlus.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignMinus(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignMultiplied(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignMinus)
            {
                expressBases.Pop();
                object rightvalue = SignMultiplied(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignMinus.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignMultiplied(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SignDivided(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignMultiplied)
            {
                expressBases.Pop();
                object rightvalue = SignDivided(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignMultiplied.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SignDivided(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = SingModulus(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignDivided)
            {
                expressBases.Pop();
                object rightvalue = SingModulus(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignDivided.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object SingModulus(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            object leftvalue = RunSignNULL(expressBases, varStack, methodProxy);
            if (!expressBases.HasNext())
                return leftvalue;
            while (expressBases.Current.Operator.Index == TokenType.SignModulus)
            {
                expressBases.Pop();
                object rightvalue = RunSignNULL(expressBases, varStack, methodProxy);
                leftvalue = OperatorSignModulus.Instance.Exec(leftvalue, rightvalue, varStack, methodProxy);
                if (!expressBases.HasNext())
                    break;
            }
            return leftvalue;
        }

        private static object RunSignNULL(ExpressPool expressBases, NetVarCollection varStack, ICBContext methodProxy)
        {
            NetExpressBase express = expressBases.Peek();
            object result = null;
            result = express.Exec(varStack, methodProxy);
            return result;
        }
    }


}
