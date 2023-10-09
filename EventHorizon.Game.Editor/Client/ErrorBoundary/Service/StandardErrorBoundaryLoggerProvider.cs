namespace EventHorizon.Game.Editor.Client.ErrorBoundary.Service;

using System.Collections.Concurrent;

using EventHorizon.Game.Editor.Client.ErrorBoundary.Api;
using EventHorizon.Game.Editor.Client.ErrorBoundary.Logger;

using Microsoft.Extensions.Logging;

public class StandardErrorBoundaryLoggerProvider : ILoggerProvider
{
    private readonly ErrorBoundaryService _errorBoundaryService;

    private readonly ConcurrentDictionary<
        string,
        ErrorBoundaryLogger
    > _loggers = new();

    public StandardErrorBoundaryLoggerProvider(
        ErrorBoundaryService errorBoundaryService
    )
    {
        _errorBoundaryService = errorBoundaryService;
    }

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(
            categoryName,
            name => new ErrorBoundaryLogger(name, _errorBoundaryService)
        );

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        => _loggers.Clear();
}
