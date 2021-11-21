namespace EventHorizon.Game.Editor.Client.ErrorBoundary.Logger;

using System;

using EventHorizon.Game.Editor.Client.ErrorBoundary.Api;

using Microsoft.Extensions.Logging;

public class ErrorBoundaryLogger
    : ILogger
{
    private readonly ErrorBoundaryService _errorBoundaryService;

    public ErrorBoundaryLogger(
        string _,
        ErrorBoundaryService errorBoundaryService
    )
    {
        _errorBoundaryService = errorBoundaryService;
    }

    public IDisposable BeginScope<TState>(
        TState state
    ) => NoOpDisposable.Instance;

    /// <summary>
    /// We are always enabled
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(
        LogLevel logLevel
    ) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception, string> formatter
    )
    {
        if (exception is not null)
        {
            _errorBoundaryService.AddException(
                exception,
                formatter(state, exception)
            );
        }
    }

    private class NoOpDisposable
        : IDisposable
    {
        public static NoOpDisposable Instance = new();

        public void Dispose() { }
    }
}
