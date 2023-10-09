namespace EventHorizon.Game.Editor.Client.AssetManagement.Components;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Clicked;
using EventHorizon.Game.Editor.Client.AssetManagement.Delete;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;

using Microsoft.AspNetCore.Components;

public class AssetFileWindowModel : EditorComponentBase
{
    [CascadingParameter]
    public AssetManagementState State { get; set; } = null!;

    public async Task HandleDirectoryContentClicked(
        FileSystemDirectoryContent directoryContent
    )
    {
        await Mediator.Publish(
            new AssetFileDirectoryContentClickedEvent(directoryContent)
        );
    }

    public async Task HandleDeleteFileDirectoryContent(
        FileSystemDirectoryContent directoryContent
    )
    {
        await Mediator.Publish(
            new AssetFileDeleteTriggeredEvent(directoryContent)
        );
    }
}
