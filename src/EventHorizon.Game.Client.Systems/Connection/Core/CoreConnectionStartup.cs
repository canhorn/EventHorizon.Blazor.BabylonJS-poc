namespace EventHorizon.Game.Client.Systems.Connection.Core;

using EventHorizon.Game.Client.Systems.Connection.Core.Api;
using EventHorizon.Game.Client.Systems.Connection.Core.State;
using Microsoft.Extensions.DependencyInjection;

public static class CoreConnectionStartup
{
    public static IServiceCollection AddCoreConnectionServices(this IServiceCollection services) =>
        services.AddSingleton<CoreConnectionState, SignalRCoreConnectionState>();
}
