namespace EventHorizon.Game.Client.Systems
{
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Client.Account;
    using EventHorizon.Game.Client.Systems.Connection;
    using EventHorizon.Game.Client.Systems.Local.Scenes;
    using EventHorizon.Game.Client.Systems.Map;
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
            .AddMapSystemServices()
        ;
    }
}
