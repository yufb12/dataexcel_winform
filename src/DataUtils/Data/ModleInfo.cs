
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Feng.Data
{
    [Serializable]
    public class ModleInfo
    {
        public string Sql;
        private SqlParameter[] _cmdParms = null;
        public SqlParameter[] cmdParms
        {
            get
            {
                if (_cmdParms == null)
                {
                    return Empty;
                }
                return _cmdParms;
            }
            set
            {
                _cmdParms = value;
            }
        }
        public ModleInfo(string sql, SqlParameter[] cmdparms)
        {
            Sql = sql;
            _cmdParms = cmdparms;
        }
        private static SqlParameter[] _Empty = new SqlParameter[] { };
        public static SqlParameter[] Empty
        {
            get
            {
                return _Empty;
            }
        }
        public ModleInfo()
        {

        }
        public ModleInfo(string sql)
        {
            this.Sql = sql;
        }
    }
    public class UpdateModleInfo
    {
        public UpdateModleInfo()
        {

        }
        public UpdateModleInfo(string updatefield, object value)
        {
            Field = updatefield;
            Fields = new List<UpdateModleFieldInfo>();
            Fields.Add(new UpdateModleFieldInfo()
            {
                Value = value
            });
        }
        public UpdateModleInfo(string updatefield, string valuefield, object value)
        {
            Field = updatefield;
            Fields = new List<UpdateModleFieldInfo>();
            Fields.Add(new UpdateModleFieldInfo()
            {
                Field = valuefield,
                Value = value
            });
        }
        public string Field { get; set; }
        private bool _Accumulate = true;
        /// <summary>
        /// Default True
        ///True: Update table set qty= {new value}
        ///True: Update table set qty=@qty+ {new value}
        /// </summary>
        public bool Accumulate
        {
            get
            {
                return _Accumulate;
            }
            set
            {
                _Accumulate = value;
            }
        }
        public List<UpdateModleFieldInfo> Fields { get; set; }
    }
    public enum UpdateModleFieldOperator
    {
        Add,
        Delete,
        Times,
        Divide
    }
    public class UpdateModleFieldInfo
    {
        public string Field { get; set; }
        public object Value { get; set; }
        private UpdateModleFieldOperator _Operator = UpdateModleFieldOperator.Add;
        public UpdateModleFieldOperator Operator
        {
            get
            {
                return _Operator;
            }
            set
            {
                _Operator = value;
            }
        }
    }





}
