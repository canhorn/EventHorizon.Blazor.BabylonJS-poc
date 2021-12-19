namespace EventHorizon.Game.Editor.Automation.EntityEditor.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;

using NUnit.Framework;

public class DisplayClientEntityToolbarWhenPageIsLoaded
    : WebHost
{
    [Test]
    [Category("Entity Editor Page")]
    public void Display_Client_Entity_Toolbar_When_Page_Is_Loaded()
    {
        this.Login<EntityEditorPage>()
            .ClientEntityToolbar.Children.Count.Should.Be(
                1
            )
            .ClientEntityToolbar.Children.Should.Contain(
                a => a.Content == "New"
            );
    }
}
