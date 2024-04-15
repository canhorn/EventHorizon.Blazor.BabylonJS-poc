namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Components.Providers;

using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Api;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Pages;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
using EventHorizon.Game.Editor.Model;
using EventHorizon.Zone.Systems.ArtifactManagement.Finished;
using Microsoft.AspNetCore.Components;

public class ZoneServerLifecycleProviderBase
    : ObservableComponentBase,
        ZoneServerBackupFinishedEventObserver,
        ZoneServerExportFinishedEventObserver,
        ZoneServerImportFinishedEventObserver
{
    [CascadingParameter]
    public ZoneArtifactManagementState State { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public GamePlatformServiceSettings Settings { get; set; } = null!;

    public async Task Handle(ZoneServerExportFinishedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Zone Server Export"],
                Localizer["Successfully Exported Assets for '{0}'.", args.ReferenceId]
            )
        );

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("browser")))
        {
            await EventHorizonBlazorInterop.RunScript(
                "OpenInNewTab",
                "window.open($args.url, '_blank');",
                new { url = args.ExportUrl }
            );
        }
    }

    public async Task Handle(ZoneServerBackupFinishedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Zone Server Backup"],
                Localizer[
                    "Successfully created Backup of Zone Server for reference '{0}'.",
                    args.ReferenceId
                ]
            )
        );

        if (args.ReferenceId == State.BackupReferenceId)
        {
            if (
                !NavigationManager
                    .Uri.ToLowerInvariant()
                    .EndsWith(ZoneServerBackupArtifactsPage.Route)
            )
            {
                NavigationManager.NavigateTo(ZoneServerBackupArtifactsPage.Route);
            }
        }
    }

    public async Task Handle(ZoneServerImportFinishedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Zone Server Import"],
                Localizer["Successfully Import Assets for '{0}'.", args.ReferenceId]
            )
        );
    }
}
