using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;
using Feng.Net.Packets;
using Feng.Excel.IO;
using Feng.Net.Extend;
using Feng.IO;

namespace Feng.Net.Tools
{
    public class FileTools
    {
        public static List<PathInfo> GetDirctoryPathInfos(string path, FileServerResourceManager fsrm)
        {
            List<PathInfo> list = new List<PathInfo>();
            string[] directories = System.IO.Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                PathInfo pi = GetDICInfo(directory, fsrm);
                list.Add(pi);
            }
            string[] files = System.IO.Directory.GetFiles(path);
            foreach (string file in files)
            {
                PathInfo pi = GetFilePI(file, fsrm);
                list.Add(pi);
            }
            return list;
        }
        public static PathInfo GetFilePI(string tempfile, FileServerResourceManager fsrm)
        {
            PathInfo di = new PathInfo();

            System.IO.FileInfo fileinfo = new System.IO.FileInfo(tempfile);
            di.FullName = fsrm.SubDirectory(tempfile);
            di.DirectoryName = fsrm.SubDirectory(FileHelper.GetFileDrectory(tempfile));
            di.Name = FileHelper.GetFileName(tempfile);
            di.Size = fileinfo.Length;
            di.PathType = PathType.GetPathType(tempfile);
            di.LastWriteTime = fileinfo.LastWriteTime;
            di.CreationTime = fileinfo.CreationTime;
            return di;
        }

        public static PathInfo GetDICInfo(string dirctory, FileServerResourceManager fsrm)
        {
            PathInfo di = new PathInfo();
            System.IO.DirectoryInfo directoryinfo = new System.IO.DirectoryInfo(dirctory);
            di.FullName = fsrm.SubDirectory(dirctory);
            di.DirectoryName = FileHelper.GetDirctoryName(dirctory);
            di.Name = FileHelper.GetFileName(dirctory);
            di.Size = dirctory.Length;
            di.PathType = PathType.TYPE_DIRECTORY;
            di.LastWriteTime = directoryinfo.LastWriteTime;
            di.CreationTime = directoryinfo.CreationTime;
            string[] directoriestemp = System.IO.Directory.GetDirectories(dirctory);
            if (directoriestemp.Length > 0)
            {
                di.HasDirctory = true;
            }
            directoriestemp = System.IO.Directory.GetFiles(dirctory);
            if (directoriestemp.Length > 0)
            {
                di.HasFile = true;
            }
            return di;
        }
    }

}
