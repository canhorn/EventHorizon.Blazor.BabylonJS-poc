namespace EventHorizon.Platform.LogProvider.Connection.Start;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Platform.LogProvider.Connection.Api;
using MediatR;

public class StartConnectionToLoggingServerCommandHandler
    : IRequestHandler<StartConnectionToLoggingServerCommand, StandardCommandResult>
{
    private readonly PlatformLoggerConnection _connection;

    public StartConnectionToLoggingServerCommandHandler(PlatformLoggerConnection connection)
    {
        _connection = connection;
    }

    public Task<StandardCommandResult> Handle(
        StartConnectionToLoggingServerCommand request,
        CancellationToken cancellationToken
    )
    {
        return _connection.Connect(request.AccessToken, cancellationToken);
    }
}
