using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

public static class ObjectExtensions
{
    public static T Cast<T>(this object objectToCast)
    {
        if (objectToCast is T typedObject)
        {
            return typedObject;
        }
        else if (!typeof(T).IsInterface
            && objectToCast != null
            && objectToCast is JObject jObject)
        {
            return jObject.ToObject<T>() ?? default;
        }
        else if (!typeof(T).IsInterface
            && objectToCast != null
            && objectToCast is JsonElement jsonElement)
        {
            return jsonElement.ToObject<T>() ?? default;
        }
        return (T)objectToCast;
    }
    public static void NullCheck(
        [NotNull] this object objectToCheck,
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

    public static bool IsNull(
        [NotNullWhen(false)] this object objectToCheck
    )
    {
        return objectToCheck == null;
    }
    public static bool IsNotNull(
        [NotNullWhen(true)] this object objectToCheck
    )
    {
        return objectToCheck != null;
    }
}