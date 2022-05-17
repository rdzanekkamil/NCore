namespace NCore.Validator
{
    public static partial class ValidatableExtensions
    {
        public static ref readonly Validatable<string> IfEndsWith(this in Validatable<string> o, string value)
        {
            if (o.Value.EndsWith(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfEndsWith<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (selector(o.Value).EndsWith(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfNotEndsWith(this in Validatable<string> o, string value)
        {
            if (!o.Value.EndsWith(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNotEndsWith<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (!selector(o.Value).EndsWith(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfStartsWith(this in Validatable<string> o, string value)
        {
            if (o.Value.StartsWith(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfStartsWith<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (selector(o.Value).StartsWith(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfNotStartsWith(this in Validatable<string> o, string value)
        {
            if (!o.Value.StartsWith(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNotStartsWith<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (!selector(o.Value).StartsWith(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfContains(this in Validatable<string> o, string value)
        {
            if (o.Value.Contains(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfContains<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (selector(o.Value).Contains(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfNotContains(this in Validatable<string> o, string value)
        {
            if (!o.Value.Contains(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNotContains<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (!selector(o.Value).Contains(value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfLongerThan(this in Validatable<string> o, int value)
        {
            if (o.Value.Length > value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfLongerThan<T>(this in Validatable<T> o, Func<T,string> selector, int value)
        {
            if (selector(o.Value).Length > value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfShorterThan(this in Validatable<string> o, int value)
        {
            if (o.Value.Length < value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfShorterThan<T>(this in Validatable<T> o, Func<T,string> selector, int value)
        {
            if (selector(o.Value).Length < value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfLengthEquals(this in Validatable<string> o, int value)
        {
            if (o.Value.Length == 0) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfLengthEquals<T>(this in Validatable<T> o, Func<T,string> selector, int value)
        {
            if (selector(o.Value).Length == value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfLengthNotEquals(this in Validatable<string> o, int value)
        {
            if (o.Value.Length != value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfLengthNotEquals<T>(this in Validatable<T> o, Func<T,string> selector, int value)
        {
            if (selector(o.Value).Length != value) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfWhiteSpace(this in Validatable<string> o)
        {
            if (o.Value.All(char.IsWhiteSpace)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfWhiteSpace<T>(this in Validatable<T> o, Func<T,string> selector)
        {
            if (selector(o.Value).All(char.IsWhiteSpace)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfEmpty(this in Validatable<string> o)
        {
            if (o.Value.Length == 0) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfEmpty<T>(this in Validatable<T> o, Func<T,string> selector)
        {
            if (selector(o.Value).Length == 0) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfNullOrEmpty(this in Validatable<string> o)
        {
            if (string.IsNullOrEmpty(o.Value)) throw o.CustomException;
            return ref o;
        }
        
        public static ref readonly Validatable<T> IfNullOrEmpty<T>(this in Validatable<T> o, Func<T,string> selector)
        {
            if (string.IsNullOrEmpty(selector(o.Value))) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfNullOrWhiteSpace(this in Validatable<string> o)
        {
            if (string.IsNullOrWhiteSpace(o.Value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNullOrWhiteSpace<T>(this in Validatable<T> o, Func<T,string> selector)
        {
            if (string.IsNullOrWhiteSpace(selector(o.Value))) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfEquals(this in Validatable<string> o, string value)
        {
            if (string.Equals(o.Value,value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfEquals<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (string.Equals(selector(o.Value),value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfEqualsIgnoreCase(this in Validatable<string> o, string value)
        {
            if (string.Equals(o.Value, value, StringComparison.OrdinalIgnoreCase)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfEqualsIgnoreCase<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (string.Equals(selector(o.Value), value, StringComparison.OrdinalIgnoreCase)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<string> IfNotEquals(this in Validatable<string> o, string value)
        {
            if (!string.Equals(o.Value,value)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNotEquals<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (!string.Equals(selector(o.Value), value)) throw o.CustomException;
            return ref o;
        }
        
        public static ref readonly Validatable<string> IfNotEqualsIgnoreCase(this in Validatable<string> o, string value)
        {
            if (string.Equals(o.Value, value, StringComparison.OrdinalIgnoreCase)) throw o.CustomException;
            return ref o;
        }

        public static ref readonly Validatable<T> IfNotEqualsIgnoreCase<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (!string.Equals(selector(o.Value), value, StringComparison.OrdinalIgnoreCase)) throw o.CustomException;
            return ref o;
        }
        
        public static ref readonly Validatable<string> IfContainsIgnoreCase(this in Validatable<string> o, string value)
        {
            if (o.Value.ToLower().Contains(value.ToLower())) throw o.CustomException;
            return ref o;
        } 

        public static ref readonly Validatable<T> IfContainsIgnoreCase<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (selector(o.Value).ToLower().Contains(value.ToLower())) throw o.CustomException;
            return ref o;
        } 
        
        public static ref readonly Validatable<string> IfNotContainsIgnoreCase(this in Validatable<string> o, string value)
        {
            if (!o.Value.ToLower().Contains(value.ToLower())) throw o.CustomException;
            return ref o;
        } 
        
        public static ref readonly Validatable<T> IfNotContainsIgnoreCase<T>(this in Validatable<T> o, Func<T,string> selector, string value)
        {
            if (!selector(o.Value).ToLower().Contains(value.ToLower())) throw o.CustomException;
            return ref o;
        } 
    }
}