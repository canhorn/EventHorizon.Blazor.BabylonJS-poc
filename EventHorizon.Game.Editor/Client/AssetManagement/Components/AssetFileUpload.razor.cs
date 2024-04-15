namespace EventHorizon.Game.Editor.Client.AssetManagement.Components;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Open;
using EventHorizon.Game.Editor.Client.Shared.Components;
using Microsoft.AspNetCore.Components;

public class AssetFileUploadModel : EditorComponentBase
{
    [CascadingParameter]
    public AssetManagementState State { get; set; } = null!;

    public async Task TriggerOpenForFileUpload()
    {
        await Mediator.Publish(
            new AssetOpenFileUploadTrggeredEvent(
                State.CurrentTreeViewNode,
                State.CurrentWorkingDirectory
            )
        );
    }
}
