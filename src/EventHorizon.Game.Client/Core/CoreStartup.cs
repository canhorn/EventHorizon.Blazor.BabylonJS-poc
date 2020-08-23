namespace EventHorizon.Game.Client.Core
{
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Factory.Model;
    using EventHorizon.Game.Client.Core.I18n.Api;
    using EventHorizon.Game.Client.Core.I18n.State;
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
            // I18n Services
            .AddSingleton<StandardI18nService>()
            .AddSingleton<II18nService>(services => services.GetRequiredService<StandardI18nService>())
            .AddSingleton<ILocalizer>(services => services.GetRequiredService<StandardI18nService>())

            .AddSingleton<IPlatformMonitor, StandardPlatformMonitor>()
            .AddSingleton<IFactory<ITimerService>>(new StandardFactory<ITimerService>(() => new TimerService()))
            .AddSingleton<IFactory<IIntervalTimerService>>(new StandardFactory<IIntervalTimerService>(() => new IntervalTimerService()))
        ;
    }
}
