namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Editor.Properties.Api;
    using Newtonsoft.Json.Linq;

    public class ZoneEditorMetadata
        : PropertiesMetadata
    {
        public static readonly string EDITOR_METADATA_PREFIX = "editor:Metadata";
        /// <summary>
        /// Use the <see cref="ZoneEditorPropertyType"/> for a list of property types.
        /// </summary>
        public IDictionary<string, string> ZoneEditorPropertyTypeMap { get; set; } = new Dictionary<string, string>();

        public string GetPropertyType(
            string name,
            object value
        )
        {
            var type = ZoneEditorPropertyTypeMap.FirstOrDefault(
                metaProp => name == metaProp.Key
            ).Value ?? ZoneEditorPropertyType.PropertyString;

            if (type == ZoneEditorPropertyType.PropertyString
                && IsComplexPropertyType(
                    value
                )
            )
            {
                type = ZoneEditorPropertyType.PropertyComplex;
            }

            return type;
        }

        private static bool IsComplexPropertyType(
            object propertyValue
        )
        {
            // Check if JSON Primitive object
            if (propertyValue is JsonElement jElement
                && jElement.ValueKind == JsonValueKind.Object
            )
            {
                return true;
            }
            else if (propertyValue is JObject jObject
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

        public object GetDefaultValueForPropertyName(
            string propertyName
        )
        {
            if (ZoneEditorPropertyTypeMap.TryGetValue(
                propertyName,
                out var propertyType
            ))
            {
                return propertyType switch
                {
                    ZoneEditorPropertyType.PropertyBoolean => false,
                    ZoneEditorPropertyType.PropertyDecimal => 0.0m,
                    ZoneEditorPropertyType.PropertyLong => 0,
                    ZoneEditorPropertyType.PropertyVector3 => ServerVector3.Zero(),
                    _ => string.Empty,
                };
            }
            // Default value of empty string
            return string.Empty;
        }

        public object GetComplexPropertyValue()
        {
            return new Dictionary<string, object>();
        }
    }

    public static class ZoneEditorPropertyType
    {
        public const string PropertyBoolean = "Boolean";
        public const string PropertyDecimal = "Decimal";
        public const string PropertyLong = "Long";
        public const string PropertyString = "String";
        public const string PropertyVector3 = "Vector3";
        public const string PropertyComplex = "Complex";

        public const string PropertyAsset = "Asset";

        public static ServerVector3 ParseVector3(object property)
        {
            var vector = property.To(() => ServerVector3.Zero());
            // TODO: Test This

            return vector;

            //return vector.IsValid() ? vector : ServerVector3.Zero();
        }
    }
}
