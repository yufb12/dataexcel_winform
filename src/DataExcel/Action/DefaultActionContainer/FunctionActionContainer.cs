using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Feng.Excel.Interfaces; 

namespace Feng.Excel.Actions
{
    public class FunctionActionContainer : BaseActionContainer
    {
        public override string Name
        {
            get { return "Function"; }
        }

        public void ExecuteExpression(object sender, ActionArgs e)
        { 
            string function = e.Action.Text;
            Feng.Script.General.Lexer lex = new Feng.Script.General.Lexer(function);
            Feng.Script.General.Token token = lex.scan();
            Feng.Script.General.Token tokeneq = lex.scan();
            if (tokeneq.ToString() == "=")
            {
                int index = lex.Index;
                string express = function.Substring(index);
                string cellname = token.ToString();
                ICell cell = e.Grid.GetCellByName(cellname);
                if (cell != null)
                {
                    cell.Value = e.Grid.GetExpressValue(cell, express);
                }
            }
            else
            {
                function = e.Action.Text;
                object value = e.Grid.GetExpressValue(null, function);
            } 
        }

        public override List<string> Actions
        {
            get
            {
                List<string> list = new List<string>();
                string ai = null;
                ai = new string();
                ai.Action = ExecuteExpression;
                ai.Name = "ExecuteExpression";
                list.Add(ai);
                 
                return list;
            }
        }
 
    }
}