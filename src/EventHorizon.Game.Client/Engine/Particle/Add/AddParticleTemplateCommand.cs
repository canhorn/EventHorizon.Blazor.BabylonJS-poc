namespace EventHorizon.Game.Client.Engine.Particle.Add
{
    using EventHorizon.Game.Client.Engine.Particle.Api;
    using MediatR;

    public struct AddParticleTemplateCommand : IRequest
    {
        public IParticleTemplate Template { get; }

        public AddParticleTemplateCommand(
            IParticleTemplate template
        )
        {
            Template = template;
        }
    }
}
