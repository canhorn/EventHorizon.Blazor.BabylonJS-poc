namespace EventHorizon.Game.Editor.Client.LiveEditor.Components;

using System.Linq;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Authentication.Model;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

public class LiveEditorModel : ComponentBase
{
    [CascadingParameter]
    public AccessTokenModel AccessToken { get; set; } = null!;

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } =
        null!;

    public string PlayerId { get; set; } = string.Empty;
    public bool IsSetup { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Setup();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnParametersSetAsync()
    {
        await Setup();
        await base.OnParametersSetAsync();
    }

    private async Task Setup()
    {
        var state =
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
        PlayerId =
            state.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value
            ?? string.Empty;

        IsSetup =
            string.IsNullOrWhiteSpace(PlayerId).IsNotTrue()
            && string.IsNullOrWhiteSpace(AccessToken.AccessToken).IsNotTrue();
    }
}
