namespace EventHorizon.Game.Editor;

using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Api;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.State;

using Microsoft.Extensions.DependencyInjection;

public static class ZoneArtifactManagementStartup
{
    public static IServiceCollection AddZoneArtifactManagementServices(
        this IServiceCollection services
    ) =>
        services.AddSingleton<
            ZoneArtifactManagementState,
            StandardZoneArtifactManagementState
        >();
}
