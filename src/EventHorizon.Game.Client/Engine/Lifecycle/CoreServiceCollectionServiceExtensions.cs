namespace Microsoft.Extensions.DependencyInjection;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;

using Microsoft.Extensions.DependencyInjection;

public static class CoreServiceCollectionServiceExtensions
{
    public static IServiceCollection AddServiceEntity<
        TService,
        TImplementation
    >(this IServiceCollection services)
        where TService : class, IServiceEntity
        where TImplementation : class, TService
    {
        return services
            .AddSingleton<TService, TImplementation>()
            .AddSingleton<IServiceEntity>(
                services => services.GetRequiredService<TService>()
            );
    }
}
