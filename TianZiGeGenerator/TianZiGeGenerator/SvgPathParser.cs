using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace TianZiGeGenerator
{
    public static class SvgPathParser
    {
        public static GraphicsPath Parse(string pathData, float scale, float offsetX, float offsetY)
        {
            GraphicsPath path = new GraphicsPath();
            if (string.IsNullOrEmpty(pathData)) return path;

            int pos = 0;
            float cx = 0, cy = 0, sx = 0, sy = 0;
            char last = ' ';

            while (pos < pathData.Length)
            {
                while (pos < pathData.Length && char.IsWhiteSpace(pathData[pos])) pos++;
                if (pos >= pathData.Length) break;

                char cmd = pathData[pos];
                bool rel = false;
                if (char.IsLetter(cmd)) { last = cmd; pos++; }
                else cmd = last;
                if (cmd >= 'a' && cmd <= 'z') rel = true;

                switch (char.ToLower(cmd))
                {
                    case 'm':
                        {
                            float x = RN(pathData, ref pos), y = RN(pathData, ref pos);
                            if (rel) { x += cx; y += cy; }
                            cx = x; cy = y; sx = x; sy = y;
                            path.StartFigure();
                            while (pos < pathData.Length && (char.IsDigit(pathData[pos]) || pathData[pos] == '-' || pathData[pos] == '.'))
                            {
                                float lx = RN(pathData, ref pos), ly = RN(pathData, ref pos);
                                if (rel) { lx += cx; ly += cy; }
                                path.AddLine(cx * scale + offsetX, cy * scale + offsetY, lx * scale + offsetX, ly * scale + offsetY);
                                cx = lx; cy = ly;
                            }
                        }
                        break;
                    case 'l':
                        {
                            float x = RN(pathData, ref pos), y = RN(pathData, ref pos);
                            if (rel) { x += cx; y += cy; }
                            path.AddLine(cx * scale + offsetX, cy * scale + offsetY, x * scale + offsetX, y * scale + offsetY);
                            cx = x; cy = y;
                        }
                        break;
                    case 'h':
                        {
                            float x = RN(pathData, ref pos);
                            if (rel) x += cx;
                            path.AddLine(cx * scale + offsetX, cy * scale + offsetY, x * scale + offsetX, cy * scale + offsetY);
                            cx = x;
                        }
                        break;
                    case 'v':
                        {
                            float y = RN(pathData, ref pos);
                            if (rel) y += cy;
                            path.AddLine(cx * scale + offsetX, cy * scale + offsetY, cx * scale + offsetX, y * scale + offsetY);
                            cy = y;
                        }
                        break;
                    case 'c':
                        {
                            float x1 = RN(pathData, ref pos), y1 = RN(pathData, ref pos);
                            float x2 = RN(pathData, ref pos), y2 = RN(pathData, ref pos);
                            float x = RN(pathData, ref pos), y = RN(pathData, ref pos);
                            if (rel) { x1 += cx; y1 += cy; x2 += cx; y2 += cy; x += cx; y += cy; }
                            path.AddBezier(cx * scale + offsetX, cy * scale + offsetY,
                                x1 * scale + offsetX, y1 * scale + offsetY,
                                x2 * scale + offsetX, y2 * scale + offsetY,
                                x * scale + offsetX, y * scale + offsetY);
                            cx = x; cy = y;
                        }
                        break;
                    case 'q':
                        {
                            float x1 = RN(pathData, ref pos), y1 = RN(pathData, ref pos);
                            float x = RN(pathData, ref pos), y = RN(pathData, ref pos);
                            if (rel) { x1 += cx; y1 += cy; x += cx; y += cy; }
                            float cx1 = cx + 2f * (x1 - cx) / 3f, cy1 = cy + 2f * (y1 - cy) / 3f;
                            float cx2 = x + 2f * (x1 - x) / 3f, cy2 = y + 2f * (y1 - y) / 3f;
                            path.AddBezier(cx * scale + offsetX, cy * scale + offsetY,
                                cx1 * scale + offsetX, cy1 * scale + offsetY,
                                cx2 * scale + offsetX, cy2 * scale + offsetY,
                                x * scale + offsetX, y * scale + offsetY);
                            cx = x; cy = y;
                        }
                        break;
                    case 'z':
                        path.CloseFigure();
                        cx = sx; cy = sy;
                        break;
                    default:
                        pos++;
                        break;
                }
            }
            return path;
        }

        private static float RN(string data, ref int pos)
        {
            while (pos < data.Length && (char.IsWhiteSpace(data[pos]) || data[pos] == ',')) pos++;
            int s = pos;
            if (pos < data.Length && (data[pos] == '-' || data[pos] == '+')) pos++;
            while (pos < data.Length && (char.IsDigit(data[pos]) || data[pos] == '.')) pos++;
            if (pos < data.Length && (data[pos] == 'e' || data[pos] == 'E'))
            {
                pos++;
                if (pos < data.Length && (data[pos] == '-' || data[pos] == '+')) pos++;
                while (pos < data.Length && char.IsDigit(data[pos])) pos++;
            }
            if (s == pos) return 0;
            float r;
            float.TryParse(data.Substring(s, pos - s),
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out r);
            return r;
        }
    }


}
