using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Feng.Utils
{
    public class BaseNode
    {
        private string _ID = string.Empty;
        public virtual string ID
        {
            get {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }
 
        private string _text= string.Empty;
        public virtual string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        public int _level = 1;
        public virtual int Level
        {
            get
            {
                return this._level;
            }
            set
            {
                this._level = value;
            }
        }
        private string _path = string.Empty;
        public virtual string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }
        public List<BaseNode> Nodes
        {
            get;
            set;
        }
    }
}
