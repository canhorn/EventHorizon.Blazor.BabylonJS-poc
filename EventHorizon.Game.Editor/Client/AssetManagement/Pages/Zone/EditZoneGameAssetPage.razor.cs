namespace EventHorizon.Game.Editor.Client.AssetManagement.Pages.Zone;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Zone.Services.Connection;
using EventHorizon.Zone.Systems.ClientAssets.Model;
using EventHorizon.Zone.Systems.ClientAssets.Query;
using EventHorizon.Zone.Systems.ClientAssets.Update;
using Microsoft.AspNetCore.Components;

public class EditZoneGameAssetPageModel
    : ObservableComponentBase,
        ZoneAdminServiceConnectedEventObserver
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    protected ClientAsset Model { get; private set; } = new ClientAsset();

    protected ComponentState ModelState { get; private set; } = ComponentState.Loading;
    protected string Message { get; private set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Setup();
    }

    protected async Task HandleSave()
    {
        ModelState = ComponentState.Loading;
        var result = await Mediator.Send(new UpdateClientAssetCommand(Model));
        if (!result)
        {
            Message = Localizer["Failed to Update Asset: {0}", result.ErrorCode ?? "ERROR"];
            ModelState = ComponentState.Error;
            return;
        }

        await ShowMessage(
            Localizer["Game Asset Edit"],
            Localizer["Successfully update Game Asset, navigating back to Asset List Page."]
        );
        NavigationManager.NavigateTo(AssetZoneManagementPage.Route);
    }

    protected void HandleCancel()
    {
        NavigationManager.NavigateTo(AssetZoneManagementPage.Route);
    }

    public async Task Handle(ZoneAdminServiceConnectedEvent _)
    {
        await Setup();
        await InvokeAsync(StateHasChanged);
    }

    private async Task Setup()
    {
        var gameAssetResult = await Mediator.Send(new QueryForClientAssetById(Id));

        if (!gameAssetResult)
        {
            if (gameAssetResult.ErrorCode == "CLIENT_ASSET_NOT_FOUND")
            {
                await ShowMessage(
                    Localizer["Game Asset Edit"],
                    Localizer["Game Asset was not found, redirecting to Management page..."],
                    MessageLevel.Warning
                );
                NavigationManager.NavigateTo(AssetZoneManagementPage.Route);
                return;
            }
            Message = Localizer["Failed to find Game Asset: {0}", gameAssetResult.ErrorCode];
            ModelState = ComponentState.Error;
            return;
        }

        Model = gameAssetResult.Result;
        ModelState = ComponentState.Content;
    }
}
