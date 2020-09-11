namespace EventHorizon.Game.Client.Systems.Entity.Modules.InteractionIndicator.Api
{
    using System;

    public interface InteractionState
    {
        public static string NAME => "interactionState";

        bool Active { get; }
    }
}
