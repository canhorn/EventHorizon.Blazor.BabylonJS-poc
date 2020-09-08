namespace EventHorizon.Game.Client.Engine.Particle.Create
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public struct CreateParticleFromTemplateCommand 
        : IRequest<StandardCommandResult>
    {
        public long Id { get; }
        public string TemplateId { get; }
        public ParticleSettings Settings { get; }

        public CreateParticleFromTemplateCommand(
            long id,
            string templateId,
            ParticleSettings settings
        )
        {
            Id = id;
            TemplateId = templateId;
            Settings = settings;
        }
    }
}
