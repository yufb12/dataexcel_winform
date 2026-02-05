using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.App
{
    public static class Product
    {
#if SHOWABOUT
        public const string AssemblyTitle = "DataExcel"; 
#else
        public const string AssemblyTitle = "DataExcel";
#endif
        public const string AssemblyProduct = "DataExcel";
        //public const string AssemblyVersion = Feng.DataUtlis.SmallVersion.AssemblySecondVersion;
        //public const string AssemblyFileVersion = Feng.DataUtlis.Product.AssemblyFileVersion;
        //public const string AssemblyInformationalVersion = Feng.DataUtlis.SmallVersion.AssemblySecondVersion;
        public const string AssemblyCopyright = Feng.DataUtlis.Product.AssemblyCopyright;
        public const string AssemblyCompany = Feng.DataUtlis.Product.AssemblyCompany;
        public const string AssemblyDescription = "DataExcel";
        public const string AssemblyHomePage = Feng.DataUtlis.Product.AssemblyHomePage;
        public const string AssemblyCulture = "";
        public const string AssemblyTrademark = "";
        public const string AssemblyConfiguration = "";
        public const string AssemblyGuid_______ = "BE75726C-8FDF-2018-0701-E788B1E88FB2";
        public const string AssemblyControlGuid = "CE75726C-8FDF-2018-0702-E788B1E88FB2";
        public const string AssemblyComGuid____ = "DE75726C-8FDF-2018-0703-E788B1E88FB2";
        public const string AssemblyFile = "DataExcel.v2.1";
        public const string AssemblyDateTime = "2014-03-18";
        public const string AssemblyDownLoadUrl = Feng.DataUtlis.Product.AssemblyDownLoadUrl;
    }

    public class NewVersion
    {
        public static string LastVersion = string.Empty;
        static NewVersion()
        {
            if (DateTime.Now.Year > 2020)
            {
                throw new Exception("check new version");
            }
        }
    }
}
