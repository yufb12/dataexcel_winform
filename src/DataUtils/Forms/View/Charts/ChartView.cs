using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{ 
    public class ChartView : DivView
    {
        public void AddGrid(CharGridBase chargrid)
        {
            this.Viewes.Add(chargrid);
        }

        public void AddLegend(ChartLegend legend)
        {
            this.Viewes.Add(legend);
        }

        public void AddTitle(ChartTitle title)
        {
            this.AddView(title);
        }
        public virtual ChartTitle ChartTitle { get; set; }
        public virtual ChartLegend ChartLegend { get; set; }
        public virtual CharGridBase CharGrid { get; set; }
    }
}
