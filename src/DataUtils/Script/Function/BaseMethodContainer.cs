using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Script.Method
{
    [Serializable]
    public abstract class BaseMethodContainer : IMethod
    {

        public delegate object BaseFunction(params object[] args);

        public BaseMethodContainer()
        {
        }

        #region IFunctionClassFactory 成员
        public virtual object RunFunction(string method, params object[] args)
        {
            object obj = null;
            method = method.ToUpper();
            foreach (IMethodInfo model in MethodList)
            {
                if (model.Name.ToUpper() == method)
                {
                    Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Functions", "BaseMethodContainer", "RunFunction", true, model.Name);
                    obj = model.Exec(args);
                    break;
                }
            }
            return obj;
        }

        #endregion

        #region ICaseSensitive 成员
        private bool _CaseSensitive = false;
        /// <summary>
        /// 大小写敏感
        /// </summary>
        public virtual bool CaseSensitive
        {
            get
            {
                return _CaseSensitive;
            }
            set
            {
                _CaseSensitive = value;
            }
        }

        #endregion

        #region IMothodNameList 成员

        public virtual bool Contains(string method)
        {
            method = method.ToUpper();
            foreach (IMethodInfo model in MethodList)
            {
                if (model.HasMethod(method))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion



        #region IMethod 成员

        private List<IMethodInfo> _MethodList = null;
        public virtual List<IMethodInfo> MethodList
        {
            get
            {
                if (_MethodList == null)
                {
                    _MethodList = new List<IMethodInfo>();
                }
                return _MethodList;
            }
        }

        #endregion

        #region IName 成员

        public abstract string Name
        {
            get;
        }
        public abstract string Description
        {
            get;
        }

        #endregion 
        public string GetText(int index, string defaulttext, params object[] args)
        {
            if (args.Length > index)
            {
                return GetTextValue(index, args);
            }
            return defaulttext;
        }


        public virtual object GetFirstValue(params object[] args)
        {
            if (args.Length > 0)
                return args[0];
            return null;
        }
        public virtual object GetSecondValue(params object[] args)
        {
            if (args.Length > 1)
                return args[1];
            return null;
        }
        public virtual object GetThirdValue(params object[] args)
        {
            if (args.Length > 2)
                return args[2];
            return null;
        }
        public virtual object GetFourthValue(params object[] args)
        {
            if (args.Length > 3)
                return args[3];
            return null;
        }
        public virtual object GetFifthValue(params object[] args)
        {
            if (args.Length > 4)
                return args[4];
            return null;
        }

        public virtual object GetArgIndex(int index, params object[] args)
        {
            if (index < args.Length)
                return args[index];
            return null;
        }

        public abstract object GetValue(int index, params object[] args);

        public virtual int GetIntValue(int index, params object[] args)
        {
            object value = GetValue(index, args);
            return Feng.Utils.ConvertHelper.ToInt32(value);
        }

        public virtual int GetIntValue(int index, int defaultvalue, params object[] args)
        {
            object value = GetValue(index, args);
            if (value == null)
                return defaultvalue;
            return Feng.Utils.ConvertHelper.ToInt32(value, defaultvalue);
        }
        public virtual bool GetBooleanValue(int index, bool defaultvalue, params object[] args)
        {
            object value = GetValue(index, args);
            if (value == null)
                return defaultvalue;
            return Feng.Utils.ConvertHelper.ToBoolean(value);
        }
        public virtual decimal GetDecimalValue(int index, decimal defaultvalue, params object[] args)
        {
            object value = GetValue(index, args);
            if (value == null)
                return defaultvalue;
            return Feng.Utils.ConvertHelper.ToDecimal(value);
        }
        public virtual Int64 GetLongValue(int index, params object[] args)
        {
            object value = GetValue(index, args);
            return Feng.Utils.ConvertHelper.ToInt64(value);
        }
        public virtual decimal GetDecimalValue(int index, params object[] args)
        {
            object value = GetValue(index, args);
            return Feng.Utils.ConvertHelper.ToDecimal(value);
        }
        public virtual double GetDoubleValue(int index, params object[] args)
        {
            object value = GetValue(index, args);
            return Feng.Utils.ConvertHelper.ToDouble(value);
        }
        public virtual string GetTextValue(int index, params object[] args)
        {
            object value = GetValue(index, args);
            return Feng.Utils.ConvertHelper.ToString(value);
        }
        public virtual string GetTextValue(int index, string dafaultvalue, params object[] args)
        {
            object value = GetValue(index, args);
            string text = Feng.Utils.ConvertHelper.ToString(value);
            if (string.IsNullOrWhiteSpace(text))
            {
                return dafaultvalue;
            }
            return text;
        }
        public virtual float GetSingleValue(int index, params object[] args)
        {
            object value = GetValue(index, args);
            return Feng.Utils.ConvertHelper.ToSingle(value);
        }
        public virtual bool HasArgs(int index, params object[] args)
        {
            return index < args.Length;
        }
        public virtual bool GetBooleanValue(int index, params object[] args)
        {
            object value = GetValue(index, args);
            return Feng.Utils.ConvertHelper.ToBoolean(value);
        }
        public virtual DateTime GetDateTimeValue(int index, params object[] args)
        {
            object value = GetValue(index, args);
            return Feng.Utils.ConvertHelper.ToDateTime(value);
        }
        public virtual float GetFloatValue(int index, float defaultvalue, params object[] args)
        {
            object value = GetValue(index, args);
            if (value == null)
                return defaultvalue;
            return Feng.Utils.ConvertHelper.ToSingle(value);
        }
        public override string ToString()
        {
            return this.Name + "_" + this.Description;
        }

        //public const byte TRUE = Constants.TRUE;
        //public const byte FALSE = Constants.FALSE;
        //public const int OK = Constants.OK;
        //public const int Cancel = Constants.Cancel;
    }
}
