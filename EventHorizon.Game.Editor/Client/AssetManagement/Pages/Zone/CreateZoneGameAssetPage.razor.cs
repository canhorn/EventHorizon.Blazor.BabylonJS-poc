namespace EventHorizon.Game.Editor.Client.AssetManagement.Pages.Zone;

using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Zone.Systems.ClientAssets.Create;
using EventHorizon.Zone.Systems.ClientAssets.Model;

using Microsoft.AspNetCore.Components;

public class CreateZoneGameAssetPageModel : EditorComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    protected ClientAsset Model { get; private set; } = new ClientAsset();

    protected async Task HandleSave()
    {
        var result = await Mediator.Send(
            new CreateClientAssetCommand(Model)
        );
        if (!result)
        {
            await ShowMessage(
                Localizer["Game Asset Error"],
                Localizer["Failed to Create Client Asset."],
                MessageLevel.Error
            );
            return;
        }

        await ShowMessage(
            Localizer["Game Asset Update"],
            Localizer[
                "Successfully created Game Asset, navigating back to Asset List Page."
            ]
        );
        NavigationManager.NavigateTo(AssetZoneManagementPage.Route);
    }

    protected void HandleCancel()
    {
        NavigationManager.NavigateTo(AssetZoneManagementPage.Route);
    }
}
