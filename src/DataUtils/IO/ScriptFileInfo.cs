using System;

namespace Feng.Excel.IO
{
    public class ScriptFileInfo
    {
        public string ID
        {
            get
            {
                int index1 = this.FullName.LastIndexOf("_");
                int index2 = this.FullName.LastIndexOf(".");
                if (index2 - index1 > 0 && index1 > 0 && index2 > 0)
                {
                    return this.FullName.Substring(index1, index2 - index1);
                }
                return string.Empty;
            }
        }

        public string DirectoryName { get; set; }

        public bool IsReadOnly { get; set; }

        public long Length { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CreationTimeUtc { get; set; }

        public string Extension { get; set; }

        public string FullName { get; set; }

        public DateTime LastAccessTime { get; set; }

        public DateTime LastAccessTimeUtc { get; set; }

        public DateTime LastWriteTime { get; set; }

        public DateTime LastWriteTimeUtc { get; set; }


    }

 
}
