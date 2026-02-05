using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Excel.Generic;
using System.Net;
using Feng.Net.Http;
using Feng.IO;

namespace Feng.Tools
{
    public class DsUpdater
    {
        public void Run()
        {
            System.Threading.Thread th = new System.Threading.Thread(CheckUpdate);
            th.IsBackground = true;
            th.Start();
        }
        //public void Test()
        //{
        //    string path = @"I:\Tool\OpenSource\SysTools\test\DataExcelSetup\2025\共享服务器服务端";
        //    Updater2 updater2 = new Updater2("V2024090412", "http://10.15.32.26:8868/charts/Uploat/updatejson.txt");
        //    //updater2.ZipPacket(path+ "\\DS\\V20240721", path + "\\packet.zip");
        //    updater2.DownLoadUpdatePacket();
        //}
        public void CheckUpdate()
        {
            while (true)
            {
                try
                {
                    Updater2 updater2 = new Updater2(Updater2.UpdateVersion, "https://www.dataexcel.cn/Updater/updatejson.txt");
                    updater2.DownLoadUpdatePacket();

                }
                catch (Exception)
                {

                }
                System.Threading.Thread.Sleep(1000 * 60 * 30);
            }

        }
    }

    public class ClientApp
    {
        //private static ClientApp _ClientApp = null;
        //public static ClientApp Default
        //{
        //    get {
        //        if (_ClientApp == null)
        //        {
        //            _ClientApp = new ClientApp();
        //        }
        //        return _ClientApp;
        //    }  
        //}
        public ClientApp()
        {

        }
        public string LocalPath { get; set; }
        public Feng.Net.Tcp.TcpClient client = new Feng.Net.Tcp.TcpClient();
        public Feng.Net.Extend.ClientExtendKernal kernalextend = new Feng.Net.Extend.ClientExtendKernal();
        public Feng.Net.Extend.ClientExtendFile fileextend = new Feng.Net.Extend.ClientExtendFile();

        public void Connection()
        {
            this.client.Close();
            this.client = new Net.Tcp.TcpClient();
            this.client.Connected += client_Connected;
            this.client.RemoteIP = "112.126.57.50";
            this.client.RemotePort = 1021;
            this.client.AutoConnected = true;
            this.client.Connection();
            this.kernalextend.Bingding(this.client);
            this.fileextend.Bingding(this.client);

        }
        public void ReSetLoac()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }
        void client_Connected(object sender, Feng.Net.Interfaces.IClientProxy client)
        {

            try
            {
                if (this.kernalextend.RegeditSession("DataExcel", "DataExcel"))
                {

                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        public string GetFullUrl(string url)
        {
            string path = url.Replace('\\', '/');
            return "Dtp://" + this.client.RemoteIP + ":" + this.client.RemotePort + path;
        }
    }
    public class Updater2
    {
        //public const string UpdateVersion = "V20241010617";
        public const string UpdateVersion = "V2025101810";
        private ClientApp clientGuest = null;
        public ClientApp ClientGuest
        {
            get
            {
                if (clientGuest == null)
                {
                    clientGuest = new ClientApp();  
                    clientGuest.Connection();
                }
                return clientGuest;
            }
        }
        public Updater2()
        { 
        }
        public Updater2(string currentversion, string downurl)
        {
            CurrentVersion = currentversion;
            DownLoadUrl = downurl;
            //version="V2024090412";
        }
        private string CurrentVersion = string.Empty;
        private string DownLoadUrl = string.Empty;
        public bool CheckVersionBigThan(string jsonversion)
        {
            long cversionl = long.Parse(CurrentVersion.Substring(1));
            long versionl = long.Parse(jsonversion.Substring(1));
            if (cversionl >= versionl)
            {
                return false;
            }
            return true;
        }
        public bool DownLoadUpdatePacket()
        {
            try
            {
                string urldownloadpath = DownLoadUrl;
                string urlfileversion = WebRequestTool.WebGet(urldownloadpath);
                if (string.IsNullOrWhiteSpace(urlfileversion))
                {
                    urlfileversion = WebRequestTool.WebGet(urldownloadpath);
                }
                string startpath = System.Windows.Forms.Application.StartupPath;
                if (!string.IsNullOrEmpty(ServerFile.DS_PATH_FILE_OR_UPDATE_VERSION_PATH))
                {
                    startpath = ServerFile.DS_PATH_FILE_OR_UPDATE_VERSION_PATH;
                }
                Feng.Json.FJsonBase urlfilefJsonBase = Feng.Json.FJsonParse.Parese(urlfileversion);
                string version = Feng.Utils.ConvertHelper.ToString(urlfilefJsonBase["version"].BaseValue);
                string packurl = Feng.Utils.ConvertHelper.ToString(urlfilefJsonBase["packurl"].BaseValue);
                string packfilename = Feng.Utils.ConvertHelper.ToString(urlfilefJsonBase["packfilename"].BaseValue);
                string downloadfile = startpath + "\\update\\" + packfilename;
                string pathtarget = startpath + "\\" + version;
                string[] files = null;
                if (System.IO.Directory.Exists(pathtarget))
                {
                    files = System.IO.Directory.GetFiles(pathtarget);
                    if (files.Length > 2)
                        return false;
                }
                if (!CheckVersionBigThan(version))
                {
                    return false;
                }
                Feng.IO.FileHelper.CreatDirctory(downloadfile);
                FileDownloader fileDownloader = new FileDownloader();
                bool res = fileDownloader.DownloadFileWithResume(packurl, downloadfile);
                if (!res)
                {
                    if (System.IO.File.Exists(downloadfile))
                    {
                        System.IO.File.Delete(downloadfile);
                    }
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(packurl, downloadfile);
                    }
                }
                if (!System.IO.File.Exists(downloadfile))
                {
                    ClientApp tempclient = new ClientApp(); 
                    tempclient.Connection();
                    for (int i = 0; i < 10; i++)
                    {
                        if (tempclient.client.HasConnected)
                            break;
                    }
                    byte[] data = tempclient.kernalextend.OnDoCommand("ds", "update", CurrentVersion, null);
                    Feng.IO.FileHelper.WriteAllBytes(downloadfile, data);
                }
                if (System.IO.File.Exists(downloadfile))
                {
                    pathtarget = startpath + "\\" + System.IO.Path.GetFileNameWithoutExtension(downloadfile);
                    if (System.IO.Directory.Exists(pathtarget))
                    {
                        files = System.IO.Directory.GetFiles(pathtarget);
                        if (files.Length > 2)
                            return false;
                    }
                    UnZipPacket(downloadfile, pathtarget);
                    string autorunexe = Feng.IO.FileHelper.Combine(pathtarget, "DataServerUp.exe");
                    if (System.IO.File.Exists(autorunexe))
                    {
                        System.Diagnostics.Process.Start(autorunexe);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;

        }
        public void ZipPacket(string directory, string packet)
        {
            string[] files = System.IO.Directory.GetFiles(directory);
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                DateTime packettime = DateTime.Now;
                string packetuser = "dataexcel";
                string packetversion = Updater2.UpdateVersion;
                string packetpath = directory;
                bw.Write(packettime);
                bw.Write(packetuser);
                bw.Write(packetversion);
                bw.Write(packetpath);
                bw.Write(1, files.Length);
                foreach (string file in files)
                {
                    string filepath = file.Substring(directory.Length);
                    bw.Write(1, filepath);
                    bw.Write(2, System.IO.File.ReadAllBytes(file));
                }
                string[] dirctories = System.IO.Directory.GetDirectories(directory);
                bw.Write(2, dirctories.Length);
                foreach (string direc in dirctories)
                {
                    ZipPacketDire(directory, direc, bw);
                }
                Feng.IO.FileHelper.WriteAllBytes(packet, Feng.IO.CompressHelper.GZipCompress(bw.GetData()));
            }
        }
        private void ZipPacketDire(string root, string directory, Feng.IO.BufferWriter bw)
        {
            string[] files = System.IO.Directory.GetFiles(directory);

            bw.Write(1, files.Length);
            foreach (string file in files)
            {
                string filepath = file.Substring(root.Length);
                bw.Write(1, filepath);
                bw.Write(2, System.IO.File.ReadAllBytes(file));
            }
            string[] dirctories = System.IO.Directory.GetDirectories(directory);
            bw.Write(2, dirctories.Length);
            foreach (string direc in dirctories)
            {
                ZipPacketDire(root, direc, bw);
            }
        }
        public void UnZipPacket(string packet, string root)
        { 
            byte[] datazip = Feng.IO.CompressHelper.GZipDecompress(System.IO.File.ReadAllBytes(packet));
            using (Feng.IO.BufferReader reader = new IO.BufferReader(datazip))
            {
                DateTime packettime = reader.ReadDateTime();
                string packetuser = reader.ReadString();
                string packetversion = reader.ReadString();
                string packetpath = reader.ReadString();
                int filelength = reader.ReadIndex(1, 0);
                for (int i = 0; i < filelength; i++)
                {
                    string filepath = reader.ReadIndex(1, "");
                    byte[] data = reader.ReadIndex(2, new byte[] { });
                    string file = root + "\\" + filepath;
                    Feng.IO.FileHelper.WriteAllBytes(file, data);
                }

                int directorylength = reader.ReadIndex(2, 0);
                for (int i = 0; i < directorylength; i++)
                {
                    UnZipPacket(root, reader);
                }
            }
        }
        private void UnZipPacket(string root, Feng.IO.BufferReader reader)
        {

            int filelength = reader.ReadIndex(1, 0);
            for (int i = 0; i < filelength; i++)
            {
                string filepath = reader.ReadIndex(1, "");
                byte[] data = reader.ReadIndex(2, new byte[] { });
                string file = root + "\\" + filepath;
                Feng.IO.FileHelper.WriteAllBytes(file, data);
            }

            int directorylength = reader.ReadIndex(2, 0);
            for (int i = 0; i < directorylength; i++)
            {
                UnZipPacket(root, reader);
            }
        }
    }
}
