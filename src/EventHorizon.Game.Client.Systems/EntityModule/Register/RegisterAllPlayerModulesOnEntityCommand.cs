namespace EventHorizon.Game.Client.Systems.EntityModule.Register
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using MediatR;

    public class RegisterAllPlayerModulesOnEntityCommand
        : IRequest<StandardCommandResult>
    {
        public IPlayerEntity Entity { get; }

        public RegisterAllPlayerModulesOnEntityCommand(
            IPlayerEntity entity
        )
        {
            Entity = entity;
        }
    }
}
