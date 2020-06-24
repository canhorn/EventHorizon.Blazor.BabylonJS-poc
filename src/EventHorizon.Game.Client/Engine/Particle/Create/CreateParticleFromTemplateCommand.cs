namespace EventHorizon.Game.Client.Engine.Particle.Create
{
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public struct CreateParticleFromTemplateCommand : IRequest
    {
        public long Id { get; }
        public string TemplateId { get; }
        public IParticleSettings Settings { get; }

        public CreateParticleFromTemplateCommand(
            long id,
            string templateId,
            IParticleSettings settings
        )
        {
            Id = id;
            TemplateId = templateId;
            Settings = settings;
        }
    }
}
