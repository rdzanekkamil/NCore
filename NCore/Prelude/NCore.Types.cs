namespace NCore
{
    public static partial class NCore
    {
        public static DateTime DTNow => DateTime.Now;
        public static DateTimeOffset DTOffsetNow => DateTimeOffset.Now;
        public static TimeSpan TNow => DateTime.Now.TimeOfDay;
        public static TimeSpan TOffsetNow => DateTimeOffset.Now.TimeOfDay;
        public static DateOnly DNow => DateOnly.FromDateTime(DateTime.Now);

        public static FileInfo OfFileInfo(string value) => new FileInfo(value);
        public static DirectoryInfo OfDictionaryInfo(string value) => new DirectoryInfo(value);
    }
}