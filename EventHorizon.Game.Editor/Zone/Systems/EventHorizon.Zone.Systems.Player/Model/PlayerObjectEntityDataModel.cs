namespace EventHorizon.Zone.Systems.Player.Model;

using System.Collections.Generic;

public class PlayerObjectEntityDataModel : Dictionary<string, object>
{
    /// <summary>
    /// Force Set are always set when a player logs in.
    /// </summary>
    public IEnumerable<string> ForceSet { get; } = [];
}
