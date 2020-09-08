namespace EventHorizon.Game.Client.Engine.Particle.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EventHorizon.Game.Client.Engine.Particle.Api;

    public class ParticleSettingsModel
        : Dictionary<string, object>,
        ParticleSettings
    {
        public static ParticleSettingsModel Merge(
            params ParticleSettings[] options
        )
        {
            var value = options.SelectMany(
                x => x
            ).GroupBy(
                d => d.Key
            ).ToDictionary(
                x => x.Key,
                y => y.Last().Value
            );
            return new ParticleSettingsModel(
                value
            );
        }

        public ParticleSettingsModel()
        {
        }

        public ParticleSettingsModel(
            IDictionary<string, object> dictionary
        ) : base(dictionary ?? new Dictionary<string, object>())
        {
        }

        public string Name => TryGetValue(
            "name",
            out var value
        ) ? value.Cast<string>() : string.Empty;
        public decimal Capacity => TryGetValue(
            "capacity",
            out var value
        ) ? value.Cast<decimal>() : 0;
    }
}
