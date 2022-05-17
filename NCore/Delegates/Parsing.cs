namespace NCore.Delegates
{
    public delegate bool TryParse<T>(string value, out T result);

    public delegate bool TryParseIgnoreCase<T>(string value, bool ignoreCase, out T result);
}