namespace System.Text.Json
{
    using System;
    using System.Buffers;
    using System.Diagnostics;

    public static partial class JsonExtensions
    {
        private readonly static JsonSerializerOptions DEFAULT_OPTIONS = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public static T ToObject<T>(
            this JsonElement element,
            JsonSerializerOptions? options = null
        )
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(
                bufferWriter
            ))
            {
                element.WriteTo(writer);
            }
            return JsonSerializer.Deserialize<T>(
                bufferWriter.WrittenSpan,
                options ?? DEFAULT_OPTIONS
            );
        }

        public static T ToObject<T>(
            this JsonDocument document,
            JsonSerializerOptions? options = null
        )
        {
            if (document == null)
            {
                throw new ArgumentNullException(
                    nameof(document)
                );
            }
            return document.RootElement.ToObject<T>(
                options
            );
        }
    }
}
