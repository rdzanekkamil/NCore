using System;

namespace NCore.Extensions
{
    public static class TimeSpanExtensions
    {
        public static DateTime Ago(this TimeSpan o) => DateTime.Now.Subtract(o);

        public static DateTime FromNow(this TimeSpan o) => DateTime.Now.Add(o);

        public static DateTime UtcAgo(this TimeSpan o) => DateTime.UtcNow.Subtract(o);

        public static DateTime UtcFromNow(this TimeSpan o) => DateTime.UtcNow.Add(o);
    }
}