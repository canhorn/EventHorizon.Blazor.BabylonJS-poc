namespace EventHorizon.Game.Editor.Client.Authentication.Fill
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using MediatR;

    public struct FillSessionValuesCommand
        : IRequest<CommandResult<SessionValues>>
    {
    }
}
