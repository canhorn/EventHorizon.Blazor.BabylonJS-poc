namespace EventHorizon.Game.Client.Core
{
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Factory.Model;
    using EventHorizon.Game.Client.Core.Monitoring.Api;
    using EventHorizon.Game.Client.Core.Monitoring.Model;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Core.Timer.Model;
    using Microsoft.Extensions.DependencyInjection;

    public static class CoreStartup
    {
        public static IServiceCollection AddCoreServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IPlatformMonitor, StandardPlatformMonitor>()
            .AddSingleton<IFactory<ITimerService>>(new StandardFactory<ITimerService>(() => new TimerService()))
            .AddSingleton<IFactory<IIntervalTimerService>>(new StandardFactory<IIntervalTimerService>(() => new IntervalTimerService()))
        ;
    }
}
