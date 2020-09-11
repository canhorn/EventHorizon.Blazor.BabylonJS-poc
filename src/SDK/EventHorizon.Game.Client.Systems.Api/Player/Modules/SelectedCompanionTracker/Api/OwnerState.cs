namespace EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api
{
    using System;

    public interface OwnerState
    {
        public static string NAME { get; } = "ownerState";

        public string OwnerId { get; }
    }
}
