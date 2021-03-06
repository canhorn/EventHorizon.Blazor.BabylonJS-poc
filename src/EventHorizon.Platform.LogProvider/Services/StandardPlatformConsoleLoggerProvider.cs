namespace EventHorizon.Platform.LogProvider.Services
{
    using EventHorizon.Platform.LogProvider.Api;
    using EventHorizon.Platform.LogProvider.Logger;
    using EventHorizon.Platform.LogProvider.Model;
    using Microsoft.Extensions.Logging;
    using System.Collections.Concurrent;

    public sealed class StandardPlatformConsoleLoggerProvider
        : ILoggerProvider
    {
        private readonly PlatformLoggerConfiguration _config;
        private readonly ClientDetailsEnrichmentService _logEnrichmentService;
        private readonly PendingLogQueue _pendingLogQueue;

        private readonly ConcurrentDictionary<string, PlatformConsoleLogger> _loggers =
            new ConcurrentDictionary<string, PlatformConsoleLogger>();

        public StandardPlatformConsoleLoggerProvider(
            PlatformLoggerConfiguration config,
            ClientDetailsEnrichmentService globalLogEnrichmentService,
            PendingLogQueue pendingLogQueue
        )
        {
            _config = config;
            _logEnrichmentService = globalLogEnrichmentService;
            _pendingLogQueue = pendingLogQueue;
        }

        public ILogger CreateLogger(
            string categoryName
        ) => _loggers.GetOrAdd(
            categoryName,
            name => new PlatformConsoleLogger(
                name,
                _config,
                _pendingLogQueue,
                _logEnrichmentService
            )
        );

        public void Dispose()
            => _loggers.Clear();
    }
}
