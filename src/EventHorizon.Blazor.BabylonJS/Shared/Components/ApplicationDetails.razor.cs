namespace EventHorizon.Blazor.BabylonJS.Shared.Components;

using EventHorizon.ApplicationDetails.Component.Api;
using Microsoft.AspNetCore.Components;

public class ApplicationDetailsModel : ComponentBase
{
    [CascadingParameter]
    public ApplicationDetailsState State { get; set; } = null!;
}
