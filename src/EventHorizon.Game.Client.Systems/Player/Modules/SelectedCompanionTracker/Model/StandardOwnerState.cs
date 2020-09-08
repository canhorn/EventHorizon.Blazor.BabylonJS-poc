namespace EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Model
{
    using System;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api;

    public class StandardOwnerState
        : OwnerState
    {
        public string OwnerId { get; set; } = string.Empty;
    }
}
