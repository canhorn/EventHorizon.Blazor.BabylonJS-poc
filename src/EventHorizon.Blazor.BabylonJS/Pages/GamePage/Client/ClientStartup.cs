namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Client
{
    using EventHorizon.Game.Client.Systems;
    using Microsoft.Extensions.DependencyInjection;

    public static class ClientStartup
    {
        public static IServiceCollection AddClientServices(
            this IServiceCollection services
        ) => services
            .AddClientSystemsServices()
        ;
    }
}
