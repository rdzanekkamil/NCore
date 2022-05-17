using System;
using System.Globalization;

namespace NCore.Extensions
{
    public static class CharExtensions
    {
        public static Int32 ConvertToUtf32(this Char highSurrogate, Char lowSurrogate)
            => Char.ConvertToUtf32(highSurrogate, lowSurrogate);

        public static Double GetNumericValue(this Char c) => Char.GetNumericValue(c);

        public static UnicodeCategory GetUnicodeCategory(this Char c) => Char.GetUnicodeCategory(c);

        public static Boolean IsControl(this Char c) => Char.IsControl(c);

        public static Boolean IsDigit(this Char c) => Char.IsDigit(c);

        public static Boolean IsHighSurrogate(this Char c) => Char.IsHighSurrogate(c);

        public static Boolean IsLetter(this Char c) => Char.IsLetter(c);

        public static Boolean IsLetterOrDigit(this Char c) => Char.IsLetterOrDigit(c);

        public static Boolean IsLowSurrogate(this Char c) => Char.IsLowSurrogate(c);

        public static Boolean IsLower(this Char c) => Char.IsLower(c);

        public static Boolean IsNumber(this Char c) => Char.IsNumber(c);

        public static Boolean IsPunctuation(this Char c) => Char.IsPunctuation(c);

        public static Boolean IsSeparator(this Char c) => Char.IsSeparator(c);

        public static Boolean IsSurrogate(this Char c) => Char.IsSurrogate(c);

        public static Boolean IsSurrogatePair(this Char highSurrogate, Char lowSurrogate)
            => Char.IsSurrogatePair(highSurrogate, lowSurrogate);

        public static Boolean IsSymbol(this Char c) => Char.IsSymbol(c);

        public static Boolean IsUpper(this Char c) => Char.IsUpper(c);

        public static Boolean IsWhiteSpace(this Char c) => Char.IsWhiteSpace(c);

        public static Char ToLower(this Char c, CultureInfo culture) => Char.ToLower(c, culture);

        public static Char ToLower(this Char c) => Char.ToLower(c);

        public static Char ToLowerInvariant(this Char c) => Char.ToLowerInvariant(c);

        public static Char ToUpper(this Char c, CultureInfo culture) => Char.ToUpper(c, culture);

        public static Char ToUpper(this Char c) => Char.ToUpper(c);

        public static Char ToUpperInvariant(this Char c) => Char.ToUpperInvariant(c);
    }
}