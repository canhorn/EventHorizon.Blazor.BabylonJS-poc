namespace EventHorizon.Game.Editor.Client.Shared.Components.Loading
{
    using System;

    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Core.Timer.Api;

    using Microsoft.AspNetCore.Components;

    public class LoadingBase
        : EditorComponentBase,
        IDisposable
    {
        [Parameter]
        public string? Text { get; set; }
        [Parameter]
        public bool HideText { get; set; }
        [Parameter]
        public bool ShowLongLoadingIndicator { get; set; }

        [Inject]
        public IFactory<ITimerService> TimerFactory { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        protected bool DisplayLongLoadingIndicator = false;
        private ITimerService? _timer;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (ShowLongLoadingIndicator)
            {
                _timer = TimerFactory.Create();
                _timer.SetTimer(
                    5000,
                    () =>
                    {
                        DisplayLongLoadingIndicator = true;
                        InvokeAsync(StateHasChanged);
                    }
                );
            }
        }

        protected void HandleReloadPage()
        {
            NavigationManager.NavigateTo(
                NavigationManager.Uri,
                true
            );
        }

        public void Dispose()
        {
            _timer?.Clear();
        }
    }
}
