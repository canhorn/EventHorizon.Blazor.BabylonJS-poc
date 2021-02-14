namespace EventHorizon.Game.Editor.Client.LiveEditor.Components
{
    using System;
    using System.Threading.Tasks;
    using BlazorPro.BlazorSize;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Engine.Input.Model;
    using EventHorizon.Game.Client.Engine.Input.Trigger;
    using EventHorizon.Game.Client.Engine.Systems.Player.Model;
    using EventHorizon.Game.Client.Engine.Window.Resize;
    using EventHorizon.Game.Editor.Client.LiveEditor.Game;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using MediatR;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class LiveEditorWindowModel
        : ComponentBase,
        IAsyncDisposable
    {
        [Parameter]
        public string AccessToken { get; set; } = string.Empty;
        [Parameter]
        public string PlayerId { get; set; } = string.Empty;

        [Inject]
        public ILogger<LiveEditorWindowModel> Logger { get; set; } = null!;
        [Inject]
        public IConfiguration Configuration { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public ResizeListener ResizeListener { get; set; } = null!;
        [Inject]
        public IStartup Startup { get; set; } = null!;

        protected bool FailedStartup { get; set; }

        protected override async Task OnAfterRenderAsync(
            bool firstRender
        )
        {
            if (firstRender)
            {
                ResizeListener.OnResized += WindowResized;
                await StartGame();
            }
        }

        private async Task StartGame()
        {
            try
            {
                FailedStartup = false;
                // Place a slight delay on startup for background process to settle before game start.
                await Task.Delay(250);
                Startup.Setup(
                    new LiveEditorGame(),
                    "live-editor-window",
                    new StandardPlayerDetails(
                        PlayerId,
                        AccessToken
                    ),
                    "/login?returnUrl=/game",
                    Configuration["Game:CoreServer"],
                    Configuration["Game:AssetServer"],
                    ""
                );
                await Startup.Run();
            }
            catch (Exception ex)
            {
                Logger.LogError(
                    ex,
                    "Failed to Start"
                );
                await Startup.Stop();
                FailedStartup = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await Startup.Stop();
            ResizeListener.OnResized -= WindowResized;
        }

        protected void HandleReloadPage()
        {
            NavigationManager.NavigateTo(
                NavigationManager.Uri,
                true
            );
        }

        protected void HandleKeyDown(
            KeyboardEventArgs args
        )
        {
            Mediator.Send(
                new TriggerInputCommand(
                    args.Key,
                    InputTriggerType.Pressed
                )
            );
        }

        protected void HandleKeyUp(
            KeyboardEventArgs args
        )
        {
            Mediator.Send(
                new TriggerInputCommand(
                    args.Key,
                    InputTriggerType.Released
                )
            );
        }

        private void WindowResized(
            object _,
            BrowserWindowSize __
        )
        {
            Mediator.Publish(
                new SystemWindowResizedEvent()
            );
        }
    }
}
