namespace EventHorizon.Game.Client.Systems;

using EventHorizon.Game.Client.Systems.Properties;
using Microsoft.Extensions.DependencyInjection;

public static class EntitySystemStartup
{
    public static IServiceCollection AddEntitySystemServices(this IServiceCollection services) =>
        services.AddEntitySystemPropertiesServices();
}
