namespace EventHorizon.Platform.ErrorBoundary;

using EventHorizon.Game.Editor.Client.ErrorBoundary.Api;
using EventHorizon.Game.Editor.Client.ErrorBoundary.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class ErrorBoundaryExtensions
{
    public static IServiceCollection AddErrorBoundary(this IServiceCollection services)
    {
        var errorBoundaryService = new StandardErrorBoundaryService();
        var platformConsoleLoggerProvider = new StandardErrorBoundaryLoggerProvider(
            errorBoundaryService
        );

        services.AddSingleton<ErrorBoundaryService>(errorBoundaryService);

        services.AddLogging(config => config.AddProvider(platformConsoleLoggerProvider));

        return services;
    }
}
