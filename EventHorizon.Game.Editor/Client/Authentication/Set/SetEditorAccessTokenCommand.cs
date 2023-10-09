namespace EventHorizon.Game.Editor.Client.Authentication.Set;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct SetEditorAccessTokenCommand : IRequest<StandardCommandResult>
{
    public string AccessToken { get; }

    public SetEditorAccessTokenCommand(string accessToken)
    {
        AccessToken = accessToken;
    }
}
