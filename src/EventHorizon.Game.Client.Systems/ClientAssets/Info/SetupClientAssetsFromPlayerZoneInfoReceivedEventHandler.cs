namespace EventHorizon.Game.Client.Systems.ClientAssets.Info;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Builder.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaded;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Info;

using MediatR;

public class SetupClientAssetsFromPlayerZoneInfoReceivedEventHandler
    : INotificationHandler<PlayerZoneInfoReceivedEvent>
{
    private readonly IMediator _mediator;
    private readonly ClientAssetState _clientAssetState;
    private readonly IBuilder<
        ClientAssetConfig,
        IDictionary<string, object>
    > _clientAssetConfigBuilder;
    private readonly ClientAssetConfigBuilderState _builderState;

    public SetupClientAssetsFromPlayerZoneInfoReceivedEventHandler(
        IMediator mediator,
        ClientAssetState clientAssetState,
        IBuilder<
            ClientAssetConfig,
            IDictionary<string, object>
        > clientAssetConfigBuilder,
        ClientAssetConfigBuilderState builderState
    )
    {
        _mediator = mediator;
        _clientAssetState = clientAssetState;
        _clientAssetConfigBuilder = clientAssetConfigBuilder;
        _builderState = builderState;
    }

    public async Task Handle(
        PlayerZoneInfoReceivedEvent notification,
        CancellationToken cancellationToken
    )
    {
        foreach (var clientAsset in notification.PlayerZoneInfo.ClientAssetList)
        {
            clientAsset.SetConfig(
                _builderState.Get(clientAsset.Type).Build(clientAsset.Data)
            );
            _clientAssetState.Set(clientAsset);
        }
        await _mediator.Publish(
            new ClientAssetsLoadedEvent(),
            cancellationToken
        );
    }
}
