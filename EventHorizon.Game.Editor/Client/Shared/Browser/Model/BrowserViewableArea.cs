namespace EventHorizon.Game.Editor.Client.Shared.Components.Window
{
    using EventHorizon.Blazor.Interop;

    public class BrowserViewableArea
        : ViewableArea
    {
        public int InnerWidth => EventHorizonBlazorInterop.Get<int>("window", "innerWidth");
        public int InnerHeight => EventHorizonBlazorInterop.Get<int>("window", "innerHeight");
    }
}
