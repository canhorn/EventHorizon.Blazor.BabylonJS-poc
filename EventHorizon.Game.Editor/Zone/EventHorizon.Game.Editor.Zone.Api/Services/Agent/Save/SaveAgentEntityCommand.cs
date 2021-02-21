namespace EventHorizon.Game.Editor.Zone.Services.Agent.Save
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using MediatR;

    public struct SaveAgentEntityCommand
        : IRequest<CommandResult<IObjectEntityDetails>>
    {
        public IObjectEntityDetails Entity { get; }

        public SaveAgentEntityCommand(
            IObjectEntityDetails entity
        )
        {
            Entity = entity;
        }
    }
}
