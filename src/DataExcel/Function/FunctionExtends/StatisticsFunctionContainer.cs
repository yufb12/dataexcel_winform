using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using Feng.Utils;
using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;
using Feng.Script.Method;

namespace Feng.Excel.Script
{
    [Serializable]
    public class StatisticsFunctionContainer : DataExcelMethodContainer
    {
        public const string Function_Category = "StatisticsFunction";
        public const string Function_Description = "统计函数";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
 
        public StatisticsFunctionContainer()
        {
            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "SFSTDEVP";
            model.Description = @"标准偏差 SFSTDEVP(2,3,4,5)";
            model.Eg = @"SFSTDEVP(2,3,4,5)";
            model.Function = SFSTDEVP;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "SFSTDEV";
            model.Description = @"标准方差 SFSTDEV(2,3,4,5)";
            model.Eg = @"SFSTDEV(2,3,4,5)";
            model.Function = SFSTDEV;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SFAVG";
            model.Description = @"求平均 SFAVG(2,3,4,5)";
            model.Eg = @"SFAVG(2,3,4,5)";
            model.Function = SFAVG;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SFCOUNT";
            model.Description = @"求数量 SFCOUNT(2,3,4,5)";
            model.Eg = @"SFCOUNT(2,3,4,5)";
            model.Function = SFCOUNT;
            MethodList.Add(model);


        }
 

        public virtual object SFSTDEVP(params object[] args)
        {
            double[] values = null;
            if (args.Length > 2)
            {
                values = new double[args.Length - 1];
                for (int i = 1; i < args.Length; i++)
                {
                    double temp = base.GetDoubleValue(i, args);
                    values[i - 1] = temp;
                }
            }
            else if (args[1] is List<ICell>)
            {
                List<ICell> list = args[1] as List<ICell>;
                values = new double[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    double temp = Feng.Utils.ConvertHelper.ToDouble(list[i].Value);
                    values[i] = temp;
                }
            }
            else
            {
                return null;
            }
            double total = 0;
            double count = values.Length;
            double ave = 0;
            for (int i = 0; i < values.Length; i++)
            {
                double temp = values[i];
                total = total + temp;
            }
            ave = total / count;
            double Variances = 0;
            for (int i = 0; i < values.Length; i++)
            {
                Variances = Variances + Math.Pow((values[i] - ave), 2);
            }
            Variances = Variances / count;
            return Math.Sqrt(Variances);
        }
        public virtual object SFSTDEV(params object[] args)
        {
            double[] values = null;
            if (args.Length > 2)
            {
                values = new double[args.Length - 1];
                for (int i = 1; i < args.Length; i++)
                {
                    double temp = Feng.Utils.ConvertHelper.ToDouble(base.GetValue(i, args));
                    values[i - 1] = temp;
                }
            }
            else if (args[1] is List<ICell>)
            {
                List<ICell> list = args[1] as List<ICell>;
                values = new double[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    double temp = Feng.Utils.ConvertHelper.ToDouble(list[i].Value);
                    values[i] = temp;
                }
            }
            else
            {
                return null;
            }
            double total = 0;
            double count = values.Length;
            double ave = 0;
            for (int i = 0; i < values.Length; i++)
            {
                double temp = values[i];
                total = total + temp;
            }
            ave = total / count;
            double Variances = 0;
            for (int i = 0; i < values.Length; i++)
            {
                Variances = Variances + Math.Pow((values[i] - ave), 2);
            }
            Variances = Variances / (count - 1);
            return Math.Sqrt(Variances);
        }
        public virtual object SFAVG(params object[] args)
        {
            double[] values = null;
            if (args.Length > 2)
            {
                values = new double[args.Length - 1];
                for (int i = 1; i < args.Length; i++)
                {
                    double temp = Feng.Utils.ConvertHelper.ToDouble(base.GetValue(i, args));
                    values[i - 1] = temp;
                }
            }
            else if (args[1] is List<ICell>)
            {
                List<ICell> list = args[1] as List<ICell>;
                values = new double[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    double temp = Feng.Utils.ConvertHelper.ToDouble(list[i].Value);
                    values[i] = temp;
                }
            }
            else
            {
                return null;
            }
            double total = 0;
            double count = values.Length;
            double ave = 0;
            for (int i = 0; i < values.Length; i++)
            {
                double temp = values[i];
                total = total + temp;
            }
            ave = total / count;
            return ave;
        }
        public virtual object SFCOUNT(params object[] args)
        {
            if (args.Length > 2)
            {
                int count = args.Length - 1;
                return count;
            }
            else if (args[1] is List<ICell>)
            {
                List<ICell> list = args[1] as List<ICell>;
                return list.Count;
            }
            return null;
        }
         

    }
}
