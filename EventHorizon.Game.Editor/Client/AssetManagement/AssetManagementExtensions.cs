namespace EventHorizon.Game.Editor;

using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Service;
using EventHorizon.Game.Editor.Client.AssetManagement.State;
using Microsoft.Extensions.DependencyInjection;

public static class AssetManagementExtensions
{
    public static IServiceCollection AddAssetManagementServices(this IServiceCollection services) =>
        services
            .AddScoped<AssetManagementState, StandardAssetManagementState>()
            .AddScoped<AssetFileManagement, RemoteAssetFileManagement>()
            .AddScoped<AssetServerService, RemoteAssetServerService>();
}
