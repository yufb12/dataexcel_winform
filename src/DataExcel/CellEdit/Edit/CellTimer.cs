using Feng.Data;
using Feng.Excel.Commands;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellTimer : CellBaseEdit
    {
        public CellTimer(DataExcel grid)
            : base(grid)
        {

        }

        public override string ShortName { get { return "CellTimer"; } set { } }

        private int _interval = 5000;
        public virtual int Interval 
        {
            get { return this._interval; }
            set { this._interval = value; }
        }

        private string _script = string.Empty;
        public virtual string Script 
        
        {
            get { return this._script; }
            set { this._script = value; }
        }


        private bool _showPause = false;
        public virtual bool ShowPasus
        {
            get { return this._showPause; }
            set { this._showPause = value; }
        }


        private bool _showStart = false;
        public virtual bool ShowStart
        {
            get { return this._showStart; }
            set { this._showStart = value; }
        }


        private bool _showStop = false;
        public virtual bool ShowStop
        {
            get { return this._showStop; }
            set { this._showStop = value; }
        }

        private bool _autoRun = false;
        public virtual bool AutoRun
        {
            get { return this._autoRun; }
            set { this._autoRun = value; }
        }

        private int  _exeTimes= 0;
        public virtual int ExeTimes
        {
            get { return this._exeTimes; }
            set { this._exeTimes = value; }
        }

        private int _Times = 0;
        public virtual int Times
        {
            get { return this._Times; }
            set { this._Times = value; }
        }

        public short State { get; set; }
        public bool HasError { get; set; }
        public string ErrorText { get; set; }
        System.Threading.Thread thread = null;
        private bool run = false;
        private void Run()
        {
            while (run)
            {
                try
                {
                    this.Grid.RunScript(this.Cell, this.Script);
                }
                catch (Exception ex)
                {
                    HasError = true;
                    ErrorText = ex.Message; 
                }
                ExeTimes = ExeTimes + 1;
                int interrval = this.Interval;
                if (interrval < 50)
                {
                    interrval = 50;
                }
                System.Threading.Thread.Sleep(interrval);
            }
        }
        private void Start()
        {
            if (thread != null)
                return;
            thread = new System.Threading.Thread(this.Run);
            thread.IsBackground = true;
            thread.Start();
        }
        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        { 
            return false;
        }

        public override bool DrawCellBack(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }

        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0); 
                this._autoRun = bw.ReadIndex(2, this._autoRun);
                this._exeTimes = bw.ReadIndex(3, this._exeTimes);
                this._interval = bw.ReadIndex(4, this._interval);
                this._script = bw.ReadIndex(5, this._script);
                this._showPause = bw.ReadIndex(6, this._showPause);
                this._showStart = bw.ReadIndex(7, this._showStart);
                this._showStop = bw.ReadIndex(8, this._showStop);
                this._Times = bw.ReadIndex(9, this._Times);
            }
        }
        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = t.FullName,
                    Name = t.Name,
                };

                using (Feng.Excel.IO.BinaryWriter bw = new Feng.Excel.IO.BinaryWriter())
                {
                    bw.Write(1, this.AddressID);
                    bw.Write(2, this._autoRun);
                    bw.Write(3, this._exeTimes);
                    bw.Write(4, this._interval);
                    bw.Write(5, this._script);
                    bw.Write(6, this._showPause);
                    bw.Write(7, this._showStart);
                    bw.Write(8, this._showStop);
                    bw.Write(9, this._Times);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }
 
         
        public override ICellEditControl Clone(DataExcel grid)
        {
            CellTimer celledit = new CellTimer(grid); 
            celledit._autoRun = this._autoRun;
            celledit._exeTimes = this._exeTimes;
            celledit._interval = this._interval;
            celledit._script = this._script;
            celledit._showPause = this._showPause;
            celledit._showStart = this._showStart;
            celledit._showStop = this._showStop;
            celledit._Times = this._Times;
            return celledit;
        }
    }

}
