using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace TianZiGeGenerator
{
    public class WorksheetConfig
    {
        private string _inputText = "宜 鹤 嫌 精巧 配合";
        private float _cellSize = 15f;
        private GridType _gridType = GridType.TianZi;
        private CharMode _charMode = CharMode.TraceWithBlank;
        private PageOrientation _orientation = PageOrientation.Portrait;
        private PaperSizeType _paperType = PaperSizeType.A4;
        private Color _gridColor = Color.Black;
        private Color _textColor = Color.Gray;
        private int _traceGrayLevel = 180;
        private string _infoFields = "姓名,班级,日期";
        private string _pageHeader = "田字格字帖";
        private string _fontName = "楷体";
        private bool _showPinyin = true;
        private bool _showSample = true;
        private int _strokeDemoCount = 0;
        private int _traceRepeatCount = 4;
        private int _practiceRows = 1;
        private bool _wordMode = true;
        private Color _pinyinColor = Color.Gray;
        private Color _sampleColor = Color.Black;

        public string InputText { get { return _inputText; } set { _inputText = value; } }
        public float CellSize { get { return _cellSize; } set { _cellSize = value; } }
        public GridType GridType { get { return _gridType; } set { _gridType = value; } }
        public CharMode CharMode { get { return _charMode; } set { _charMode = value; } }
        public PageOrientation Orientation { get { return _orientation; } set { _orientation = value; } }
        public PaperSizeType PaperType { get { return _paperType; } set { _paperType = value; } }
        public Color GridColor { get { return _gridColor; } set { _gridColor = value; } }
        public Color TextColor { get { return _textColor; } set { _textColor = value; } }
        public int TraceGrayLevel { get { return _traceGrayLevel; } set { _traceGrayLevel = value; } }
        public string InfoFields { get { return _infoFields; } set { _infoFields = value; } }
        public string PageHeader { get { return _pageHeader; } set { _pageHeader = value; } }
        public string FontName { get { return _fontName; } set { _fontName = value; } }
        public bool ShowPinyin { get { return _showPinyin; } set { _showPinyin = value; } }
        public bool ShowSample { get { return _showSample; } set { _showSample = value; } }
        public int StrokeDemoCount { get { return _strokeDemoCount; } set { _strokeDemoCount = value; } }
        public int TraceRepeatCount { get { return _traceRepeatCount; } set { _traceRepeatCount = value; } }
        public int PracticeRows { get { return _practiceRows; } set { _practiceRows = value; } }
        public bool WordMode { get { return _wordMode; } set { _wordMode = value; } }
        public Color PinyinColor { get { return _pinyinColor; } set { _pinyinColor = value; } }
        public Color SampleColor { get { return _sampleColor; } set { _sampleColor = value; } }

        public float GetPageWidth()
        {
            float w = (_paperType == PaperSizeType.A4) ? 210f : 215.9f;
            return (_orientation == PageOrientation.Landscape) ? GetPageHeightRaw() : w;
        }
        public float GetPageHeight()
        {
            float h = GetPageHeightRaw();
            return (_orientation == PageOrientation.Landscape) ?
                ((_paperType == PaperSizeType.A4) ? 210f : 215.9f) : h;
        }
        private float GetPageHeightRaw()
        {
            return (_paperType == PaperSizeType.A4) ? 297f : 279.4f;
        }

        public void SaveToFile(string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                f.Serialize(fs, this);
            }
            finally { if (fs != null) fs.Close(); }
        }
        public static WorksheetConfig LoadFromFile(string filePath)
        {
            FileStream fs = null;
            WorksheetConfig c = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                c = (WorksheetConfig)f.Deserialize(fs);
            }
            finally { if (fs != null) fs.Close(); }
            return c;
        }
    }


}
