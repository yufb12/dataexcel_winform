using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using Feng.Excel.Designer;
using System.Drawing.Design;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using Feng.Utils;
using Feng.Forms.Controls.Designer;
using Feng.Data;
using Feng.Excel.Args;
using Feng.Excel.Edits;
using Feng.Excel.Interfaces;
using Feng.Excel.Delegates;
using Feng.Excel.Data;
using Feng.Excel.Base;

namespace Feng.Excel
{
    partial class DataExcel
    {
        #region IDataMember 成员

        private string _datamember = string.Empty;
        [Editor(typeof(DataMemberDesignerDesigner), typeof(UITypeEditor))]
        [DefaultValue("")]
        [Browsable(true)]
        public virtual string DataMember
        {
            get
            {
                return _datamember;
            }
            set
            {
                _datamember = value;
            }
        }

        #endregion



        //private object _bingdingvalue = null;
        //[Category(CategorySetting.DataBingding)]
        //public object BingdingValue
        //{
        //    get
        //    {
        //        return _bingdingvalue;
        //    }
        //    set
        //    {
        //        _bingdingvalue = value;
        //    }
        //}

        private int _bingdingindex = 0;
        [Category(CategorySetting.DataBingding)]
        [DefaultValue(0)]
        public int BingdingIndex
        {
            get
            {
                return _bingdingindex;
            }
            set
            {
                _bingdingindex = value;
            }
        }

        private object _DataSource = null;
        [AttributeProvider(typeof(IListSource))]
        [DefaultValue(null)]
        [Category(CategorySetting.DataBingding)]
        public object DataSource
        {
            get { return this._DataSource; }
            set
            {

                try
                {
                    this.BeginReFresh();
                    this.BeginSetFirstDisplayColumnIndex();
                    this.BeginSetFirstDisplayRowIndex();
                    if (value != null)
                    {
                    }
                    else
                    {
                        if (this._DataSource == null)
                        {
                            return;
                        }
                    }
                    bool newsource = true;
                    if (_DataSource != null)
                    {
                        if (value != null)
                        {
                            Type tvalue = value.GetType();
                            Type tsource = _DataSource.GetType();
                            if (tvalue == tsource)
                            {
                                newsource = false;
                            }
                        }
                    }

                    _DataSource = value;
                    _SortInfo = null;
                    this.pribindsource = null;

                    if (newsource)
                    {
                        this.ShowSelectAddRect = false;
                        this.ShowSelectBorder = true;
                    }
                    if (newsource)
                    {
                        if (this.AutoGenerateColumns)
                        {
                            InitDefultColumnFiles(_DataSource);
                            ReFreshColumnHeaderWidth(false);
                        }
                    }
                    this.ClearContents();
                    ReFreshDataSource(_DataSource);
                    refreshmaxrow();

                }
                finally
                {
                    this.EndReFresh();
                    this.EndSetFirstDisplayColumnIndex();
                    this.EndSetFirstDisplayRowIndex();
                }
            }
        }
        [NonSerialized]
        private Feng.Forms.ComponentModel.SortInfo _SortInfo = null;
        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Feng.Forms.ComponentModel.SortInfo SortInfo
        {
            get
            {
                return _SortInfo;
            }
            set
            {
                _SortInfo = value;
            }
        }
        [NonSerialized]
        private Feng.Forms.ComponentModel.FilterInfo _filterinfo = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Feng.Forms.ComponentModel.FilterInfo FilterInfo
        {
            get
            {
                return _filterinfo;
            }
            set
            {
                _filterinfo = value;
            }
        }

        public void SortDataSource()
        {
            if (this._DataSource is DataSet)
            {
                SortDataTable(((DataSet)this._DataSource).Tables[0]);
            }
            else if (this._DataSource is DataTable)
            {
                SortDataTable((DataTable)this._DataSource);
            }
            else if (this._DataSource is System.Collections.IList)
            {
                SortList(this._DataSource as System.Collections.IList);
            }
            this.ReFreshDataSource();
        }

        public void SetColumnVisible(string field, bool visible)
        {
            foreach (IColumn col in this.Columns)
            {
                if (col.ID.ToLower() == field.ToLower())
                {
                    col.Visible = visible;
                }
            }
        }
        public void SetColumnCaption(string field, string caption)
        {
            IColumn col = this.Columns[field];
            if (col == null)
            {
                col = this.ClassFactory.CreateDefaultColumn(this);
                col.Index = this.Columns.Max + 1;
                this.Columns.Add(col);
            }
            if (col != null)
            {
                col.Caption = caption;
                col.ID = field;
            }

        }
        public void SetColumnCaption(string field, string caption, int width)
        {
            IColumn col = this.Columns[field];
            if (col == null)
            {
                col = this.ClassFactory.CreateDefaultColumn(this);
                col.Index = this.Columns.Max + 1;
                this.Columns.Add(col);
            }
            if (col != null)
            {
                col.Caption = caption;
                col.ID = field;
                col.Width = width;
            }

        }
        public void SetColumnCaption(string field, int width)
        {
            SetColumnCaption(field, field, width);
        }
        public void SetColumnFormat(string field, FormatType formattype, string format)
        {
            foreach (IColumn col in this.Columns)
            {
                if (col.ID.ToLower() == field.ToLower())
                {
                    col.FormatType = formattype;
                    col.FormatString = format;
                }
            }
        }
        public void SetColumnWidth(string field, int width)
        {
            foreach (IColumn col in this.Columns)
            {
                if (col.ID.ToLower() == field.ToLower())
                {
                    col.Width = width;
                }
            }
        }
        public void FilterDataSource()
        {
            if (this._DataSource is DataSet)
            {
                FilterDataSource(((DataSet)this._DataSource).Tables[0]);
            }
            else if (this._DataSource is DataTable)
            {
                FilterDataSource((DataTable)this._DataSource);
            }
            else if (this._DataSource is System.Collections.IList)
            {
                FilterDataSource(this._DataSource as System.Collections.IList);
            }
            else
            {
                return;
            }
            if (ListFilter != null)
            {
                if (this.AllowAdd)
                {
                    this.MaxRow = ListFilter.Count + 1;
                }
                else
                {
                    this.MaxRow = ListFilter.Count;
                }
            }
            if (this.SortInfo != null)
            {
                this.SortDataSource();
            }
            //this.ReFreshFirstDisplayRowIndex();
        }

        public void FilterDataSource(DataTable table)
        {
            ListFilter = FilterDataTable(table, this.FilterInfo);

        }

        public void FilterDataSource(IList table)
        {
            ListFilter = FilterDataTable(table, this.FilterInfo);
        }

        public IColumn AddColumn(string field, string caption)
        {

            Column col = new Column(this);
            col.Index = ReSetHasValue().Y + 1;
            this.Columns.Add(col);
            col.ID = field;
            col.Caption = caption;
            return col;
        }

        private List<int> FilterDataTable(IList table, Feng.Forms.ComponentModel.FilterInfo filterinfo)
        {
            if (filterinfo == null)
                return null;
            List<int> list = new List<int>();

            for (int i = 0; i < table.Count; i++)
            {
                object dr = table[i];
                for (int j = 0; j < filterinfo.Count; j++)
                {
                    Feng.Forms.ComponentModel.Filter ft = filterinfo[j];
                    Type t = dr.GetType();
                    PropertyInfo pi = t.GetProperty(ft.Field);
                    if (ft.Value.Equals(pi.GetValue(dr, null)))
                    {
                        list.Add(i);
                        break;
                    }
                }
            }

            return list;
        }
        private List<int> FilterDataTable(System.Data.DataTable table, Feng.Forms.ComponentModel.FilterInfo filterinfo)
        {
            if (filterinfo == null)
                return null;
            List<int> list = new List<int>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow dr = table.Rows[i];
                for (int j = 0; j < filterinfo.Count; j++)
                {
                    Feng.Forms.ComponentModel.Filter ft = filterinfo[j];

                    if (ft.Value.Equals(dr[ft.Field]))
                    {
                        list.Add(i);
                        break;
                    }
                }
            }

            return list;
        }
        private List<int> ListFilter;
        private void SortDataTable(System.Data.DataTable table)
        {
            if (this._SortInfo == null)
            {
                return;
            }
            List<DataRow> list = new List<DataRow>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (ListFilter != null)
                {
                    if (!ListFilter.Contains(i))
                    {
                        continue;
                    }
                }

                list.Add(table.Rows[i]);
            }
            list.Sort(ComparerSortInfo);

            this.pribindsource = list;
        }

        int ComparerSortInfo(DataRow old, DataRow newobj)
        {
            foreach (Feng.Forms.ComponentModel.SortColumn col in _SortInfo)
            {
                if (Compare(old[col.Field], newobj[col.Field], col.Type) > 0)
                {
                    if (col.SortOrder == Feng.Forms.ComponentModel.SortOrder.Ascending)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else if (Compare(old[col.Field], newobj[col.Field], col.Type) < 0)
                {
                    if (col.SortOrder == Feng.Forms.ComponentModel.SortOrder.Ascending)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        int ComparerSortInfo(object value1, object value2)
        {
            object old = value1; object newobj = value2;
            if (old == null)
            {
                old = newobj;
            }
            if (newobj == null)
            {
                newobj = old;
            }
            if (old == null)
            {
                return 0;
            }
            Type t = old.GetType();

            foreach (Feng.Forms.ComponentModel.SortColumn col in _SortInfo)
            {
                object valuep1 = ReflectionHelper.GetValue(old, col.Field);
                object valuep2 = ReflectionHelper.GetValue(newobj, col.Field);
                int rescompare = Compare(valuep1, valuep2, col.Type);
                if (rescompare > 0)
                {
                    if (col.SortOrder == Feng.Forms.ComponentModel.SortOrder.Ascending)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else if (rescompare < 0)
                {
                    if (col.SortOrder == Feng.Forms.ComponentModel.SortOrder.Ascending)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public int Compare(object oldvalue, object newvalue, byte type)
        {
            if (oldvalue == null && newvalue == null)
            {
                return 0;
            }
            else if (oldvalue == null)
            {
                return -1;
            }
            else if (newvalue == null)
            {
                return 1;
            }


            if (oldvalue == DBNull.Value && newvalue == DBNull.Value)
            {
                return 0;
            }
            else if (oldvalue == DBNull.Value)
            {
                return -1;
            }
            else if (newvalue == DBNull.Value)
            {
                return 1;
            }
            int result = 0;
            switch (type)
            {
                case TypeEnum.Tbool:
                    result = CompareHelper.Compare((bool)oldvalue, (bool)newvalue);
                    break;
                case TypeEnum.Tstring:
                    result = CompareHelper.Compare((string)oldvalue, (string)newvalue);
                    break;
                case TypeEnum.Tdecimal:
                    result = CompareHelper.Compare((decimal)oldvalue, (decimal)newvalue);
                    break;
                case TypeEnum.Tbyte:
                    result = CompareHelper.Compare((byte)oldvalue, (byte)newvalue);
                    break;
                case TypeEnum.Tchar:
                    result = CompareHelper.Compare((char)oldvalue, (char)newvalue);
                    break;
                case TypeEnum.TDateTime:
                    result = CompareHelper.Compare((DateTime)oldvalue, (DateTime)newvalue);
                    break;
                case TypeEnum.TNullableDateTime:
                    result = CompareHelper.Compare((DateTime?)oldvalue, (DateTime?)newvalue);
                    break;
                case TypeEnum.Tdouble:
                    result = CompareHelper.Compare((double)oldvalue, (double)newvalue);
                    break;
                case TypeEnum.Tfloat:
                    result = CompareHelper.Compare((int)oldvalue, (int)newvalue);
                    break;
                case TypeEnum.Tint:
                    result = CompareHelper.Compare((int)oldvalue, (int)newvalue);
                    break;
                case TypeEnum.Tlong:
                    result = CompareHelper.Compare((long)oldvalue, (long)newvalue);
                    break;
                case TypeEnum.Tsbyte:
                    result = CompareHelper.Compare((sbyte)oldvalue, (sbyte)newvalue);
                    break;
                case TypeEnum.Tshort:
                    result = CompareHelper.Compare((short)oldvalue, (short)newvalue);
                    break;
                case TypeEnum.Tuint:
                    result = CompareHelper.Compare((uint)oldvalue, (uint)newvalue);
                    break;
                case TypeEnum.Tulong:
                    result = CompareHelper.Compare((ulong)oldvalue, (ulong)newvalue);
                    break;
                case TypeEnum.Tushort:
                    result = CompareHelper.Compare((ushort)oldvalue, (ushort)newvalue);
                    break;
                default:
                    Type tp = oldvalue.GetType();
                    if (tp == typeof(DateTime))
                    {
                        result = CompareHelper.Compare((DateTime)oldvalue, (DateTime)newvalue);
                    }

                    else if (tp == typeof(bool))
                    {
                        result = CompareHelper.Compare((bool)oldvalue, (bool)newvalue);
                    }

                    else if (tp == typeof(string))
                    {
                        result = CompareHelper.Compare((string)oldvalue, (string)newvalue);
                    }

                    else if (tp == typeof(decimal))
                    {
                        result = CompareHelper.Compare((decimal)oldvalue, (decimal)newvalue);
                    }

                    else if (tp == typeof(byte))
                    {
                        result = CompareHelper.Compare((byte)oldvalue, (byte)newvalue);
                    }

                    else if (tp == typeof(char))
                    {
                        result = CompareHelper.Compare((char)oldvalue, (char)newvalue);
                    }

                    else if (tp == typeof(DateTime))
                    {
                        result = CompareHelper.Compare((DateTime)oldvalue, (DateTime)newvalue);
                    }

                    else if (tp == typeof(DateTime?))
                    {
                        result = CompareHelper.Compare((DateTime?)oldvalue, (DateTime?)newvalue);
                    }

                    else if (tp == typeof(double))
                    {
                        result = CompareHelper.Compare((double)oldvalue, (double)newvalue);
                    }

                    else if (tp == typeof(int))
                    {
                        result = CompareHelper.Compare((int)oldvalue, (int)newvalue);
                    }

                    else if (tp == typeof(int))
                    {
                        result = CompareHelper.Compare((int)oldvalue, (int)newvalue);
                    }

                    else if (tp == typeof(long))
                    {
                        result = CompareHelper.Compare((long)oldvalue, (long)newvalue);
                    }

                    else if (tp == typeof(sbyte))
                    {
                        result = CompareHelper.Compare((sbyte)oldvalue, (sbyte)newvalue);
                    }

                    else if (tp == typeof(short))
                    {
                        result = CompareHelper.Compare((short)oldvalue, (short)newvalue);
                    }

                    else if (tp == typeof(uint))
                    {
                        result = CompareHelper.Compare((uint)oldvalue, (uint)newvalue);
                    }

                    else if (tp == typeof(ulong))
                    {
                        result = CompareHelper.Compare((ulong)oldvalue, (ulong)newvalue);
                    }

                    else if (tp == typeof(ushort))
                    {
                        result = CompareHelper.Compare((ushort)oldvalue, (ushort)newvalue);
                    }
                    break;
            }
            return result;
        }
        private object pribindsource = null;
        protected object PriaveteBindingSource
        {
            get
            {
                if (pribindsource == null)
                    return this.DataSource;
                return pribindsource;
            }
        }
        public void SortList(IList table)
        {
            if (this._SortInfo == null)
            {
                return;
            }
            List<object> list = new List<object>();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }

            list.Sort(ComparerSortInfo);
            pribindsource = list;
        }

        public System.Data.DataRow GetCurrentDataRow()
        {
            if (this.FocusedCell == null)
                return null;
            return this.GetDataRow(this.FocusedCell.Row.Index);

        }

        public System.Data.DataRow GetDataRow(int index)
        {
            if (index < 1)
            {
                return null;
            }
            DataTable table = null;
            if (this._DataSource is DataSet)
            {
                table = (((DataSet)this._DataSource).Tables[0]);
            }
            else if (this._DataSource is DataTable)
            {
                table = ((DataTable)this._DataSource);
            }
            else
            {
                return null;
            }
            if (table == null)
            {
                return null;
            }
            int rowindex = index - 1;


            if (ListFilter != null)
            {
                if (rowindex < ListFilter.Count)
                {
                    return table.Rows[ListFilter[rowindex]];
                }
            }
            if (rowindex < table.Rows.Count)
            {
                return table.Rows[rowindex];
            }

            return null;
        }
        public System.Data.DataRow GetDataRow(ICell cell)
        {
            return GetDataRow(cell.Row.Index);
        }
        public System.Data.DataRow GetDataRow(IRow row)
        {
            return GetDataRow(row.Index);
        }

        public object GetBingdingRow(int index)
        {
            if (index < 1)
                return null;
            System.Collections.IList table = null;
            if (this._DataSource is System.Collections.IList)
            {
                table = ((System.Collections.IList)this._DataSource);
            }
            else
            {
                return null;
            }
            int rowindex = index - 1;

            if (ListFilter != null)
            {

                if (rowindex < ListFilter.Count)
                {
                    rowindex = ListFilter[rowindex];
                    return table[rowindex];
                }
            }
            else
            {
                if (rowindex < table.Count)
                {
                    return table[rowindex];
                }
            }
            return null;
        }
        /// <summary>
        /// GetDataSourceModel
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public object GetBingdingRowObject(IRow row)
        {
            return GetBingdingRow(row.Index);
        }
        /// <summary>
        /// GetDataSourceModel
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public object GetBingdingRowObject(ICell cell)
        {
            if (cell == null)
                return null;
            return GetBingdingRow(cell.Row.Index);
        }
        public void InitDefultColumnFiles(object datasource)
        {
            if (_AutoGenerateColumns)
            {
                if (datasource is DataSet)
                {
                    if (string.IsNullOrEmpty(this._datamember))
                    {
                        InitDefaultColumnFields(((DataSet)datasource).Tables[0]);
                    }
                    else
                    {
                        InitDefaultColumnFields(((DataSet)datasource).Tables[this._datamember]);
                    }
                }
                else if (datasource is DataTable)
                {
                    InitDefaultColumnFields((DataTable)datasource);
                }
                else if (IsIlistDataSource(datasource))
                {
                    InitDefaultColumnFields(datasource as System.Collections.IList);
                }
            }
        }
        public virtual bool IsIlistDataSource(object datasource)
        {
            return datasource is System.Collections.IList;
            //Type t = datasource.GetType().GetGenericArguments()[0];
            //t = datasource.GetType().GetInterface("System.Collections.Generic.IList");
            //if (t != null)
            //{
            //    return true;
            //}
            //return false;
        }
        public void InitDefaultColumnFields(DataTable table)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                int columnindex = i + 1;
                IColumn col = this.Columns[columnindex];

                if (col == null)
                {
                    col = ClassFactory.CreateDefaultColumn(this, columnindex);
                    this.Columns.Add(col);
                }
                if (col != null)
                {
                    col.Caption = table.Columns[i].ColumnName;
                    col.ID = table.Columns[i].ColumnName;
                    col.DataType = table.Columns[i].DataType;
                }
            }
            foreach (IRow row in this.Rows)
            {
                if (row.Index < 1)
                {
                    continue;
                }
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    IColumn col = this.Columns[i + 1];
                    if (col == null)
                    {
                        if (this.AutoGenerateColumns)
                        {
                            col = this.ClassFactory.CreateDefaultColumn(this, i + 1);
                            this.Columns.Add(col);
                        }
                    }
                    if (col == null)
                    {
                        break;
                    }
                    ICell cell = row[col];
                    if (cell == null)
                    {
                        cell = this.ClassFactory.CreateDefaultCell(row, col);
                    }
                }

            }

        }

        public void InitDefaultColumnFields(System.Collections.IList datasource)
        {
            Type t = this._DataSource.GetType().GetGenericArguments()[0];
            System.Reflection.PropertyInfo[] ps = t.GetProperties();

            for (int i = 0; i < ps.Length; i++)
            {
                int columnindex = i + 1;
                IColumn col = this.Columns[columnindex];

                if (col == null)
                {
                    col = ClassFactory.CreateDefaultColumn(this, columnindex);
                    this.Columns.Add(col);
                }
                if (col != null)
                {
                    col.Caption = ps[i].Name;
                    col.DataType = ps[i].PropertyType;
                    col.ID = ps[i].Name;
                }
            }
            foreach (IRow row in this.Rows)
            {
                if (row.Index < 1)
                {
                    continue;
                }
                for (int i = 0; i < ps.Length; i++)
                {
                    IColumn col = this.Columns[i + 1];
                    if (col != null)
                    {
                        ICell cell = row[col];
                        if (cell == null)
                        {
                            cell = this.ClassFactory.CreateDefaultCell(row, col);
                        }
                    }
                }

            }
        }
        public void ClearBingdingRow()
        {
            for (int i = 1; i < this.Rows.Max; i++)
            {
                this.Rows.RemoveAt(i);
            }
        }
        public void ReFreshDataSource()
        {
            this.ClearBingdingRow();
            ReFreshDataSource(this.PriaveteBindingSource);
            this.ReFreshFirstDisplayRowIndex();
        }

        public void ReFreshDataSource(object datasource)
        {
            if (datasource == null)
            {
                return;
            }

            if (datasource is DataSet)
            {
                DataSet ds = (DataSet)datasource;
                if (ds.Tables.Contains(this.DataMember))
                {
                    BindingDatatableValues(ds.Tables[this.DataMember]);
                }
                else
                {
                    if (string.IsNullOrEmpty(this.DataMember))
                    {
                        if (ds.Tables.Count > 0)
                        {
                            BindingDatatableValues(ds.Tables[0]);
                        }
                    }
                }
            }
            else if (datasource is DataTable)
            {
                BindingDatatableValues((DataTable)datasource);
            }
            else if (datasource is System.Collections.IList)
            {
                BindingIlistValues(datasource as System.Collections.IList);
            }

            refreshmaxrow();
        }

        //public void BingDataSource()
        //{
        //    System.Windows.Forms.BindingManagerBase bmb = new System.Windows.Forms.BindingManager();


        //}

        public void BindingDatatableValues(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                IRow row = this.GetRow(i + 1);
                if (row.Index <= 0)
                {
                    continue;
                }

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    IColumn column = this.GetColumn(j + 1);
                    if (column != null)
                    {
                        if (column.Index <= 0)
                        {
                            continue;
                        }
                        ICell cell = row[column];
                        if (cell == null)
                        {
                            cell = this.ClassFactory.CreateDefaultCell(row, column);
                        }
                        string fieldname = column.ID;
                        if (row.Index <= table.Rows.Count)
                        {
                            if (table.Columns.Contains(fieldname))
                            {
                                int rowindex = row.Index - 1;
                                if (ListFilter != null)
                                {
                                    if (rowindex < ListFilter.Count)
                                    {
                                        rowindex = ListFilter[rowindex];
                                        object value = table.Rows[rowindex][fieldname];
                                        cell.BingValue = value;
                                        continue;
                                    }
                                }
                                else
                                {
                                    object value = table.Rows[rowindex][fieldname];
                                    cell.BingValue = value;
                                    continue;
                                }

                            }
                        }
                        cell.BingValue = null;
                    }
                }
            }

        }

        public void BindingIlistValues(System.Collections.IList ilist)
        {

            Type t = null;
            if (ilist.Count < 1)
            {
                t = ilist.GetType().GetGenericArguments()[0];
            }
            else
            {
                t = ilist[0].GetType();
            }
            if (this.AllowAdd)
            {
                this.MaxRow = ilist.Count + 1;
            }
            else
            {
                this.MaxRow = ilist.Count;
            }
            PropertyInfo[] ps = t.GetProperties();
            if (ps == null)
                return;
            for (int ilistindex = 0; ilistindex < ilist.Count; ilistindex++)
            {
                IRow row = this.GetRow(ilistindex + 1);

                if (row.Index <= ilist.Count && row.Index > 0)
                {
                    int rowindex = row.Index - 1;
                    object value = ilist[rowindex];
                    if (value is DataRow)
                    {
                        SetRowBindingValue(row, value as DataRow);
                    }
                    else
                    {
                        SetRowBindingValue(row, value);
                    }
                }
            }
        }
        public void SetRowBindingValue(IRow row, DataRow datarow)
        {
            foreach (IColumn column in this.Columns)
            {
                if (column.Index <= 0)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(column.ID))
                {
                    continue;
                }
                ICell cell = row[column];
                if (cell == null)
                {
                    cell = this.ClassFactory.CreateDefaultCell(row, column);
                }

                object value = datarow[column.ID];
                cell.BingValue = value;
                string text = cell.Text;
            }
        }

        public void RefreshRow(IBaseCell cell)
        {
            IRow row = cell.Row;
            RefreshRow(row);
        }
        public void RefreshRow(IRow row)
        {
            object model = this.GetBingdingRowObject(row);
            SetRowBindingValue(row, model);
        }
        public void SetRowBindingValue(IRow row, object model)
        {
            foreach (IColumn column in this.Columns)
            {
                if (column.Index <= 0)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(column.ID))
                {
                    continue;
                }
                ICell cell = row[column];
                if (cell == null)
                {
                    cell = this.ClassFactory.CreateDefaultCell(row, column);
                }
                Type t = model.GetType();
                PropertyInfo pt = t.GetProperty(column.ID);
                if (pt != null)
                {
                    object value = pt.GetValue(model, null);
                    cell.BingValue = value;
                    string text = cell.Text;
                    continue;
                }
                cell.BingValue = null;
            }
        }

        public void UpdateBingTableValue(DataTable table, ICell cell, object value)
        {
            IRow row = cell.Row;
            string fieldname = cell.Column.ID;
            int position = row.Index - 1;
            if (this.ListFilter != null)
            {
                if (position == this.ListFilter.Count)
                {
                    if (this.AllowAdd)
                    {
                        DataRow newrow = table.NewRow();
                        newrow[fieldname] = value;
                        table.Rows.Add(newrow);

                        this.ListFilter.Add(table.Rows.IndexOf(newrow));
                        this.MaxRow = this.MaxRow + 1;
                        return;
                    }
                }
            }

            else if (position == table.Rows.Count)
            {
                if (this.AllowAdd)
                {
                    DataRow newrow = table.NewRow();
                    newrow[fieldname] = value;
                    table.Rows.Add(newrow);
                    this.refreshmaxrow();
                    return;
                }
            }

            DataRow newrow2 = GetDataRow(row);
            if (newrow2 != null)
            {
                newrow2[fieldname] = value;
            }

        }
        public static bool IsValidDataSource(object datasource)
        {
            if (datasource == null) return true;
            if (datasource is IList) return true;
            if (datasource is IListSource) return true;
            if (datasource is DataSet) return true;
            if (datasource is System.Data.DataView)
            {
                System.Data.DataView dv = datasource as System.Data.DataView;
                if (dv.Table == null) return false;
                return true;
            }
            if (datasource is System.Data.DataTable) return true;
            return false;
        }
        public void UpdateBingListValue(System.Collections.IList ilist, ICell cell, object value)
        {
            IRow row = cell.Row;
            string fieldname = cell.Column.ID;
            Type t = null;
            PropertyInfo pt = null;
            int position = row.Index - 1;
            t = this.DataSource.GetType().GetGenericArguments()[0];

            if (this.ListFilter != null)
            {
                if (position == this.ListFilter.Count)
                {
                    if (this.AllowAdd)
                    {
                        if (t.IsClass)
                        {
                            object newmodel = t.Assembly.CreateInstance(t.FullName);
                            pt = t.GetProperty(fieldname);
                            ReflectionHelper.SetValue(pt, newmodel, value);
                            if (BeforeAddNewBindingData != null)
                            {
                                DataExcelCancelEventArgs e = new DataExcelCancelEventArgs();
                                BeforeAddNewBindingData(this, newmodel, e);
                                if (e.Cancel)
                                {
                                    return;
                                }
                            }
                            ilist.Add(newmodel);
                            SetRowBindingValue(row, newmodel);
                            this.ListFilter.Add(ilist.IndexOf(newmodel));
                            this.MaxRow = this.MaxRow + 1;
                            return;
                        }
                    }
                    return;
                }
            }
            else if (position == ilist.Count)
            {
                if (this.AllowAdd)
                {
                    if (t.IsClass)
                    {
                        object newmodel = t.Assembly.CreateInstance(t.FullName);
                        pt = t.GetProperty(fieldname);
                        ReflectionHelper.SetValue(pt, newmodel, value);
                        if (BeforeAddNewBindingData != null)
                        {
                            DataExcelCancelEventArgs e = new DataExcelCancelEventArgs();
                            BeforeAddNewBindingData(this, newmodel, e);
                            if (e.Cancel)
                            {
                                return;
                            }
                        }
                        ilist.Add(newmodel);
                        SetRowBindingValue(row, newmodel);
                        this.MaxRow = this.MaxRow + 1;
                        return;
                    }
                }
            }

            t = this.DataSource.GetType().GetGenericArguments()[0];
            pt = t.GetProperty(fieldname);
            object obj = GetBingdingRowObject(row);
            //DataExcelCancelEventArgs ee = new DataExcelCancelEventArgs();
            //if (BeforeUpdateBindingData != null)
            //{
            //    BeforeUpdateBindingData(this, cell, obj, value,ee);
            //}
            //if (ee.Cancel)
            //{ 
            //    return;
            //}
            ReflectionHelper.SetValue(pt, obj, value);
        }

        //public delegate void BeforeUpdateBindingDataHandler(object sender, IBaseCell cell, object model, object value, DataExcelCancelEventArgs e);
        //public event BeforeUpdateBindingDataHandler BeforeUpdateBindingData;

        public event BeforeAddNewBindingDataHandler BeforeAddNewBindingData;

        public void UpateBingValue(ICell cell, object value)
        {
            IRow row = cell.Row;
            string fieldname = cell.Column.ID;
            int position = row.Index - 1;
            if (this.DataSource is DataSet)
            {
                DataTable ilist = (this.DataSource as DataSet).Tables[0];
                UpdateBingTableValue(ilist, cell, value);
            }
            else if (this.DataSource is DataTable)
            {
                DataTable ilist = this.DataSource as DataTable;
                UpdateBingTableValue(ilist, cell, value);
            }
            else if (this.DataSource is System.Collections.IList)
            {
                System.Collections.IList ilist = this.DataSource as System.Collections.IList;
                UpdateBingListValue(ilist, cell, value);
            }
        }

        public int GetBingingDataSourceCount()
        {
            object datasource = this.DataSource;
            if (datasource is DataSet)
            {
                if (string.IsNullOrEmpty(this._datamember))
                {
                    return ((DataSet)datasource).Tables[0].Rows.Count;
                }
                else
                {
                    return (((DataSet)datasource).Tables[this._datamember].Rows.Count);
                }
            }
            else if (datasource is DataTable)
            {
                return ((DataTable)datasource).Rows.Count;
            }
            else if (datasource is System.Collections.IList)
            {
                return (datasource as System.Collections.IList).Count;
            }
            return 0;
        }

        public void Filter(ICell cell)
        {
            if (this.FilterInfo == null)
            {
                this.FilterInfo = new Feng.Forms.ComponentModel.FilterInfo();
            }
            this.FilterInfo.Clear();
            this.FilterInfo.Add(new Feng.Forms.ComponentModel.Filter()
            {
                Field = cell.Column.ID,
                Value = cell.Value,
                ValueType = TypeEnum.GetTypeEnum(cell.Column.DataType)
            }
                );
            this.FilterDataSource();
        }
 
        public void Filter(ISelectCellCollection cells)
        {
            if (this.FilterInfo == null)
            {
                this.FilterInfo = new Feng.Forms.ComponentModel.FilterInfo();
            }
            this.FilterInfo.Clear();
            List<ICell> list = cells.GetAllCells();
            foreach (ICell cell in list)
            {
                this.FilterInfo.Add(new Feng.Forms.ComponentModel.Filter()
                {
                    Field = cell.Column.ID,
                    Value = cell.Value,
                    ValueType = TypeEnum.GetTypeEnum(cell.Column.DataType)
                }
                    );
            }
            this.FilterDataSource();
        }

        public void ClearFilter()
        {
            this.ListFilter = null;
            this._filterinfo = null;
            FilterDataSource();
            this.SortDataSource();
            this.refreshmaxrow();
            this.ReFreshDataSource(this.DataSource);
        }

        public DataRow GetFourceDataRow()
        {
            if (this.FocusedCell == null)
            {
                return null;
            }
            return this.GetDataRow(this.FocusedCell.Row);
        }

        public object GetFourceRow()
        {
            if (this.FocusedCell == null)
            {
                return null;
            }
            return this.GetBingdingRowObject(this.FocusedCell.Row);
        }

        public static string GetTableName(string field)
        {
            int i = field.IndexOf("\\");
            string str = string.Empty;
            if (i > 0)
            {
                str = field.Substring(0, i);
            }
            return str;
        }

        public static string GetColumnName(string field)
        {
            int i = field.IndexOf("\\");
            int j = field.IndexOf(":");
            if (i > 0)
            {
                if (j > 0)
                {
                    string str = field.Substring(i + 1, j - i - 1);
                    return str.Trim();
                }
            }
            return string.Empty;
        }

        public static int GetRowIndex(string field)
        {
            int i = field.IndexOf("{");
            int j = field.IndexOf("}");
            if (i > 0)
            {
                if (j > 0)
                {
                    string str = field.Substring(i + 1, j - i - 1);
                    int index = 1;
                    if (int.TryParse(str, out index))
                    {
                        return index;
                    }
                }
            }
            return 1;
        }

        public FieldDataBaseInfo GetFieldDataBase()
        {
            FieldDataBaseInfo database = new FieldDataBaseInfo();
            foreach (ICell cell in this.FieldCells)
            {
                string tablename = GetTableName(cell.FieldName);
                string columnname = GetColumnName(cell.FieldName);
                int rowindex = GetRowIndex(cell.FieldName);
                FieldTableInfo fti = database[tablename];
                if (fti == null)
                {
                    fti = new FieldTableInfo(database);
                    fti.TableName = tablename;
                    database.Add(fti);
                }
                FieldRowInfo row = fti[rowindex];
                if (row == null)
                {
                    row = new FieldRowInfo(fti);
                    row.Index = rowindex;
                    fti.Add(row);
                }
                FieldCellInfo fci = new FieldCellInfo(row);
                fci.Cell = cell;
                fci.ColumName = columnname.ToLower();
                row.Add(fci);
            }
            return database;
        }

        //public virtual void SettJson(string json)
        //{
        //    Json.JsonObj jsonobj= Feng.Json.Parse.Parese(json); 
        //}

        //public virtual void SetJson(Json.JsonObj jsonobj)
        //{

        //}
    }
}
