namespace EventHorizon.Game.Client.Systems.Lighting.Model
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.Lighting.Api;

    public class LightDetailsModel
        : ILightDetails
    {
        public string Name { get; set; }
        public IList<string> Tags { get; set; } = new List<string>();
        public bool EnableDayNightCycle { get; set; }
        /// <summary>
        /// Supported Types: point | hemispheric
        /// </summary>
        public string Type { get; set; }
    }
}
