namespace EventHorizon.Game.Editor.Client.Logging.Setup
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Authentication.Set;
    using EventHorizon.Game.Editor.Client.Logging.Connection.Api;
    using MediatR;

    public class SetupLoggerConnectionWhenAccessTokenSetEventHandler
        : INotificationHandler<AccessTokenSetEvent>
    {
        private readonly ClientLoggerConnection _connection;

        public SetupLoggerConnectionWhenAccessTokenSetEventHandler(
            ClientLoggerConnection connection
        )
        {
            _connection = connection;
        }

        public Task Handle(
            AccessTokenSetEvent notification,
            CancellationToken cancellationToken
        )
        {
            return _connection.Connect(
                notification.AccessToken,
                cancellationToken
            );
        }
    }
}
