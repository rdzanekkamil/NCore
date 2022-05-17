namespace NCore.Extensions
{
    public static class DisposableExtensions
    {
        public static void DisposableWithGC<T>(this T o) where T : IDisposable
        {
            o.Dispose();
            GC.SuppressFinalize(o);
        }

        public static void DisposableWithGC<T>(this T o, Action<IDisposable> disopeAction) where T : IDisposable
        {
            disopeAction(o);
            o.DisposableWithGC();
        }

        public static void DisposableWithGCAsync<T>(this T o) where T : IDisposable
        {
            Task.Run(() => o.DisposableWithGC());
        }

        public static void DisposableWithGCAsync<T>(this T o, Action<IDisposable> disopeAction) where T : IDisposable
        {
            Task.Run(() => o.DisposableWithGC(disopeAction));
        }
    }
}