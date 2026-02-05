using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class FileFunctionContainer : CBMethodContainer
    {

        public const string Function_Category = "FileFunction";
        public const string Function_Description = "系统文件";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public FileFunctionContainer()
        {

            BaseMethod model = new BaseMethod();
            model.Name = "FileDelete";
            model.Description = "删除文件";
            model.Eg = @"FileDelete(Cell(""A5""))";
            model.Function = FileDelete;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "FileReName";
            model.Description = "重命名文件";
            model.Eg = @"FileReName(Cell(""A5""),Cell(""A6""))";
            model.Function = FileReName;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "FileReadText";
            model.Description = "读取文本";
            model.Eg = @"FileReadText(Cell(""A5""))";
            model.Function = FileReadText;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "FileWriteText";
            model.Description = "读取文本 FileWriteText(file,text)";
            model.Eg = @"FileWriteText(file,text)";
            model.Function = FileWriteText;
            MethodList.Add(model);

            //model = new BaseMethod();
            //model.Name = "FileExec";
            //model.Description = "执行文件";
            //model.Eg = @"FileExec(Cell(""A5""))";
            //model.Function = FileExec;
            //MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DirectoryName";
            model.Description = "获取文件夹名称";
            model.Eg = @"DirectoryName(Cell(""A5""))";
            model.Function = DirectoryName;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "FileExtension";
            model.Description = "获取文件扩展名";
            model.Eg = @"FileExtension(Cell(""A5""))";
            model.Function = FileExtension;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "FileName";
            model.Description = "获取文件名称";
            model.Eg = @"FileName(Cell(""A5""))";
            model.Function = FileName;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "FileFullPath";
            model.Description = "获取文件全路径";
            model.Eg = @"FileFullPath(Cell(""A5""))";
            model.Function = FileFullPath;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "FileList";
            model.Description = "获取文件夹下面的文件列表";
            model.Eg = @"FileList(Cell(""A5""))";
            model.Function = this.FileList;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DirectoriesList";
            model.Description = "获取文件夹下面的文件夹列表";
            model.Eg = @"DirectoriesList(Cell(""A5""))";
            model.Function = this.DirectoriesList;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "FileSystemEntries";
            model.Description = "获取文件夹下面的文件与文件夹列表";
            model.Eg = @"FileSystemEntries(Cell(""A5""))";
            model.Function = this.FileSystemEntries;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "FileRoot";
            model.Description = "获取根目录";
            model.Eg = @"FileRoot(Cell(""A5""))";
            model.Function = FileRoot;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "FileRandomName";
            model.Description = "获取随机文件名";
            model.Eg = @"FileRandomName(Cell(""A5""))";
            model.Function = FileRandomName;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "FileTempPath";
            model.Description = "获取临时文件名";
            model.Eg = @"FileTempPath(Cell(""A5""))";
            model.Function = FileTempPath;
            MethodList.Add(model);



        }

        public virtual object FileName(params object[] args)
        {
            string file = base.GetTextValue(1, args);
            return System.IO.Path.GetFileName(file);
        }

        public virtual object FileRoot(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            string file = Feng.Utils.ConvertHelper.ToString(value1);
            return System.IO.Path.GetPathRoot(file);

        }

        public virtual object FileTempPath(params object[] args)
        { 
            return System.IO.Path.GetTempPath();

        }

        public virtual object FileRandomName(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            string file = Feng.Utils.ConvertHelper.ToString(value1);
            return System.IO.Path.GetRandomFileName();

        }

        public virtual object FileExtension(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            string file = Feng.Utils.ConvertHelper.ToString(value1);
            return System.IO.Path.GetExtension(file);

        }

        public virtual object DirectoryName(params object[] args)
        {
            string file = base.GetTextValue(1, args);
            return System.IO.Path.GetDirectoryName(file);
        }

        public virtual object FileList(params object[] args)
        {
            string file = base.GetTextValue(1, args);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            list.AddRange(System.IO.Directory.GetFiles(file));
            return list;
        }

        public virtual object DirectoriesList(params object[] args)
        {
            string file = base.GetTextValue(1, args);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            list.AddRange(System.IO.Directory.GetFiles(file));
            return list;
        }

        public virtual object FileSystemEntries(params object[] args)
        {
            string file = base.GetTextValue(1, args);
            System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
            list.AddRange(System.IO.Directory.GetFiles(file));
            return list;
        }

        public virtual object FileFullPath(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            string file = Feng.Utils.ConvertHelper.ToString(value1);
            return System.IO.Path.GetFullPath(file);

        }

        public virtual object FileDelete(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            string file = Feng.Utils.ConvertHelper.ToString(value1);
            if (Feng.Utils.MsgBox.ShowQuestion("确认删除文件:" + file + "？") == System.Windows.Forms.DialogResult.OK)
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                    return Feng.Utils.Constants.OK;
                }
            }
            return Feng.Utils.Constants.Fail;
        }

        public virtual object FileReName(params object[] args)
        {
            string file1 = base.GetTextValue(1, args);
            string file2 = base.GetTextValue(2, args);
            if (Feng.Utils.MsgBox.ShowQuestion("确认重命名:" + file1 + " 更改为:" + file2 + "？")
                == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.Move(file1, file2);
                return Feng.Utils.Constants.OK;
            }
            return Feng.Utils.Constants.Fail;
        }

        public virtual object FileReadText(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            string file = Feng.Utils.ConvertHelper.ToString(value1);
            if (System.IO.File.Exists(file))
            {
                return System.IO.File.ReadAllText(file);
            }

            return null;
        }

        public virtual object FileWriteText(params object[] args)
        {
            string file = base.GetTextValue(1, args);
            string text = base.GetTextValue(2, args);
            if (!System.IO.File.Exists(file))
            {
                Feng.IO.FileHelper.WriteAllText(file, text);
                return Feng.Utils.Constants.OK;
            }
            else
            {
                if (Feng.Utils.MsgBox.ShowQuestion("文件已经存在:" + file  + " 是否覆盖？") == System.Windows.Forms.DialogResult.OK)
                {
                    Feng.IO.FileHelper.WriteAllText(file, text);
                    return Feng.Utils.Constants.OK;
                }
            }
            return Feng.Utils.Constants.Fail;
        }
        //public virtual object FileExec(params object[] args)
        //{
        //    object value1 = base.GetArgIndex(1, args);
        //    object value2 = base.GetArgIndex(2, args);
        //    string file1 = Feng.Utils.ConvertHelper.ToString(value1);
        //    string argmen = Feng.Utils.ConvertHelper.ToString(value2);
        //    if (System.IO.File.Exists(file1))
        //    {
        //        System.Diagnostics.Process.Start(file1, argmen);
        //        return 1;
        //    }

        //    return null;
        //}

    }
}
