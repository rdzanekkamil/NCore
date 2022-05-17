using System;
using System.IO;

namespace NCore.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] Resize(this byte[] o, int newSize)
        {
            Array.Resize(ref o, newSize);
            return o;
        }

        public static MemoryStream ToMemoryStream(this byte[] o) => new MemoryStream(o);

        public static String ToBase64String(this Byte[] inArray) => Convert.ToBase64String(inArray);

        public static String ToBase64String(this Byte[] inArray, Base64FormattingOptions options)
            => Convert.ToBase64String(inArray, options);

        public static String ToBase64String(this Byte[] inArray, Int32 offset, Int32 length)
            => Convert.ToBase64String(inArray, offset, length);

        public static String ToBase64String(this Byte[] inArray, Int32 offset, Int32 length, Base64FormattingOptions options)
            => Convert.ToBase64String(inArray, offset, length, options);

        public static Int32 ToBase64CharArray(this Byte[] inArray, Int32 offsetIn, Int32 length, Char[] outArray, Int32 offsetOut)
            => Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut);

        public static Int32 ToBase64CharArray(this Byte[] inArray, Int32 offsetIn, Int32 length, Char[] outArray, Int32 offsetOut, Base64FormattingOptions options)
            => Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, options);
    }
}