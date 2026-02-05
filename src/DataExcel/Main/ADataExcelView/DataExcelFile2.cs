using Feng.Data;
using Feng.Enums;
using Feng.Excel.App;
using Feng.Excel.Collections;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        private DataStruct DataValueDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "DataValue";
                Feng.Data.DataValueCollection datavalues = GetCellDataValueCollection();
                byte[] data = GetDataValueData(datavalues);
                dataStruct.Data = (data);
                return dataStruct;
            }
        }
        private DataStruct EditControlDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "EditControl";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    List<ICellEditControl> list = GetCellEditList();
                    DataStructCollection datastructs = new DataStructCollection();
                    for (int i = 0; i < list.Count; i++)
                    {
                        ICellEditControl ie = list[i];
                        ie.AddressID = i + 1;
                        datastructs.Add(ie.Data);
                    }
                    bw.Write(datastructs);
                    dataStruct.Data = bw.GetData();
                }

                return dataStruct;
            }
        }
        private DataStruct DataFileDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "DataFile";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    DataStruct griddata = this.Data;
                    griddata.FullName = "Grid";
                    bw.Write(griddata);


                    DataStruct editdata = this.EditControlDataStruct;
                    editdata.FullName = "Edit";
                    bw.Write(editdata);

                    DataStruct columndata = this.Columns.Data;
                    columndata.FullName = "Column";
                    bw.Write(columndata);

                    DataStruct rowdata = this.Rows.Data;
                    rowdata.FullName = "Row";
                    bw.Write(rowdata);

                    if (this.MergeCells != null)
                    {
                        DataStruct mergedata = this.MergeCells.Data;
                        mergedata.FullName = "Merge";
                        bw.Write(mergedata);
                    }
                    byte[] data = bw.GetData();
                    data = Feng.IO.CompressHelper.GZipCompress(data);
                    dataStruct.Data = data;
                }
                return dataStruct;
            }
        }
        private DataStruct DisplayAreaDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "DisplayArea";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    bw.Write(this.DisplayArea.BeginCell.Row.Index);
                    bw.Write(this.DisplayArea.BeginCell.Column.Index);
                    bw.Write(this.DisplayArea.EndCell.Row.Index);
                    bw.Write(this.DisplayArea.EndCell.Column.Index);
                    dataStruct.Data = bw.GetData();
                }
                return dataStruct;
            }
        }
        private DataStruct PrintAreaDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "DisplayArea";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    bw.Write(118, this.PrintArea.BeginCell.Row.Index);
                    bw.Write(118, this.PrintArea.BeginCell.Column.Index);
                    bw.Write(118, this.PrintArea.EndCell.Row.Index);
                    bw.Write(118, this.PrintArea.EndCell.Column.Index);
                    dataStruct.Data = bw.GetData();
                }
                return dataStruct;
            }
        }
        private DataStruct UserDefineExtensDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "DisplayArea";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    if (this.UserDefineExtensData.Count > 0)
                    {
                        bw.Write(ConstantValue.ExtendData);
                        bw.WriteInt(this.UserDefineExtensData.Count);
                        foreach (System.Collections.Generic.KeyValuePair<string, DataStruct> key in this.UserDefineExtensData)
                        {
                            bw.Write(key.Key);
                            bw.Write(key.Value);
                        }
                    }
                    dataStruct.Data = bw.GetData();
                }
                return dataStruct;
            }
        }
        private DataStruct ExtendDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "Extend";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    if (this.BackCells != null)
                    {
                        DataStruct data = this.BackCells.Data;
                        data.FullName = "BackCell";
                        bw.Write(data);
                    }
                    if (this.ListExtendCells != null)
                    {
                        DataStruct data = this.ListExtendCells.Data;
                        data.FullName = "ListExtend";
                        bw.Write(data);
                    }
                    if (this.DisplayArea != null)
                    {
                        DataStruct data = this.DisplayAreaDataStruct;
                        data.FullName = "DisplayArea";
                        bw.Write(data);
                    }
                    if (this.PrintArea != null)
                    {
                        DataStruct data = this.PrintAreaDataStruct;
                        data.FullName = "PrintArea";
                        bw.Write(data);
                    }
                    if (this.UserDefineExtensData != null)
                    {
                        if (this.UserDefineExtensData.Count > 0)
                        {
                            DataStruct data = this.UserDefineExtensDataStruct;
                            data.FullName = "UserDefine";
                            bw.Write(data);
                        }
                    }
                    dataStruct.Data = bw.GetData();
                }
                return dataStruct;
            }
        }
        private DataStruct CodeDataStruct
        {
            get
            {
                if (string.IsNullOrEmpty(this.Code))
                    return null;
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "Code";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    bw.Write(this.Code);
                    dataStruct.Data = bw.GetData();
                }
                return dataStruct;
            }
        }
        private DataStruct FilterStruct
        {
            get
            {
                if (this.FilterExcel == null)
                    return null;
                DataStruct dataStruct = this.FilterExcel.Data;
                dataStruct.FullName = "Filter";
                return dataStruct;
            }
        }
        private DataStruct DataBaseDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "DataBase";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                { 
                    bw.Write(this.CellDataBase.Data);
                    dataStruct.Data = bw.GetData();
                }
                return dataStruct;
            }
        }
        private DataStruct BaseDataStruct
        {
            get
            {
                DataStruct dataStruct = new DataStruct();
                dataStruct.FullName = "Base";
                using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
                {
                    DataStruct dsbase = base.Data;
                    bw.Write(dsbase);
                    dataStruct.Data = bw.GetData();
                }
                return dataStruct;
            }
        }

        public virtual void SaveIOVersion(Feng.Excel.IO.BinaryWriter bw)
        {
            bw.Write(ConstantValue.FileHeader);
            bw.Write(IOVersion_DataStruct);
            bw.Write(HasPassword);
            DataStruct dataStruct = DataValueDataStruct;
            if (dataStruct != null)
            {
                bw.Write(EncryptDataStruct(dataStruct));
            }
            dataStruct = DataFileDataStruct;
            if (dataStruct != null)
            {
                bw.Write(EncryptDataStruct(dataStruct));
            }
            dataStruct = ExtendDataStruct;
            if (dataStruct != null)
            {
                bw.Write(EncryptDataStruct(dataStruct));
            }
            dataStruct = CodeDataStruct;
            if (dataStruct != null)
            {
                bw.Write(EncryptDataStruct(dataStruct));
            }
            dataStruct = DataBaseDataStruct;
            if (dataStruct != null)
            {
                bw.Write(EncryptDataStruct(dataStruct));
            }
            dataStruct = FilterStruct;
            if (dataStruct != null)
            {
                bw.Write(EncryptDataStruct(dataStruct));
            }
            dataStruct = BaseDataStruct;
            if (dataStruct != null)
            {
                bw.Write(EncryptDataStruct(dataStruct));
            }
            bw.Write(ConstantValue.FileFooter);
        }
        private DataStruct EncryptDataStruct(DataStruct dataStruct)
        {
            if (this.HasPassword)
            {
                dataStruct.Data = Feng.IO.DEncrypt.Encrypt(this.Password, dataStruct.Data);
            }
            return dataStruct;
        }
        public virtual void ReadIOVersion(Feng.Excel.IO.BinaryReader reader)
        {
            for (int i = 0; i < 300; i++)
            {
                if ((reader.BaseStream.Position + 4) >= reader.BaseStream.Length)
                {
                    break;
                }
                try
                {
                    DataStruct dataStruct = reader.ReadDataStruct();
                    if (dataStruct == null)
                        return;
                    if (this.HasPassword)
                    {
                        dataStruct.Data = Feng.IO.DEncrypt.Decrypt(this.Password, dataStruct.Data);
                    }
                    ReadIOVersionDataStruct(dataStruct);
                }
                catch (Exception ex)
                {
                    Feng.Utils.TraceHelper.WriteTrace("DataDesign", "DataExcel", "ReadIOVersion", ex);
                }
            }
        }

        private void ReadIOVersionDataStruct(DataStruct dataStruct)
        {
            switch (dataStruct.FullName)
            {
                case "DataValue":
                    ReadDataValueDataStruct(dataStruct);
                    break;
                case "DataFile":
                    ReadDataFileDataStruct(dataStruct);
                    break;
                case "Extend":
                    ReadExtendDataStruct(dataStruct);
                    break;
                case "Code":
                    ReadCodeDataStruct(dataStruct);
                    break;
                case "DataBase":
                    ReadDataBaseDataStruct(dataStruct);
                    break;
                case "Filter":
                    ReadDataFileFilterStruct(dataStruct);
                    break;
                case "Base":
                    ReadBaseStruct(dataStruct);
                    break;

                default:
                    break;
            }
        }
        public void ReadBaseStruct(DataStruct dataStruct)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(dataStruct.Data))
            {
                DataStruct dsbase = reader.ReadDataStruct();  
                base.ReadDataStruct(dsbase);
            }
        }
        public void ReadDataBaseDataStruct(DataStruct dataStruct)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(dataStruct.Data))
            {
                DataStruct data = reader.ReadDataStruct();
                this.CellDataBase.Read(data);
            }
        }
        public void ReadCodeDataStruct(DataStruct dataStruct)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(dataStruct.Data))
            {
                string code = reader.ReadString();
                this.Code = code;
            }
        }
        private void ReadExtendBackCellDataStruct(DataStruct dataStruct)
        {
            DataStruct datamergecells = dataStruct;
            this.BackCells = this.ClassFactory.CreateDefaultBackCells(this);
            this.BackCells.ReadDataStruct(datamergecells);
        }
        private void ReadExtendListExtendDataStruct(DataStruct dataStruct)
        {

        }
        private void ReadExtendDisplayAreaExtendDataStruct(DataStruct dataStruct)
        {
            using (Feng.Excel.IO.BinaryReader reader = new IO.BinaryReader(dataStruct.Data))
            {
                this.DisplayArea = new SelectCellCollection();
                int DisplayArea_BeginCell_Row_Index = reader.ReadInt32();
                int DisplayArea_BeginCell_Column_Index = reader.ReadInt32();
                int DisplayArea_EndCell_Row_Index = reader.ReadInt32();
                int DisplayArea_EndCell_Column_Index = reader.ReadInt32();
                this.DisplayArea.BeginCell = this[DisplayArea_BeginCell_Row_Index, DisplayArea_BeginCell_Column_Index];
                this.DisplayArea.EndCell = this[DisplayArea_EndCell_Row_Index, DisplayArea_EndCell_Column_Index];

            }
        }
        private void ReadExtendPrintAreaExtendDataStruct(DataStruct dataStruct)
        {
            using (Feng.Excel.IO.BinaryReader reader = new IO.BinaryReader(dataStruct.Data))
            {
                int printareabegincellrowindex = reader.ReadIndex(118, -1);
                int printareabegincellcolumnindex = reader.ReadIndex(118, -1);
                int printareaendcellrowindex = reader.ReadIndex(118, -1);
                int printareaendcellcolumnindex = reader.ReadIndex(118, -1);
                if ((printareabegincellrowindex == -1) || (printareabegincellcolumnindex == -1)
                    || (printareaendcellrowindex == -1) || (printareaendcellcolumnindex == -1))
                {

                }
                else
                {
                    SelectCellCollection sel = new SelectCellCollection();
                    sel.BeginCell = this[printareabegincellrowindex, printareabegincellcolumnindex];
                    sel.EndCell = this[printareaendcellrowindex, printareaendcellcolumnindex];
                    this.PrintArea = sel;
                }
            }
        }
        private void ReadExtendUserDefineExtendDataStruct(DataStruct dataStruct)
        {
            using (Feng.Excel.IO.BinaryReader reader = new IO.BinaryReader(dataStruct.Data))
            {
                int count = reader.ReadInt();
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        string key = reader.ReadString();
                        DataStruct data = reader.ReadDataStruct();
                        this.UserDefineExtensData.Add(key, data);
                    }
                }
            }
        }
        private void ReadExtendDataStruct(DataStruct dataStruct)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(dataStruct.Data))
            {
                for (int i = 0; i < 300; i++)
                {
                    if ((reader.BaseStream.Position + 4) >= reader.BaseStream.Length)
                    {
                        break;
                    }
                    DataStruct datafilestruct = reader.ReadDataStruct();
                    if (datafilestruct == null)
                        return;
                    switch (datafilestruct.FullName)
                    {
                        case "BackCell":
                            ReadExtendBackCellDataStruct(datafilestruct);
                            break;
                        case "ListExtend":
                            ReadExtendListExtendDataStruct(datafilestruct);
                            break;
                        case "DisplayArea":
                            ReadExtendDisplayAreaExtendDataStruct(datafilestruct);
                            break;
                        case "PrintArea":
                            ReadExtendPrintAreaExtendDataStruct(datafilestruct);
                            break;
                        case "UserDefine":
                            ReadExtendUserDefineExtendDataStruct(datafilestruct);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private void ReadDataFileGridDataStruct(DataStruct dataStruct)
        {
            this.ReadDataStruct(dataStruct);
        }
        private void ReadDataFileEditDataStruct(DataStruct dataStruct)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(dataStruct.Data))
            {
                DataStructCollection datastructs = reader.ReadDataStructs();
                foreach (DataStruct ds in datastructs)
                {
                    try
                    {
                        if (ds != null)
                        {
                            ICellEditControl ie = DataExcel.GetCellEdit(this, ds);
                            if (ie == null)
                            {
                                ie = this.Edits[ds.FullName];
                                if (ie != null)
                                {
                                    ie = ie.Clone(this);
                                }
                            }
                            if (ie != null)
                            {
                                ie.ReadDataStruct(ds);
                                this.CellSaveEdits.Add(ie);
                                this.CellEdits.Add(ie);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcelFile", "ReadEditControls", ex);
                    }
                }
            }
        }
        private void ReadDataFileColumnDataStruct(DataStruct dataStruct)
        {
            DataStruct datacolumns = dataStruct;
            this.Columns = this.ClassFactory.CreateDefaultColumns(this);
            this.Columns.ReadDataStruct(datacolumns);
        }
        private void ReadDataFileRowDataStruct(DataStruct dataStruct)
        {
            DataStruct datarows = dataStruct;
            this.Rows = this.ClassFactory.CreateDefaultRows(this);
            this.Rows.ReadDataStruct(datarows);
        }
        private void ReadDataFileMergeDataStruct(DataStruct dataStruct)
        {
            DataStruct datamergecells = dataStruct;
            this.MergeCells = this.ClassFactory.CreateDefaultMergeCells(this);
            this.MergeCells.ReadDataStruct(datamergecells);
        }
        private void ReadDataFileDataStruct(DataStruct dataStruct)
        {
            byte[] data = dataStruct.Data;
            data = Feng.IO.CompressHelper.GZipDecompress(data);
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data))
            {
                for (int i = 0; i < 300; i++)
                {
                    if ((reader.BaseStream.Position + 4) >= reader.BaseStream.Length)
                    {
                        break;
                    }
                    DataStruct datafilestruct = reader.ReadDataStruct();
                    if (datafilestruct == null)
                        return;
                    switch (datafilestruct.FullName)
                    {
                        case "Grid":
                            ReadDataFileGridDataStruct(datafilestruct);
                            break;
                        case "Edit":
                            ReadDataFileEditDataStruct(datafilestruct);
                            break;
                        case "Column":
                            ReadDataFileColumnDataStruct(datafilestruct);
                            break;
                        case "Row":
                            ReadDataFileRowDataStruct(datafilestruct);
                            break;
                        case "Merge":
                            ReadDataFileMergeDataStruct(datafilestruct);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        private void ReadDataValueDataStruct(DataStruct dataStruct)
        { 
        }
        private void ReadDataFileFilterStruct(DataStruct dataStruct)
        {
            if (this.FilterExcel == null)
            {
                this.FilterExcel = new Fillter.FilterExcel();
                this.FilterExcel.Grid = this;
                this.FilterExcel.ReadDataStruct(dataStruct);
            }
        }
        private Feng.Data.DataValueCollection ReadDataValues(Feng.Excel.IO.BinaryReader reader)
        {
            for (int i = 0; i < 30; i++)
            {
                DataStruct dataStruct = ReadDataStruct(reader, "DataValue");
                if (dataStruct != null)
                {
                    if (dataStruct.ReadValue)
                    {
                        Feng.Data.DataValueCollection datavalues = ReadDataValue(dataStruct.Data);
                        return datavalues;
                    }
                }
            } 
            return null;
        }
        private DataStruct ReadDataStruct(Feng.IO.BufferReader reader, string name)
        {
            DataStruct dataStruct = reader.ReadDataStruct(name);
            return dataStruct;
        }

 
        public virtual void ReadDataBase(Feng.Excel.IO.BinaryReader reader)
        {
            for (int i = 0; i < 30; i++)
            {
                DataStruct dataStruct = ReadDataStruct(reader, "DataBase");
                if (dataStruct != null)
                {
                    if (dataStruct.ReadValue)
                    {
                        ReadDataBaseDataStruct(dataStruct); 
                    }
                }
            } 
        }
    }
}
