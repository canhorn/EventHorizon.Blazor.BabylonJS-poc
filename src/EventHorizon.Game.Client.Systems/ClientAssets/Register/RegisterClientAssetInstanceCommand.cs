namespace EventHorizon.Game.Client.Systems.ClientAssets.Register;

using System;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;

using MediatR;

public class RegisterClientAssetInstanceCommand
    : IRequest<StandardCommandResult>
{
    public ClientAssetInstance Instance { get; }

    public RegisterClientAssetInstanceCommand(ClientAssetInstance instance)
    {
        Instance = instance;
    }
}
