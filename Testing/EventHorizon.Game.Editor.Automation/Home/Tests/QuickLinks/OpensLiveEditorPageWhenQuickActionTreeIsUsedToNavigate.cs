namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks
{
    using System.Linq;

    using Atata;

    using EventHorizon.Game.Editor.Automation.Core;
    using EventHorizon.Game.Editor.Automation.Core.Browser;
    using EventHorizon.Game.Editor.Automation.Home.Pages;
    using EventHorizon.Game.Editor.Automation.LiveEditor.Pages;

    using Xunit;

    public class OpensLiveEditorPageWhenQuickActionTreeIsUsedToNavigate
        : WebHost
    {
        [Trait("Category", "Quick Links")]
        [PrettyFact(
            nameof(
                OpensLiveEditorPageWhenQuickActionTreeIsUsedToNavigate
            )
        )]
        public void Test()
        {
            this.Login<HomePage>()
                .SideBar.QuickLinks.Tree.Open()
                .Children.Should.Contain(
                    a => a.Text == "Live Editor"
                )
                .SideBar.QuickLinks.Tree.Children.First(
                    a => a.Text == "Live Editor"
                )
                .Link.ClickAndGo<LiveEditorPage>()
                .Header.Should.Be("Live Editor");
        }
    }
}
