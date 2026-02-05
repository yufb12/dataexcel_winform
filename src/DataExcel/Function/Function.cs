using Feng.Excel.Builder;
using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Script
{
    public class FunctionArg
    {
        public DataExcel Grid { get; set; }
        public ICell CurrentCell { get; set; }
    }
    [Serializable]
    public class Function
    {
        public static string input = @"";

        public static object RunExpress(DataExcel datagrid, ICell cell, string input)
        {
            bool hasError = false;
            return RunExpress(datagrid, cell, input, null, out hasError);
        }

        public static object RunExpress(DataExcel datagrid, ICell cell, string input, List<ICell> list, out bool hasError)
        {
            try
            {
                NetParser script = ScriptBuilder.BuilderScript(datagrid, cell);
                object currentvalue = string.Empty;
                currentvalue = script.ExecExpress(input);// GetMethodResult(datagrid, cell, msg, "getvalue", ref list);
                hasError = false;
                return currentvalue;
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Functions", "Function", "GetResult", ex);
                hasError = true;
                return "#Error";
            }
        }

        public static string GetNextRowExpress(string express, int sindex)
        {
            string pattern = "([A-Za-z]{1,2})([1-9][0-9]{0,4})";
            System.Text.RegularExpressions.MatchCollection mcs = System.Text.RegularExpressions.Regex.Matches(express, pattern);
            StringBuilder sb = new StringBuilder();

            int t = 0;
            if (mcs.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match m in mcs)
                {
                    System.Text.RegularExpressions.Group col = m.Groups[1];
                    System.Text.RegularExpressions.Group row = m.Groups[2];
                    string coltext = col.Value;
                    string rowtext = row.Value;
                    int index = m.Index;
                    int len = m.Length;

                    string text = express.Substring(t, index - t);
                    t = index + len;
                    sb.Append(text);
                    sb.Append(coltext);
                    sb.Append(int.Parse(rowtext) + sindex);
                }
            }
            string f = express.Substring(t);
            sb.Append(f);
            return sb.ToString();
        }
        public static string GetNextColumnExpress(string express, int sindex)
        {
            string pattern = "([A-Za-z]{1,2})([1-9][0-9]{0,4})";
            System.Text.RegularExpressions.MatchCollection mcs = System.Text.RegularExpressions.Regex.Matches(express, pattern);
            StringBuilder sb = new StringBuilder();

            int t = 0;
            if (mcs.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match m in mcs)
                {
                    System.Text.RegularExpressions.Group col = m.Groups[1];
                    System.Text.RegularExpressions.Group row = m.Groups[2];
                    string coltext = col.Value;
                    string rowtext = row.Value;
                    int index = m.Index;
                    int len = m.Length;

                    string text = express.Substring(t, index - t);
                    t = index + len;
                    sb.Append(text);
                    string columnheader = DataExcel.GetColumnHeaderByColumnIndex((DataExcel.GetColumnIndexByColumnHeader(coltext) + sindex));
                    if (string.IsNullOrWhiteSpace(columnheader))
                    {
                        columnheader = coltext;
                    }
                    sb.Append(columnheader);
                    sb.Append(rowtext);
                }
            }
            string f = express.Substring(t);
            sb.Append(f);
            return sb.ToString();
        }
    }
}
