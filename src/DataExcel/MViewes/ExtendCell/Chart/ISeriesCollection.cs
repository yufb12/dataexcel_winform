using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using Feng.Forms.Interface;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Chart
{

    public interface ISeriesValueCollection : IList<ISeriesValue>, IDraw, IGrid, IChart,IOwnerSeries
    {

    }
    public interface ISeriesCollection : IList<ISeries>, IDraw, IGrid, IChart 
    {

    }

    public interface ITitleCollection : IList<IChartTitle>, IDraw, IGrid, IChart
    {
        int GetWidth();
        int GetHeight();
    }
 
}
