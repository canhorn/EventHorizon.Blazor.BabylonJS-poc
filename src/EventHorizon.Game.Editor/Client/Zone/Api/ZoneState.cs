namespace EventHorizon.Game.Editor.Client.Zone.Api
{
    using EventHorizon.Game.Editor.Core.Services.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;

    public interface ZoneState
    {
        CoreZoneDetails Zone { get; }
        ZoneInfo ZoneInfo { get; }
        ZoneEditorState EditorState { get; }
    }
}
