namespace System.Collections.Generic
{
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
            this IDictionary<TKey, TValue> root,
            params IDictionary<TKey, TValue>[] values
        ) => values.InsertItem(
            0,
            root
        ).SelectMany(
            x => x
        ).GroupBy(
            d => d.Key
        ).ToDictionary(
            x => x.Key,
            y => y.Last().Value
        );
    }
}
