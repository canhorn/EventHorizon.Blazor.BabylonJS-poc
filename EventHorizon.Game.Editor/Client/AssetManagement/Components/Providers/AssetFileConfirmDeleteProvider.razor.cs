namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Providers;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.AssetManagement.Delete;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;

using Microsoft.AspNetCore.Components;

public class AssetFileConfirmDeleteProviderModel
    : ObservableComponentBase,
        AssetFileDeleteTriggeredEventObserver
{
    private FileSystemDirectoryContent? _directoryContent;

    protected bool IsOpen { get; private set; }
    protected string Name => _directoryContent?.Name ?? string.Empty;
    protected ElementReference YesButton { get; set; }

    public async Task Handle(AssetFileDeleteTriggeredEvent args)
    {
        if (IsOpen)
        {
            await ShowMessage(
                Localizer["Asset Management - Delete"],
                Localizer["A Delete Confirmation is already active."],
                MessageLevel.Warning
            );
            return;
        }

        _directoryContent = args.DirectoryContent;
        IsOpen = true;

        await InvokeAsync(StateHasChanged);
    }

    protected async Task HandleYesClicked()
    {
        if (_directoryContent.IsNull())
        {
            IsOpen = false;
            _directoryContent = null;
            await ShowMessage(
                Localizer["Asset Management - Delete"],
                Localizer["Nothing to delete, closing modal."],
                MessageLevel.Warning
            );
            return;
        }

        var result = await Mediator.Send(
            new AssetFileDeleteDirectoryContentCommand(_directoryContent)
        );
        if (!result)
        {
            await ShowMessage(
                Localizer["Asset Management - Delete"],
                Localizer[
                    "Failed to delete Directory Content. (Code = [{0}])",
                    result.ErrorCode
                ],
                MessageLevel.Error
            );
            return;
        }

        await ShowMessage(
            Localizer["Asset Management - Delete"],
            Localizer[
                "Successfully Deleted the Directory Content '{0}'.",
                _directoryContent.Name
            ]
        );
        IsOpen = false;
        _directoryContent = null;
    }

    protected async Task HandleModalShown()
    {
        await YesButton.FocusAsync();
    }

    protected void HandleCancel()
    {
        _directoryContent = null;
        IsOpen = false;
    }
}
