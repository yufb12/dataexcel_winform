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
 
    internal class DataExcelLicense : License
    { 
        private DataExcelLicenseType licType;
 
        public DataExcelLicense(DataExcelLicenseType licType)
        {
            this.licType = licType;
        }

        public override void Dispose()
        {
        }
 
        public override string LicenseKey
        {
            get
            {
                return "DataExcelKey";
            }
        }

        public DataExcelLicenseType LicType
        {
            get
            {
                return this.licType;
            }
        }
    }
 
}
