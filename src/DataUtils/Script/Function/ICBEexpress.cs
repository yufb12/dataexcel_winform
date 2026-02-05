
using Feng.Diagnostics;
using Feng.Forms.Interface;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
 

    public interface IBreak
    {
        bool Break { get; set; }
    }

    public interface IRunMethod
    {
        object RunMethod(object item, string method, object[] args);
    }

    public interface IMethod
    {
        List<IMethodInfo> MethodList { get; }
        bool Contains(string method); 
        object RunFunction(string methodname, params object[] args);
        bool CaseSensitive { get; set; }
        string Name { get; }
        string Description { get; }
    }

    public interface IMethodInfo
    {
        string Name { get; set; }
        string Description { get; set; }
        string Eg { get; set; }
        bool HasMethod(string function);
        object Exec(params object[] args);

    }

    public interface IMethodCollection : IList<IMethod>, IAddrangle<IMethod> 
    {
        object RunMethod(string methodname, ref bool hasMethod, params object[] args);
    }
}
