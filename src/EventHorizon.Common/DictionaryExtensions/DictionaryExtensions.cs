namespace System.Collections.Generic
{
    using System.Linq;

    public static class DictionaryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="root"></param>
        /// <param name="values"></param>
        /// <returns></returns>
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
