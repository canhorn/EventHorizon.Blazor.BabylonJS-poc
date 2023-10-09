namespace EventHorizon.Game.Client.Systems.Lighting.Model;

using System.Collections.Generic;

using EventHorizon.Game.Client.Systems.Lighting.Api;

public class LightDetailsModel : ILightDetails
{
    public string Name { get; set; } = string.Empty;
    public IList<string> Tags { get; set; } = new List<string>();
    public bool EnableDayNightCycle { get; set; }

    /// <summary>
    /// Supported Types: unknown | point | hemispheric
    /// </summary>
    public string Type { get; set; } = "unknown";
}
