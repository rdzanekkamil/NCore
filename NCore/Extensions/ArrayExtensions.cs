using System;
using System.Collections.Generic;

namespace NCore.Extensions
{
    public static class ArrayExtensions
    {
        public static void ClearAll<T>(this T[] o) => Array.Clear(o, 0, o.Length);

        public static void ClearAt<T>(this T[] o, int at) => Array.Clear(o, at, 1);

        public static List<T> ToValueList<T>(this T[,] o)
        {
            List<T> result = new List<T>();
            foreach (var item in o)
            {
                result.Add(item);
            }
            return result;
        }

        public static bool IndexExists<T>(this T[] o, int index)
            => index >= 0 && index < o.Length; 
    }
}