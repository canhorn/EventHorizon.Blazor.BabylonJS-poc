namespace EventHorizon.Game.Editor.Automation.EntityEditor.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;

using NUnit.Framework;

public class DisplayHeadingsForDistanctAreasWhenLandingOnPage
    : WebHost
{
    [Test]
    [Category("Entity Editor Page")]
    public void Display_Headings_For_Distanct_Areas_When_Landing_On_Page()
    {
        this.Login<EntityEditorPage>()
            .ClientEntityListHeader.Should.Be(
                "Client Entity List"
            )
            .AgentEntityListHeader.Should.Be(
                "Agent Entity List"
            );
    }
}
