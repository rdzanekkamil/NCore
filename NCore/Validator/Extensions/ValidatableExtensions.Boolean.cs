namespace NCore.Validator
{
    public static partial class ValidatableExtensions
    {
        public static ref readonly Validatable<bool> IfTrue(this in Validatable<bool> o)
        {
            if (o.Value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<bool> IfTrue(this in Validatable<bool> o, bool condition)
        {
            if (condition) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfTrue<T>(this in Validatable<T> o, Func<T,bool> condition)
        {
            if (condition(o.Value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<bool> IfFalse(this in Validatable<bool> o)
        {
            if (!o.Value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<bool> IfFalse(this in Validatable<bool> o, bool condition)
        {
            if (!condition) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfFalse<T>(this in Validatable<T> o, Func<T,bool> condition)
        {
            if (!condition(o.Value)) throw o.CustomException;
            return ref o;
        }
    }
}