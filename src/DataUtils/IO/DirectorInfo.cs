using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Feng.Data;
 
namespace Feng.Excel.IO
{
    public class DirectorInfo
    { 
        public string DirectoryName { get; set; }

        public bool IsReadOnly { get; set; }

        public virtual bool HasDirctory { get; set; }

        public virtual bool HasFile { get; set; }
 
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }  

        public string FullName { get; set; }

        public DateTime LastAccessTime { get; set; } 

        public DateTime LastWriteTime { get; set; }
        public void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                reader.ReadCache();
                this.CreationTime = reader.ReadIndex(1, this.CreationTime);
                this.DirectoryName = reader.ReadIndex(2, this.DirectoryName);
                this.FullName = reader.ReadIndex(3, this.FullName);
                this.HasDirctory = reader.ReadIndex(4, this.HasDirctory);
                this.HasFile = reader.ReadIndex(5, this.HasFile);
                this.IsReadOnly = reader.ReadIndex(6, this.IsReadOnly);
                this.LastAccessTime = reader.ReadIndex(7, this.LastAccessTime);
                this.LastWriteTime = reader.ReadIndex(8, this.LastWriteTime);
                this.Name = reader.ReadIndex(9, this.Name);
            }
        }
        public DataStruct GetData()
        {
            DataStruct ds = new DataStruct();
            using (Feng.IO.BufferWriter writer = new Feng.IO.BufferWriter())
            {
                writer.Write(1, this.CreationTime);
                writer.Write(2, this.DirectoryName);
                writer.Write(3, this.FullName);
                writer.Write(4, this.HasDirctory);
                writer.Write(5, this.HasFile);
                writer.Write(6, this.IsReadOnly);
                writer.Write(7, this.LastAccessTime);
                writer.Write(8, this.LastWriteTime);
                writer.Write(9, this.Name);
                ds.Data = writer.GetData();
            }
            return ds;    
        }
    }
    public class PathType
    {
        public const short TYPE_FILE = 1;
        public const short TYPE_DIRECTORY = 2;
        public const short TYPE_DATAEXCELTEMPLATE = 3;
        public const short TYPE_DATAEXCELTEMPLATEDATA = 6;
        public const short TYPE_DATAEXCELTEMPLATE_NODE = 31;
        public const short TYPE_DATAEXCELTEMPLATE_DIRECTORY = 32;
        public const short TYPE_DATAEXCEL = 5; 
        public const short TYPE_USERFILE_RECV = 201;
        public const short TYPE_USERFILE_SEND = 202;
        public const short TYPE_USERFILE_FAVD = 203; 
        public const short TYPE_USERFILE_SERVERFILE = 205; 
        public const short TYPE_USERFILE_LOCALFILE = 206; 
        public const short TYPE_DATATABLE = 101;
        public const short TYPE_DATAEXCELTEMPLATEFILED = 3001; 
        public const short TYPE_Chat_User = 5001; 
        public const short TYPE_Chat_Talk = 5002; 
        public static short GetPathType(string file)
        {
            if (Feng.IO.FileHelper.GetExtension(file) == Feng.App.FileExtension_DataExcel.DataExcelTemplate)
            {
                return TYPE_DATAEXCELTEMPLATE;
            }
            if (Feng.IO.FileHelper.GetExtension(file) == Feng.App.FileExtension_DataExcel.DataExcelTemplateData)
            {
                return TYPE_DATAEXCELTEMPLATEDATA;
            } 
            if (Feng.IO.FileHelper.GetExtension(file) == Feng.App.FileExtension_DataExcel.DataExcel)
            {
                return PathType.TYPE_DATAEXCEL;
            }
            return TYPE_FILE;
        }
    }

    public class PathInfo
    {

        public string MappingSource { get; set; }
        public string MappingTarget { get; set; }
        public string ServerPath { get; set; }
        public bool LocationMapping { get; set; }

        public string DirectoryName { get; set; }
        public string TemplatePath { get; set; }

        public short PathType { get; set; }

        public bool IsReadOnly { get; set; }

        public virtual bool HasDirctory { get; set; }

        public virtual bool HasFile { get; set; }
 
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }  

        public string FullName { get; set; }

        public DateTime LastAccessTime { get; set; } 

        public DateTime LastWriteTime { get; set; }
        public long Size { get; set; }
        public void Read(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                reader.ReadCache();
                this.CreationTime = reader.ReadIndex(1, this.CreationTime);
                this.DirectoryName = reader.ReadIndex(2, this.DirectoryName);
                this.FullName = reader.ReadIndex(3, this.FullName);
                this.HasDirctory = reader.ReadIndex(4, this.HasDirctory);
                this.HasFile = reader.ReadIndex(5, this.HasFile);
                this.IsReadOnly = reader.ReadIndex(6, this.IsReadOnly);
                this.LastAccessTime = reader.ReadIndex(7, this.LastAccessTime);
                this.LastWriteTime = reader.ReadIndex(8, this.LastWriteTime);
                this.Name = reader.ReadIndex(9, this.Name);
                this.PathType = reader.ReadIndex(10, this.PathType);
                this.Size = reader.ReadIndex(11, this.Size);
                this.TemplatePath = reader.ReadIndex(12, this.TemplatePath);
            }
        }
        public DataStruct GetData()
        {
            DataStruct ds = new DataStruct();
            using (Feng.IO.BufferWriter writer = new Feng.IO.BufferWriter())
            {
                writer.Write(1, this.CreationTime);
                writer.Write(2, this.DirectoryName);
                writer.Write(3, this.FullName);
                writer.Write(4, this.HasDirctory);
                writer.Write(5, this.HasFile);
                writer.Write(6, this.IsReadOnly);
                writer.Write(7, this.LastAccessTime);
                writer.Write(8, this.LastWriteTime);
                writer.Write(9, this.Name);
                writer.Write(10, this.PathType);
                writer.Write(11, this.Size);
                writer.Write(12, this.TemplatePath);
                ds.Data = writer.GetData();
            }
            return ds;    
        }

        public override string ToString()
        {
            return FullName;
        }

        public DateTime LocalWriteTime { get; set; }
        public string State { get; set; }
        public PathInfo ParentPathInfo { get; set; }
        public string LocalPath { get; set; }
    }
}
