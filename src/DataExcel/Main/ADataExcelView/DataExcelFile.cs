using Feng.Data;
using Feng.Enums;
using Feng.Excel.App;
using Feng.Excel.Collections;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Forms.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        public static void ShowFile(string file)
        {
            System.Windows.Forms.Form form = new Form();
            form.Width = 960;
            form.Height = 800;
            form.StartPosition = FormStartPosition.CenterScreen;
            DataExcelControl grid = new DataExcelControl();
            grid.EditView.Open(file);
            grid.Dock = DockStyle.Fill;
            form.Controls.Add(grid);
            form.ShowDialog();
        }

        public const int IOVersion = 1;
        public const int IOVersion_DataStruct = 2;
        public virtual void SaveEditControls(Feng.Excel.IO.BinaryWriter bw)
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
        }
        public virtual void Export()
        {
            if (this._filename != string.Empty)
            {
                this.Save(this._filename);
                return;
            }
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = Feng.App.FileExtension_DataExcel.SelDataExcelFile;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.Save(dlg.FileName);
                }
            }
        }

        public virtual Feng.Data.DataValueCollection GetCellDataValueCollection()
        {
            Feng.Data.DataValueCollection datas = new DataValueCollection();
            Feng.Data.DataValue data = null;
            foreach (IRow row in this.Rows)
            {
                foreach (IColumn col in this.Columns)
                {
                    ICell cell = row[col];
                    if (cell != null)
                    {
                        if (cell.Value != null)
                        {
                            byte type = TypeEnum.GetTypeEnum(cell.Value.GetType());
                            if (type < TypeEnum.TMAXVALUE)
                            {
                                if (!string.IsNullOrWhiteSpace(cell.ID))
                                {
                                    data = new DataValue();
                                    data.Name = cell.ID;
                                    data.Value = cell.Value;
                                    data.Type = type;
                                    datas.Add(data);
                                }

                                data = new DataValue();
                                data.Name = cell.Name;
                                data.Value = cell.Value;
                                data.Type = type;
                                datas.Add(data);

                            }
                        }
                    }
                }
            }
            data = new DataValue();
            data.Name = "FCreateTime";
            data.Value = this.CreateTime;
            data.Type = TypeEnum.GetTypeEnum(this.CreateTime.GetType());
            datas.Add(data);

            data = new DataValue();
            data.Name = "FCreateUser";
            data.Value = this.CreateUser;
            data.Type = TypeEnum.Tstring;
            datas.Add(data);

            data = new DataValue();
            data.Name = "FUpdateTime";
            data.Value = this.UpdateTime;
            data.Type = TypeEnum.GetTypeEnum(this.UpdateTime.GetType());
            datas.Add(data);

            data = new DataValue();
            data.Name = "FUpdateUser";
            data.Value = this.UpdateUser;
            data.Type = TypeEnum.Tstring;
            datas.Add(data);

            data = new DataValue();
            data.Name = "FUpdateUserName";
            data.Value = this.UpdateUserName;
            data.Type = TypeEnum.Tstring;
            datas.Add(data);

            data = new DataValue();
            data.Name = "FUpdateTimes";
            data.Value = this.UpdateTimes;
            data.Type = TypeEnum.GetTypeEnum(this.UpdateTimes.GetType());
            datas.Add(data);
            return datas;
        }
        public byte[] GetDataValueData(Feng.Data.DataValueCollection data)
        {
            byte[] datas = null;
            using (Feng.IO.BufferWriter stream = new Feng.IO.BufferWriter())
            {
                stream.Write(data.Count);
                foreach (DataValue dv in data)
                {
                    stream.Write(dv);
                }
                datas = stream.GetData();
            }
            return datas;
        }
        public Feng.Data.DataValueCollection ReadDataValue(byte[] datas)
        {
            Feng.Data.DataValueCollection data = new DataValueCollection();
            using (Feng.IO.BufferReader stream = new Feng.IO.BufferReader(datas))
            {
                int count = stream.ReadInt();
                for (int i = 0; i < count; i++)
                {
                    DataValue dv = stream.ReadDataValue();
                    data.Add(dv);
                }
            }
            return data;
        }
        public virtual void SaveDataValue(Feng.Excel.IO.BinaryWriter bw)
        {
            Feng.Data.DataValueCollection datavalues = GetCellDataValueCollection();
            byte[] data = GetDataValueData(datavalues);
            if (HasPassword)
            {
                data = Feng.IO.DEncrypt.Encrypt(data, this.Password);
            }
            bw.Write(DataExcelFileEnum.DataExcelFile_DATAVALUE);
            bw.Write(data);
        }
        public virtual void SaveDataFile(Feng.Excel.IO.BinaryWriter bw)
        {
            using (Feng.Excel.IO.BinaryWriter stream = new IO.BinaryWriter())
            {
                stream.Write(this.Data);
                SaveEditControls(stream);
                stream.Write(this.Columns.Data);
                stream.Write(this.Rows.Data);

                if (this.MergeCells != null)
                {
                    stream.Write(ConstantValue.FileEndMergeCell);
                    stream.Write(this.MergeCells.Data);
                }

                if (this.BackCells != null)
                {
                    stream.Write(ConstantValue.FileEndBackCell);
                    stream.Write(this.BackCells.Data);
                }
                if (this.ListExtendCells != null)
                {
                    stream.Write(ConstantValue.ListExtendCells);
                    stream.Write(this.ListExtendCells.Data);
                }

                if (this.DisplayArea != null)
                {
                    stream.Write(ConstantValue.FileDisplayArea);
                    stream.Write(this.DisplayArea.BeginCell.Row.Index);
                    stream.Write(this.DisplayArea.BeginCell.Column.Index);
                    stream.Write(this.DisplayArea.EndCell.Row.Index);
                    stream.Write(this.DisplayArea.EndCell.Column.Index);
                }
                SavePrintArea(stream);
                SaveExtendData(stream);
                stream.Write(ConstantValue.FileOver);
                byte[] data = stream.GetData();
                if (HasPassword)
                {
                    data = Feng.IO.DEncrypt.Encrypt(data, this.Password);
                }
                bw.Write(DataExcelFileEnum.DataExcelFile_All);
                data = Feng.IO.CompressHelper.GZipCompress(data);
                bw.Write(data);
            }
        }
        public virtual void SaveUpdateInfo(Feng.Excel.IO.BinaryWriter bw)
        {
            byte[] data = this.UpdateInfo.Data;
            if (HasPassword)
            {
                data = Feng.IO.DEncrypt.Encrypt(data, this.Password);
            }
            bw.Write(DataExcelFileEnum.DataExcelFile_UPDATEINFO);
            bw.Write(data);
        }
        public virtual void SaveCode(Feng.Excel.IO.BinaryWriter bw)
        {
            byte[] data = Feng.IO.BitConver.GetBytes(this.Code);
            if (HasPassword)
            {
                data = Feng.IO.DEncrypt.Encrypt(data, this.Password);
            }
            bw.Write(DataExcelFileEnum.DataExcelFile_CODE);
            bw.Write(data);
        }
        public virtual void SaveEnd(Feng.Excel.IO.BinaryWriter bw)
        {
            bw.Write(DataExcelFileEnum.DataExcelFile_End);
        }
        public bool HasPassword
        {
            get
            {
                bool haspassword = false;
                if (!string.IsNullOrEmpty(this.Password))
                {
                    haspassword = true;
                }
                return haspassword;
            }
        }
        public virtual void Save(Feng.Excel.IO.BinaryWriter bw)
        {
            SaveIOVersion(bw);
            return;
            bw.Write(ConstantValue.FileHeader);
            bw.Write(IOVersion);
            bw.Write(HasPassword);

            long beginoposition = bw.BaseStream.Position;
            SavePosition(bw);
            long updateinfoposition = bw.BaseStream.Position;
            SaveUpdateInfo(bw);
            long datavalueposition = bw.BaseStream.Position;
            SaveDataValue(bw);
            long datafileposition = bw.BaseStream.Position;
            SaveDataFile(bw);
            long datacodeposition = bw.BaseStream.Position;
            SaveCode(bw);

            SaveEnd(bw);
            bw.Write(ConstantValue.FileFooter);
            bw.Write(datavalueposition);
        }

        public virtual void SavePosition(Feng.Excel.IO.BinaryWriter bw)
        {
            bw.Write(DataExcelFileEnum.DataExcelFile_POSITION);
            using (Feng.Excel.IO.BinaryWriter stream = new IO.BinaryWriter())
            {
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                bw.Write(stream.GetData());
            }
        }
        public virtual void SavePosition(Feng.Excel.IO.BinaryWriter bw
            , long beginoposition
            , long updateinfoposition
            , long datavalueposition
            , long datafileposition
            )
        {
            bw.Write(DataExcelFileEnum.DataExcelFile_POSITION);
            using (Feng.Excel.IO.BinaryWriter stream = new IO.BinaryWriter())
            {
                stream.Write(beginoposition);
                stream.Write(updateinfoposition);
                stream.Write(datavalueposition);
                stream.Write(datafileposition);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                stream.Write((long)0);
                bw.Write(stream.GetData());
            }
        }

        public virtual void SaveExtendData(Feng.Excel.IO.BinaryWriter stream)
        {
            if (this.UserDefineExtensData.Count > 0)
            {
                stream.Write(ConstantValue.ExtendData);
                stream.WriteInt(this.UserDefineExtensData.Count);
                foreach (System.Collections.Generic.KeyValuePair<string, DataStruct> key in this.UserDefineExtensData)
                {
                    stream.Write(key.Key);
                    stream.Write(key.Value);
                }
            }
        }

        public virtual void Save(string file)
        {
            this.Save(file, string.Empty);
        }


        public virtual void Save(string filename, string password)
        {
            this.Password = password;
            string file = System.IO.Path.GetFullPath(filename);
            string path = System.IO.Path.GetDirectoryName(file);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            bool handled = false;

            this.OnBeforeSaveFile(handled, file);
            if (handled)
            {
                return;
            }
            using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Create))
            {
                Save(fs);
            }
            this.OnSaveFile(file);
        }

        public virtual void Save(System.IO.Stream stream)
        {
            using (Feng.Excel.IO.BinaryWriter bw = this.ClassFactory.CreateBinaryWriter(stream))
            {
                this.Save(bw);
            }
        }

        public virtual byte[] GetFileData()
        {
            byte[] data = null;
            using (Feng.Excel.IO.BinaryWriter bw = new IO.BinaryWriter())
            {
                this.Save(bw);
                data = bw.GetData();
            }
            return data;
        }

        public virtual void Save()
        {
            if (this.FileName != string.Empty)
            {
                this.Save(this.FileName);
                return;
            }
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = Feng.App.FileExtension_DataExcel.SelDataExcelFile;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this._filename = dlg.FileName;
                    this.Save(this._filename);

                }
            }
        }

        public virtual void SaveAs()
        {
            if (this.AllowSaveAs)
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = Feng.App.FileExtension_DataExcel.SelDataExcelFile;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this._filename = dlg.FileName;
                        this.Save(this._filename);

                    }
                }
            }
        }

        public virtual void ReadEditControls(int version, Feng.Excel.IO.BinaryReader reader)
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
                            ie.Read(this, version, ds);
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

        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader stream = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                Read(stream);
            }
        }

        public virtual void ReadExtendData(Feng.Excel.IO.BinaryReader stream)
        {
            int count = stream.ReadInt();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string key = stream.ReadString();
                    DataStruct data = stream.ReadDataStruct();
                    this.UserDefineExtensData.Add(key, data);
                }
            }
        }
        public Feng.IO.FileReadCacheCollection readDics = new Feng.IO.FileReadCacheCollection();
        public virtual void Read(Feng.Excel.IO.BinaryReader stream)
        {
            stream.ReadCache();
            string strproduct = stream.ReadIndex(0, string.Empty);
            if (strproduct != Product.AssemblyProduct)
            {
                //MessageBox.Show(strproduct);
            }
            _AddSelect = stream.ReadIndex(1, _AddSelect);

            this._AutoGenerateRows = stream.ReadIndex(2, this._AutoGenerateRows);

            this._AutoGenerateColumns = stream.ReadIndex(3, this._AutoGenerateColumns);

            _AutoExecuteExpress = stream.ReadIndex(4, _AutoExecuteExpress);

            _backcolor = stream.ReadIndex(5, _backcolor);

            //_CellMouseState = (CellMouseState)stream.ReadIndex(7, (int)_CellMouseState);

            _columnautowidth = stream.ReadIndex(8, _columnautowidth);


            _DefaultColumnWidth = stream.ReadIndex(21, _DefaultColumnWidth);

            _DefaultRowHeight = stream.ReadIndex(22, _DefaultRowHeight);

            _EndReFresh = stream.ReadIndex(25, _EndReFresh);

            _figures = stream.ReadIndex(27, _figures);

            stream.ReadIndex(28, _filename);


            _FirstDisplayedColumnIndex = stream.ReadIndex(30, _FirstDisplayedColumnIndex);

            _FirstDisplayedRowIndex = stream.ReadIndex(31, _FirstDisplayedRowIndex);


            _FontSize = stream.ReadIndex(33, _FontSize);

            _GridLineColor = stream.ReadIndex(38, _GridLineColor);

            _showhorizontalruler = stream.ReadIndex(42, _showhorizontalruler);

            _LineColor = stream.ReadIndex(50, _LineColor);

            _maxColumn = stream.ReadIndex(53, _maxColumn);

            _maxRow = stream.ReadIndex(54, _maxRow);

            stream.ReadIndex(60, false);

            _MouseWheelChangedValue = stream.ReadIndex(64, _MouseWheelChangedValue);

            _PaddingLeft = stream.ReadIndex(65, _PaddingLeft);

            _PaddingTop = stream.ReadIndex(66, _PaddingTop);

            _pageindex = stream.ReadIndex(67, _pageindex);


            _password = stream.ReadIndex(69, _password);

            _printindex = stream.ReadIndex(74, _printindex);

            _rowautoheight = stream.ReadIndex(76, _rowautoheight);

            _rowautosize = stream.ReadIndex(77, _rowautosize);

            _RowBackColor = stream.ReadIndex(78, _RowBackColor);

            _SelectBorderColor = stream.ReadIndex(84, _SelectBorderColor);

            _SelectBorderWidth = stream.ReadIndex(85, _SelectBorderWidth);
            this._showrowheader = stream.ReadIndex(86, this._showrowheader);
            this._showcolumnheader = stream.ReadIndex(87, this._showcolumnheader);

            _showgridcolumnline = stream.ReadIndex(90, _showgridcolumnline);

            _showgridrowline = stream.ReadIndex(91, _showgridrowline);

            _SrollStep = stream.ReadIndex(92, _SrollStep);

            _showverticalruler = stream.ReadIndex(97, _showverticalruler);

            _showselectaddrect = stream.ReadIndex(99, _showselectaddrect);

            Cursor cursor = stream.ReadIndex(100, this.DefaultCursor);

            this._showhorizontalscroller = stream.ReadIndex(101, this._showhorizontalscroller);

            this._showverticalscroller = stream.ReadIndex(102, this._showverticalscroller);

            this._Allowchangedfirstdisplayrow = stream.ReadIndex(105, this._Allowchangedfirstdisplayrow);

            this._Allowchangedfirstdisplaycolumn = stream.ReadIndex(106, this._Allowchangedfirstdisplaycolumn);

            this._readonly = stream.ReadIndex(107, this._readonly);

            this._datamember = stream.ReadIndex(108, this._datamember);

            //this._DataSource = stream.ReadSerializeValueIndex(109, this._DataSource);

            this._canundoredo = stream.ReadIndex(110, this._canundoredo);

            this._BorderColor = stream.ReadIndex(111, this._BorderColor);

            this._PrintMargins = stream.ReadIndex(112, this._PrintMargins);

            this._PrintLandScope = stream.ReadIndex(113, this.PrintLandScope);
            this._printdocumentname = stream.ReadIndex(114, this.PrintDocumentName);
            this._papername = stream.ReadIndex(115, this.PaperName);
            this._paperheight = stream.ReadIndex(116, this.PaperHeight);
            this._paperwidth = stream.ReadIndex(117, this.PaperWidth);

            stream.ReadIndex(119, false);
            this._multiple = stream.ReadIndex(120, this.MultiSelect);
            byte[] dataBackGroundImage = stream.ReadIndex(121, new byte[] { });
            this.__BackgroundImageLayout = (ImageLayout)stream.ReadIndex(122, (int)this.BackgroundImageLayout);
            this.BackgroundImageLayout = this.__BackgroundImageLayout;
            if (dataBackGroundImage != null)
            {
                if (dataBackGroundImage.Length > 0)
                {
                    this.__BackgroundImage = Feng.Drawing.ImageHelper.GetImageByCache(dataBackGroundImage);
                    this.BackgroundImage = this.__BackgroundImage;
                }
            }
            this._assemblyfileversion = stream.ReadIndex(123, string.Empty);

            stream.ReadIndex(124, string.Empty);
            stream.ReadIndex(125, string.Empty);
            stream.ReadIndex(126, this._PropertyFormClosing);
            stream.ReadIndex(127, string.Empty);
            this._fileid = stream.ReadIndex(128, this._fileid);
            this._autoshowscroller = stream.ReadIndex(129, this._autoshowscroller);
            stream.ReadIndex(130, string.Empty);
            this._ShowSelectBorder = stream.ReadIndex(131, this._ShowSelectBorder);


            this._FirstDisplayedRowIndex = stream.ReadIndex(132, this._FirstDisplayedRowIndex);
            this._FirstDisplayedColumnIndex = stream.ReadIndex(133, this._FirstDisplayedColumnIndex);

            int focusedrowindex = 0;
            int focusedcolumnindex = 0;
            focusedrowindex = stream.ReadIndex(134, focusedrowindex);
            focusedcolumnindex = stream.ReadIndex(135, focusedcolumnindex);


            this._showfocusedcellborder = stream.ReadIndex(136, _showfocusedcellborder);
            this._showfocusedcellbordercolor = stream.ReadIndex(137, _showfocusedcellbordercolor);
            int _maxHasValueRow = stream.ReadIndex(138, 0);
            int _maxHasValueColumn = stream.ReadIndex(139, 0);

            this._formborderstyle = (System.Windows.Forms.FormBorderStyle)stream.ReadIndex(140, (int)this._formborderstyle);
            this._showborder = stream.ReadIndex(141, this._showborder);
            this._createtime = stream.ReadIndex(142, this._createtime);
            this._createuser = stream.ReadIndex(143, this._createuser);
            this._createusername = stream.ReadIndex(144, this._createusername);
            this._updatetime = stream.ReadIndex(145, this._updatetime);
            this._updatetimes = stream.ReadIndex(146, this._updatetimes);
            this._updateuser = stream.ReadIndex(147, this._updateuser);
            this._updateusername = stream.ReadIndex(148, this._updateusername);
            this._url = stream.ReadIndex(149, this._url);
            this._contentwidth = stream.ReadIndex(150, this._contentwidth);
            this._contentheight = stream.ReadIndex(151, this._contentheight);

            _PropertyClick = stream.ReadIndex(152, _PropertyClick);
            _PropertyDoubleClick = stream.ReadIndex(153, _PropertyDoubleClick);
            _PropertyValueChanged = stream.ReadIndex(154, _PropertyValueChanged);
            this._PropertyDataLoadCompleted = stream.ReadIndex(155, this._PropertyDataLoadCompleted);
            _PropertyKeyDown = stream.ReadIndex(156, _PropertyKeyDown);
            _PropertyKeyUp = stream.ReadIndex(157, _PropertyKeyUp);

            _PropertyEdit = stream.ReadIndex(158, _PropertyEdit);
            this._PropertyFormClosing = stream.ReadIndex(159, this._PropertyFormClosing);
            this._PropertyNew = stream.ReadIndex(160, this._PropertyNew);
            stream.ReadIndex(161, string.Empty);
            this._PropertyEndEdit = stream.ReadIndex(162, this._PropertyEndEdit);
            this._AllowInputExpress = (YesNoInhert)stream.ReadIndex(163, (int)this._AllowInputExpress);
            this._AllowSaveAs = stream.ReadIndex(164, this._AllowSaveAs);

            byte[] buf = new byte[0];
            buf = stream.ReadIndex(165, buf);
            _frozenrow = stream.ReadIndex(166, _frozenrow);
            _Frozencolumn = stream.ReadIndex(167, _Frozencolumn);
            this._lastsavefileguid = stream.ReadIndex(168, this._lastsavefileguid);
            this._lastsavefiledatetime = stream.ReadIndex(169, this._lastsavefiledatetime);
            this._lastsavefileindex = stream.ReadIndex(170, this._lastsavefileindex);
            this._opencount = stream.ReadIndex(171, this._opencount);
            byte[] enterAccountData = new byte[] { };
            this._enterAccount = null;
            enterAccountData = stream.ReadIndex(172, enterAccountData);
            if (enterAccountData.Length > 0)
            {
                this._enterAccount = new EnterAccount();
                this._enterAccount.ReadData(enterAccountData);
            }
            if (buf.Length > 0)
            {
                using (Feng.IO.BufferReader bwfun = new Feng.IO.BufferReader(buf))
                {
                    int count = bwfun.Read();
                    this.FunctionList.Clear();
                    for (int i = 0; i < count; i++)
                    {
                        string keytext = bwfun.ReadString();
                        string valuetext = bwfun.ReadString();
                        this.FunctionList.Add(keytext, valuetext);
                    }
                }
            }


            readDics.Add("focusedcellrowindex", focusedrowindex);
            readDics.Add("focusedcellcolumnindex", focusedcolumnindex);
        }

        public virtual void OpenRows(int version, Feng.Excel.IO.BinaryReader stream)
        {

            DataStruct datarows = stream.ReadDataStruct();
            if (string.IsNullOrEmpty(datarows.DllName))
            {
                this.Rows = this.ClassFactory.CreateDefaultRows(this);
            }
            else
            {
                try
                {
                    string filename = DataExcel.GetFileName(datarows.DllName);
                    if (!System.IO.File.Exists(filename))
                    {
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            wc.DownloadFile(datarows.AessemlyDownLoadUrl, filename);
                        }
                    }
                    Assembly.LoadFrom(filename);
                    Type type = Type.GetType(datarows.FullName, false);
                    if (type != null)
                    {
                        this.Rows = Activator.CreateInstance(type, new object[] { this }) as IRowCollection;
                    }
                }
                catch (Exception ex)
                {
                    this.OnException(ex);
                }
            }
            if (this.Rows == null)
            {
                this.Rows = this.ClassFactory.CreateDefaultRows(this);
            }

            this.Rows.Read(this, version, datarows);
        }

        public virtual void OpenColumns(int version, Feng.Excel.IO.BinaryReader stream)
        {

            DataStruct datacolumns = stream.ReadDataStruct();
            if (string.IsNullOrEmpty(datacolumns.DllName))
            {
                this.Columns = this.ClassFactory.CreateDefaultColumns(this);
            }
            else
            {
                try
                {
                    string filename = DataExcel.GetFileName(datacolumns.DllName);
                    if (!System.IO.File.Exists(filename))
                    {
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            wc.DownloadFile(datacolumns.AessemlyDownLoadUrl, filename);
                        }
                    }
                    Assembly.LoadFrom(filename);
                    Type type = Type.GetType(datacolumns.FullName, false);
                    if (type != null)
                    {
                        this.Rows = Activator.CreateInstance(type, new object[] { this }) as IRowCollection;
                    }
                }
                catch (Exception ex)
                {
                    this.OnException(ex);
                }
            }
            if (this.Columns == null)
            {
                this.Columns = this.ClassFactory.CreateDefaultColumns(this);
            }

            this.Columns.Read(this, version, datacolumns);
        }

        public virtual void OpenMergeCells(int version, Feng.Excel.IO.BinaryReader stream)
        {
            DataStruct datamergecells = stream.ReadDataStruct();
            if (string.IsNullOrEmpty(datamergecells.DllName))
            {
                this.MergeCells = this.ClassFactory.CreateDefaultMergeCells(this);
            }
            else
            {
                try
                {
                    string filename = DataExcel.GetFileName(datamergecells.DllName);
                    if (!System.IO.File.Exists(filename))
                    {
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            wc.DownloadFile(datamergecells.AessemlyDownLoadUrl, filename);
                        }
                    }
                    Assembly.LoadFrom(filename);
                    Type type = Type.GetType(datamergecells.FullName, false);
                    if (type != null)
                    {
                        this.MergeCells = Activator.CreateInstance(type, new object[] { this }) as IMergeCellCollection;
                    }
                }
                catch (Exception ex)
                {
                    this.OnException(ex);
                }
            }
            if (this.MergeCells == null)
            {
                this.MergeCells = this.ClassFactory.CreateDefaultMergeCells(this);
            }

            this.MergeCells.Read(this, version, datamergecells);
        }

        public virtual void OpenBackCells(DataExcel grid, int version, Feng.Excel.IO.BinaryReader stream)
        {
            DataStruct datamergecells = stream.ReadDataStruct();
            if (string.IsNullOrEmpty(datamergecells.DllName))
            {
                this.BackCells = this.ClassFactory.CreateDefaultBackCells(this);
            }
            else
            {
                try
                {
                    string filename = DataExcel.GetFileName(datamergecells.DllName);
                    if (!System.IO.File.Exists(filename))
                    {
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            wc.DownloadFile(datamergecells.AessemlyDownLoadUrl, filename);
                        }
                    }
                    Assembly.LoadFrom(filename);
                    Type type = Type.GetType(datamergecells.FullName, false);
                    if (type != null)
                    {
                        this.MergeCells = Activator.CreateInstance(type, new object[] { this }) as IMergeCellCollection;
                    }
                }
                catch (Exception ex)
                {
                    this.OnException(ex);
                }
            }
            if (this.BackCells == null)
            {
                this.BackCells = this.ClassFactory.CreateDefaultBackCells(this);
            }

            this.BackCells.Read(grid, version, datamergecells);
        }

        public virtual void ReadUpdateInfo(byte[] data, bool haspassword, string password)
        {
            if (haspassword)
            {
                data = Feng.IO.DEncrypt.Decrypt(data, password);
            }
            this.UpdateInfo.Read(data);
        }

        public virtual Feng.Data.DataValueCollection ReadDataValue(byte[] data, bool haspassword, string password)
        {
            if (haspassword)
            {
                data = Feng.IO.DEncrypt.Decrypt(data, password);
            }
            Feng.Data.DataValueCollection datavalues = ReadDataValue(data);
            return datavalues;
        }

        public virtual void ReadDataFile(byte[] data, int version, bool haspassword, string password)
        {
            using (Feng.Excel.IO.BinaryReader br = new IO.BinaryReader(data))
            {
                DataStruct ds = br.ReadDataStruct();
                readDics.Clear();
                ReadDataStruct(ds);
                ReadEditControls(version, br);
                OpenColumns(version, br);
                OpenRows(version, br);
                InitRows();
                InitColumns();
                short filestate = br.ReadInt16();
                while (filestate != ConstantValue.FileOver)
                {
                    DataStruct ds2 = null;
                    switch (filestate)
                    {
                        case ConstantValue.FileEndMergeCell:
                            OpenMergeCells(version, br);
                            break;
                        case ConstantValue.FileEndBackCell:
                            OpenBackCells(this, version, br);
                            break;
                        case ConstantValue.FileDisplayArea:
                            this.DisplayArea = new SelectCellCollection();
                            this.DisplayArea.BeginCell = this[br.ReadInt32(), br.ReadInt32()];
                            this.DisplayArea.EndCell = this[br.ReadInt32(), br.ReadInt32()];
                            break;
                        case ConstantValue.FileFooter:
                            return;
                        case ConstantValue.ListExtendCells:
                            ds2 = br.ReadDataStruct();
                            break;
                        case ConstantValue.ExtendData:
                            this.ReadExtendData(br);
                            break;
                        case ConstantValue.FilePrintArea:
                            this.ReadPrintArea(br);
                            break;
                        default:
                            DataStruct datamergecells = br.ReadDataStruct();
                            break;
                    }
                    filestate = br.ReadInt16();
                }
            }
        }
        public virtual void OpenFileData(byte[] data)
        {
            Open(data);
        }
        public virtual bool Open(byte[] data)
        {
            return Open(data, string.Empty, null);
        }
        public virtual bool Open(byte[] data, ReadInfo readInfo)
        {
            return Open(data, string.Empty, readInfo);
        }

        public virtual bool Open(byte[] data, string password, ReadInfo readInfo)
        {
            this.Clear();
            bool resutl = false;
            if (data == null)
                return false;
            if (data.Length < 1)
                return false;
            using (Feng.Excel.IO.BinaryReader reader = new Feng.Excel.IO.BinaryReader(data))
            {
                resutl = this.Open(reader, password, readInfo);
            }
            return resutl;
        }
        public virtual bool Open(Feng.Excel.IO.BinaryReader stream, string password, ReadInfo readInfo)
        {
            bool resutl = false;
            try
            {

                bool res = Feng.Utils.ConvertHelper.ToBoolean(OnPropertyBeforeLoadRow());
                if (res)
                {
                    return resutl;
                }
                this.BeginSetFirstDisplayRowIndex();
                this.BeginSetFirstDisplayColumnIndex();
                OpenContents(stream, password, readInfo);
                //ushort fileheader = stream.ReadUInt16();
                //if (fileheader != ConstantValue.FileHeader)
                //{
                //    throw new Exception("Not DataExcel File");
                //}

                //int version = stream.ReadInt();
                //bool haspassword = stream.ReadBoolean();
                //if (version == IOVersion_DataStruct)
                //{
                //    ReadIOVersion(stream);
                //}
                //else if (version == IOVersion)
                //{

                //    byte datafileenum = stream.ReadByte();
                //    while (datafileenum != DataExcelFileEnum.DataExcelFile_End)
                //    {
                //        byte[] data = stream.ReadBytes();
                //        switch (datafileenum)
                //        {
                //            case DataExcelFileEnum.DataExcelFile_All:
                //                data = Feng.IO.CompressHelper.GZipDecompress(data);
                //                //System.IO.File.WriteAllBytes("d:\\qq.dat", data);
                //                ReadDataFile(data, version, haspassword, password);
                //                break;
                //            case DataExcelFileEnum.DataExcelFile_DATAVALUE:
                //                ReadDataValue(data, haspassword, password);
                //                break;
                //            case DataExcelFileEnum.DataExcelFile_UPDATEINFO:
                //                ReadUpdateInfo(data, haspassword, password);
                //                break;
                //            case DataExcelFileEnum.DataExcelFile_CODE:
                //                ReadCode(data, haspassword, password);
                //                break;
                //            default:
                //                break;
                //        }
                //        datafileenum = stream.ReadByte();
                //    }
                //    short filefooter = stream.ReadInt16();
                //}
                //else
                //{
                //    this[1, 1].Value = "文件打开失败，更新至最新版";
                //}
                if (readInfo == null)
                {
                    if (this.AutoExecuteExpress)
                    {
                        ExecuteExpress();
                    }

                    OnLoadCompleted();
                    OnPropertyLoadCompleted();
                    FileLoadFinishedFreshCellDataBase();
                }
                else
                {
                    if (readInfo.ExecuteExpress)
                    {
                        if (this.AutoExecuteExpress)
                        {
                            ExecuteExpress();
                        }
                    }
                    if (readInfo.LoadCompleted)
                    {
                        OnLoadCompleted();
                    }
                    if (readInfo.PropertyLoadCompleted)
                    {
                        OnPropertyLoadCompleted();
                    }
                    if (readInfo.FileLoadFinishedFreshCellDataBase)
                    {
                        FileLoadFinishedFreshCellDataBase();
                    }
                }
                resutl = true;
                this.OpenCount = this.OpenCount + 1;
            }
            catch (Exception ex)
            {
                this.Init();
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcelFile", "Open", ex);
                Feng.IO.LogHelper.Log(ex);
                this.OnException(ex);
            }
            finally
            {
                this.EndSetFirstDisplayRowIndex();
                this.EndSetFirstDisplayColumnIndex();
            }
            return resutl;
        }
        public virtual void OpenContents(Feng.Excel.IO.BinaryReader stream, string password, ReadInfo readInfo)
        {
            ushort fileheader = stream.ReadUInt16();
            if (fileheader != ConstantValue.FileHeader)
            {
                throw new Exception("Not DataExcel File");
            }

            int version = stream.ReadInt();
            bool haspassword = stream.ReadBoolean();
            if (version == IOVersion_DataStruct)
            {
                ReadIOVersion(stream);
            }
            else if (version == IOVersion)
            {

                byte datafileenum = stream.ReadByte();
                while (datafileenum != DataExcelFileEnum.DataExcelFile_End)
                {
                    byte[] data = stream.ReadBytes();
                    switch (datafileenum)
                    {
                        case DataExcelFileEnum.DataExcelFile_All:
                            data = Feng.IO.CompressHelper.GZipDecompress(data);
                            //System.IO.File.WriteAllBytes("d:\\qq.dat", data);
                            ReadDataFile(data, version, haspassword, password);
                            break;
                        case DataExcelFileEnum.DataExcelFile_DATAVALUE:
                            ReadDataValue(data, haspassword, password);
                            break;
                        case DataExcelFileEnum.DataExcelFile_UPDATEINFO:
                            ReadUpdateInfo(data, haspassword, password);
                            break;
                        case DataExcelFileEnum.DataExcelFile_CODE:
                            ReadCode(data, haspassword, password);
                            break;
                        default:
                            break;
                    }
                    datafileenum = stream.ReadByte();
                }
                short filefooter = stream.ReadInt16();
            }
            else
            {
                this[1, 1].Value = "文件打开失败，更新至最新版";
            }

        }

        private void ReadCode(byte[] data, bool haspassword, string password)
        {
            if (haspassword)
            {
                data = Feng.IO.DEncrypt.Decrypt(data, password);
            }
            this.Code = Feng.IO.BitConver.GetString(data);
        }

        public void OnLoadCompleted()
        {
            if (LoadCompleted != null)
            {
                LoadCompleted(this);
            }

        }

        public virtual bool Open(string file, string password, ReadInfo readInfo)
        {
            bool resutl = false;
            if (!System.IO.File.Exists(file))
            {
                return false;
            }
            bool handled = false;
            this.OnBeforeOpenFile(handled, file);
            if (handled)
            {
                return false;
            }
            string filetype = System.IO.Path.GetExtension(file);
            filetype = filetype.ToLower();

            this.Clear();
            using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Open))
            {
                resutl = Open(fs, password, readInfo);
            }

            int focusedrowindex = Feng.Utils.ConvertHelper.ToInt32(this.readDics.Get("focusedcellrowindex"), -1);
            int focusedcolumnindex = Feng.Utils.ConvertHelper.ToInt32(this.readDics.Get("focusedcellcolumnindex"), -1);
            this.VScroll.Max = this.Rows.Max;
            if (focusedrowindex > 0 && focusedcolumnindex > 0)
            {
                ICell cell = this.GetCell(focusedrowindex, focusedcolumnindex);
                this.FocusedCell = cell;
            }
            this.OnOpenFile(file);
            return resutl;
        }

        public void Open(System.IO.Stream stream, ReadInfo readInfo)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(stream))
            {
                this.Open(bw, string.Empty, readInfo);
            }
        }

        public bool Open(System.IO.Stream stream, string password, ReadInfo readInfo)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(stream))
            {
                return this.Open(bw, password, readInfo);
            }
        }
        public virtual bool Open(string file)
        {
            return Open(file, null);
        }
        public virtual bool OpenContents(string file)
        {
            return OpenContents(file, null);
        }
        public virtual bool OpenContents(string file, ReadInfo readInfo)
        {
            bool resutl = false;
            string fileold = this.FileName;

            resutl = this.OpenContents(file, this.Password, readInfo);
            if (InDesign)
            {
                this.FileName = fileold;
            }
            else
            {
                this.FileName = file;
            }
            return resutl;
        }


        public virtual bool OpenContents(string file, string password, ReadInfo readInfo)
        {
            bool resutl = false;
            if (!System.IO.File.Exists(file))
            {
                return false;
            }
            this.Clear();
            using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Open))
            {
                resutl = OpenContents(fs, password, readInfo);
            }

            return resutl;
        }

        public bool OpenContents(System.IO.Stream stream, string password, ReadInfo readInfo)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(stream))
            {
                this.OpenContents(bw, password, readInfo);
                return true;
            }
        }

        public virtual bool Open(string file, ReadInfo readInfo)
        {
            bool resutl = false;
            string fileold = this.FileName;

            resutl = this.Open(file, this.Password, readInfo);
            if (InDesign)
            {
                this.FileName = fileold;
            }
            else
            {
                this.FileName = file;
            }
            return resutl;
        }

        public virtual string Open()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = Feng.App.FileExtension_DataExcel.SelDataExcelFileAndAll;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this._filename = dlg.FileName;
                    this.Open(this._filename);
                    return dlg.FileName;
                }
            }
            return string.Empty;
        }


        public virtual List<DataStruct> GetCellsEditControls(List<ICell> cells)
        {
            List<DataStruct> dataStructs = new List<DataStruct>();
            List<ICellEditControl> list = GetCellEditList(cells);
            for (int i = 0; i < list.Count; i++)
            {
                ICellEditControl ie = list[i];
                ie.AddressID = i + 1;
                dataStructs.Add(ie.Data);
            }
            return dataStructs;
        }
        public virtual List<ICellEditControl> GetCellEditList(List<ICell> cells)
        {
            List<ICellEditControl> cellEditControls = new List<ICellEditControl>();
            for (int i = 0; i < cells.Count; i++)
            {
                ICell cell = cells[i];
                if (cell == null)
                    continue;
                if (cell.OwnEditControl == null)
                    continue;
                if (cell.OwnEditControl.GetType() == this.DefaultEdit.GetType())
                    continue;
                if (cellEditControls.Contains(cell.OwnEditControl))
                    continue;
                cellEditControls.Add(cell.OwnEditControl);
            }
            return cellEditControls;
        }

        #region Data 成员

        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = string.Empty,
                    Version = string.Empty,
                    AessemlyDownLoadUrl = string.Empty,
                    FullName = string.Empty,
                    Name = string.Empty,
                };
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (Feng.Excel.IO.BinaryWriter bw = this.ClassFactory.CreateBinaryWriter(ms))
                    {
                        bw.Write(0, Product.AssemblyProduct);
                        bw.Write(1, _AddSelect);
                        bw.Write(2, this._AutoGenerateRows);
                        bw.Write(3, this._AutoGenerateColumns);
                        bw.Write(4, _AutoExecuteExpress);
                        bw.Write(5, _backcolor);
                        //bw.Write(7, (int)_CellMouseState);
                        bw.Write(8, _columnautowidth);
                        bw.Write(21, _DefaultColumnWidth);
                        bw.Write(22, _DefaultRowHeight);
                        bw.Write(25, _EndReFresh);
                        bw.Write(27, _figures);
                        bw.Write(28, _filename);

                        bw.Write(30, _FirstDisplayedColumnIndex);
                        bw.Write(31, _FirstDisplayedRowIndex);

                        bw.Write(33, _FontSize);
                        bw.Write(38, _GridLineColor);
                        bw.Write(42, _showhorizontalruler);
                        bw.Write(50, _LineColor);
                        bw.Write(53, _maxColumn);
                        bw.Write(54, _maxRow);
                        bw.Write(60, false);
                        bw.Write(64, _MouseWheelChangedValue);
                        bw.Write(65, _PaddingLeft);
                        bw.Write(66, _PaddingTop);
                        bw.Write(67, _pageindex);
                        bw.Write(69, _password);
                        bw.Write(74, _printindex);
                        bw.Write(76, _rowautoheight);
                        bw.Write(77, _rowautosize);
                        bw.Write(78, _RowBackColor);
                        bw.Write(84, _SelectBorderColor);
                        bw.Write(85, _SelectBorderWidth);
                        bw.Write(86, _showrowheader);
                        bw.Write(87, _showcolumnheader);
                        bw.Write(90, _showgridcolumnline);
                        bw.Write(91, _showgridrowline);
                        bw.Write(92, _SrollStep);
                        bw.Write(97, _showverticalruler);
                        bw.Write(99, _showselectaddrect);
                        bw.Write(100, this.Cursor);
                        bw.Write(101, this._showhorizontalscroller);
                        bw.Write(102, this._showverticalscroller);
                        bw.Write(105, this._Allowchangedfirstdisplayrow);
                        bw.Write(106, this._Allowchangedfirstdisplaycolumn);
                        bw.Write(107, this._readonly);
                        bw.Write(108, this.DataMember);
                        //bw.WriteSerializeValue(109, this.DataSource);
                        bw.Write(110, this.CanUndoRedo);
                        bw.Write(111, this.BorderColor);

                        bw.Write(112, this._PrintMargins);

                        bw.Write(113, this.PrintLandScope);
                        bw.Write(114, this.PrintDocumentName);
                        bw.Write(115, this.PaperName);
                        bw.Write(116, this.PaperHeight);
                        bw.Write(117, this.PaperWidth);

                        bw.Write(119, false);
                        bw.Write(120, this.MultiSelect);
                        byte[] dataBackGroundImage = new byte[] { };
                        if (this.BackgroundImage != null)
                        {
                            dataBackGroundImage = Feng.Drawing.ImageHelper.GetData(this.BackgroundImage as Bitmap);
                        }
                        bw.Write(121, dataBackGroundImage);
                        bw.Write(122, (int)this.BackgroundImageLayout);
                        bw.Write(123, Feng.DataUtlis.SmallVersion.AssemblySecondVersion);

                        bw.Write(124, string.Empty);
                        bw.Write(125, string.Empty);
                        bw.Write(126, string.Empty);
                        bw.Write(127, string.Empty);
                        bw.Write(128, this._fileid);
                        bw.Write(129, this._autoshowscroller);
                        bw.Write(130, string.Empty);
                        bw.Write(131, this._ShowSelectBorder);
                        bw.Write(132, this._FirstDisplayedRowIndex);
                        bw.Write(133, this._FirstDisplayedColumnIndex);
                        int focusedrowindex = 1;
                        int focusedcolumnindex = 1;
                        if (this.FocusedCell != null)
                        {
                            focusedrowindex = this.FocusedCell.Row.Index;
                            focusedcolumnindex = this.FocusedCell.Column.Index;
                        }
                        bw.Write(134, focusedrowindex);
                        bw.Write(135, focusedcolumnindex);
                        bw.Write(136, _showfocusedcellborder);
                        bw.Write(137, _showfocusedcellbordercolor);
                        bw.Write(138, 0);
                        bw.Write(139, 0);
                        bw.Write(140, (int)this._formborderstyle);
                        bw.Write(141, this._showborder);
                        if (string.IsNullOrWhiteSpace(this._createuser))
                        {
                            this._createtime = DateTime.Now;
                            this._createuser = this.UserID;
                            this._createusername = this.UserName;
                        }
                        bw.Write(142, this._createtime);
                        bw.Write(143, this._createuser);
                        bw.Write(144, this._createusername);
                        bw.Write(145, DateTime.Now);//this.updatetime
                        bw.Write(146, this._updatetimes + 1);//this._updatetimes
                        bw.Write(147, this.UserID);//this.updateuserid
                        bw.Write(148, this.UserName);//this.updateusername
                        bw.Write(149, this._url);
                        bw.Write(150, this._contentwidth);
                        bw.Write(151, this._contentheight);
                        bw.Write(152, _PropertyClick);
                        bw.Write(153, _PropertyDoubleClick);
                        bw.Write(154, _PropertyValueChanged);
                        bw.Write(155, this._PropertyDataLoadCompleted);
                        bw.Write(156, _PropertyKeyDown);
                        bw.Write(157, _PropertyKeyUp);
                        bw.Write(158, _PropertyEdit);
                        bw.Write(159, this._PropertyFormClosing);
                        bw.Write(160, this._PropertyNew);
                        bw.Write(161, string.Empty);
                        bw.Write(162, this._PropertyEndEdit);
                        bw.Write(163, (int)this._AllowInputExpress);
                        bw.Write(164, this._AllowSaveAs);
                        byte[] buf = new byte[0];
                        if (this._FunctionList != null)
                        {
                            using (Feng.IO.BufferWriter bwfun = new Feng.IO.BufferWriter())
                            {
                                bwfun.Write(this._FunctionList.Count);
                                foreach (object key in this._FunctionList.Keys)
                                {
                                    string keytext = Feng.Utils.ConvertHelper.ToString(key);
                                    string valuetext = Feng.Utils.ConvertHelper.ToString(this._FunctionList[key]);
                                    bwfun.Write(keytext);
                                    bwfun.Write(valuetext);
                                }
                                buf = bwfun.GetData();
                            }
                        }
                        bw.Write(165, buf);
                        bw.Write(166, this._frozenrow);
                        bw.Write(167, this._Frozencolumn);
                        _lastsavefileindex++;
                        _lastsavefileguid = Guid.NewGuid().ToString();
                        _lastsavefiledatetime = DateTime.Now.ToString();
                        bw.Write(168, this._lastsavefileguid);
                        bw.Write(169, this._lastsavefiledatetime);
                        bw.Write(170, this._lastsavefileindex);
                        bw.Write(171, this._opencount);

                        byte[] enterAccountData = new byte[] { };
                        if (this.EnterAccount != null)
                        {
                            enterAccountData = this.EnterAccount.GetData();
                        }
                        bw.Write(172, enterAccountData);
                    }
                    data.Data = ms.ToArray();
#if DEBUG2
                  Feng.Utils.TraceHelper.WriteTrace("DataStruct Data", data.Data);
#endif
                }

                return data;
            }
        }

        private string _lastsavefileguid = string.Empty;
        private string _lastsavefiledatetime = string.Empty;
        private int _lastsavefileindex = 0;

        #endregion

        #region SavePrintArea 成员

        public virtual void SavePrintArea(Feng.Excel.IO.BinaryWriter bw)
        {
            if (this.PrintArea != null)
            {
                bw.Write(ConstantValue.FilePrintArea);
                bw.Write(118, this.PrintArea.BeginCell.Row.Index);
                bw.Write(118, this.PrintArea.BeginCell.Column.Index);
                bw.Write(118, this.PrintArea.EndCell.Row.Index);
                bw.Write(118, this.PrintArea.EndCell.Column.Index);
            }

        }

        public virtual void ReadPrintArea(Feng.Excel.IO.BinaryReader stream)
        {
            int printareabegincellrowindex = stream.ReadIndex(118, -1);
            int printareabegincellcolumnindex = stream.ReadIndex(118, -1);
            int printareaendcellrowindex = stream.ReadIndex(118, -1);
            int printareaendcellcolumnindex = stream.ReadIndex(118, -1);
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

        #endregion

        #region DataValues
        public virtual DataValueCollection GetValusFormFile(string file)
        {
            return GetValusFormFile(file, string.Empty);
        }
        public virtual DataValueCollection GetValusFormFile(string file, string password)
        {
            DataValueCollection dvs = null;
            using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Open))
            {
                using (Feng.Excel.IO.BinaryReader stream = new IO.BinaryReader(fs))
                {
                    dvs = GetValusFormFile(stream, password);
                }
                fs.Close();
            }
            return dvs;
        }
        public virtual DataValueCollection GetValusFormFile(Feng.Excel.IO.BinaryReader stream, string password)
        {
            try
            {
                ushort fileheader = stream.ReadUInt16();
                if (fileheader != ConstantValue.FileHeader)
                {
                    throw new Exception("Not DataExcel File");
                }

                int version = stream.ReadInt();
                bool haspassword = stream.ReadBoolean();
                if (version == IOVersion_DataStruct)
                {
                    return ReadDataValues(stream);
                }
                byte datafileenum = stream.ReadByte();
                while (datafileenum != DataExcelFileEnum.DataExcelFile_End)
                {
                    byte[] data = stream.ReadBytes();
                    switch (datafileenum)
                    {
                        case DataExcelFileEnum.DataExcelFile_All:
                            break;
                        case DataExcelFileEnum.DataExcelFile_DATAVALUE:
                            return ReadDataValue(data, haspassword, password);
                        case DataExcelFileEnum.DataExcelFile_UPDATEINFO:
                            break;
                        default:
                            break;
                    }
                    datafileenum = stream.ReadByte();
                }
                short filefooter = stream.ReadInt16();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
            }
            return null;
        }
        #endregion

        #region DataValues
        public virtual void ReadDataBase(string file)
        {
            ReadDataBase(file, string.Empty);
        }
        public virtual void ReadDataBase(string file, string password)
        {
            using (System.IO.FileStream fs = System.IO.File.Open(file, System.IO.FileMode.Open))
            {
                using (Feng.Excel.IO.BinaryReader stream = new IO.BinaryReader(fs))
                {
                    ReadDataBase(stream, password);
                }
                fs.Close();
            }
        }
        public virtual void ReadDataBase(Feng.Excel.IO.BinaryReader stream, string password)
        {
            try
            {
                ushort fileheader = stream.ReadUInt16();
                if (fileheader != ConstantValue.FileHeader)
                {
                    throw new Exception("Not DataExcel File");
                }

                int version = stream.ReadInt();
                bool haspassword = stream.ReadBoolean();
                if (version == IOVersion_DataStruct)
                {
                    ReadDataBase(stream);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
            }
        }

        public virtual void FileLoadFinishedFreshCellDataBase()
        {
            foreach (var item in this.CellDataBase.Tables)
            {
                foreach (var row in item.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        try
                        {
                            cell.Value.Refresh();
                        }
                        catch (Exception ex)
                        {
                            Feng.Utils.BugReport.Log(ex);
                            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcelFile", "FileLoadFinishedFreshCellDataBase", ex);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
