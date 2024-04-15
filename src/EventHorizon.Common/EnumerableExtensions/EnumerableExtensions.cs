namespace System.Collections.Generic;

using System;
using System.Linq;
using System.Text;

public static class EnumerableExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static IEnumerable<T> AddItem<T>(this IEnumerable<T> enumerable, T item)
    {
        var newList = enumerable.ToList();
        newList.Add(item);
        return newList.AsReadOnly();
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="index"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static IEnumerable<T> InsertItem<T>(this IEnumerable<T> enumerable, int index, T item)
    {
        var newList = enumerable.ToList();
        newList.Insert(index, item);
        return newList.AsReadOnly();
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static IEnumerable<T> RemoveItem<T>(this IEnumerable<T> enumerable, T item)
    {
        var newList = enumerable.ToList();
        newList.Remove(item);
        return newList.AsReadOnly();
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="predicate"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool TryGetItem<TSource>(
        this IEnumerable<TSource> enumerable,
        Func<TSource, bool> predicate,
        out TSource item
    ) => (item = enumerable.FirstOrDefault(predicate)).IsNotNull();
}
