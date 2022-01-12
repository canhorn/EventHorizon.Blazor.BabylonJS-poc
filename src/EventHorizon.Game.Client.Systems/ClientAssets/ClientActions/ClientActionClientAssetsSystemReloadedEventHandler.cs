namespace EventHorizon.Game.Client.Systems.ClientAssets.ClientActions;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaded;

using MediatR;

public class ClientActionClientAssetsSystemReloadedEventHandler
    : INotificationHandler<ClientActionClientAssetsSystemReloadedEvent>
{
    private readonly IMediator _mediator;
    private readonly ClientAssetState _clientAssetState;
    private readonly ClientAssetConfigBuilderState _builderState;

    public ClientActionClientAssetsSystemReloadedEventHandler(
        IMediator mediator,
        ClientAssetState clientAssetState,
        ClientAssetConfigBuilderState builderState
    )
    {
        _mediator = mediator;
        _clientAssetState = clientAssetState;
        _builderState = builderState;
    }

    public async Task Handle(
        ClientActionClientAssetsSystemReloadedEvent notification,
        CancellationToken cancellationToken
    )
    {
        _clientAssetState.Reset();

        foreach (
            var clientAsset in notification.ClientAssetList
        )
        {
            clientAsset.SetConfig(
                _builderState
                    .Get(clientAsset.Type)
                    .Build(clientAsset.Data)
            );
            _clientAssetState.Set(clientAsset);
        }

        await _mediator.Publish(
            new ClientAssetsLoadedEvent(),
            cancellationToken
        );
    }
}
