namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage
{
    using System;
    using global::BabylonJS;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model;
    using EventHorizon.Game.Client;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;
    using MediatR;
    using EventHorizon.Game.Client.Engine.Input.Trigger;
    using EventHorizon.Game.Client.Engine.Input.Model;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Authorization;
    using EventHorizon.Html.Interop;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes;
    using System.Threading;
    using System.Timers;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Timer.Api;
    using EventHorizon.Game.Client.Core.Factory.Api;
    using EventHorizon.Game.Client.Engine.Systems.Player.Model;

    [Authorize]
    public class GamePageModel : ComponentBase
    {
        [Inject]
        public IStartup Startup { get; set; }
        [Inject]
        public IMediator Mediator { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IConfiguration Configuration { get; set; }
        [Inject]
        public IFactory<ITimerService> TimerServiceFactory { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //TimerService.SetTimer(2000, () => HandleStartGame().ConfigureAwait(false).GetAwaiter().GetResult());
                await HandleStartGame();
            }
        }

        public async Task HandleStartGame()
        {
            await StartGame_ByClient();
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

        public async Task StartGame_ByClient()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var accessToken = state.User.Claims.FirstOrDefault(a => a.Type == "access_token").Value;
            var playerId = state.User.Claims.FirstOrDefault(a => a.Type == "sub").Value;
            Startup.Setup(
                new ServerGame(),
                "game-window",
                new StandardPlayerDetails(
                    playerId,
                    accessToken
                ),
                "/login?returnUrl=/game",
                Configuration["Game:CoreServer"],
                Configuration["Game:AssetServer"],
                ""
            );
            await Startup.Run();
        }

        public void HandleStartGame_DirectBabylonJS()
        {
            var canvas = Canvas.Create(
                "game-window"
            );
            var engine = new Engine(
                canvas
            );
            var scene = new Scene(
                engine
            );
            var light0 = new PointLight(
                "Omni",
                new Vector3(
                    0,
                    2,
                    8
                ),
                scene
            );
            var box1 = Mesh.CreateBox(
                "b1",
                1.0m,
                scene
            );
            var freeCamera = new FreeCamera(
                "FreeCamera",
                new Vector3(
                    0,
                    0,
                    5
                ),
                scene
            );
            freeCamera.rotation = new Vector3(
                0,
                (decimal)System.Math.PI,
                0
            );
            scene.activeCamera = freeCamera;
            freeCamera.attachControl(
                canvas,
                true
            );

            engine.runRenderLoop(() => Task.Run(() => scene.render()));
            //engine.StartRenderLoop(
            //    scene
            //);
        }
    }
}
