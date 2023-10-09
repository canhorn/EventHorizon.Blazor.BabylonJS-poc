namespace EventHorizon.Game.Client.Systems;

using EventHorizon.Game.Client.Systems.Height.Api;
using EventHorizon.Game.Client.Systems.Height.State;

using Microsoft.Extensions.DependencyInjection;

public static class HeightSystemStartup
{
    public static IServiceCollection AddHeightSystemServices(
        this IServiceCollection services
    ) =>
        services
            .AddSingleton<HeightResolver, HeightResolver>()
            .AddSingleton<IHeightResolver>(
                services => services.GetRequiredService<HeightResolver>()
            )
            .AddSingleton<ISetHeightResolver>(
                services => services.GetRequiredService<HeightResolver>()
            );
}
