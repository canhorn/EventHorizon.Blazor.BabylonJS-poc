namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Register;

using System;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;
using MediatR;

public struct RegisterClientAssetConfigTypeBuilderCommand : IRequest<StandardCommandResult>
{
    public string Type { get; }
    public ClientAssetConfigTypeBuilder Builder { get; }

    public RegisterClientAssetConfigTypeBuilderCommand(
        string type,
        ClientAssetConfigTypeBuilder builder
    )
    {
        Type = type;
        Builder = builder;
    }
}
