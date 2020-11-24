namespace EventHorizon.Game.Editor.Zone.Editor.Services.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;

    public class ZoneEditorMetadata
    {
        public static readonly string EDITOR_METADATA_PREFIX = "editor:Metadata";
        /// <summary>
        /// Use the <see cref="ZoneEditorPropertyType"/> for a list of property types.
        /// </summary>
        public IDictionary<string, string> ZoneEditorPropertyTypeMap { get; set; }

        public string GetPropertyType(
            string name
        ) => ZoneEditorPropertyTypeMap.FirstOrDefault(
            metaProp => name == metaProp.Key
        ).Value ?? ZoneEditorPropertyType.PropertyString;

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
                    ZoneEditorPropertyType.PropertyFloat => 0.0f,
                    ZoneEditorPropertyType.PropertyLong => 0,
                    ZoneEditorPropertyType.PropertyVector3 => ServerVector3.Zero(),
                    _ => string.Empty,
                };
            }
            // Default value of empty string
            return string.Empty;
        }
    }

    public static class ZoneEditorPropertyType
    {
        public const string PropertyBoolean = "Boolean";
        public const string PropertyFloat = "Float";
        public const string PropertyLong = "Long";
        public const string PropertyString = "String";
        public const string PropertyVector3 = "Vector3";

        public const string PropertyAsset = "Asset";

        public static ServerVector3 ParseVector3(object property)
        {
            var vector = property.Cast<ServerVector3>();
            // TODO: Test This

            return vector;

            //return vector.IsValid() ? vector : ServerVector3.Zero();
        }
    }
}
