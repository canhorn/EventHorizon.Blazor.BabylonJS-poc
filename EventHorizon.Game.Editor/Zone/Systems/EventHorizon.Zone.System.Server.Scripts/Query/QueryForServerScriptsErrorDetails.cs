namespace EventHorizon.Zone.System.Server.Scripts.Query
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Zone.System.Server.Scripts.Model;

    using MediatR;

    public struct QueryForServerScriptsErrorDetails
        : IRequest<CommandResult<ServerScriptsErrorDetailsResponse>>
    {
    }
}
