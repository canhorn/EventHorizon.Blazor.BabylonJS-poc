namespace EventHorizon.Platform.LogProvider.Connection.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Platform.LogProvider.Connection.Model;

    public interface PlatformLoggerConnection
    {
        bool IsConnected { get; }

        Task<StandardCommandResult> Connect(
            string accessToken,
            CancellationToken cancellationToken
        );

        Task<StandardCommandResult> LogMessage(
            ClientLogMessage message,
            CancellationToken cancellationToken
        );
    }
}
