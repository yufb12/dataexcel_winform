using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Drawing;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class StyleFunctionContainer : CBMethodContainer
    {
        public const string Function_Category = "StyleFunction";
        public const string Function_Description = "样式函数";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public StyleFunctionContainer()
        {
            BaseMethod model = new BaseMethod();
            model.Name = "StyleFont";
            model.Description = "获取字体";
            model.Function = StyleFont;
            model.Eg = @"var font=StyleFont(""宋体"",12,bold,italic,underline,strikeout)";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StyleFontName";
            model.Description = "获取或设置字体名称";
            model.Function = StyleFontName;
            model.Eg = @"var font=StyleFontName(font,""宋体"")";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StyleFontBold";
            model.Description = "加粗字体";
            model.Function = StyleFontBold;
            model.Eg = @"var font=StyleFontBold(font,bold)";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StyleColor";
            model.Description = "获取颜色";
            model.Function = StyleColor;
            model.Eg = @"var color=StyleColor(alpha, red,green,blue)";
            MethodList.Add(model);
        }

        public object StyleFont(params object[] args)
        {
            string familyName = base.GetTextValue(1, args);            //     新 System.Drawing.Font 的 System.Drawing.FontFamily 的字符串表示形式。
            float emSize = base.GetSingleValue(2, args);            //     新字体的全身大小（采用 unit 参数指定的单位）。
            bool Bold = base.GetBooleanValue(3, args); 
            bool Italic = base.GetBooleanValue(4, args);
            bool Underline = base.GetBooleanValue(5, args);
            bool Strikeout = base.GetBooleanValue(6, args);
            FontStyle fs = FontStyle.Regular;
            if (Bold)
            {
                fs = fs | FontStyle.Bold;
            }
            if (Underline)
            {
                fs = fs | FontStyle.Underline;
            }
            if (Strikeout)
            {
                fs = fs | FontStyle.Strikeout;
            }
            if (Italic)
            {
                fs = fs | FontStyle.Italic;
            }
            Font font = new Font(familyName, emSize, fs);
            return font;
        }

        public object StyleFontName(params object[] args)
        {
            Font font1 = base.GetArgIndex(1, args) as Font;
            if (args.Length > 2)
            {
                string name = base.GetTextValue(2, args);
                Font font = new Font(name, font1.Size);
                return font;
            }

            return font1.FontFamily.Name;
        }

        public object StyleFontBold(params object[] args)
        {
            Font font1 = base.GetArgIndex(1, args) as Font;
            bool Bold = base.GetBooleanValue(2, args);
            FontStyle fs = font1.Style;
            if (Bold)
            {
                fs = fs | FontStyle.Bold;
            }
            else
            {
                fs = fs & FontStyle.Bold;
            }
            Font font = new Font(font1.FontFamily, font1.Size, fs);
            return font;
        }

        public object StyleColor(params object[] args)
        {
            int a = base.GetIntValue(1, args);
            int r = base.GetIntValue(2, args);
            int g = base.GetIntValue(3, args);
            int b = base.GetIntValue(4, args);
            Color color = Color.FromArgb(a, r, g, b);
            return color;
        }

    }
}
