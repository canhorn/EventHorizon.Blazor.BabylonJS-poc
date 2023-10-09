namespace EventHorizon.Game.Editor.Client.Zone.State;

using System;
using System.Collections.Generic;

using EventHorizon.Game.Editor.Client.Zone.Api;

public class InMemoryZoneStateCache : ZoneStateCache
{
    private readonly IDictionary<string, ZoneState> _map =
        new Dictionary<string, ZoneState>();

    public ZoneState? Active { get; private set; }

    public bool Exists(string zoneId)
    {
        return _map.ContainsKey(zoneId);
    }

    public void Remove(string zoneId)
    {
        _map.Remove(zoneId);

        if (Active.IsNotNull() && Active.Zone.Id == zoneId)
        {
            Active = null;
        }
    }

    public void Set(string zoneId, ZoneState zone)
    {
        _map[zoneId] = zone;
    }

    public void SetActive(string zoneId, Func<ZoneState> notFoundBuilder)
    {
        if (!Exists(zoneId))
        {
            Set(zoneId, notFoundBuilder());
        }

        if (_map.TryGetValue(zoneId, out var zone))
        {
            Active = zone;
            return;
        }

        throw new InvalidOperationException("Failed to set Active Zone State.");
    }
}
