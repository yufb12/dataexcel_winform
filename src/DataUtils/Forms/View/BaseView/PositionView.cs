using Feng.Data;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class PositionView:DivView
    {
        public PositionView()
        {

        }
        /// <summary>
        /// 相对于父级视图
        /// </summary>
        public bool Absolute { get; set; }
        /// <summary>
        /// 流式布局
        /// </summary>
        public bool Flow { get; set; } 

        public bool AnchorLeft { get; set; }
        public bool AnchorRight { get; set; }
        public bool AnchorTop { get; set; }
        public bool AnchorBottom { get; set; }
        /// <summary>
        /// 比例 1到100 默认0 AnchorLeft=true,AnchorRight=true
        /// </summary>
        public ushort AnchorZoomWidth { get; set; }

        /// <summary>
        /// 比例 1到100 默认0 AnchorTop=true,AnchorBottom=true
        /// </summary>
        public ushort AnchorZoomHeight { get; set; }

        public bool Static { get; set; }
        public bool Inherit { get; set; }

        public int MaxWidth { get; set; }

        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            return base.OnSizeChanged(sender, e, ve);
        }

    }
 
}

