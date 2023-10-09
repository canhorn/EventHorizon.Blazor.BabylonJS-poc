namespace EventHorizon.Game.Editor.Automation.Wizard.Tests;

using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.IdentityServer.Data;
using EventHorizon.Game.Editor.Automation.Wizard.Data;
using EventHorizon.Game.Editor.Automation.Wizard.Pages;

using NUnit.Framework;

using Translations = Localization.WizardEditorPageTranslations;

public class OpenFirstStepForMapEditorWizardWhenSelectedFromList : WebHost
{
    [Test]
    [Category("Wizard Editor Page")]
    public void Open_First_Step_For_Map_Editor_Wizard_When_Selected_From_List()
    {
        this.Login<WizardEditorPage>(IdentityServerData.DefaultAdminUser)
            .Header.Should.Equal(Translations.EN_US.Header)
            .WizardList.Select(WizardData.MapEditor.Id)
            .WizardStepEditor.Name.Should.Be(
                WizardData.MapEditor.Steps.First().Name
            )
            .WizardStepEditor.Description.Should.Be(
                WizardData.MapEditor.Steps.First().Description
            );
    }
}
