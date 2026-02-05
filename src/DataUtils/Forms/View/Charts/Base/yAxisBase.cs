using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public abstract class yAxisBase: DivView
    {
        /// <summary>
        ///坐标轴两边留白策略，类目轴和非类目轴的设置和表现不一样。
        ///类目轴中 boundaryGap 可以配置为 true 和 false。默认为 true，
        ///这时候刻度只是作为分隔线，标签和数据点都会在两个刻度之间的带(band)中间。
        ///非类目轴，包括时间，数值，对数轴，boundaryGap是一个两个值的数组，
        ///分别表示数据最小值和最大值的延伸范围，
        ///可以直接设置数值或者相对的百分比，在设置 min 和 max 后无效。
        /// </summary>
        public bool BoundaryGap { get; set; }
        public bool Visible { get; set; }

        public System.Drawing.StringAlignment NameLocation { get; set; }

        public AxisType Type { get; set; }

        public abstract int GetXPoint(object index);
        public abstract AxisItemCollection Itemes { get; set; }
        public virtual ArraryValue Value { get; set; }
        public virtual int Index { get; set; }
        public virtual AlignMode AlignMode { get; set; }
    } 
}
