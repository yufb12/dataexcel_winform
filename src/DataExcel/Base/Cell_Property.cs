using Feng.Data;
using Feng.Drawing;
using Feng.Enums;
using Feng.Excel.Actions;
using Feng.Excel.App;
using Feng.Excel.Args;
using Feng.Excel.Commands;
using Feng.Excel.Designer;
using Feng.Excel.Drawing;
using Feng.Excel.Edits;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Excel.Script;
using Feng.Excel.Styles;
using Feng.Forms.Base;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Views;
using Feng.Print;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace Feng.Excel.Base
{
    public partial class Cell : ICell, IReadDataStruct
    {
        #region PropertyEvent 成员
        private string _PropertyOnMouseUp = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseUp
        {
            get
            {
                return _PropertyOnMouseUp;
            }
            set
            {
                _PropertyOnMouseUp = value;

            }
        }



        private string _PropertyOnMouseMove = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseMove
        {
            get
            {
                return _PropertyOnMouseMove;
            }
            set
            {
                _PropertyOnMouseMove = value;

            }
        }

        private string _PropertyOnMouseLeave = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseLeave
        {
            get
            {
                return _PropertyOnMouseLeave;
            }
            set
            {
                _PropertyOnMouseLeave = value;

            }
        }

        private string _PropertyOnMouseHover = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseHover
        {
            get
            {
                return _PropertyOnMouseHover;
            }
            set
            {
                _PropertyOnMouseHover = value;

            }
        }

        private string _PropertyOnMouseEnter = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseEnter
        {
            get
            {
                return _PropertyOnMouseEnter;
            }
            set
            {
                _PropertyOnMouseEnter = value;

            }
        }

        private string _PropertyOnMouseDown = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseDown
        {
            get
            {
                return _PropertyOnMouseDown;
            }
            set
            {
                _PropertyOnMouseDown = value;

            }
        }

        private string _PropertyOnMouseDoubleClick = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseDoubleClick
        {
            get
            {
                return _PropertyOnMouseDoubleClick;
            }
            set
            {
                _PropertyOnMouseDoubleClick = value;

            }
        }

        private string _PropertyOnMouseClick = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseClick
        {
            get
            {
                return _PropertyOnMouseClick;
            }
            set
            {
                _PropertyOnMouseClick = value;

            }
        }

        private string _PropertyOnMouseCaptureChanged = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseCaptureChanged
        {
            get
            {
                return _PropertyOnMouseCaptureChanged;
            }
            set
            {
                _PropertyOnMouseCaptureChanged = value;

            }
        }

        private string _PropertyOnMouseWheel = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnMouseWheel
        {
            get
            {
                return _PropertyOnMouseWheel;
            }
            set
            {
                _PropertyOnMouseWheel = value;

            }
        }

        private string _PropertyOnClick = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnClick
        {
            get
            {
                return _PropertyOnClick;
            }
            set
            {
                _PropertyOnClick = value;

            }
        }

        private string _PropertyOnKeyDown = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnKeyDown
        {
            get
            {
                return _PropertyOnKeyDown;
            }
            set
            {
                _PropertyOnKeyDown = value;

            }
        }

        private string _PropertyOnKeyPress = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnKeyPress
        {
            get
            {
                return _PropertyOnKeyPress;
            }
            set
            {
                _PropertyOnKeyPress = value;

            }
        }

        private string _PropertyOnKeyUp = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnKeyUp
        {
            get
            {
                return _PropertyOnKeyUp;
            }
            set
            {
                _PropertyOnKeyUp = value;

            }
        }

        private string _PropertyOnPreviewKeyDown = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnPreviewKeyDown
        {
            get
            {
                return _PropertyOnPreviewKeyDown;
            }
            set
            {
                _PropertyOnPreviewKeyDown = value;

            }
        }

        private string _PropertyOnDoubleClick = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnDoubleClick
        {
            get
            {
                return _PropertyOnDoubleClick;
            }
            set
            {
                _PropertyOnDoubleClick = value;

            }
        }

        private string _PropertyOnCellInitEdit = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnCellInitEdit
        {
            get
            {
                return _PropertyOnCellInitEdit;
            }
            set
            {
                _PropertyOnCellInitEdit = value;

            }
        }

        private string _PropertyOnCellEndEdit = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnCellEndEdit
        {
            get
            {
                return _PropertyOnCellEndEdit;
            }
            set
            {
                _PropertyOnCellEndEdit = value;

            }
        }

        private string _PropertyOnCellValueChanged = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnCellValueChanged
        {
            get
            {
                return _PropertyOnCellValueChanged;
            }
            set
            {
                _PropertyOnCellValueChanged = value;

            }
        }


        private string _PropertyOnDrawBack = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnDrawBack
        {
            get
            {
                return _PropertyOnDrawBack;
            }
            set
            {
                _PropertyOnDrawBack = value;

            }
        }


        private string _PropertyOnDrawCell = string.Empty;
        [Browsable(true)]
        [Category(CategorySetting.PropertyEvent)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string PropertyOnDrawCell
        {
            get
            {
                return _PropertyOnDrawCell;
            }
            set
            {
                _PropertyOnDrawCell = value;

            }
        }

        #endregion
    }
}