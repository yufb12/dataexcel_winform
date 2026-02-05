using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;
using System.Collections.Specialized;

namespace Feng.Forms
{ 
    public class ClipboardHelper
    {
        public static IDataObject GetDataObject()
        {
            IDataObject idata = System.Windows.Forms.Clipboard.GetDataObject();
            return idata;
        }
        public static StringCollection GetFileDropList()
        {
            StringCollection files = System.Windows.Forms.Clipboard.GetFileDropList();
            return files;
        }
        public static object GetData(string format)
        {
            object data = Clipboard.GetData(format);
            return data;
        }
        public static bool ContainsFileDropList()
        {
            return Clipboard.ContainsFileDropList();
        }
        public static void SetClip(string text)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(text);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("SYSTEM", "CLIPBOARD", "SETTEXT",ex);
            }
        }
        public static void SetText(string txt)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(txt);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("SYSTEM", "CLIPBOARD", "SETTEXT", ex);
            }
        }
        public static string GetText()
        {
            return System.Windows.Forms.Clipboard.GetText();
        }      
        public static Image GetImage()
        {
            return System.Windows.Forms.Clipboard.GetImage();
        }
        public static void Clear()
        {
            System.Windows.Forms.Clipboard.Clear();
        }

        public static void SetClipDataObject(string text, string format,object data)
        {
            var dataObject = new DataObject();
            dataObject.SetData(DataFormats.Text, true, text);
            dataObject.SetData(format, false, data);
            Clipboard.SetDataObject(dataObject, true);
           object value = Clipboard.GetData(format);
        }

        public static object GetClipDataObject(string format)
        {
            object data = Clipboard.GetData(format);
            return data;
        }
    }
}
