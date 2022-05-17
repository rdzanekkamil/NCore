namespace NCore.Monadas.Models
{
    public interface IErrorValue
    {
        ErrorLevel ErrorLevel { get; init; }
        string? Message { get; init; }
        Exception? Exception { get; init; }
        object? ObjectData { get; init; }
    }
}