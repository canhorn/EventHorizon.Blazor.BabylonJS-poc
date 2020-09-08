namespace EventHorizon.Game.Client.Engine.Particle.Stop
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public struct StopParticleSystemCommand
        : IRequest<StandardCommandResult>
    {
        public long Id { get; }

        public StopParticleSystemCommand(
            long id
        )
        {
            Id = id;
        }
    }
}
