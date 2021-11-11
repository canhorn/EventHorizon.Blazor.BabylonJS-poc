namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks
{
    using System.Linq;

    using Atata;

    using EventHorizon.Game.Editor.Automation.Core;
    using EventHorizon.Game.Editor.Automation.Core.Browser;
    using EventHorizon.Game.Editor.Automation.DataStorage.Pages;
    using EventHorizon.Game.Editor.Automation.Home.Pages;

    using Xunit;

    public class OpensDataStoragePageWhenQuickActionTreeIsUsedToNavigate
        : WebHost
    {
        [Trait("Category", "Quick Links")]
        [PrettyFact(
            nameof(
                OpensDataStoragePageWhenQuickActionTreeIsUsedToNavigate
            )
        )]
        public void Test()
        {
            this.Login<HomePage>()
                .SideBar.QuickLinks.Tree.Open()
                .Children.Should.Contain(
                    a => a.Text == "Data Storage"
                )
                .SideBar.QuickLinks.Tree.Children.First(
                    a => a.Text == "Data Storage"
                )
                .Link.ClickAndGo<DataStoragePage>()
                .Header.Should.Be("Data Storage Management");
        }
    }
}
