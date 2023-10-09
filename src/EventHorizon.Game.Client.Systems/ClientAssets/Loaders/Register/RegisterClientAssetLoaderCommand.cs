namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Register;

using System;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api;

using MediatR;

public struct RegisterClientAssetLoaderCommand : IRequest<StandardCommandResult>
{
    public string Id { get; }
    public ClientAssetLoader Loader { get; }

    public RegisterClientAssetLoaderCommand(string id, ClientAssetLoader loader)
    {
        Id = id;
        Loader = loader;
    }
}
