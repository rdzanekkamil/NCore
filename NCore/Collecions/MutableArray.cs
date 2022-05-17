using System.Collections;
using NCore.Extensions;
using NCore.Monadas;
using static NCore.NCore;

namespace NCore.Collecions
{
    public readonly struct MutableArray<T> : IEnumerable<T>
    {
        public readonly T[] Array { get; }
        public readonly int Count { get; }

        public static MutableArray<T> Empty = new MutableArray<T>();

        public MutableArray(T[] data)
        {
            this.Array = data;
            this.Count = data.Length;
        }

        public NOptional<T> Get(int index)
            => OfNullable(Array)
                .Filter(x => x.IndexExists(index))
                .Map(x => x[index]);

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in Array)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in Array)
            {
                yield return item;
            }
        }

        public T this[int index]
        {
            get => this.Array.IndexExists(index)
                ? this.Array[index]
                : throw new IndexOutOfRangeException();
            set
            {
                if (this.Array.IndexExists(index)) this.Array[index] = value;
                throw new IndexOutOfRangeException();
            }
        }

        public static MutableArray<T> Of(int count) => new MutableArray<T>(new T[count]);
        public static MutableArray<T> Of(IEnumerable<T> collecion) => new MutableArray<T>(collecion.ToArray());
        public static MutableArray<T> Of(T[] array) => new MutableArray<T>(array);

        public MutableArray<T> GetElements(int startIndex, int count)
        {
            if (!this.Array.IndexExists(startIndex)) throw new ArgumentOutOfRangeException(nameof(startIndex));
            return new MutableArray<T>(this.Array.Skip(startIndex).Take(count - startIndex).ToArray());
        }

        public MutableArray<T> Take(int take) => GetElements(0, take);
        public MutableArray<T> Skip(int skip) => GetElements(skip, this.Count);

        public NOptional<T> First() 
            => OfNullable(this.Array)
                .Filter(arr => arr.Any())
                .Map(arr => arr.First());

        public NOptional<T> Last() 
            => OfNullable(this.Array)
                .Filter(arr => arr.Any())
                .Map(arr => arr.Last());
    }
}