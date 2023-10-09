namespace EventHorizon.Game.Editor;

using EventHorizon.Game.Editor.Core.Services.Api;
using EventHorizon.Game.Editor.Core.Services.Service;

using Microsoft.Extensions.DependencyInjection;

public static class EditorCoreExtensions
{
    public static IServiceCollection AddEditorCoreServices(
        this IServiceCollection services
    ) => services.AddSingleton<CoreAdminServices, SignalrCoreAdminServices>();
}
