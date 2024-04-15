namespace EventHorizon.Game.Editor.Client.Logging.Setup;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Authentication.Set;
using EventHorizon.Platform.LogProvider.Connection.Api;
using MediatR;

public class SetupLoggerConnectionWhenAccessTokenSetEventHandler
    : INotificationHandler<AccessTokenSetEvent>
{
    private readonly PlatformLoggerConnection _connection;

    public SetupLoggerConnectionWhenAccessTokenSetEventHandler(PlatformLoggerConnection connection)
    {
        _connection = connection;
    }

    public Task Handle(AccessTokenSetEvent notification, CancellationToken cancellationToken)
    {
        return _connection.Connect(notification.AccessToken, cancellationToken);
    }
}
