using NCore.Delegates;
using NCore.Monadas;

namespace NCore
{
    public static partial class NCore
    {
        public static NOptional<T> OfConvert<T>(object value)
        {
            if (value == null) return OfEmptyOptional<T>();
            return OfTryApplay<NOptional<T>>(() => OfNullable((T)Convert.ChangeType(value, typeof(T))))
                .OrElse(OfEmptyOptional<T>());
        }

        public static NOptional<T> OfParse<T>(TryParse<T> parse, string value) 
            => parse(value, out T result)
                ? OfNullable(result)
                : OfEmptyOptional<T>();

        public static NOptional<T> OfParseIgnoreCase<T>(TryParseIgnoreCase<T> parse, bool ignoreCase, string value) 
            => parse(value, ignoreCase, out T result)
                ? OfNullable(result)
                : OfEmptyOptional<T>();

        public static NOptional<long> ParseLong(string value) => OfParse<long>(long.TryParse, value);
        public static NOptional<ulong> ParseULong(string value) => OfParse<ulong>(ulong.TryParse, value);
        public static NOptional<int> ParseInt(string value) => OfParse<int>(int.TryParse, value);
        public static NOptional<uint> ParseUInt(string value) => OfParse<uint>(uint.TryParse, value);
        public static NOptional<short> ParseShort(string value) => OfParse<short>(short.TryParse, value);
        public static NOptional<ushort> ParseUShort(string value) => OfParse<ushort>(ushort.TryParse, value);
        public static NOptional<char> ParseChar(string value) => OfParse<char>(char.TryParse, value);
        public static NOptional<sbyte> ParseSByte(string value) => OfParse<sbyte>(sbyte.TryParse, value);
        public static NOptional<byte> ParseByte(string value) => OfParse<byte>(byte.TryParse, value);
        public static NOptional<float> ParseFloat(string value) => OfParse<float>(float.TryParse, value);
        public static NOptional<double> ParseDouble(string value) => OfParse<double>(double.TryParse, value);
        public static NOptional<decimal> ParseDecimal(string value) => OfParse<decimal>(decimal.TryParse, value);
        public static NOptional<bool> ParseBool(string value) => OfParse<bool>(bool.TryParse, value);
        public static NOptional<Guid> ParseGuid(string value) => OfParse<Guid>(Guid.TryParse, value);
        public static NOptional<DateTimeOffset> ParseDateTimeOffset(string value) 
            => OfParse<DateTimeOffset>(DateTimeOffset.TryParse, value);
        public static NOptional<DateTime> ParseDateTime(string value)
            => OfParse<DateTime>(DateTime.TryParse, value);
        public static NOptional<TimeSpan> ParseTimeSpan(string value)
            => OfParse<TimeSpan>(TimeSpan.TryParse, value);
        public static NOptional<TEnum> ParseEnum<TEnum>(string value, bool ignoreCase) where TEnum : struct
            => OfParseIgnoreCase<TEnum>(Enum.TryParse, ignoreCase, value);
    }
}