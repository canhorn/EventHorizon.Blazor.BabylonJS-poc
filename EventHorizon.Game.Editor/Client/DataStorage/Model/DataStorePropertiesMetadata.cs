namespace EventHorizon.Game.Editor.Client.DataStorage.Model;

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using EventHorizon.Game.Editor.Properties.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using Newtonsoft.Json.Linq;

public class DataStorePropertiesMetadata : PropertiesMetadata
{
    public const string DATA_STORE_SCHEMA_KEY = "dataStore:Schema";

    private readonly Dictionary<string, string>? _metadata = new();

    public DataStorePropertiesMetadata(
        Dictionary<string, string>? metadata = null
    )
    {
        if (metadata is not null)
        {
            _metadata = metadata;
        }
    }

    public string GetPropertyType(string name, object value)
    {
        var type =
            _metadata?.FirstOrDefault(prop => name == prop.Key).Value
            ?? ZoneEditorPropertyType.PropertyString;

        if (
            type == ZoneEditorPropertyType.PropertyString
            && IsComplexPropertyType(value)
        )
        {
            type = ZoneEditorPropertyType.PropertyComplex;
        }

        return type;
    }

    public object GetDefaultValueForPropertyType(string propertyType)
    {
        return propertyType switch
        {
            ZoneEditorPropertyType.PropertyBoolean => false,
            ZoneEditorPropertyType.PropertyDecimal => 0.0m,
            ZoneEditorPropertyType.PropertyLong => 0,
            ZoneEditorPropertyType.PropertyString => string.Empty,
            ZoneEditorPropertyType.PropertyComplex => new { },
            _ => string.Empty,
        };
    }

    private static bool IsComplexPropertyType(object propertyValue)
    {
        if (
            propertyValue is JsonElement jsonElement
            && jsonElement.ValueKind == JsonValueKind.Object
        )
        {
            return true;
        }
        else if (
            propertyValue is JObject jObject
            && jObject.Type == JTokenType.Object
        )
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
