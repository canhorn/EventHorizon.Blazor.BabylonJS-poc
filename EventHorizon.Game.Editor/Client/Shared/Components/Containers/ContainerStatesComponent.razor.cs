namespace EventHorizon.Game.Editor.Client.Shared.Components.Containers
{
    using Microsoft.AspNetCore.Components;

    public partial class ContainerStatesComponent
        : ComponentBase
    {
        [Parameter]
        public ComponentState State { get; set; }

        [Parameter]
        public RenderFragment LoadingFragment { get; set; } = null!;

        [Parameter]
        public RenderFragment ContentFragment { get; set; } = null!;

        [Parameter]
        public RenderFragment ErrorFragment { get; set; } = null!;

        private RenderFragment GetComponentStateFragment()
        {
            return State switch
            {
                ComponentState.Loading => LoadingFragment,
                ComponentState.Content => ContentFragment,
                ComponentState.Error => ErrorFragment,
                _ => ErrorFragment
            };
        }
    }
}
