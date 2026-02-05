using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class xAxisValue : xAxisBase
    {
        /// <summary>
        /// 用于制作动态排序柱状图。设为 true 时，表示 X 轴开启实时排序效果，仅当 X 轴的 type 是 'value' 时有效。
        /// </summary>
        public bool RealtimeSort { get; set; }
        /// <summary>
        /// 例如可以设置成1保证坐标轴分割刻度显示成整数 只在数值轴或时间轴中（type: 'value' 或 'time'）有效
        /// </summary>
        public int MinInterval { get; set; }
        /// <summary>
        /// 自动计算的坐标轴最大间隔大小。  
        /// 例如，在时间轴（（type: 'time'））可以设置成 3600 * 24 * 1000 保证坐标轴分割刻度最大为一天。
        /// 只在数值轴或时间轴中（type: 'value' 或 'time'）有效。
        /// </summary>
        public int MaxInterval { get; set; }
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

        public override int GetXPoint(int index)
        {
            throw new NotImplementedException();
        } 
        public override AxisItemCollection Itemes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
 
}
