namespace EventHorizon.Game.Editor.Client
{
    using EventHorizon.Game.Editor.Client.Logging.Api;
    using EventHorizon.Game.Editor.Client.Logging.Background;
    using EventHorizon.Game.Editor.Client.Logging.Connection.Api;
    using EventHorizon.Game.Editor.Client.Logging.Connection.Service;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class PlatformConsoleLoggerExtensions
    {
        public static ILoggingBuilder AddPlatformConsoleLogger(
            this ILoggingBuilder builder,
            PlatformConsoleLoggerConfiguration config
        )
        {
            var globalLogEnrichmentService = new GlobalClientDetailsEnrichmentService();
            var pendingLogQueue = new StandardPendingLogQueue();
            var platformConsoleLoggerProvider = new StandardPlatformConsoleLoggerProvider(
                config,
                globalLogEnrichmentService,
                pendingLogQueue
            );

            builder.Services.AddSingleton<PendingLogQueue>(
                pendingLogQueue
            ).AddSingleton<ClientDetailsEnrichmentService>(
                globalLogEnrichmentService
            );

            builder.AddProvider(
                platformConsoleLoggerProvider
            );

            builder.Services.AddSingleton<ClientLoggerConnection, SignalrClientLoggerConnection>();

            builder.Services.AddSingleton<LoggingHostedService, StandardLoggingHostedService>();

            return builder;
        }
    }
}
