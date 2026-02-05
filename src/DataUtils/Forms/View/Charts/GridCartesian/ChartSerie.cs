using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class ChartSerie : DivView
    {
        private ChartSerieItemCollection SerieItemes { get; set; }
        public ChartSerie()
        {
            SerieItemes = new ChartSerieItemCollection();
        }
        public virtual ArraryValue Value { get; set; }

        public virtual xAxisBase xAxis { get; set; }
        public virtual yAxisBase yAxis { get; set; }
        public override bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            return base.OnRefresh(sender, e, ve);
        }
    }
 
}
