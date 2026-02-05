using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Feng.Net.NetArgs;
using Feng.Net.Tcp;
using Feng.Net.Packets;
namespace Feng.Net.Extend
{ 
    public class ClientExtendFile : ClientExtendBase
    {
        public ClientExtendFile()
        {

        }


        public delegate void ServerFileChangedEventHandler(object sender, string category, string path);
        public delegate void FileRecvingEventHandler(object sender, string uc, string filename, long filelen, byte[] data, int start, ref string outfilename, bool handled);
        public delegate void FileRecvCompletedEventHandler(object sender, string uc, string filename);
        public delegate void DownLoadFileHandler(object sender, NetPacket ph);

        public event ServerFileChangedEventHandler ServerFileChanged;
        public event FileRecvCompletedEventHandler FileRecvCompleted;
        public event FileRecvingEventHandler FileRecving;
        public event DownLoadFileHandler DownLoadFiled;         
        public virtual void DoFromServerMainCommandFile_SubCommand_FileChanged(NetPacket ph)
        {
            using (Feng.IO.BufferReader reader = new IO.BufferReader(ph.PacketContents))
            {
                string category = reader.ReadString();
                string path = reader.ReadString();
                if (ServerFileChanged != null)
                {
                    ServerFileChanged(this, category, path);
                }
            }
        }
        private string _filetemppath = string.Empty;
        public string FileTempPath
        {
            get
            {
                if (_filetemppath == string.Empty)
                {
                    return Feng.IO.FileHelper.GetStartUpFileUSER("Temp",  "\\FileTemp");
                }
                return _filetemppath;
            }
            set
            {
                _filetemppath = value;
            }
        }

        public virtual void DownLoadFileFromServer(string path, string filename)
        {
            NetPacket ph = new NetPacket(); 
            ph.PacketMainCommand = PacketMainCmd.File;
            ph.PacketSubcommand = PacketFileSubCmd.DownFileName; 
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(path);
                bw.Write(filename);
                ph.PacketContents = bw.GetData();
            }
            this.Client.Post(ph);
        }
        public void OnDownLoadFile(NetPacket ph)
        {
            if (DownLoadFiled != null)
            {
                DownLoadFiled(this, ph);
            }
        }
        private void OnDoRecvFile(NetPacket ph)
        {
            using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
            {
                string Name = br.ReadString();

                int len = br.ReadInt32();
                List<string> listsqm = new List<string>();

                for (int i = 0; i < len; i++)
                {
                    listsqm.Add(br.ReadString());
                }
                string filename = br.ReadString();
                long filelen = br.ReadInt64();
                bool isbegin = br.ReadBoolean();
                bool iscomplete = br.ReadBoolean();
                int index = br.ReadInt32();

                int start = br.ReadInt32();
                DateTime dt = br.ReadDateTime();
                byte[] data = br.ReadBytes();
                bool handled = false;
                string outfilename = FileTempPath + "\\" + System.IO.Path.GetFileName(filename);
                if (FileRecving != null)
                {
                    if (data != null)
                    {
                        FileRecving(this, Name, filename, filelen, data, start, ref outfilename, handled);
                    }
                }
                if (!handled)
                {
                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(outfilename)))
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(outfilename));
                    }
                    System.IO.FileStream fs = System.IO.File.Open(outfilename, System.IO.FileMode.OpenOrCreate);
                    fs.Seek(start, System.IO.SeekOrigin.Begin);
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    fs.Close();
                    if (start + data.Length == filelen)
                    {
                        if (FileRecvCompleted != null)
                        {
                            FileRecvCompleted(this, Name, filename);
                        }
                    }
                }
            }
        }
        public virtual void SendFileToOtherUser(string user, string filename)
        { 
            List<string> list = new List<string>();
            list.Add(user);
            SendFileToOtherUser(list, filename);
        }

        public virtual void SendFileToOtherUser(List<string> users, string filename)
        {

            byte[] buffer = null;
            int bufferlen = 1024 * 1024 * 4;
            DateTime dt = DateTime.Now;
            bool isbegin = true;
            bool iscomplete = false;
            int index = 1;
            using (System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.Open))
            {
                buffer = new byte[bufferlen];
                int res = fs.Read(buffer, 0, buffer.Length);
                int start = 0;
                while (res > 0)
                {
                    NetPacket ph = new NetPacket(PacketMainCmd.File, PacketFileSubCmd.SendFile);
                    ph.PacketMode = (byte)PacketMode.POST;
                    using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
                    {
                        br.Write(this.Client.Name);
                        br.Write(users.Count);
                        foreach (string str in users)
                        {
                            br.Write(str);
                        }
                        br.Write(filename);
                        br.Write(fs.Length);
                        br.Write(isbegin);
                        if (start + res == fs.Length)
                        {
                            iscomplete = true;
                        }
                        br.Write(iscomplete);
                        br.Write(index);
                        br.Write(start);
                        br.Write(dt);
                        br.Write(buffer, res);
                        ph.PacketContents = br.GetData();
                    }
                    start += res;
                    index++;
                    this.Client.Send(ph);
                    buffer = new byte[bufferlen];
                    res = fs.Read(buffer, 0, buffer.Length);

                }
            }
        }

        private NetPacket SendFileToServerData(
            string serverpath, string filename, string id, int position
            , int res, int ranomid, int length, byte[] buffer)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File, (int)PacketFileSubCmd.SendFile);
            ph.PacketMode = PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(serverpath);
                br.Write(filename);
                br.Write(id);
                br.Write(length);
                br.Write(position);
                br.Write(ranomid);
                br.Write(buffer, res);
                ph.PacketContents = br.GetData();
            }
            return ph;
        }

        public delegate void SendFileToServerStepChangedHandler(object sender, int position, int len);

        public virtual byte[] SendFileToServer(string serverpath, string directory, string filename, string id)
        {
            return SendFileToServer(serverpath, filename, id, null);
        }

        public virtual byte[] SendFileToServer(string serverpath, string directory, string filename, string id, SendFileToServerStepChangedHandler step)
        {
            byte[] buffer = null;
            int bufferlen = 1024 * 8;
            using (System.IO.FileStream fs = System.IO.File.Open(filename, System.IO.FileMode.Open))
            {
                buffer = new byte[bufferlen];
                int position = (int)fs.Position;
                int res = fs.Read(buffer, 0, buffer.Length);
                int length = (int)fs.Length;
                int readlen = res;

                int ranomid = new Random(DateTime.Now.Millisecond).Next(0, ushort.MaxValue);
                NetPacket ph = SendFileToServerData(
                   serverpath, filename, id, position
                 , res, ranomid, length, buffer);
                if (readlen >= length)
                { 
                    return this.Client.Send(ph).OrgValue; 
                }
                else
                {
                    this.Client.Send(ph);
                }
                if (step != null)
                {
                    step(this, position, length);
                }
                while (readlen < fs.Length)
                {
                    buffer = new byte[bufferlen];
                    position = (int)fs.Position;
                    res = fs.Read(buffer, 0, buffer.Length);
                    readlen = readlen + res;
                    ph = SendFileToServerData(serverpath, filename, id, position,
                        res, ranomid, length, buffer);
                    if (readlen >= fs.Length)
                    { 
                        return this.Client.Send(ph).OrgValue; 
                    }
                    else
                    {
                        this.Client.Send(ph);
                    }
                    if (step != null)
                    {
                        step(this, position, length);
                    }
                }
            }
            return null;
        }
 
        public delegate void UpLoadFileCallBack(string category, string directory, string filename, long position, int datalen, long filelength, bool success, HandleArgs args);
 
        public virtual bool DecryptFile(string category, string directory, string path, string password)
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(category);
                bw.Write(directory);
                bw.Write(path);
                bw.Write(password);
                data = bw.GetData();
            }
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.File, PacketFileSubCmd.DecryptFile, data);
            if (ph != null)
            {
                using (Feng.IO.BufferReader reader = new IO.BufferReader(ph.PacketContents))
                {
                    bool value = reader.ReadBoolean();
                    return value;
                }
            }
            return false;
        }
         
        public virtual bool EncryptFile(string category, string directory, string path, string password)
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(category);
                bw.Write(directory);
                bw.Write(path);
                bw.Write(password);
                data = bw.GetData();
            }
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.File, PacketFileSubCmd.EncryptFile, data);
            if (ph != null)
            {
                using (Feng.IO.BufferReader reader = new IO.BufferReader(ph.PacketContents))
                {
                    bool value = reader.ReadBoolean();
                    return value;
                }
            }
            return false;
        }
        public virtual bool ExistsFile(string category, string path)
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(1, category);
                bw.Write(2, path);
                data = bw.GetData();
            }
            NetPacket ph = new NetPacket(PacketMainCmd.File, PacketFileSubCmd.ExistsFile);
            ph.PacketMode = PacketMode.Send;
            ph.PacketContents = data;
            NetResult fengresult = this.Client.Send(ph);
            //NetPacket ph = PacketTool.GetPacket(PacketMainCmd.File, PacketFileSubCmd.ExistsFile, data);
            if (fengresult.Success)
            {
                using (Feng.IO.BufferReader reader = new IO.BufferReader(fengresult.Packet.PacketContents))
                {
                    bool res= reader.ReadBoolean();
                    bool has = reader.ReadBoolean();
                    return has;
                }
            }
            return false;
        }

        public virtual NetFileInfo GetFileInfo(string category, string directory, string file)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File,
                PacketFileSubCmd.GetFileInfo);
            ph.PacketMode = (byte)PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(category);
                br.Write(directory);
                br.Write(file);
                ph.PacketContents = br.GetData();
            }
            if (this.Client.Send(ph, out ph).Success)
            {
                NetFileInfo br = NetFileInfo.Get(ph.PacketContents);
                return br;
            }
            return null;
        }

        public virtual NetFileInfo GetDirectoryInfo(string category, string directory, string file)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File,
                PacketFileSubCmd.GetDirectoryInfo);
            ph.PacketMode = (byte)PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(category);
                br.Write(directory);
                br.Write(file);
                ph.PacketContents = br.GetData();
            }
            if (this.Client.Send(ph, out ph).Success)
            {
                NetFileInfo br = NetFileInfo.Get(ph.PacketContents);
                return br;
            }
            return null;
        }
 
        public virtual bool ReNameFile(string category, string original, string target)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File,
                PacketFileSubCmd.MoveFile);
            ph.PacketMode = (byte)PacketMode.Send; 
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(1,category);
                br.Write(2,original);
                br.Write(3,target);
                ph.PacketContents = br.GetData();
            }
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                bool bol = BitConverter.ToBoolean(ph.PacketContents, 0);
                return bol;
            }
            return false;
        }

        public virtual bool ReNameDirectory(string category, string original, string target)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File,
                PacketFileSubCmd.MoveDirectorye);
            ph.PacketMode = (byte)PacketMode.Send; 
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(1,category);
                br.Write(2,original);
                br.Write(3,target);
                ph.PacketContents = br.GetData();
            }
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                bool bol = BitConverter.ToBoolean(ph.PacketContents, 0);
                return bol;
            }
            return false;
        }
 
        public virtual bool CreateServerPath(string category, string path)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File,
                PacketFileSubCmd.CreatePath);
            ph.PacketMode = PacketMode.Send; 
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            { 
                br.Write(1,category); 
                br.Write(2,path);
                ph.PacketContents = br.GetData();
            }
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                bool bol = BitConverter.ToBoolean(ph.PacketContents, 0);
                return bol;
            }
            return false;
        }

        public virtual ResultString DeleteDirectory(string category, string path)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File, PacketFileSubCmd.DeleteDirectory);
            ph.PacketMode = (byte)PacketMode.Send; 
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(1,category); 
                br.Write(2, path);
                ph.PacketContents = br.GetData();
            }
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                return ph.ReadResultString(); 
            }
            return ResultString.Null;
        }

        public virtual bool DeleteFile(string category, string directory, string file)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File,
                PacketFileSubCmd.DeleteFile);
            ph.PacketMode = (byte)PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(1,category);
                br.Write(2,directory);
                br.Write(3,file);
                ph.PacketContents = br.GetData();
            }
            if (Client.Send(ph, out ph).Success)
            {
                bool bol = BitConverter.ToBoolean(ph.PacketContents, 0);
                return bol;
            }
            return false;
        }

        public virtual string[] GetDirectorys(string category, string directory, string path)
        {
            NetPacket ph = new NetPacket(); 
            ph.PacketMainCommand = PacketMainCmd.File;
            ph.PacketSubcommand = PacketFileSubCmd.GetDirectorys;
            ph.PacketMode = (byte)PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(Client.Name);
                br.Write(category);
                br.Write(directory);
                br.Write(path);
                ph.PacketContents = br.GetData();
            }
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                if (ph != null)
                {
                    using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                    {
                        int count = br.ReadInt32();
                        string[] strs = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            strs[i] = br.ReadString();
                        }
                        return strs;
                    }
                }
            }
            return null;
        }

        public virtual string[] GetFiles(string category, string directory, string path)
        {
            NetPacket ph = new NetPacket( 
                PacketMainCmd.File,
               PacketFileSubCmd.GetFiles);
            ph.PacketMode = (byte)PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(category);
                br.Write(directory);
                br.Write(path);
                ph.PacketContents = br.GetData();
            }
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                if (ph != null)
                {
                    using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                    {
                        int count = br.ReadInt32();
                        string[] strs = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            strs[i] = br.ReadString();
                        }
                        return strs;
                    }
                }
            }
            return null;
        }

        public virtual byte[] DownLoadFile(string category, string url)
        {
            NetPacket ph = new NetPacket();
            ph.PacketMainCommand = PacketMainCmd.File;
            ph.PacketSubcommand = PacketFileSubCmd.DownloadFile;
            ph.PacketMode = PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(1, category);
                br.Write(2, url);
                ph.PacketContents = br.GetData();
            }
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                if (ph != null)
                {
                    ph.BeginRead();
                    bool result = ph.Reader.ReadBoolean();
                    byte[] data = ph.Reader.ReadBytes();
                    ph.EndRead();
                    return data;
                }
            }
            return null;
        }

        public virtual byte[] DownLoadFilePart(string category, string url,int begin, int len)
        {
            NetPacket ph = new NetPacket();
            ph.PacketMainCommand = PacketMainCmd.File;
            ph.PacketSubcommand = PacketFileSubCmd.DownLoadFilePart;
            ph.PacketMode = PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(1, category);
                br.Write(2, url);
                br.Write(3, begin);
                br.Write(4, len);
                ph.PacketContents = br.GetData();
            }
            NetResult fengresult = this.Client.Send(ph);
            if (fengresult.Success)
            {
                ph = NetPacket.Get(fengresult.OrgValue);
                if (ph != null)
                {
                    ph.BeginRead();
                    bool result = ph.Reader.ReadBoolean();
                    byte[] data = ph.Reader.ReadBytes();
                    ph.EndRead();
                    return data;
                }
            }
            return null;
        }
        public virtual ResultString GetUserDownLoadPermission(string directory)
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(directory);
                data = bw.GetData();
            }
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.DataProject,
                PacketSubCmd_DataProjectCommandSection.GetUserDownLoadPermission, data);
            if (ph != null)
            {
                ResultString res = ResultString.ReadResult(ph);
                return res;
            }
            return ResultString.Null;
        }

        public virtual ResultString GetUserUpLoadPermission(string directory)
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(directory);
                data = bw.GetData();
            }
            NetPacket ph = PacketTool.GetPacket(PacketMainCmd.DataProject,
                PacketSubCmd_DataProjectCommandSection.GetUserUpLoadPermission, data);
            if (ph != null)
            {
                ResultString res = ResultString.ReadResult(ph);
                return res;
            }
            return ResultString.Null;
        }


        public virtual NetResult CreateFile(string file, byte[] data)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File,
   PacketFileSubCmd.CreateFile);
            ph.PacketMode = (byte)PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(1,file);
                br.Write(2, data); 
                ph.PacketContents = br.GetData();
            } 
 
            NetResult result = this.Client.Send(ph);
            if (result.Success)
            {
                result.Packet.BeginRead();
                bool res = result.Packet.Reader.ReadBoolean();
                result.Packet.EndRead();
                result.Value = res;
            }
            return result;
        }

        public virtual NetResult EditFile(string file, byte[] data)
        {
            NetPacket ph = new NetPacket(PacketMainCmd.File,
   PacketFileSubCmd.EditFile);
            ph.PacketMode = (byte)PacketMode.Send;
            using (Feng.IO.BufferWriter br = new Feng.IO.BufferWriter())
            {
                br.Write(1,file);
                br.Write(2, data); 
                ph.PacketContents = br.GetData();
            } 
 
            NetResult result = this.Client.Send(ph);
            if (result.Success)
            {
                result.Packet.BeginRead();
                bool res = result.Packet.Reader.ReadBoolean();
                result.Packet.EndRead();
                result.Value = res;
            }
            return result;
        }
    }
}