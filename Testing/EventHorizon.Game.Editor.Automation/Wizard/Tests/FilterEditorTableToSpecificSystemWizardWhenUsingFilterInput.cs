namespace EventHorizon.Game.Editor.Automation.Wizard.Tests
{
    using Atata;

    using EventHorizon.Game.Editor.Automation.Core.Browser;
    using EventHorizon.Game.Editor.Automation.Home.Tests;
    using EventHorizon.Game.Editor.Automation.IdentityServer.Data;
    using EventHorizon.Game.Editor.Automation.Wizard.Data;
    using EventHorizon.Game.Editor.Automation.Wizard.Pages;

    using Xunit;

    public class FilterEditorTableToSpecificSystemWizardWhenUsingFilterInput
        : WebHost
    {
        [Trait("Category", "Wizard Editor Page")]
        [PrettyFact(
            nameof(
                FilterEditorTableToSpecificSystemWizardWhenUsingFilterInput
            )
        )]
        public void Test()
        {
            this.Login<WizardEditorPage>(
                IdentityServerData.DefaultAdminUser
            )
                .Header.Should.Equal("Wizards")
                .WizardList.FilterArea.Filter.Set(
                    WizardData.MapEditor.Name
                )
                .WizardList.GetRow(WizardData.MapEditor.Id)
                .Name.Should.Be(WizardData.MapEditor.Name);
        }
    }
}
