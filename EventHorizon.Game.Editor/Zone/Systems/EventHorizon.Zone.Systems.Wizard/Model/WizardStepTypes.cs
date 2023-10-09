namespace EventHorizon.Zone.Systems.Wizard.Model;

public static class WizardStepTypes
{
    public const string CaptureCurrentLocation = nameof(CaptureCurrentLocation);
    public const string FormInput = nameof(FormInput);
    public const string InfoText = nameof(InfoText);
    public const string LocationNavigate = nameof(LocationNavigate);
    public const string Navigate = nameof(Navigate);
    public const string Null = nameof(Null);
    public const string ServerProcess = nameof(ServerProcess);
    public const string TextInput = nameof(TextInput);
    public const string TriggerReloadingStateEvent = nameof(
        TriggerReloadingStateEvent
    );
    public const string WaitForActivityEvent = nameof(WaitForActivityEvent);
}
