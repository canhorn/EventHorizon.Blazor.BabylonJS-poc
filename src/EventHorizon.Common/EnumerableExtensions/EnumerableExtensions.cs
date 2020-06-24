using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class EnumerableExtensions
{
    public static IEnumerable<T> AddItem<T>(
        this IEnumerable<T> enumerable,
        T item
    )
    {
        var newList = enumerable.ToList();
        newList.Add(item);
        return newList.AsReadOnly();
    }

    public static IEnumerable<T> RemoveItem<T>(
        this IEnumerable<T> enumerable,
        T item
    )
    {
        var newList = enumerable.ToList();
        newList.Remove(item);
        return newList.AsReadOnly();
    }
}
