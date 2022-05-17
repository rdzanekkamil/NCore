using System;
using System.Collections.Generic;
using System.Text;

namespace NCore.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendIf<T>(this StringBuilder o, Func<T, bool> predicate, params T[] values)
        {
            foreach (var value in values)
            {
                if (predicate(value)) o.Append(value);
            }
            return o;
        }

        public static StringBuilder AppendJoin<T>(this StringBuilder o, string separator, IEnumerable<T> values)
        {
            o.Append(string.Join(separator, values));
            return o;
        }

        public static StringBuilder AppendJoin<T>(this StringBuilder o, string separator, params T[] values)
        {
            o.Append(string.Join(separator, values));
            return o;
        }

        public static StringBuilder AppendLineFormat(this StringBuilder o, string format, params object[] args)
        {
            o.AppendLine(string.Format(format, args));
            return o;
        }

        public static StringBuilder AppendLineFormat(this StringBuilder o, string format, List<IEnumerable<object>> args)
        {
            o.AppendLine(string.Format(format, args));
            return o;
        }

        public static StringBuilder AppendLineIf<T>(this StringBuilder o, Func<T, bool> predicate, params T[] values)
        {
            foreach (var value in values)
            {
                if (predicate(value)) o.AppendLine(value.ToString());
            }
            return o;
        }

        public static StringBuilder AppendLineJoin<T>(this StringBuilder o, string separator, IEnumerable<T> values)
        {
            o.AppendLine(string.Join(separator, values));
            return o;
        }

        public static StringBuilder AppendLineJoin(this StringBuilder o, string separator, params object[] values)
        {
            o.AppendLine(string.Join(separator, values));
            return o;
        }

        public static string Substring(this StringBuilder o, int startIndex) => o.ToString(startIndex, o.Length - startIndex);

        public static string Substring(this StringBuilder o, int startIndex, int length) => o.ToString(startIndex, length);
    }
}