namespace EventHorizon.Game.Client.Systems.ClientAssets
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Builder;
    using EventHorizon.Game.Client.Systems.ClientAssets.Store;
    using Microsoft.Extensions.DependencyInjection;

    public static class ClientAssetsSystemStartup
    {
        public static IServiceCollection AddClientAssetsSystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<IBuilder<IClientAssetConfig, IDictionary<string, object>>, ClientAssetConfigBuilder>()

            .AddSingleton<IClientAssetInstanceStore, StandardClientAssetInstanceStore>()
            .AddSingleton<IClientAssetMeshCache, StandardClientAssetMeshCache>()
            .AddSingleton<IClientAssetStore, StandardClientAssetStore>()
        ;
    }
}
