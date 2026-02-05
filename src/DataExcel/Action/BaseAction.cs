using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Feng.Excel.Interfaces;
using Feng.Data;
using System.Drawing.Design;
using Feng.Excel.Designer;

namespace Feng.Excel.Actions
{
    public class string
    {
        private string _package = "DataExcel";
        [DefaultValue("DataExcel")]
        [Browsable(false)]
        public virtual string Package
        {
            get
            {
                return _package;
            }
            set
            {
                _package = value;
            }
        }
        private string _name = string.Empty;
        [Browsable(false)]
        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private string _description = string.Empty;
        [Browsable(false)]
        public virtual string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private string _commandid = string.Empty;
        [Browsable(false)]
        public virtual string CommandID
        {
            get
            {
                return _commandid;
            }
            set
            {
                _commandid = value;
            }
        }


        private string _text = string.Empty;
        [Browsable(true)]
        [Editor(typeof(MemonDesigner), typeof(UITypeEditor))]
        public virtual string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public virtual void Exec(object sender, ActionArgs e)
        {
            if (Action != null)
            {
                Action(sender, e);
            }
        }
        [Browsable(false)]
        public BaseActionHandler Action { get; set; }

        public virtual void Init(IBaseCell cell)
        {

        }

        public virtual void Init(DataExcel grid)
        {

        }

        public virtual void OnSelect()
        {

        }

        public override string ToString()
        {
            return Name;
        }

        public virtual void Read(byte[] data)
        { 

        }

        public virtual byte[] GetData()
        {
            byte[] data = null; 
            return data;
        }

        public byte[] ReadBase(byte[] data)
        {
            byte[] buffer = null;
            using (Feng.IO.BufferReader stream = new Feng.IO.BufferReader(data))
            {
                this.Package = stream.ReadIndex(1, string.Empty);
                this.Name = stream.ReadIndex(2, string.Empty);
                this.Description = stream.ReadIndex(3, string.Empty);
                this.Text = stream.ReadIndex(4, string.Empty);
                buffer = stream.ReadIndex(5, buffer);
                this.CommandID = stream.ReadIndex(6, string.Empty);
            }
            return buffer;
        }
        public byte[] BaseData()
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter stream = new Feng.IO.BufferWriter())
            {
                stream.Write(1, Package);
                stream.Write(2, Name);
                stream.Write(3, Description);
                stream.Write(4, Text);
                stream.Write(5, this.GetData());
                stream.Write(6, this.CommandID);
                data = stream.GetData();
            }
            return data;
        }
    }


    public class TempAction : string, IDisposable
    {
        public TempAction()
        {

        }
        private byte[] _data = null;
        public virtual byte[] Data
        {
            get
            {
                return _data;
            }
        }
        public override void Read(byte[] data)
        {

            try
            { 
                _data = base.ReadBase(data);  
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
  
        }
        public void Dispose()
        {
            _data = null;
        }

    }
    public class basetest
    {
        public string text = string.Empty;
        public virtual byte[] getdate()
        {
            return null;
        }
    }
    public class basetestc : basetest
    {
        public string name = string.Empty;
        public override byte[] getdate()
        {
            return base.getdate();
        }
    }
}
