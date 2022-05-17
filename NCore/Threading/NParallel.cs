namespace NCore.Threading
{
    public class NParallel
    {
        public static ParallelLoopResult For(int numberOfIteration, int numberOfThreads, Action<int> body)
            => Parallel.ForEach(Enumerable.Range(0, numberOfIteration), new ParallelOptions()
            {
                MaxDegreeOfParallelism = numberOfThreads
            }, body);

        public static ParallelLoopResult For(int numberOfIteration, Action<int> body)
            => Parallel.ForEach(Enumerable.Range(0, numberOfIteration), body);
        
        public static ParallelLoopResult ForEach<T>(IEnumerable<T> collection, int numberOfThreads, Action<T> body)
            => Parallel.ForEach<T>(collection, new ParallelOptions()
            {
                MaxDegreeOfParallelism = numberOfThreads
            }, body);
    }
}