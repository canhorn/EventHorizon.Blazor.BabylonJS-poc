namespace EventHorizon.Game.Editor.Client.Zone.Api;

using System;

public interface ZoneStateCache
{
    ZoneState? Active { get; }

    void SetActive(string zoneId, Func<ZoneState> notFoundBuilder);

    void Set(string zoneId, ZoneState zone);

    bool Exists(string zoneId);

    void Remove(string zoneId);
}
