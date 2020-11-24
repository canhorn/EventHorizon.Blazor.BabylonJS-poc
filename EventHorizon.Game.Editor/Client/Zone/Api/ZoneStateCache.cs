namespace EventHorizon.Game.Editor.Client.Zone.Api
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface ZoneStateCache
    {
        [MaybeNull]
        ZoneState Active { get; }

        void SetActive(
            string zoneId,
            Func<ZoneState> notFoundBuilder
        );

        ZoneState Get(
            string zoneId
        );

        void Set(
            string zoneId,
            ZoneState zone
        );

        bool Exists(
            string zoneId
        );
    }
}
