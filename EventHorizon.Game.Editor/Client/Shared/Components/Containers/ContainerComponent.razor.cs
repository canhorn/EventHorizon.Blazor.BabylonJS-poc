namespace EventHorizon.Game.Editor.Client.Shared.Components.Containers
{
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using Microsoft.AspNetCore.Components;

    public partial class ContainerComponent
        : ComponentBase
    {
        [Parameter]
        public ComponentState State { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; } = null!;

        [Parameter]
        public string Error { get; set; } = null!;

        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
    }
}
