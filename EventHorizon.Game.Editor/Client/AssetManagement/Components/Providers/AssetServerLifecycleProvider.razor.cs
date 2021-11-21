namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Providers;

using System.Threading.Tasks;

using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
using EventHorizon.Game.Editor.Model;
using EventHorizon.Game.Server.Asset.Finished;

using Microsoft.AspNetCore.Components;

public class AssetServerLifecycleProviderBase
    : ObservableComponentBase,
      AssetServerBackupFinishedEventObserver,
      AssetServerBackupUploadedEventObserver,
      AssetServerExportFinishedEventObserver,
      AssetServerExportUploadedEventObserver,
      AssetServerImportUploadedEventObserver
{
    [CascadingParameter]
    public AssetManagementState State { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public GamePlatformServiceSettings Settings { get; set; } = null!;

    public async Task Handle(AssetServerExportFinishedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Asset Server Export"],
                Localizer[
                    "Successfully Exported Assets for '{0}'.",
                    args.ReferenceId
                ]
            )
        );

        if (args.ReferenceId == State.ExportReferenceId)
        {
            await EventHorizonBlazorInterop.RunScript(
                "OpenInNewTab",
                "window.open($args.url, '_blank');",
                new { url = $"{Settings.AssetServer}{args.ExportPath}" }
            );
        }
    }

    public async Task Handle(AssetServerBackupFinishedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Asset Server Backup"],
                Localizer[
                    "Successfully created Backup Assets Server for reference '{0}'.",
                    args.ReferenceId
                ]
            )
        );

        if (args.ReferenceId == State.BackupReferenceId)
        {
            NavigationManager.NavigateTo(
                "/asset/management/server/backups"
            );
        }
    }

    public async Task Handle(AssetServerBackupUploadedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Asset Server Backup Uploaded"],
                Localizer[
                    "Successfully Uploaded Backup file for '{0}' service.",
                    args.Service
                ]
            )
        );
    }

    public async Task Handle(AssetServerExportUploadedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Asset Server Export Uploaded"],
                Localizer[
                    "Successfully Uploaded Export file for '{0}' service.",
                    args.Service
                ]
            )
        );
    }

    public async Task Handle(AssetServerImportUploadedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Asset Server Import Uploaded"],
                Localizer[
                    "Successfully Uploaded Import file for '{0}' service.",
                    args.Service
                ]
            )
        );
    }
}
