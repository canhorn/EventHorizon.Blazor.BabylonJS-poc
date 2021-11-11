namespace EventHorizon.Game.Editor.Automation.LiveEditor.Pages
{
    using Atata;

    using EventHorizon.Game.Editor.Automation.Layout;

    using _ = LiveEditorPage;

    [Url("/live-editor")]
    public class LiveEditorPage
        : ThreeSplitLayoutPage<_>
    {
        public H1<_> Header { get; private set; }
    }
}
