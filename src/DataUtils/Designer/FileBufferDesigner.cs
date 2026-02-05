using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Feng.IO;

namespace Feng.Forms.Controls.Designer
{ 

    [Serializable]
    public class FileBufferDesigner : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                using (System.Windows.Forms.OpenFileDialog scs = new System.Windows.Forms.OpenFileDialog())
                {
                    scs.Filter = "*.*|*.*";
                    if (scs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        FileBuffer model = new FileBuffer();
                        model.FullName = scs.FileName;
                        System.IO.FileInfo fi = new System.IO.FileInfo(model.FullName);
                        model.Name = fi.Name;
                        model.LastAccessTime = fi.LastAccessTime;
                        model.LastWriteTime = fi.LastWriteTime;
                        model.Length = fi.Length;
                        model.DirectoryName = fi.DirectoryName;
                        model.CreationTime = fi.CreationTime;
                        model.Extension = fi.Extension;
                        model.Buffer = System.IO.File.ReadAllBytes(model.FullName);
                        return model;
                    }
                }
            }
  
            return base.EditValue(context, provider, value);
        }


    }
}
