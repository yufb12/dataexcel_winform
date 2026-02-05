//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Configuration.Install;
//using System.Runtime.CompilerServices;
//using System.IO;
//using System.Windows.Forms;
//using System.Diagnostics;
//using System.Text;
//namespace Feng.Excel
//{
//    [EditorBrowsable(EditorBrowsableState.Never)]
//    [ToolboxItem(false)]
//    [RunInstaller(true)]
//    public partial class Gacutil : Installer
//    {
//        public Gacutil()
//        {
//            InitializeComponent();
//            EventLogInstaller eventinstaller = new EventLogInstaller();
//            eventinstaller.Source = "DataExcelInstall";
//            eventinstaller.Log = "DataExcel";
//            this.Installers.Add(eventinstaller);
//        }

//        protected override void OnBeforeInstall(IDictionary savedState)
//        {

//            try
//            {

//                base.OnBeforeInstall(savedState);
//#if DEBUG
//                StringBuilder sb = new StringBuilder();
//                foreach (DictionaryEntry str in this.Parent.Context.Parameters)
//                {
//                    sb.AppendLine(string.Format("OnBeforeInstall Key={0} Value={1}", str.Key, str.Value));
//                }

//                MessageBox.Show(sb.ToString());
//#endif

//                //HKEY_LOCAL_MACHINE\SOFTWARE\360Safe\360Scan;

//                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
//                key = key.OpenSubKey(@"SOFTWARE\DataExcel", true);

//                if (key == null)
//                {
//                    key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\DataExcel");
//                }
//                key = key.CreateSubKey(Product.AssemblyVersion);
//                string path = this.Context.Parameters["assemblypath"];
//                key.SetValue("Install", path);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("OnBeforeInstall=" + ex.Message);
//            }

//        }

//        public override void Install(IDictionary stateSaver)
//        {
//            try
//            {
//                base.Install(stateSaver);
 
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Install=" + ex.Message);
//            }
        

//        }

//        protected override void OnAfterInstall(IDictionary savedState)
//        {
//            base.OnAfterInstall(savedState);
//            try
//            {
//                string path = this.Context.Parameters["assemblypath"];
 
//                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
//                key = key.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework\v2.0.50727\AssemblyFoldersEx", true);

//                if (key == null)
//                {
//                    key = key.CreateSubKey(@"SOFTWARE\Microsoft\.NETFramework\v2.0.50727\AssemblyFoldersEx");
//                }
//                key = key.CreateSubKey("DataExcel");
//                key.SetValue("", System.IO.Path.GetDirectoryName(path));

//                key = Microsoft.Win32.Registry.LocalMachine;

 
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("OnAfterInstall=" + ex.Message);
//            }
//        }
 
//        public override void Uninstall(IDictionary savedState)
//        {

//            try
//            {

//                base.Uninstall(savedState);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Uninstall=" + ex.Message);
//            }



//        }

//        protected override void OnBeforeUninstall(IDictionary savedState)
//        {
//            try
//            { 
//                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
//                key = key.OpenSubKey(@"SOFTWARE\DataExcel" + "\\" + Product.AssemblyVersion, true);
//                if (key == null)
//                {
//                    return;
//                }
//                object value = null;
//                value = key.GetValue("Install", value);
//                if (value == null)
//                {
//                    return;
//                }
//                string path = value.ToString();

//                string gpath = System.IO.Path.GetDirectoryName(path) + "\\gacutil.exe";
//                if (!System.IO.File.Exists(gpath))
//                {
//                    MessageBox.Show(path + " gpath文件不存在");
//                }
//                key = Microsoft.Win32.Registry.LocalMachine;
//                key = key.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework\v2.0.50727\AssemblyFoldersEx", true);
//                if (key != null)
//                {
//                    key.DeleteSubKey("DataExcel", false);
//                }

//                System.Diagnostics.ProcessStartInfo ps = new ProcessStartInfo("\"" + System.IO.Path.GetDirectoryName(path)
//                    + "\\gacutil.exe" + "\"", "/u " + " DataExcel.v1.1");// System.IO.Path.GetFileNameWithoutExtension(path));
//                ps.CreateNoWindow = true;
//                ps.WindowStyle = ProcessWindowStyle.Hidden;
//                ps.CreateNoWindow = true;
//                ps.RedirectStandardError = true;
//                ps.RedirectStandardInput = true;
//                ps.RedirectStandardOutput = true;
//                ps.UseShellExecute = false;
//                ps.WindowStyle = ProcessWindowStyle.Hidden;
//                System.Diagnostics.Process prc = System.Diagnostics.Process.Start(ps);
 
 

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("OnBeforeUninstall=" + ex.Message);
//            }
//        }

//    }

//}
