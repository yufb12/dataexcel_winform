using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Actions
{ 
    public class ReminderAction : string
    {
        public ReminderAction()
        { 

        }

        public bool Reminder { get; set; }

        public override void Read(byte[] data)
        { 
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data))
            { 
                this.Reminder = reader.ReadIndex(1, Reminder); 
            }
        }

        public override byte[] GetData()
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter stream = new Feng.IO.BufferWriter())
            { 
                stream.Write(1, Reminder);
                data = stream.GetData();
            }
            return data;
        }

    }

    public class FileAction : string
    { 
        public FileAction()
        {
            this.Action = TotalCell;
        }

        public string Path { get; set; }

        public override void Init(IBaseCell cell)
        {
            using (System.Windows.Forms.FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Path = dlg.SelectedPath;
                }
            }

            base.Init(cell);
        }

        public void TotalCell(object sender, ActionArgs e)
        {
            MessageBox.Show(Path);
        }

        public override void Read(byte[] data)
        { 
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data))
            { 
                this.Path = reader.ReadIndex(1, Path); 
            }
        }

        public override byte[] GetData()
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter stream = new Feng.IO.BufferWriter())
            { 
                stream.Write(1, Path);
                data = stream.GetData();
            }
            return data;
        }
    }

    public class FileActionContainer :  BaseActionContainer
    {
        public override string Name
        {
            get { return "File"; }
        }

        public void Save(object sender, ActionArgs e)
        {
            e.Grid.Save();
        }
        public void SaveAs(object sender, ActionArgs e)
        {
            e.Grid.SaveAs();
        }

        public void OpenFile(object sender, ActionArgs e)
        {
            e.Grid.Open(e.Action.Text);
        }
        public static string lastOpenDirctory = string.Empty;
        public void OpenDirctory(object sender, ActionArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            { 
                dlg.SelectedPath = lastOpenDirctory;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OpenDirectory(sender, e, dlg.SelectedPath, e.Cell);
                }
            }
        }
        public void OpenDirectory(object sender, ActionArgs e, string path, ICell cell)
        {
            string[] files = System.IO.Directory.GetFiles(path);
            ICell cel = cell;
            foreach (string file in files)
            {
                cel = DataExcelTool.GetDownCell(e.Grid, cel);
                cel.Value = file;
            }
            //string[] directories = System.IO.Directory.GetDirectories(path);
            //foreach (string dirc in files)
            //{
            //    OpenDirectory(sender, e, dirc, cel);
            //}
        }
        public void ListDirctory(object sender, ActionArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = lastOpenDirctory;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ListDirectory(sender, e, dlg.SelectedPath, e.Cell);
                }
            }
        }
        public void ListDirectory(object sender, ActionArgs e, string path, ICell cell)
        {
            string[] files = System.IO.Directory.GetFiles(path);
            ICell cel = cell;
             
            string[] directories = System.IO.Directory.GetDirectories(path);
            foreach (string dirc in directories)
            {
                cel = DataExcelTool.GetDownCell(e.Grid, cel);
                cel.Value =System.IO.Path.GetDirectoryName( dirc);
            }
        }
        public void ListFile(object sender, ActionArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = lastOpenDirctory;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ListFile(sender, e, dlg.SelectedPath, e.Cell);
                }
            }
        }
        public void ListFile(object sender, ActionArgs e, string path, ICell cell)
        {
            string[] files = System.IO.Directory.GetFiles(path);
            ICell cel = cell;
            foreach (string file in files)
            {
                cel = DataExcelTool.GetDownCell(e.Grid, cel);
                cel.Value = System.IO.Path.GetFileName(file);
            }
        }
        public override List<string> Actions
        {
            get
            {
                List<string> list = new List<string>();
                string ai = new FileAction();
                ai.Action = OpenFile;
                ai.Name = "OpenFile";
                ai.Description = "打开文件";
                list.Add(ai);

                ai = new string();
                ai.Action = Save;
                ai.Name = "Save";
                ai.Description = "保存文件";
                list.Add(ai);

                ai = new string();
                ai.Action = SaveAs;
                ai.Name = "SaveAs";
                ai.Description = "另存为";
                list.Add(ai);


                ai = new string();
                ai.Action = OpenDirctory;
                ai.Name = "OpenDirctory";
                ai.Description = "显示文件路径";
                list.Add(ai);

                ai = new string();
                ai.Action = ListDirctory;
                ai.Name = "ListDirctory";
                ai.Description = "显示文件夹";
                list.Add(ai);

                ai = new string();
                ai.Action = ListFile;
                ai.Name = "ListFile";
                ai.Description = "显示文件名";
                list.Add(ai);
                return list;
            }
        }
 
    }
}