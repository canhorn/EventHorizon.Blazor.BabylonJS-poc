namespace EventHorizon.Game.Client.Systems.Particle.Model;

using EventHorizon.Game.Client.Engine.Particle.State;

public struct ParticleEmitterOptions
{
    public long ParticleId { get; }
    public string TemplateId { get; }
    public bool IgnoreMeshVisibility { get; }
    public bool StartAfterCreated { get; }

    public ParticleEmitterOptions(
        long particleId,
        string templateId,
        bool ignoreMeshVisibility = false,
        bool startAfterCreated = true
    )
    {
        ParticleId = particleId;
        TemplateId = templateId;
        IgnoreMeshVisibility = ignoreMeshVisibility;
        StartAfterCreated = startAfterCreated;

        if (TemplateId.IsNullOrEmpty())
        {
            TemplateId = DefaultParticleSettings.DEFAULT_TEMPLATE_ID;
        }
    }
}
