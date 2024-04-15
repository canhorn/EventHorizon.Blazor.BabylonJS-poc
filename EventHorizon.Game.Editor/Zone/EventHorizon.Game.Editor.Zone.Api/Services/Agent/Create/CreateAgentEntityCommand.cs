namespace EventHorizon.Game.Editor.Zone.Services.Agent.Create;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using MediatR;

public struct CreateAgentEntityCommand : IRequest<CommandResult<IObjectEntityDetails>>
{
    public IObjectEntityDetails Entity { get; }

    public CreateAgentEntityCommand(IObjectEntityDetails entity)
    {
        Entity = entity;
    }
}
