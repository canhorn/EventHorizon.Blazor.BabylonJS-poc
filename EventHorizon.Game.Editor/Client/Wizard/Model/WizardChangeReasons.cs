namespace EventHorizon.Game.Editor.Client.Wizard.Model;

public static class WizardChangeReasons
{
    public const string WIZARD_LIST_UPDATED = nameof(WIZARD_LIST_UPDATED);
    public const string WIZARD_DATA_CHANGED = nameof(WIZARD_DATA_CHANGED);
    public const string WIZARD_STEP_SET_TO_NEXT = nameof(WIZARD_STEP_SET_TO_NEXT);
    public const string WIZARD_STEP_SET_TO_PREVIOUS = nameof(WIZARD_STEP_SET_TO_PREVIOUS);
    public const string WIZARD_STEP_RESET = nameof(WIZARD_STEP_RESET);
    public const string WIZARD_PROCESSING_CHANGED = nameof(WIZARD_PROCESSING_CHANGED);
    public const string WIZARD_INVALID = nameof(WIZARD_INVALID);
    public const string WIZARD_CANCELLED = nameof(WIZARD_CANCELLED);
}
