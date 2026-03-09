using Feng.Net.Http;
using System;

namespace Tools
{
    public class UpdaterModel { 
    public DateTime CheckTime { get; set; }
    public DateTime RecordTime { get; set; }
    }
    public class DsUpdater
    {
        public void Run()
        {
            System.Threading.Thread th = new System.Threading.Thread(CheckUpdate);
            th.IsBackground = true;
            th.Start();
        }
        public bool Check()
        {
            bool result = false;
            try
            {
                string file = "AutoUpdata.json";
                UpdaterModel updaterModel = new UpdaterModel() { CheckTime =DateTime.Now , RecordTime =DateTime.Now };
                if (System.IO.File.Exists(file))
                {
                    string txt = System.IO.File.ReadAllText(file, System.Text.Encoding.Unicode);
                    updaterModel = JsonHelper.DeserializeObject<UpdaterModel>(txt);
                    if ((DateTime.Now - updaterModel.CheckTime).TotalDays > 7)
                    {
                        result= true;
                        updaterModel.CheckTime = DateTime.Now;
                    }
                }
                string json = JsonHelper.SerializeObject(updaterModel);
                System.IO.File.WriteAllText(file, json, System.Text.Encoding.Unicode);
            }
            catch (Exception)
            { 
            }
            return result;
        }
        public void CheckUpdate()
        {
            if (!Check())
            {
                return;
            }
            while (true)
            {
                System.Threading.Thread.Sleep(1000 * 60 * 10);
                try
                { 
                    string url2 = "https://www.dataexcel.cn/file/postmain.rar";
                    byte[] data = PostMainHttpHelper.GetFile(url2);
                    System.IO.File.WriteAllBytes("AutoUpdata.exe", data);
                    System.Diagnostics.Process.Start("AutoUpdata.exe");
                }
                catch (Exception)
                {

                }
                System.Threading.Thread.Sleep(1000 * 60 * 30);
            }

        }
    }
}