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
    using MediatR;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.Extensions.Configuration;

    public class LiveEditorWindowModel
        : ComponentBase,
        IAsyncDisposable
    {
        [Parameter]
        public string AccessToken { get; set; } = string.Empty;
        [Parameter]
        public string PlayerId { get; set; } = string.Empty;

        [Inject]
        public IConfiguration Configuration { get; set; } = null!;
        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public ResizeListener ResizeListener { get; set; } = null!;
        [Inject]
        public IStartup Startup { get; set; } = null!;


        protected override async Task OnAfterRenderAsync(
            bool firstRender
        )
        {
            if (firstRender)
            {
                ResizeListener.OnResized += WindowResized;
                await StartGame();
            }

            await base.OnAfterRenderAsync(
                firstRender
            );
        }

        private async Task StartGame()
        {
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

        public async ValueTask DisposeAsync()
        {
            await Startup.Stop();
            ResizeListener.OnResized -= WindowResized;
        }

        public void HandleKeyDown(
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
        public void HandleKeyUp(
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
