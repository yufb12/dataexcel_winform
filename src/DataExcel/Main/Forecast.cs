//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Feng.Excel.Script
//{
//    //[Serializable]
//    //public class Forecast
//    //{
//    //    //[Serializable]
//    //    //public class pare
//    //    //{
//    //    //    public int index;
//    //    //    public decimal value;
//    //    //    public override string ToString()
//    //    //    {
//    //    //        return string.Format("{0},{1}", index, value);
//    //    //    }
//    //    //}

//    //    //public static int GetIndex(List<pare> list, int index)
//    //    //{
//    //    //    int i = 0;
//    //    //    foreach (pare p in list)
//    //    //    {

//    //    //        if (index == p.index)
//    //    //        {
//    //    //            break;
//    //    //        }
//    //    //        i++;
//    //    //    }
//    //    //    return i;
//    //    //}

//    //    //public static decimal GetValue(int startindex, decimal[] ds, int endindex)
//    //    //{
//    //    //    List<pare> list = new List<pare>();
//    //    //    for (int i = 0; i < ds.Length; i++)
//    //    //    {
//    //    //        list.Add(new pare()
//    //    //        {
//    //    //            index = startindex + i,
//    //    //            value = ds[i]
//    //    //        }
//    //    //        );
//    //    //    }
//    //    //    return GetValue(list, endindex);
//    //    //}

//    //    //public static decimal GetValue(List<pare> list, int endindex)
//    //    //{
//    //    //    int index = (list.Count - 1) * (int)Math.Ceiling(list[0].index / (decimal)(list.Count - 1));
//    //    //    List<decimal> listd = new List<decimal>();
//    //    //    int inx = GetIndex(list, index);
//    //    //    List<pare> listtemp = new List<pare>();
//    //    //    for (int i = inx; i < list.Count - 1; i++)
//    //    //    {
//    //    //        listd.Add(list[i + 1].value - list[i].value);
//    //    //    }
//    //    //    for (int i = inx - 1; i >= 0; i--)
//    //    //    {
//    //    //        listd.Add(list[i + 1].value - list[i].value);
//    //    //    }
//    //    //    for (int i = 1; i < listd.Count; i++)
//    //    //    {
//    //    //        listd[i] = listd[i] + listd[i - 1];
//    //    //    }
//    //    //    decimal chae = list[list.Count - 1].value - list[0].value;
//    //    //    pare first = new pare();
//    //    //    if (list[0].index != 0)
//    //    //    {
//    //    //        int index2 = GetIndex(list, index);
//    //    //        decimal d = list[index2].value;
//    //    //        decimal f = d - index / (list.Count - 1) * chae;
//    //    //        first.value = f;
//    //    //    }
//    //    //    else
//    //    //    {
//    //    //        first = list[0];
//    //    //    }
//    //    //    int eindex = (list.Count - 1) * (int)Math.Floor(endindex / (decimal)(list.Count - 1));
//    //    //    decimal dr = eindex / (list.Count - 1) * chae + first.value;
//    //    //    int ic = endindex - eindex;
//    //    //    decimal drr = ic > 0 ? dr + listd[ic - 1] : dr;
//    //    //    return drr;
//    //    //}
//    //}
//}
