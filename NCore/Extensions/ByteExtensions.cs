using System;

namespace NCore.Extensions
{
    public static class ByteExtensions
    {
        public static Byte Max(this Byte val1, Byte val2) => Math.Max(val1, val2);

        public static Byte Min(this Byte val1, Byte val2) => Math.Min(val1, val2);
    }
}