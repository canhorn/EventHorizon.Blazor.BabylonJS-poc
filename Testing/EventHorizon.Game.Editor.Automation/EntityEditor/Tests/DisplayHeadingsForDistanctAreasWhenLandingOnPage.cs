namespace EventHorizon.Game.Editor.Automation.EntityEditor.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core;
using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;

using Xunit;

public class DisplayHeadingsForDistanctAreasWhenLandingOnPage
    : WebHost
{
    [Trait("Category", "Entity Editor Page")]
    [PrettyFact(
        nameof(
            DisplayHeadingsForDistanctAreasWhenLandingOnPage
        )
    )]
    public void Test()
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
