namespace EventHorizon.Game.Editor.Client;

using EventHorizon.Game.Editor.Client.Wizard.Api;
using EventHorizon.Game.Editor.Client.Wizard.State;

using Microsoft.Extensions.DependencyInjection;

public static class EditorWizardExtensions
{
    public static IServiceCollection AddEditorWizard(
        this IServiceCollection services
    ) => services.AddSingleton<WizardState, StandardWizardState>();
}
