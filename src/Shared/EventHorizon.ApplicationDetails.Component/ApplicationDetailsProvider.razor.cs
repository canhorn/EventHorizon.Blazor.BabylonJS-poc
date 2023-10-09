namespace EventHorizon.ApplicationDetails.Component;

using System;
using System.Threading.Tasks;

using EventHorizon.ApplicationDetails.Component.Api;
using EventHorizon.ApplicationDetails.Component.State;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

public class ApplicationDetailsProviderModel : ComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = null!;

    [Inject]
    public ILogger<ApplicationDetailsProvider> Logger { get; set; } = null!;

    public ApplicationDetailsState State { get; set; } =
        new StandardApplicationDetailsState();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        try
        {
            var moduleTask = await JSRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                "./version.js"
            );

            State = new StandardApplicationDetailsState
            {
                ApplicationVersion = await moduleTask.InvokeAsync<string>(
                    "APPLICATION_VERSION"
                )
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to get Application Details.");
        }
    }
}
