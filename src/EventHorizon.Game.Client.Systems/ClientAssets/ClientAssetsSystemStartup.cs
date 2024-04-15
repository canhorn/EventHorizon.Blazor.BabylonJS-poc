namespace EventHorizon.Game.Client.Systems;

using System.Collections.Generic;
using EventHorizon.Game.Client.Core.Builder.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Api.Mesh;
using EventHorizon.Game.Client.Systems.ClientAssets.Builder;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.State;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.State;
using EventHorizon.Game.Client.Systems.ClientAssets.Platform;
using EventHorizon.Game.Client.Systems.ClientAssets.State;
using Microsoft.Extensions.DependencyInjection;

public static class ClientAssetsSystemStartup
{
    public static IServiceCollection AddClientAssetsSystemServices(
        this IServiceCollection services
    ) =>
        services
            .AddSingleton<IServiceEntity, BabylonJSClientAssetsInitializePlatformService>()
            .AddSingleton<
                IBuilder<ClientAssetConfig, IDictionary<string, object>>,
                StandardClientAssetConfigBuilder
            >()
            .AddSingleton<ClientAssetState, StandardClientAssetState>()
            .AddSingleton<ClientAssetInstanceState, StandardClientAssetInstanceState>()
            .AddSingleton<ClientAssetMeshCache, StandardClientAssetMeshCache>()
            // Config
            .AddSingleton<ClientAssetConfigBuilderState, StandardClientAssetConfigBuilderState>()
            // Loaders
            .AddSingleton<ClientAssetLoaderState, StandardClientAssetLoaderState>();
}
