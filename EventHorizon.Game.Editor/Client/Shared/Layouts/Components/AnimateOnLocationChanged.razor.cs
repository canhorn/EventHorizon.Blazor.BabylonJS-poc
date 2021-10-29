namespace EventHorizon.Game.Editor.Client.Shared.Layouts.Components
{
    using System;

    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components;

    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Routing;

    public class AnimateOnLocationChangedBase
        : EditorComponentBase,
        IDisposable
    {
        private ITimerService? _timerService;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public IFactory<ITimerService> TimerServiceFactory { get; set; } = null!;

        protected string AnimationCSS { get; private set; } = "animate__animated animate__fadeIn";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            NavigationManager.LocationChanged += HandleNavigationLocationChanged;

            _timerService = TimerServiceFactory.Create();
        }

        private void HandleTimerTriggered()
        {
            AnimationCSS = "animate__animated animate__fadeIn";
            InvokeAsync(StateHasChanged);
        }

        private void HandleNavigationLocationChanged(
            object? sender,
            LocationChangedEventArgs e
        )
        {
            AnimationCSS = "--animated-display-none";
            InvokeAsync(StateHasChanged);

            _timerService?.SetTimer(100, HandleTimerTriggered);
        }

        public void Dispose()
        {
            _timerService?.Clear();
            NavigationManager.LocationChanged -= HandleNavigationLocationChanged;
        }
    }
}
