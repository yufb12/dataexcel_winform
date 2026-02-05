using System; 
namespace Feng.Data
{
    public class ColumnInfo
    {
        private int _cid = 0;
        public int CID
        {
            get
            {
                return _cid;
            }
            set
            {
                _cid = value;
            }
        }

        private bool _primarykey = false;
        public bool PrimaryKey
        {
            get
            {
                return _primarykey;
            }
            set
            {
                _primarykey = value;
            }
        }

        private string _columnname = string.Empty;
        public string ColumnName
        {
            get
            {
                return _columnname;
            }
            set
            {
                _columnname = value;
            }
        }

        private string _columntype = string.Empty;
        public string ColumnType
        {
            get
            {
                return _columntype;
            }
            set
            {
                _columntype = value;
            }
        }


        private string _defaultvalue = string.Empty;
        public string DefaultValue
        {
            get
            {
                return _defaultvalue;
            }
            set
            {
                _defaultvalue = value;
            }
        }

        private int _columnwidth = 0;
        public int ColumnWidth
        {
            get
            {
                return _columnwidth;
            }
            set
            {
                _columnwidth = value;
            }
        }

        private int _digits = 0;
        public int Digits
        {
            get
            {
                return _digits;
            }
            set
            {
                _digits = value;
            }
        }

        private bool _nullable = false;
        public bool Nullable
        {
            get
            {
                return _nullable;
            }
            set
            {
                _nullable = value;
            }
        }

        private bool _identity = false;
        public bool Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }
    }
}
