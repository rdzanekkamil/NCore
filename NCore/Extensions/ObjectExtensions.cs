using NCore.Monadas;
using static NCore.NCore;

namespace NCore.Extensions
{
    public static class ObjectExtensions
    {
        public static bool EqualsAny(this object o, params object[] args)
        {
            foreach (var item in args)
            {
                if(object.Equals(item, o)) return true;
            }
            return false;
        }

        public static bool EqualsNotAny(this object o, params object[] args)
            => !EqualsAll(o, args);

        public static bool EqualsAll(this object o, params object[] args)
        {
            foreach (var item in args)
            {
                if(!object.Equals(item, o)) return false;
            }
            return true;
        }

        public static bool EqualsNotAll(this object o, params object[] args)
            => !EqualsAny(o, args);

        public static T CastTo<T>(this object o) => (T) o;

        public static T? AsOrDefault<T>(this object o)
        {
            try
            {
                return (T) o;
            }
            catch
            {
                return default(T);
            }
        }

        public static T AsOrDefault<T>(this object o, T defaultValue)
        {
            try
            {
                return (T) o;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T AsOrDefault<T>(this object o, Func<T> defaultValueFactory)
        {
            try
            {
                return (T) o;
            }
            catch
            {
                return defaultValueFactory();
            }
        }

        public static T AsOrDefault<T>(this object o, Func<object, T> defaultValueFactory)
        {
            try
            {
                return (T) o;
            }
            catch
            {
                return defaultValueFactory(o);
            }
        }

        public static NOptional<T> TryCast<T>(this object o)
            => o is T item ? NOptional<T>.OfNullable(item) : NOptional<T>.Empty;

        public static Object ChangeType(this Object value, TypeCode typeCode) => Convert.ChangeType(value, typeCode);

        public static Object ChangeType(this Object value, TypeCode typeCode, IFormatProvider provider)
            => Convert.ChangeType(value, typeCode, provider);

        public static Object ChangeType(this Object value, Type conversionType) => Convert.ChangeType(value, conversionType);

        public static Object ChangeType(this Object value, Type conversionType, IFormatProvider provider)
            => Convert.ChangeType(value, conversionType, provider);

        public static T ChangeType<T>(this Object value) => (T)Convert.ChangeType(value, typeof (T));

        public static T ChangeType<T>(this Object value, IFormatProvider provider)
            => (T)Convert.ChangeType(value, typeof (T), provider);

        public static TypeCode GetTypeCode(this Object value) => Convert.GetTypeCode(value);

        public static NOptional<string> ToOptionalString(this object @this) => OfNullable(@this).Map(x => x.ToString());

        public static bool IsNull(this object o) => o == null;

        public static bool IsNotNull(this object o) => o != null;
    }
}