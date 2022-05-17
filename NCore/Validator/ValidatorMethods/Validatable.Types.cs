namespace NCore.Validator
{
    public partial record struct Validatable<T>
    {
        public Validatable<T> IfType<TOther>()
        {
            if (typeof(T) == typeof(TOther)) throw this.CustomException;
            return this;
        }

        public Validatable<T> IfType(Type type)
        {
            if (typeof(T) == type) throw this.CustomException;
            return this;
        }

        public Validatable<T> IfNotType<TOther>()
        {
            if (typeof(T) != typeof(TOther)) throw this.CustomException;
            return this;
        }

        public Validatable<T> IfNotType(Type type)
        {
            if (typeof(T) != type) throw this.CustomException;
            return this;
        }
    }
}