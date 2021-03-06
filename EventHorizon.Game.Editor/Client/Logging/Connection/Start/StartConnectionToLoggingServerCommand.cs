namespace EventHorizon.Game.Editor.Client.Logging.Connection.Start
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public struct StartConnectionToLoggingServerCommand
        : IRequest<StandardCommandResult>
    {
        public string AccessToken { get; }

        public StartConnectionToLoggingServerCommand(
            string accessToken
        )
        {
            AccessToken = accessToken;
        }
    }
}
