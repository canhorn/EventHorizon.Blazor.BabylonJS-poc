namespace EventHorizon.Game.Editor.Client.Zone.Builders;

using EventHorizon.Game.Client.Core.Builder.Api;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Model;

public class ZoneStateModelFromZoneStateBuilder
    : IBuilder<ZoneStateModel, ZoneState>
{
    public ZoneStateModel Build(ZoneState details) =>
        new()
        {
            IsLoading = details.IsLoading,
            IsPendingReload = details.IsPendingReload,
            Zone = details.Zone,
            ZoneInfo = details.ZoneInfo,
            EditorState = details.EditorState,
            ScriptErrorDetails = details.ScriptErrorDetails,
        };
}
