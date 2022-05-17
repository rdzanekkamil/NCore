using NCore.Monadas;
using static NCore.NCore;

namespace NCore.Utils
{
    public static class EnumUtil
    {
        public static NOptional<TEnum> TryParse<TEnum>(string enumName)
            where TEnum : struct
        => TryParse<TEnum>(enumName, true);

        public static NOptional<TEnum> TryParse<TEnum>(string enumName, bool ignoreCase)
            where TEnum : struct
        {
            return Enum.TryParse(typeof(TEnum), enumName, ignoreCase, out object? value)
                ? value != null 
                ? NOptional<TEnum>.OfNullable((TEnum)value)
                : NOptional<TEnum>.Empty : NOptional<TEnum>.Empty;
        }

        public static IEnumerable<TEnum> GetAllEnums<TEnum>() where TEnum : struct, Enum
            => OfOptional(Enum.GetNames<TEnum>())
                .Map(x => x.Select(x =>(TEnum)Enum.Parse(typeof(TEnum), x, true)).ToList())
                .OrElse(new List<TEnum>());
    }
}