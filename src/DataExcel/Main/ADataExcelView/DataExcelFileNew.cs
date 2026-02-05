//using Feng.Data;
//using Feng.Enums;
//using Feng.Excel.App;
//using Feng.Excel.Collections;
//using Feng.Excel.Generic;
//using Feng.Excel.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Windows.Forms;

//namespace Feng.Excel
//{
//    partial class DataExcel
//    {

//        public const int IOVersionNew = 2;
//        public virtual void SaveEditControls2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            List<ICellEditControl> list = GetCellEditList();
//            DataStructCollection datastructs = new DataStructCollection();
//            for (int i = 0; i < list.Count; i++)
//            {
//                ICellEditControl ie = list[i];
//                ie.AddressID = i + 1;
//                datastructs.Add(ie.Data);
//            }
//            bw.Write(datastructs);
//        }
//        public virtual Feng.Data.DataValueCollection GetCellDataValueCollection2()
//        {
//            Feng.Data.DataValueCollection datas = new DataValueCollection();
//            foreach (IRow row in this.Rows)
//            {
//                foreach (IColumn col in this.Columns)
//                {
//                    ICell cell = row[col];
//                    if (cell != null)
//                    {
//                        if (cell.Value != null)
//                        {
//                            byte type = TypeEnum.GetTypeEnum(cell.Value.GetType());
//                            if (type < TypeEnum.TMAXVALUE)
//                            {
//                                Feng.Data.DataValue data = new DataValue();
//                                data.Name = cell.ID;
//                                data.Value = cell.Value;
//                                data.Type = type;
//                                datas.Add(data);
//                            }
//                        }
//                    }
//                }
//            }
//            return datas;
//        }
//        public byte[] GetDataValueData2(Feng.Data.DataValueCollection data)
//        {
//            byte[] datas = new byte[0];
//            using (Feng.IO.BufferWriter stream = new Feng.IO.BufferWriter())
//            {
//                stream.Write(data.Count);
//                foreach (DataValue dv in data)
//                {
//                    stream.Write(dv);
//                }
//                datas = stream.GetData();
//            }
//            return datas;
//        }
//        public Feng.Data.DataValueCollection ReadDataValue2(byte[] datas)
//        {
//            Feng.Data.DataValueCollection data = new DataValueCollection();
//            using (Feng.IO.BufferReader stream = new Feng.IO.BufferReader(datas))
//            {
//                int count = stream.ReadInt();
//                for (int i = 0; i < count; i++)
//                {
//                    DataValue dv = stream.ReadDataValue();
//                    data.Add(dv);
//                }
//            }
//            return data;
//        }
//        public virtual void SaveDataValue2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            Feng.Data.DataValueCollection datavalues = GetCellDataValueCollection();
//            byte[] data = GetDataValueData(datavalues);
//            if (HasPassword)
//            {
//                data = Feng.IO.DEncrypt.Encrypt(data, this.Password);
//            }
//            bw.Write(DataExcelFileEnum.DataExcelFile_DATAVALUE);
//            bw.Write(data);
//        }
//        public virtual void SaveDataFile2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            using (Feng.Excel.IO.BinaryWriter stream = new IO.BinaryWriter())
//            {
//                stream.Write(this.Data);
//                SaveEditControls(stream);
//                stream.Write(this.Columns.Data);
//                stream.Write(this.Rows.Data);

//                if (this.MergeCells != null)
//                {
//                    stream.Write(ConstantValue.FileEndMergeCell);
//                    stream.Write(this.MergeCells.Data);
//                }

//                if (this.BackCells != null)
//                {
//                    stream.Write(ConstantValue.FileEndBackCell);
//                    stream.Write(this.BackCells.Data);
//                }
//                if (this.ListExtendCells != null)
//                {
//                    stream.Write(ConstantValue.ListExtendCells);
//                    stream.Write(this.ListExtendCells.Data);
//                }

//                if (this.DisplayArea != null)
//                {
//                    stream.Write(ConstantValue.FileDisplayArea);
//                    stream.Write(this.DisplayArea.BeginCell.Row.Index);
//                    stream.Write(this.DisplayArea.BeginCell.Column.Index);
//                    stream.Write(this.DisplayArea.EndCell.Row.Index);
//                    stream.Write(this.DisplayArea.EndCell.Column.Index);
//                }
//                SavePrintArea(stream);
//                SaveExtendData(stream);
//                stream.Write(ConstantValue.FileOver);
//                byte[] data = stream.GetData();
//                if (HasPassword)
//                {
//                    data = Feng.IO.DEncrypt.Encrypt(data, this.Password);
//                }
//                bw.Write(DataExcelFileEnum.DataExcelFile_All);
//                data = Feng.IO.CompressHelper.GZipCompress(data);
//                bw.Write(data);
//            }
//        }
//        public virtual void SaveUpdateInfo2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            byte[] data = this.UpdateInfo.Data;
//            if (HasPassword)
//            {
//                data = Feng.IO.DEncrypt.Encrypt(data, this.Password);
//            }
//            bw.Write(DataExcelFileEnum.DataExcelFile_UPDATEINFO);
//            bw.Write(data);
//        }
//        public virtual void SaveCode2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            byte[] data = Feng.IO.BitConver.GetBytes(this.Code);
//            if (HasPassword)
//            {
//                data = Feng.IO.DEncrypt.Encrypt(data, this.Password);
//            }
//            bw.Write(DataExcelFileEnum.DataExcelFile_CODE);
//            bw.Write(data);
//        }
//        public virtual void SaveEnd2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            bw.Write(DataExcelFileEnum.DataExcelFile_End);
//        }
//        public virtual void Save2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            bw.Write(ConstantValue.FileHeader);
//            bw.Write(IOVersionNew);
//            bw.Write(HasPassword);

//            long beginoposition = bw.BaseStream.Position;
//            SavePosition2(bw);
//            long updateinfoposition = bw.BaseStream.Position;
//            SaveUpdateInfo2(bw);
//            long datavalueposition = bw.BaseStream.Position;
//            SaveDataValue2(bw);
//            long datafileposition = bw.BaseStream.Position;
//            SaveDataFile2(bw);
//            long datacodeposition = bw.BaseStream.Position;
//            SaveCode2(bw);

//            SaveEnd(bw);
//            bw.Write(ConstantValue.FileFooter);
//            bw.Write(datavalueposition);
//        }
//        public virtual void SavePosition2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            bw.Write(DataExcelFileEnum.DataExcelFile_POSITION);
//            using (Feng.Excel.IO.BinaryWriter stream = new IO.BinaryWriter())
//            {
//                stream.Write((long)0);
//                stream.Write((long)0);
//                stream.Write((long)0);
//                stream.Write((long)0);
//                stream.Write((long)0);
//                stream.Write((long)0);
//                stream.Write((long)0);
//                stream.Write((long)0);
//                stream.Write((long)0);
//                bw.Write(stream.GetData());
//            }
//        }
 
//        public virtual void Save2(string file, string password)
//        {
//            this.Password = password;
//            file = System.IO.Path.GetFullPath(file);
//            string path = System.IO.Path.GetDirectoryName(file);
//            if (!System.IO.Directory.Exists(path))
//            {
//                System.IO.Directory.CreateDirectory(path);
//            }
//            bool handled = false;

//            this.OnBeforeSaveFile(handled, file);
//            if (handled)
//            {
//                return;
//            }
//            using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.OpenOrCreate))
//            {
//                Save2(fs);
//            }
//            this.OnSaveFile(file);
//        }

//        public virtual void Save2(System.IO.Stream stream)
//        {
//            using (Feng.Excel.IO.BinaryWriter bw = this.ClassFactory.CreateBinaryWriter(stream))
//            {
//                this.Save2(bw);
//            }
//        }

 

 
//        public Feng.IO.FileReadCacheCollection readDics2 = new Feng.IO.FileReadCacheCollection();
 

//        public virtual void OpenRows2(int version, Feng.Excel.IO.BinaryReader stream)
//        {

//            DataStruct datarows = stream.ReadDataStruct();
//            if (string.IsNullOrEmpty(datarows.DllName))
//            {
//                this.Rows = this.ClassFactory.CreateDefaultRows(this);
//            }
//            else
//            {
//                try
//                {
//                    string filename = DataExcel.GetFileName(datarows.DllName);
//                    if (!System.IO.File.Exists(filename))
//                    {
//                        using (System.Net.WebClient wc = new System.Net.WebClient())
//                        {
//                            wc.DownloadFile(datarows.AessemlyDownLoadUrl, filename);
//                        }
//                    }
//                    Assembly.LoadFile(filename);
//                    Type type = Type.GetType(datarows.FullName, false);
//                    if (type != null)
//                    {
//                        this.Rows = Activator.CreateInstance(type, new object[] { this }) as IRowCollection;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    this.OnException(ex);
//                }
//            }
//            if (this.Rows == null)
//            {
//                this.Rows = this.ClassFactory.CreateDefaultRows(this);
//            }

//            this.Rows.Read(this, version, datarows);
//        }

//        public virtual void OpenColumns2(int version, Feng.Excel.IO.BinaryReader stream)
//        {

//            DataStruct datacolumns = stream.ReadDataStruct();
//            if (string.IsNullOrEmpty(datacolumns.DllName))
//            {
//                this.Columns = this.ClassFactory.CreateDefaultColumns(this);
//            }
//            else
//            {
//                try
//                {
//                    string filename = DataExcel.GetFileName(datacolumns.DllName);
//                    if (!System.IO.File.Exists(filename))
//                    {
//                        using (System.Net.WebClient wc = new System.Net.WebClient())
//                        {
//                            wc.DownloadFile(datacolumns.AessemlyDownLoadUrl, filename);
//                        }
//                    }
//                    Assembly.LoadFile(filename);
//                    Type type = Type.GetType(datacolumns.FullName, false);
//                    if (type != null)
//                    {
//                        this.Rows = Activator.CreateInstance(type, new object[] { this }) as IRowCollection;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    this.OnException(ex);
//                }
//            }
//            if (this.Columns == null)
//            {
//                this.Columns = this.ClassFactory.CreateDefaultColumns(this);
//            }

//            this.Columns.Read(this, version, datacolumns);
//        }

//        public virtual void OpenMergeCells2(int version, Feng.Excel.IO.BinaryReader stream)
//        {
//            DataStruct datamergecells = stream.ReadDataStruct();
//            if (string.IsNullOrEmpty(datamergecells.DllName))
//            {
//                this.MergeCells = this.ClassFactory.CreateDefaultMergeCells(this);
//            }
//            else
//            {
//                try
//                {
//                    string filename = DataExcel.GetFileName(datamergecells.DllName);
//                    if (!System.IO.File.Exists(filename))
//                    {
//                        using (System.Net.WebClient wc = new System.Net.WebClient())
//                        {
//                            wc.DownloadFile(datamergecells.AessemlyDownLoadUrl, filename);
//                        }
//                    }
//                    Assembly.LoadFile(filename);
//                    Type type = Type.GetType(datamergecells.FullName, false);
//                    if (type != null)
//                    {
//                        this.MergeCells = Activator.CreateInstance(type, new object[] { this }) as IMergeCellCollection;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    this.OnException(ex);
//                }
//            }
//            if (this.MergeCells == null)
//            {
//                this.MergeCells = this.ClassFactory.CreateDefaultMergeCells(this);
//            }

//            this.MergeCells.Read(this, version, datamergecells);
//        }

//        public virtual void OpenBackCells2(DataExcel grid, int version, Feng.Excel.IO.BinaryReader stream)
//        {
//            DataStruct datamergecells = stream.ReadDataStruct();
//            if (string.IsNullOrEmpty(datamergecells.DllName))
//            {
//                this.BackCells = this.ClassFactory.CreateDefaultBackCells(this);
//            }
//            else
//            {
//                try
//                {
//                    string filename = DataExcel.GetFileName(datamergecells.DllName);
//                    if (!System.IO.File.Exists(filename))
//                    {
//                        using (System.Net.WebClient wc = new System.Net.WebClient())
//                        {
//                            wc.DownloadFile(datamergecells.AessemlyDownLoadUrl, filename);
//                        }
//                    }
//                    Assembly.LoadFile(filename);
//                    Type type = Type.GetType(datamergecells.FullName, false);
//                    if (type != null)
//                    {
//                        this.MergeCells = Activator.CreateInstance(type, new object[] { this }) as IMergeCellCollection;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    this.OnException(ex);
//                }
//            }
//            if (this.BackCells == null)
//            {
//                this.BackCells = this.ClassFactory.CreateDefaultBackCells(this);
//            }

//            this.BackCells.Read(grid, version, datamergecells);
//        }

//        public virtual void ReadUpdateInfo2(byte[] data, bool haspassword, string password)
//        {
//            if (haspassword)
//            {
//                data = Feng.IO.DEncrypt.Decrypt(data, password);
//            }
//            this.UpdateInfo.Read(data);
//        }

//        public virtual Feng.Data.DataValueCollection ReadDataValue2(byte[] data, bool haspassword, string password)
//        {
//            if (haspassword)
//            {
//                data = Feng.IO.DEncrypt.Decrypt(data, password);
//            }
//            Feng.Data.DataValueCollection datavalues = ReadDataValue(data);
//            return datavalues;
//        }

//        public virtual void ReadDataFile2(byte[] data, int version, bool haspassword, string password)
//        {
//            using (Feng.Excel.IO.BinaryReader br = new IO.BinaryReader(data))
//            {
//                DataStruct ds = br.ReadDataStruct();
//                this.Clear();
//                readDics.Clear();
//                Read(ds);
//                ReadEditControls(version, br);
//                OpenColumns(version, br);
//                OpenRows(version, br);
//                short filestate = br.ReadInt16();
//                while (filestate != ConstantValue.FileOver)
//                {
//                    DataStruct ds2 = null;
//                    switch (filestate)
//                    {
//                        case ConstantValue.FileEndMergeCell:
//                            OpenMergeCells(version, br);
//                            break;
//                        case ConstantValue.FileEndBackCell:
//                            OpenBackCells(this, version, br);
//                            break;
//                        case ConstantValue.FileDisplayArea:
//                            this.DisplayArea = new SelectCellCollection();
//                            this.DisplayArea.BeginCell = this[br.ReadInt32(), br.ReadInt32()];
//                            this.DisplayArea.EndCell = this[br.ReadInt32(), br.ReadInt32()];
//                            break;
//                        case ConstantValue.FileFooter:
//                            return;
//                        case ConstantValue.ListExtendCells:
//                            ds2 = br.ReadDataStruct();
//                            break;
//                        case ConstantValue.ExtendData:
//                            this.ReadExtendData(br);
//                            break;
//                        case ConstantValue.FilePrintArea:
//                            this.ReadPrintArea(br);
//                            break;
//                        default:
//                            DataStruct datamergecells = br.ReadDataStruct();
//                            break;
//                    }
//                    filestate = br.ReadInt16();
//                }
//            }
//        }
//        public virtual void Open2(byte[] data)
//        {
//            Open(data, string.Empty);
//        }
//        public virtual void OpenZipData2(byte[] data)
//        {
//            OpenZipData(data, string.Empty);
//        }
//        public virtual void OpenZipData2(byte[] data, string password)
//        {
//            data = Feng.IO.CompressHelper.GZipDecompress(data);
//            Open(data, password);
//        }
//        public virtual void Open2(byte[] data, string password)
//        {
//            if (data == null)
//                return;
//            using (Feng.Excel.IO.BinaryReader reader = new Feng.Excel.IO.BinaryReader(data))
//            {
//                this.Open(reader, password);
//            }
//        }
//        public virtual void Open2(Feng.Excel.IO.BinaryReader stream, string password)
//        {

//            try
//            {
//                this.Clear();
//                this.BeginSetFirstDisplayRowIndex();
//                this.BeginSetFirstDisplayColumnIndex();
//                ushort fileheader = stream.ReadUInt16();
//                if (fileheader != ConstantValue.FileHeader)
//                {
//                    throw new Exception("Not DataExcel File");
//                }

//                int version = stream.ReadInt();
//                bool haspassword = stream.ReadBoolean();

//                byte datafileenum = stream.ReadByte();
//                while (datafileenum != DataExcelFileEnum.DataExcelFile_End)
//                {
//                    byte[] data = stream.ReadBytes();
//                    switch (datafileenum)
//                    {
//                        case DataExcelFileEnum.DataExcelFile_All:
//                            data = Feng.IO.CompressHelper.GZipDecompress(data);
//                            ReadDataFile(data, version, haspassword, password);
//                            break;
//                        case DataExcelFileEnum.DataExcelFile_DATAVALUE:
//                            ReadDataValue(data, haspassword, password);
//                            break;
//                        case DataExcelFileEnum.DataExcelFile_UPDATEINFO:
//                            ReadUpdateInfo(data, haspassword, password);
//                            break;
//                        case DataExcelFileEnum.DataExcelFile_CODE:
//                            ReadCode(data, haspassword, password);
//                            break;
//                        default:
//                            break;
//                    }
//                    datafileenum = stream.ReadByte();
//                }
//                short filefooter = stream.ReadInt16();

//                if (this.AutoExecuteExpress)
//                {
//                    ExecuteExpress();
//                }
//                //this.HScroller.Visible = this.ShowHorizontalScroller;
//                //this.VScroller.Visible = this.ShowVerticalScroller;
//                OnLoadCompleted(); 
//            }
//            catch (Exception ex)
//            {
//                this.Clear();
//                this.Init();
//                Feng.IO.LogHelper.Log(ex);
//            }
//            finally
//            {
//                this.EndSetFirstDisplayRowIndex();
//                this.EndSetFirstDisplayColumnIndex();
//            }
//        }

//        private void ReadCode2(byte[] data, bool haspassword, string password)
//        {
//            if (haspassword)
//            {
//                data = Feng.IO.DEncrypt.Decrypt(data, password);
//            }
//            this.Code = Feng.IO.BitConver.GetString(data);
//        }
 
//        public void ExecuteExpress2()
//        {
//            foreach (ICell cell in this.ExpressionCells)
//            {
//                cell.Expression = cell.Expression;
//            }
//        }


//        public virtual void Open2(string file, string password)
//        {
//            if (!System.IO.File.Exists(file))
//            {
//                return;
//            }
//            bool handled = false;
//            this.OnBeforeOpenFile(handled, file);
//            if (handled)
//            {
//                return;
//            }
//            string filetype = System.IO.Path.GetExtension(file);
//            filetype = filetype.ToLower();
//            if (filetype == Feng.App.FileExtension_DataExcel.DataExcelDataZip)
//            {
//                byte[] data = System.IO.File.ReadAllBytes(file);
//                data = Feng.IO.CompressHelper.GZipDecompress(data);
//                Open(data, password);
//            }
//            else
//            {
//                using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Open))
//                {
//                    Open(fs, password);
//                }
//            }
//            //readDics.Add("focusedcellrowindex", focusedrowindex);
//            //readDics.Add("focusedcellcolumnindex", focusedcolumnindex);
//            int focusedrowindex = Feng.Utils.ConvertHelper.ToInt32(this.readDics.Get("focusedcellrowindex"), -1);
//            int focusedcolumnindex = Feng.Utils.ConvertHelper.ToInt32(this.readDics.Get("focusedcellcolumnindex"), -1);
//            if (focusedrowindex > 0 && focusedcolumnindex > 0)
//            {
//                ICell cell = this.GetCell(focusedrowindex, focusedcolumnindex);
//                this.FocusedCell = cell;
//            }
//            this.OnOpenFile(file);
//        }

//        public void Open2(System.IO.Stream stream)
//        {
//            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(stream))
//            {
//                this.Open(bw, string.Empty);
//            }
//        }

//        public void Open2(System.IO.Stream stream, string password)
//        {
//            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(stream))
//            {
//                this.Open(bw, password);
//            }
//        }

//        public virtual void Open2(string file)
//        {
//            this.Open(file, this.Password);
//            this.FileName = file;
//        }
//        /// <summary>
//        /// ==Open(file)
//        /// </summary>
//        /// <param name="file"></param>
//        public virtual void Load2(string file)
//        {
//            Open(file);
//        }

//        public virtual string Open2()
//        {
//            using (OpenFileDialog dlg = new OpenFileDialog())
//            {
//                dlg.Filter = Feng.App.FileExtension_DataExcel.SelDataExcelFileAndAll;
//                if (dlg.ShowDialog() == DialogResult.OK)
//                {
//                    this._filename = dlg.FileName;
//                    this.Open(this._filename);
//                    return dlg.FileName;
//                }
//            }
//            return string.Empty;
//        }


//        #region SavePrintArea 成员

//        public virtual void SavePrintArea2(Feng.Excel.IO.BinaryWriter bw)
//        {
//            if (this.PrintArea != null)
//            {
//                bw.Write(ConstantValue.FilePrintArea);
//                bw.Write(118, this.PrintArea.BeginCell.Row.Index);
//                bw.Write(118, this.PrintArea.BeginCell.Column.Index);
//                bw.Write(118, this.PrintArea.EndCell.Row.Index);
//                bw.Write(118, this.PrintArea.EndCell.Column.Index);
//            }

//        }

//        public virtual void ReadPrintArea2(Feng.Excel.IO.BinaryReader stream)
//        {
//            int printareabegincellrowindex = stream.ReadIndex(118, -1);
//            int printareabegincellcolumnindex = stream.ReadIndex(118, -1);
//            int printareaendcellrowindex = stream.ReadIndex(118, -1);
//            int printareaendcellcolumnindex = stream.ReadIndex(118, -1);
//            if ((printareabegincellrowindex == -1) || (printareabegincellcolumnindex == -1)
//                || (printareaendcellrowindex == -1) || (printareaendcellcolumnindex == -1))
//            {

//            }
//            else
//            {
//                SelectCellCollection sel = new SelectCellCollection();
//                sel.BeginCell = this[printareabegincellrowindex, printareabegincellcolumnindex];
//                sel.EndCell = this[printareaendcellrowindex, printareaendcellcolumnindex];
//                this.PrintArea = sel;
//            }
//        }

//        #endregion

//        #region DataValues
//        public virtual DataValueCollection GetValusFormFile2(string file)
//        {
//            return GetValusFormFile(file, string.Empty);
//        }
//        public virtual DataValueCollection GetValusFormFile2(string file, string password)
//        {
//            DataValueCollection dvs = null;
//            using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Open))
//            {
//                using (Feng.Excel.IO.BinaryReader stream = new IO.BinaryReader(fs))
//                {
//                    dvs = GetValusFormFile(stream, password);
//                }
//                fs.Close();
//            }
//            return dvs;
//        }
//        public virtual DataValueCollection GetValusFormFile2(Feng.Excel.IO.BinaryReader stream, string password)
//        {
//            try
//            {
//                ushort fileheader = stream.ReadUInt16();
//                if (fileheader != ConstantValue.FileHeader)
//                {
//                    throw new Exception("Not DataExcel File");
//                }

//                int version = stream.ReadInt();
//                bool haspassword = stream.ReadBoolean();

//                byte datafileenum = stream.ReadByte();
//                while (datafileenum != DataExcelFileEnum.DataExcelFile_End)
//                {
//                    byte[] data = stream.ReadBytes();
//                    switch (datafileenum)
//                    {
//                        case DataExcelFileEnum.DataExcelFile_All:
//                            break;
//                        case DataExcelFileEnum.DataExcelFile_DATAVALUE:
//                            return ReadDataValue(data, haspassword, password);
//                        case DataExcelFileEnum.DataExcelFile_UPDATEINFO:
//                            break;
//                        default:
//                            break;
//                    }
//                    datafileenum = stream.ReadByte();
//                }
//                short filefooter = stream.ReadInt16();
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.BugReport.Log(ex);
//                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
//            }
//            return null;
//        }
//        #endregion

//    }
//}
