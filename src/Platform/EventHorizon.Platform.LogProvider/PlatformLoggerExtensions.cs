namespace EventHorizon.Platform.LogProvider;

using EventHorizon.Platform.LogProvider.Api;
using EventHorizon.Platform.LogProvider.Background;
using EventHorizon.Platform.LogProvider.Connection.Api;
using EventHorizon.Platform.LogProvider.Connection.Service;
using EventHorizon.Platform.LogProvider.Model;
using EventHorizon.Platform.LogProvider.Services;
using EventHorizon.Platform.LogProvider.State;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class PlatformLoggerExtensions
{
    public static ILoggingBuilder AddPlatformLogger(
        this ILoggingBuilder builder,
        PlatformLoggerConfiguration config
    )
    {
        var globalLogEnrichmentService =
            new GlobalClientDetailsEnrichmentService();
        var pendingLogQueue = new StandardPendingLogQueue();
        var platformConsoleLoggerProvider =
            new StandardPlatformConsoleLoggerProvider(
                config,
                globalLogEnrichmentService,
                pendingLogQueue
            );

        builder.Services
            .AddSingleton<PendingLogQueue>(pendingLogQueue)
            .AddSingleton<ClientDetailsEnrichmentService>(
                globalLogEnrichmentService
            );

        builder.AddProvider(platformConsoleLoggerProvider);

        builder.Services.AddSingleton<
            PlatformLoggerConnection,
            SignalrClientLoggerConnection
        >();

        builder.Services.AddSingleton<
            LoggingHostedService,
            StandardLoggingHostedService
        >();

        return builder;
    }
}
