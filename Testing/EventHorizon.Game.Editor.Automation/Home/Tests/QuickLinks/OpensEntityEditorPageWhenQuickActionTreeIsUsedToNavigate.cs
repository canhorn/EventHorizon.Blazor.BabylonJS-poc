namespace EventHorizon.Game.Editor.Automation.Home.Tests.QuickLinks
{
    using System.Linq;

    using Atata;

    using EventHorizon.Game.Editor.Automation.Core;
    using EventHorizon.Game.Editor.Automation.Core.Browser;
    using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;
    using EventHorizon.Game.Editor.Automation.Home.Pages;

    using Xunit;

    public class OpensEntityEditorPageWhenQuickActionTreeIsUsedToNavigate
        : WebHost
    {
        [Trait("Category", "Quick Links")]
        [PrettyFact(
            nameof(
                OpensEntityEditorPageWhenQuickActionTreeIsUsedToNavigate
            )
        )]
        public void Test()
        {
            this.Login<HomePage>()
                .SideBar.QuickLinks.Tree.Open()
                .Children.Should.Contain(
                    a => a.Text == "Entity Editor"
                )
                .SideBar.QuickLinks.Tree.Children.First(
                    a => a.Text == "Entity Editor"
                )
                .Link.ClickAndGo<EntityEditorPage>()
                .Header.Should.Be("Entity Editor");
        }
    }
}
