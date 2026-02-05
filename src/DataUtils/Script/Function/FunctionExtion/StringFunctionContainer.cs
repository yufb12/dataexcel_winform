using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class StringFunctionContainer : CBMethodContainer
    {
        public const string Function_Category = "CharacterFunction";
        public const string Function_Description = "字符函数";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public StringFunctionContainer() 
        {
            BaseMethod model = new BaseMethod();
            model.Name = "StrMerge";
            model.Description = "StrMerge";
            model.Eg = @"StrMerge(""Cell(""A5"")"")";
            model.Function = StrMerge;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "StrCombine";
            model.Description = "合并多个字符串";
            model.Eg = @"StrCombine(""A5"",""A5"",""A5"",""A5"",""A5"",""A5"")";
            model.Function = StrCombine;
            MethodList.Add(model);
            


            model = new BaseMethod();
            model.Name = "StrIsEmpty";
            model.Description = "判断字符串是否为空";
            model.Eg = @"StrIsEmpty(""Cell(""A5"")"")";
            model.Function = StrIsEmpty;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StrHas";
            model.Description = "判断第一个字符串是否包含后续字符串";
            model.Eg = @"StrHas(""Cell(""A5"")"",""第二个字符"")";
            model.Function = StrHas;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrRemove";
            model.Description = "StrRemove";
            model.Function = StrRemove;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrSub";
            model.Description = "StrSub";
            model.Function = StrSub;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrStartsWith";
            model.Description = "StrStartsWith";
            model.Function = StrStartsWith;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrSplit";
            model.Description = "StrSplit";
            model.Function = StrSplit;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrReplace";
            model.Description = "StrReplace";
            model.Function = StrReplace;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StrReplaceSpace";
            model.Description = "StrReplaceSpace";
            model.Function = StrReplaceSpace;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "StrToLower";
            model.Description = "转为小写";
            model.Eg = @"StrToLower(CELL(""A5""))";
            model.Function = this.StrToLower;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "StrToUpper";
            model.Description = "转为大写";
            model.Eg = @"StrToUpper(CELL(""A5""))";
            model.Function = this.StrToUpper;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrTrim";
            model.Description = "去除前缀，后缀字符";
            model.Eg = @"StrTrim(CELL(""A5""),""AAA"")";
            model.Function = this.StrTrim;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrTrimStart";
            model.Description = "去除前缀";
            model.Eg = @"StrTrimStart(CELL(""A5""),""AAA"")";
            model.Function = this.StrTrimStart;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StrTrimEnd";
            model.Description = "去除后缀字符";
            model.Eg = @"StrTrimEnd(CELL(""A5""),""AAA"")";
            model.Function = this.StrTrimEnd;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrFormat";
            model.Description = "格式化字显示";
            model.Eg = @"StrFormat(""{0:N1}"",CELL(""A5""))";
            model.Function = this.StrTrimEnd;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SplitByRegex";
            model.Description = "SplitByRegex";
            model.Function = StrSplitByRegex;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StrTrans";
            model.Description = "转义为脚本字符";
            model.Function = StrTrans;
            model.Eg = "StrTrans()";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrDEncrypt";
            model.Description = "解密文本 StrDEncrypt(text,pwd)";
            model.Function = StrDEncrypt;
            model.Eg = "StrDEncrypt(text,pwd)";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StrEncrypt";
            model.Description = "加密文本 StrEncrypt(text,pwd)";
            model.Function = StrEncrypt;
            model.Eg = "StrEncrypt(text,pwd)";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StrEmpty";
            model.Description = "返回空字符串 占位使用";
            model.Function = StrEmpty;
            model.Eg = @"StrEmpty(""名称"")";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "T";
            model.Description = "提示当前参数 并返回传入值";
            model.Function = T;
            model.Eg = @"T(""名称"",""张三"")";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "NULL";
            model.Description = "返回null 占位使用";
            model.Function = NULL;
            model.Eg = @"NULL(""名称"")";
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "ZERO";
            model.Description = "返回数字0 占位使用";
            model.Function = ZERO;
            model.Eg = @"ZERO(""名称"")";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Match";
            model.Description = "Match";
            model.Function = Match;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchText";
            model.Description = "MatchText";
            model.Eg = @"MatchText("""","""")";
            model.Function = MatchText;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchCapturesCount";
            model.Description = "捕获的集合数量 MatchCapturesCount";
            model.Eg = @"MatchCapturesCount(match)";
            model.Function = MatchCapturesCount;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchCapture";
            model.Description = "捕获的集合索引值 MatchCapture";
            model.Eg = @"MatchCapture(match,1)";
            model.Function = MatchCapture;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchGroupsCount";
            model.Description = "组的集合数量 MatchGroupsCount";
            model.Eg = @"MatchGroupsCount(match)";
            model.Function = MatchGroupsCount;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "MatchGroup";
            model.Description = "组的集合索引值 MatchGroup";
            model.Eg = @"MatchGroup(match,1)";
            model.Function = MatchGroup;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchInsert";
            model.Description = "MatchInsert";
            model.Function = MatchInsert;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchReplace";
            model.Description = "MatchReplace";
            model.Function = MatchReplace;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchAZaz";
            model.Description = "英文字符";
            model.Function = MatchAZaz;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchCnText";
            model.Description = "中文字符";
            model.Function = MatchCnText;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchEmail";
            model.Description = "邮箱";
            model.Function = MatchEmail;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchNRIC";
            model.Description = "身份证号码";
            model.Function = MatchNRIC;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchNumber";
            model.Description = "数字";
            model.Function = MatchNumber;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchPhoneNumber";
            model.Description = "电话号码";
            model.Function = MatchPhoneNumber;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchUrl";
            model.Description = "Url";
            model.Function = MatchUrl;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchDate";
            model.Description = "匹配日期";
            model.Function = MatchDate;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchTime";
            model.Description = "匹配时间";
            model.Function = MatchTime;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchReplaceAZaz";
            model.Description = "英文字符";
            model.Function = MatchReplaceAZaz;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchReplaceCnText";
            model.Description = "中文字符";
            model.Function = MatchReplaceCnText;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchReplaceEmail";
            model.Description = "邮箱";
            model.Function = MatchReplaceEmail;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchReplaceNRIC";
            model.Description = "身份证号码";
            model.Function = MatchReplaceNRIC;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchReplaceNumber";
            model.Description = "数字";
            model.Function = MatchReplaceNumber;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchReplacePhoneNumber";
            model.Description = "电话号码";
            model.Function = MatchReplacePhoneNumber;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchReplaceUrl";
            model.Description = "Url";
            model.Function = MatchReplaceUrl;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "MatchReplaceDate";
            model.Description = "匹配日期";
            model.Function = MatchReplaceDate;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "MatchReplaceTime";
            model.Description = "匹配时间";
            model.Function = MatchReplaceTime;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "StrPadLeft";
            model.Description = "向左填充指定数字的符号";
            model.Function = StrPadLeft;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrPadRight";
            model.Description = "向右填充指定数字的符号";
            model.Function = StrPadRight;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrStartsWith";
            model.Description = "是否以指定字符开始";
            model.Function = StrStartsWith;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StrEndsWith";
            model.Description = "是否以指定字符结束";
            model.Function = StrEndsWith;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrIndexOf";
            model.Description = "从开始查找指定字符，返回所在位置";
            model.Function = StrIndexOf;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrLastIndexOf";
            model.Description = "从结束查找指定字符，返回所在位置";
            model.Function = StrLastIndexOf;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "StrInsert";
            model.Description = "在指定位置插入字符";
            model.Function = StrInsert;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrLength";
            model.Description = "返回字符长度";
            model.Function = StrLength;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "StrToArray";
            model.Description = "字符转为数组";
            model.Function = StrToArray;
            MethodList.Add(model);
 
        }


        public virtual object StrTrans(params object[] args)
        {
            string str = base.GetTextValue(1, args);
            return Utils.TextHelper.Trope(str);
        }
        public virtual object StrIsEmpty(params object[] args)
        {
            string str = base.GetTextValue(1, args);
            return string.IsNullOrWhiteSpace(str);
        }
        public string StrMerge(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args);
            if (value1 == null && value2 == null)
            {
                return string.Empty;
            }
            if (value1 == null)
            {
                return value2.ToString();
            }
            if (value2 == null)
            {
                return value1.ToString();
            }
            return value1.ToString() + value2.ToString();
        }        
        public string StrCombine(params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < args.Length; i++)
            {
                sb.Append(Feng.Utils.ConvertHelper.ToString(args[i]));
            }
            return sb.ToString();
        }
        public string StrRemove(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            int value2 = base.GetIntValue(2, args);
            int value3 = base.GetIntValue(3, args);

            return value1.Remove(value2, value3);
        }
        public string StrSub(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            int value2 = base.GetIntValue(2, args);
            int value3 = base.GetIntValue(3, args);

            return value1.Substring(value2, value3);
        }
        public object StrHas(params object[] args)
        {
            string text = Feng.Utils.ConvertHelper.ToString(base.GetTextValue(1, args));
            for (int i = 2; i < args.Length; i++)
            {
                string text1 = Feng.Utils.ConvertHelper.ToString(base.GetTextValue(i, args));
                if (text.Contains(text1))
                {
                    return true;
                }
            }
            return false;
        }

        public string[] StrSplit(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);
            return value1.Split(new string[] { (string)value2 }, StringSplitOptions.RemoveEmptyEntries);
        }
        public string[] StrSplitByRegex(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);

            return System.Text.RegularExpressions.Regex.Split(value1, value2);
        }
        public string StrReplace(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);
            string value3 = base.GetTextValue(3, args);
            return value1.ToString().Replace(value2.ToString(), value3);
        }
        public string StrReplaceSpace(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string text = value1;
            for (int i = 2; i < args.Length; i++)
            {
                string value2 = base.GetTextValue(i, args);
                text = text.Replace(value2, "");
            }
            return text;
        }
        public string StrTrim(params object[] args)
        {
            string txt = base.GetTextValue(1, args);
            string txttrm = base.GetTextValue(2, args);
            return txt.Trim(txttrm.ToCharArray());
        }
        public string StrTrimStart(params object[] args)
        {
            string txt = base.GetTextValue(1, args);
            string txttrm = base.GetTextValue(2, args);
            return txt.TrimStart(txttrm.ToCharArray());
        }
        public string StrTrimEnd(params object[] args)
        {
            string txt = base.GetTextValue(1, args);
            string txttrm = base.GetTextValue(2, args);
            return txt.TrimEnd(txttrm.ToCharArray());
        }
        public string StrToLower(params object[] args)
        {
            string txt = base.GetTextValue(1, args);
            return txt.ToLower();
        }
        public string StrToUpper(params object[] args)
        {
            string txt = base.GetTextValue(1, args);
            return txt.ToUpper();
        }
        public string StrFormat(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            object[] ars = new object[args.Length - 2];
            for (int i = 0; i < args.Length; i++)
            {
                ars[i] = args[i + 2];
            }
            return string.Format(text, ars);
        }
        public string StrPadLeft(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            int len = base.GetIntValue(2, args);
            string textpad = base.GetTextValue(3, args);
            return text.PadLeft(len, textpad[0]);
        }
        public string StrPadRight(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            int len = base.GetIntValue(2, args);
            string textpad = base.GetTextValue(3, args);
            return text.PadRight(len, textpad[0]);
        }
        public object StrStartsWith(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            string textpad = base.GetTextValue(2, args);
            return text.StartsWith(textpad);
        }
        public object StrEndsWith(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            string textpad = base.GetTextValue(2, args);
            return text.EndsWith(textpad);
        }
        public object StrIndexOf(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            string textpad = base.GetTextValue(2, args);
            return text.IndexOf(textpad);
        }
        public object StrLastIndexOf(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            string textpad = base.GetTextValue(2, args);
            return text.LastIndexOf(textpad);
        }
        public object StrInsert(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            int len = base.GetIntValue(2, args);
            string textpad = base.GetTextValue(3, args);
            return text.Insert(len, textpad);
        }
        public object StrLength(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            return text.Length;
        }
        public object StrToArray(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            return text.ToCharArray();
        }
        public object StrDEncrypt(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            string pwd = base.GetTextValue(2, args);
            return Feng.IO.DEncrypt.Decrypt(text, pwd);
        }
        public object StrEncrypt(params object[] args)
        {
            string text = base.GetTextValue(1, args);
            string pwd = base.GetTextValue(2, args);
            return Feng.IO.DEncrypt.Encrypt(text, pwd);
        }
        public object StrEmpty(params object[] args)
        {
            return string.Empty;
        }

        public object T(params object[] args)
        {
            return args[2];
        }
        public object NULL(params object[] args)
        {
            return string.Empty;
        }
        public object ZERO(params object[] args)
        {
            return 0;
        }

        public object Match(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match;
            }
            return null;
        }
        public object MatchNext(params object[] args)
        {
            System.Text.RegularExpressions.Match match = base.GetArgIndex(1, args) 
                as System.Text.RegularExpressions.Match;

            return match.NextMatch();
        }
        public object MatchCapturesCount(params object[] args)
        {
            CaptureCollection captureCollection = null;
            System.Text.RegularExpressions.Match match = base.GetArgIndex(1, args)
                as System.Text.RegularExpressions.Match;
            if (match != null)
            {
                captureCollection = match.Captures;
            }
            else
            {
                System.Text.RegularExpressions.Group group = base.GetArgIndex(1, args)
        as System.Text.RegularExpressions.Group;
                if (match != null)
                {
                    captureCollection = group.Captures;
                }
            }
            int i = base.GetIntValue(2, args);
            return captureCollection.Count;
        }
        public object MatchCapture(params object[] args)
        {
            CaptureCollection captureCollection = null;
            System.Text.RegularExpressions.Match match = base.GetArgIndex(1, args)
                as System.Text.RegularExpressions.Match;
            if (match != null)
            {
                captureCollection = match.Captures;
            }
            else
            {
                System.Text.RegularExpressions.Group group = base.GetArgIndex(1, args)
        as System.Text.RegularExpressions.Group;
                if (match != null)
                {
                    captureCollection = group.Captures;
                }
            }
            int i = base.GetIntValue(2, args);
            return captureCollection[i].Value;
        }
        public object MatchGroupsCount(params object[] args)
        {
            System.Text.RegularExpressions.Match match = base.GetArgIndex(1, args)
                as System.Text.RegularExpressions.Match;
            return match.Groups.Count;
        }
        public object MatchGroup(params object[] args)
        {
            System.Text.RegularExpressions.Match match = base.GetArgIndex(1, args)
                as System.Text.RegularExpressions.Match;
            int i = base.GetIntValue(2, args);
            return match.Groups[i].Value;
        }
        public string MatchText(params object[] args)
        {
            System.Text.RegularExpressions.Match match = base.GetArgIndex(1, args)
     as System.Text.RegularExpressions.Match;
            if (match != null)
            {
                return match.Value;
            }
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);
            match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchNumber(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"[+-]?\d+[\.]?\d*";
            System.Text.RegularExpressions.MatchCollection match = System.Text.RegularExpressions.Regex.Matches(value1, value2);
            if (match.Count > 0)
            {
                return match[0].Value;
            }
            return string.Empty;
        }
        public string MatchCnText(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"[\u4e00-\u9fa5]+";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchDate(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"(\d{2,4})-(\d{1,2})-(\d{1,2})";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchAZaz(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"[A-Za-z]+";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchEmail(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchUrl(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchPhoneNumber(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"0{0,1}(13[1-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchNRIC(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"\d{18,}|\d{15,}";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchTime(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"(([1-9]{1})|([0-1][0-9])|([1-2][0-3])):([0-5][0-9])";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(value1, value2);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
        public string MatchInsert(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);
            string value3 = base.GetTextValue(3, args);
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(value1, value2);
            StringBuilder sb = new StringBuilder();
            int index = 0;
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                sb.Append(value1.Substring(index, match.Index));
                index = match.Index + match.Length;
                sb.Append(value3);
            }
            sb.Append(value1.Substring(index));
            return sb.ToString();
        }
        public string MatchReplace(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = base.GetTextValue(2, args);
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplaceNumber(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"\d{15}|\d{18}";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplaceCnText(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"[\u4e00-\u9fa5]";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplaceDate(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"(\d{2,4})-(\d{1,2})-(\d{1,2})";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplaceAZaz(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"[A-Za-z]+";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplaceEmail(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplaceUrl(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplacePhoneNumber(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"0{0,1}(13[1-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplaceNRIC(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"\d{18,}|\d{15,}";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }
        public string MatchReplaceTime(params object[] args)
        {
            string value1 = base.GetTextValue(1, args);
            string value2 = @"(([1-9]{1})|([0-1][0-9])|([1-2][0-3])):([0-5][0-9])";
            string value3 = base.GetTextValue(3, args);
            string res = System.Text.RegularExpressions.Regex.Replace(value1, value2, value3);
            return res;
        }

    }
}
