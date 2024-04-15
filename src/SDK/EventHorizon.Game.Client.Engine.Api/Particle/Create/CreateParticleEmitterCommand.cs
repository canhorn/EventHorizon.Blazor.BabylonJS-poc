namespace EventHorizon.Game.Client.Engine.Particle.Create;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Systems.Particle.Api;
using MediatR;

public struct CreateParticleEmitterCommand : IRequest<CommandResult<ParticleEmitter>>
{
    public string TemplateId { get; }
    public IVector3 Position { get; }
    public decimal Speed { get; }

    public CreateParticleEmitterCommand(string templateId, IVector3 position, decimal speed)
    {
        TemplateId = templateId;
        Position = position;
        Speed = speed;
    }
}
