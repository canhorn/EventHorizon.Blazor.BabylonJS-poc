namespace EventHorizon.Game.Client.Systems.Lighting.Api
{
    using System.Collections.Generic;

    public interface ILightDetails
    {
        string Name { get; }
        IList<string> Tags { get; }
        bool EnableDayNightCycle { get; }
        string Type { get; }
    }
}