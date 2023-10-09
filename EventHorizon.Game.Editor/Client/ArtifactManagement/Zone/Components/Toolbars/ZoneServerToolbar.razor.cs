namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Components.Toolbars;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Start;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Trigger;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Triggered;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;

public class ZoneServerToolbarBase
    : ObservableComponentBase,
        TriggredZoneServerArtifactImportEventObserver
{
    protected async Task HandleTriggerExportClicked()
    {
        var result = await Sender.Send(
            new TriggerZoneServerArtifactExportCommand()
        );
        if (!result)
        {
            await ShowMessage(
                Localizer["Zone Server Export"],
                Localizer[
                    "Failed to Trigger Zone Server Export. ErrorCode = {0}",
                    result.ErrorCode
                ],
                MessageLevel.Error
            );
            return;
        }

        await ShowMessage(
            Localizer["Zone Server Export"],
            Localizer["Successfully Triggered Zone Server Export."]
        );
    }

    protected async Task HandleImportClicked()
    {
        var result = await Sender.Send(
            new StartZoneServerArtifactImportCommand()
        );

        if (!result)
        {
            await ShowMessage(
                Localizer["Zone Server Import"],
                Localizer[
                    "Failed to Start Zone Server Import Process. ErrorCode = {0}",
                    result.ErrorCode
                ],
                MessageLevel.Error
            );
        }
    }

    protected async Task HandleBackupClicked()
    {
        var result = await Sender.Send(
            new TriggerZoneServerArtifactBackupCommand()
        );
        if (!result)
        {
            await ShowMessage(
                Localizer["Zone Server Backup"],
                Localizer[
                    "Failed to Trigger Zone Server Backup. ErrorCode = {0}",
                    result.ErrorCode
                ],
                MessageLevel.Error
            );
            return;
        }

        await ShowMessage(
            Localizer["Zone Server Backup"],
            Localizer["Successfully Triggered Zone Server Backup."]
        );
    }

    public async Task Handle(TriggredZoneServerArtifactImportEvent args)
    {
        if (!args.Success)
        {
            await ShowMessage(
                Localizer["Zone Server Import"],
                Localizer[
                    "Failed to Trigger Zone Server Import. ErrorCode = {0}",
                    args.ErrorCode
                ],
                MessageLevel.Error
            );
            return;
        }

        await ShowMessage(
            Localizer["Zone Server Import"],
            Localizer["Successfully Triggered Zone Server Import."]
        );
    }
}
