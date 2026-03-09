using System;
using System.Runtime.InteropServices;

namespace Tools
{ 
    public class RecycleHelper
    { 
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHFileOperation(ref SHFILEOPSTRUCT lpFileOp);
         
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;           
            public uint wFunc;           
            public string pFrom;          
            public string pTo;            
            public ushort fFlags;         
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }
         
        private const uint FO_DELETE = 0x0003;            
        private const ushort FOF_ALLOWUNDO = 0x0040;      
        private const ushort FOF_NOCONFIRMATION = 0x0010;  
        private const ushort FOF_SILENT = 0x0004;         

        public static bool DeleteToRecycleBin(string folderPath)
        {  
            SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT();
            fileOp.wFunc = FO_DELETE; 
            fileOp.pFrom = folderPath + "\0\0"; 
            fileOp.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION | FOF_SILENT; 
            int result = SHFileOperation(ref fileOp);
            return result == 0;
        }
    }
}