using static NCore.NCore;
using NCore.Extensions;
using NCore.Monadas;

namespace NCore.IO
{
    public struct FileExt
    {
        public const string POINT = ".";

        public NOptional<string> Extension { get; init; }

        public string ValueOrEmpty => Extension.OrElse(string.Empty);

        public string UpperExtensionOrEmpty => ValueOrEmpty.ToUpper();

        public string LowerExtensionOrEmpty => ValueOrEmpty.ToLower();

        public bool IsEmpty => Extension.IsEmpty;

        public bool HasValue => Extension.HasValue;

        public static readonly FileExt Empty = new FileExt();

        public string ExtensionWithPointOrEmpty 
            => Extension
                .Filter(x => !string.IsNullOrEmpty(x))
                .Map(x => $".{x}")
                .OrElse(string.Empty);

        public FileExt(string value)
        {
            Extension = OfNullable(value)
                .Filter(x => !string.IsNullOrEmpty(x))
                .Map(x => x.Remove(POINT));
        }

        public static implicit operator string(FileExt o) => o.Extension.OrElse(string.Empty);

        public static implicit operator FileExt(string o) => new FileExt(o);

        public static implicit operator FileExt(NOptional<string> o) 
            => new FileExt(o.HasValue ? o.Value : Empty);

        public static string operator +(string v1, FileExt v2)
            => $"{v1}{v2.ExtensionWithPointOrEmpty}";

        public static string operator -(string v1, FileExt v2)
            => v1.Remove(v2.ExtensionWithPointOrEmpty);
    }
}