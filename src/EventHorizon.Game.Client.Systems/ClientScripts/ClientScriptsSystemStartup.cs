namespace EventHorizon.Game.Client.Systems
{
    using System;
    using EventHorizon.Game.Client.Engine.Scripting.Services;
    using EventHorizon.Game.Client.Systems.ClientScripts.Api;
    using EventHorizon.Game.Client.Systems.ClientScripts.Services;
    using EventHorizon.Game.Client.Systems.ClientScripts.State;
    using Microsoft.Extensions.DependencyInjection;

    public static class ClientScriptsSystemStartup
    {
        public static IServiceCollection AddClientScriptsSystemServices(
            this IServiceCollection services
        ) => services
            .AddSingleton<ClientScriptsState, StandardClientScriptsState>()

            .AddTransient<ScriptServices, StandardScriptServices>()
        ;
    }
}
