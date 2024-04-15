namespace EventHorizon.Platform.LogProvider.Background;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Factory.Api;
using EventHorizon.Game.Client.Core.Timer.Api;
using EventHorizon.Platform.LogProvider.Api;
using EventHorizon.Platform.LogProvider.Process;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class StandardLoggingHostedService : LoggingHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IIntervalTimerService? _intervalTimerService;

    public StandardLoggingHostedService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        _intervalTimerService = scope
            .ServiceProvider.GetRequiredService<IFactory<IIntervalTimerService>>()
            .Create()
            .Setup(100, DoWork)
            .Start();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _intervalTimerService?.Dispose();

        return Task.CompletedTask;
    }

    private async Task DoWork()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new ProcesssPlatformLogMessageCommand());
    }

    public void Dispose()
    {
        _intervalTimerService?.Dispose();
    }
}
