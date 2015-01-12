using System;
using System.Collections.Generic;

namespace LanceTrack.Server.Cqrs.Infrastructure
{
    public static class DictionaryExtension
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default (TValue))
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            TValue existingValue;
            if (dictionary.TryGetValue(key, out existingValue))
                return existingValue;

            dictionary.Add(key, defaultValue);
            return defaultValue;
        }
    }
}