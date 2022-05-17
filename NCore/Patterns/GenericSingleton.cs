namespace NCore.Patterns
{
    public class GenericSingleton<T>
    {
        private static Lazy<T> lazyInstance;

        public GenericSingleton(Func<T> factory) => lazyInstance = new Lazy<T>(factory);

        public static T Instance => lazyInstance.Value;
    }
}