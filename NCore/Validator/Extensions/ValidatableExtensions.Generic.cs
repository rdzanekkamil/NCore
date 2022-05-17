namespace NCore.Validator
{
    public static partial class ValidatableExtensions
    {
        public static ref readonly Validatable<T> IfNull<T>(this in Validatable<T> o)
        {
            if (o.Value == null) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNotNull<T>(this in Validatable<T> o)
        {
            if (o.Value != null) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfDefault<T>(this in Validatable<T> o)
        {
            if (Equals(o.Value, default(T))) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNotDefault<T>(this in Validatable<T> o)
        {
            if (!Equals(o.Value, default(T))) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfThen<T>(this in Validatable<T> o, Predicate<T> predicate)
        {
            if (predicate(o.Value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfThenNot<T>(this in Validatable<T> o, Predicate<T> predicate)
        {
            if (!predicate(o.Value)) throw o.CustomException;
            return ref o;
        }
    }
}