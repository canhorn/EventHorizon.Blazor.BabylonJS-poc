namespace EventHorizon.Game.Editor.Automation.EntityEditor.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.EntityEditor.Pages;

using NUnit.Framework;

using Translations = Localization.EntityEditorPageTranslations;

public class DisplayHeadingsForDistanctAreasWhenLandingOnPage : WebHost
{
    [Test]
    [Category("Entity Editor Page")]
    public void Display_Headings_For_Distanct_Areas_When_Landing_On_Page()
    {
        this.Login<EntityEditorPage>()
            .Header.Should.Be(Translations.EN_US.Header)
            .ClientEntityListHeader.Should.Be(
                Translations.EN_US.ClientEntityHeader
            )
            .AgentEntityListHeader.Should.Be(
                Translations.EN_US.AgentEntityHeader
            );
    }
}
