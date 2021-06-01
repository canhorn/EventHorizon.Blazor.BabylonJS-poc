namespace EventHorizon.Game.Editor.Client.Shared.Components.Containers
{
    using Microsoft.AspNetCore.Components;

    public partial class DisplayContainerComponent
        : ComponentBase
    {
        [Parameter]
        public ComponentState State { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;
    }
}
