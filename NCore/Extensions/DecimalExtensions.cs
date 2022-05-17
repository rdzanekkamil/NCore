using System;

namespace NCore.Extensions
{
    public static class DecimalExtensions
    {
        public static Decimal Divide(this Decimal d1, Decimal d2) => Decimal.Divide(d1, d2);

        public static Decimal Multiply(this Decimal d1, Decimal d2) => Decimal.Multiply(d1, d2);

        public static Decimal Negate(this Decimal d) => Decimal.Negate(d);

        public static Decimal Remainder(this Decimal d1, Decimal d2) => Decimal.Remainder(d1, d2);

        public static Decimal Subtract(this Decimal d1, Decimal d2) => Decimal.Subtract(d1, d2);
    }
}