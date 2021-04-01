namespace EventHorizon.Game.Editor.Client.Wizard.State
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Wizard.Api;
    using EventHorizon.Game.Editor.Client.Wizard.Model;
    using EventHorizon.Zone.Systems.Wizard.Model;

    public class StandardWizardState
        : WizardState
    {
        private WizardMetadata? _currentWizard;

        public IEnumerable<WizardMetadata> WizardList { get; private set; } = new List<WizardMetadata>();

        public string CurrentWizardId { get; private set; } = string.Empty;
        public CommandResult<WizardStep> CurrentStep { get; private set; } = new CommandResult<WizardStep>(WizardErrorCodes.WIZARD_NOT_STARTED);
        public WizardData CurrentData { get; private set; } = new WizardData();

        public event WizardState.OnChangeHandler OnChange = () => Task.CompletedTask;

        public Task SetWizardList(
            IEnumerable<WizardMetadata> wizardList
        )
        {
            WizardList = wizardList.ToList();
            return OnChange.Invoke();
        }

        public async Task<StandardCommandResult> Next()
        {
            if (_currentWizard.IsNull())
            {
                return WizardErrorCodes.WIZARD_NOT_STARTED;
            }
            else if (string.IsNullOrWhiteSpace(
                CurrentStep.Result.NextStep
            ))
            {
                return WizardErrorCodes.WIZARD_IS_COMPLETED;
            }
            else if (_currentWizard.StepList.TryGetItem(
                obj => obj.Id == CurrentStep.Result.NextStep,
                out var nextStep
            ))
            {
                CurrentStep = nextStep;
                await OnChange.Invoke();
                return new();
            }

            return WizardErrorCodes.WIZARD_STEP_NOT_FOUND;
        }

        public async Task<StandardCommandResult> Previous()
        {
            if (_currentWizard.IsNull())
            {
                return WizardErrorCodes.WIZARD_NOT_STARTED;
            }
            else if (string.IsNullOrWhiteSpace(
                CurrentStep.Result.PreviousStep
            ))
            {
                return WizardErrorCodes.WIZARD_NO_PREVIOUS_STEP;
            }
            else if (_currentWizard.StepList.TryGetItem(
                obj => obj.Id == CurrentStep.Result.PreviousStep,
                out var previousStep
            ))
            {
                CurrentStep = previousStep;
                await OnChange.Invoke();
                return new();
            }

            return WizardErrorCodes.WIZARD_STEP_NOT_FOUND;
        }

        public async Task<StandardCommandResult> Start(
            WizardMetadata metadata
        )
        {
            if (string.IsNullOrWhiteSpace(
                metadata.FirstStep
            ))
            {
                return WizardErrorCodes.WIZARD_FIRST_STEP_EMPTY;
            }
            else if (!metadata.StepList.Any())
            {
                return WizardErrorCodes.WIZARD_STEP_LIST_EMPTY;
            }
            else if (metadata.StepList.TryGetItem(
                obj => obj.Id == metadata.FirstStep,
                out var nextStep
            ))
            {
                _currentWizard = metadata;
                foreach (var step in _currentWizard.StepList)
                {
                    step.Reset();
                }

                CurrentWizardId = metadata.Id;
                CurrentStep = nextStep;
                CurrentData = new WizardData();

                await OnChange.Invoke();

                return new();
            }

            return WizardErrorCodes.WIZARD_FIRST_STEP_NOT_FOUND;
        }

        public async Task<StandardCommandResult> Cancel()
        {
            _currentWizard = null;

            CurrentWizardId = string.Empty;
            CurrentStep = new CommandResult<WizardStep>(WizardErrorCodes.WIZARD_NOT_STARTED);
            CurrentData = new WizardData();

            await OnChange.Invoke();

            return new();
        }

        public async Task<StandardCommandResult> SetToInvalid(
            string errorCode
        )
        {
            if (!CurrentStep)
            {
                return WizardErrorCodes.WIZARD_NOT_STARTED;
            }

            CurrentStep.Result.ErrorCode = errorCode;
            CurrentStep.Result.IsInvalid = true;

            await OnChange.Invoke();

            return new();
        }

        public async Task<StandardCommandResult> IsProcessing(
            bool isProcessing
        )
        {
            if (!CurrentStep)
            {
                return WizardErrorCodes.WIZARD_NOT_STARTED;
            }

            if (isProcessing)
            {
                CurrentStep.Result.Reset();
            }

            CurrentStep.Result.IsProcessing = isProcessing;

            await OnChange.Invoke();

            return new();
        }
    }
}
