using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

public static class ObjectExtensions
{
    /// <summary>
    /// Converts the object into the Type Parameters, uses a combination of pattern matching and JSON deserialization.
    /// Supported JSON Types: Newtonsoft.Json.Linq.JObject and System.Text.Json.JsonElement
    /// </summary>
    /// <typeparam name="T">The type this should transform to.</typeparam>
    /// <param name="objectToCast">The object to be converted to the Type Parameter.</param>
    /// <returns>The object casted to the type, can return a new object if is a raw Json Element.</returns>
    [return: NotNullIfNotNull("defaultValue")]
    public static T? To<T>(this object objectToCast, Func<T>? defaultValue = default)
    {
        if (objectToCast is T typedObject)
        {
            return typedObject;
        }
        else if (!typeof(T).IsInterface
            && objectToCast != null
            && objectToCast is JObject jObject)
        {
            return jObject.ToObject<T>() ?? (defaultValue != null ? defaultValue() : default);
        }
        else if (!typeof(T).IsInterface
            && objectToCast != null
            && objectToCast is JsonElement jsonElement)
        {
            return jsonElement.ToObject<T>() ?? (defaultValue != null ? defaultValue() : default);
        }

        return (T?)objectToCast ?? (defaultValue != null ? defaultValue() : default);
    }

    public static T? To<T>(this object objectToCast)
    {
        if (objectToCast is T typedObject)
        {
            return typedObject;
        }
        else if (!typeof(T).IsInterface
            && objectToCast != null
            && objectToCast is JObject jObject)
        {
            return jObject.ToObject<T>();
        }
        else if (!typeof(T).IsInterface
            && objectToCast != null
            && objectToCast is JsonElement jsonElement)
        {
            return jsonElement.ToObject<T>();
        }

        return (T?)objectToCast;
    }

    /// <summary>
    /// Throws an ArgumentNullException if null.
    /// </summary>
    /// <param name="objectToCheck">The object to check for null.</param>
    /// <param name="paramName">Optional parameter to pass into Exception if null.</param>
    public static void NullCheck(
        [NotNull] this object? objectToCheck,
        string paramName = ""
    )
    {
        if (objectToCheck == null)
        {
            throw new System.ArgumentNullException(
                paramName
            );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objectToCheck"></param>
    /// <returns></returns>
    public static bool IsNull(
        [NotNullWhen(false)] this object? objectToCheck
    )
    {
        return objectToCheck == null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objectToCheck"></param>
    /// <returns></returns>
    public static bool IsNotNull(
        [NotNullWhen(true)] this object? objectToCheck
    )
    {
        return objectToCheck != null;
    }
}