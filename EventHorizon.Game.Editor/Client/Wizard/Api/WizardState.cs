namespace EventHorizon.Game.Editor.Client.Wizard.Api;

using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Wizard.Model;

public interface WizardState
{
    IEnumerable<WizardMetadata> WizardList { get; }

    Task SetWizardList(IEnumerable<WizardMetadata> wizardList);

    string CurrentWizardId { get; }
    bool IsEmbedded { get; }
    CommandResult<WizardStep> CurrentStep { get; }
    WizardData CurrentData { get; }

    Task<StandardCommandResult> Start(WizardMetadata metadata, bool embedded = false);
    Task<StandardCommandResult> Next();
    Task<StandardCommandResult> Previous();
    Task<StandardCommandResult> Cancel();
    Task<StandardCommandResult> SetToInvalid(string errorCode);
    Task<StandardCommandResult> IsProcessing(bool isProcessing);

    Task<StandardCommandResult> UpdateData(WizardData data);

    delegate Task OnChangeHandler(WizardStateChangeArgs args);
    event OnChangeHandler OnChange;
}
