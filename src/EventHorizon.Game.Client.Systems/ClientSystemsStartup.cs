namespace EventHorizon.Game.Client.Systems
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class ClientSystemsStartup
    {
        public static IServiceCollection AddClientSystemsServices(
            this IServiceCollection services
        ) => services
            .AddMediatR(typeof(ClientSystemsStartup).Assembly)
            // Local Systems
            .AddClientScenesServices()

            // Core Server Systems
            .AddAccountSystemServices()
            .AddConnectionSystemServices()

            // Zone Server Systems
            .AddEntitySystemServices()
            .AddPlayerSystemServices()
            .AddMapSystemServices()
            .AddHeightSystemServices()
            .AddClientAssetsSystemServices()
            .AddClientScriptsSystemServices()
            .AddEntityModuleSystemServices()
            .AddServerModuleSystemServices()
            .AddDialogSystemServices()
        ;
    }
}
