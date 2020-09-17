namespace EventHorizon.Game.Client.Systems
{
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Client.Account;
    // TODO: Move SystemsStartups into Systems Root Namespace
    using EventHorizon.Game.Client.Systems.ClientAssets;
    using EventHorizon.Game.Client.Systems.ClientScripts;
    using EventHorizon.Game.Client.Systems.Connection;
    using EventHorizon.Game.Client.Systems.Dialog;
    using EventHorizon.Game.Client.Systems.Entity;
    using EventHorizon.Game.Client.Systems.EntityModule;
    using EventHorizon.Game.Client.Systems.Height;
    using EventHorizon.Game.Client.Systems.Local.Scenes;
    using EventHorizon.Game.Client.Systems.Map;
    using EventHorizon.Game.Client.Systems.Player;
    using EventHorizon.Game.Client.Systems.ServerModule;
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
