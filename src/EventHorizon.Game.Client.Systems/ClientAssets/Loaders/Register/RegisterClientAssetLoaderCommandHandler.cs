namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Register;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api;
using MediatR;

public class RegisterClientAssetLoaderCommandHandler
    : IRequestHandler<RegisterClientAssetLoaderCommand, StandardCommandResult>
{
    private readonly ClientAssetLoaderState _state;

    public RegisterClientAssetLoaderCommandHandler(ClientAssetLoaderState state)
    {
        _state = state;
    }

    public Task<StandardCommandResult> Handle(
        RegisterClientAssetLoaderCommand request,
        CancellationToken cancellationToken
    )
    {
        _state.Set(request.Id, request.Loader);

        return new StandardCommandResult().FromResult();
    }
}
