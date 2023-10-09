namespace EventHorizon.Game.Client.Systems.ClientAssets.Platform;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Dispose;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Api.Mesh;

using MediatR;

public class CleanupClientAssetsOnDisposeOfEngineEventHandler
    : INotificationHandler<DisposeOfEngineEvent>
{
    private readonly ClientAssetState _clientAssetState;
    private readonly ClientAssetInstanceState _clientAssetInstanceState;
    private readonly ClientAssetMeshCache _clientAssetMeshCache;

    public CleanupClientAssetsOnDisposeOfEngineEventHandler(
        ClientAssetState clientAssetState,
        ClientAssetInstanceState clientAssetInstanceState,
        ClientAssetMeshCache clientAssetMeshCache
    )
    {
        _clientAssetState = clientAssetState;
        _clientAssetInstanceState = clientAssetInstanceState;
        _clientAssetMeshCache = clientAssetMeshCache;
    }

    public Task Handle(
        DisposeOfEngineEvent notification,
        CancellationToken cancellationToken
    )
    {
        _clientAssetMeshCache.Clear();
        _clientAssetState.Reset();
        _clientAssetInstanceState.Clear();

        return Task.CompletedTask;
    }
}
