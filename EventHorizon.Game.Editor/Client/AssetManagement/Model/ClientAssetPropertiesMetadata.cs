namespace EventHorizon.Game.Editor.Client.AssetManagement.Model;

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using EventHorizon.Game.Editor.Properties.Api;
using EventHorizon.Game.Editor.Properties.Model;
using Newtonsoft.Json.Linq;

public class ClientAssetPropertiesMetadata : PropertiesMetadata
{
    private const string METADATA_KEY_PREFIX = "metadata:";

    private readonly Dictionary<string, string>? _metadata = new();

    public ClientAssetPropertiesMetadata(IDictionary<string, string>? metadata = null)
    {
        if (metadata is not null)
        {
            _metadata = new Dictionary<string, string>(metadata);
        }
    }

    public string GetPropertyType(string name, object value)
    {
        var type = _metadata?.FirstOrDefault(prop => name == prop.Key).Value ?? PropertyType.String;

        if (type == PropertyType.String && IsComplexPropertyType(value))
        {
            type = PropertyType.Complex;
        }

        return type;
    }

    public object GetDefaultValueForPropertyType(string propertyType)
    {
        return propertyType switch
        {
            PropertyType.Boolean => false,
            PropertyType.Decimal => 0.0m,
            PropertyType.Long => 0,
            PropertyType.String => string.Empty,
            PropertyType.Complex => new { },
            PropertyType.InputKeyMap => "{}",
            _ => string.Empty,
        };
    }

    public static IDictionary<string, object> MergeMetadataInto(
        IDictionary<string, object> newData,
        IDictionary<string, object> existingData
    )
    {
        var merged = new Dictionary<string, object>(newData);
        foreach (var metadataTypeData in existingData.Where(FilerOnlyMetadata))
        {
            merged.Add(metadataTypeData.Key, metadataTypeData.Value);
        }

        return merged;
    }

    public static bool FilterOutMetadata(KeyValuePair<string, object> dataValue) =>
        !dataValue.Key.StartsWith(METADATA_KEY_PREFIX);

    public static bool FilerOnlyMetadata(KeyValuePair<string, object> dataValue) =>
        dataValue.Key.StartsWith(METADATA_KEY_PREFIX);

    private static bool IsComplexPropertyType(object propertyValue)
    {
        if (
            propertyValue is JsonElement jsonElement
            && jsonElement.ValueKind == JsonValueKind.Object
        )
        {
            return true;
        }
        else if (propertyValue is JObject jObject && jObject.Type == JTokenType.Object)
        {
            return true;
        }
        else if (propertyValue is Dictionary<string, object>)
        {
            return true;
        }

        return false;
    }
}
