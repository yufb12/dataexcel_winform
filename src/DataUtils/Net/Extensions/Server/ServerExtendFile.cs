using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using Feng.Net.Tcp;
using Feng.Net.Packets;
using Feng.Net.NetArgs;
using Feng.Project;

namespace Feng.Net.Extend
{

    public class ServerExtendFile : ServerExtendBase
    {
        public ServerExtendFile(Base.NetServer server) : base(server)
        {

        }
        //public ServerExtendFile()
        //{

        //}
        public ServerExtendFile(Base.NetServer server,IFilePermissionManager fspm):base(server)
        {
            _fspm = fspm;
        }
        private FileServerResourceManager _fsrm = null;
        public FileServerResourceManager FSRM
        {
            get
            {
                if (_fsrm == null)
                {
                    _fsrm = new FileServerResourceManager();
                }
                return _fsrm;
            }
        }
        private ServerManager _ServerManager = null;
        public ServerManager ServerManager
        {
            get
            {
                if (_ServerManager == null)
                {
                    _ServerManager = new ServerManager();
                }
                return _ServerManager;
            }
        }
        private IFilePermissionManager _fspm = null;
        public IFilePermissionManager FSPM
        {
            get
            {
                if (_fspm == null)
                {
                    _fspm = new FileServerPermissionManager();
                }
                return _fspm;
            }
        }

        public void Init(FileServerResourceManager fsrm)
        {
            _fsrm = fsrm;
        } 

        public override void DoExtendCommand(NetPacket ph, ReciveEventArgs e)
        {
            if (ph.PacketMainCommand != PacketMainCmd.File)
                return;
            int uc = ph.PacketSubcommand;
            switch (uc)
            { 
                case PacketFileSubCmd.CreatePath:
                    CreatePath(ph, e);
                    break;
                case PacketFileSubCmd.GetDirectorys:
                    GetDirectorys(ph, e);
                    break;
                case PacketFileSubCmd.GetFiles:
                    GetFiles(ph, e);
                    break;
                case PacketFileSubCmd.GetFileInfo:
                    GetFileInfo(ph, e);
                    break;
                case PacketFileSubCmd.DeleteDirectory:
                    DeleteDirectory(ph, e);
                    break;
                case PacketFileSubCmd.DeleteFile:
                    DeleteFile(ph, e);
                    break;
                case PacketFileSubCmd.MoveDirectorye:
                    MoveDirectorye(ph, e);
                    break;
                case PacketFileSubCmd.EncryptFile:
                    EncryptFile(ph, e);
                    break; 
                case PacketFileSubCmd.DecryptFile:
                    DecryptFile(ph, e);
                    break;
                case PacketFileSubCmd.ExistsFile:
                    ExistsFile(ph, e);
                    break; 
                case PacketFileSubCmd.GetDirectoryInfo:
                    GetDirectoryInfo(ph, e);
                    break;
                case PacketFileSubCmd.MoveFile:
                    MoveFile(ph, e);
                    break;
                case PacketFileSubCmd.CopyFile:
                    CopyFile(ph, e);
                    break;
                case PacketFileSubCmd.DownloadFile:
                    DownloadFile(ph, e);
                    break; 
                case PacketFileSubCmd.DownLoadFilePart:
                    DownLoadFilePart(ph, e);
                    break; 
                case PacketFileSubCmd.CreateFile:
                    OnCreateFile(ph, e);
                    break;
                case PacketFileSubCmd.EditFile:
                    OnEditFile(ph, e);
                    break;
                default:
                    break;
            }
        }

        public virtual void OnEditFile(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string path = br.ReadIndex(1, string.Empty);
                    byte[] data = br.ReadIndex(2, Feng.Utils.Constants.EmptyData);
                    string rootpath = Feng.IO.FileHelper.GetFileDircotry(path);
                    if (HasPermission(e, rootpath, PermissionEnum.chkFileEdit))
                    {
                        path = this.FSRM.GetPath(path);
                        string userid = GetUserID(e);
                        this.FSRM.BackFile(path, userid);

                        //Server.FSRM.BackFile(path);

                        Feng.IO.FileHelper.WriteFile(path, data);
                        result = true;
                    }
                    ph.PacketContents = BitConverter.GetBytes(result);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void OnCreateFile(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string path = br.ReadIndex(1, string.Empty);
                    byte[] data = br.ReadIndex(2, Feng.Utils.Constants.EmptyData);
                    string rootpath = Feng.IO.FileHelper.GetFileDircotry(path);
                    if (HasPermission(e, rootpath, Project.PermissionEnum.chkFileNew))
                    {
                        path = this.FSRM.GetPath(path);
                        Feng.IO.FileHelper.WriteFile(path, data);
                        result = true;
                    }
                    ph.PacketContents = BitConverter.GetBytes(result);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public string GetRole(ReciveEventArgs e)
        {
            Feng.Json.FJson user = e.ClientProxy.Tag as Feng.Json.FJson;
            string role = string.Empty;
            string userid = string.Empty;
            if (user != null)
            {
                role = Feng.Utils.ConvertHelper.ToString(user[UserVar.RoleID].BaseValue); 
            }
            return role;
        }

        public string GetUserID(ReciveEventArgs e)
        {
            Feng.Json.FJson user = e.ClientProxy.Tag as Feng.Json.FJson; 
            string userid = string.Empty;
            if (user != null)
            { 
                userid = Feng.Utils.ConvertHelper.ToString(user[UserVar.UserID].BaseValue);
            }
            return userid;
        }

        public bool HasPermission(ReciveEventArgs e, string path,string permission)
        {
            Feng.Json.FJson user = e.ClientProxy.Tag as Feng.Json.FJson;
            string role = string.Empty;
            string userid = string.Empty; 
            if (user != null)
            {
                role = GetRole(e);
                userid = GetUserID(e);
            }
         
            if (FSPM.HasPermission(userid, role, path, permission))
            {
                return true;
            }
            bool isuserpath = this.FSRM.IsUserDirectory(userid, path);
            if (isuserpath)
            {
                return true;
            }
            return false;
        }
 
        public virtual void DownloadFile(NetPacket ph, ReciveEventArgs e)
        { 
            try
            { 
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string categroy = br.ReadIndex(1, string.Empty);
                    string path = br.ReadIndex(2, string.Empty);
                    byte[] data = null;
                    bool has = false;
                    //if (Feng.IO.FileHelper.GetExtension(path) == Feng.App.FileExtension_DataExcel.DataExcelTemplateData)
                    //{
                    //    has = true;
                    //    path = this.FSRM.GetPath(path);
                    //    data = Feng.IO.FileHelper.ReadFile(path);
                    //    result = true;
                    //}
                    //string utl= this.FSRM.GetUrl(path);
                    string utl=  path;
                    if (HasPermission(e, utl, Project.PermissionEnum.chkDirView))
                    {
                        path = this.FSRM.GetPath(path);
                        data = Feng.IO.FileHelper.ReadFile(path);
                        result = true;
                    }
                    

                    ph.WriteResult(result,data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void DownLoadFilePart(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string categroy = br.ReadIndex(1, string.Empty);
                    string path = br.ReadIndex(2, string.Empty);
                    int begin = br.ReadIndex(3, 0);
                    int len = br.ReadIndex(4, 0);
                     
                    byte[] data = null; 
                    string utl = path;
                    if (HasPermission(e, utl, Project.PermissionEnum.chkDirView))
                    {
                        path = this.FSRM.GetPath(path);
                        using (FileStream fs = System.IO.File.Open(path, FileMode.Open))
                        {
                            long arclen = fs.Length - begin;
                            len = Math.Min(len, (int)arclen);
                            data = new byte[len];
                            fs.Seek(begin, SeekOrigin.Begin);
                            fs.Read(data, 0, len);
                            fs.Close();
                        }
                        result = true;
                    }


                    ph.WriteResult(result, data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }
        public virtual void CreatePath(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string category = br.ReadIndex(1, string.Empty);
                    string path = br.ReadIndex(2, string.Empty);
                    string rootpath = Feng.IO.FileHelper.GetFileDrectory(path);
                    if (HasPermission(e, rootpath, Project.PermissionEnum.chkDirAdd))
                    {
                        path = this.FSRM.GetPath(path);
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        result = true;
                    }
                    ph.PacketContents = BitConverter.GetBytes(result);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void DeleteDirectory(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string msg = string.Empty;
                    string category = br.ReadIndex(1, string.Empty);
                    string path = br.ReadIndex(2, string.Empty);
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        if (HasPermission(e, path, Project.PermissionEnum.chkDirDelete))
                        {
                            path = this.FSRM.GetPath(path);
                            string[] files = System.IO.Directory.GetFiles(path);
                            if (files.Length > 0)
                            {
                                msg = "文件夹内容不为空";
                                result = false;
                            }
                            else
                            {
                                files = System.IO.Directory.GetDirectories(path);
                                if (files.Length > 0)
                                {
                                    msg = "文件夹内容不为空";
                                    result = false;
                                }
                                else
                                {
                                    result = true;
                                }
                            }
                            if (result)
                            {
                                this.FSRM.RecycleBinManager.AddDirctory(path);
                            }
                        }
                        else
                        {
                            msg = "没有此文件夹删除权限";
                        }
                    } 
                    ph.WriteResultString(result, msg);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void DeleteFile(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string category = br.ReadIndex(1, string.Empty);
                    string directory = br.ReadIndex(2, string.Empty);
                    string path = br.ReadIndex(3, string.Empty);
                    if (HasPermission(e, path, Project.PermissionEnum.chkFileDel))
                    {
                        path = this.FSRM.GetPath(path);
                        string userid = GetUserID(e);
                        this.FSRM.BackFile(path, userid);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        result = true;
                    }
                    ph.PacketContents = BitConverter.GetBytes(result);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void GetDirectorys(NetPacket ph, ReciveEventArgs e)
        {
           
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string path = br.ReadIndex(1, string.Empty);
                    byte[] data = null;
                    if (HasPermission(e, path, Project.PermissionEnum.chkDirView))
                    {
                        path = this.FSRM.GetPath(path);
                        string[] paths = System.IO.Directory.GetDirectories(path);
                        using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                        {
                            bw.Write(paths.Length);
                            foreach (string str in paths)
                            {
                                int index = str.LastIndexOf("\\");
                                string text = str.Substring(index + 1, str.Length - index - 1);
                                bw.Write(text);
                            }
                            data = bw.GetData(); 
                        }
                        result = true;
                    }

                    ph.WriteResult(result, data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void GetFiles(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string path = br.ReadIndex(1, string.Empty);
                    byte[] data = null;
                    if (HasPermission(e, path, Project.PermissionEnum.chkDirView))
                    {
                        path = this.FSRM.GetPath(path);
                        if (System.IO.Directory.Exists(path))
                        {
                            string[] paths = System.IO.Directory.GetFiles(path);
                            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                            {
                                bw.Write(paths.Length);
                                foreach (string str in paths)
                                {
                                    bw.Write(System.IO.Path.GetFileName(str));
                                }
                                data = bw.GetData();
                            }
                        }
                        result = true;
                    }

                    ph.WriteResult(result, data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void GetDirectoryInfo(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string path = br.ReadIndex(1, string.Empty);
                    byte[] data = null;
                    if (HasPermission(e, path, Project.PermissionEnum.chkDirView))
                    {
                        path = this.FSRM.GetPath(path);
                        if (System.IO.Directory.Exists(path))
                        {
                            DirectoryInfo fi = new DirectoryInfo(path);
                            NetFileInfo tfi = new NetFileInfo()
                            {
                                CreationTime = fi.CreationTime,
                                Extension = fi.Extension,
                                FullName = fi.FullName,
                                IsReadOnly = false,
                                LastAccessTime = fi.LastAccessTime,
                                LastWriteTime = fi.LastWriteTime,
                                Length = 0,
                                Name = fi.Name
                            };
                            data = tfi.ToData(); 
                        }
                        result = true;
                    }

                    ph.WriteResult(result, data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void GetFileInfo(NetPacket ph, ReciveEventArgs e)
        {  
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string path = br.ReadIndex(1, string.Empty);
                    byte[] data = null;
                    if (HasPermission(e, path, Project.PermissionEnum.chkDirView))
                    {
                        path = this.FSRM.GetPath(path);
                        if (System.IO.File.Exists(path))
                        {

                            FileInfo fi = new FileInfo(path);
                            NetFileInfo tfi = new NetFileInfo()
                            {
                                CreationTime = fi.CreationTime,
                                Extension = fi.Extension,
                                FullName = fi.FullName,
                                IsReadOnly = fi.IsReadOnly,
                                LastAccessTime = fi.LastAccessTime,
                                LastWriteTime = fi.LastWriteTime,
                                Length = fi.Length,
                                Name = fi.Name
                            }; 
                            data = tfi.ToData(); 
                        }
                        result = true;
                    }

                    ph.WriteResult(result, data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void MoveFile(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string category = br.ReadIndex(1, string.Empty);
                    string original = br.ReadIndex(2, string.Empty);
                    string target = br.ReadIndex(3, string.Empty);
                    byte[] data = null;
                    string path = original;
                    if (HasPermission(e, path, Project.PermissionEnum.chkFileEdit))
                    {
                        path = this.FSRM.GetPath(path);
                        string targetfile = this.FSRM.GetPath(target);
                        string originalfile = this.FSRM.GetPath(original);
                        string userid = GetUserID(e);
                        if (System.IO.File.Exists(targetfile))
                        {
                            this.FSRM.BackFile(targetfile, userid);
                            System.IO.File.Delete(targetfile);
                        }
                        this.FSRM.BackFile(originalfile, userid);
                         
                        if (System.IO.File.Exists(originalfile))
                        { 
                            System.IO.File.Move(originalfile, targetfile);
                        }

                        result = true;
                    }

                    ph.WriteResult(result, data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }


        public virtual void CopyFile(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string category = br.ReadIndex(1, string.Empty);
                    string original = br.ReadIndex(2, string.Empty);
                    string target = br.ReadIndex(3, string.Empty);
                    byte[] data = null;
                    string path = original;
                    if (HasPermission(e, path, Project.PermissionEnum.chkFileEdit))
                    {
                        path = this.FSRM.GetPath(path);
                        string targetfile = this.FSRM.GetPath(target);
                        string originalfile = this.FSRM.GetPath(original);

                        if (System.IO.File.Exists(originalfile))
                        {
                            if (System.IO.File.Exists(targetfile))
                            {
                                string fileother = "-副本";
                                for (int i = 0; i < short.MaxValue; )
                                {
                                    string newpath = System.IO.Path.GetDirectoryName(targetfile);
                                    string newfilename = System.IO.Path.GetFileNameWithoutExtension(targetfile);
                                    string newextent = System.IO.Path.GetExtension(targetfile);
                                    string newfile = Feng.IO.FileHelper.Combine(newpath, newfilename)+ fileother + newextent;
                                    if (!System.IO.File.Exists(newfile))
                                    {
                                        byte[] datafile = System.IO.File.ReadAllBytes(originalfile);
                                        IO.FileHelper.WriteAllBytes(newfile, datafile);
                                        break;
                                    }
                                    i++;
                                    fileother = "-副本" + i.ToString();
                                }
                            }
                            else
                            {
                                byte[] datafile = System.IO.File.ReadAllBytes(originalfile);
                                IO.FileHelper.WriteAllBytes(targetfile, datafile);
                            }
                        }

                        result = true;
                    }

                    ph.WriteResult(result, data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void ExistsFile(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string dectory = br.ReadIndex(1, string.Empty);
                    string path = br.ReadIndex(2, string.Empty);
                    if (HasPermission(e, path, Project.PermissionEnum.chkDirView))
                    {
                        path = this.FSRM.GetPath(path);
                        if (System.IO.File.Exists(path))
                        {
                            result = true;
                        }
                    }

                    ph.WriteResult(true, result);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void EncryptFile(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string path = br.ReadIndex(1, string.Empty);
                    string password = br.ReadIndex(2, string.Empty);
                    if (HasPermission(e, path, Project.PermissionEnum.chkFileEdit))
                    {
                        path = this.FSRM.GetPath(path);
                        if (System.IO.File.Exists(path))
                        {
                            byte[] data = Feng.IO.FileHelper.ReadFile(path);
                            data = Feng.IO.DEncrypt.Encrypt(password,data);
                            Feng.IO.FileHelper.WriteFile(path, data);
                            result = true;
                        }
                    }

                    ph.WriteResult(result);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void DecryptFile(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string path = br.ReadIndex(1, string.Empty);
                    string password = br.ReadIndex(2, string.Empty);
                    if (HasPermission(e, path, Project.PermissionEnum.chkFileEdit))
                    {
                        path = this.FSRM.GetPath(path);
                        if (System.IO.File.Exists(path))
                        {
                            byte[] data = Feng.IO.FileHelper.ReadFile(path);
                            data = Feng.IO.DEncrypt.Decrypt(password, data);
                            Feng.IO.FileHelper.WriteFile(path, data);
                            result = true;
                        }
                    }

                    ph.WriteResult(result);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }

        public virtual void MoveDirectorye(NetPacket ph, ReciveEventArgs e)
        { 
            try
            {
                bool result = false;
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    string category = br.ReadIndex(1, string.Empty);
                    string original = br.ReadIndex(2, string.Empty);
                    string target = br.ReadIndex(3, string.Empty);
                    byte[] data = null;
                    string path = original;
                    if (HasPermission(e, path, Project.PermissionEnum.chkFileEdit))
                    {
                        path = this.FSRM.GetPath(path);
                        string targetfile = this.FSRM.GetPath(target);
                        string originalfile = this.FSRM.GetPath(original);

                        if (System.IO.Directory.Exists(originalfile))
                        {
                            System.IO.Directory.Move(originalfile, targetfile);
                        }

                        result = true;
                    }

                    ph.WriteResult(result, data);
                    e.ClientProxy.Respond(ph);
                }
            }
            catch (Exception ex)
            {
                Feng.Net.NetErrorTool.RespondError(e.ClientProxy, ph, ex);
            }
        }
         
    }

}
