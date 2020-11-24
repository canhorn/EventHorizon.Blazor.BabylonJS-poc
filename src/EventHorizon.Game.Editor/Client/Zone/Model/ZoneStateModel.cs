namespace EventHorizon.Game.Editor.Client.Zone.Model
{
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Core.Services.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;

    public class ZoneStateModel
        : ZoneState
    {
        public CoreZoneDetails Zone { get; set; }
        public ZoneInfo ZoneInfo { get; set; }
        public ZoneEditorState EditorState { get; set; }
    }
}
