//using Feng.Excel.Interfaces;
//using Feng.Script.CBEexpress;
//using System.Collections.Generic;

//namespace Feng.Excel.Script
//{

//    public class CBDataExcelScript : Eexpress
//    {
//        public CBDataExcelScript()
//        {

//        }
//        private OperatorExecBase execproxy = null;
//        public override OperatorExecBase ExecProxy
//        {
//            get
//            {
//                if (execproxy == null)
//                {
//                    DataExcelScriptStmtProxy dataExcelScriptStmtProxy = new DataExcelScriptStmtProxy();
//                    dataExcelScriptStmtProxy.Cells = new List<ICell>(); 
//                    execproxy = dataExcelScriptStmtProxy;
//                }
//                return execproxy;
//            }
//            set
//            {
//                execproxy = value;
//            }
//        }
         
//    }
//}
