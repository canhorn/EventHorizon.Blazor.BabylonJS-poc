namespace EventHorizon.Game.Editor.Client.Authentication.Query
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using MediatR;

    public class QueryForSessionValues
        : IRequest<CommandResult<SessionValues>>
    {
    }
}
