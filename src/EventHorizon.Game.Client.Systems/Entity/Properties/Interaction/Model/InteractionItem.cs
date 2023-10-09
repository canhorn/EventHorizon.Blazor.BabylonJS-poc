namespace EventHorizon.Game.Client.Systems.Entity.Properties.Interaction.Model;

using System.Collections.Generic;

public struct InteractionItem
{
    public string ScriptId { get; set; }
    public int DistanceToPlayer { get; set; }
    public IDictionary<string, object> Data { get; set; }
}
