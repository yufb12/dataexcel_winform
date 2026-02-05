using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.App
{
    public sealed class AssemblyLoadAttribute : Attribute
    {
        //public AssemblyLoadAttribute()
        //{
        //    if (System.Diagnostics.Debugger.IsAttached)
        //    {
        //        System.Windows.Forms.Application.Exit();
        //    }
        //}
    }
    [AssemblyAttribute("6F-6C-37-6B-5A-34-7A-31-76-6A-62-71-34-2B-72-50-6D-55-4A-54-75-77-3D-3D")]
    public sealed class AssemblyAttribute : Attribute
    {
        public AssemblyAttribute()
        {

        }    
        public AssemblyAttribute(string author)
        {
            
        }
    }
}
