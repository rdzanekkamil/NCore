namespace NCore
{
    public static partial class NCore
    {
        public static Task<T> OfTaskResult<T>(T item) => Task.FromResult(item);

        public static Task OfTaskRun(Action action) => Task.Run(action);

        public static Task OfTaskRun(Action action, CancellationToken token) => Task.Run(action, token);

        public static Task<T> OfTaskRun<T>(Func<T> action) => Task.Run(action);

        public static Task<T> OfTaskRun<T>(Func<T> action, CancellationToken token) => Task.Run(action, token);

        public static Task OfTaskRun(Func<Task> action) => Task.Run(action);

        public static Task OfTaskRun(Func<Task> action, CancellationToken token) => Task.Run(action, token);

        public static Task<T> OfTaskRun<T>(Func<Task<T>> action) => Task.Run(action);

        public static Task<T> OfTaskRun<T>(Func<Task<T>> action, CancellationToken token) => Task.Run(action, token);
    }
}