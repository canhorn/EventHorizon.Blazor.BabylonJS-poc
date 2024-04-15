namespace EventHorizon.Game.Client.Engine.Particle.Model;

using System;
using EventHorizon.Game.Client.Engine.Particle.Api;

public class ParticleTemplateModel : ParticleTemplate
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public ParticleSettingsModel DefaultSettings { get; set; } = new ParticleSettingsModel();
    ParticleSettings ParticleTemplate.DefaultSettings => DefaultSettings;
}
