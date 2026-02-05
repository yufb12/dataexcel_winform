using System;
using System.Collections.Generic;

namespace Feng.IO
{
    public class Setting
    { 

        public Setting()
        { 
        }
 
 
        private string _file = string.Empty;
        public virtual string File
        {
            get { return _file; }
            set {
                _file = value;
            }
        }
        public void Init()
        {
            Init(File);
        }
        public virtual void Read(byte[] data)
        {
            dics.Clear();
            if (data == null)
                return;
            if (data.Length < 1)
                return;
            using (BufferReader reader = new BufferReader(data))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    byte[] value = reader.ReadBytes();
                    dics[key] = value;
                }
            } 
        }
        public virtual void Init(string file)
        {
            dics.Clear();
            if (System.IO.File.Exists(file))
            {
                byte[] data = System.IO.File.ReadAllBytes(file);
                using (BufferReader reader = new BufferReader(data))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        string key = reader.ReadString();
                        byte[] value = reader.ReadBytes();
                        dics[key] = value;
                    }
                }
            }
        }
        private Dictionary<string, byte[]> dics = new Dictionary<string, byte[]>();
        public string this[string key]
        {
            get {
                return this.GetString(key);
            }

            set {
                this.SetValue(key, value);
            }
        } 

        public virtual void SetValue(string key, int value)
        {

            byte[] data = Utils.ConvertHelper.ToBytes(value);
            SetValue(key, data);
        }
        public virtual void SetValue(string key, bool value)
        {

            byte[] data = Utils.ConvertHelper.ToBytes(value);
            SetValue(key, data);
        }
        public virtual void SetValue(string key, byte[] value)
        {
            if (dics.ContainsKey(key))
            {
                dics[key] = value;
            }
            else
            { 
                dics.Add(key, value);
            }
        }
        public virtual void SetValue(string key, string value)
        {

            byte[] data = Utils.ConvertHelper.ToBytes(value);
            SetValue(key, data);
        }
        public virtual void SetValue(string key, DateTime value)
        {

            byte[] data = Utils.ConvertHelper.ToBytes(value);
            SetValue(key, data);
        }
 
        public virtual void SetValue(string key, decimal value)
        {

            byte[] data = Utils.ConvertHelper.ToBytes(value);
            SetValue(key, data);
        }

        public virtual void SetValue(string key, Setting value)
        {

            byte[] data = value.GetData();
            SetValue(key, data);
        }
        public virtual void SetValue(string key, string[] value)
        {
            using (Feng.IO.BufferWriter bw = new BufferWriter())
            {
                bw.Write(value); 
                byte[] data = bw.GetData();
                SetValue(key, data);
            }
        }
        public virtual string[] GetStrings(string key)
        {
            if (dics.ContainsKey(key))
            {
                byte[] data = (dics[key]);
                using (Feng.IO.BufferReader bw = new BufferReader(data))
                { 
                    string[] values = bw.ReadStrings();
                    return values;
                }

            }
            else
            {
                return null;
            }
        }
        public virtual bool GetBoolean(string key)
        {
             
            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToBoolean(dics[key]);
            }
            return false; 
        }
        public virtual bool GetBoolean(string key,bool defaultvalue)
        {

            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToBoolean(dics[key]);
            }
            return defaultvalue;
        }
        public virtual string GetString(string key)
        {

            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToString(dics[key]);
            }
            return string.Empty;

        }
        public virtual Setting GetSetting(string key)
        {
            Setting setting = new Setting();
            if (dics.ContainsKey(key))
            {
                byte[] data = dics[key];
                setting.Read(data);
            }
            return setting;

        }
        public virtual string GetString(string key,string defaultvalue)
        {

            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToString(dics[key]);
            }
            return defaultvalue;

        }
        public virtual byte[] GetData(string key)
        {

            if (dics.ContainsKey(key))
            {
                return dics[key];
            }
            return null;
        }
        public virtual int GetInt(string key)
        {
            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToInt32(dics[key]);
            }
            return 0;
        }
        public virtual DateTime GetDateTime(string key)
        {
            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToDateTime(dics[key]);
            }
            return DateTime.MinValue;
        }
        public virtual byte[] GetBuffer(string key)
        {
            if (dics.ContainsKey(key))
            {
                return dics[key];
            }
            return null;
        }
        public virtual decimal GetDecimal(string key)
        {
            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToDecimal(dics[key]);
            }
            return 0;

        }

        public virtual int GetInt(string key,int value)
        {
            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToInt32(dics[key]);
            }
            return value;
        }
        public virtual DateTime GetDateTime(string key,DateTime value)
        {
            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToDateTime(dics[key]);
            }
            return value;
        }
        public virtual decimal GetDecimal(string key,decimal value)
        {
            if (dics.ContainsKey(key))
            {
                return Utils.ConvertHelper.ToDecimal(dics[key]);
            }
            return value;

        }

        public virtual void Save()
        {
            Save(File);
        }
        public virtual byte[] GetData()
        {
            byte[] data = null;
            using (BufferWriter bw = new BufferWriter())
            {
                bw.Write(dics.Count);
                foreach (KeyValuePair<string, byte[]> k in this.dics)
                {
                    byte[] buffer = null;
                    if (!GetData(k.Key, data))
                    {
                        bw.Write(k.Key);
                        bw.Write(k.Value);
                    }
                    else
                    {
                        bw.Write(k.Key);
                        bw.Write(buffer);
                    }
                }
                data = bw.GetData();
            }
            return data;
        }
        public virtual bool GetData(string key, byte[] data)
        {
            return false;
        }
        public virtual void Save(string file)
        {
            byte[] data = GetData();
            string dic = System.IO.Path.GetDirectoryName(file);
            if (!System.IO.Directory.Exists(dic))
            {
                System.IO.Directory.CreateDirectory(dic);
            }
            IO.FileHelper.WriteAllBytes(file, data);
        }
        public virtual void ShowSaveDialog()
        {
            using (System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog())
            {
                dlg.Filter ="*.dat|*.dat";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this._file = dlg.FileName;
                    this.Save(dlg.FileName);
                }
            }
        }
        public virtual void ShowOpenDialog()
        {
            using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
            {
                dlg.Filter = "*.dat|*.dat";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this._file = dlg.FileName;
                    this.Init(dlg.FileName);
                }
            }
        }


        private static Setting _default = null;  
        public static Setting Default
        {
            get {
                if (_default == null)
                {
                    _default = new Setting();
                }
                return _default;
            }
        }
        public static void Begin()
        {
            Default.Init();
        }
        public static void WriteValue(string key, int value)
        {
            Default.SetValue(key, value);
        }
        public static void WriteValue(string key, bool value)
        {
            Default.SetValue(key, value);
        }
        public static void WriteValue(string key, byte[] value)
        {
            Default.SetValue(key, value);
        }
        public static void WriteValue(string key, string value)
        {
            Default.SetValue(key, value);
        }
        public static void WriteValue(string key, DateTime value)
        {
            Default.SetValue(key, value);
        }
        public static void WriteValue(string key, decimal value)
        {
            Default.SetValue(key, value);
        }
        public static bool ReadBoolean(string key)
        {
            return Default.GetBoolean(key);

        }
        public static string ReadString(string key)
        {
            return Default.GetString(key);
        }
        public static byte[] ReadData(string key)
        {
            return Default.GetData(key);
        }
        public static int ReadInt(string key)
        {
            return Default.GetInt(key);
        }
        public static DateTime ReadDateTime(string key)
        {
            return Default.GetDateTime(key);
        }
        public static decimal ReadDecimal(string key)
        {
            return Default.GetDecimal(key);
        }
        public static void End()
        {
            Default.Save();
        }
        public static void SaveSetting()
        {
            Default.Save();
        }
        public static void ReadSetting()
        {
            Default.Init();
        }
        public List<string > Keys
        {
            get {
                List<string> list = new List<string>();
                foreach (KeyValuePair<string, byte[]> key in dics)
                {
                    list.Add(key.Key);
                }
                return list;
            }
        }
        public static List<string> GetKeys()
        {
            return Default.Keys;
        }
    }
    public class SettingKeyValueCollection : IGetData
    {
        private List<SettingKeyValue> Keys { get; set; }

        public byte[] GetData()
        {
            return null;
        }
    }
    public class SettingKeyValue:IGetData
    {
        public string Key { get; set; }
        public byte[] Data { get; set; }

        public byte[] GetData()
        {
            byte[] buf = null;
            using (Feng.IO.BufferWriter bw = new BufferWriter())
            {
                bw.Write(this.Key);
                bw.Write(this.Data);
                buf = bw.GetData();
            }
            return buf;
        }
    }
    public interface IGetData
    {
        byte[] GetData();
    }
}
