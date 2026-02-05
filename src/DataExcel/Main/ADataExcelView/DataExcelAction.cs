using Feng.Excel.Actions;
using Feng.Excel.Args;
using Feng.Excel.Builder;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Excel.Script;
using Feng.Forms.Controls.Designer;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        #region Method

        public object RunFunction(string functionname)
        {
            object funobj = this.FunctionList[functionname];
            string fun = Feng.Utils.ConvertHelper.ToString(funobj);
            if (string.IsNullOrWhiteSpace(fun))
            {
                return string.Empty;
            }
            object value = RunScript(this.FocusedCell, fun);
            string text = Feng.Utils.ConvertHelper.ToString(value);
            return text;
        }

        public object RunScript(ICell cell, string script)
        {
            object res = ScriptBuilder.Exec(this, cell, script, null);
            return res;
        }

        public object RunMethod(ICell cell, string method, params object[] args)
        {
            if (string.IsNullOrEmpty(method))
            {
                if (args.Length < 1)
                {
                    throw new Exception(ExceptionString.InvalidFunctionName);
                }
                return args[0];
            }
            object value = null;
            bool hasMethod = false;
            HandledEventArgs e = new HandledEventArgs();
            object res = OnExecFunction(e);
            if (e.Handled)
            {
                return res;
            }
            value = this.Methods.RunMethod(method, ref hasMethod, args);
            if (hasMethod)
            {
                return value;
            }

            return value;
        }

        public static bool GetCellIndex(string express, ref int rowindex, ref int columnindex)
        {
            if (string.IsNullOrWhiteSpace(express))
                return false;

            for (int i = 0; i < express.Length; i++)
            {
                char c = express[i];
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }
                if (char.IsLetter(c))
                {
                    string column = c.ToString();
                    for (i++; i < express.Length; i++)
                    {
                        c = express[i];
                        if (column.Length > 3)
                        {
                            return false;
                        }
                        if (char.IsLetter(c))
                        {
                            column = column + c.ToString();
                        }
                        else if (char.IsDigit(c))
                        {
                            c = express[i];
                            string row = c.ToString();
                            for (i++; i < express.Length; i++)
                            {
                                c = express[i];
                                if (char.IsDigit(c))
                                {
                                    row = row + c.ToString();
                                }
                                else if (char.IsWhiteSpace(c))
                                {
                                    for (i++; i < express.Length; i++)
                                    {
                                        c = express[i];
                                        if (!char.IsWhiteSpace(c))
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            string coltext = column;
                            string rowtext = row;
                            rowindex = int.Parse(rowtext);
                            columnindex = DataExcel.GetColumnIndexByColumnHeader(coltext);
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
            //string pattern = "([A-Za-z]{1,2})([1-9][0-9]{0,4})";
            //System.Text.RegularExpressions.MatchCollection mcs = System.Text.RegularExpressions.Regex.Matches(express, pattern);

            //if (mcs.Count == 1)
            //{
            //    System.Text.RegularExpressions.Match m = mcs[0];
            //    System.Text.RegularExpressions.Group col = m.Groups[1];
            //    System.Text.RegularExpressions.Group row = m.Groups[2];
            //    string coltext = col.Value;
            //    string rowtext = row.Value;
            //    rowindex = int.Parse(rowtext);
            //    columnindex = DataExcel.GetColumnIndexByColumnHeader(coltext);
            //    return true;
            //}
            //return false;
        }

        public ICell GetCell(string text)
        {
            int row = 1, col = 1;
            if (GetCellIndex(text, ref row, ref col))
            {
                return this[row, col];
            }
            return null;
        }
        public ICell GetCell(int rowindex, string columnname)
        {
            IRow row = this.Rows[rowindex];
            if (row != null)
            {
                return row.GetCellByName(columnname);
            }

            return null;
        }
        public bool CheckID(string id)
        {
            int row = 1, col = 1;
            return GetCellIndex(id, ref row, ref col);
        }
        #endregion
        #region Function
        public void AddMethod(IMethod method)
        {
            BeforeAddMethodArgs e = new BeforeAddMethodArgs();
            if (BeforeAddMethod != null)
            {
                BeforeAddMethod(this, e);
            }
            if (!e.Cancel)
            {
                this._Methods.Add(method);
            }
        }
        #endregion
 
        public void ExecuteExpress()
        {
            this.ExpressionCells.Sort();
            bool haschanged = false;
            foreach (ICell cell in this.ExpressionCells)
            {
                if (string.IsNullOrWhiteSpace(cell.Expression))
                {
                    haschanged = true;
                    continue;
                }
#if DEBUG
                if (cell.Name == "H26")
                {

                }
                if (cell.Name == "H10")
                {

                }
#endif
                cell.ExecuteExpression();
            }
            if (haschanged)
            {
                this.ExpressionCells.Refresh();
            }
        }

        public void ExecuteExpress(string script,ICell cell)
        {
            string express = script;
            List<ICell> list = new List<ICell>();
            bool blexec = false;
            object res = Function.RunExpress(this, cell, express, list, out blexec);
        }
        public object ExecuteAction(ActionArgs e, string txt, params object[] args)
        {
            try
            {
                if (this.InDesign)
                {
                    if (System.Windows.Forms.Control.ModifierKeys != Keys.Control)
                    {
                        return null;
                    }
                }
                return ScriptBuilder.Exec(this, e.Cell, txt, args);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            return null;
        }


        public void InitProperyEvent()
        {
 
        }
        private void DataExcel_ParentChanged(object sender, EventArgs e)
        {
            try
            { 
                InitProperyEvent();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
            }
        }
 
 
        public virtual void OnPropertyFormClosing()
        {
            if (PropertyFormClosing != null && !string.IsNullOrWhiteSpace(PropertyFormClosing))
            {
                ExecuteAction(new ActionArgs(PropertyFormClosing, this, null), PropertyFormClosing);
            }
        }
 
        public virtual void OnPropertyLoadCompleted()
        {
            if (PropertyDataLoadCompleted != null && !string.IsNullOrWhiteSpace(PropertyDataLoadCompleted))
            {
                ExecuteAction(new ActionArgs(PropertyDataLoadCompleted, this, null), PropertyDataLoadCompleted);
            }
        }
        private bool lckOnPropertyBeforeLoadRow = false;
        public virtual object OnPropertyBeforeLoadRow()
        {
            if (lckOnPropertyBeforeLoadRow)
                return null;
            lckOnPropertyBeforeLoadRow = true;
            try
            {

                if (PropertyBeforeLoadRow != null && !string.IsNullOrWhiteSpace(PropertyBeforeLoadRow))
                {
                    return ExecuteAction(new ActionArgs(PropertyBeforeLoadRow, this, null), PropertyBeforeLoadRow);
                }
            } 
            finally
            {
                lckOnPropertyBeforeLoadRow = false;
            }
            return null;
        }

        private string _PropertyBeforeLoadRow = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyBeforeLoadRow
        {
            get
            {
                return _PropertyBeforeLoadRow;
            }
            set
            {
                _PropertyBeforeLoadRow = value;

            }
        }

        private string _PropertyDataLoadCompleted = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyDataLoadCompleted
        {
            get
            {
                return _PropertyDataLoadCompleted;
            }
            set
            {
                _PropertyDataLoadCompleted = value;

            }
        }
  

        private string _PropertyFormClosing = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyFormClosing
        {
            get
            {
                return _PropertyFormClosing;
            }
            set
            {
                _PropertyFormClosing = value;

            }
        }
 
 
        private string _PropertyEndEdit = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyEndEdit
        {
            get
            {
                return _PropertyEndEdit;
            }
            set
            {
                _PropertyEndEdit = value;
            }
        }


        private string _PropertyClick = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyClick
        {
            get
            {
                return _PropertyClick;
            }
            set
            {
                _PropertyClick = value;
            }
        }



        private string _PropertyDoubleClick = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyDoubleClick
        {
            get
            {
                return _PropertyDoubleClick;
            }
            set
            {
                _PropertyDoubleClick = value;
            }
        }

        private string _PropertyValueChanged = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyValueChanged
        {
            get
            {
                return _PropertyValueChanged;
            }
            set
            {
                _PropertyValueChanged = value;
            }
        }
 
        private string _PropertyKeyDown = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyKeyDown
        {
            get
            {
                return _PropertyKeyDown;
            }
            set
            {
                _PropertyKeyDown = value;
            }
        }
 
        private string _PropertyKeyUp = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyKeyUp
        {
            get
            {
                return _PropertyKeyUp;
            }
            set
            {
                _PropertyKeyUp = value;
            }
        }
 
        private string _PropertyEdit = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyEdit
        {
            get
            {
                return _PropertyEdit;
            }
            set
            {
                _PropertyEdit = value;
            }
        }


        private string _PropertyNew = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue("")]
        public virtual string PropertyNew
        {
            get
            {
                return _PropertyNew;
            }
            set
            {
                _PropertyNew = value;
            }
        }
    }
}
