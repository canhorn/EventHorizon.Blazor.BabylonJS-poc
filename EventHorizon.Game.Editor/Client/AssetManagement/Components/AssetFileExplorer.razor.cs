namespace EventHorizon.Game.Editor.Client.AssetManagement.Components;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Clicked;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using Microsoft.AspNetCore.Components;

public class AssetFileExplorerModel : EditorComponentBase
{
    [CascadingParameter]
    public AssetManagementState State { get; set; } = null!;

    public async Task HandleFileNodeChanged(TreeViewNodeData node)
    {
        await Mediator.Publish(new AssetFileNodeClickedEvent(node));
    }
}
