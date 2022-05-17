namespace NCore.Validator
{
    public static partial class ValidatableExtensions
    {
        private static bool IsEquals(string firstValue, string secondValue) 
            => string.Equals(firstValue, secondValue, StringComparison.OrdinalIgnoreCase);
    }
}