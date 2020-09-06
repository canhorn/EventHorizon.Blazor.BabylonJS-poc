namespace EventHorizon.Game.Client.Engine.Systems.ClientAction
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class EngineClientActionSystemStartup
    {
        public static IServiceCollection AddEngineClientActionSystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<ClientActionState, StandardClientActionState>()
        ;
    }
}
