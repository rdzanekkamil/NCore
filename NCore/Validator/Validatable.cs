using NCore.Validator.Fluent;

namespace NCore.Validator
{
    public partial record struct Validatable<T> : IThrowSelector<T>
    {
        public T? Value { get; }
        public Exception CustomException { get; private set; } = new Exception();

        private Validatable(T? value)
        {
            this.Value = value;
        }

        public static Validatable<T> Valid(T? value) => new Validatable<T>(value);

        public Validatable<T> Throw(Func<Exception> exceptionFactory)
        {
            this.CustomException = exceptionFactory();
            return (Validatable<T>)this;
        }

        public Validatable<T> Throw(Exception exceptionFactory)
        {
            this.CustomException = exceptionFactory;
            return (Validatable<T>)this;
        }

        public Validatable<T> ThrowNullReferenceException() => Throw(new NullReferenceException());

        public Validatable<T> ThrowNullReferenceException(string message) => Throw(new NullReferenceException(message));

        public Validatable<T> ThrowArgumentNullException() => Throw(new ArgumentNullException());
 
        public Validatable<T> ThrowArgumentNullException(string paramName) => Throw(new ArgumentNullException(paramName));

        public Validatable<T> ThrowArgumentNullException(string paramName, string message) 
            => Throw(new ArgumentNullException(paramName, message));

        public Validatable<T> ThrowArgumentException() => Throw(new ArgumentException());

        public Validatable<T> ThrowArgumentException(string paramName) => Throw(new ArgumentException(paramName));

        public Validatable<T> ThrowArgumentException(string paramName, string message) 
            => Throw(new ArgumentException(paramName, message));

        public Validatable<T> ThrowInvalidOperationException()
            => Throw(new InvalidOperationException());

        public Validatable<T> ThrowInvalidOperationException(string message)
            => Throw(new InvalidOperationException(message));

        public Validatable<T> ThrowInvalidCastException()
            => Throw(new InvalidCastException());

        public Validatable<T> ThrowInvalidCastException(string message)
            => Throw(new InvalidCastException(message));

        public Validatable<T> ThrowInvalidCastException(string message, int errorCode)
            => Throw(new InvalidCastException(message, errorCode));

        public Validatable<T> ThrowFileNotFoundException()
            => Throw(new FileNotFoundException());

        public Validatable<T> ThrowFileNotFoundException(string message)
            => Throw(new FileNotFoundException(message));

        public Validatable<T> ThrowFileNotFoundException(string message, string fileName)
            => Throw(new FileNotFoundException(message, fileName));
    }
}