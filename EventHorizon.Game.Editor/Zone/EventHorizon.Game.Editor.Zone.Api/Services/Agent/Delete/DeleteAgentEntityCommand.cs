namespace EventHorizon.Game.Editor.Zone.Services.Agent.Delete;

using System;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public class DeleteAgentEntityCommand : IRequest<StandardCommandResult>
{
    public string EntityId { get; }

    public DeleteAgentEntityCommand(string entityId)
    {
        EntityId = entityId;
    }
}
