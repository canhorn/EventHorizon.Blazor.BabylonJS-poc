namespace EventHorizon.Game.Client.Systems.ClientAssets.Dispose;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using MediatR;

public class DisposeOfClientAssetInstanceCommandHandler
    : IRequestHandler<DisposeOfClientAssetInstanceCommand, StandardCommandResult>
{
    private readonly ClientAssetInstanceState _store;

    public DisposeOfClientAssetInstanceCommandHandler(ClientAssetInstanceState store)
    {
        _store = store;
    }

    public Task<StandardCommandResult> Handle(
        DisposeOfClientAssetInstanceCommand request,
        CancellationToken cancellationToken
    )
    {
        var clientInstanceAsset = _store.Get(request.AssetInstanceId);
        if (clientInstanceAsset.HasValue)
        {
            clientInstanceAsset.Value.Dispose();
            _store.Remove(request.AssetInstanceId);
        }

        return new StandardCommandResult().FromResult();
    }
}
