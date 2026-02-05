using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class FormFunctionContainer : CBMethodContainer
    {

        public const string Function_Category = "FormFunction";
        public const string Function_Description = "窗口函数";

        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }

        }
        public FormFunctionContainer()
        {

            BaseMethod model = new BaseMethod();
            model.Name = "ShowMessage";
            model.Description = "显示提示信息";
            model.Eg = @"ShowMessage(""显示内容"",""标题"")";
            model.Function = ShowMessage;
            model.IsMethod = true;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ShowMessageBox";
            model.Description = "显示确认信息";
            model.Eg = @"ShowMessageBox(""显示内容"",""标题"")";
            model.Function = ShowMessageBox;
            model.IsMethod = true;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "OpenFileDialog";
            model.Description = "打开选择文件对话框 OpenFileDialog(filter,mulselect,checkexits)";
            model.Eg = @"OpenFileDialog(CELL(""A5""),OpenFileDialog())";
            model.Function = OpenFileDialog;
            MethodList.Add(model);
 

            model = new BaseMethod();
            model.Name = "OpenFolderDialog";
            model.Description = "打开选择文件夹对话框";
            model.Eg = @"OpenFolderDialog(CELL(""A5""),OpenFolderDialog())";
            model.Function = OpenFolderDialog;
            MethodList.Add(model);


        }

 
        public virtual object OpenFolderDialog(params object[] args)
        {
            string filter = base.GetTextValue(1, args);
            using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return dlg.SelectedPath;
                }
            }
            return null;
        }
 
        public virtual object OpenFileDialog(params object[] args)
        {
            string filter = base.GetTextValue(1, args);
            bool mulsel = base.GetBooleanValue(2, args);
            bool checkexit= base.GetBooleanValue(3, args);
            using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
            {
                dlg.Filter = filter;
                dlg.Multiselect = mulsel;
                dlg.CheckFileExists = checkexit;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (mulsel)
                    {
                        return dlg.FileNames;
                    }
                    else
                    {
                        return dlg.FileName;
                    }
                }
            }
            return null;
        }

        public virtual object ShowMessage(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            string caption = base.GetText(2, Feng.DataUtlis.Product.DialogCaption, args);
            System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK);
            return Feng.Utils.Constants.OK;
        }

        public virtual object ShowMessageBox(params object[] args)
        {
            string text = Feng.Utils.ConvertHelper.ToString(this.GetArgIndex(1, args));
            string caption = Feng.Utils.ConvertHelper.ToString(this.GetArgIndex(2, args), Feng.DataUtlis.Product.DialogCaption);
            if (System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                return Feng.Utils.Constants.OK;
            }
            return Feng.Utils.Constants.Cancel;
        }
 

    }
}
