namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Providers;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Changed;
using EventHorizon.Game.Editor.Client.AssetManagement.Reload;
using EventHorizon.Game.Editor.Client.Authentication.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using Microsoft.AspNetCore.Components;

public class AssetManagementProviderModel
    : ObservableComponentBase,
        AssetManagementStateChangedEventObserver,
        ForceReloadAssetManagementStateEventObserver
{
    [CascadingParameter]
    public AccessTokenModel AccessToken { get; set; } = null!;

    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public AssetManagementState State { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (AccessToken.IsFilled)
        {
            await State.Initialized(AccessToken.AccessToken);
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (AccessToken.IsFilled)
        {
            await State.Initialized(AccessToken.AccessToken);
        }
    }

    public async Task Handle(AssetManagementStateChangedEvent args)
    {
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ForceReloadAssetManagementStateEvent args)
    {
        if (AccessToken.IsFilled)
        {
            await State.Reload(AccessToken.AccessToken);
            await InvokeAsync(StateHasChanged);
        }
    }
}
