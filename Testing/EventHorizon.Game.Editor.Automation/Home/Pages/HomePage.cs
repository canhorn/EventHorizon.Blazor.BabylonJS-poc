namespace EventHorizon.Game.Editor.Automation.Home.Pages;

using Atata;

using EventHorizon.Game.Editor.Automation.Layout;

using _ = HomePage;

[Url("/")]
public class HomePage : MainLayoutPage<_>
{
    [TestSelector("twitter-link")]
    public Link<_> TwitterLink { get; private set; }
}
