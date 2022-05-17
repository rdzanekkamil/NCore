namespace NCore.Validator.Fluent
{
    public interface IThrowSelector<T>
    {
        Validatable<T> Throw(Func<Exception> exceptionFactory);
        Validatable<T> Throw(Exception exceptionFactory);
        Validatable<T> ThrowNullReferenceException();
        Validatable<T> ThrowNullReferenceException(string message);
        Validatable<T> ThrowArgumentNullException();
        Validatable<T> ThrowArgumentNullException(string paramName);
        Validatable<T> ThrowArgumentNullException(string paramName, string message);
        Validatable<T> ThrowArgumentException();
        Validatable<T> ThrowArgumentException(string paramName);
        Validatable<T> ThrowArgumentException(string paramName, string message);
        Validatable<T> ThrowInvalidOperationException();
        Validatable<T> ThrowInvalidOperationException(string message);
        Validatable<T> ThrowInvalidCastException();
        Validatable<T> ThrowInvalidCastException(string message);
        Validatable<T> ThrowInvalidCastException(string message, int errorCode);
        Validatable<T> ThrowFileNotFoundException();
        Validatable<T> ThrowFileNotFoundException(string message);
        Validatable<T> ThrowFileNotFoundException(string message, string fileName);
    }
}