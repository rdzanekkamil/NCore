namespace NCore.Monadas.Models
{
    public struct ErrorValue : IErrorValue
    {
        public static ErrorValue Empty = new ErrorValue();
        public static ErrorValue NoneOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.NONE, message, exception, objectData);

        public static ErrorValue DebugOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.DEBUG, message, exception, objectData);

        public static ErrorValue InformationalOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.INFORMATIONAL, message, exception, objectData);

        public static ErrorValue NoticeOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.NOTICE, message, exception, objectData);

        public static ErrorValue WarningOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.WARNING, message, exception, objectData);

        public static ErrorValue ErrorOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.ERROR, message, exception, objectData);

        public static ErrorValue CriticalOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.CRITICAL, message, exception, objectData);

        public static ErrorValue AlertOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.ALERT, message, exception, objectData);

        public static ErrorValue EmergencyOf(string? message, Exception? exception, object? objectData)
            => new ErrorValue(ErrorLevel.EMERGENCY, message, exception, objectData);

        public ErrorLevel ErrorLevel { get; init; }
        public string? Message { get; init; }
        public Exception? Exception { get; init; }
        public object? ObjectData { get; init; }

        public ErrorValue(ErrorLevel errorLevel,
                          string? message,
                          Exception? exception,
                          object? objectData)
        {
            this.ErrorLevel = errorLevel;
            this.Exception = exception;
            this.Message = message;
            this.ObjectData = objectData;
        }
    }
}