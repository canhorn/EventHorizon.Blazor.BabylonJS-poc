namespace EventHorizon.Game.Client.Engine.Particle.Add;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Particle.Api;

using MediatR;

public struct AddParticleTemplateCommand : IRequest<StandardCommandResult>
{
    public ParticleTemplate Template { get; }

    public AddParticleTemplateCommand(ParticleTemplate template)
    {
        Template = template;
    }
}
