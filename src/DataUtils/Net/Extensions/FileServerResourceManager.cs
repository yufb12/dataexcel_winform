using Feng.Net.Interfaces;
using System;
using System.Collections.Generic;

namespace Feng.Net.Extend
{
    public interface IFilePermissionManager
    {
        bool HasPermission(string userid, string role, string path, string permission);
        void Clear();
    }
    public class FileServerResourceManager
    {
        public FileServerResourceManager()
        { 
        }
        private string root = string.Empty;
        public string Root
        {
            get { return root; }
            set { root = value; }
        }
        private string loginpath = "系统信息\\用户管理\\登录.fext";
        public string LoginPath
        {
            get { return loginpath; }
            set { loginpath = value; }
        }

        public virtual string BackFilePath { get; set; }
        public virtual string UserFilePath { get; set; }
        public virtual string DataFilePath { get; set; }
        public virtual string LogFilePath { get; set; }
        public virtual string RecyFilePath { get; set; }

        public virtual string BackFilePathShort { get; set; }
        public virtual string UserFilePathShort { get; set; }
        public virtual string DataFilePathShort { get; set; }
        public virtual string LogFilePathShort { get; set; }
        public virtual string RecyFilePathShort { get; set; }

        public bool IsUserDirectory(string user,string path)
        {
            string userpath = "\\" + UserFilePathShort + "\\"+ user+ "\\";
            if (path.StartsWith(userpath))
            {
                return true;
            }
            return false;
        }

        public string GetServerUserPath(string user)
        {
            return "\\" + user;
        }
        public string GetUserPath(string user)
        {
            return UserFilePath + "\\" + user;
        }
        public string GetUserPathFav(string user)
        {
            return UserFilePath + "\\" + user + "\\收藏";
        }
        public string GetUserPathTodo(string user)
        {
            return UserFilePath + "\\" + user + "\\待办";
        }
        public string GetUserPathRecv(string user)
        {
            return UserFilePath + "\\" + user + "\\收到";
        }
        public string GetUserPathSend(string user)
        {
            return UserFilePath + "\\" + user + "\\发送";
        }
        public string GetUserPathFocu(string user)
        {
            return UserFilePath + "\\" + user + "\\关注";
        }
        public string GetUserPathFile(string user)
        {
            return UserFilePath + "\\" + user + "\\文件";
        }
        public virtual string GetPath(string path)
        {
            string p = path.Replace('/', '\\');
            string file = Feng.IO.FileHelper.Combine(Root, p);
            return file;
        }
        public virtual string GetUrl(string path)
        {
            if (path.Length > Root.Length)
            {
                return "\\" + path.Substring(Root.Length+1, path.Length - Root.Length-1);
            }
            return string.Empty;
        }
        public string SubDirectory(string path)
        {
            if (path.Length > Root.Length)
            {
                return path.Substring(Root.Length, path.Length - Root.Length);
            }
            return string.Empty;
        }
        private static int CopyFileIndex = 10000;
        public virtual void BackFile(string path,string user)
        {
            try
            {

                if (System.IO.File.Exists(path))
                {
                    if (!string.IsNullOrWhiteSpace(BackFilePath))
                    {
                        string userid = user;
                        if (string.IsNullOrWhiteSpace(userid))
                        {
                            userid = "000000";
                        }
                        string desfile = Feng.IO.FileHelper.Combine(BackFilePath, this.SubDirectory(path));
                        string filename = System.IO.Path.GetFileName(desfile);
                        desfile = Feng.IO.FileHelper.Combine(desfile, DateTime.Now.ToString("yyyyMM"));
                        desfile = Feng.IO.FileHelper.Combine(desfile,
                                     "_Date" + DateTime.Now.ToString("yyyyMMddHHmmssff")
                                   + "_" + (CopyFileIndex++).ToString()) + "_User" + userid + "_" + filename;
                        Feng.IO.FileHelper.CopyFile(path, desfile);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log("BackFile", ex);
            }
        }

        public virtual List<string> GetBackFiles(string path)
        {
            List<string> list = new List<string>();
            try
            {
                if (!string.IsNullOrWhiteSpace(BackFilePath))
                {
                    string desfile = Feng.IO.FileHelper.Combine(BackFilePath, path);
                    string filename = System.IO.Path.GetFileName(desfile);
                    if (System.IO.Directory.Exists(desfile))
                    {
                        string[] files = System.IO.Directory.GetFiles(desfile);
                        foreach (string item in files)
                        {
                            list.Add(item);
                        }
                        string[] directories = System.IO.Directory.GetDirectories(desfile);
                        foreach (string directory in directories)
                        {
                            files = System.IO.Directory.GetFiles(directory);
                            foreach (string item in files)
                            {
                                list.Add(item);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log("BackFile", ex);
            }
            return list;
        }

        //public bool WriteFile(string path, byte[] data)
        //{
        //    string file = GetPath(path);
        //    BackFile(file);
        //    Feng.IO.FileHelper.WriteFile(file, data);
        //    return true;
        //}
        private RecycleBinManager recyclebinmanager = null;
        public RecycleBinManager RecycleBinManager
        {
            get
            {
                if (recyclebinmanager == null)
                {
                    recyclebinmanager = new RecycleBinManager();
                    recyclebinmanager.Path = Feng.IO.FileHelper.Combine(Root, "Recyclebin");
                 
                }
                return recyclebinmanager;
            }
        }
         
    }
    public class RecycleBinManager
    {
        public string Path { get; set; }
        public bool AddFile(string file)
        {
            string filename = string.Empty;
            try
            {
                string path = Feng.IO.FileHelper.Combine(Path, DateTime.Now.ToString("yyyyMMdd"));
                filename = Feng.IO.FileHelper.GetFileName(file)+ DateTime.Now.ToString("yyyyMMdHHmmssfff");
                string targetfile = Feng.IO.FileHelper.Combine(path, filename);
                Feng.IO.FileHelper.ExistAndCreateDirectory(targetfile);
                string tempfile = Feng.IO.FileHelper.Combine(path, filename) + ".rec";
                Feng.IO.FileHelper.Move(targetfile, file);
                System.IO.File.WriteAllText(tempfile, file, System.Text.Encoding.Unicode);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool AddFile(string file, System.Text.StringBuilder sb)
        {
            string filename = string.Empty;
            try
            {
                string path = Feng.IO.FileHelper.Combine(Path, DateTime.Now.ToString("yyyyMMdHHmmssfff"));
                filename = Feng.IO.FileHelper.GetFileName(file);
                string targetfile = Feng.IO.FileHelper.Combine(path, filename);
                Feng.IO.FileHelper.ExistAndCreateDirectory(targetfile);
                string tempfile = Feng.IO.FileHelper.Combine(path, filename) + ".rec";
                Feng.IO.FileHelper.Move(targetfile, file);
                System.IO.File.WriteAllText(tempfile, file, System.Text.Encoding.Unicode);
                return true;
            }
            catch (Exception ex)
            {
                sb.AppendLine(filename);
                return false;
            }

        }
        public bool AddDirctory(string path, System.Text.StringBuilder sb)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(path);
                foreach (string file in files)
                {
                    AddFile(file, sb);
                }
                string[] dires = System.IO.Directory.GetDirectories(path);
                foreach (string path1 in files)
                { 
                    AddDirctory(path1, sb); 
                }
                System.IO.Directory.Delete(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddDirctory(string path)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(path);
                foreach (string file in files)
                {
                    AddFile(file);
                }
                string[] dires = System.IO.Directory.GetDirectories(path);
                foreach (string path1 in dires)
                {
                    AddDirctory(path1);
                }
                System.IO.Directory.Delete(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class FileServerPermissionManager: IFilePermissionManager
    {
        public void Clear()
        {
        }

        public bool HasPermission(string userid, string role, string path, string permission)
        {
            return true;
        }
    }

    public class ServerManager
    {
        public string GetUser(IClientProxy sockethelper)
        {
            return string.Empty;
        }
    }

}