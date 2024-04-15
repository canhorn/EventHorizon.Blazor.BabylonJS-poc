namespace EventHorizon.Game.Client.Systems;

using System;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Systems.Dialog.Api;
using EventHorizon.Game.Client.Systems.Dialog.Platform;
using EventHorizon.Game.Client.Systems.Dialog.State;
using Microsoft.Extensions.DependencyInjection;

public static class DialogSystemStartup
{
    public static IServiceCollection AddDialogSystemServices(this IServiceCollection services) =>
        services
            .AddSingleton<DialogState, StandardDialogState>()
            .AddSingleton<IServiceEntity, DialogInitializePlatformService>();
}
