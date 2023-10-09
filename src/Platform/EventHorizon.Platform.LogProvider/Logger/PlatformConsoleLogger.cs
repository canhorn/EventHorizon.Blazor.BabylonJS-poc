namespace EventHorizon.Platform.LogProvider.Logger;

using System;
using System.Collections.Generic;
using System.Text.Json;

using EventHorizon.Platform.LogProvider.Api;
using EventHorizon.Platform.LogProvider.Model;

using Microsoft.Extensions.Logging;

public class PlatformConsoleLogger : ILogger
{
    private static readonly JsonSerializerOptions JSON_OPTIONS =
        new JsonSerializerOptions { WriteIndented = true };

    private readonly string _name;
    private readonly PlatformLoggerConfiguration _config;
    private readonly PendingLogQueue _pendingLogQueue;
    private readonly ClientDetailsEnrichmentService _logEnrichmentService;

    public PlatformConsoleLogger(
        string name,
        PlatformLoggerConfiguration config,
        PendingLogQueue pendingLogQueue,
        ClientDetailsEnrichmentService logEnrichmentService
    )
    {
        _name = name;
        _config = config;
        _pendingLogQueue = pendingLogQueue;
        _logEnrichmentService = logEnrichmentService;
    }

    public IDisposable BeginScope<TState>(TState state) =>
        NoOpDisposable.Instance;

    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter
    )
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var stateAsDictionary = new Dictionary<string, object>();
        var timestamp = DateTimeOffset.UtcNow;

        if (state is IEnumerable<KeyValuePair<string, object>> typedState)
        {
            var map = new Dictionary<string, object>(typedState);
            foreach (var mapValue in map)
            {
                stateAsDictionary[$"Client.{mapValue.Key}"] = mapValue.Value;
            }
        }
        _logEnrichmentService.EnrichReference(stateAsDictionary);
        stateAsDictionary["Client.Timestamp"] = $"{timestamp:O}";
        stateAsDictionary["Client.SourceContext"] = _name;

        if (exception != null)
        {
            stateAsDictionary["Client.Exception"] = exception.ToString();
        }

        _pendingLogQueue.Add(
            new PlatformLogMessage
            {
                Level = $"{logLevel}",
                Message = formatter(state, null!),
                Args = stateAsDictionary
            }
        );

        if (_config.DebugView)
        {
            Console.WriteLine(
                $"[{timestamp:O}] [{logLevel}] [{_name}[{eventId.Id}]] {formatter(state, exception!)}"
            );
            Console.WriteLine(state?.GetType()?.FullName ?? string.Empty);
            Console.WriteLine(exception?.ToString());
            Console.WriteLine(
                JsonSerializer.Serialize(stateAsDictionary, JSON_OPTIONS)
            );
            Console.WriteLine();
        }
    }

    private class NoOpDisposable : IDisposable
    {
        public static NoOpDisposable Instance = new NoOpDisposable();

        public void Dispose() { }
    }
}
