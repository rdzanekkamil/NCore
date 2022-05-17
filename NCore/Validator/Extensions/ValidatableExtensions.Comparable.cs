namespace NCore.Validator
{
    public static partial class ValidatableExtensions
    {
        public static ref readonly Validatable<T> IfGreaterThan<T>(this in Validatable<T> o, T value) where T : notnull, IComparable
        {
            if (Comparer<T>.Default.Compare(o.Value, value) > 0) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfLessThan<T>(this in Validatable<T> o, T value) where T : notnull, IComparable
        {
            if (Comparer<T>.Default.Compare(o.Value, value) < 0) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfOutOfRange<T>(this in Validatable<T> o, T min, T max) where T : notnull, IComparable
        {
            if (Comparer<T>.Default.Compare(o.Value, min) < 0 || Comparer<T>.Default.Compare(o.Value, max) > 0) 
                throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNotEquals<T>(this in Validatable<T> o, T value) where T : notnull, IComparable
        {
            if (Comparer<T>.Default.Compare(o.Value, value) != 0) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfEquals<T>(this in Validatable<T> o, T value) where T : notnull, IComparable
        {
            if (Comparer<T>.Default.Compare(o.Value, value) == 0) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfPositive<T>(this in Validatable<T> o, T value) where T : notnull, IComparable
            => ref IfGreaterThan(o, value);

        public static ref readonly Validatable<T> IfNegative<T>(this in Validatable<T> o, T value) where T : notnull, IComparable
            => ref IfLessThan(o, value);
    }
}