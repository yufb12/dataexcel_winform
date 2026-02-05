using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Data;
using System.Reflection;
using Feng.Enums;
using Feng.Forms.Controls.GridControl.Edits;
using Feng.Forms.Controls.GridControl;
using Feng.Forms.Interface;

namespace Feng.Forms.Views.Vector
{
    public partial class Canvas : DivView, Feng.Forms.Interface.IDataStruct, Feng.Print.IPrint, IDraw, IBounds 
    {
        public Canvas()
        {

        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            GraphicsState gs = g.Graphics.Save();
            g.Graphics.SetClip(this.Rect);
            g.Graphics.TranslateTransform(this.Left, this.Top);

            g.Graphics.Restore(gs);
            return true;
        }

        #region 初始化

        public override void Init()
        {

        }

        #endregion

        #region 保存


        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = string.Empty,
                    Version = string.Empty,
                    AessemlyDownLoadUrl = string.Empty,
                    FullName = string.Empty,
                    Name = string.Empty,
                };

                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                { 
                    //bw.Write(2, this._backcolor); 
                    //bw.Write(8, this._font);
                    //bw.Write(9, this._forecolor); 
                    //bw.Write(12, this._height);
                    //bw.Write(13, this._left);
                    //bw.Write(19, this._top);
                    //bw.Write(20, this._width);
                    //bw.Write(23, this._readonly);
                    data.Data = bw.GetData();
                    bw.Close();
                }

                return data;
            }
        }
 
 
        #endregion
        LayerCollection _layerss = null;
        public virtual LayerCollection Layerss
        {
            get
            {
                if (_layerss == null)
                {
                    _layerss = new LayerCollection();
                }
                return _layerss;
            }
        }

        public virtual void Refresh()
        {
            
        }
    }
}

