namespace EventHorizon.Zone.Systems.DataStorage.Delete;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct DeleteDataStoreValueCommand : IRequest<StandardCommandResult>
{
    public string Key { get; }

    public DeleteDataStoreValueCommand(string key)
    {
        Key = key;
    }
}
