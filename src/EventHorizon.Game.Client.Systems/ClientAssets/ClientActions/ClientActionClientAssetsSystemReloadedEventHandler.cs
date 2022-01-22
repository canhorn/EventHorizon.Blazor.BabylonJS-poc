namespace EventHorizon.Game.Client.Systems.ClientAssets.ClientActions;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaded;
using EventHorizon.Game.Client.Systems.ClientAssets.Reload;

using MediatR;

public class ClientActionClientAssetsSystemReloadedEventHandler
    : INotificationHandler<ClientActionClientAssetsSystemReloadedEvent>
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;
    private readonly ClientAssetState _clientAssetState;
    private readonly ClientAssetConfigBuilderState _builderState;

    public ClientActionClientAssetsSystemReloadedEventHandler(
        ISender sender,
        IPublisher publisher,
        ClientAssetState clientAssetState,
        ClientAssetConfigBuilderState builderState
    )
    {
        _sender = sender;
        _publisher = publisher;
        _clientAssetState = clientAssetState;
        _builderState = builderState;
    }

    public async Task Handle(
        ClientActionClientAssetsSystemReloadedEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _publisher.Publish(
            new ReloadingClientAssetsEvent(),
            cancellationToken
        );

        _clientAssetState.Reset();

        foreach (var clientAsset in notification.ClientAssetList)
        {
            clientAsset.SetConfig(
                _builderState.Get(clientAsset.Type).Build(clientAsset.Data)
            );
            _clientAssetState.Set(clientAsset);
        }

        await _publisher.Publish(
            new ClientAssetsLoadedEvent(),
            cancellationToken
        );
    }
}
