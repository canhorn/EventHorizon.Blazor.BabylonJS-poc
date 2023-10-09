namespace EventHorizon.Game.Editor.Core.Services.Connect;

using System;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public class StartConnectionToCoreServerCommand
    : IRequest<StandardCommandResult>
{
    public string AccessToken { get; }

    public StartConnectionToCoreServerCommand(string accessToken)
    {
        AccessToken = accessToken;
    }
}
