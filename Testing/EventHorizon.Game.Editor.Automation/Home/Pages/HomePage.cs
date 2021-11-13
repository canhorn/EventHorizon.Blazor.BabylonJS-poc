namespace EventHorizon.Game.Editor.Automation.Home.Pages
{
    using Atata;

    using EventHorizon.Game.Editor.Automation.Layout;

    using _ = HomePage;

    [Url("/")]
    public class HomePage : MainLayoutPage<_>
    {
        public H1<_> Header { get; private set; }

        [TestSelector("twitter-link")]
        public Link<_> TwitterLink { get; private set; }
    }
}
