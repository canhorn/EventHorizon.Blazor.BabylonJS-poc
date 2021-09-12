namespace EventHorizon.Game.Editor
{
    using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
    using EventHorizon.Game.Editor.Zone.AdminClientAction.State;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Service;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Service;

    using Microsoft.Extensions.DependencyInjection;

    public static class EditorZoneExtensions
    {
        public static IServiceCollection AddEditorZoneServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<AdminClientActionState, StandardAdminClientActionState>()

            .AddSingleton<ZoneAdminServices, SignalrZoneAdminServices>()

            .AddSingleton<ZoneEditorServices, SignalrZoneEditorServices>()
        ;
    }
}
