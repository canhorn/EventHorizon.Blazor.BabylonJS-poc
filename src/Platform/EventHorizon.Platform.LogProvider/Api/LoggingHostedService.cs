namespace EventHorizon.Platform.LogProvider.Api
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface LoggingHostedService
    {
        Task StartAsync(
            CancellationToken cancellationToken = default
        );

        Task StopAsync(
            CancellationToken cancellationToken = default
        );
    }
}
