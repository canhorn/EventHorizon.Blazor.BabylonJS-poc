namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Components.Providers;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
using EventHorizon.Game.Server.Asset.Finished;

using Microsoft.AspNetCore.Components;

public class ArtifactManagementProviderBase
    : ObservableComponentBase,
        AssetServerBackupUploadedEventObserver,
        AssetServerExportUploadedEventObserver,
        AssetServerImportUploadedEventObserver
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    public async Task Handle(AssetServerBackupUploadedEvent args)
    {
        await Mediator.Publish(
            new ShowMessageEvent(
                Localizer["Artifact Backup Uploaded"],
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
                Localizer["Artifact Export Uploaded"],
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
                Localizer["Artifact Import Uploaded"],
                Localizer[
                    "Successfully Uploaded Import file for '{0}' service.",
                    args.Service
                ]
            )
        );
    }
}
