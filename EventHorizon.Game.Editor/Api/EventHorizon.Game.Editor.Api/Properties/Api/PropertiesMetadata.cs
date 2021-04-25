namespace EventHorizon.Game.Editor.Properties.Api
{
    public interface PropertiesMetadata
    {
        string GetPropertyType(
           string name,
           object value
       );
    }
}
