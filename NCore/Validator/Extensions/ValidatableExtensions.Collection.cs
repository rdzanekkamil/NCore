namespace NCore.Validator
{
    public static partial class ValidatableExtensions
    {
        public static ref readonly Validatable<IEnumerable<T>> IfEmpty<T>(this in Validatable<IEnumerable<T>> o) 
        {
            if (!o.Value.Any()) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<IEnumerable<T>> IfNotEmpty<T>(this in Validatable<IEnumerable<T>> o) 
        {
            if (o.Value.Any()) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<IEnumerable<T>> IfCountNotEquals<T>(this in Validatable<IEnumerable<T>> o, int count) 
        {
            if (o.Value.Count() != count) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<IEnumerable<T>> IfCountEquals<T>(this in Validatable<IEnumerable<T>> o, int count) 
        {
            if (o.Value.Count() == count) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<IEnumerable<T>> IfCountGreaterThan<T>(this in Validatable<IEnumerable<T>> o, int count) 
        {
            if (o.Value.Count() > count) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<IEnumerable<T>> IfCountLessThan<T>(this in Validatable<IEnumerable<T>> o, int count) 
        {
            if (o.Value.Count() < count) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<IEnumerable<T>> IfHasNull<T>(this in Validatable<IEnumerable<T>> o) 
        {
            foreach (var item in o.Value)
            {
                if (item == null) throw o.CustomException;
            } 
            return ref o;
        }

        public static ref readonly Validatable<IEnumerable<T>> IfContains<T>(this in Validatable<IEnumerable<T>> o, T element) 
        {
            foreach (var item in o.Value)
            {
                if (Equals(item, element)) throw o.CustomException;
            } 
            return ref o;
        }

        public static ref readonly Validatable<IEnumerable<T>> IfNotContains<T>(this in Validatable<IEnumerable<T>> o, T element) 
        {
            foreach (var item in o.Value)
            {
                if (!Equals(item, element)) throw o.CustomException;
            } 
            return ref o;
        }
    }
}