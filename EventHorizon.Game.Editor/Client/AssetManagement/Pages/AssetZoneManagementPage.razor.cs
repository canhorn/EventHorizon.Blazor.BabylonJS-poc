namespace EventHorizon.Game.Editor.Client.AssetManagement.Pages;

using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.AssetManagement.Pages.Zone;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Zone.Services.Connection;
using EventHorizon.Zone.Systems.ClientAssets.Delete;
using EventHorizon.Zone.Systems.ClientAssets.Model;
using EventHorizon.Zone.Systems.ClientAssets.Query;
using Microsoft.AspNetCore.Components;

public class AssetZoneManagementPageModel
    : ObservableComponentBase,
        ZoneAdminServiceConnectedEventObserver
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    protected IEnumerable<ClientAsset> GameAssetList { get; private set; } =
        new List<ClientAsset>();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Setup();
    }

    private async Task Setup()
    {
        var result = await Mediator.Send(new QueryForAllClientAssets());

        if (!result)
        {
            return;
        }

        GameAssetList = result.Result;
    }

    protected void HandleEditGameAsset(string id)
    {
        NavigationManager.NavigateTo(EditZoneGameAssetPage.Route(id));
    }

    #region Delete Prompt
    protected bool IsDeleteConfirmOpen { get; private set; }
    protected ClientAsset DeleteModel { get; private set; } = new();

    protected void HandleDeleteGameAsset(ClientAsset clientAsset)
    {
        DeleteModel = clientAsset;
        IsDeleteConfirmOpen = true;
    }

    protected void HandleCloseDeletePrompt()
    {
        DeleteModel = new();
        IsDeleteConfirmOpen = false;
    }

    protected async Task HandleYesDelete()
    {
        var deleteResult = await Mediator.Send(new DeleteClientAssetCommand(DeleteModel.Id));
        if (deleteResult)
        {
            await ShowMessage(
                Localizer["Delete Game Asset Confirm"],
                Localizer["Successfully Delete Game Asset: {0}", DeleteModel.Name]
            );
            HandleCloseDeletePrompt();
            await Setup();
            return;
        }
        await ShowMessage(
            Localizer["Delete Game Asset Confirm"],
            Localizer[
                "Failed to Delete Game Asset: {0} (Code = {1})",
                DeleteModel.Name,
                deleteResult.ErrorCode ?? "DELETE_CLIENT_ASSET_ERROR"
            ],
            MessageLevel.Error
        );
    }
    #endregion

    protected void HandleNewAssetClicked()
    {
        NavigationManager.NavigateTo(CreateZoneGameAssetPage.Route);
    }

    public async Task Handle(ZoneAdminServiceConnectedEvent _)
    {
        await Setup();
        await InvokeAsync(StateHasChanged);
    }
}
