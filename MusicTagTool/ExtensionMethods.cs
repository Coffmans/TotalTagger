using System;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Except<T>(this IEnumerable<T> ienum, T o)
        {
            List<T> list = new List<T>();
            list.Add(o);
            return ienum.Except(list);
        }
        public static int IndexOf(this object[] array, object element)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(element))
                {
                    return i;
                }
            }
            return -1;
        } 

        public static string ToTime(this int timeint)
        {
            if (timeint < 0) return "00:00";
            string ret = "";
            int temp;
            if (timeint >= 3600)
            {
                temp = timeint / 3600;
                if (temp < 10) ret += "0";
                ret += temp + ":";
                timeint -= temp * 3600;
            }
            temp = timeint / 60;
            if (temp < 10) ret += "0";
            ret += temp + ":";
            timeint -= temp * 60;
            if (timeint < 10) ret += "0";
            ret += timeint;
            return ret;
        }

        public static string ToTime(this double timedb)
        {
            return ToTime((int)timedb);
        }
    }
}
