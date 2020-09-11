namespace EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api
{
    using System;
    using EventHorizon.Game.Client.Engine.Systems.Module.Api;

    public interface SelectedCompanionTrackerModule
        : IModule
    {
        public static string MODULE_NAME { get; } = "SELECTED_COMPANION_TRACKER_MODULE_NAME";

        bool HasSelectedEntity { get; }
        long SelectedEntityId { get; }
    }
}
