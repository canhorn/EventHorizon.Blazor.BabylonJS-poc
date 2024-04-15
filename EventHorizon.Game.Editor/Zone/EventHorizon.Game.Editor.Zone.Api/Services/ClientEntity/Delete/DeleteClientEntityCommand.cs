namespace EventHorizon.Game.Editor.Zone.Services.ClientEntity.Delete;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct DeleteClientEntityCommand : IRequest<StandardCommandResult>
{
    public string EntityId { get; }

    public DeleteClientEntityCommand(string entityId)
    {
        EntityId = entityId;
    }
}
