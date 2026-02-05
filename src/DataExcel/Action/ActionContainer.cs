using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Actions
{ 
    public abstract class DelBaseActionContainer
    {
        public BaseActionContainer()
        {

        }
        public abstract List<string> Actions { get; }
        public abstract string Name { get; }
        public virtual void Load(object sender)
        {

        }

        public virtual bool Execute(object sender, ActionArgs e, string action)
        {
            bool res = false;
            try
            { 
                foreach (string model in Actions)
                {
                    if (model == action)
                    {
                        model.Action(sender, e);
                        res = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except",ex); 
            }
            return res;
        }
    }
 

}
