using System;

namespace Feng.Forms.Views.Charts
{
    public class ChartContents : DivView
    {
        public ChartSerieCollection ChartSeries { get; set; }
        public ChartContents()
        {
            ChartSeries = new ChartSerieCollection();
        }
        public void AddSerie(ChartSerie serie)
        {
            this.AddView(serie);
            ChartSeries.Add(serie); 
        }
        public void RemoveSerie(ChartSerie serie)
        {
            ChartSeries.Remove(serie);
        }
        public override bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            int left = 0;
            int top = 0;
            foreach (BaseView item in this.Viewes)
            {
                IItemSize its= item as IItemSize;
                
                item.Left = this.Left;
                item.Top = this.Top;
                item.Width = this.Width;
                item.Height = this.Height;
                if (its != null)
                {
                    its.ItemLeft = left;
                    its.ItemTop = top;
                    left = left + its.ItemWidth;
                    top = top + its.ItemHeight;
                    
                }
            }
            return base.OnRefresh(sender, e, ve);
        }
    }

}
