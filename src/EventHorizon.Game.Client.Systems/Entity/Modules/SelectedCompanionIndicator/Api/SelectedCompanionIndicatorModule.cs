namespace EventHorizon.Game.Client.Systems.Entity.Modules.SelectedCompanionIndicator.Api;

using System;

using EventHorizon.Game.Client.Engine.Systems.Module.Api;

public interface SelectedCompanionIndicatorModule : IModule
{
    public static string MODULE_NAME =>
        "SELECTED_COMPANION_INDICATOR_MODULE_NAME";
}
