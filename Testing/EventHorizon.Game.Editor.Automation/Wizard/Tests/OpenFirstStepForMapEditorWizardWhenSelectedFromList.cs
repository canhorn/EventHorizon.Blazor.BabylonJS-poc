namespace EventHorizon.Game.Editor.Automation.Wizard.Tests
{
    using System.Linq;

    using Atata;

    using EventHorizon.Game.Editor.Automation.Core.Browser;
    using EventHorizon.Game.Editor.Automation.Home.Tests;
    using EventHorizon.Game.Editor.Automation.IdentityServer.Data;
    using EventHorizon.Game.Editor.Automation.Wizard.Data;
    using EventHorizon.Game.Editor.Automation.Wizard.Pages;

    using Xunit;

    public class OpenFirstStepForMapEditorWizardWhenSelectedFromList
        : WebHost
    {
        [Trait("Category", "Wizard Editor Page")]
        [PrettyFact(
            nameof(
                OpenFirstStepForMapEditorWizardWhenSelectedFromList
            )
        )]
        public void Test()
        {
            this.Login<WizardEditorPage>(
                IdentityServerData.DefaultAdminUser
            )
                .Header.Should.Equal("Wizards")
                .WizardList.Select(WizardData.MapEditor.Id)
                .WizardStepEditor.Name.Should.Be(
                    WizardData.MapEditor.Steps.First().Name
                )
                .WizardStepEditor.Description.Should.Be(
                    WizardData.MapEditor.Steps.First().Description
                );
        }
    }
}
