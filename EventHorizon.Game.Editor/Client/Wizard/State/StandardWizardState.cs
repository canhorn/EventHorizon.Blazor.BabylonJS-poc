namespace EventHorizon.Game.Editor.Client.Wizard.State;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using EventHorizon.Game.Editor.Client.Wizard.Model;
using EventHorizon.Zone.Systems.Wizard.Model;

public class StandardWizardState : WizardState
{
    public IEnumerable<WizardMetadata> WizardList { get; private set; } = [];
    public event WizardState.OnChangeHandler OnChange = _ => Task.CompletedTask;

    private readonly Dictionary<string, WizardMetadata> _currentWizard = [];
    private readonly Dictionary<string, CommandResult<WizardStep>> _currentStep = [];
    private readonly Dictionary<string, WizardData> _currentData = [];

    public string CurrentWizardId(string context)
    {
        if (_currentWizard.TryGetValue(context, out var value))
        {
            return value.Id;
        }

        return string.Empty;
    }

    public CommandResult<WizardStep> CurrentStep(string context)
    {
        if (_currentStep.TryGetValue(context, out var value))
        {
            return value;
        }

        return new CommandResult<WizardStep>(WizardErrorCodes.WIZARD_NOT_STARTED);
    }

    public WizardData CurrentData(string context)
    {
        if (_currentData.TryGetValue(context, out var value))
        {
            return value;
        }

        return [];
    }

    private WizardMetadata? CurrentWizardMetadata(string context)
    {
        if (_currentWizard.TryGetValue(context, out var value))
        {
            return value;
        }

        return null;
    }

    public Task SetWizardList(IEnumerable<WizardMetadata> wizardList)
    {
        WizardList = wizardList.ToList();
        return OnChange.Invoke(new("wizard_state", WizardChangeReasons.WIZARD_LIST_UPDATED));
    }

    public async Task<StandardCommandResult> Next(string context)
    {
        var currentWizard = CurrentWizardMetadata(context);
        var currentStep = CurrentStep(context);
        if (currentWizard.IsNull())
        {
            return WizardErrorCodes.WIZARD_NOT_STARTED;
        }
        else if (string.IsNullOrWhiteSpace(currentStep.Result.NextStep))
        {
            return WizardErrorCodes.WIZARD_IS_COMPLETED;
        }
        else if (
            currentWizard.StepList.TryGetItem(
                obj => obj.Id == currentStep.Result.NextStep,
                out var nextStep
            )
        )
        {
            _currentStep[context] = nextStep;
            await OnChange.Invoke(new(context, WizardChangeReasons.WIZARD_STEP_SET_TO_NEXT));
            return new();
        }

        return WizardErrorCodes.WIZARD_STEP_NOT_FOUND;
    }

    public async Task<StandardCommandResult> Previous(string context)
    {
        var currentWizard = CurrentWizardMetadata(context);
        var currentStep = CurrentStep(context);
        if (currentWizard.IsNull())
        {
            return WizardErrorCodes.WIZARD_NOT_STARTED;
        }
        else if (string.IsNullOrWhiteSpace(currentStep.Result.PreviousStep))
        {
            return WizardErrorCodes.WIZARD_NO_PREVIOUS_STEP;
        }
        else if (
            currentWizard.StepList.TryGetItem(
                obj => obj.Id == currentStep.Result.PreviousStep,
                out var previousStep
            )
        )
        {
            _currentStep[context] = previousStep;
            await OnChange.Invoke(new(context, WizardChangeReasons.WIZARD_STEP_SET_TO_PREVIOUS));
            return new();
        }

        return WizardErrorCodes.WIZARD_STEP_NOT_FOUND;
    }

    public async Task<StandardCommandResult> Start(string context, WizardMetadata metadata)
    {
        if (string.IsNullOrWhiteSpace(metadata.FirstStep))
        {
            return WizardErrorCodes.WIZARD_FIRST_STEP_EMPTY;
        }
        else if (!metadata.StepList.Any())
        {
            return WizardErrorCodes.WIZARD_STEP_LIST_EMPTY;
        }
        else if (
            metadata.StepList.TryGetItem(obj => obj.Id == metadata.FirstStep, out var nextStep)
        )
        {
            _currentWizard[context] = metadata;
            foreach (var step in metadata.StepList)
            {
                step.Reset();
            }

            _currentStep[context] = nextStep;
            _currentData[context] = [];

            await OnChange.Invoke(new(context, WizardChangeReasons.WIZARD_STEP_RESET));

            return new();
        }

        return WizardErrorCodes.WIZARD_FIRST_STEP_NOT_FOUND;
    }

    public async Task<StandardCommandResult> Cancel(string context)
    {
        _currentWizard.Remove(context);

        _currentStep[context] = new CommandResult<WizardStep>(WizardErrorCodes.WIZARD_NOT_STARTED);
        _currentData[context] = [];

        await OnChange.Invoke(new(context, WizardChangeReasons.WIZARD_CANCELLED));

        return new();
    }

    public async Task<StandardCommandResult> SetToInvalid(string context, string errorCode)
    {
        var currentStep = CurrentStep(context);
        if (!currentStep)
        {
            return WizardErrorCodes.WIZARD_NOT_STARTED;
        }

        currentStep.Result.ErrorCode = errorCode;
        currentStep.Result.IsInvalid = true;

        await OnChange.Invoke(new(context, WizardChangeReasons.WIZARD_INVALID));

        return new();
    }

    public async Task<StandardCommandResult> IsProcessing(string context, bool isProcessing)
    {
        var currentStep = CurrentStep(context);
        if (!currentStep)
        {
            return WizardErrorCodes.WIZARD_NOT_STARTED;
        }

        if (isProcessing)
        {
            currentStep.Result.Reset();
        }

        currentStep.Result.IsProcessing = isProcessing;

        await OnChange.Invoke(new(context, WizardChangeReasons.WIZARD_PROCESSING_CHANGED));

        return new();
    }

    public async Task<StandardCommandResult> UpdateData(string context, WizardData data)
    {
        var currentStep = CurrentStep(context);
        if (!currentStep)
        {
            return WizardErrorCodes.WIZARD_NOT_STARTED;
        }

        _currentData[context] = data;

        await OnChange.Invoke(new(context, WizardChangeReasons.WIZARD_DATA_CHANGED));

        return new();
    }
}
