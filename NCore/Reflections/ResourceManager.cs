using System.Reflection;
using NCore.Monadas;
using static NCore.NCore;

namespace NCore.Reflections
{
    public static class ResourceManager
    {
        public static NOptional<Stream> ReadResource(Assembly assembly, string resourceName)
            => OfNullable(assembly.GetManifestResourceNames())
                .Map(ressouceNames => ressouceNames.SingleOrDefault(x => x.Contains(resourceName, StringComparison.OrdinalIgnoreCase)))
                .Filter(x => IsNotNullOrEmpty(x))
                .Map(x => assembly.GetManifestResourceStream(x)!);

        public static NOptional<Stream> ReadResource<T>(string resourceName)
            => OfNullable(resourceName)
                .Filter(x => IsNotNullOrEmpty(x))
                .Bind(x => ReadResource(typeof(T).Assembly, resourceName));

        public static NOptional<string> ReadResourceAsString(Assembly assembly, string resourceName)
            => ReadResource(assembly, resourceName)
                .Map(stream => {
                    using var streamReader = new StreamReader(stream);
                    return streamReader.ReadToEnd();
                });

        public static NOptional<string> ReadResourceAsString<T>(string resourceName)
            => OfNullable(resourceName)
                .Filter(x => !string.IsNullOrEmpty(x))
                .Bind(x => ReadResourceAsString(typeof(T).Assembly, resourceName));
    }
}