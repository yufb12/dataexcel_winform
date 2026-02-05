using Feng.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Component
{
    public partial class TimerComponent : DivView
    {
        private System.Windows.Forms.Timer timer = null;
        [DefaultValue("")]
        public virtual string Name { get; set; }
        [DefaultValue(100)] 
        public virtual int Interval { get; set; }
        [DefaultValue(0)]
        public virtual int Times { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime LastTime { get; set; }
        [DefaultValue(true)]
        public virtual bool Enabled { get; set; }
        [DefaultValue("")]
        public virtual string Script { get; set; }
        public virtual void Start()
        {
            if (timer == null)
            {
                this.timer = new System.Windows.Forms.Timer();
                this.timer.Interval = this.Interval;
                this.timer.Tick += Timer_Tick;
            }
            this.timer.Start();
        }
        private bool lck = false;
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lck)
                    return;
                lck = true;
                Run();
            }
            finally
            {
                lck = false;
            }
        }

        public virtual Feng.Script.CBEexpress.NetParser Function { get; set; }
        public virtual void Run()
        {
            Function.Exec(this.Script);

        }
        public virtual void Stop()
        {
            if (timer != null)
            {
                this.timer.Stop();
            }
        }
        public TimerComponent()
        { 

        } 
        public override void Init()
        {
            timer = null;
            base.Init();
        }
        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.IO.BufferReader bw = new  IO.BufferReader(data.Data))
            {
                this.Interval = bw.ReadIndex(1, 0);
                this.Script = bw.ReadIndex(2, string.Empty);
                this.Enabled= bw.ReadIndex(3, false); 
            }
        }
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                { 
                    FullName = t.FullName,
                    Name = t.Name,
                };

                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                {
                    bw.Write(1, this.Interval);
                    bw.Write(2, this.Script);
                    bw.Write(3, this.Enabled);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

    }
}
