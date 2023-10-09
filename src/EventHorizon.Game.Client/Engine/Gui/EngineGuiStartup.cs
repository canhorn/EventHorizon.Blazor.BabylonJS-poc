namespace EventHorizon.Game.Client.Engine.Gui;

using System;

using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Factory;
using EventHorizon.Game.Client.Engine.Gui.Platform;
using EventHorizon.Game.Client.Engine.Gui.State;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

using Microsoft.Extensions.DependencyInjection;

public static class EngineGuiStartup
{
    public static IServiceCollection AddEngineGuiServices(
        this IServiceCollection services
    ) =>
        services
            .AddSingleton<IServiceEntity, GuiInitializePlatformService>()
            .AddSingleton<IGuiControlFactory, BabylonJSGuiControlFactory>()
            .AddSingleton<
                IGuiControlChildrenState,
                StandardGuiControlChildrenState
            >()
            .AddSingleton<IGuiControlState, StandardGuiControlState>()
            .AddSingleton<
                IGuiControlTemplateState,
                StandardGuiControlTemplateState
            >()
            .AddSingleton<IGuiDefinitionState, StandardGuiDefinitionState>()
            .AddSingleton<IGuiLayoutDataState, StandardGuiLayoutDataState>();
}
