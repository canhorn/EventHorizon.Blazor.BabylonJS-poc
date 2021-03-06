namespace EventHorizon.Game.Editor.Client.Logging.Background
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Editor.Client.Logging.Api;
    using EventHorizon.Game.Editor.Client.Logging.Process;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class StandardLoggingHostedService
        : LoggingHostedService
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IIntervalTimerService? _intervalTimerService;

        public StandardLoggingHostedService(
            ILogger<LoggingHostedService> logger,
            IServiceScopeFactory serviceScopeFactory
        )
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(
            CancellationToken stoppingToken
        )
        {
            _logger.LogInformation("Logging Hosted Service is Starting.");

            using var scope = _serviceScopeFactory.CreateScope();
            _intervalTimerService = scope.ServiceProvider
                .GetRequiredService<IFactory<IIntervalTimerService>>()
                .Create()
                .Setup(
                    100,
                    DoWork
                ).Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(
            CancellationToken stoppingToken
        )
        {
            _logger.LogInformation("Logging Hosted Service is Stopping.");

            _intervalTimerService?.Dispose();

            return Task.CompletedTask;
        }

        private async Task DoWork()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(
                new ProcesssPlatformLogMessageCommand()
            );
        }

        public void Dispose()
        {
            _logger.LogInformation("Logging Hosted Service Disposed.");

            _intervalTimerService?.Dispose();
        }
    }
}
