namespace EventHorizon.Zone.Systems.ClientAssets.Create;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.ClientAssets.Model;

using MediatR;

public struct CreateClientAssetCommand : IRequest<StandardCommandResult>
{
    public ClientAsset ClientAsset { get; }

    public CreateClientAssetCommand(ClientAsset clientAsset)
    {
        ClientAsset = clientAsset;
    }
}
