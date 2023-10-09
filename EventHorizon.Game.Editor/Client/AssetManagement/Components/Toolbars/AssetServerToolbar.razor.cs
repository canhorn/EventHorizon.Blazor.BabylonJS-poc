namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Toolbars;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.AssetManagement.Open;
using EventHorizon.Game.Editor.Client.AssetManagement.Trigger;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;

public class AssetServerToolbarBase : EditorComponentBase
{
    protected async Task HandleTriggerExportClicked()
    {
        var result = await Sender.Send(new TriggerAssetServerExportCommand());
        if (!result)
        {
            await ShowMessage(
                Localizer["Asset Server Export"],
                Localizer[
                    "Failed to Trigger Asset Server Export. ErrorCode = {0}",
                    result.ErrorCode
                ],
                MessageLevel.Error
            );
            return;
        }
    }

    protected async Task HandleImportClicked()
    {
        await Publisher.Publish(new OpenAssetServerImportFileUploaderEvent());
    }

    protected async Task HandleBackupClicked()
    {
        var result = await Sender.Send(new TriggerAssetServerBackupCommand());
        if (!result)
        {
            await ShowMessage(
                Localizer["Asset Server Backup"],
                Localizer[
                    "Failed to Trigger Asset Server Backup. ErrorCode = {0}",
                    result.ErrorCode
                ],
                MessageLevel.Error
            );
            return;
        }
    }
}
