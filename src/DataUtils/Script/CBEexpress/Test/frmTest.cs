using Feng.Script.CBEexpress;
using System;
using System.Windows.Forms;

namespace RestSharp
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }
        NetParser netParser = new NetParser();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //netParser.AddFunction(new SqliteDB.SqlMethodContainer());
            //netParser.AddFunction(new SqliteDB.SqlServerMethodContainer());
            //netParser.AddFunction(new SqliteDB.DebugMethodContainer());
            //netParser.AddFunction(new SqliteDB.MainViewMethodContainer());
            //netParser.AddFunction(new SqliteDB.NotificationMethodContainer());
            //netParser.AddFunction(new SqliteDB.CFusionMethodContainer());
            //netParser.AddFunction(new SqliteDB.StaticMethodContainer());
            netParser.AddFunction(new Feng.Script.FunctionContainer.DateTimeFunctionContainer());
            netParser.AddFunction(new Feng.Script.FunctionContainer.CollectionFunctionContainer());
            netParser.InitDebugScript(this.richTextBox1.Text);
            netParser.Debug();
            netParser.debug.DebugEvent += Debug_DebugEvent;
        }
        int index = 100000;
        private void Debug_DebugEvent(object sender, DebugEventArgs e)
        {
            try
            {
                if (e.EventType == DebugEventType.Finish)
                {
                    string txt = Feng.Utils.ConvertHelper.ToString(e.Value);
                    AppendLog3(txt);
                    return;
                }
                if (e.CurrentStatement.NetStatements != null)
                {
                    if (e.CurrentStatement.NetStatements.Count > 0)
                    {
                        return;
                    }
                }
                string debugvalue = Feng.Utils.ConvertHelper.ToString(e.Value);
                if (e.EventType == DebugEventType.LogValue)
                {
                    Type t = typeof(OperatorSignModulus);
                    string txt = e.CurrentStatement.ToString();
                    AppendLog1(e, txt);
                }
                if (e.EventType == DebugEventType.BreakpointHit)
                {
                    Type t = typeof(OperatorSignModulus);
                    string txt = e.CurrentStatement.ToString();
                    AppendLog2(e, txt);
                }
                if (e.EventType == DebugEventType.ContextChanged)
                {
                    Type t = typeof(OperatorSignRelationalLessThan);
                    string txt = e.CurrentStatement.ToString();
                    AppendLog2(e, txt);
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void AppendLog2(DebugEventArgs e, string txt)
        {
            if (this.richTextBox2.InvokeRequired)
            {
                this.richTextBox2.Invoke(new Action(() =>
                {
                    this.richTextBox2.AppendText((index++).ToString());
                    this.richTextBox2.AppendText(" NestingLevel:" + e.NestingLevel + " ");
                    this.richTextBox2.AppendText("BreakpointHit:");
                    this.richTextBox2.AppendText(txt);
                    this.richTextBox2.AppendText(System.Environment.NewLine);
                }));
            }
            else
            {
                this.richTextBox2.AppendText((index++).ToString());
                this.richTextBox2.AppendText(" NestingLevel:" + e.NestingLevel + " ");
                this.richTextBox2.AppendText("BreakpointHit:");
                this.richTextBox2.AppendText(txt);
                this.richTextBox2.AppendText(System.Environment.NewLine);
            }
        }
        public void AppendLog1(DebugEventArgs e, string txt)
        {
            if (this.richTextBox3.InvokeRequired)
            {
                this.richTextBox3.Invoke(new Action(() =>
                {
                    this.richTextBox3.AppendText((index++).ToString());
                    this.richTextBox3.AppendText(" NestingLevel:" + e.NestingLevel + " ");
                    this.richTextBox3.AppendText("LogValue:");
                    this.richTextBox3.AppendText(txt);
                    this.richTextBox3.AppendText(e.VarName + " :");
                    this.richTextBox3.AppendText(e.Value.ToString());
                    this.richTextBox3.AppendText(System.Environment.NewLine);
                }));
            }
            else
            {
                this.richTextBox3.AppendText((index++).ToString());
                this.richTextBox3.AppendText(" NestingLevel:" + e.NestingLevel + " ");
                this.richTextBox3.AppendText("BreakpointHit:");
                this.richTextBox3.AppendText(txt);
                this.richTextBox3.AppendText(" Value:");
                this.richTextBox3.AppendText(e.Value.ToString());
                this.richTextBox3.AppendText(System.Environment.NewLine);
            }
        }
        public void AppendLog3(string txt)
        {
            if (this.richTextBox3.InvokeRequired)
            {
                this.richTextBox3.Invoke(new Action(() =>
                {
                    this.richTextBox3.AppendText((index++).ToString());
                    this.richTextBox3.AppendText("LogValue:");
                    this.richTextBox3.AppendText(txt);
                    this.richTextBox3.AppendText(System.Environment.NewLine);
                }));
            }
            else
            {
                this.richTextBox3.AppendText((index++).ToString());
                this.richTextBox3.AppendText("BreakpointHit:");
                this.richTextBox3.AppendText(txt);
                this.richTextBox3.AppendText(" Value:");
                this.richTextBox3.AppendText(System.Environment.NewLine);
            }
        }
        public void Test()
        {
            int b = 0;
            for (var i = 0; i < 100; i++)
            {
                b = b + i;
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            netParser.debug.SetCommand(DebugCommand.StepInto);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            netParser.debug.SetCommand(DebugCommand.StepOver);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            netParser.debug.SetCommand(DebugCommand.StepOut);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            netParser.debug.SetCommand(DebugCommand.Pause);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            netParser.debug.SetCommand(DebugCommand.Continue);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            netParser.debug.SetCommand(DebugCommand.Stop);
        }
    }
}

