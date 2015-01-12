using System;
using System.Collections.Generic;
using System.Linq;

namespace LanceTrack.Server.Cqrs.Infrastructure
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// Split collection to batches consists from sequences of elements with the same key.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Batch<T, TKey>(this IEnumerable<T> source, Func<T, TKey> by)
            where TKey : IEquatable<TKey>
        {
            var batch = new List<T>();

            foreach (var e in source)
            {
                if (batch.Count == 0)
                    batch.Add(e);
                else
                {
                    var key = by(e);
                    var batchKey = by(batch[0]);

                    if (batchKey.Equals(key))
                        batch.Add(e);
                    else
                    {
                        yield return batch;

                        batch = new List<T> { e };
                    }
                }
            }

            if (batch.Any())
                yield return batch;
        }

        public static decimal SumOrDefault<T>(this IEnumerable<T> source, Func<T, decimal> selector, decimal defaultValue = 0)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");

            if (!source.Any())
                return defaultValue;

            return source.Sum(selector);
        }
    }
}