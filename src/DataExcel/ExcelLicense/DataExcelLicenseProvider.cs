using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Security;
using System.Runtime.Serialization;

namespace Feng.Excel.ExcelLicense
{
 
    internal class DataExcelLicenseProvider : LicenseProvider
    {
        public DataExcelLicenseProvider()
        {

        } 
 
        protected virtual string EncodeKey(DataExcelLicenseType licType)
        {
            switch (licType)
            {
                case DataExcelLicenseType.Full:
                    return "Full";

                case DataExcelLicenseType.Trial:
                    return "Trial";
            }
            return null;
        }

        public static string Get(LicenseContext context)
        {
            if (DataExcel.GenuineValidation())
            {
                return "Full";
            }
            return string.Empty;
        }

        public ProductKind Kind
        {
            get { return ProductKind.DataExcel; }
        }

        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        { 
            DataExcelLicenseType none = DataExcelLicenseType.None;
            string savedLicenseKey = this.GetSavedLicenseKey(context, type);
            if ((savedLicenseKey != null) && (savedLicenseKey != ""))
            {
                none = this.ParseKey(savedLicenseKey);
            }
            if ((none == DataExcelLicenseType.None) || (context.UsageMode == LicenseUsageMode.Designtime))
            {
                none = (Get(context) == "") ? DataExcelLicenseType.Trial : DataExcelLicenseType.Full; 
            }
     
            if (none != DataExcelLicenseType.None)
            {
                return new DataExcelLicense(none);
            }
            return null;
        }

        private string GetSavedLicenseKey(LicenseContext context, Type type)
        {
            object savedLicenseKey = null;
            if (savedLicenseKey != null)
            {
                return savedLicenseKey.ToString();
            }
            try
            {
                savedLicenseKey = context.GetSavedLicenseKey(type, null);
            }
            catch (Exception)
            {
                return "Full";
            }
            if (savedLicenseKey != null)
            {
                return savedLicenseKey.ToString();
            }
            return null;
        }

        protected virtual DataExcelLicenseType ParseKey(string key)
        {
            if (key == "Full")
            {
                return DataExcelLicenseType.Full;
            }
            if (key == "Trial")
            {
                return DataExcelLicenseType.Trial;
            }
            return DataExcelLicenseType.None;
        }

        private void SetSavedLicenseKey(LicenseContext context, Type type, string key)
        {
            if (key != null)
            { 
                context.SetSavedLicenseKey(type, key);
            }
        }
 
    }


}
