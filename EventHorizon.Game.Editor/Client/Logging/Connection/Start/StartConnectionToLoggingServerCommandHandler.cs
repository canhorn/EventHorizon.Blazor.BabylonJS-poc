namespace EventHorizon.Game.Editor.Client.Logging.Connection.Start
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Logging.Connection.Api;
    using MediatR;

    public class StartConnectionToLoggingServerCommandHandler
        : IRequestHandler<StartConnectionToLoggingServerCommand, StandardCommandResult>
    {
        private readonly ClientLoggerConnection _connection;

        public StartConnectionToLoggingServerCommandHandler(
            ClientLoggerConnection connection
        )
        {
            _connection = connection;
        }

        public Task<StandardCommandResult> Handle(
            StartConnectionToLoggingServerCommand request, 
            CancellationToken cancellationToken
        )
        {
            return _connection.Connect(
                request.AccessToken,
                cancellationToken
            );
        }
    }
}
