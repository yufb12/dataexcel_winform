using Feng.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class yAxisValue : yAxisBase
    {
        public yAxisValue()
        {
            Itemes = new AxisItemCollection();
            Width = 36;
            MaxInterval = decimal.MinValue;
            MinInterval = decimal.MaxValue;
        }

        [Browsable(true)]
        [DefaultValue(36)]
        public override int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        /// <summary>
        /// 用于制作动态排序柱状图。设为 true 时，表示 X 轴开启实时排序效果，仅当 X 轴的 type 是 'value' 时有效。
        /// </summary>
        public bool RealtimeSort { get; set; }
        /// <summary>
        /// 例如可以设置成1保证坐标轴分割刻度显示成整数 只在数值轴或时间轴中（type: 'value' 或 'time'）有效
        /// </summary>
        public decimal MinInterval { get; set; }
        /// <summary>
        /// 自动计算的坐标轴最大间隔大小。  
        /// 例如，在时间轴（（type: 'time'））可以设置成 3600 * 24 * 1000 保证坐标轴分割刻度最大为一天。
        /// 只在数值轴或时间轴中（type: 'value' 或 'time'）有效。
        /// </summary>
        public decimal MaxInterval { get; set; }
        /// <summary>
        /// 坐标轴的分割段数，需要注意的是这个分割段数只是个预估值，
        /// 最后实际显示的段数会在这个基础上根据分割后坐标轴刻度显示的易读程度作调整。
        /// 在类目轴 category 中无效。 
        /// </summary>
        public int SplitNumber { get; set; }
        /// <summary>
        /// 只在数值轴中（type: 'value'）有效
        /// 是否是脱离 0 值比例。设置成 true 后坐标刻度不会强制包含零刻度。在双数值轴的散点图中比较有用。
        /// 在设置 min 和 max 之后该配置项无效。
        /// </summary>
        public bool Scale { get; set; }

        public override AxisItemCollection Itemes
        {
            get;
            set;
        }
        public override int GetXPoint(object obj)
        {
            decimal value = Feng.Utils.ConvertHelper.ToDecimal(obj);
            decimal d = (value - MinInterval) / (MaxInterval-MinInterval);
            int top =Feng.Utils.ConvertHelper.ToInt(this.Bottom - this.Height * d);
            return top;
        }
        public virtual void Add(decimal value)
        {
            if (MaxInterval < value)
            {
                MaxInterval = value;
            }
            if (MinInterval > value)
            {
                MinInterval = value;
            }
            Add(new yAxisItem() { Text = value.ToString(), Value = value });
        }
        public virtual void Add(decimal[] textes)
        {
            foreach (decimal value in textes)
            {
                Add(value);
            }
        }
        public virtual void Add(yAxisItem item)
        {
            item.HorizontalAlignment = System.Drawing.StringAlignment.Far;
            item.VerticalAlignment = System.Drawing.StringAlignment.Far;
            Itemes.Add(item);
            this.AddView(item);
        }
        public virtual void RefreshItem()
        {
            if (Itemes.Count < 1)
                return;
            int d = this.Height / Itemes.Count;
            int left = this.Bottom- d;
            foreach (yAxisItem item in Itemes)
            {
                item.Top = left;
                left = left - d;
                item.Height = d;
                item.Width = this.Width;
                item.Left = this.Left;
            }
        }
        public override bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            RefreshItem();
            return base.OnRefresh(sender, e, ve);
        }
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            RefreshItem();
            return base.OnSizeChanged(sender, e, ve);
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.DrawLine(System.Drawing.Pens.Black, this.Right, this.Top, this.Right, this.Bottom);
            return base.OnDraw(sender, g);
        }
    }

}
