namespace EventHorizon.Game.Editor.Automation.Wizard.Tests;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser;
using EventHorizon.Game.Editor.Automation.Wizard.Data;
using EventHorizon.Game.Editor.Automation.Wizard.Pages;

using NUnit.Framework;

using Translations = Localization.WizardEditorPageTranslations;

public class FilterEditorTableToSpecificSystemWizardWhenUsingFilterInput
    : WebHost
{
    [Test]
    [Category("Wizard Editor Page")]
    public void Filter_Editor_Table_To_Specific_System_Wizard_When_Using_Filter_Input()
    {
        this.Login<WizardEditorPage>()
            .Header.Should.Equal(
                Translations.EN_US.Header
            )
            .WizardList.FilterArea.Filter.Set(
                WizardData.MapEditor.Name
            )
            .WizardList.GetRow(
                WizardData.MapEditor.Id
            )
            .Name.Should.Be(
                WizardData.MapEditor.Name
            );
    }
}
