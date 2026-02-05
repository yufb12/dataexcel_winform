using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    //笛 卡尔 坐标系
    public class ChartViewCartesian:ChartView 
    {
        private CharGridCartesian cartesian = null;
        public virtual CharGridCartesian Cartesian
        {
            get
            {
                if (cartesian == null)
                {
                    cartesian = new CharGridCartesian();
                    this.AddGrid(cartesian);
                }
                return cartesian;
            }
            set
            {
                if (this.cartesian != null)
                {
                    this.Viewes.Remove(cartesian);
                }
                cartesian = value;
                this.AddGrid(cartesian);
            }
        }
        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            Cartesian.Left = this.Left + this.Padding.Left;
            Cartesian.Top = this.Top + this.Padding.Top;
            Cartesian.Width = this.Width - this.Padding.Left - this.Padding.Right;
            Cartesian.Height = this.Height - this.Padding.Top - this.Padding.Bottom;
            base.OnSizeChanged(sender, e, ve);
            OnRefresh(sender, e, ve);
            return false;
        }
        public virtual void InitGrid(CharGridCartesian value)
        {
            if (this.cartesian != null)
            {
                this.Viewes.Remove(cartesian);
            }
            cartesian = value;
            this.AddGrid(cartesian);
        }
        public override bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            Cartesian.Left = this.Left + this.Padding.Left;
            Cartesian.Top = this.Top + this.Padding.Top;
            Cartesian.Width = this.Width - this.Padding.Left - this.Padding.Right;
            Cartesian.Height = this.Height - this.Padding.Top - this.Padding.Bottom;
            return base.OnRefresh(sender, e, ve);
        }
    }
}
