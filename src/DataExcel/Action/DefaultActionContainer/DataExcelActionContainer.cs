using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;

namespace Feng.Excel.Actions
{
    public class DataExcelActionContainer :  BaseActionContainer
    {
        public override string Name
        {
            get { return "DataExcel"; }
        }

        public static string PrintAction
        {
            get
            {
                return new string()
                {
                    Action = Print,
                    Name = "Print",
                    CommandID = "Print"
                };
            }
        }

        public DataExcelActionContainer()
        {

        }

        public void Exit(object sender, ActionArgs e)
        {
            CancelEventArgs ec = new CancelEventArgs();
            Application.Exit(ec);
            if (ec.Cancel)
            {

            }
        }

        public void Close(object sender, ActionArgs e)
        {
            if (e.Grid != null)
            {
                if (e.Action is ReminderAction)
                {
                    ReminderAction action = e.Action as ReminderAction;
                    if (action != null)
                    {
                        if (action.Reminder)
                        {
                            if (MessageBox.Show("确定关闭窗口?", "系统消息", MessageBoxButtons.OKCancel) != DialogResult.OK)
                            {
                                return;
                            }
                        }
                    }
                    e.Grid.FindForm().Close();
                }
            }
        }

        public void Clear(object sender, ActionArgs e)
        {
            e.Grid.Clear();
        }

        public static void Print(object sender, ActionArgs e)
        {
            e.Grid.Print();
        }

        public void PrintView(object sender, ActionArgs e)
        {
            e.Grid.PrintView();
        }

        public void PrintStep(object sender, ActionArgs e)
        {
            e.Grid.PrintStep();
        }

        public void RefreshExpression(object sender, ActionArgs e)
        {
            e.Grid.ExecuteExpress();
        }

        public void RunExpress(object sender, ActionArgs e)
        {
            if (e.Grid.FocusedCell != null)
            {
                e.Grid.FocusedCell.ExecuteExpression();
            }
        }

        public void FirstRow(object sender, ActionArgs e)
        {
            int index = Feng.Utils.ConvertHelper.ToInt32(e.Action.Text, -1);
            if (index > 0)
            {
                e.Grid.FirstDisplayedRowIndex = index;
            }
        }
 
        public void FirstColumn(object sender, ActionArgs e)
        {
            int index = Feng.Utils.ConvertHelper.ToInt32(e.Action.Text, -1);
            if (index > 0)
            {
                e.Grid.FirstDisplayedColumnIndex = index;
            }
        }

        //public void DoActions(object sender, ActionArgs e)
        //{
        //    ListAction list = e.Action as ListAction;
        //    if (list != null)
        //    {
        //        foreach (string c in list.Actions)
        //        {
        //            c.Exec(sender, e);
        //        }
        //    }
        //}

        public override List<string> Actions
        {
            get
            {
                List<string> list = new List<string>();
                string ai = new string();
                ai.Action = Exit;
                ai.Name = "Exit";
                list.Add(ai);
                 

                ai = new ReminderAction();
                ai.Action = Close;
                ai.Name = "Close";
                list.Add(ai);

                ai = new ReminderAction();
                ai.Action = Clear; 
                ai.Name = "Clear";
                list.Add(ai);

                ai = new string();
                ai.Action = Print;
                ai.Name = "Print";
                list.Add(ai);

                ai = new string();
                ai.Action = PrintView;
                ai.Name = "PrintView";
                list.Add(ai);

                ai = new string();
                ai.Action = PrintStep;
                ai.Name = "PrintStep";
                list.Add(ai);

                ai = new string();
                ai.Action = RefreshExpression;
                ai.Name = "RefreshExpression";
                list.Add(ai);

                ai = new string();
                ai.Action = FirstRow;
                ai.Name = "FirstRow";
                list.Add(ai);

                ai = new string();
                ai.Action = FirstColumn;
                ai.Name = "FirstColumn";
                list.Add(ai);



                //ai = new ListAction();
                //ai.Action = DoActions;
                //ai.Name = "DoActions";
                //list.Add(ai);
                return list;
            }
        }
 
    }
}