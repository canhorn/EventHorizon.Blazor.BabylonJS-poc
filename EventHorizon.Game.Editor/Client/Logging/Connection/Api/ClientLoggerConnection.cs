namespace EventHorizon.Game.Editor.Client.Logging.Connection.Api
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Logging.Connection.Model;

    public interface ClientLoggerConnection
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
