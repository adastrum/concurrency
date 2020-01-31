using System.Collections.Concurrent;
using System.Linq;

namespace Concurrency.Collections
{
    public class BestPractices
    {
        /// <summary>
        /// Avoid unless necessary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="concurrentDictionary"></param>
        public void AcquireAllLocks<TKey, TValue>(ConcurrentDictionary<TKey, TValue> concurrentDictionary)
        {
            var count = concurrentDictionary.Count;
            var isEmpty = concurrentDictionary.IsEmpty;
            var keys = concurrentDictionary.Keys;
            var values = concurrentDictionary.Values;

            concurrentDictionary.Clear();
            var array = concurrentDictionary.ToArray();
        }

        /// <summary>
        /// Preferred way
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="concurrentDictionary"></param>
        public void LockFreeEnumeration<TKey, TValue>(ConcurrentDictionary<TKey, TValue> concurrentDictionary)
        {
            var count = concurrentDictionary.Skip(0).Count();
            var keys = concurrentDictionary.Select(x => x.Key);
            var values = concurrentDictionary.Select(x => x.Value);
        }
    }
}
