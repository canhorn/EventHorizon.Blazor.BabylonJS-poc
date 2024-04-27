namespace EventHorizon.Game.Editor.Client.Wizard.Api;

using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Wizard.Model;

public interface WizardState
{
    IEnumerable<WizardMetadata> WizardList { get; }

    Task SetWizardList(IEnumerable<WizardMetadata> wizardList);

    string CurrentWizardId(string context);
    CommandResult<WizardStep> CurrentStep(string context);
    WizardData CurrentData(string context);

    Task<StandardCommandResult> Start(string context, WizardMetadata metadata);
    Task<StandardCommandResult> Next(string context);
    Task<StandardCommandResult> Previous(string context);
    Task<StandardCommandResult> Cancel(string context);
    Task<StandardCommandResult> SetToInvalid(string context, string errorCode);
    Task<StandardCommandResult> IsProcessing(string context, bool isProcessing);

    Task<StandardCommandResult> UpdateData(string context, WizardData data);

    delegate Task OnChangeHandler(WizardStateChangeArgs args);
    event OnChangeHandler OnChange;
}
