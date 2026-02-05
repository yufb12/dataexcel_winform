using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{ 

    public class CharGridPolar : CharGridBase
    {
        public void AddXAxis(xAxisBase axisbase)
        {
            this.AddView(axisbase);
        }
        public void AddYAxis(yAxisBase yaxisbase)
        {
            this.AddView(yaxisbase);
        }
        public void AddTitle(ChartTitle title)
        {
            this.AddView(title);
        }
        public void AddSerie(ChartSerie serie)
        {
            this.AddView(serie);
        }
    }
}
