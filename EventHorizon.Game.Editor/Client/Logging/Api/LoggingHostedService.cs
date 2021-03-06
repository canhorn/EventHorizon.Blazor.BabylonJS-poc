namespace EventHorizon.Game.Editor.Client.Logging.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
