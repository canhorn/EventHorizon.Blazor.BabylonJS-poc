namespace EventHorizon.Game.Server.Asset;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Model;
using EventHorizon.Game.Server.Asset.Query;
using EventHorizon.Game.Server.Asset.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class AssetServerStartupExtensions
{
    public static IServiceCollection AddAssetServerAdminServices(
        this IServiceCollection services
    ) => services.AddSingleton<AssetServerAdminService, SignalrAssetServerAdminService>();
    // TODO: Look at updating the generated code.
    // .AddScoped<
    //     IPipelineBehavior<
    //         QueryForFileManagementAssets,
    //         CommandResult<FileManagementAssets>
    //     >,
    //     QueryForFileManagementAssetsGeneratedCachedBehavior
    // >();
}
