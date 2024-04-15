namespace System.Text.Json;

using System;
using System.Buffers;

public static partial class JsonExtensions
{
    public static readonly JsonSerializerOptions DEFAULT_OPTIONS =
        new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="element"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static T? ToObject<T>(this JsonElement element, JsonSerializerOptions? options = null)
    {
        var bufferWriter = new ArrayBufferWriter<byte>();
        using (var writer = new Utf8JsonWriter(bufferWriter))
        {
            element.WriteTo(writer);
        }
        return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, options ?? DEFAULT_OPTIONS);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="document"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static T? ToObject<T>(this JsonDocument document, JsonSerializerOptions? options = null)
    {
        if (document == null)
        {
            throw new ArgumentNullException(nameof(document));
        }
        return document.RootElement.ToObject<T>(options);
    }
}
