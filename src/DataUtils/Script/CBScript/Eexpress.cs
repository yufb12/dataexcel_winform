
namespace Feng.Script.CBEexpress
{

    public class Eexpress : ICBEexpress
    {
        public Eexpress()
        {

        }

        private Parse parse = null;
        public virtual Parse Parse
        {
            get
            {
                if (parse == null)
                {
                    parse = new Parse();
                }
                return parse;
            }
            set
            {
                parse = value;
            }
        }

        public object Exec(SymbolTable table)
        {
            object value = null;
            value = Parse.Exec(ExecProxy, table);
            return value;
        }

        public void Write(object sender, SymbolTable table, string varname, object value)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                if (OutWatch == null)
                    return;
            }
            try
            {
                string text = string.Empty;
                if (value == null)
                {
                    text = "=null";
                }
                else if (string.Empty.Equals(value))
                {
                    text = string.Empty;
                }
                else if (value is System.Data.DataTable)
                {
                    text = "=TableRows[" + (value as System.Data.DataTable).Rows.Count + "]";
                }
                else if (value is Feng.Collections.HashtableEx)
                {
                    text = "=Hashtable[" + (value as Feng.Collections.HashtableEx).Count + "]";
                }
                else if (value is Feng.Collections.ListEx<object>)
                {
                    text = "=List[" + (value as Feng.Collections.ListEx<object>).Count + "]";
                }
                else
                {
                    text = "=" + Feng.Utils.ConvertHelper.ToString(value);
                }
                string str = string.Format("{0},{1},{2}{3}",  
                    table.TextLine.ToString().PadLeft(5, '0'),
                    table.GetText(), varname, text);
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(str);
                }
                if (OutWatch != null)
                {
                    OutWatch.Write(str);
                }
            }
            catch (System.Exception)
            {
            }
        }

        private OperatorExecBase execproxy = null;
        public virtual OperatorExecBase ExecProxy
        {
            get
            {
                if (execproxy == null)
                {
                    execproxy = new ExcuteProxy();
                }
                return execproxy;
            }
            set { execproxy = value; }
        }

        public virtual bool Finished { get; set; }
        public virtual object FinishObj { get; set; }
        public virtual bool Break { get; set; }
        public IOutWatch OutWatch { get; set; }
    }
}
