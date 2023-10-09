namespace EventHorizon.Game.Client.Systems.Player.Modules.SelectedTracker.Api;

using System;

using EventHorizon.Game.Client.Engine.Systems.Module.Api;

public interface SelectedTrackerModule : IModule
{
    public static string MODULE_NAME { get; } = "SELECTED_TRACKER_MODULE_NAME";

    bool HasSelectedEntity { get; }
    long SelectedEntityId { get; }
}
