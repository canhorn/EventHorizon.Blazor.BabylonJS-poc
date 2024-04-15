namespace EventHorizon.Zone.Systems.ClientAssets.Delete;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct DeleteClientAssetCommand : IRequest<StandardCommandResult>
{
    public string Id { get; }

    public DeleteClientAssetCommand(string id)
    {
        Id = id;
    }
}
