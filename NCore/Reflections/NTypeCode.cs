namespace NCore.Reflections
{
    public enum NTypeCode
    {  
        Empty = TypeCode.Empty,
        Object = TypeCode.Object,
        DBNull = TypeCode.DBNull,
        Boolean = TypeCode.Boolean,
        Char = TypeCode.Char,
        SByte = TypeCode.SByte,
        Byte = TypeCode.Byte,
        Int16 = TypeCode.Int16,
        UInt16 = TypeCode.UInt16,
        Int32 = TypeCode.Int32,
        UInt32 = TypeCode.UInt32,
        Int64 = TypeCode.Int64,
        UInt64 = TypeCode.UInt64,
        Single = TypeCode.Single,
        Double = TypeCode.Double,
        Decimal = TypeCode.Decimal,
        DateTime = TypeCode.DateTime,
        String = TypeCode.String,
        PathInfo = 19,
        WebUrl = 20,
        Position = 21,
        NOptional = 22,
        NEither = 23,
        NBuilder = 24,
        NNone = 25,
        NResult = 26,
        NSwitcher = 27,
        NTry = 28
    }
}