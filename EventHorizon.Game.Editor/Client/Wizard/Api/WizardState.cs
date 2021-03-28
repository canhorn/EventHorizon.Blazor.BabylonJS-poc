namespace EventHorizon.Game.Editor.Client.Wizard.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Zone.Systems.Wizard.Model;

    public interface WizardState
    {
        IEnumerable<WizardMetadata> WizardList { get; }

        Task SetWizardList(
            IEnumerable<WizardMetadata> wizardList
        );

        CommandResult<WizardStep> CurrentStep { get; }
        WizardData CurrentData { get; }

        Task<StandardCommandResult> Start(
            WizardMetadata metadata
        );
        Task<StandardCommandResult> Next();
        Task<StandardCommandResult> Previous();
        Task<StandardCommandResult> Cancel();

        delegate Task OnChangeHandler();
        event OnChangeHandler OnChange;
    }
}
