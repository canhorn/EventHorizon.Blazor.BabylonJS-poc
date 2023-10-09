namespace EventHorizon.Game.Client.Engine.Particle.State;

using EventHorizon.Game.Client.Engine.Particle.Api;
using EventHorizon.Game.Client.Engine.Particle.Model;

public static class DefaultParticleSettings
{
    public static readonly string DEFAULT_TEMPLATE_ID = "DEFAULT";

    internal static ParticleSettings Settings = new ParticleSettingsModel
    {
        ["capacity"] = 1m,
        ["particleTexture"] = string.Empty,
    };
}
