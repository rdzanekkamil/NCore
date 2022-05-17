using System.Reflection;
using NCore.IO;
using NCore.Monadas;
using NCore.Net;
using NCore.NTypes;
using NCore.Reflections;

namespace NCore.Extensions
{
    public static class TypeExtensions
    {
        private static NSwitcher<NTypeCode> nTypeCodeByType = new()
        {
            {typeof(PathInfo), NTypeCode.PathInfo},
            {typeof(WebUrl), NTypeCode.WebUrl},
            {typeof(Position), NTypeCode.Position},
            {typeof(NOptional<>), NTypeCode.NOptional},
            {typeof(NEither<,>), NTypeCode.NEither},
            {typeof(NSwitcher<>), NTypeCode.NSwitcher},
            {typeof(NBuilder<>), NTypeCode.NBuilder},
            {typeof(NNone), NTypeCode.NNone},
            {typeof(NTry<>), NTypeCode.NTry}
        };

        private static NSwitcher<NTypeCode> nTypeCodeByTypeCode = new()
        {
            {TypeCode.Empty, NTypeCode.Empty},
            {TypeCode.Object, NTypeCode.Object},
            {TypeCode.DBNull, NTypeCode.DBNull},
            {TypeCode.Boolean, NTypeCode.Boolean},
            {TypeCode.Char, NTypeCode.Char},
            {TypeCode.SByte, NTypeCode.SByte},
            {TypeCode.Byte, NTypeCode.Byte},
            {TypeCode.Int16, NTypeCode.Int16},
            {TypeCode.UInt16, NTypeCode.UInt16},
            {TypeCode.Int32, NTypeCode.Int32},
            {TypeCode.UInt32, NTypeCode.UInt32},
            {TypeCode.Int64, NTypeCode.Int64},
            {TypeCode.UInt64, NTypeCode.UInt64},
            {TypeCode.Single, NTypeCode.Single},
            {TypeCode.Double, NTypeCode.Double},
            {TypeCode.Decimal, NTypeCode.Decimal},
            {TypeCode.DateTime, NTypeCode.DateTime},
            {TypeCode.String, NTypeCode.String},
        };

        public static bool HasAttribute<TAttribute>(this Type o) where TAttribute : Attribute
            => o.GetCustomAttribute<TAttribute>() != null;

        public static bool HasAttribute(this Type o, Type attributeType)
            => o.GetCustomAttribute(attributeType) != null;

        public static NTypeCode GetNTypeCode(this Type o)
            => nTypeCodeByType.Match(o)
                .OrElse(nTypeCodeByTypeCode.Match(o.GetTypeCode())
                    .OrElse(NTypeCode.Object));
    }
}