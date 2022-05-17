using System;
using System.Globalization;

namespace NCore.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertTimeToUtc(this DateTime dateTime) => TimeZoneInfo.ConvertTimeToUtc(dateTime);

        public static DateTime ConvertTimeToUtc(this DateTime dateTime, TimeZoneInfo sourceTimeZone) 
            => TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone);

        public static DateTime ConvertTimeFromUtc(this DateTime dateTime, TimeZoneInfo destinationTimeZone)
            => TimeZoneInfo.ConvertTimeFromUtc(dateTime, destinationTimeZone);
        
        public static int Age(this DateTime o)
        {
            if (DateTime.Today.Month < o.Month ||
                DateTime.Today.Month == o.Month &&
                DateTime.Today.Day < o.Day)
            {
                return DateTime.Today.Year - o.Year - 1;
            }
            return DateTime.Today.Year - o.Year;
        }

        public static DateTime EndOfDay(this DateTime o)
            => new DateTime(o.Year, o.Month, o.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        
        public static DateTime EndOfMonth(this DateTime o)
            => new DateTime(o.Year, o.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));

        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            DateTime end = dt;
            DayOfWeek endDayOfWeek = startDayOfWeek - 1;
            if (endDayOfWeek < 0) endDayOfWeek = DayOfWeek.Saturday;
            if (end.DayOfWeek != endDayOfWeek)
            {
                if (endDayOfWeek < end.DayOfWeek)
                    end = end.AddDays(7 - (end.DayOfWeek - endDayOfWeek));
                else
                    end = end.AddDays(endDayOfWeek - end.DayOfWeek);
            }
            return new DateTime(end.Year, end.Month, end.Day, 23, 59, 59, 999);
        }

        public static DateTime EndOfYear(this DateTime o)
            => new DateTime(o.Year, 1, 1).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));

        public static DateTime FirstDayOfWeek(this DateTime o)
            => new DateTime(o.Year, o.Month, o.Day).AddDays(-(int) o.DayOfWeek);

        public static bool IsAfternoon(this DateTime o)
            => o.TimeOfDay >= new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;

        public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
            => (date.Date == dateToCompare.Date);

        public static bool IsFuture(this DateTime o) => o > DateTime.Now;

        public static bool IsMorning(this DateTime o) => o.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;

        public static bool IsNow(this DateTime o) => o == DateTime.Now;

        public static bool IsPast(this DateTime o) => o < DateTime.Now;

        public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare) => (time.TimeOfDay == timeToCompare.TimeOfDay);

        public static bool IsToday(this DateTime o) => o.Date == DateTime.Today;

        public static bool IsWeekDay(this DateTime o) 
            => !(o.DayOfWeek == DayOfWeek.Saturday || o.DayOfWeek == DayOfWeek.Sunday);

        public static bool IsWeekendDay(this DateTime o)
            => (o.DayOfWeek == DayOfWeek.Saturday || o.DayOfWeek == DayOfWeek.Sunday);

        public static DateTime LastDayOfWeek(this DateTime o)
            => new DateTime(o.Year, o.Month, o.Day).AddDays(6 - (int) o.DayOfWeek);

        public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
            => new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);

        public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
            => SetTime(current, hour, minute, second, 0);

        public static DateTime SetTime(this DateTime current, int hour, int minute)
            => SetTime(current, hour, minute, 0, 0);
        
        public static DateTime SetTime(this DateTime current, int hour) => SetTime(current, hour, 0, 0, 0);

        public static DateTime StartOfDay(this DateTime o) => new DateTime(o.Year, o.Month, o.Day);

        public static DateTime StartOfMonth(this DateTime o) => new DateTime(o.Year, o.Month, 1);

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            var start = new DateTime(dt.Year, dt.Month, dt.Day);
            if (start.DayOfWeek != startDayOfWeek)
            {
                int d = startDayOfWeek - start.DayOfWeek;
                if (startDayOfWeek <= start.DayOfWeek) return start.AddDays(d);
                return start.AddDays(-7 + d);
            }
            return start;
        }

        public static DateTime StartOfYear(this DateTime o) => new DateTime(o.Year, 1, 1);

        public static TimeSpan ToEpochTimeSpan(this DateTime o) => o.Subtract(new DateTime(1970, 1, 1));

        public static DateTime Tomorrow(this DateTime o) => o.AddDays(1);

        public static DateTime Yesterday(this DateTime o) => o.AddDays(-1);

        public static string ToFullDateTimeString(this DateTime @this) => @this.ToString("F", DateTimeFormatInfo.CurrentInfo);

        public static string ToFullDateTimeString(this DateTime @this, string culture) => @this.ToString("F", new CultureInfo(culture));

        public static string ToFullDateTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("F", culture);

        public static string ToLongDateShortTimeString(this DateTime @this) => @this.ToString("f", DateTimeFormatInfo.CurrentInfo);

        public static string ToLongDateShortTimeString(this DateTime @this, string culture) => @this.ToString("f", new CultureInfo(culture));

        public static string ToLongDateShortTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("f", culture);

        public static string ToLongDateString(this DateTime @this) => @this.ToString("D", DateTimeFormatInfo.CurrentInfo);

        public static string ToLongDateString(this DateTime @this, string culture) => @this.ToString("D", new CultureInfo(culture));

        public static string ToLongDateString(this DateTime @this, CultureInfo culture) => @this.ToString("D", culture);

        public static string ToLongTimeString(this DateTime @this) => @this.ToString("T", DateTimeFormatInfo.CurrentInfo);

        public static string ToLongTimeString(this DateTime @this, string culture) => @this.ToString("T", new CultureInfo(culture));

        public static string ToLongTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("T", culture);

        public static string ToMonthDayString(this DateTime @this) => @this.ToString("m", DateTimeFormatInfo.CurrentInfo);

        public static string ToMonthDayString(this DateTime @this, string culture) => @this.ToString("m", new CultureInfo(culture));

        public static string ToMonthDayString(this DateTime @this, CultureInfo culture) => @this.ToString("m", culture);

        public static string ToShortDateLongTimeString(this DateTime @this) => @this.ToString("G", DateTimeFormatInfo.CurrentInfo);

        public static string ToShortDateLongTimeString(this DateTime @this, string culture) => @this.ToString("G", new CultureInfo(culture));

        public static string ToShortDateLongTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("G", culture);

        public static string ToShortDateString(this DateTime @this) => @this.ToString("d", DateTimeFormatInfo.CurrentInfo);

        public static string ToShortDateString(this DateTime @this, string culture) => @this.ToString("d", new CultureInfo(culture));

        public static string ToShortDateString(this DateTime @this, CultureInfo culture) => @this.ToString("d", culture);

        public static string ToShortDateTimeString(this DateTime @this) => @this.ToString("g", DateTimeFormatInfo.CurrentInfo);

        public static string ToShortDateTimeString(this DateTime @this, string culture) => @this.ToString("g", new CultureInfo(culture));

        public static string ToShortDateTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("g", culture);

        public static string ToShortTimeString(this DateTime @this) => @this.ToString("t", DateTimeFormatInfo.CurrentInfo);

        public static string ToShortTimeString(this DateTime @this, string culture) => @this.ToString("t", new CultureInfo(culture));

        public static string ToShortTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("t", culture);

        public static string ToSortableDateTimeString(this DateTime @this) => @this.ToString("s", DateTimeFormatInfo.CurrentInfo);

        public static string ToSortableDateTimeString(this DateTime @this, string culture) => @this.ToString("s", new CultureInfo(culture));

        public static string ToSortableDateTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("s", culture);

        public static string ToUniversalSortableDateTimeString(this DateTime @this) => @this.ToString("u", DateTimeFormatInfo.CurrentInfo);

        public static string ToUniversalSortableDateTimeString(this DateTime @this, string culture) 
            => @this.ToString("u", new CultureInfo(culture));

        public static string ToUniversalSortableDateTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("u", culture);

        public static string ToUniversalSortableLongDateTimeString(this DateTime @this) => @this.ToString("U", DateTimeFormatInfo.CurrentInfo);

        public static string ToUniversalSortableLongDateTimeString(this DateTime @this, string culture) => 
            @this.ToString("U", new CultureInfo(culture));

        public static string ToUniversalSortableLongDateTimeString(this DateTime @this, CultureInfo culture) => @this.ToString("U", culture);

        public static string ToYearMonthString(this DateTime @this) => @this.ToString("y", DateTimeFormatInfo.CurrentInfo);

        public static string ToYearMonthString(this DateTime @this, string culture) => @this.ToString("y", new CultureInfo(culture));

        public static string ToYearMonthString(this DateTime @this, CultureInfo culture) => @this.ToString("y", culture);
    }
}