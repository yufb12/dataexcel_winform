using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{

    public abstract class OperatorExecBase
    {

        private Feng.Collections.DictionaryEx<string, object> staticvalues = null;
        public Feng.Collections.DictionaryEx<string, object> STATICVALUES
        {
            get {
                if (staticvalues == null)
                {
                    staticvalues = new Feng.Collections.DictionaryEx<string, object>();
                }
                return staticvalues;
            }
        }

        private Dictionary<string, object> keyvalues = null;
        public Dictionary<string, object> KeyValues
        {
            get
            {
                if (keyvalues == null)
                {
                    keyvalues = new Dictionary<string, object>();
                }
                return keyvalues;
            }
        }

        public virtual bool HasKey(string key1)
        {
            string key = key1.ToLower();
            return this.KeyValues.ContainsKey(key);
        }
        public virtual object GetKeyValue(string key1)
        { 
            string key = key1.ToLower();
            if (this.KeyValues.ContainsKey(key))
                return this.KeyValues[key];
            return null;
        }

        public virtual object SetKeyValue(string key1, object value)
        {
            string key = key1.ToLower();
            if (this.KeyValues.ContainsKey(key))
            {
                this.KeyValues[key] = value;
            }
            else
            {
                this.KeyValues.Add(key, value);
            }
            return Feng.Utils.Constants.OK; ;
        }

        /// <summary>
        /// &&
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract object LogicalAND(object value1, object value2);
        /// <summary>
        /// ||
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object LogicalOR(object value1, object value2);
        /// <summary>
        /// <<
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object BitwiseShiftLeft(object value1, object value2);
        /// <summary>
        /// >>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object BitwiseShiftRight(object value1, object value2);
        /// <summary>
        /// |
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object BitwiseOR(object value1, object value2);
        /// <summary>
        /// &
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object BitwiseAnd(object value1, object value2);
        /// <summary>
        /// ^ A^B
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object BitwiseXOR(object value1, object value2);
        /// <summary>
        /// a+b
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Add(object value1, object value2);
        /// <summary>
        /// a-b
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Minus(object value1, object value2);
        /// <summary>
        /// "a"+"b"
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Combine(object value1, object value2);
        /// <summary>
        /// a*b
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Multiply(object value1, object value2);
        /// <summary>
        /// / a/b
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Divide(object value1, object value2);
        /// <summary>
        /// []
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Index(object value1, object value2);
        /// <summary>
        /// a.b
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Property(object value1, object value2);

        /// <summary>
        /// a.b()
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object PropertyFunction(object value, object value1, List<object> values);

        /// <summary>
        /// max(a,b,c)
        /// </summary>
        /// <param name="value1">max</param>
        /// <param name="values">a,b,c</param>
        /// <returns></returns>
        public abstract object Function(object value1, List<object> values);
        /// <summary>
        /// ()
        /// </summary>
        /// <param name="value1"></param>
        /// <returns></returns>
        public abstract object Parenthesis(object value1);
        /// <summary>
        /// a==b
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Equal(object value1, object value2);
        /// <summary>
        /// !=
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object NotEqual(object value1, object value2);
        /// <summary>
        /// <
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object LessThan(object value1, object value2);
        /// <summary>
        /// >
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object MoreThan(object value1, object value2);
        /// <summary>
        /// <=
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object LessThanEqual(object value1, object value2);
        /// <summary>
        /// >=
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object MoreThanEqual(object value1, object value2);
        /// <summary>
        /// 负数-
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Negative(object value1);
        /// <summary>
        /// 按位取反 ~
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object BitwiseNOT(object value1);
        /// <summary>
        /// % 3%4=1
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Mod(object value1, object value2);
        /// <summary>
        /// 取反!
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Not(object value1);
        /// <summary>
        /// A1:B12
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Range(object value1, object value2);

        /// <summary>
        /// 123
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object ToNumber(object value1);

        /// <summary>
        /// 123
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object ToString(object value1);

        /// <summary>
        /// ++
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Increment(object value1);
        /// <summary>
        /// --
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public abstract object Decrement(object value1);
    }

}
