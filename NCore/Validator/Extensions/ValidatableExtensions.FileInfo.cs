namespace NCore.Validator
{
    public static partial class ValidatableExtensions
    {
        public static ref readonly Validatable<FileInfo> IfFileNotExsits(this in Validatable<FileInfo> o)
        {
            if (o.Value.Exists == false) throw o.CustomException;
            return ref o;
        }
    }
}