namespace EventHorizon.Game.Server.Asset
{
    using EventHorizon.Game.Server.Asset.Api;
    using EventHorizon.Game.Server.Asset.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class AssetServerStartupExtensions
    {
        public static IServiceCollection AddAssetServerAdminServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<AssetServerAdminService, SignalrAssetServerAdminService>()
        ;
    }
}
