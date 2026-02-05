using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Feng.Utils
{

    public static class RegeditHelper
    {
        public const string LastOpenFilePath = "LastOpenFilePath";

        public static void SetLastOpenFilePath(string cate, string value)
        {
            Microsoft.Win32.RegistryKey key = System.Windows.Forms.Application.UserAppDataRegistry;
            Microsoft.Win32.RegistryKey key2 = key.OpenSubKey(LastOpenFilePath, true);
            if (key2 == null)
            {
                key2 = key.CreateSubKey(LastOpenFilePath, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            if (key2 != null)
            {
                key2.SetValue(cate, value);
            }
            key2.Close();
        }

        public static string GetLastOpenFilePath(string cate)
        {
            Microsoft.Win32.RegistryKey key = System.Windows.Forms.Application.UserAppDataRegistry;
            Microsoft.Win32.RegistryKey key2 = key.OpenSubKey(LastOpenFilePath);
            object value = null;
            if (key2 != null)
            {
                value = key2.GetValue(cate, value);
            }
            if (value == null)
            {
                return string.Empty;
            }
            return value.ToString();
        }

        /// <summary>
        /// 读取注册表值
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static object GetRegEditData(string strName)
        {
            object registData = null;
            Microsoft.Win32.RegistryKey key = Feng.App.Systeminfo.ApplicationRegistryKey;
            string[] valuenames = key.GetValueNames();
            foreach (string name in valuenames)
            {
                if (name == strName)
                {
                    registData = key.GetValue(strName);
                }
            }

            return registData;
        }

        /// <summary>
        /// 写入注册表
        /// </summary>
        /// <param name="strName"></param>
        public static void SetRegEditData(string strName, object strValue)
        {
            Feng.App.Systeminfo.ApplicationRegistryKey.SetValue(strName, strValue);
        }

        /// <summary>
        /// 修改注册表项
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        public static void ModifyRegEditData(string strName, string strValue)
        {
            Feng.App.Systeminfo.ApplicationRegistryKey.SetValue(strName, strValue);

        }

        /// <summary>
        /// 判断指定注册表项是否存在
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static bool IsExist(string strName)
        {
            bool exit = false;
            string[] subkeyNames;

            subkeyNames = Feng.App.Systeminfo.ApplicationRegistryKey.GetValueNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == strName)
                {
                    exit = true;
                    return exit;
                }
            }
            return exit;
        }

        public static byte[] GetRegEditDataBytes(string strName)
        {
            byte[] registData = null;

            Microsoft.Win32.RegistryKey key = Feng.App.Systeminfo.ApplicationRegistryKey;
            string[] valuenames = key.GetValueNames();
            foreach (string name in valuenames)
            {
                if (name == strName)
                {
                    registData = (byte[])key.GetValue(strName);
                }
            }
            return registData;

        }
        /// <summary>
        /// 写入注册表
        /// </summary>
        /// <param name="strName"></param>
        public static void SetRegEditDataByts(string strName, byte[] strValue)
        {
            Feng.App.Systeminfo.ApplicationRegistryKey.SetValue(strName, strValue);

        }

        /// <summary>
        /// 读取注册表值
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        
        public static string GetRegEditDataString(string strName)
        {
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    if (strName == "A1002")
            //        return "8+vw0oVEJitmoBcn1uvYIzUSRijiH+JTEPoNlYpV05k=";
            //    return "O7Fk/Oc4erFuXgMjA1Y0WA==";
            //}

            string registData = string.Empty;

            Microsoft.Win32.RegistryKey key = Feng.App.Systeminfo.ApplicationRegistryKey;
            string[] valuenames = key.GetValueNames();
            foreach (string name in valuenames)
            {
                if (name == strName)
                {
                    registData = key.GetValue(strName).ToString();
                }
            }


            return registData;
        }

        /// <summary>
        /// 写入注册表
        /// </summary>
        /// <param name="strName"></param>
        public static void SetRegEditDataString(string strName, string strValue)
        {
            try
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    return;
                }
                Feng.App.Systeminfo.ApplicationRegistryKey.SetValue(strName, strValue);
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }


        }
    }
}
