namespace EventHorizon.Game.Editor.Client.Zone.Api
{
    using EventHorizon.Game.Editor.Core.Services.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using EventHorizon.Zone.System.Server.Scripts.Model;

    public interface ZoneState
    {
        bool IsLoading { get; }
        bool IsPendingReload { get; }
        CoreZoneDetails Zone { get; }
        ZoneInfo ZoneInfo { get; }
        ZoneEditorState EditorState { get; }
        ServerScriptsErrorDetailsResponse ScriptErrorDetails { get; }
    }
}
