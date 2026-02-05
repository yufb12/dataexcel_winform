using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Styles
{
    [Serializable]
    public class LineAmount
    {
        public static readonly float[] Line2 = new float[] { 0.0f, 0.33333f, 0.66667f, 1.0f };
        public static readonly float[] Line3 = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };

        public static float[] GetLinePosition(int count)
        {
            float[] Line2 = new float[] { 0.0f, 0.33f, 0.67f, 1.0f };
            return Line2;
        }
    }
}
