using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Feng.IO
{
    public class FileHelper
    {
        public const string USERDATA = "USER";
        public static string StartupPath { get; set; }
        public static string StartupPathUserData
        {
            get
            {
                if (string.IsNullOrEmpty(StartupPath))
                {
                    StartupPath = Application.StartupPath;
                    return System.IO.Path.Combine(Application.StartupPath, USERDATA);
                }

                return System.IO.Path.Combine(StartupPath, USERDATA);
            }
        }

        public static string ReadAllText(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                return string.Empty;
            }
            return System.IO.File.ReadAllText(file, System.Text.Encoding.UTF8);
        }
        public static void WriteAllText(string file,string txt)
        {
            if (!System.IO.File.Exists(file))
            {
                CreatDirctory(file);
            }
            System.IO.File.WriteAllText(file, txt, System.Text.Encoding.UTF8);
        }
        public static byte[] ReadFile(string file)
        {
            if (System.IO.File.Exists(file))
            {
                return System.IO.File.ReadAllBytes(file);
            }
            return null;
        }

        public static bool WriteFile(string file, byte[] data)
        {
            using (System.IO.FileStream fs = new FileStream(file, FileMode.Create))
            {
                fs.Write(data, 0, data.Length);
            }
            return true;
        }

        public static bool AppendFile(string file, byte[] data)
        {
            using (System.IO.FileStream fs = new FileStream(file, FileMode.Append))
            {
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
            return true;
        }
        public static bool WriteFile(string file, string txt)
        {
            using (System.IO.FileStream fs = new FileStream(file, FileMode.Create))
            {
                byte[] data = System.Text.Encoding.Unicode.GetBytes(txt);
                fs.Write(data, 0, data.Length);
            }
            return true;
        }
        public static bool CopyFile(string sourceFileName, string destFileName)
        {
            try
            {
                CreatDirctory(destFileName);
                System.IO.File.Copy(sourceFileName, destFileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string GetStartUpFile(string filename)
        {
            filename = filename.TrimStart('\\'); 

            string file = System.IO.Path.Combine(StartupPathUserData, filename);
            CreatDirctory(file);
            return file;
        }
        public static string GetStartUpFileUSER(string path,string filename)
        {
            filename = filename.TrimStart('\\');
            path = path.TrimStart('\\');

            string file = System.IO.Path.Combine(StartupPathUserData, path, filename); 
            CreatDirctory(file);
            return file;
        }
        public static string GetStartUpDirctoryUSER(string path, string filename)
        {
            filename = filename.TrimStart('\\');
            path = path.TrimStart('\\'); 
            string file = System.IO.Path.Combine(StartupPathUserData, path, filename);
            if (!System.IO.Directory.Exists(file))
            {
                System.IO.Directory.CreateDirectory(file);
            }
            return file;
        }
        public static string GetStartUpFile(string path1, string path2, string filename)
        {
            filename = filename.TrimStart('\\');
            path1 = path1.TrimStart('\\');
            path2 = path2.TrimStart('\\');
            string file = System.IO.Path.Combine(StartupPathUserData, path1, path2, filename); 
            CreatDirctory(file);
            return file;
        }
        public static string CreatDirctory(string file)
        {
            string dic = System.IO.Path.GetDirectoryName(file);
            if (!System.IO.Directory.Exists(dic))
            {
                System.IO.Directory.CreateDirectory(dic);
            }
            return dic;
        }
        public static string GetFileDircotryName(string file)
        {
            string dircotry = System.IO.Path.GetDirectoryName(file);
            return System.IO.Path.GetFileName(dircotry);
        }
        public static string GetFileDircotry(string file)
        { 
            return System.IO.Path.GetDirectoryName(file);
        }
        public static List<string> GetFiles(string path)
        {
            List<string> files = new List<string>();
            List<string> dirs = GetDirectorys(path);
            dirs.Add(path);
            foreach (string dir in dirs)
            {
                files.AddRange(System.IO.Directory.GetFiles(dir)); 
            }
            return files;
        }
        public static List<string> GetAllFiles(string path,int count)
        {
            List<string> files = new List<string>();
            GetDiretoryFiles(path, files, count);
            return files;
        }
        public static void GetDiretoryFiles(string path, List<string>list, int count)
        {
            string[] files = System.IO.Directory.GetFiles(path);
            list.AddRange(files);
            string[] directories = System.IO.Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                if (list.Count > count)
                {
                    return;
                }
                GetDiretoryFiles(directory, list, count);

            }
        }
        public static List<string> GetDirectorys(string path)
        {
            List<string> list = new List<string>();
            string[] dirs = System.IO.Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                list.Add(dir);
                list.AddRange(GetDirectorys(dir).ToArray());
            }
            return list;
        }
        public static void WriteAllBytes(string file, byte[] data)
        {
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(file)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(file));
            }
            WriteFile(file, data);
        }
        public static byte[] ReadFilePart(string filename, long offset, int length, out int read, out long filelength)
        {
            if (System.IO.File.Exists(filename))
            {
                FileStream fs = System.IO.File.Open(filename, FileMode.Open);
                filelength = fs.Length;
                fs.Seek(offset, SeekOrigin.Begin);
                byte[] data = new byte[length];
                read = fs.Read(data, 0, length);
                fs.Close();
                return data;
            }
            read = 0;
            filelength = 0;
            return null;
        }
        public static void DeleteFiles(string orgpath)
        {
            if (System.IO.Directory.Exists(orgpath))
            {
                string[] files = System.IO.Directory.GetFiles(orgpath);
                foreach (string file in files)
                {
                    System.IO.File.Delete(file);
                }
                string[] dires = System.IO.Directory.GetDirectories(orgpath);
                foreach (string dir in dires)
                {
                    DeleteFiles(dir);
                }
            }
        }
        public static void CopyFiles(string orgpath, string despath)
        {
            if (System.IO.Directory.Exists(orgpath))
            {
                string[] files = System.IO.Directory.GetFiles(orgpath);
                foreach (string file in files)
                {
                    string filename = System.IO.Path.GetFileName(file);
                    string desfile = despath + "\\" + filename;
                    if (System.IO.File.Exists(desfile))
                    {
                        System.IO.File.Delete(desfile);
                    }
                    System.IO.File.Move(file, desfile);
                }
                string[] dires = System.IO.Directory.GetDirectories(orgpath);
                foreach (string dir in dires)
                {
                    int index = dir.LastIndexOf("\\");
                    int len = dir.Length - index;
                    if (len > 0 && index > 0)
                    {
                        string dirname = dir.Substring(index + 1, dir.Length - index - 1);
                        string desdir = despath + "\\" + dirname;
                        if (!System.IO.Directory.Exists(desdir))
                        {
                            System.IO.Directory.CreateDirectory(desdir);
                        }
                        CopyFiles(dir, desdir);
                    }
                }
            }
        }
        public static bool IsImageFile(string file)
        {
            if (file.ToLower().EndsWith("bmp"))
            {
                return true;
            } 
            if (file.ToLower().EndsWith("gif"))
            {
                return true;
            }
            if (file.ToLower().EndsWith("jpg"))
            {
                return true;
            }
            if (file.ToLower().EndsWith("jpeg"))
            {
                return true;
            }
            if (file.ToLower().EndsWith("png"))
            {
                return true;
            }
            return false;
        }
 
        public static object WriteFileByThreadEvents = new object();
        //public class FileCacheWrite
        //{
        //    public string File { get; set; }
        //    public long FileLength { get; set; }
        //    public Dictionary<long, byte[]> Dics = new Dictionary<long, byte[]>();
        //    private long Length = 0;
        //    public void Add(long offset, byte[] data)
        //    { 
        //        try
        //        {
        //            lock (WriteFileByThreadEvents)
        //            {
        //                lasttime = DateTime.Now;
        //                Length = Length + data.Length;
        //                Dics.Add(offset, data);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Feng.Utils.ExceptionHelper.ShowError(ex);
        //        } 
        //    }
 
        //    private DateTime lasttime = DateTime.Now;
        //    public void WriteToFile()
        //    {
        //        lock (WriteFileByThreadEvents)
        //        {
        //            if (FileLength == Length)
        //            { 
        //                WriteFile(File, Dics);
        //                Dics.Clear();
        //                CacheFileList.Remove(this.File);
        //            }
        //            else
        //            {
        //                if (Length > CacheFileSize)
        //                {
        //                    WriteFile(File, Dics);
        //                    Dics.Clear();
        //                }
        //            } 
        //        }
        //    }
           
        //}
        public static void WriteFile(string file, Dictionary<long, byte[]> dics)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.OpenOrCreate))
            {
                foreach (KeyValuePair<long, byte[]> key in dics)
                {
                    fs.Seek(key.Key, System.IO.SeekOrigin.Begin);
                    fs.Write(key.Value, 0, key.Value.Length);
                }
                fs.Flush();
                fs.Close();
            }
        }
        public const int CacheFileSize = 1024 * 1024 * 5;
        //public static Dictionary<string, FileCacheWrite> CacheFileList = new Dictionary<string, FileCacheWrite>();
        public static void WriteFileByThread(string file, long offset, long filelength, byte[] data)
        { 
            //try
            //{
            //    FileCacheWrite fc = null;

            //    if (CacheFileList.ContainsKey(file))
            //    {
            //        fc = CacheFileList[file];
            //        fc.Add(offset, data);
            //    }
            //    else
            //    {
            //        fc = new FileCacheWrite();
            //        fc.File = file;
            //        fc.FileLength = filelength;
            //        fc.Add(offset, data);
            //        CacheFileList.Add(file, fc);
            //    }
            //    fc.WriteToFile(); 
            //}
            //catch (Exception ex)
            //{
            //    Feng.Utils.ExceptionHelper.ShowError(ex);
            //}
        
          
            //return;
 
            lock (WriteFileByThreadEvents)
            {
                string directory = System.IO.Path.GetDirectoryName(file);
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
                using (System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.OpenOrCreate))
                {
                    fs.Seek(offset, System.IO.SeekOrigin.Begin);
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    fs.Close();
                }
            } 
 
        }
        public static Image GetImageFromImageSelectDialog(out string filename)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = Feng.IO.FileFilter.JpgPng;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    Image img = Feng.IO.BufferReader.GetImage(filename);
                }
            }
            filename = string.Empty;
            return null;
        }
 
        public static string constDecrypt = "_解密";
        public static string constEncrypt = "_加密";
        public const int constFileChangedModeCreate = 1;
        public const int constFileChangedModeDelete = 2;
        public const int constFileChangedModeChanged = 3;
        public const int constFileChangedModeReName = 4;
 
        public static bool EncryptFile(string file, string user, string password)
        {
            if (System.IO.File.Exists(file))
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(file);
                string Extension = System.IO.Path.GetExtension(file);
                string Directory = System.IO.Path.GetDirectoryName(file);
                string path = Directory + "\\" + filename + constEncrypt + Extension;
                int readlen=8*1024*1024;
                using (FileStream fsRead = System.IO.File.Open(file, FileMode.Open))
                {
                    using (FileStream fsWrite = System.IO.File.Open(path, FileMode.OpenOrCreate))
                    {
                        using (Feng.IO.BufferReader reader = new BufferReader(fsRead))
                        {
                            using (Feng.IO.BufferWriter bw = new BufferWriter(fsWrite))
                            {
                                bw.Write(constEncrypt);
                                bw.Write(DateTime.Now);
                                bw.Write(Feng.App.Systeminfo.Mac);
                                bw.Write(user);
                                bw.Write(password.Length);
                                string epwd = Feng.IO.DEncrypt.Encrypt(password, password);
                                bw.Write(epwd);
                                bw.Write(string.Empty);
                                byte[] data = null;
                                while (!reader.IsEnd())
                                {
                                    data = reader.ReadBytes(readlen);
                                    data = Feng.IO.DEncrypt.Encrypt(data, password);
                                    bw.Write(data);
                                    bw.Flush();
                                }
                            }
                        }
                        fsWrite.Close();
                    }
                    fsRead.Close();
                }
                return true;
            }
            return false;
        }

        public static bool DecryptFile(string file, string password)
        {
            if (System.IO.File.Exists(file))
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(file);
                string Extension = System.IO.Path.GetExtension(file);
                string Directory = System.IO.Path.GetDirectoryName(file);
                filename = filename.Substring(0,filename.Length - constEncrypt.Length  );
                string path = Directory + "\\" + filename + Extension;
                using (FileStream fsRead = System.IO.File.Open(file, FileMode.Open))
                {
                    using (FileStream fsWrite = System.IO.File.Open(path, FileMode.OpenOrCreate))
                    {
                        using (Feng.IO.BufferReader reader = new BufferReader(fsRead))
                        {
                            using (Feng.IO.BufferWriter bw = new BufferWriter(fsWrite))
                            {
                                string Encrypt = reader.ReadString();
                                if (Encrypt == constEncrypt)
                                {
                                    DateTime dt = reader.ReadDateTime();
                                    string mac = reader.ReadString();
                                    string user = reader.ReadString();
                                    int len = reader.ReadInt32();
                                    string epwd = reader.ReadString();
                                    if (epwd == Feng.IO.DEncrypt.Encrypt(password, password))
                                    {
                                        string emp = reader.ReadString();
                                        byte[] data = null;
                                        while (!reader.IsEnd())
                                        {
                                            data = reader.ReadBytes();
                                            data = Feng.IO.DEncrypt.Decrypt(data, password);
                                            bw.Write(data);
                                            bw.Flush();
                                        }
                                    }
                                }
                            }
                        }
                        fsWrite.Close();
                    }
                    fsRead.Close();
                }
                return true;
            }
            return false;
        }

        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public static int DeleteFileToRecycler(string File)
        {
            Feng.Utils.UnsafeNativeMethods._SHFILEOPSTRUCT pm = new Utils.UnsafeNativeMethods._SHFILEOPSTRUCT();
            pm.wFunc = Feng.Utils.UnsafeNativeMethods.FO_DELETE;
            pm.pFrom = File + '\0';
            pm.pTo = null;
            pm.fFlags = Feng.Utils.UnsafeNativeMethods.FOF_ALLOWUNDO | Feng.Utils.UnsafeNativeMethods.FOF_NOCONFIRMATION;
            return Feng.Utils.UnsafeNativeMethods.SHFileOperation(pm);
        }

        public static string GetExtension(string file)
        {
            return System.IO.Path.GetExtension(file).ToLower();
        }
        public static string Combine(string path1, string path2)
        {
            if (string.IsNullOrWhiteSpace(path1))
                return path2;
            if (path1.EndsWith("\\"))
            {
                if (path2.StartsWith("\\"))
                {
                    return path1.TrimEnd('\\') + path2;
                }
            }
            else
            {
                if (!path2.StartsWith("\\"))
                {
                    return path1 + "\\" + path2;
                }
            }
            return path1 + path2;
        }
        public static bool CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            string directory = path;
            if (!System.IO.Directory.Exists(directory))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ExistAndCreateDirectory(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                return false;
            }
            string directory = System.IO.Path.GetDirectoryName(file);
            if (!System.IO.Directory.Exists(directory))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public static string SubDirectory(string root, string path)
        {
            if (path.Length > root.Length)
            {
                return path.Substring(root.Length, path.Length - root.Length);
            }
            return string.Empty;
        }

        public static string GetDirctoryName(string dirctory)
        {
            string name = System.IO.Path.GetDirectoryName(dirctory);

            return name;
        }

        public static string GetFileDrectory(string file)
        {
            return System.IO.Path.GetDirectoryName(file);
        }

        public static string GetFileName(string dirctory)
        {
            string name = System.IO.Path.GetFileName(dirctory);
            return name;
        }

        public static string GetFileNameWithoutExtension(string file)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(file);
            return name;
        }

        internal static void Move(string targetfile, string file)
        {
            System.IO.File.Move(file, targetfile);
        }

        public static void DeleteToRecycleBin(string filePath)
        {
            FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        }
    }
}
