namespace EventHorizon.Game.Editor.Client.Shared.Properties;

using System.Collections.Generic;

public record PropertiesDisplayChangedArgs(
    string PropertyName,
    IDictionary<string, object> Data
);
