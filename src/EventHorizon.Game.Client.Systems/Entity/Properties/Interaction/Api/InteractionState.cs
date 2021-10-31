namespace EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Api
{
    using System.Collections.Generic;

    using EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Model;

    public interface InteractionState
    {
        public static string NAME = "interactionState";

        bool Active { get; }
        int DistanceToPlayer { get; }
        string ParticleTemplate { get; }
        IList<InteractionItem>? List { get; }
    }
}
