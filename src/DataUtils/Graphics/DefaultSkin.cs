using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Forms.Skin
{ 
    public class DefaultSkin
    {
        static DefaultSkin()
        {
        }
        public DefaultSkin()
        { 
            _skinname = "DefaultSkin";
        }
        private string _skinname = string.Empty;
        public string SkinName {
            get {
                return this._skinname;
            } 

        }
        
    }


}
