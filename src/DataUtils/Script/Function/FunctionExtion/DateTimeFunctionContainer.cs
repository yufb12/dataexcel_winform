using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Globalization;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class DateTimeFunctionContainer : CBMethodContainer
    {
        public const string Function_Category = "DateTimeFunction";
        public const string Function_Description = "日期时间";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public DateTimeFunctionContainer()  
        {
            BaseMethod model = new BaseMethod();
            model.Name = "TimeNow";
            model.Description = "获取当前系统时间";
            model.Eg = @"TimeNow()";
            model.Function = TimeNow;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeYear";
            model.Description = "获取时间年份";
            model.Eg = @"TimeYear()";
            model.Function = TimeYear;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeMonth";
            model.Description = "获取时间月份";
            model.Eg = @"TimeMonth()";
            model.Function = TimeMonth;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeDay";
            model.Description = "获取时间天";
            model.Eg = @"TimeDay()";
            model.Function = TimeDay;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeHour";
            model.Description = "获取时间 时";
            model.Eg = @"TimeHour()";
            model.Function = TimeHour;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeMinute";
            model.Description = "获取时间 分";
            model.Eg = @"TimeMinute()";
            model.Function = TimeMinute;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeSecond";
            model.Description = "获取时间 秒";
            model.Eg = @"TimeSecond()";
            model.Function = TimeSecond;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeDayOfWeek";
            model.Description = "获取时间周几";
            model.Eg = @"TimeDayOfWeek()";
            model.Function = TimeDayOfWeek;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeDayOfYear";
            model.Description = "获取时间年内第几天";
            model.Eg = @"TimeDayOfYear()";
            model.Function = TimeDayOfYear;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeWeekOfYear";
            model.Description = "获取日期所在年的第几周";
            model.Eg = @"TimeWeekOfYear()";
            model.Function = TimeWeekOfYear;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeWeekOfMonth";
            model.Description = "获取日期所在月的第几周";
            model.Eg = @"TimeWeekOfMonth()";
            model.Function = TimeWeekOfMonth;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "TimeAddSeconds";
            model.Description = "给定时间上加个指定秒数";
            model.Eg = @"TimeAddSeconds()";
            model.Function = TimeAddSeconds;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "TimeAddMinutes";
            model.Description = "给定时间上加个指定分钟数";
            model.Eg = @"TimeAddMinutes()";
            model.Function = TimeAddMinutes;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "TimeAddHours";
            model.Description = "给定时间上加个指定小时数";
            model.Eg = @"TimeAddHours()";
            model.Function = TimeAddHours;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeAddDays";
            model.Description = "给定时间上加个指定天数";
            model.Eg = @"TimeAddDays()";
            model.Function = TimeAddDays;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeAddWeeks";
            model.Description = "给定时间上加个指定周数";
            model.Eg = @"TimeAddWeeks()";
            model.Function = TimeAddWeeks;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeAddMonths";
            model.Description = "给定时间上加个指定月数";
            model.Eg = @"TimeAddMonths()";
            model.Function = TimeAddMonths;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeAddYears";
            model.Description = "给定时间上加个指定年数";
            model.Eg = @"TimeAddYears()";
            model.Function = TimeAddYears;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeAddTicks";
            model.Description = "给定时间上加个指定周期";
            model.Eg = @"TimeAddTicks()";
            model.Function = TimeAddTicks;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeTicks";
            model.Description = "获取日期所在月的第几周";
            model.Eg = @"TimeTicks()";
            model.Function = TimeTicks;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeFormat";
            model.Description = "返回格式化日期字符";
            model.Eg = @"TimeFormat()";
            model.Function = TimeFormat;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeDate";
            model.Description = "获取日期部分";
            model.Eg = @"TimeDate()";
            model.Function = TimeDate;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeDateText";
            model.Description = "获取日期部分字符串 返回如:20150501";
            model.Eg = @"TimeDateText()";
            model.Function = TimeDateText;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeText";
            model.Description = "获取日期部分字符串 返回如:20150501130130";
            model.Eg = @"TimeText()";
            model.Function = TimeText;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeParse";
            model.Description = "字符串转换为时间";
            model.Eg = @"TimeParse()";
            model.Function = TimeParse;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeSameDay";
            model.Description = "比较两个时间的年，月，日是不是同一天";
            model.Eg = @"TimeSameDay()";
            model.Function = TimeSameDay;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeWeekDay";
            model.Description = "获取当前周的第几天 DateTimeWeekDay(Now,1)";
            model.Eg = @"TimeWeekDay()";
            model.Function = TimeWeekDay;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeSpanDays";
            model.Description = "两个时间之间相间的天数 TimeSpanDays(datetime1,datetime2)";
            model.Eg = @"TimeSpanDays(datetime1,datetime2)";
            model.Function = TimeSpanDays;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "TimeSpanHours";
            model.Description = "两个时间之间相间的小时数 TimeSpanHours(datetime1,datetime2)";
            model.Eg = @"TimeSpanHours(datetime1,datetime2)";
            model.Function = TimeSpanHours;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeSpanMinutes";
            model.Description = "两个时间之间相间的分钟数 TimeSpanMinutes(datetime1,datetime2)";
            model.Eg = @"TimeSpanMinutes(datetime1,datetime2)";
            model.Function = TimeSpanMinutes;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeSpanSeconds";
            model.Description = "两个时间之间相间的秒数 TimeSpanSeconds(datetime1,datetime2)";
            model.Eg = @"TimeSpanSeconds(datetime1,datetime2)";
            model.Function = TimeSpanSeconds;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "TimeSpanMilliseconds";
            model.Description = "两个时间之间相间的毫秒数 TimeSpanMilliseconds(datetime1,datetime2)";
            model.Eg = @"TimeSpanMilliseconds(datetime1,datetime2)";
            model.Function = TimeSpanMilliseconds;
            MethodList.Add(model);
        }

        public virtual object TimeNow(params object[] args)
        {
            return DateTime.Now;
        }
        public virtual object TimeYear(params object[] args)
        {
            if (args.Length < 2)
            {
                return DateTime.Now.Year;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return value.Year;
        }
        public virtual object TimeMonth(params object[] args)
        {
            if (args.Length < 2)
            {
                return DateTime.Now.Month;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return value.Month;
        }
        public virtual object TimeDay(params object[] args)
        {
            if (args.Length < 2)
            {
                return DateTime.Now.Day;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return value.Day;
        }
        public virtual object TimeHour(params object[] args)
        {

            if (args.Length < 2)
            {
                return DateTime.Now.Hour;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return value.Hour;
        }
        public virtual object TimeMinute(params object[] args)
        {
            if (args.Length < 2)
            {
                return DateTime.Now.Minute;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return value.Minute;
        }
        public virtual object TimeSecond(params object[] args)
        {

            if (args.Length < 2)
            {
                return DateTime.Now.Second;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return value.Second;
        }
        public virtual object TimeTicks(params object[] args)
        {

            if (args.Length < 2)
            {
                return DateTime.Now.Ticks;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return value.Ticks;
        }
        public virtual object TimeDayOfWeek(params object[] args)
        {
            if (args.Length < 2)
            {
                return DateTime.Now.DayOfWeek;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return (int)value.DayOfWeek;
        }
        public virtual object TimeDayOfYear(params object[] args)
        {
            if (args.Length < 2)
            {
                return DateTime.Now.DayOfYear;
            }
            DateTime value = base.GetDateTimeValue(1, args);
            return value.DayOfYear;
        }
        public virtual object TimeWeekOfMonth(params object[] args)
        {
            DateTime value = DateTime.Now;
            if (args.Length > 1)
            {
                value = base.GetDateTimeValue(1, args);
            }
            GregorianCalendar gc = new GregorianCalendar();
            int weekofmonth = 0;
            int dayInMonth = value.Day; 
            DateTime firstDay = value.AddDays(1 - value.Day); 
            int weekday = (int)firstDay.DayOfWeek == 0 ? 7 : (int)firstDay.DayOfWeek; 
            int firstWeekEndDay = 7 - (weekday - 1); 
            int diffday = dayInMonth - firstWeekEndDay;
            diffday = diffday > 0 ? diffday : 1;
            weekofmonth = ((diffday % 7) == 0
             ? (diffday / 7 - 1)
             : (diffday / 7)) + 1 + (dayInMonth > firstWeekEndDay ? 1 : 0); 
            return weekofmonth;
        }
        public virtual object TimeWeekOfYear(params object[] args)
        {
            DateTime value = DateTime.Now;
            if (args.Length > 1)
            {
                value = base.GetDateTimeValue(1, args);
            }
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return value.DayOfYear;
        }
        public virtual object TimeDate(params object[] args)
        {
            DateTime value = DateTime.Now;
            if (args.Length > 1)
            {
                value = base.GetDateTimeValue(1, args);
            }

            return value.Date;
        }
        public virtual object TimeDateText(params object[] args)
        {
            DateTime value = DateTime.Now;
            if (args.Length > 1)
            {
                value = base.GetDateTimeValue(1, args);
            }

            return value.Date.ToString("yyyyMMdd");
        }
        public virtual object TimeText(params object[] args)
        {
            DateTime value = DateTime.Now;
            if (args.Length > 1)
            {
                value = base.GetDateTimeValue(1, args);
            }

            return value.Date.ToString("yyyyMMddHHmmss");
        }
        public virtual object TimeFormat(params object[] args)
        {
            DateTime value = DateTime.Now;
            string format = "yyyy-MM-dd";
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                format = base.GetTextValue(2, args);
            }
            else
            {
                format = base.GetTextValue(1, args);
            }
            return value.ToString(format);
        }
        public virtual object TimeAddTicks(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddTicks(v);
        }
        public virtual object TimeAddYears(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddYears(v);
        }
        public virtual object TimeAddMonths(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddMonths(v);
        }
        public virtual object TimeAddWeeks(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddDays(v * 7);
        }
        public virtual object TimeAddDays(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddDays(v);
        }
        public virtual object TimeAddHours(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddHours(v);
        }
        public virtual object TimeAddMinutes(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddMinutes(v);
        }
        public virtual object TimeAddSeconds(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddSeconds(v);
        }
        public virtual object TimeAddMilliseconds(params object[] args)
        {
            DateTime value = DateTime.Now;
            int v = 1;
            if (args.Length > 2)
            {
                value = base.GetDateTimeValue(1, args);
                v = base.GetIntValue(2, args);
            }
            else
            {
                v = base.GetIntValue(1, args);
            }
            return value.AddMilliseconds(v);
        }
        public virtual object TimeParse(params object[] args)
        {
            string value = base.GetTextValue(1, args);
            DateTime dtout;
            if (DateTime.TryParse(value, out dtout))
            {
                return dtout;
            }
            if (args.Length > 2)
            {
                DateTime dt = base.GetDateTimeValue(2, args);
                return dt;
            }
            throw new Exception(value + " Not DateTime Format");
        }
        public virtual object TimeSameDay(params object[] args)
        {
            DateTime value1 = base.GetDateTimeValue(1, args);
            DateTime value2 = base.GetDateTimeValue(2, args);
            if (value1.Year == value2.Year && value1.Month == value2.Month && value1.Day == value2.Day)
            {
                return Feng.Utils.Constants.TRUE;
            }
            return Feng.Utils.Constants.FALSE;
        }
        public virtual object TimeWeekDay(params object[] args)
        {
            DateTime value = base.GetDateTimeValue(1, args);
            int weekday = base.GetIntValue(2, args);
            int len = (int)value.DayOfWeek;
            if (len == 0)
            {
                len = 7;
            }
            return value.AddDays(len * -1 + weekday);
        }
        public virtual object TimeSpanDays(params object[] args)
        {
            DateTime value1 = base.GetDateTimeValue(1, args);
            DateTime value2 = base.GetDateTimeValue(2, args);
            return (value2 - value1).TotalDays;
        }
        public virtual object TimeSpanHours(params object[] args)
        {
            DateTime value1 = base.GetDateTimeValue(1, args);
            DateTime value2 = base.GetDateTimeValue(2, args);
            return (value2 - value1).TotalHours;
        }
        public virtual object TimeSpanMinutes(params object[] args)
        {
            DateTime value1 = base.GetDateTimeValue(1, args);
            DateTime value2 = base.GetDateTimeValue(2, args);
            return (value2 - value1).TotalMinutes;
        }
        public virtual object TimeSpanSeconds(params object[] args)
        {
            DateTime value1 = base.GetDateTimeValue(1, args);
            DateTime value2 = base.GetDateTimeValue(2, args);
            return (value2 - value1).TotalSeconds;
        }
        public virtual object TimeSpanMilliseconds(params object[] args)
        {
            DateTime value1 = base.GetDateTimeValue(1, args);
            DateTime value2 = base.GetDateTimeValue(2, args);
            return (value2 - value1).TotalMilliseconds;
        }
    }
}
