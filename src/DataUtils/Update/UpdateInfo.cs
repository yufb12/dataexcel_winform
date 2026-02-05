using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.App
{
    public class UpdateInfo
    {
        private string _updataurl = string.Empty;
        private Version _version = Version.Empty;
        private int _updatemode = 1;
        private string _guid = string.Empty;
        private bool _autoupdate = false;
        private string _updatepassword = string.Empty;

        public bool AutoUpdate
        {
            get {
                return _autoupdate;
            }
            set {
                _autoupdate = value;
            }
        }
        public string UpdatePassword
        {
            get {
                return _updatepassword;
            }
            set {
                _updatepassword = value;
            }
        }
        public int UpdateMode {
            get {
                return this._updatemode;
            }
            set {
                this._updatemode = value;
            }
        }
        public string UpdataUrl {
            get {
                return this._updataurl;
            }
            set { 
                this._updataurl = value;
            }
        }
        public Version Version {
            get {
                return this._version;
            }
            set {
                this._version = value;
            }
        }
        public string Guid 
        {
            get {
                //[Guid("144DC177-0CA4-41AF-9420-F3F96B90B6BD")]
                if (_guid == string.Empty)
                {
                    _guid = System.Guid.NewGuid().ToString() + System.Guid.NewGuid().ToString();
                }
                return _guid;
            }
            set {
                _guid = value;
            }
        
        }
 
        public byte[] Data
        {
            get
            {
                byte[] bytes = new byte[1024 + 32];
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter(bytes))
                {
                    bw.Write(1,this.Version.First);
                    bw.Write(2,this.Version.Second);
                    bw.Write(3,this.Version.Third);
                    bw.Write(4,this.Version.Fouth); 
                    bw.Write(5,this.UpdataUrl);
                    bw.Write(6,this.UpdateMode);
                    bw.Write(7,this.Guid);
                    bw.Write(8,this.AutoUpdate);
                    bw.Write(9,this.UpdatePassword);
                    bw.Flush();
                }
                return bytes;
            }
        }

        public void Read(byte[] bytes)
        {
            using (Feng.IO.BufferReader bw = new IO.BufferReader(bytes))
            {
                this.Version.First = bw.ReadIndex(1, this.Version.First);
                this.Version.Second = bw.ReadIndex(2, this.Version.Second);
                this.Version.Third = bw.ReadIndex(3, this.Version.Third);
                this.Version.Fouth = bw.ReadIndex(4, this.Version.Fouth);

                this.UpdataUrl = bw.ReadIndex(5, this.UpdataUrl);
                this.UpdateMode = bw.ReadIndex(6, this.UpdateMode);
                this.Guid = bw.ReadIndex(7, Guid);
                this.AutoUpdate = bw.ReadIndex(8, AutoUpdate);
                this.UpdatePassword = bw.ReadIndex(9, UpdatePassword);
            }     
        }

        public override string ToString()
        {
            return this.Version.ToString() + " " + this.UpdataUrl;
        }
    }

    public class Version
    {
        public static readonly Version Empty;
        private ushort _first = 0;
        private ushort _secont = 0;
        private ushort _third = 0;
        private ushort _fouth = 1;

        public ushort First
        {
            get {
                return this._first;
            }
            set {
                this._first = value;
            }
        }
        public ushort Second
        {
            get {
                return this._secont;
            }
            set {
                this._secont = value;
            }
        }
        public ushort Third
        {
            get {
                return this._third;
            }
            set {
                this._third = value;
            }
        }
        public ushort Fouth
        {
            get {
                return this._fouth;
            }
            set {
                this._fouth = value;
            }
        }
        public override bool Equals(object obj)
        {
            Version ver = obj as Version;
            if (ver != null)
                return false;
            return base.Equals(obj);
        }
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", this.First, this.Second, this.Third, this.Fouth);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static int Compare(Version v1, Version v2)
        {
            if (v1.First > v2.First)
            {
                return 1;
            }
            else if (v1.First < v2.First)
            {
                return -1;
            }
            if (v1.Second > v2.Second)
            {
                return 1;
            }
            else if (v1.Second < v2.Second)
            {
                return -1;
            }
            if (v1.Third > v2.Third)
            {
                return 1;
            }
            else if (v1.Third <v2.Third)
            {
                return -1;
            }
            if (v1.Fouth > v2.Fouth)
            {
                return 1;
            }
            else if (v1.Fouth < v2.Fouth)
            {
                return -1;
            }
            return 0;
        }

        public static bool operator ==(Version v1, Version v2)
        {
            return Compare(v1, v2) == 0;
        }
        public static bool operator !=(Version v1, Version v2)
        {
            return Compare(v1, v2) != 0;
        }
        public static bool operator >(Version v1, Version v2)
        {
            return Compare(v1, v2) > 0;
        }
        public static bool operator >=(Version v1, Version v2)
        {
            return Compare(v1, v2) >= 0;
        }
        public static bool operator <(Version v1, Version v2)
        {
            return Compare(v1, v2)< 0;
        }
        public static bool operator <=(Version v1, Version v2)
        {
            return Compare(v1, v2) <= 0;
        }

        //public static void operator ++(Version v1)
        //{
        //    v1.Fouth = (ushort)(v1.Fouth + 1);
        //}

        //public static void operator --(Version v1)
        //{
        //    v1.Fouth = (ushort)(v1.Fouth - 1);
        //    if (v1.Fouth < 1)
        //        v1.Fouth = 0;
        //}
    }
}
