namespace EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Model;

using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Api;

public class InteractionStateModel : InteractionState
{
    public bool Active { get; set; }
    public int DistanceToPlayer { get; set; } = 10;
    public string ParticleTemplate { get; set; } = string.Empty;
    public List<InteractionItem>? List { get; set; }
    IList<InteractionItem>? InteractionState.List => List;
}
